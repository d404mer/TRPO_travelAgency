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
            // Проверка на пустые поля для имени и фамилии
            if (string.IsNullOrWhiteSpace(GuideNameTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, введите имя гида.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(GuideLastNameTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, введите фамилию гида.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Убираем лишние пробелы по бокам
            string guideName = GuideNameTextBox.Text.Trim();
            string guideLastName = GuideLastNameTextBox.Text.Trim();

            // Создание нового объекта гида
            var newGuide = new Guide
            {
                Guid_Name = guideName,
                Guid_Lastname = guideLastName
            };

            // Вызываем метод репозитория для добавления гида
            bool success = _guideRepository.AddGuide(newGuide);
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
