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
    /// Interaction logic for AppopintmentDetailWindow.xaml
    /// </summary>
    public partial class AppopintmentDetailWindow : Window
    {
        public AppopintmentDetailWindow(
         AppointmentDetailViewModel vm)
        {
            InitializeComponent();

            DataContext = vm;
            vm.RequestClose += () => Close();
        }
    }
}
