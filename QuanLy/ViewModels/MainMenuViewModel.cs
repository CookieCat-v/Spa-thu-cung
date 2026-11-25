using Microsoft.Xaml.Behaviors.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;

namespace QuanLy.ViewModels
{
    public class MainMenuViewModel : INotifyPropertyChanged
    {
        public event Action<string>? NavigationRequested;
        public RelayCommand NavigationCommand { get; }
        private string _selectedPage="";
        public string SelectedPage
        {
            get => _selectedPage;
            set
            {
                if (_selectedPage != value)
                {
                    _selectedPage = value;
                    OnPropertyChanged();
                }
            }
        }

        public MainMenuViewModel()
        {
            NavigationCommand = new RelayCommand(
                Navigate
                );
        }
        private void Navigate(object? parameter)
        {
            
            if (parameter is not string page) return;
            SelectedPage = page;

            if (page == "Logout")
                return;

            NavigationRequested?.Invoke(page);
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(
            [CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}
