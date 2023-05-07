﻿namespace ProjectNASA;


public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(ApodPage), typeof(ApodPage));
		Routing.RegisterRoute(nameof(ImagePage), typeof(ImagePage));
        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
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
                case TabPages.ApodPage:
                    shellTabBar.CurrentItem = tabApodPage;
                    break;
                case TabPages.ProfilePage:
                    shellTabBar.CurrentItem = tabProfilePage;
                    break;
            };
        });
    }
}
