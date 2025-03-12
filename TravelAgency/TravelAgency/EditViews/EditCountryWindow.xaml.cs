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
    /// Логика взаимодействия для EditCountryWindow.xaml
    /// </summary>
    public partial class EditCountryWindow : Window
    {
        private Country _country;
        private CountryRepository _countryRepository;

        public EditCountryWindow(Country country)
        {
            InitializeComponent();
            _country = country;
            _countryRepository = new CountryRepository();
            LoadCountryData();
        }

        private void LoadCountryData()
        {
            CountryNameTextBox.Text = _country.Country_Name;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _country.Country_Name = CountryNameTextBox.Text;

            _countryRepository.UpdateCountry(_country); 

            MessageBox.Show("Данные страны обновлены!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}
