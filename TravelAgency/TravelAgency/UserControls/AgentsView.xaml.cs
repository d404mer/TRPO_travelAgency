﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TravelAgency.DbAcess;
using TravelAgency.EditViews;
using TravelAgency.DbAcess.Repos;
using System.Data.SqlClient;
using TravelAgency.AddViews;

namespace TravelAgency.UserControls
{
    /// <summary>
    /// Логика взаимодействия для AgentsView.xaml
    /// </summary>
    public partial class AgentsView : UserControl
    {
        private AgentRepository _agentRepository;
        private List<Agent> _allAgents;
        private Agent _selectedAgent;
        public AgentsView()
        {
            InitializeComponent();

            _agentRepository = new AgentRepository();
            _allAgents = new List<Agent>();

            // Загрузка данных при открытии окна
            this.Loaded += AgentsPanel_Loaded;
        }

        private void AgentsPanel_Loaded(object sender, RoutedEventArgs e)
        {
            LoadAgents();
        }

        private void LoadAgents()
        {
            try
            {
                _allAgents = _agentRepository.GetAllAgents();

                this.Dispatcher.Invoke(() =>
                {
                    if (AgentsDataGrid != null)
                    {
                        UpdateAgentsGrid();
                    }
                });
            }
            catch (Exception ex)
            {
                _allAgents = new List<Agent>();
                MessageBox.Show($"Ошибка загрузки агентов: {ex.Message}",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateAgentsGrid()
        {
            if (AgentsDataGrid == null || _allAgents == null) return;

            var displayAgents = _allAgents.Select(a => new
            {
                Agent_Name = a.Agent_Name,
                Name = a.Name,
                Last_Name = a.Last_Name,
                Role = a.Role,
                Phone_Number = a.Phone_Number,
                Email = a.Email
            }).ToList();

            AgentsDataGrid.ItemsSource = displayAgents;
        }

        private void AgentsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = AgentsDataGrid.SelectedItem;
            if (selectedItem != null)
            {
                var type = selectedItem.GetType();
                var agentNameProperty = type.GetProperty("Agent_Name");
                string agentName = (string)agentNameProperty.GetValue(selectedItem);

                _selectedAgent = _allAgents.FirstOrDefault(a => a.Agent_Name == agentName);
            }
            else
            {
                _selectedAgent = null;
            }
        }


        private void AgentsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = AgentsDataGrid.SelectedItem;
            if (selectedItem != null)
            {
                var type = selectedItem.GetType();
                var agentNameProperty = type.GetProperty("Agent_Name");
                string agentName = (string)agentNameProperty.GetValue(selectedItem);

                var selectedAgent = _allAgents.FirstOrDefault(a => a.Agent_Name == agentName);
                if (selectedAgent != null)
                {
                    EditAgentWindow editWindow = new EditAgentWindow(selectedAgent);
                    editWindow.ShowDialog();

                    // После закрытия обновляем список агентов
                    LoadAgents();
                }
            }
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

        // Метод для добавления нового агента
        private void AddAgentButton_Click(object sender, RoutedEventArgs e)
        {
            AddAgentWindow addAgentWindow = new AddAgentWindow();
            if (addAgentWindow.ShowDialog() == true)
            {
                LoadAgents(); // Обновляем список агентов после добавления
            }
        }

        // Метод для удаления выбранного агента
        private void DeleteAgentButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedAgent != null)
            {
                var result = MessageBox.Show($"Вы уверены, что хотите удалить агента {_selectedAgent.Agent_Name}?", "Подтверждение удаления", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    bool success = AgentRepository.DeleteAgent(_selectedAgent.Agent_Name);
                    if (success)
                    {
                        LoadAgents(); 
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при удалении агента.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите агента для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public bool DeleteAgent(string agentName)
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

        public bool AddAgent(Agent agent)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                string query = @"INSERT INTO Agents (Agent_Name, Name, Last_Name, Role, Phone_Number, Email) 
                                 VALUES (@AgentName, @Name, @LastName, @Role, @Phone, @Email)";

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
                    return rowsAffected > 0; // Возвращает true, если агент был добавлен
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при добавлении агента: {ex.Message}");
                    return false; // Возвращает false в случае ошибки
                }
            }
        LoadAgents(); 
        }
    }
}
