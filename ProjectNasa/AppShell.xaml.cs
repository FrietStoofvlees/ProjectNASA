namespace ProjectNasa;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(ApodPage), typeof(ApodPage));
		Routing.RegisterRoute(nameof(ImagePage), typeof(ImagePage));
    }
}
