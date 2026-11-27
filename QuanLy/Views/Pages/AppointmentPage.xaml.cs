using Microsoft.Extensions.DependencyInjection;
using QuanLy.Models;
using QuanLy.Services;
using QuanLy.ViewModels;
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
        private readonly IServiceProvider _services;
        private readonly AppointmentService _appointmentService;

        public AppointmentPage( AppointmentService appointmentService , IServiceProvider services)
        {
            InitializeComponent();
            _services = services;
            _appointmentService = appointmentService;
            LoadAppointments();
        }

        private void LoadAppointments()
        {
            _appointments = new ObservableCollection<Appointment>(_appointmentService.GetAppointment()); 

            _appointmentView =
                CollectionViewSource.GetDefaultView(_appointments);

            AppointmentGrid.ItemsSource = _appointmentView;
        }
        private void OpenAppointmentDetail(Appointment appointment)
        {
            var vm = ActivatorUtilities.CreateInstance<
                   AppointmentDetailViewModel>(
                       _services,
                       appointment);

            var window = new AppopintmentDetailWindow(vm);

            window.Owner = Application.Current.MainWindow;
            window.ShowDialog();

            LoadAppointments();
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
                    appointment.Phone.Contains(keyword) ||
                    appointment.PetType.ToLower().Contains(keyword);

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
                
                OpenAppointmentDetail(appointment);
            }
        }
    }
}
