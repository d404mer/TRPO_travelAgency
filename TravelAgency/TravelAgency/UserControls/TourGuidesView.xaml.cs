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
    }
}
