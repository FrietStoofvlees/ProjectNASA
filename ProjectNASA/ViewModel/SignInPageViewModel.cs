using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ProjectNASA.ViewModel
{
    public partial class SignInPageViewModel : BaseViewModel
    {
        readonly IAuthService authService;

        [ObservableProperty]
        string username;
        [ObservableProperty]
        string password;
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(HasNoAuth))]
        bool hasAuth = true;

        public bool HasNoAuth => !HasAuth;

        public SignInPageViewModel(IAuthService authService)
        {
            this.authService = authService;
        }

        [RelayCommand]
        public void ToggleSignUp()
        {
            HasAuth = !HasAuth;
        }
        
        [RelayCommand]
        public async Task SignInAsync()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                await Toast.Make("Please fill in your username and password").Show();
                return;
            } 
            if (await authService.SignInAsync(Username, Password))
                await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        public async Task SignUpAsync()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                await Toast.Make("Please choose a username and password").Show();
                return;
            }
            if (await authService.SignUpAsync(Username, Password))
                await Shell.Current.GoToAsync("..");
        }
    }
}
