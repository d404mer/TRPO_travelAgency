using System;
using System.Windows;
using TravelAgency.DbAcess;
using TravelAgency.DbAcess.Repos;

namespace TravelAgency.AddViews
{
    public partial class AddBookingWindow : Window
    {
        public AddBookingWindow()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var newBooking = new Booking
            {
                Agent_ID = AgentIDTextBox.Text,
                Tour_ID = int.TryParse(TourIDTextBox.Text, out var tourId) ? tourId : 0,
                Date_Of_Book = BookingDatePicker.SelectedDate ?? DateTime.Now,
                Hotel_ID = int.TryParse(HotelIDTextBox.Text, out var hotelId) ? hotelId : (int?)null,
                Price = decimal.TryParse(PriceTextBox.Text, out var price) ? price : 0
            };

            bool success = BookingRepo.AddBooking(newBooking);
            if (success)
            {
                MessageBox.Show("Бронирование добавлено!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Ошибка при добавлении бронирования.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
