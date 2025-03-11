using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.DbAcess.Repos
{
    class AgentRepository
    {
        /// <summary>
        /// Получает список всех агентов из базы данных
        /// </summary>
        /// <returns>Список агентов</returns>
        public List<Agent> GetAllAgents()
        {
            List<Agent> agents = new List<Agent>();

            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                string query = @"SELECT Agent_Name, Name, Last_Name, Role, Phone_Number, Email FROM Agents";
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Agent agent = new Agent
                            {
                                Agent_Name = reader.GetString(0),
                                Name = reader.GetString(1),
                                Last_Name = reader.GetString(2),
                                Role = reader.GetString(3),
                                Phone_Number = reader.GetString(4),
                                Email = reader.GetString(5)
                            };
                            agents.Add(agent);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при получении списка агентов: {ex.Message}");
                    throw;
                }
            }

            return agents;
        }


        public static bool UpdateAgent(Agent agent)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                string query = @"UPDATE Agents 
                         SET Name = @Name, 
                             Last_Name = @LastName,
                             Role = @Role, 
                             Phone_Number = @Phone, 
                             Email = @Email
                         WHERE Agent_Name = @AgentName";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@AgentName", agent.Agent_Name);
                command.Parameters.AddWithValue("@Name", agent.Name);
                command.Parameters.AddWithValue("@LastName", agent.Last_Name);
                command.Parameters.AddWithValue("@Role", agent.Role);
                command.Parameters.AddWithValue("@Phone", agent.Phone_Number);
                command.Parameters.AddWithValue("@Email", agent.Email);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при обновлении агента: {ex.Message}");
                    return false;
                }
            }
        }

        public static bool AddAgent(Agent agent)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                string query = @"INSERT INTO Agents (Agent_Name, Name, Last_Name, Role, Phone_Number, Email, Password) 
                         VALUES (@AgentName, @Name, @LastName, @Role, @Phone, @Email, @Password)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@AgentName", agent.Agent_Name);
                command.Parameters.AddWithValue("@Name", agent.Name);
                command.Parameters.AddWithValue("@LastName", agent.Last_Name);
                command.Parameters.AddWithValue("@Role", agent.Role);
                command.Parameters.AddWithValue("@Phone", agent.Phone_Number);
                command.Parameters.AddWithValue("@Email", agent.Email);
                command.Parameters.AddWithValue("@Password", agent.Password);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0; // Возвращает true, если агент был добавлен
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при добавлении агента: {ex.Message}");
                    return false; // Возвращает false в случае ошибки
                }
            }
        }

        public static bool DeleteAgent(string agentName)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                string query = @"DELETE FROM Agents WHERE Agent_Name = @AgentName";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@AgentName", agentName);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0; // Возвращает true, если агент был удален
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при удалении агента: {ex.Message}");
                    return false; // Возвращает false в случае ошибки
                }
            }
        }

    }
}
