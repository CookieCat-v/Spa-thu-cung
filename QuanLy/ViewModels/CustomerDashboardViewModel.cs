using QuanLy.Models;
using QuanLy.Services;
using QuanLy.Views;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace QuanLy.ViewModels
{
    public class CustomerDashboardViewModel : INotifyPropertyChanged
    {
        private Appointment _selectedAppointment = new Appointment();
        private readonly IWindowService _windowService;

        
        public RelayCommand BookAppointmentCommand { get; set; }
        public RelayCommand LogOutCommand { get; set; }
       
        private AppointmentService _appointmentService;
        public Appointment SelectedAppointment
        {
            get => _selectedAppointment;
            set
            {
                if (_selectedAppointment!=null)
                    _selectedAppointment.PropertyChanged -= Appointement_PropertyChanged;
                _selectedAppointment = value;
                if (_selectedAppointment!=null)
                    _selectedAppointment.PropertyChanged += Appointement_PropertyChanged;
                OnPropertyChanged();
                BookAppointmentCommand.RaiseCanExecuteChanged();
            }
        }

        public CustomerDashboardViewModel(AppointmentService appointmentService, IWindowService windowService)
        {
            _appointmentService = appointmentService;
            _windowService = windowService;
            BookAppointmentCommand = new RelayCommand
            (
                execute: _ => BookAppointment(),
               // canExecute: _ => true
                canExecute: _ => _selectedAppointment != null && !string.IsNullOrWhiteSpace(_selectedAppointment.CustomerName) && !string.IsNullOrWhiteSpace(_selectedAppointment.Phone) && !string.IsNullOrWhiteSpace(_selectedAppointment.PetType) && !string.IsNullOrWhiteSpace(_selectedAppointment.ServiceName)
                );
            LogOutCommand = new RelayCommand(
                execute: _ => LogOut()
                );
            _selectedAppointment.PropertyChanged += Appointement_PropertyChanged;

        }
        
        private void LogOut()
        {

            _windowService.ShowLoginWindow();


        }
        private void Appointement_PropertyChanged(object? sender,PropertyChangedEventArgs e)
        {

            BookAppointmentCommand.RaiseCanExecuteChanged();
        }
        private void BookAppointment()
        {
            var s = _selectedAppointment;
            var newAppointment = new Appointment
            {
                PetName = s.PetName,
                PetType = s.PetType,
                CustomerName = s.CustomerName,
                Phone = s.Phone,
                Notes = s.Notes,
                ServiceName = s.ServiceName,
                AppointmentDate = s.AppointmentDate,
                Status = "Pending"

            };
            _appointmentService.AddAppointment(newAppointment);
            Clear();
            MessageBox.Show("Book successfully");
            
        }
        
        private void Clear()
        {
            SelectedAppointment = new Appointment();
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
