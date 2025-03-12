using System;
using System.Windows;
using TravelAgency.DbAcess;
using TravelAgency.DbAcess.Repos;

namespace TravelAgency.AddViews
{
    public partial class AddTourWindow : Window
    {
        private TourRepository _tourRepository;

        public AddTourWindow()
        {
            InitializeComponent();
            _tourRepository = new TourRepository();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Проверка на пустые поля
            if (string.IsNullOrWhiteSpace(TourNameTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, введите название тура.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Проверка на корректность ввода ID страны
            if (!int.TryParse(CountryIDTextBox.Text, out int countryId) || countryId <= 0)
            {
                MessageBox.Show("Пожалуйста, введите корректный ID страны.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Проверка на корректность ввода времени пребывания
            if (!int.TryParse(StayTimeTextBox.Text, out int stayTime) || stayTime <= 0)
            {
                MessageBox.Show("Пожалуйста, введите корректное количество дней для пребывания.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Проверка на корректность ввода цены
            if (!decimal.TryParse(PriceTextBox.Text, out decimal price) || price <= 0)
            {
                MessageBox.Show("Пожалуйста, введите корректную цену.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Генерация уникального ID для тура
            int newTourId = GenerateUniqueTourId();

            // Создание нового объекта тура
            Tour newTour = new Tour
            {
                Tour_ID = newTourId,  // Присваиваем сгенерированный ID
                Tour_Name = TourNameTextBox.Text.Trim(),
                Country_ID = countryId,
                Stay_Time = stayTime,
                Price = price
            };

            // Добавляем тур в репозиторий
            bool success = _tourRepository.AddTour(newTour);
            if (success)
            {
                MessageBox.Show("Тур добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Ошибка при добавлении тура.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Метод для генерации уникального ID тура
        private int GenerateUniqueTourId()
        {
            Random random = new Random();
            return random.Next(1000, 9999);  // Генерация ID от 1000 до 9999
        }
    }
}
