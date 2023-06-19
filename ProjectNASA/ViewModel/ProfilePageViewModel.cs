using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProjectNASA.Data;

namespace ProjectNASA.ViewModel
{
    public partial class ProfilePageViewModel : BaseViewModel
    {
        readonly IAuthService authService;
        readonly IUserRepository userRepository;
        
        [ObservableProperty]
        User user;

        [ObservableProperty]
        bool hasAuth = false;

        public ProfilePageViewModel(IAuthService authService, IUserRepository userRepository)
        {
            this.authService = authService;
            this.userRepository = userRepository;
        }

        [RelayCommand]
        async Task CheckAuthenticationAsync(bool animate)
        {
            if (HasAuth)
                return;

            if (await authService.HasAuthenticationAsync())
            {
                User = AppHelpers.User;
                HasAuth = true;
            } else {
                await Shell.Current.GoToAsync(nameof(SignInPage), animate);
            }
        }

        [RelayCommand]
        async Task DeleteProfileAsync()
        {
            bool answer = await Shell.Current.DisplayAlert("Delete Profile?", "Do you really want to delete your profile? This action cannot be undone.", "Yes! Delete", "Cancel");

            if (answer)
            {
                authService.SignOut(answer);
                await Toast.Make("Your account has been deleted!").Show();
            }
        }

        [RelayCommand]
        void EditProfile()
        { 
            IsBusy = true;
        }

        [RelayCommand]
        async Task SaveChangesAsync()
        {
            try
            {
                userRepository.SaveUser(User);
                await Toast.Make("Profile saved!").Show();
            }
            catch (Exception ex)
            {
                await Toast.Make(ex.Message).Show();
            }

            IsBusy = false;
        }

        [RelayCommand]
        async Task SignOutAsync()
        {
            HasAuth = !authService.SignOut(false);

            if (HasAuth)
            {
                await Toast.Make("Unable to Sign Out!").Show();
                return;
            }

            await Toast.Make("Sign Out succesfull!").Show();

            HasAuth = false;
            User = null;

            await CheckAuthenticationAsync(true);
        }
    }
}
