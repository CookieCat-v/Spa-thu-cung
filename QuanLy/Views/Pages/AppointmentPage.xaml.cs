using QuanLy.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuanLy.Views.Pages
{
    /// <summary>
    /// Interaction logic for AppointmentPage.xaml
    /// </summary>
    public partial class AppointmentPage : Page
    {
        private ObservableCollection<Appointment> _appointments;
        private ICollectionView _appointmentView;

        public AppointmentPage()
        {
            InitializeComponent();

            LoadAppointments();
        }

        private void LoadAppointments()
        {
            _appointments = new ObservableCollection<Appointment>
            {
                new Appointment
                {
                    Id = 1,
                    CustomerName = "Nguyen Van A",
                    Phone = "0901234567",
                    PetName = "Lucky",
                    PetType = "Dog",
                    ServiceName = "Bath & Cleaning",
                    AppointmentDate = DateTime.Today,
                    Status = "Pending",
                    Notes = "Sensitive skin"
                },

                new Appointment
                {
                    Id = 2,
                    CustomerName = "Tran Thi B",
                    Phone = "0912345678",
                    PetName = "Mimi",
                    PetType = "Cat",
                    ServiceName = "Haircut & Styling",
                    AppointmentDate = DateTime.Today.AddDays(1),
                    Status = "Confirmed"
                },

                new Appointment
                {
                    Id = 3,
                    CustomerName = "Le Van C",
                    Phone = "0923456789",
                    PetName = "Max",
                    PetType = "Dog",
                    ServiceName = "Nail & Paw Care",
                    AppointmentDate = DateTime.Today.AddDays(2),
                    Status = "Completed"
                },

                new Appointment
                {
                    Id = 4,
                    CustomerName = "Pham Thi D",
                    Phone = "0934567890",
                    PetName = "Kitty",
                    PetType = "Cat",
                    ServiceName = "Bath & Cleaning",
                    AppointmentDate = DateTime.Today.AddDays(3),
                    Status = "Cancelled"
                }
            };

            _appointmentView =
                CollectionViewSource.GetDefaultView(_appointments);

            AppointmentGrid.ItemsSource = _appointmentView;
        }

        private void ApplyFilter()
        {
            string keyword = SearchTextBox.Text.Trim().ToLower();

            string status =
                (StatusFilter.SelectedItem as ComboBoxItem)
                ?.Content?.ToString() ?? "All Status";

            _appointmentView.Filter = obj =>
            {
                if (obj is not Appointment appointment)
                    return false;

                bool matchesSearch =
                    string.IsNullOrWhiteSpace(keyword) ||
                    appointment.CustomerName.ToLower().Contains(keyword) ||
                    appointment.PetName.ToLower().Contains(keyword) ||
                    appointment.Phone.Contains(keyword);

                bool matchesStatus =
                    status == "All Status" ||
                    appointment.Status == status;

                return matchesSearch && matchesStatus;
            };

            _appointmentView.Refresh();
        }

        private void SearchTextBox_TextChanged(
            object sender, TextChangedEventArgs e)
        {
            ApplyFilter();
        }

        private void StatusFilter_SelectionChanged(
            object sender, SelectionChangedEventArgs e)
        {
            if (_appointmentView != null)
                ApplyFilter();
        }

        private void Refresh_Click(
            object sender, RoutedEventArgs e)
        {
            SearchTextBox.Clear();
            StatusFilter.SelectedIndex = 0;

            ApplyFilter();
        }

        private void AppointmentGrid_SelectionChanged(
            object sender, SelectionChangedEventArgs e)
        {
            if (AppointmentGrid.SelectedItem is Appointment appointment)
            {
                // You can open an appointment detail window here.
            }
        }
    }
}
