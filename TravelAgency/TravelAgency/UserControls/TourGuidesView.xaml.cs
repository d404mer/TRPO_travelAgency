using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TravelAgency.DbAcess;
using TravelAgency.DbAcess.Repos;

namespace TravelAgency.UserControls
{
    /// <summary>
    /// Логика взаимодействия для TourGuideView.xaml
    /// </summary>
    public partial class TourGuideView : UserControl
    {
        private TourGuideRepository _tourGuideRepository;
        private List<TourGuide> _allTourGuides;
        private TourGuide _selectedTourGuide;

        public TourGuideView()
        {
            InitializeComponent();

            _tourGuideRepository = new TourGuideRepository();
            _allTourGuides = new List<TourGuide>();

            // Загрузка данных при открытии окна
            this.Loaded += TourGuidePanel_Loaded;
        }

        private void TourGuidePanel_Loaded(object sender, RoutedEventArgs e)
        {
            LoadTourGuides();
        }

        private void LoadTourGuides()
        {
            try
            {
                _allTourGuides = _tourGuideRepository.GetAllTourGuides();

                this.Dispatcher.Invoke(() =>
                {
                    if (TourGuidesDataGrid != null)
                    {
                        UpdateTourGuidesGrid();
                    }
                });
            }
            catch (Exception ex)
            {
                _allTourGuides = new List<TourGuide>();
                MessageBox.Show($"Ошибка загрузки данных о связи тура и гида: {ex.Message}",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateTourGuidesGrid()
        {
            if (TourGuidesDataGrid == null || _allTourGuides == null) return;

            var displayTourGuides = _allTourGuides.Select(tg => new
            {
                Tour_ID = tg.Tour_ID,
                Guid_ID = tg.Guid_ID
            }).ToList();

            TourGuidesDataGrid.ItemsSource = displayTourGuides;
        }

        private void TourGuidesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = TourGuidesDataGrid.SelectedItem;
            if (selectedItem != null)
            {
                var type = selectedItem.GetType();
                var tourIdProperty = type.GetProperty("Tour_ID");
                int tourId = (int)tourIdProperty.GetValue(selectedItem);

                var guideIdProperty = type.GetProperty("Guid_ID");
                int guideId = (int)guideIdProperty.GetValue(selectedItem);

                _selectedTourGuide = _allTourGuides.FirstOrDefault(tg => tg.Tour_ID == tourId && tg.Guid_ID == guideId);
            }
            else
            {
                _selectedTourGuide = null;
            }
        }

        private void TourGuidesDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = TourGuidesDataGrid.SelectedItem;
            if (selectedItem != null)
            {
                var type = selectedItem.GetType();
                var tourIdProperty = type.GetProperty("Tour_ID");
                int tourId = (int)tourIdProperty.GetValue(selectedItem);

                var guideIdProperty = type.GetProperty("Guid_ID");
                int guideId = (int)guideIdProperty.GetValue(selectedItem);

                var selectedTourGuide = _allTourGuides.FirstOrDefault(tg => tg.Tour_ID == tourId && tg.Guid_ID == guideId);
                if (selectedTourGuide != null)
                {
                    // Например, здесь можно реализовать логику для редактирования связи между туром и гидом
                    //EditTourGuideWindow editWindow = new EditTourGuideWindow(selectedTourGuide);
                    //editWindow.ShowDialog();

                    // После закрытия обновляем список связей
                    LoadTourGuides();
                }
            }
        }

        // Метод для добавления новой связи
        private void AddTourGuideButton_Click(object sender, RoutedEventArgs e)
        {
            //AddTourGuideWindow addTourGuideWindow = new AddTourGuideWindow();
            //if (addTourGuideWindow.ShowDialog() == true)
            //{
            //    LoadTourGuides(); // Обновляем список связей после добавления
            //}
        }

        // Метод для удаления выбранной связи
        private void DeleteTourGuideButton_Click(object sender, RoutedEventArgs e)
        {
            //if (_selectedTourGuide != null)
            //{
            //    var result = MessageBox.Show($"Вы уверены, что хотите удалить связь тура с гидом?", "Подтверждение удаления", MessageBoxButton.YesNo);
            //    if (result == MessageBoxResult.Yes)
            //    {
            //        bool success = _tourGuideRepository.DeleteTourGuide(_selectedTourGuide.Tour_ID, _selectedTourGuide.Guid_ID);
            //        if (success)
            //        {
            //            LoadTourGuides();
            //        }
            //        else
            //        {
            //            MessageBox.Show("Ошибка при удалении связи.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            //        }
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Пожалуйста, выберите связь для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            //}
        }
    }
}
