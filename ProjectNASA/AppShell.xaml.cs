namespace ProjectNASA;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(ApodPage), typeof(ApodPage));
        Routing.RegisterRoute(nameof(FavoriteDetailsPage), typeof(FavoriteDetailsPage));
        Routing.RegisterRoute(nameof(GalleryPage), typeof(GalleryPage));
        Routing.RegisterRoute(nameof(ImagePage), typeof(ImagePage));
        Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
        Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
        Routing.RegisterRoute(nameof(SignInPage), typeof(SignInPage));
        Routing.RegisterRoute(nameof(WtiaPageMobile), typeof(WtiaPageMobile));
    }

    public void SwitchtoTab(TabPages tabPage)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            switch (tabPage)
            {
                case TabPages.MainPage:
                    shellTabBar.CurrentItem = tabMainPage;
                    break;
                case TabPages.GalleryPage:
                    shellTabBar.CurrentItem = tabGalleryPage;
                    break;
                case TabPages.ProfilePage:
                    shellTabBar.CurrentItem = tabProfilePage;
                    break;
            };
        });
    }
}
