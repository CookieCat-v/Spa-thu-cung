using QuanLy.Models;
using QuanLy.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for DashboardPage.xaml
    /// </summary>
    
    public partial class DashboardPage : Page
    {
        private readonly AppointmentService _appointmentService;
       
        public DashboardPage(  AppointmentService appointmentService)
        {
            InitializeComponent();
       
            _appointmentService = appointmentService;
            LoadRecentAppointments();
        }

        private void LoadRecentAppointments()
        {
            var appointments = new ObservableCollection<Appointment>(_appointmentService.GetAppointment());
           

            RecentAppointmentsGrid.ItemsSource =
                appointments.Take(5);
        }
    }
}
