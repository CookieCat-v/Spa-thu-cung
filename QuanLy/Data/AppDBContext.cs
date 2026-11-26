using Microsoft.EntityFrameworkCore;
using QuanLy.Models;
namespace QuanLy.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Pet> Pets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=petspa.db");
        }
    }
}
