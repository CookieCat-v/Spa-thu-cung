using System;
using System.Collections.Generic;
using System.Text;

namespace QuanLy.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        public string CustomerName { get; set; } = "";

        public string Phone { get; set; } = "";

        public string PetName { get; set; } = "";

        public string PetType { get; set; } = "";

        public string ServiceName { get; set; } = "";

        public DateTime AppointmentDate { get; set; }

        public string Status { get; set; } = "Pending";

        public string Notes { get; set; } = "";
    }
}
