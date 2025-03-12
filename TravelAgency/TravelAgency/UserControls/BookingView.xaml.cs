using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TravelAgency.DbAcess;
using TravelAgency.EditViews;
using TravelAgency.DbAcess.Repos;
using System.Data.SqlClient;
using TravelAgency.AddViews;

namespace TravelAgency.UserControls
{
    /// <summary>
    /// Логика взаимодействия для BookingView.xaml
    /// </summary>
    public partial class BookingView : UserControl
    {
        private BookingRepo _bookingRepository;
        private List<Booking> _allBookings;
        private Booking _selectedBooking;

        public BookingView()
        {
            InitializeComponent();

            _bookingRepository = new BookingRepo();
            _allBookings = new List<Booking>();

            // Загрузка данных при открытии окна
            this.Loaded += BookingPanel_Loaded;
        }

        private void BookingPanel_Loaded(object sender, RoutedEventArgs e)
        {
            LoadBookings();
        }

        private void LoadBookings()
        {
            try
            {
                _allBookings = _bookingRepository.GetAllBookings();

                this.Dispatcher.Invoke(() =>
                {
                    if (BookingsDataGrid != null)
                    {
                        UpdateBookingsGrid();
                    }
                });
            }
            catch (Exception ex)
            {
                _allBookings = new List<Booking>();
                MessageBox.Show($"Ошибка загрузки бронирований: {ex.Message}",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateBookingsGrid()
        {
            if (BookingsDataGrid == null || _allBookings == null) return;

            var displayBookings = _allBookings.Select(b => new
            {
                Book_ID = b.Book_ID,
                Agent_ID = b.Agent_ID,
                Tour_ID = b.Tour_ID,
                Date_Of_Book = b.Date_Of_Book,
                Hotel_ID = b.Hotel_ID,
                Price = b.Price
            }).ToList();

            BookingsDataGrid.ItemsSource = displayBookings;
        }

        private void BookingsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = BookingsDataGrid.SelectedItem;
            if (selectedItem != null)
            {
                var type = selectedItem.GetType();
                var bookIdProperty = type.GetProperty("Book_ID");
                int bookId = (int)bookIdProperty.GetValue(selectedItem);

                _selectedBooking = _allBookings.FirstOrDefault(b => b.Book_ID == bookId);
            }
            else
            {
                _selectedBooking = null;
            }
        }

        private void BookingsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = BookingsDataGrid.SelectedItem;
            if (selectedItem != null)
            {
                var type = selectedItem.GetType();
                var bookIdProperty = type.GetProperty("Book_ID");
                int bookId = (int)bookIdProperty.GetValue(selectedItem);

                var selectedBooking = _allBookings.FirstOrDefault(b => b.Book_ID == bookId);
                if (selectedBooking != null)
                {
                    EditBookWindow editWindow = new EditBookWindow(selectedBooking);
                    editWindow.ShowDialog();

                    // После закрытия обновляем список бронирований
                    LoadBookings();
                }
            }
        }

        // Метод для добавления нового бронирования
        private void AddBookingButton_Click(object sender, RoutedEventArgs e)
        {
            AddBookingWindow addBookingWindow = new AddBookingWindow();
            if (addBookingWindow.ShowDialog() == true)
            {
                LoadBookings(); // Обновляем список бронирований после добавления
            }
        }

        // Метод для удаления выбранного бронирования
        private void DeleteBookingButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedBooking != null)
            {
                var result = MessageBox.Show($"Вы уверены, что хотите удалить бронирование {_selectedBooking.Book_ID}?", "Подтверждение удаления", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    bool success = BookingRepo.DeleteBooking(_selectedBooking.Book_ID);
                    if (success)
                    {
                        LoadBookings();
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при удалении бронирования.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите бронирование для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
