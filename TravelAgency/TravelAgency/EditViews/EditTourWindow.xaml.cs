using System;
using System.Windows;
using TravelAgency.DbAcess;
using TravelAgency.DbAcess.Repos;

namespace TravelAgency.EditViews
{
    public partial class TourEditWindow : Window
    {
        private Tour _tour;
        private TourRepository _tourRepository;

        public TourEditWindow(Tour tour)
        {
            InitializeComponent();
            _tour = tour;
            _tourRepository = new TourRepository();
            LoadTourData();
        }

        private void LoadTourData()
        {
            TourNameTextBox.Text = _tour.Tour_Name;
            CountryIdTextBox.Text = _tour.Country_ID.ToString();
            StayTimeTextBox.Text = _tour.Stay_Time.ToString();
            PriceTextBox.Text = _tour.Price.ToString();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _tour.Tour_Name = TourNameTextBox.Text;
            _tour.Country_ID = int.Parse(CountryIdTextBox.Text);
            _tour.Stay_Time = int.Parse(StayTimeTextBox.Text);
            _tour.Price = decimal.Parse(PriceTextBox.Text);

            _tourRepository.UpdateTour(_tour);  // Вызов метода через экземпляр репозитория

            MessageBox.Show("Данные тура обновлены!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}
