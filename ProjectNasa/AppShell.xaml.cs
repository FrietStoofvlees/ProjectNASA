namespace ProjectNasa;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(ApodPage), typeof(ApodPage));
		Routing.RegisterRoute(nameof(ImagePage), typeof(ImagePage));
    }

    public void SwitchtoTab(int tabIndex)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            switch (tabIndex)
            {
                case 0: shelltabbar.CurrentItem = shelltab_0; break;
                case 1: shelltabbar.CurrentItem = shelltab_1; break;
                case 2: shelltabbar.CurrentItem = shelltab_2; break;
            };
        });
    }
}
