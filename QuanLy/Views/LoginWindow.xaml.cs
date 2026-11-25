using Microsoft.Extensions.DependencyInjection;
using QuanLy.Models;
using QuanLy.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QuanLy.Views
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly IServiceProvider _services;
        public LoginWindow(LoginViewModel vm,IServiceProvider services)
        {
            InitializeComponent();
            _services = services;
            DataContext = vm;
            
            vm.LoginSuccess += OnLoginSuccess;
        }
        private void OnLoginSuccess(User user)
        {
            Window mainWindow;
             if (user.Role == UserRole.Customer)
             {
                  mainWindow =
            ActivatorUtilities.CreateInstance<MainWindow>(
                _services, user);
             }
             else
             {
                 mainWindow = ActivatorUtilities.CreateInstance<MainMenu>(_services, user);
             } 
             Application.Current.MainWindow = mainWindow;
             mainWindow.Show();
             Close(); 
           
        }
        private void PasswordBox_PasswordChanged(
    object sender,
    RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                ((dynamic)this.DataContext).Password = ((PasswordBox)sender).Password;
            }
        }
        private void NameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
