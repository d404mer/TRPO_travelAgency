using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TravelAgency.DbAcess;

namespace TravelAgency.DbAcess.Repos
{
    public class GuideRepository
    {
        /// <summary>
        /// Получает всех гидов из базы данных.
        /// </summary>
        /// <returns>Список гидов</returns>
        public List<Guide> GetAllGuides()
        {
            List<Guide> guides = new List<Guide>();

            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                string query = "SELECT Guid_ID, Guid_Name, Guid_Lastname FROM Guides";
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Guide guide = new Guide
                            {
                                Guid_ID = reader.GetInt32(0),
                                Guid_Name = reader.GetString(1),
                                Guid_Lastname = reader.GetString(2)
                            };
                            guides.Add(guide);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при получении списка гидов: {ex.Message}");
                    throw;
                }
            }

            return guides;
        }

        /// <summary>
        /// Получает гида по его ID.
        /// </summary>
        /// <param name="guideId">ID гида</param>
        /// <returns>Гид</returns>
        public Guide GetGuideById(int guideId)
        {
            Guide guide = null;

            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                string query = "SELECT Guid_ID, Guid_Name, Guid_Lastname FROM Guides WHERE Guid_ID = @Guide_ID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Guide_ID", guideId);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            guide = new Guide
                            {
                                Guid_ID = reader.GetInt32(0),
                                Guid_Name = reader.GetString(1),
                                Guid_Lastname = reader.GetString(2)
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при получении гида: {ex.Message}");
                    throw;
                }
            }

            return guide;
        }

        /// <summary>
        /// Добавляет нового гида в базу данных.
        /// </summary>
        /// <param name="guide">Информация о гиде</param>
        /// <returns>True, если добавлено успешно, иначе False</returns>
        public bool AddGuide(Guide guide)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                string query = @"INSERT INTO Guides (Guid_Name, Guid_Lastname) 
                                 VALUES (@Guid_Name, @Guid_Lastname)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Guid_Name", guide.Guid_Name);
                command.Parameters.AddWithValue("@Guid_Lastname", guide.Guid_Lastname);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при добавлении гида: {ex.Message}");
                    return false;
                }
            }
        }

        /// <summary>
        /// Обновляет информацию о гиде в базе данных.
        /// </summary>
        /// <param name="guide">Обновленные данные о гиде</param>
        /// <returns>True, если обновлено успешно, иначе False</returns>
        public bool UpdateGuide(Guide guide)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                string query = @"UPDATE Guides 
                                 SET Guid_Name = @Guid_Name, 
                                     Guid_Lastname = @Guid_Lastname 
                                 WHERE Guid_ID = @Guide_ID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Guide_ID", guide.Guid_ID);
                command.Parameters.AddWithValue("@Guid_Name", guide.Guid_Name);
                command.Parameters.AddWithValue("@Guid_Lastname", guide.Guid_Lastname);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при обновлении гида: {ex.Message}");
                    return false;
                }
            }
        }

        /// <summary>
        /// Удаляет гида по его ID.
        /// </summary>
        /// <param name="guideId">ID гида</param>
        /// <returns>True, если удалено успешно, иначе False</returns>
        public bool DeleteGuide(int guideId)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                string query = @"DELETE FROM Guides WHERE Guid_ID = @Guide_ID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Guide_ID", guideId);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при удалении гида: {ex.Message}");
                    return false;
                }
            }
        }
    }
}
