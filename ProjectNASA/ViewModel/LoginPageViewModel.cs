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
        public void Login()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                Toast.Make("Please fill in your username and password").Show();
            } 
            else
            {
                if (loginService.Login(Username, "", Password))
                {
                    ((AppShell)Application.Current.MainPage).SwitchtoTab(TabPages.ProfilePage);
                }

                //Toast.Make("Error during login").Show();
            }
        }
    }
}
