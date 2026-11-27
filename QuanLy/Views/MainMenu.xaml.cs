using Microsoft.Extensions.DependencyInjection;
using QuanLy.Models;
using QuanLy.Services;
using QuanLy.ViewModels;
using QuanLy.Views.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static MaterialDesignThemes.Wpf.Theme;

namespace QuanLy.Views
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        private readonly MainMenuViewModel _vm;
        private readonly IWindowService _windowService;
        private readonly IServiceProvider _services;
        public MainMenu(User user, IServiceProvider services, MainMenuViewModel vm, IWindowService windowService)
        {
            InitializeComponent();
            _vm = vm;
            _services = services;
            _windowService = windowService;
            DataContext = _vm;
            _vm.NavigationRequested += NavigateToPage;
             MainFrame.Navigate(services.GetRequiredService<DashboardPage>());
           // MainFrame.Navigate(new Views.Pages.PetPage());
        }
        private void NavigateToPage(string page)
        {
           
            switch (page)
            {
                case "Dashboard":
                    MainFrame.Navigate(_services.GetRequiredService<DashboardPage>());
                    break;

                case "Appointments":
                    MainFrame.Navigate(_services.GetRequiredService<AppointmentPage>());
                    break;

                

                case "Pets":
                    MainFrame.Navigate(_services.GetRequiredService<PetPage>());
                    break;

                case "Logout":
                    Logout();
                    break;


            }
        }
       
        private void Logout()
        {
            _windowService.ShowLoginWindow();
        }
        protected override void OnClosed(EventArgs e)
        {
            _vm.NavigationRequested -= NavigateToPage;

            base.OnClosed(e);
        }
    }
}
