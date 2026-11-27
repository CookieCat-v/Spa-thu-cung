
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using QuanLy.Models;
using QuanLy.Services;

namespace QuanLy.ViewModels
{
    public class AppointmentDetailViewModel : INotifyPropertyChanged
    {
        private readonly Appointment _originalAppointment;
        private readonly Appointment _editedAppointment;

        private readonly AppointmentService _appointmentService;
        public event Action? RequestClose;

        public event PropertyChangedEventHandler? PropertyChanged;

        public AppointmentDetailViewModel(
            Appointment appointment,
            AppointmentService appointmentService
            )
        {
            _originalAppointment = appointment;
            _appointmentService = appointmentService;
           

            // Create a separate editable copy
            _editedAppointment = new Appointment
            {
                Id = appointment.Id,
                CustomerId = appointment.CustomerId,
                PetId = appointment.PetId,

                CustomerName = appointment.CustomerName,
                Phone = appointment.Phone,

                PetName = appointment.PetName,
                PetType = appointment.PetType,

                ServiceName = appointment.ServiceName,
                AppointmentDate = appointment.AppointmentDate,
                Notes = appointment.Notes
            };

            SaveCommand = new RelayCommand(
                _ => SaveChanges());

            DeleteCommand = new RelayCommand(
                _ => DeleteAppointment());
        }

        // Customer information
        public string CustomerName
        {
            get => _editedAppointment.CustomerName;
            set
            {
                _editedAppointment.CustomerName = value;
                OnPropertyChanged();
            }
        }

        public string Phone
        {
            get => _editedAppointment.Phone;
            set
            {
                _editedAppointment.Phone = value;
                OnPropertyChanged();
            }
        }

        public int CustomerId => _editedAppointment.CustomerId;

        // Pet information
        public string PetName
        {
            get => _editedAppointment.PetName;
            set
            {
                _editedAppointment.PetName = value;
                OnPropertyChanged();
            }
        }

        public string PetType
        {
            get => _editedAppointment.PetType;
            set
            {
                _editedAppointment.PetType = value;
                OnPropertyChanged();
            }
        }

        public int PetId => _editedAppointment.PetId;

        // Appointment information
        public string ServiceName
        {
            get => _editedAppointment.ServiceName;
            set
            {
                _editedAppointment.ServiceName = value;
                OnPropertyChanged();
            }
        }

        public DateTime AppointmentDate
        {
            get => _editedAppointment.AppointmentDate;
            set
            {
                _editedAppointment.AppointmentDate = value;
                OnPropertyChanged();
            }
        }

        public string Notes
        {
            get => _editedAppointment.Notes;
            set
            {
                _editedAppointment.Notes = value;
                OnPropertyChanged();
            }
        }
        public string Status
        {
            get => _editedAppointment.Status;
            set
            {
                _editedAppointment.Status = value;
                OnPropertyChanged();
            }
        }

        // Commands
        public RelayCommand SaveCommand { get; }
        public RelayCommand DeleteCommand { get; }

        private void SaveChanges()
        {
            try
            {
                // Update database using the edited copy
                _appointmentService.UpdateAppointment(
                    _editedAppointment);

                // Copy changes to the original object only after saving
                _originalAppointment.CustomerName =
                    _editedAppointment.CustomerName;

                _originalAppointment.Phone =
                    _editedAppointment.Phone;

                _originalAppointment.PetName =
                    _editedAppointment.PetName;

                _originalAppointment.PetType =
                    _editedAppointment.PetType;

                _originalAppointment.ServiceName =
                    _editedAppointment.ServiceName;

                _originalAppointment.AppointmentDate =
                    _editedAppointment.AppointmentDate;

                _originalAppointment.Notes =
                    _editedAppointment.Notes;

                _originalAppointment.Status = _editedAppointment.Status;

                MessageBox.Show(
                    "Appointment updated successfully!",
                    "Success",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                RequestClose?.Invoke();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Failed to update appointment: {ex.Message}",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void DeleteAppointment()
        {
            var result = MessageBox.Show(
                "Are you sure you want to delete this appointment?",
                "Confirm Delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result != MessageBoxResult.Yes)
                return;

            try
            {
                _appointmentService.DeleteAppointment(
                    _originalAppointment);

                MessageBox.Show(
                    "Appointment deleted successfully!",
                    "Success",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                RequestClose?.Invoke();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Failed to delete appointment: {ex.Message}",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        protected void OnPropertyChanged(
            [CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}
