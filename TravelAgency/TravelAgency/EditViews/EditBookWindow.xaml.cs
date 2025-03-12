using System;
using System.Windows;
using TravelAgency.DbAcess;
using TravelAgency.DbAcess.Repos;

namespace TravelAgency.EditViews
{
    /// <summary>
    /// Логика взаимодействия для EditBookWindow.xaml
    /// </summary>
    public partial class EditBookWindow : Window
    {
        private Booking _booking;

        public EditBookWindow(Booking booking)
        {
            InitializeComponent();
            _booking = booking;
            LoadBookingData();
        }

        private void LoadBookingData()
        {
            BookIDTextBox.Text = _booking.Book_ID.ToString();
            AgentIDTextBox.Text = _booking.Agent_ID;
            TourIDTextBox.Text = _booking.Tour_ID.ToString();
            BookingDatePicker.SelectedDate = _booking.Date_Of_Book;
            HotelIDTextBox.Text = _booking.Hotel_ID.ToString();
            PriceTextBox.Text = _booking.Price.ToString("F2");
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Обновляем значения из интерфейса в объект бронирования
            _booking.Agent_ID = AgentIDTextBox.Text;
            _booking.Tour_ID = int.Parse(TourIDTextBox.Text);
            _booking.Date_Of_Book = BookingDatePicker.SelectedDate.Value;
            _booking.Hotel_ID = int.Parse(HotelIDTextBox.Text);
            _booking.Price = decimal.Parse(PriceTextBox.Text);

            // Сохраняем изменения в базе данных
            bool success = BookingRepo.UpdateBooking(_booking);

            if (success)
            {
                MessageBox.Show("Бронирование успешно обновлено.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Произошла ошибка при сохранении данных.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
