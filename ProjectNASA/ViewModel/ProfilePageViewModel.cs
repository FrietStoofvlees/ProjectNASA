using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProjectNASA.Data;
using System.Text.Json;

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
        void EditProfile()
        { 
            IsBusy = true;
        }

        [RelayCommand]
        async Task SaveChanges()
        {
            try
            {
                if (await userRepository.SaveUserAsync(User))
                {
                    await Toast.Make("Profile saved!").Show();
                }
            }
            catch (Exception ex)
            {
                await Toast.Make(ex.Message).Show();
            }

            IsBusy = false;
        }

        [RelayCommand]
        async Task SignOut()
        {
            if (authService.SignOut())
            {
                await Toast.Make("Sign Out succesfull!").Show();

                HasAuth = false;
                User = null;

                await CheckAuthenticationAsync(true);
                return;
            }
            
            await Toast.Make("Unable to Sign Out!").Show();
        }
    }
}
