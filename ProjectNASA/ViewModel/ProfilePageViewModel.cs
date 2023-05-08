using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core.Platform;
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
            this.loginService = loginService;
            this.loginService.Logout();
        }

        public async Task CheckUserLogin()
        {
            if (IsLoggedIn)
                return;

            string user = Preferences.Get(nameof(App.User), "");

            if (string.IsNullOrWhiteSpace(user))
            {
                await Shell.Current.GoToAsync(nameof(LoginPage));
                return;
            }

            User = JsonSerializer.Deserialize<User>(user);

            IsLoggedIn = true;
        }

        [RelayCommand]
        private void EditProfile()
        { 
            IsBusy = true;
        }

        [RelayCommand]
        private static async Task HideKeyboardAsync()
        {
            if (KeyboardExtensions.IsSoftKeyboardShowing(default))
            {
                await KeyboardExtensions.HideKeyboardAsync(default, default);
            }
        }

        [RelayCommand]
        private void SaveChanges()
        {
            if (loginService.Login(User.Username, User.Email, User.Password))
                Toast.Make("Details saved!").Show();

            IsBusy = false;
        }

        [RelayCommand]
        private async Task Logout()
        {
            if (!loginService.Logout())
            {
                await Toast.Make("Unable to logout!").Show();
                return;
            }
            IsLoggedIn = false;
            User = new User();
            await Toast.Make("Logout succesfull!").Show();
            await CheckUserLogin();
        }
    }
}
