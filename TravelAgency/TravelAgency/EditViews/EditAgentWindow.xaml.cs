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

namespace TravelAgency.EditViews
{
    /// <summary>
    /// Логика взаимодействия для EditAgentWindow.xaml
    /// </summary>
    public partial class EditAgentWindow : Window
    {
        public EditAgentWindow()
        {
            InitializeComponent();
        }
        private Agent _agent;

        public EditAgentWindow(Agent agent)
        {
            InitializeComponent();
            _agent = agent;
            LoadAgentData();
        }

        private void LoadAgentData()
        {
            AgentNameTextBox.Text = _agent.Agent_Name;
            AgentTypeTextBox.Text = _agent.Role;
            PhoneTextBox.Text = _agent.Phone_Number;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _agent.Agent_Name = AgentNameTextBox.Text;
            _agent.Role= AgentTypeTextBox.Text;
            _agent.Phone_Number = PhoneTextBox.Text;

            // Сохраняем изменения в базу
            AgentRepository.UpdateAgent(_agent);

            MessageBox.Show("Данные агента обновлены!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}
