using QuanLy.Models;
using QuanLy.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using Serilog;
namespace QuanLy.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly Authentication _authService;

        private string _username = "";
        private string _password = "";

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
                LoginCommand.RaiseCanExecuteChanged();
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
                LoginCommand.RaiseCanExecuteChanged();
            }
        }
        public User? LoggedInUser { get; private set; }

        public RelayCommand LoginCommand { get; }
        public RelayCommand GuestLoginCommand { get; }

        public event Action<User>? LoginSuccess;

        public LoginViewModel(Authentication authService)
        {
            _authService = authService;
            LoginCommand = new RelayCommand(
                execute: _ => Login(),
                canExecute: _ => (!string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password))
                );
            GuestLoginCommand = new RelayCommand(
                execute: _ => GuestLogin()
                
                );
        }
        private void GuestLogin()
        {
            var user = _authService.Login("customer01", "123");
            if (user == null)
            {
                MessageBox.Show("An error has happened");
                return;
            }
            LoggedInUser = user;
            try
            {
                LoginSuccess?.Invoke(user);
            }
            catch
            {
                 Log.Error("Log in failed");
                MessageBox.Show("Log in failed");
                return;
            }
             Log.Information("{User} logged in", user.Username);
        }
        private void Login()
        {
            var user = _authService.Login(Username, Password);
            if (user == null)
            {
                MessageBox.Show("Username or password is incorrect!");
                return;
            }
            LoggedInUser = user;
            try
            {
                LoginSuccess?.Invoke(user);
            }
            catch
            {
                Log.Error("Log in failed");
                MessageBox.Show("Log in failed");
                return;
            }
            Log.Information("{User} logged in", user.Username);
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
