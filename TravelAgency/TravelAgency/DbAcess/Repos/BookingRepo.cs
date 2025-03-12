using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.DbAcess.Repos
{
    class BookingRepo
    {
        public List<Booking> GetAllBookings()
        {
            List<Booking> bookings = new List<Booking>();

            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                string query = @"SELECT Book_ID, Agent_ID, Tour_ID, Date_Of_Book, Hotel_ID, Price FROM Book";
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
                                Hotel_ID = reader.GetInt32(4),
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
        /// Обновляет данные о бронировании
        /// </summary>
        /// <param name="booking">Объект бронирования</param>
        /// <returns>Результат операции</returns>
        public static bool UpdateBooking(Booking booking)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                string query = @"UPDATE Book 
                                 SET Agent_ID = @AgentID, 
                                     Tour_ID = @TourID, 
                                     Date_Of_Book = @DateOfBook, 
                                     Hotel_ID = @HotelID, 
                                     Price = @Price
                                 WHERE Book_ID = @BookID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@BookID", booking.Book_ID);
                command.Parameters.AddWithValue("@AgentID", booking.Agent_ID);
                command.Parameters.AddWithValue("@TourID", booking.Tour_ID);
                command.Parameters.AddWithValue("@DateOfBook", booking.Date_Of_Book);
                command.Parameters.AddWithValue("@HotelID", booking.Hotel_ID);
                command.Parameters.AddWithValue("@Price", booking.Price);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при обновлении бронирования: {ex.Message}");
                    return false;
                }
            }
        }

        /// <summary>
        /// Добавляет новое бронирование
        /// </summary>
        /// <param name="booking">Объект бронирования</param>
        /// <returns>Результат операции</returns>
        public static bool AddBooking(Booking booking)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                // Получаем максимальный Book_ID для генерации нового ID
                int newBookId = GetNextBookId();

                // Запрос на добавление нового бронирования
                string query = @"
        INSERT INTO Book (Book_ID, Agent_ID, Tour_ID, Date_Of_Book, Hotel_ID, Price)
        VALUES (@Book_ID, @AgentID, @TourID, @DateOfBook, @HotelID, @Price);";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Book_ID", newBookId);
                command.Parameters.AddWithValue("@AgentID", booking.Agent_ID);
                command.Parameters.AddWithValue("@TourID", booking.Tour_ID);
                command.Parameters.AddWithValue("@DateOfBook", booking.Date_Of_Book);
                command.Parameters.AddWithValue("@HotelID", booking.Hotel_ID.HasValue ? (object)booking.Hotel_ID.Value : DBNull.Value);
                command.Parameters.AddWithValue("@Price", booking.Price);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0; // Возвращаем true, если бронирование было добавлено
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при добавлении бронирования: {ex.Message}");
                    return false; // Возвращаем false в случае ошибки
                }
            }
        }

        // Метод для получения следующего Book_ID
        private static int GetNextBookId()
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                string query = "SELECT ISNULL(MAX(Book_ID), 0) + 1 FROM Book"; // Получаем максимальный ID и увеличиваем на 1
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    return Convert.ToInt32(result);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при получении следующего Book_ID: {ex.Message}");
                    return 1; // Если произошла ошибка, начинаем с 1
                }
            }
        }





        /// <summary>
        /// Удаляет бронирование по идентификатору
        /// </summary>
        /// <param name="bookId">Идентификатор бронирования</param>
        /// <returns>Результат операции</returns>
        public static bool DeleteBooking(int bookId)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                string query = @"DELETE FROM Book WHERE Book_ID = @BookID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@BookID", bookId);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при удалении бронирования: {ex.Message}");
                    return false;
                }
            }
        }
    }
}
