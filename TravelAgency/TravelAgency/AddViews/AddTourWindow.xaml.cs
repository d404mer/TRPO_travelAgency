using System;
using System.Windows;
using TravelAgency.DbAcess;
using TravelAgency.DbAcess.Repos;

namespace TravelAgency.AddViews
{
    public partial class AddTourWindow : Window
    {
        private TourRepository _tourRepository;

        public AddTourWindow()
        {
            InitializeComponent();
            _tourRepository = new TourRepository();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Генерация уникального ID для тура
            int newTourId = GenerateUniqueTourId();

            // Получаем введенные данные
            Tour newTour = new Tour
            {
                Tour_ID = newTourId,  // Присваиваем сгенерированный ID
                Tour_Name = TourNameTextBox.Text,
                Country_ID = int.Parse(CountryIDTextBox.Text),
                Stay_Time = int.Parse(StayTimeTextBox.Text),
                Price = decimal.Parse(PriceTextBox.Text)
            };

            bool success = _tourRepository.AddTour(newTour);
            if (success)
            {
                MessageBox.Show("Тур добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Ошибка при добавлении тура.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private int GenerateUniqueTourId()
        {
            // Простой способ сгенерировать уникальный ID для нового тура
            // Можно использовать текущее время, или, например, увеличить ID на 1 от последнего ID
            Random random = new Random();
            return random.Next(1000, 9999);  // Генерация ID от 1000 до 9999
        }
    }
}
