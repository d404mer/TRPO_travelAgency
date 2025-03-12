using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TravelAgency.DbAcess;

namespace TravelAgency.DbAcess.Repos
{
    public class ReportRepository
    {
        /// <summary>
        /// Получает список всех бронирований для формирования отчетов.
        /// </summary>
        /// <returns>Список бронирований</returns>
        public List<Booking> GetAllBookings()
        {
            List<Booking> bookings = new List<Booking>();

            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                string query = @"SELECT b.Book_ID, b.Agent_ID, b.Tour_ID, b.Date_Of_Book, b.Hotel_ID, b.Price, 
                                 a.Agent_Name, t.Tour_Name, h.Hotel_Name 
                                 FROM Bookings b
                                 LEFT JOIN Agents a ON b.Agent_ID = a.Agent_Name
                                 LEFT JOIN Tours t ON b.Tour_ID = t.Tour_ID
                                 LEFT JOIN Hotels h ON b.Hotel_ID = h.Hotel_ID";

                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Booking booking = new Booking
                            {
                                Book_ID = reader.GetInt32(0),
                                Agent_ID = reader.GetString(1),
                                Tour_ID = reader.GetInt32(2),
                                Date_Of_Book = reader.GetDateTime(3),
                                Hotel_ID = reader.IsDBNull(4) ? null : (int?)reader.GetInt32(4),
                                Price = reader.GetDecimal(5)
                            };

                            bookings.Add(booking);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при получении списка бронирований: {ex.Message}");
                    throw;
                }
            }

            return bookings;
        }

        /// <summary>
        /// Получает отчет по бронированиям для определенного агента.
        /// </summary>
        /// <param name="agentName">Имя агента</param>
        /// <returns>Список бронирований для данного агента</returns>
        public List<Booking> GetBookingsByAgent(string agentName)
        {
            List<Booking> bookings = new List<Booking>();

            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                string query = @"SELECT b.Book_ID, b.Agent_ID, b.Tour_ID, b.Date_Of_Book, b.Hotel_ID, b.Price, 
                                 a.Agent_Name, t.Tour_Name, h.Hotel_Name 
                                 FROM Bookings b
                                 LEFT JOIN Agents a ON b.Agent_ID = a.Agent_Name
                                 LEFT JOIN Tours t ON b.Tour_ID = t.Tour_ID
                                 LEFT JOIN Hotels h ON b.Hotel_ID = h.Hotel_ID
                                 WHERE a.Agent_Name = @AgentName";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@AgentName", agentName);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Booking booking = new Booking
                            {
                                Book_ID = reader.GetInt32(0),
                                Agent_ID = reader.GetString(1),
                                Tour_ID = reader.GetInt32(2),
                                Date_Of_Book = reader.GetDateTime(3),
                                Hotel_ID = reader.IsDBNull(4) ? null : (int?)reader.GetInt32(4),
                                Price = reader.GetDecimal(5)
                            };

                            bookings.Add(booking);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при получении списка бронирований для агента {agentName}: {ex.Message}");
                    throw;
                }
            }

            return bookings;
        }

        /// <summary>
        /// Получает отчет по бронированиям за определенный период.
        /// </summary>
        /// <param name="startDate">Дата начала периода</param>
        /// <param name="endDate">Дата окончания периода</param>
        /// <returns>Список бронирований за указанный период</returns>
        public List<Booking> GetBookingsByDateRange(DateTime startDate, DateTime endDate)
        {
            List<Booking> bookings = new List<Booking>();

            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                string query = @"SELECT b.Book_ID, b.Agent_ID, b.Tour_ID, b.Date_Of_Book, b.Hotel_ID, b.Price, 
                                 a.Agent_Name, t.Tour_Name, h.Hotel_Name 
                                 FROM Bookings b
                                 LEFT JOIN Agents a ON b.Agent_ID = a.Agent_Name
                                 LEFT JOIN Tours t ON b.Tour_ID = t.Tour_ID
                                 LEFT JOIN Hotels h ON b.Hotel_ID = h.Hotel_ID
                                 WHERE b.Date_Of_Book BETWEEN @StartDate AND @EndDate";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@StartDate", startDate);
                command.Parameters.AddWithValue("@EndDate", endDate);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Booking booking = new Booking
                            {
                                Book_ID = reader.GetInt32(0),
                                Agent_ID = reader.GetString(1),
                                Tour_ID = reader.GetInt32(2),
                                Date_Of_Book = reader.GetDateTime(3),
                                Hotel_ID = reader.IsDBNull(4) ? null : (int?)reader.GetInt32(4),
                                Price = reader.GetDecimal(5)
                            };

                            bookings.Add(booking);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при получении списка бронирований за период с {startDate.ToShortDateString()} по {endDate.ToShortDateString()}: {ex.Message}");
                    throw;
                }
            }

            return bookings;
        }

        /// <summary>
        /// Получает общий доход за определенный период.
        /// </summary>
        /// <param name="startDate">Дата начала периода</param>
        /// <param name="endDate">Дата окончания периода</param>
        /// <returns>Общий доход за период</returns>
        public decimal GetTotalRevenueByDateRange(DateTime startDate, DateTime endDate)
        {
            decimal totalRevenue = 0;

            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                string query = @"SELECT SUM(Price) 
                                 FROM Bookings 
                                 WHERE Date_Of_Book BETWEEN @StartDate AND @EndDate";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@StartDate", startDate);
                command.Parameters.AddWithValue("@EndDate", endDate);

                try
                {
                    connection.Open();
                    totalRevenue = (decimal)command.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при получении общего дохода за период: {ex.Message}");
                    throw;
                }
            }

            return totalRevenue;
        }

        /// <summary>
        /// Получает статистику по бронированиям для тура.
        /// </summary>
        /// <param name="tourId">ID тура</param>
        /// <returns>Список бронирований для конкретного тура</returns>
        public List<Booking> GetBookingsByTour(int tourId)
        {
            List<Booking> bookings = new List<Booking>();

            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                string query = @"SELECT b.Book_ID, b.Agent_ID, b.Tour_ID, b.Date_Of_Book, b.Hotel_ID, b.Price, 
                                 a.Agent_Name, t.Tour_Name, h.Hotel_Name 
                                 FROM Bookings b
                                 LEFT JOIN Agents a ON b.Agent_ID = a.Agent_Name
                                 LEFT JOIN Tours t ON b.Tour_ID = t.Tour_ID
                                 LEFT JOIN Hotels h ON b.Hotel_ID = h.Hotel_ID
                                 WHERE b.Tour_ID = @Tour_ID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Tour_ID", tourId);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Booking booking = new Booking
                            {
                                Book_ID = reader.GetInt32(0),
                                Agent_ID = reader.GetString(1),
                                Tour_ID = reader.GetInt32(2),
                                Date_Of_Book = reader.GetDateTime(3),
                                Hotel_ID = reader.IsDBNull(4) ? null : (int?)reader.GetInt32(4),
                                Price = reader.GetDecimal(5)
                            };

                            bookings.Add(booking);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при получении списка бронирований для тура {tourId}: {ex.Message}");
                    throw;
                }
            }

            return bookings;
        }
    }
}
