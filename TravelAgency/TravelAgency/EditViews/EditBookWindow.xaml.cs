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
using TravelAgency.DbAcess;
using TravelAgency.DbAcess.Repos;

namespace TravelAgency.EditViews
{
    /// <summary>
    /// Логика взаимодействия для EditBookWindow.xaml
    /// </summary>
    public partial class EditBookWindow : Window
    {
        public EditBookWindow()
        {
            InitializeComponent();
        }
        private Booking _book;

        public EditBookWindow(Booking book)
        {
            InitializeComponent();
            _book = _book;
            LoadAgentData();
        }

        private void LoadAgentData()
        {
            //AgentNameTextBox.Text = _book.Agent_Name;
            //NameTextBox.Text = _book.Name;
            //LastNameTextBox.Text = _book.Last_Name;
            //AgentTypeTextBox.Text = _book.Role;
            //PhoneTextBox.Text = _book.Phone_Number;
            //EmailTextBox.Text = _book.Email;
        }
        //private void SaveButton_Click(object sender, RoutedEventArgs e)
        //{
        //    // _agent.Agent_Name = AgentNameTextBox.Text; // Удалено, чтобы не изменять Agent_Name
        //    _agent.Name = NameTextBox.Text;
        //    _agent.Last_Name = LastNameTextBox.Text;
        //    _agent.Role = AgentTypeTextBox.Text;
        //    _agent.Phone_Number = PhoneTextBox.Text;
        //    _agent.Email = EmailTextBox.Text;

        //    // Сохраняем изменения в базу
        //    AgentRepository.UpdateAgent(_agent);

        //    MessageBox.Show("Данные агента обновлены!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        //    this.Close();
        //}
    }
}
