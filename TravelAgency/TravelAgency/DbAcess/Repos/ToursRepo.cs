using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TravelAgency.DbAcess;

namespace TravelAgency.DbAcess.Repos
{
    public class TourRepository
    {
        /// <summary>
        /// Получает все туры.
        /// </summary>
        /// <returns>Список всех туров</returns>
        public List<Tour> GetAllTours()
        {
            List<Tour> tours = new List<Tour>();

            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                string query = @"SELECT Tour_ID, Tour_Name, Country_ID, Stay_Time, Price 
                                 FROM Tours";

                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Tour tour = new Tour
                            {
                                Tour_ID = reader.GetInt32(0),
                                Tour_Name = reader.GetString(1),
                                Country_ID = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2),
                                Stay_Time = reader.GetInt32(3),
                                Price = reader.GetDecimal(4)
                            };

                            tours.Add(tour);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при получении списка туров: {ex.Message}");
                    throw;
                }
            }

            return tours;
        }

        /// <summary>
        /// Получает тур по его ID.
        /// </summary>
        /// <param name="tourId">ID тура</param>
        /// <returns>Тур</returns>
        public Tour GetTourById(int tourId)
        {
            Tour tour = null;

            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                string query = @"SELECT Tour_ID, Tour_Name, Country_ID, Stay_Time, Price 
                                 FROM Tours 
                                 WHERE Tour_ID = @Tour_ID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Tour_ID", tourId);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            tour = new Tour
                            {
                                Tour_ID = reader.GetInt32(0),
                                Tour_Name = reader.GetString(1),
                                Country_ID = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2),
                                Stay_Time = reader.GetInt32(3),
                                Price = reader.GetDecimal(4)
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при получении тура с ID {tourId}: {ex.Message}");
                    throw;
                }
            }

            return tour;
        }

        /// <summary>
        /// Добавляет новый тур.
        /// </summary>
        /// <param name="tour">Объект Tour для добавления</param>
        /// <returns>True, если добавление прошло успешно, иначе False</returns>
        public bool AddTour(Tour tour)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                // Получаем максимальный Tour_ID для генерации нового ID
                int newTourId = GetNextTourId();

                string query = @"INSERT INTO Tours (Tour_ID, Tour_Name, Country_ID, Stay_Time, Price) 
                         VALUES (@Tour_ID, @Tour_Name, @Country_ID, @Stay_Time, @Price)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Tour_ID", newTourId);
                command.Parameters.AddWithValue("@Tour_Name", tour.Tour_Name);
                command.Parameters.AddWithValue("@Country_ID", tour.Country_ID.HasValue ? (object)tour.Country_ID.Value : DBNull.Value);
                command.Parameters.AddWithValue("@Stay_Time", tour.Stay_Time);
                command.Parameters.AddWithValue("@Price", tour.Price);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0; // Возвращает true, если тур был добавлен
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при добавлении тура: {ex.Message}");
                    return false; // Возвращает false в случае ошибки
                }
            }
        }

        // Метод для получения следующего Tour_ID
        private int GetNextTourId()
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                string query = "SELECT ISNULL(MAX(Tour_ID), 0) + 1 FROM Tours"; // Получаем максимальный ID и увеличиваем на 1
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    return Convert.ToInt32(result);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при получении следующего Tour_ID: {ex.Message}");
                    return 1; // Если произошла ошибка, начинаем с 1
                }
            }
        }




        /// <summary>
        /// Обновляет информацию о туре.
        /// </summary>
        /// <param name="tour">Обновленный объект Tour</param>
        /// <returns>True, если обновление прошло успешно, иначе False</returns>
        public bool UpdateTour(Tour tour)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                string query = @"UPDATE Tours 
                                 SET Tour_Name = @Tour_Name, 
                                     Country_ID = @Country_ID, 
                                     Stay_Time = @Stay_Time, 
                                     Price = @Price 
                                 WHERE Tour_ID = @Tour_ID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Tour_ID", tour.Tour_ID);
                command.Parameters.AddWithValue("@Tour_Name", tour.Tour_Name);
                command.Parameters.AddWithValue("@Country_ID", tour.Country_ID.HasValue ? (object)tour.Country_ID.Value : DBNull.Value);
                command.Parameters.AddWithValue("@Stay_Time", tour.Stay_Time);
                command.Parameters.AddWithValue("@Price", tour.Price);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0; // Возвращает true, если обновление прошло успешно
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при обновлении тура с ID {tour.Tour_ID}: {ex.Message}");
                    return false; // Возвращает false в случае ошибки
                }
            }
        }

        /// <summary>
        /// Удаляет тур по его ID.
        /// </summary>
        /// <param name="tourId">ID тура</param>
        /// <returns>True, если удаление прошло успешно, иначе False</returns>
        public bool DeleteTour(int tourId)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                string query = @"DELETE FROM Tours WHERE Tour_ID = @Tour_ID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Tour_ID", tourId);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0; // Возвращает true, если тур был удален
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при удалении тура с ID {tourId}: {ex.Message}");
                    return false; // Возвращает false в случае ошибки
                }
            }
        }
    }
}
