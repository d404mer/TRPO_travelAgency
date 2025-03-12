using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TravelAgency.DbAcess;
using TravelAgency.DbAcess.Repos;

namespace TravelAgency.UserControls
{
    public partial class ReportView : UserControl
    {
        private BookingRepo _bookingRepository;
        private TourRepository _tourRepository;
        private AgentRepository _agentRepository;
        private GuideRepository _guideRepository;

        public ReportView()
        {
            InitializeComponent();
            _bookingRepository = new BookingRepo();
            _tourRepository = new TourRepository();
            _agentRepository = new AgentRepository();
            _guideRepository = new GuideRepository();
        }

        // Метод для генерации отчета
        private void GenerateReportButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedReportType = ((ComboBoxItem)ReportTypeComboBox.SelectedItem)?.Content?.ToString();
            string filter = FilterTextBox.Text;

            if (selectedReportType == null)
            {
                MessageBox.Show("Пожалуйста, выберите тип отчета.");
                return;
            }

            try
            {
                switch (selectedReportType)
                {
                    case "Отчет по бронированиям":
                        GenerateBookingReport(filter);
                        break;
                    case "Отчет по турам":
                        GenerateTourReport(filter);
                        break;
                    case "Отчет по агентам":
                        GenerateAgentReport(filter);
                        break;
                    case "Отчет по гидам":
                        GenerateGuideReport(filter);
                        break;
                    default:
                        MessageBox.Show("Неизвестный тип отчета.");
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при генерации отчета: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GenerateBookingReport(string filter)
        {
            // Загружаем все бронирования, применяем фильтрацию, если нужно
            var bookings = _bookingRepository.GetAllBookings();
            if (!string.IsNullOrEmpty(filter))
            {
                bookings = bookings.Where(b => b.Date_Of_Book.ToString().Contains(filter)).ToList();
            }

            ReportDataGrid.ItemsSource = bookings.Select(b => new
            {
                ID = b.Book_ID,
                Name = b.Agent_ID, // Или используйте другие поля, как Agent Name
                Date = b.Date_Of_Book,
                Price = b.Price
            }).ToList();
        }

        private void GenerateTourReport(string filter)
        {
            // Загружаем все туры, применяем фильтрацию
            var tours = _tourRepository.GetAllTours();
            if (!string.IsNullOrEmpty(filter))
            {
                tours = tours.Where(t => t.Tour_Name.Contains(filter)).ToList();
            }

            ReportDataGrid.ItemsSource = tours.Select(t => new
            {
                ID = t.Tour_ID,
                Name = t.Tour_Name,
                Date = "", // Здесь можно указать дополнительные поля
                Price = t.Price
            }).ToList();
        }

        private void GenerateAgentReport(string filter)
        {
            // Загружаем всех агентов
            var agents = _agentRepository.GetAllAgents();
            if (!string.IsNullOrEmpty(filter))
            {
                agents = agents.Where(a => a.Name.Contains(filter)).ToList();
            }

            ReportDataGrid.ItemsSource = agents.Select(a => new
            {
                ID = a.Agent_Name,
                Name = a.Name,
                Date = "", // Например, можно добавить информацию о дате регистрации агента
                Price = "" // Можно указать другие поля, связанные с агентом
            }).ToList();
        }

        private void GenerateGuideReport(string filter)
        {
            // Загружаем всех гидов
            var guides = _guideRepository.GetAllGuides();
            if (!string.IsNullOrEmpty(filter))
            {
                guides = guides.Where(g => g.Guid_Name.Contains(filter)).ToList();
            }

            ReportDataGrid.ItemsSource = guides.Select(g => new
            {
                ID = g.Guid_ID,
                Name = g.Guid_Name,
                Date = "", // Можно добавить дополнительные поля, если есть
                Price = "" // Можно добавить дополнительные поля, если есть
            }).ToList();
        }
    }
}
