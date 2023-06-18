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
            HasAuth = await IsAuthenticated();
            if (HasAuth)
                return;

            // use secure storage
            string user = Preferences.Get(nameof(AppHelpers.User), "");

            if (string.IsNullOrWhiteSpace(user))
            {
                await Shell.Current.GoToAsync(nameof(SignInPage), animate);
                return;
            }

            User = JsonSerializer.Deserialize<User>(user);

            HasAuth = true;
        }

        [RelayCommand]
        void EditProfile()
        { 
            IsBusy = true;
        }

        private static async Task<bool> IsAuthenticated()
        {
            var hasAuth = await SecureStorage.GetAsync("hasAuth");
            return hasAuth != null;
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
        async Task Logout()
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
