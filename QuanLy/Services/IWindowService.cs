using QuanLy.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuanLy.Services
{
    public interface IWindowService
    {
        void ShowLoginWindow();
        void ShowMainWindow(User user);
    }
}
