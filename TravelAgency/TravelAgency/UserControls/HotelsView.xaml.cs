using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TravelAgency.AddViews;
using TravelAgency.DbAcess;
using TravelAgency.DbAcess.Repos;
using TravelAgency.EditViews;

namespace TravelAgency.UserControls
{
    /// <summary>
    /// Логика взаимодействия для HotelView.xaml
    /// </summary>
    public partial class HotelView : UserControl
    {
        private HotelRepository _hotelRepository;
        private List<Hotel> _allHotels;
        private Hotel _selectedHotel;

        public HotelView()
        {
            InitializeComponent();

            _hotelRepository = new HotelRepository();
            _allHotels = new List<Hotel>();

            // Загрузка данных при открытии окна
            this.Loaded += HotelPanel_Loaded;
        }

        private void HotelPanel_Loaded(object sender, RoutedEventArgs e)
        {
            LoadHotels();
        }

        private void LoadHotels()
        {
            try
            {
                _allHotels = _hotelRepository.GetAllHotels();

                this.Dispatcher.Invoke(() =>
                {
                    if (HotelsDataGrid != null)
                    {
                        UpdateHotelsGrid();
                    }
                });
            }
            catch (Exception ex)
            {
                _allHotels = new List<Hotel>();
                MessageBox.Show($"Ошибка загрузки данных об отелях: {ex.Message}",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateHotelsGrid()
        {
            if (HotelsDataGrid == null || _allHotels == null) return;

            var displayHotels = _allHotels.Select(h => new
            {
                Hotel_ID = h.Hotel_ID,
                Hotel_Name = h.Hotel_Name,
                Stars = h.Stars,
                Price_Per_Night = h.Price_Per_Night
            }).ToList();

            HotelsDataGrid.ItemsSource = displayHotels;
        }

        private void HotelsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = HotelsDataGrid.SelectedItem;
            if (selectedItem != null)
            {
                var type = selectedItem.GetType();
                var hotelIdProperty = type.GetProperty("Hotel_ID");
                int hotelId = (int)hotelIdProperty.GetValue(selectedItem);

                _selectedHotel = _allHotels.FirstOrDefault(h => h.Hotel_ID == hotelId);
            }
            else
            {
                _selectedHotel = null;
            }
        }

        private void HotelsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = HotelsDataGrid.SelectedItem;
            if (selectedItem != null)
            {
                var type = selectedItem.GetType();
                var hotelIdProperty = type.GetProperty("Hotel_ID");
                int hotelId = (int)hotelIdProperty.GetValue(selectedItem);

                var selectedHotel = _allHotels.FirstOrDefault(h => h.Hotel_ID == hotelId);
                if (selectedHotel != null)
                {
                    // Например, здесь можно реализовать логику для редактирования отеля
                    //EditHotelWindow editWindow = new EditHotelWindow(selectedHotel);
                    //editWindow.ShowDialog();

                    // После закрытия обновляем список отелей
                    LoadHotels();
                }
            }
        }

        // Метод для добавления нового отеля
        private void AddHotelButton_Click(object sender, RoutedEventArgs e)
        {
            AddHotelWindow addHotelWindow = new AddHotelWindow();
            if (addHotelWindow.ShowDialog() == true)
            {
                LoadHotels(); // Обновляем список отелей после добавления
            }
        }

        // Метод для удаления выбранного отеля
        private void DeleteHotelButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedHotel != null)
            {
                var result = MessageBox.Show($"Вы уверены, что хотите удалить отель {_selectedHotel.Hotel_Name}?", "Подтверждение удаления", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    bool success = _hotelRepository.DeleteHotel(_selectedHotel.Hotel_ID);
                    if (success)
                    {
                        LoadHotels();
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при удалении отеля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите отель для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
