using QuanLy.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuanLy.Services
{
    public class Authentication
    {
        private readonly List<User> _users = new()
        {
            new User
            {
                Username = "clerk01",
                Password = "123",
                Role = UserRole.Clerk
            },
            new User
            {
                Username = "customer01",
                Password = "123",
                Role = UserRole.Customer,

            }
        };

        public User? Login(string username, string password)
        {
            return _users.FirstOrDefault(u =>
                u.Username == username &&
                u.Password == password);
        }
    }
}
