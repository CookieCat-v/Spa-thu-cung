using QuanLy.Models;
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
        public DashboardPage()
        {
            InitializeComponent();

            LoadRecentAppointments();
        }

        private void LoadRecentAppointments()
        {
            var appointments = new ObservableCollection<Appointment>
            {
                new Appointment
                {
                    CustomerName = "Nguyen Van A",
                    PetName = "Lucky",
                    ServiceName = "Bath & Cleaning",
                    AppointmentDate = DateTime.Today,
                    Status = "Pending"
                },

                new Appointment
                {
                    CustomerName = "Tran Thi B",
                    PetName = "Mimi",
                    ServiceName = "Haircut & Styling",
                    AppointmentDate = DateTime.Today.AddDays(1),
                    Status = "Confirmed"
                },

                new Appointment
                {
                    CustomerName = "Le Van C",
                    PetName = "Max",
                    ServiceName = "Nail & Paw Care",
                    AppointmentDate = DateTime.Today.AddDays(2),
                    Status = "Completed"
                }
            };

            RecentAppointmentsGrid.ItemsSource =
                appointments.Take(5);
        }
    }
}
