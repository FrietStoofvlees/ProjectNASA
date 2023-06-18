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
        [NotifyPropertyChangedFor(nameof(HasNoAccount))]
        bool hasAccount;

        public bool HasNoAccount => !HasAccount;

        public SignInPageViewModel(IAuthService authService)
        {
            this.authService = authService;
            HasAccount = true;
        }

        [RelayCommand]
        public void ToggleSignUp()
        {
            HasAccount = !HasAccount;
        }
        
        [RelayCommand]
        public async Task SignInAsync()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                await Toast.Make("Please fill in your username and password").Show();
                return;
            } 
            if (await authService.SignIn(Username, Password))
                await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        public async Task SignUpAsync()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                await Toast.Make("Please fill in your username and password").Show();
                return;
            }
            if (await authService.SignIn(Username, Password))
                await Shell.Current.GoToAsync("..");
        }
    }
}
