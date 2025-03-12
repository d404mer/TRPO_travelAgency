using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TravelAgency.DbAcess;

namespace TravelAgency.DbAcess.Repos
{
    public class HotelRepository
    {
        /// <summary>
        /// Получает все отели из базы данных.
        /// </summary>
        /// <returns>Список отелей</returns>
        public List<Hotel> GetAllHotels()
        {
            List<Hotel> hotels = new List<Hotel>();

            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                string query = "SELECT Hotel_ID, Hotel_Name, Stars, Price_Per_Night FROM Hotels";
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Hotel hotel = new Hotel
                            {
                                Hotel_ID = reader.GetInt32(0),
                                Hotel_Name = reader.GetString(1),
                                Stars = reader.GetInt32(2),
                                Price_Per_Night = reader.GetDecimal(3)
                            };
                            hotels.Add(hotel);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при получении списка отелей: {ex.Message}");
                    throw;
                }
            }

            return hotels;
        }

        /// <summary>
        /// Получает отель по его ID.
        /// </summary>
        /// <param name="hotelId">ID отеля</param>
        /// <returns>Отель</returns>
        public Hotel GetHotelById(int hotelId)
        {
            Hotel hotel = null;

            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                string query = "SELECT Hotel_ID, Hotel_Name, Stars, Price_Per_Night FROM Hotels WHERE Hotel_ID = @Hotel_ID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Hotel_ID", hotelId);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            hotel = new Hotel
                            {
                                Hotel_ID = reader.GetInt32(0),
                                Hotel_Name = reader.GetString(1),
                                Stars = reader.GetInt32(2),
                                Price_Per_Night = reader.GetDecimal(3)
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при получении отеля: {ex.Message}");
                    throw;
                }
            }

            return hotel;
        }

        /// <summary>
        /// Добавляет новый отель в базу данных.
        /// </summary>
        /// <param name="hotel">Информация об отеле</param>
        /// <returns>True, если добавлено успешно, иначе False</returns>
        public bool AddHotel(Hotel hotel)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                string query = @"INSERT INTO Hotels (Hotel_Name, Stars, Price_Per_Night) 
                                 VALUES (@Hotel_Name, @Stars, @Price_Per_Night)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Hotel_Name", hotel.Hotel_Name);
                command.Parameters.AddWithValue("@Stars", hotel.Stars);
                command.Parameters.AddWithValue("@Price_Per_Night", hotel.Price_Per_Night);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при добавлении отеля: {ex.Message}");
                    return false;
                }
            }
        }

        /// <summary>
        /// Обновляет информацию об отеле в базе данных.
        /// </summary>
        /// <param name="hotel">Обновленные данные об отеле</param>
        /// <returns>True, если обновлено успешно, иначе False</returns>
        public bool UpdateHotel(Hotel hotel)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                string query = @"UPDATE Hotels 
                                 SET Hotel_Name = @Hotel_Name, 
                                     Stars = @Stars, 
                                     Price_Per_Night = @Price_Per_Night
                                 WHERE Hotel_ID = @Hotel_ID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Hotel_ID", hotel.Hotel_ID);
                command.Parameters.AddWithValue("@Hotel_Name", hotel.Hotel_Name);
                command.Parameters.AddWithValue("@Stars", hotel.Stars);
                command.Parameters.AddWithValue("@Price_Per_Night", hotel.Price_Per_Night);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при обновлении отеля: {ex.Message}");
                    return false;
                }
            }
        }

        /// <summary>
        /// Удаляет отель по его ID.
        /// </summary>
        /// <param name="hotelId">ID отеля</param>
        /// <returns>True, если удалено успешно, иначе False</returns>
        public bool DeleteHotel(int hotelId)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                string query = @"DELETE FROM Hotels WHERE Hotel_ID = @Hotel_ID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Hotel_ID", hotelId);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при удалении отеля: {ex.Message}");
                    return false;
                }
            }
        }
    }
}
