using System.Windows;
using TravelAgency.UserControls;

namespace TravelAgency
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ShowBooking(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new BookingView();
        }

        private void ShowTours(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new ToursView();
        }

        private void ShowCountries(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new CountriesView();
        }

        private void ShowGuides(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new GuidesView();
        }

        private void ShowHotels(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new HotelsView();
        }

        private void ShowAgents(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new AgentsView();
        }

        private void ShowTourGuides(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new TourGuidesView();
        }

        private void ShowReports(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new ReportsView();
        }
    }
}
