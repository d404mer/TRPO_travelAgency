using System;
using System.Windows;
using TravelAgency.DbAcess;
using TravelAgency.DbAcess.Repos;

namespace TravelAgency.AddViews
{
    public partial class AddGuideWindow : Window
    {
        private GuideRepository _guideRepository;

        public AddGuideWindow()
        {
            InitializeComponent();
            _guideRepository = new GuideRepository();  // Создаем экземпляр репозитория
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var newGuide = new Guide
            {
                Guid_Name = GuideNameTextBox.Text,
                Guid_Lastname = GuideLastNameTextBox.Text
            };

            bool success = _guideRepository.AddGuide(newGuide);  // Вызываем метод через экземпляр репозитория
            if (success)
            {
                MessageBox.Show("Гид добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Ошибка при добавлении гида.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
