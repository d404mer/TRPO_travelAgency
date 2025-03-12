using System;
using System.Windows;
using TravelAgency.DbAcess;
using TravelAgency.DbAcess.Repos;

namespace TravelAgency.AddViews
{
    public partial class AddHotelWindow : Window
    {
        private HotelRepository _hotelRepository;

        public AddHotelWindow()
        {
            InitializeComponent();
            _hotelRepository = new HotelRepository();  // Создаем экземпляр репозитория
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Проверка на пустые поля
            if (string.IsNullOrWhiteSpace(HotelNameTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, введите название отеля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Проверка на корректность ввода количества звезд
            if (!int.TryParse(StarsTextBox.Text, out int stars) || stars < 1 || stars > 5)
            {
                MessageBox.Show("Пожалуйста, введите корректное количество звезд отеля (от 1 до 5).", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Проверка на корректность ввода цены за ночь
            if (!decimal.TryParse(PricePerNightTextBox.Text, out decimal price) || price <= 0)
            {
                MessageBox.Show("Пожалуйста, введите корректную цену за ночь.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Убираем лишние пробелы по бокам
            string hotelName = HotelNameTextBox.Text.Trim();

            // Создание нового объекта отеля
            var newHotel = new Hotel
            {
                Hotel_Name = hotelName,
                Stars = stars,
                Price_Per_Night = price
            };

            // Вызываем метод репозитория для добавления отеля
            bool success = _hotelRepository.AddHotel(newHotel);
            if (success)
            {
                MessageBox.Show("Отель добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Ошибка при добавлении отеля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
