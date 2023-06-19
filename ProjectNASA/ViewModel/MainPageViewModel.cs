using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ProjectNASA.ViewModel
{
    public partial class MainPageViewModel : BaseViewModel
    {
        readonly IApodService apodService;
        readonly IAuthService authService;
        readonly IWtiaService wtiaService;
        readonly IConnectivity connectivity;
        readonly IDispatcherTimer dispatcherTimer;
        readonly IMap map;

        [ObservableProperty]
        Apod apod;

        [ObservableProperty]
        Iss iss;

        public MainPageViewModel(IApodService apodService, IAuthService authService, IWtiaService wtiaService, IConnectivity connectivity, IMap map)
        {
            this.apodService = apodService;
            this.authService = authService;
            this.wtiaService = wtiaService;
            this.connectivity = connectivity;
            this.map = map;

            Task.Run(async() => 
            {
                IsBusy = true;
                Apod = await this.apodService.GetAstronomyPictureOftheDayAsync();
                IsBusy = false;

            });

            dispatcherTimer = Application.Current.Dispatcher.CreateTimer();
            dispatcherTimer.Interval = TimeSpan.FromSeconds(5);
            dispatcherTimer.Tick += async (s, e) =>
            {
                await TrackIssAsync();
            };
        }

#if ANDROID
        public void Github14471Hack(string propertyName) => OnPropertyChanged(propertyName);
#endif

        [RelayCommand]
        async Task GoToApodPageAsync()
        {
            await Shell.Current.GoToAsync(nameof(FavoriteDetailsPage), true, new Dictionary<string, object>()
            {
                { "Apod", Apod }
            });
        }

        [RelayCommand]
        async Task OpenMapsAsync()
        {
            if (Iss is not null)
            {
#if __MOBILE__
                await map.OpenAsync(Iss.Latitude, Iss.Longitude, new MapLaunchOptions
                {
                    Name = "Real-time ISS location!",
                    NavigationMode = NavigationMode.None
                });
#elif WINDOWS
                await Launcher.OpenAsync(Iss.Coordinates.MapUrl);
#endif
            }
        }

        [RelayCommand]
        async Task TrackIssAsync()
        {
            try
            {
                if (connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await Shell.Current.DisplayAlert("No connectivity!", "Check your internet connection", "OK");
                    return;
                }

                Iss = await wtiaService.GetIssCurrentLocationAsync();

                if (Iss.Coordinates.CountryCode == "??")
                {
                    Iss.Coordinates.CountryCode = "Over the ocean";
                    OnPropertyChanged(nameof(Iss));
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("The following error occured:", ex.Message, "OK");
            }
        }

        [RelayCommand]
        async Task ToggleIssTrackingAsync()
        {
            if (dispatcherTimer.IsRunning)
            {
                dispatcherTimer.Stop();
                await Toast.Make("No longer tracking the ISS!").Show();
                return;
            }

            await TrackIssAsync();
            dispatcherTimer.Start();
            await Toast.Make("Tracking the location of the ISS!").Show();
        }
    }
}
