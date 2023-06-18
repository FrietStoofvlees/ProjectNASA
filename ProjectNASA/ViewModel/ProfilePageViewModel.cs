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
            this.authService.SignOut();
        }

        [RelayCommand]
        async Task CheckUserLoginAsync(bool animate)
        {
            HasAuth = await authService.IsAuthenticated();
            if (HasAuth)
                return;

            User = AppHelpers.User;

            if (User is null)
            {
                await Shell.Current.GoToAsync(nameof(SignInPage), animate);
                return;
            }

            HasAuth = true;
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
            if (!authService.SignOut())
            {
                await Toast.Make("Unable to Sign Out!").Show();
                return;
            }
            HasAuth = false;
            User = new User();
            await Toast.Make("Sign Out succesfull!").Show();
            await CheckUserLoginAsync(true);
        }
    }
}
