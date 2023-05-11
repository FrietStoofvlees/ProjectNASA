#if ANDROID
using Android.Content.PM;
using Android.OS;
#endif
using CommunityToolkit.Maui.Alerts;

using CommunityToolkit.Mvvm.ComponentModel;

namespace ProjectNASA.ViewModel
{
    public partial class WTIAViewModel : BaseViewModel
    {
        readonly IConnectivity connectivity;
        readonly IWTIAService wTIAService;

        [ObservableProperty]
        ISS iss;

        public WTIAViewModel(IWTIAService wTIAService, IConnectivity connectivity)
        {
            this.wTIAService = wTIAService;
            this.connectivity = connectivity;

            if (!IsMapsSDKApiKeySet())
            {
                Toast.Make("No API Key provided for the Maps SDK for Android! Google map features are disabled").Show();
            }
        }

        private static bool IsMapsSDKApiKeySet()
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
