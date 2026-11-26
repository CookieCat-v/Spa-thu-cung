using QuanLy.Models;
using System;
using System.Collections.Generic;
using System.Text;
using QuanLy.Data;
using System.Windows;
namespace QuanLy.Services
{
    public class AppointmentService
    {
        public List<Appointment> GetAppointment()
        {
            using var context = new AppDbContext();
            return context.Appointments.ToList();
        }

        public void AddAppointment(Appointment appointment)
        {
            using var context = new AppDbContext();

            context.Appointments.Add(appointment);
            try
            {
                context.SaveChanges();
            }
            catch
            {
                MessageBox.Show("Error saving appointment");
                return;
            }
        }
        public void DeleteAppointment(Appointment appointment)
        {
            using var context = new AppDbContext();
            if (appointment != null)
            {
                context.Appointments.Remove(appointment);
                try
                {
                    context.SaveChanges();
                }
                catch
                {
                    MessageBox.Show("Error deleting appointment");
                    return;
                }
            }
        }
        public void UpdateAppointment(Appointment appointment)
        {
            using var context = new AppDbContext();
            var currAppointment = context.Appointments.FirstOrDefault(s => s.Id == appointment.Id);
            if (currAppointment == null)
            {
                MessageBox.Show("Selected appointment doesnt exist in database");
                return;
            }
            currAppointment.Status = appointment.Status;
            currAppointment.Phone = appointment.Phone;
            try
            {
                context.SaveChanges();
            } catch
            {
                MessageBox.Show("Error updating appointment");
                return;
            }
        }
    }
}
