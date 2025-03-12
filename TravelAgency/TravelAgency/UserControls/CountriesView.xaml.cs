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
    /// Логика взаимодействия для CountryView.xaml
    /// </summary>
    public partial class CountryView : UserControl
    {
        private CountryRepository _countryRepository;
        private List<Country> _allCountries;
        private Country _selectedCountry;

        public CountryView()
        {
            InitializeComponent();

            _countryRepository = new CountryRepository();
            _allCountries = new List<Country>();

            // Загрузка данных при открытии окна
            this.Loaded += CountryPanel_Loaded;
        }

        private void CountryPanel_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCountries();
        }

        private void LoadCountries()
        {
            try
            {
                _allCountries = _countryRepository.GetAllCountries();

                this.Dispatcher.Invoke(() =>
                {
                    if (CountriesDataGrid != null)
                    {
                        UpdateCountriesGrid();
                    }
                });
            }
            catch (Exception ex)
            {
                _allCountries = new List<Country>();
                MessageBox.Show($"Ошибка загрузки стран: {ex.Message}",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateCountriesGrid()
        {
            if (CountriesDataGrid == null || _allCountries == null) return;

            var displayCountries = _allCountries.Select(c => new
            {
                Country_ID = c.Country_ID,
                Country_Name = c.Country_Name
            }).ToList();

            CountriesDataGrid.ItemsSource = displayCountries;
        }

        private void CountriesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = CountriesDataGrid.SelectedItem;
            if (selectedItem != null)
            {
                var type = selectedItem.GetType();
                var countryIdProperty = type.GetProperty("Country_ID");
                int countryId = (int)countryIdProperty.GetValue(selectedItem);

                _selectedCountry = _allCountries.FirstOrDefault(c => c.Country_ID == countryId);
            }
            else
            {
                _selectedCountry = null;
            }
        }

        private void CountriesDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = CountriesDataGrid.SelectedItem;
            if (selectedItem != null)
            {
                var type = selectedItem.GetType();
                var countryIdProperty = type.GetProperty("Country_ID");
                int countryId = (int)countryIdProperty.GetValue(selectedItem);

                var selectedCountry = _allCountries.FirstOrDefault(c => c.Country_ID == countryId);
                if (selectedCountry != null)
                {
                    // EditCountryWindow editWindow = new EditCountryWindow(selectedCountry);
                    // editWindow.ShowDialog();

                    // После закрытия обновляем список стран
                    LoadCountries();
                }
            }
        }

        // Метод для добавления новой страны
        private void AddCountryButton_Click(object sender, RoutedEventArgs e)
        {
            AddCountryWindow addCountryWindow = new AddCountryWindow();
            if (addCountryWindow.ShowDialog() == true)
            {
                LoadCountries(); // Обновляем список стран после добавления
            }
        }

        // Метод для удаления выбранной страны
        private void DeleteCountryButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedCountry != null)
            {
                var result = MessageBox.Show($"Вы уверены, что хотите удалить страну {_selectedCountry.Country_Name}?", "Подтверждение удаления", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    bool success = _countryRepository.DeleteCountry(_selectedCountry.Country_ID);
                    if (success)
                    {
                        LoadCountries();
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при удалении страны.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите страну для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
