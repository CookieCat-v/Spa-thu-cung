using QuanLy.Models;
using QuanLy.ViewModels;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuanLy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow(User user, IServiceProvider services,CustomerDashboardViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }

        private void Click_Home(object sender, RoutedEventArgs e)
        {
            main_scroll.ScrollToHome();
        }

        private void Click_Services(object sender, RoutedEventArgs e)
        {
            main_scroll.ScrollToVerticalOffset(225);
        }

        private void Click_Appointment(object sender, RoutedEventArgs e)
        {
            main_scroll.ScrollToBottom();
        }
    }
    
}