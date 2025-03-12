using System;
using System.Windows;
using TravelAgency.DbAcess;
using TravelAgency.DbAcess.Repos;

namespace TravelAgency.AddViews
{
    public partial class AddCountryWindow : Window
    {
        private CountryRepository _countryRepository;

        public AddCountryWindow()
        {
            InitializeComponent();
            _countryRepository = new CountryRepository();  // Создаем экземпляр репозитория
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var newCountry = new Country
            {
                Country_Name = CountryNameTextBox.Text
            };

            bool success = _countryRepository.AddCountry(newCountry);  // Вызываем метод через экземпляр репозитория
            if (success)
            {
                MessageBox.Show("Страна добавлена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Ошибка при добавлении страны.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
