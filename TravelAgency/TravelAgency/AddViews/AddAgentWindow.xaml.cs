using System;
using System.Text.RegularExpressions;
using System.Windows;
using TravelAgency.DbAcess.Repos;
using TravelAgency.DbAcess;

namespace TravelAgency.AddViews
{
    /// <summary>
    /// Логика взаимодействия для AddAgentWindow.xaml
    /// </summary>
    public partial class AddAgentWindow : Window
    {
        public AddAgentWindow()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Проверяем, что все поля заполнены
            if (string.IsNullOrWhiteSpace(AgentNameTextBox.Text) ||
                string.IsNullOrWhiteSpace(NameTextBox.Text) ||
                string.IsNullOrWhiteSpace(LastNameTextBox.Text) ||
                string.IsNullOrWhiteSpace(RoleTextBox.Text) ||
                string.IsNullOrWhiteSpace(PhoneTextBox.Text) ||
                string.IsNullOrWhiteSpace(EmailTextBox.Text) ||
                string.IsNullOrWhiteSpace(PasswordTextBox.Password))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Проверка формата телефона (например, должен быть числовым и длина должна быть 10 символов)
            if (!Regex.IsMatch(PhoneTextBox.Text, @"^\d{10}$"))
            {
                MessageBox.Show("Неверный формат телефона. Должно быть 10 цифр.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Проверка формата email
            if (!Regex.IsMatch(EmailTextBox.Text, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                MessageBox.Show("Неверный формат email.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Проверка пароля на минимальную длину (например, 6 символов)
            if (PasswordTextBox.Password.Length < 6)
            {
                MessageBox.Show("Пароль должен содержать хотя бы 6 символов.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Создаем нового агента
            Agent newAgent = new Agent
            {
                Agent_Name = AgentNameTextBox.Text,
                Name = NameTextBox.Text,
                Last_Name = LastNameTextBox.Text,
                Role = RoleTextBox.Text,
                Phone_Number = PhoneTextBox.Text,
                Email = EmailTextBox.Text,
                Password = PasswordTextBox.Password
            };

            // Добавляем агента в репозиторий
            bool success = AgentRepository.AddAgent(newAgent);
            if (success)
            {
                MessageBox.Show("Агент добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Ошибка при добавлении агента.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
