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
                //if (hasApiKey)
                //{
                //    Pins.Add(new Pin
                //    {
                //        Address = Iss.Coordinates.TimezoneId,
                //        Description = "Real-time ISS location!",
                //        Location = new Location(Iss.Latitude, Iss.Longitude)
                //    });
                //    return;
                //}

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
                if (IsBusy) return;

                if (connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await Shell.Current.DisplayAlert("No connectivity!", "Check your internet connection", "OK");
                    return;
                }

                Iss = await wtiaService.GetIssCurrentLocationAsync();
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

            IsBusy = true;
            await TrackIssAsync();
            dispatcherTimer.Start();
            IsBusy = false;
            await Toast.Make("Tracking the location of the ISS!").Show();
        }
    }
}
