#if ANDROID
using Android.Content.PM;
using Android.OS;
#endif
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace ProjectNASA.ViewModel
{
    public partial class WtiaViewModel : BaseViewModel
    {
        readonly IConnectivity connectivity;
        readonly IMap map;
        readonly IWtiaService wtiaService;
        bool hasApiKey = false;
        public IDispatcherTimer dispatcherTimer;

        public ObservableCollection<Pin> Pins { get; } = new();

        [ObservableProperty]
        Iss iss;

        public WtiaViewModel(IWtiaService wtiaService, IConnectivity connectivity, IMap map)
        {
            this.wtiaService = wtiaService;
            this.connectivity = connectivity;
            this.map = map;

            dispatcherTimer = Application.Current.Dispatcher.CreateTimer();
            dispatcherTimer.Interval = TimeSpan.FromSeconds(5);
            dispatcherTimer.Tick += async (s, e) =>
            {
                await GetIssAsync();
            };
        }

        [RelayCommand]
        async Task GetIssAsync()
        { 
            try
            {
                if (IsBusy) return;

                if (connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await Shell.Current.DisplayAlert("No connectivity!", "Check your internet connection", "OK");
                    return;
                }

                IsBusy = true;

                Iss = await wtiaService.GetIssCurrentLocationAsync();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("The following error occured:", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        static bool HasMapsSdkApiKey()
        {
            /*
            [Register("getPackageInfo", "(Ljava/lang/String;Landroid/content/pm/PackageManager$PackageInfoFlags;)Landroid/content/pm/PackageInfo;", "GetGetPackageInfo_Ljava_lang_String_Landroid_content_pm_PackageManager_PackageInfoFlags_Handler", ApiSince = 33)]
            [SupportedOSPlatform("android33.0")]
            public virtual PackageInfo GetPackageInfo(string packageName, PackageInfoFlags flags);
            */

#if ANDROID33_0
            // disable obsolete warning https://github.com/dotnet/maui/issues/11319
#pragma warning disable 618
            PackageManager currentPM = Platform.AppContext.PackageManager;
            PackageInfo packageInfo = currentPM.GetPackageInfo(Platform.AppContext.PackageName, PackageInfoFlags.MetaData);
            Bundle metaBundle = packageInfo.ApplicationInfo.MetaData;
#pragma warning restore 618

            string apiKey = "";

            foreach (var key  in metaBundle.KeySet())
            {
                if (key == "com.google.android.geo.API_KEY")
                {
                    apiKey = metaBundle.GetString(key);
                    break;
                }
            }

            if (apiKey == "PASTE-YOUR-API-KEY-HERE" || apiKey == "")
                return false;
#endif
            return true;
        }

        [RelayCommand]
        async Task OpenMapsAsync()
        {
            if (Iss != null)
            {
#if __MOBILE__
                if (hasApiKey)
                {
                    Pins.Add(new Pin
                    {
                        Address = Iss.Coordinates.TimezoneId,
                        Description = "Real-time ISS location!",
                        Location = new Location(Iss.Latitude, Iss.Longitude)
                    });
                    return;
                }

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
        async Task ToggleIssTracking()
        {
            if (dispatcherTimer.IsRunning)
            {
                dispatcherTimer.Stop();
                await Toast.Make("No longer tracking the ISS!").Show();
                return;
            }

            await GetIssAsync();
            dispatcherTimer.Start();
            await Toast.Make("Tracking the location of the ISS!").Show();
        }

        [RelayCommand]
        async Task WtiaPageLoadedAsync()
        {
            hasApiKey = HasMapsSdkApiKey();
            if (hasApiKey)
                return;

            await Shell.Current.DisplayAlert("Warning", $"No API Key provided for the Maps SDK for Android. {System.Environment.NewLine} {System.Environment.NewLine} Google Maps features are disabled!", "OK");
        }
    }
}
