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
            ContentArea.Content = new TourView();
        }

        private void ShowCountries(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new CountryView();
        }

        private void ShowGuides(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new GuideView();
        }

        private void ShowHotels(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new HotelView();
        }

        private void ShowAgents(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new AgentsView();
        }

        private void ShowTourGuides(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new TourGuideView();
        }

        private void ShowReports(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new ReportView();
        }
    }
}
