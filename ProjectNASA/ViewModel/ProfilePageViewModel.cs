using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Text.Json;

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
