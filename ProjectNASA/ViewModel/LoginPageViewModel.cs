using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProjectNASA.ViewModel
{
    public partial class LoginPageViewModel : BaseViewModel
    {
        readonly ILoginService loginService;

        [ObservableProperty]
        string username;
        [ObservableProperty]
        string password;

        public LoginPageViewModel(ILoginService loginService)
        {
            this.loginService = loginService;
        }
        
        [RelayCommand]
        public async Task LoginAsync()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                await Toast.Make("Please fill in your username and password").Show();
                return;
            } 
            if (loginService.Login(Username, "", Password))
                await Shell.Current.GoToAsync("..");
        }
    }
}
