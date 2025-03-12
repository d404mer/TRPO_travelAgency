using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TravelAgency.DbAcess;

namespace TravelAgency.DbAcess.Repos
{
    public class TourGuideRepository
    {
        /// <summary>
        /// Получает все связи между туром и гидом.
        /// </summary>
        /// <returns>Список всех записей в таблице TourGuides</returns>
        public List<TourGuide> GetAllTourGuides()
        {
            List<TourGuide> tourGuides = new List<TourGuide>();

            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                string query = @"SELECT Tour_Guid_ID, Tour_ID, Guid_ID 
                                 FROM TourGuides";

                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TourGuide tourGuide = new TourGuide
                            {
                                Tour_Guid_ID = reader.GetInt32(0),
                                Tour_ID = reader.GetInt32(1),
                                Guid_ID = reader.GetInt32(2)
                            };

                            tourGuides.Add(tourGuide);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при получении списка гидов для туров: {ex.Message}");
                    throw;
                }
            }

            return tourGuides;
        }

        /// <summary>
        /// Получает все туры, к которым привязан конкретный гид.
        /// </summary>
        /// <param name="guidId">ID гида</param>
        /// <returns>Список туров, к которым привязан гид</returns>
        public List<TourGuide> GetTourGuidesByGuid(int guidId)
        {
            List<TourGuide> tourGuides = new List<TourGuide>();

            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                string query = @"SELECT Tour_Guid_ID, Tour_ID, Guid_ID 
                                 FROM TourGuides 
                                 WHERE Guid_ID = @Guid_ID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Guid_ID", guidId);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TourGuide tourGuide = new TourGuide
                            {
                                Tour_Guid_ID = reader.GetInt32(0),
                                Tour_ID = reader.GetInt32(1),
                                Guid_ID = reader.GetInt32(2)
                            };

                            tourGuides.Add(tourGuide);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при получении списка туров для гида с ID {guidId}: {ex.Message}");
                    throw;
                }
            }

            return tourGuides;
        }

        /// <summary>
        /// Добавляет новую связь между туром и гидом.
        /// </summary>
        /// <param name="tourGuide">Объект TourGuide для добавления</param>
        /// <returns>True, если добавление прошло успешно, иначе False</returns>
        public bool AddTourGuide(TourGuide tourGuide)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                string query = @"INSERT INTO TourGuides (Tour_ID, Guid_ID) 
                                 VALUES (@Tour_ID, @Guid_ID)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Tour_ID", tourGuide.Tour_ID);
                command.Parameters.AddWithValue("@Guid_ID", tourGuide.Guid_ID);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0; // Возвращает true, если связь была добавлена
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при добавлении связи между туром и гидом: {ex.Message}");
                    return false; // Возвращает false в случае ошибки
                }
            }
        }

        /// <summary>
        /// Удаляет связь между туром и гидом.
        /// </summary>
        /// <param name="tourGuideId">ID записи связи между туром и гидом</param>
        /// <returns>True, если удаление прошло успешно, иначе False</returns>
        public bool DeleteTourGuide(int tourGuideId)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                string query = @"DELETE FROM TourGuides WHERE Tour_Guid_ID = @Tour_Guid_ID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Tour_Guid_ID", tourGuideId);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0; // Возвращает true, если связь была удалена
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при удалении связи между туром и гидом: {ex.Message}");
                    return false; // Возвращает false в случае ошибки
                }
            }
        }

        /// <summary>
        /// Получает связь между конкретным туром и гидом.
        /// </summary>
        /// <param name="tourId">ID тура</param>
        /// <param name="guidId">ID гида</param>
        /// <returns>Запись связи между туром и гидом</returns>
        public TourGuide GetTourGuideByTourAndGuid(int tourId, int guidId)
        {
            TourGuide tourGuide = null;

            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                string query = @"SELECT Tour_Guid_ID, Tour_ID, Guid_ID 
                                 FROM TourGuides 
                                 WHERE Tour_ID = @Tour_ID AND Guid_ID = @Guid_ID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Tour_ID", tourId);
                command.Parameters.AddWithValue("@Guid_ID", guidId);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            tourGuide = new TourGuide
                            {
                                Tour_Guid_ID = reader.GetInt32(0),
                                Tour_ID = reader.GetInt32(1),
                                Guid_ID = reader.GetInt32(2)
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при получении связи для тура {tourId} и гида {guidId}: {ex.Message}");
                    throw;
                }
            }

            return tourGuide;
        }
    }
}
