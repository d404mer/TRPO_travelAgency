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
            var newHotel = new Hotel
            {
                Hotel_Name = HotelNameTextBox.Text,
                Stars = int.Parse(StarsTextBox.Text),
                Price_Per_Night = decimal.Parse(PricePerNightTextBox.Text)
            };

            bool success = _hotelRepository.AddHotel(newHotel);  // Вызываем метод через экземпляр репозитория
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
