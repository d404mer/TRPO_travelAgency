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
    /// Логика взаимодействия для GuideView.xaml
    /// </summary>
    public partial class GuideView : UserControl
    {
        private GuideRepository _guideRepository;
        private List<Guide> _allGuides;
        private Guide _selectedGuide;

        public GuideView()
        {
            InitializeComponent();

            _guideRepository = new GuideRepository();
            _allGuides = new List<Guide>();

            // Загрузка данных при открытии окна
            this.Loaded += GuidePanel_Loaded;
        }

        private void GuidePanel_Loaded(object sender, RoutedEventArgs e)
        {
            LoadGuides();
        }

        private void LoadGuides()
        {
            try
            {
                _allGuides = _guideRepository.GetAllGuides();

                this.Dispatcher.Invoke(() =>
                {
                    if (GuidesDataGrid != null)
                    {
                        UpdateGuidesGrid();
                    }
                });
            }
            catch (Exception ex)
            {
                _allGuides = new List<Guide>();
                MessageBox.Show($"Ошибка загрузки данных о гидах: {ex.Message}",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateGuidesGrid()
        {
            if (GuidesDataGrid == null || _allGuides == null) return;

            var displayGuides = _allGuides.Select(g => new
            {
                Guid_ID = g.Guid_ID,
                Guid_Name = g.Guid_Name,
                Guid_Lastname = g.Guid_Lastname
            }).ToList();

            GuidesDataGrid.ItemsSource = displayGuides;
        }

        private void GuidesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = GuidesDataGrid.SelectedItem;
            if (selectedItem != null)
            {
                var type = selectedItem.GetType();
                var guideIdProperty = type.GetProperty("Guid_ID");
                int guideId = (int)guideIdProperty.GetValue(selectedItem);

                _selectedGuide = _allGuides.FirstOrDefault(g => g.Guid_ID == guideId);
            }
            else
            {
                _selectedGuide = null;
            }
        }

        private void GuidesDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = GuidesDataGrid.SelectedItem;
            if (selectedItem != null)
            {
                var type = selectedItem.GetType();
                var guideIdProperty = type.GetProperty("Guid_ID");
                int guideId = (int)guideIdProperty.GetValue(selectedItem);

                var selectedGuide = _allGuides.FirstOrDefault(g => g.Guid_ID == guideId);
                if (selectedGuide != null)
                {
                    // Например, здесь можно реализовать логику для редактирования гида
                    GuideEditWindow editWindow = new GuideEditWindow(selectedGuide);
                    editWindow.ShowDialog();

                    // После закрытия обновляем список гидов
                    LoadGuides();
                }
            }
        }

        // Метод для добавления нового гида
        private void AddGuideButton_Click(object sender, RoutedEventArgs e)
        {
            AddGuideWindow addGuideWindow = new AddGuideWindow();
            if (addGuideWindow.ShowDialog() == true)
            {
                LoadGuides(); // Обновляем список гидов после добавления
            }
        }

        // Метод для удаления выбранного гида
        private void DeleteGuideButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedGuide != null)
            {
                var result = MessageBox.Show($"Вы уверены, что хотите удалить гида {_selectedGuide.Guid_Name} {_selectedGuide.Guid_Lastname}?", "Подтверждение удаления", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    bool success = _guideRepository.DeleteGuide(_selectedGuide.Guid_ID);
                    if (success)
                    {
                        LoadGuides();
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при удалении гида.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите гида для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
