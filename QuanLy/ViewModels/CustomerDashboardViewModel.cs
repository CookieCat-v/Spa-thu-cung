using QuanLy.Models;
using QuanLy.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;

namespace QuanLy.ViewModels
{
    public class CustomerDashboardViewModel : INotifyPropertyChanged
    {
        private Appointment _selectedAppointment = new Appointment();
        public RelayCommand BookAppointmentCommand { get; set; }
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

        public CustomerDashboardViewModel(AppointmentService appointmentService)
        {
            _appointmentService = appointmentService;

            BookAppointmentCommand = new RelayCommand
            (
                execute: _ => BookAppointment(),
               // canExecute: _ => true
                canExecute: _ => _selectedAppointment != null && !string.IsNullOrWhiteSpace(_selectedAppointment.CustomerName) && !string.IsNullOrWhiteSpace(_selectedAppointment.Phone) && !string.IsNullOrWhiteSpace(_selectedAppointment.PetType) && !string.IsNullOrWhiteSpace(_selectedAppointment.ServiceName)
                );
            _selectedAppointment.PropertyChanged += Appointement_PropertyChanged;

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
