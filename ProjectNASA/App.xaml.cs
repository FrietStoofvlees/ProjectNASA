namespace ProjectNASA;

public partial class App : Application
{
	public static User User;

	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}
}
