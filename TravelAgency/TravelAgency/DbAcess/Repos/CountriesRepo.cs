using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.DbAcess;

namespace TravelAgency.DbAcess.Repos
{
    public class CountryRepository
    {
        /// <summary>
        /// Получает все страны из базы данных.
        /// </summary>
        /// <returns>Список стран</returns>
        public List<Country> GetAllCountries()
        {
            List<Country> countries = new List<Country>();

            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                string query = "SELECT Country_ID, Country_Name FROM Countries";
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Country country = new Country
                            {
                                Country_ID = reader.GetInt32(0),
                                Country_Name = reader.GetString(1)
                            };
                            countries.Add(country);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при получении списка стран: {ex.Message}");
                    throw;
                }
            }

            return countries;
        }

        /// <summary>
        /// Получает страну по её ID.
        /// </summary>
        /// <param name="countryId">ID страны</param>
        /// <returns>Страна</returns>
        public Country GetCountryById(int countryId)
        {
            Country country = null;

            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                string query = "SELECT Country_ID, Country_Name FROM Countries WHERE Country_ID = @Country_ID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Country_ID", countryId);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            country = new Country
                            {
                                Country_ID = reader.GetInt32(0),
                                Country_Name = reader.GetString(1)
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при получении страны: {ex.Message}");
                    throw;
                }
            }

            return country;
        }

        /// <summary>
        /// Добавляет новую страну в базу данных.
        /// </summary>
        /// <param name="country">Информация о стране</param>
        /// <returns>True, если добавлено успешно, иначе False</returns>
        public bool AddCountry(Country country)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                string query = @"INSERT INTO Countries (Country_Name) 
                                 VALUES (@Country_Name)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Country_Name", country.Country_Name);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при добавлении страны: {ex.Message}");
                    return false;
                }
            }
        }

        /// <summary>
        /// Обновляет информацию о стране в базе данных.
        /// </summary>
        /// <param name="country">Обновленные данные о стране</param>
        /// <returns>True, если обновлено успешно, иначе False</returns>
        public bool UpdateCountry(Country country)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                string query = @"UPDATE Countries 
                                 SET Country_Name = @Country_Name 
                                 WHERE Country_ID = @Country_ID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Country_ID", country.Country_ID);
                command.Parameters.AddWithValue("@Country_Name", country.Country_Name);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при обновлении страны: {ex.Message}");
                    return false;
                }
            }
        }

        /// <summary>
        /// Удаляет страну по её ID.
        /// </summary>
        /// <param name="countryId">ID страны</param>
        /// <returns>True, если удалено успешно, иначе False</returns>
        public bool DeleteCountry(int countryId)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                string query = @"DELETE FROM Countries WHERE Country_ID = @Country_ID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Country_ID", countryId);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при удалении страны: {ex.Message}");
                    return false;
                }
            }
        }
    }
}
