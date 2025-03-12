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
            // Проверка, что название страны не пустое
            if (string.IsNullOrWhiteSpace(CountryNameTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, введите название страны.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Убираем лишние пробелы по бокам
            string countryName = CountryNameTextBox.Text.Trim();

            // Создание нового объекта страны
            var newCountry = new Country
            {
                Country_Name = countryName
            };

            // Вызываем метод репозитория для добавления страны
            bool success = _countryRepository.AddCountry(newCountry);
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
