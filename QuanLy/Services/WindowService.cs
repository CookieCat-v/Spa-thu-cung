using Microsoft.Extensions.DependencyInjection;
using QuanLy.Models;
using QuanLy.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
namespace QuanLy.Services
{
    public class WindowService : IWindowService
    {
        private readonly IServiceProvider _services;

        public WindowService(IServiceProvider services)
        {
            _services = services;
        }

        public void ShowLoginWindow()
        {
            var loginWindow =
                _services.GetRequiredService<LoginWindow>();

            var currentWindow = Application.Current.MainWindow;

            Application.Current.MainWindow = loginWindow;

            loginWindow.Show();

            currentWindow?.Close();
        }

        public void ShowMainWindow(User user)
        {
            var mainWindow =
                ActivatorUtilities.CreateInstance<MainMenu>(
                    _services, user);

            var currentWindow = Application.Current.MainWindow;

            Application.Current.MainWindow = mainWindow;

            mainWindow.Show();

            currentWindow?.Close();
        }
    }
}
