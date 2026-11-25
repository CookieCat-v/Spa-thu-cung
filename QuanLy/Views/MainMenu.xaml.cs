using QuanLy.Models;
using QuanLy.ViewModels;
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
        public MainMenu(User user, IServiceProvider services, MainMenuViewModel vm)
        {
            InitializeComponent();
            _vm = vm;
            DataContext = _vm;
            _vm.NavigationRequested += NavigateToPage;
            MainFrame.Navigate(new Views.Pages.DashboardPage());
        }
        private void NavigateToPage(string page)
        {
           
            switch (page)
            {
                case "Dashboard":
                    MainFrame.Navigate(new Views.Pages.DashboardPage());
                    break;

                case "Appointments":
                    MainFrame.Navigate(new Views.Pages.AppointmentPage());
                    break;

                

                case "Pets":
                    MainFrame.Navigate(new Views.Pages.PetPage());
                    break;

                case "Logout":
                    Logout();
                    break;


            }
        }
       
        private void Logout()
        {
            MessageBox.Show("log out");
        }
        protected override void OnClosed(EventArgs e)
        {
            _vm.NavigationRequested -= NavigateToPage;

            base.OnClosed(e);
        }
    }
}
