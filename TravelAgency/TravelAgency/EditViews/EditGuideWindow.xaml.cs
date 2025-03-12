using System;
using System.Windows;
using TravelAgency.DbAcess;
using TravelAgency.DbAcess.Repos;

namespace TravelAgency.EditViews
{
    public partial class GuideEditWindow : Window
    {
        private Guide _guide;
        private GuideRepository _guideRepository;

        public GuideEditWindow(Guide guide)
        {
            InitializeComponent();
            _guide = guide;
            _guideRepository = new GuideRepository();
            LoadGuideData();
        }

        private void LoadGuideData()
        {
            GuideNameTextBox.Text = _guide.Guid_Name;
            GuideLastNameTextBox.Text = _guide.Guid_Lastname;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _guide.Guid_Name = GuideNameTextBox.Text;
            _guide.Guid_Lastname = GuideLastNameTextBox.Text;

            _guideRepository.UpdateGuide(_guide);  // Вызов метода через экземпляр репозитория

            MessageBox.Show("Данные гида обновлены!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}