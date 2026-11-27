using System;
using System.Collections.Generic;
using System.Text;

namespace QuanLy.Models
{
    public enum UserRole
    {
        Clerk,
        Customer
    }
    public class User
    {
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public UserRole Role { get; set; }

    }
}
