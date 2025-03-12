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
    /// Логика взаимодействия для TourView.xaml
    /// </summary>
    public partial class TourView : UserControl
    {
        private TourRepository _tourRepository;
        private List<Tour> _allTours;
        private Tour _selectedTour;

        public TourView()
        {
            InitializeComponent();

            _tourRepository = new TourRepository();
            _allTours = new List<Tour>();

            // Загрузка данных при открытии окна
            this.Loaded += TourPanel_Loaded;
        }

        private void TourPanel_Loaded(object sender, RoutedEventArgs e)
        {
            LoadTours();
        }

        private void LoadTours()
        {
            try
            {
                _allTours = _tourRepository.GetAllTours();

                this.Dispatcher.Invoke(() =>
                {
                    if (ToursDataGrid != null)
                    {
                        UpdateToursGrid();
                    }
                });
            }
            catch (Exception ex)
            {
                _allTours = new List<Tour>();
                MessageBox.Show($"Ошибка загрузки данных о турах: {ex.Message}",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateToursGrid()
        {
            if (ToursDataGrid == null || _allTours == null) return;

            var displayTours = _allTours.Select(t => new
            {
                Tour_ID = t.Tour_ID,
                Tour_Name = t.Tour_Name,
                Country_ID = t.Country_ID,
                Stay_Time = t.Stay_Time,
                Price = t.Price
            }).ToList();

            ToursDataGrid.ItemsSource = displayTours;
        }

        private void ToursDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = ToursDataGrid.SelectedItem;
            if (selectedItem != null)
            {
                var type = selectedItem.GetType();
                var tourIdProperty = type.GetProperty("Tour_ID");
                int tourId = (int)tourIdProperty.GetValue(selectedItem);

                _selectedTour = _allTours.FirstOrDefault(t => t.Tour_ID == tourId);
            }
            else
            {
                _selectedTour = null;
            }
        }

        private void ToursDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = ToursDataGrid.SelectedItem;
            if (selectedItem != null)
            {
                var type = selectedItem.GetType();
                var tourIdProperty = type.GetProperty("Tour_ID");
                int tourId = (int)tourIdProperty.GetValue(selectedItem);

                var selectedTour = _allTours.FirstOrDefault(t => t.Tour_ID == tourId);
                if (selectedTour != null)
                {
                    // Например, здесь можно реализовать логику для редактирования тура
                    //EditTourWindow editWindow = new EditTourWindow(selectedTour);
                    //editWindow.ShowDialog();

                    // После закрытия обновляем список туров
                    LoadTours();
                }
            }
        }

        // Метод для добавления нового тура
        private void AddTourButton_Click(object sender, RoutedEventArgs e)
        {
            AddTourWindow addTourWindow = new AddTourWindow();
            if (addTourWindow.ShowDialog() == true)
            {
                LoadTours(); // Обновляем список туров после добавления
            }
        }

        // Метод для удаления выбранного тура
        private void DeleteTourButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedTour != null)
            {
                var result = MessageBox.Show($"Вы уверены, что хотите удалить тур {_selectedTour.Tour_Name}?",
                    "Подтверждение удаления", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    bool success = _tourRepository.DeleteTour(_selectedTour.Tour_ID);
                    if (success)
                    {
                        LoadTours();
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при удалении тура.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите тур для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
