using System;
using System.Windows;
using TravelAgency.DbAcess;
using TravelAgency.DbAcess.Repos;

namespace TravelAgency.EditViews
{
    public partial class HotelEditWindow : Window
    {
        private Hotel _hotel;
        private HotelRepository _hotelRepository;

        public HotelEditWindow(Hotel hotel)
        {
            InitializeComponent();
            _hotel = hotel;
            _hotelRepository = new HotelRepository();
            LoadHotelData();
        }

        private void LoadHotelData()
        {
            HotelNameTextBox.Text = _hotel.Hotel_Name;
            StarsTextBox.Text = _hotel.Stars.ToString();
            PricePerNightTextBox.Text = _hotel.Price_Per_Night.ToString();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _hotel.Hotel_Name = HotelNameTextBox.Text;
            _hotel.Stars = int.Parse(StarsTextBox.Text);
            _hotel.Price_Per_Night = decimal.Parse(PricePerNightTextBox.Text);

            _hotelRepository.UpdateHotel(_hotel);  // Вызов метода через экземпляр репозитория

            MessageBox.Show("Данные отеля обновлены!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}
