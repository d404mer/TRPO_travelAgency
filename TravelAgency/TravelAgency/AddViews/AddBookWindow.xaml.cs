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
            // Проверяем, что все поля заполнены
            if (string.IsNullOrWhiteSpace(AgentIDTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, введите ID агента.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(TourIDTextBox.Text) || !int.TryParse(TourIDTextBox.Text, out var tourId))
            {
                MessageBox.Show("Неверный формат Tour ID. Пожалуйста, введите числовое значение.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Проверка на пустую или неверную дату бронирования
            if (BookingDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Пожалуйста, выберите дату бронирования.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Проверка на правильный формат Hotel ID
            int? hotelId = null;
            if (!string.IsNullOrWhiteSpace(HotelIDTextBox.Text))
            {
                if (!int.TryParse(HotelIDTextBox.Text, out var parsedHotelId))
                {
                    MessageBox.Show("Неверный формат Hotel ID. Пожалуйста, введите числовое значение.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                hotelId = parsedHotelId;
            }

            // Проверка на правильный формат цены
            decimal price = 0;
            if (!decimal.TryParse(PriceTextBox.Text, out price) || price <= 0)
            {
                MessageBox.Show("Неверный формат цены. Пожалуйста, введите положительное числовое значение.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Создание нового объекта бронирования
            var newBooking = new Booking
            {
                Agent_ID = AgentIDTextBox.Text,
                Tour_ID = tourId,
                Date_Of_Book = BookingDatePicker.SelectedDate.Value,
                Hotel_ID = hotelId,
                Price = price
            };

            // Добавление бронирования в репозиторий
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
