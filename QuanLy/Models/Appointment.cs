using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text;

namespace QuanLy.Models
{
    public class Appointment : INotifyPropertyChanged
    {
        private string _customerName = "";
        public string CustomerName
        {
            get => _customerName;
            set
            {
                _customerName = value;
                OnPropertyChanged();
            }
        }

        private string _phone = "";
        public string Phone
        {
            get => _phone;
            set
            {
                _phone = value;
                OnPropertyChanged();
            }
        }

        private string _petName = "";
        public string PetName
        {
            get => _petName;
            set
            {
                _petName = value;
                OnPropertyChanged();
            }
        }

        private string _petType = "";
        public string PetType
        {
            get => _petType;
            set
            {
                _petType = value;
                OnPropertyChanged();
            }
        }

        private string _serviceName = "";
        public string ServiceName
        {
            get => _serviceName;
            set
            {
                _serviceName = value;
                OnPropertyChanged();
            }
        }

        public int Id { get; set; }

        public int CustomerId { get; set; }
        public int PetId { get; set; }
        public DateTime AppointmentDate { get; set; }

        public string Status { get; set; } = "Pending";

        public string Notes { get; set; } = "";

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(
            [CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}
