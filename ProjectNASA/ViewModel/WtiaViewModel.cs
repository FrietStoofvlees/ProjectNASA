#if ANDROID
using Android.Content.PM;
using Android.OS;
#endif
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ProjectNASA.ViewModel
{
    public partial class WtiaViewModel : BaseViewModel
    {
        readonly IConnectivity connectivity;
        readonly IWtiaService wtiaService;

        [ObservableProperty]
        Iss iss;

        public WtiaViewModel(IWtiaService wtiaService, IConnectivity connectivity)
        {
            this.wtiaService = wtiaService;
            this.connectivity = connectivity;
        }

        [RelayCommand]
        static async Task WtiaPageLoadedAsync()
        {
            if (IsMapsSdkApiKeySet())
                return;

            await Shell.Current.DisplayAlert("Warning", $"No API Key provided for the Maps SDK for Android. {System.Environment.NewLine} {System.Environment.NewLine} Google Maps features are disabled!", "OK");
        }

        private static bool IsMapsSdkApiKeySet()
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

            //AppInfo.Current.ShowSettingsUI(); 
#endif
            return true;
        }
    }
}
