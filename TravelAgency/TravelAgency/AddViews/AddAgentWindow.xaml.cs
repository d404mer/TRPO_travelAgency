using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
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
