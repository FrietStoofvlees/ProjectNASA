using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ProjectNASA.ViewModel
{
    public partial class SettingsViewModel : BaseViewModel
    {
        readonly IConnectivity connectivity;
        [ObservableProperty]
        string name;
        [ObservableProperty]
        string packageName;
        [ObservableProperty]
        string version;
        public SettingsViewModel(IConnectivity connectivity)
        {
            this.connectivity = connectivity;
            Name = AppInfo.Name;
            PackageName = AppInfo.PackageName;
            Version = AppInfo.VersionString;

            Title = "Settings";
        }

        [RelayCommand]
        async Task GoToGitHubAsync()
        {
            try
            {
                if (connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await Shell.Current.DisplayAlert("No connectivity!", "Check your internet connection", "OK");
                    return;
                }
                Uri uri = new("https://github.com/FrietStoofvlees/ProjectNASA");
                await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception)
            {
                await Toast.Make("Unable to open the browser on this device.").Show();
            }
        }

        [RelayCommand]
        static void ShowAppSettings()
        {
            AppInfo.Current.ShowSettingsUI();
        }

        [RelayCommand]
        static async Task ShowEasterEggAsync()
        {
            await Shell.Current.DisplayAlert("Easter Egg", "Now you can stop tapping!", "Okay...");
        }
    }
}
