using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProjectNASA.ViewModel
{
    public partial class ProfilePageViewModel : BaseViewModel
    {
        readonly ILoginService loginService;
        
        [ObservableProperty]
        User user;

        [ObservableProperty]
        bool isLoggedIn = false;

        public ProfilePageViewModel(ILoginService loginService)
        {
            CheckUserLogin();
            this.loginService = loginService;
        }

        private async void CheckUserLogin()
        {
            string user = Preferences.Get(nameof(App.User), "");

            if (!IsLoggedIn)
            {
                if (string.IsNullOrWhiteSpace(user))
                    await Shell.Current.GoToAsync(nameof(LoginPage));
                IsLoggedIn = true;
            }

            User = JsonSerializer.Deserialize<User>(user);
        }

        [RelayCommand]
        private void EditProfile()
        { 
            IsBusy = true;
        }

        [RelayCommand]
        private void SaveProfile()
        {
            if (loginService.Login(User.Username, User.Email, User.Password))
            {
                Toast.Make("Details saved!").Show();
            };

            IsBusy = false;
        }
    }
}
