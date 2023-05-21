namespace ProjectNASA.View;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

	private async void OnApodPageClickedAsync(object sender, EventArgs e)
	{
        //((AppShell)Application.Current.MainPage).SwitchtoTab(TabPages.ApodPage);
        await Shell.Current.GoToAsync(nameof(WtiaPage), true, new Dictionary<string, object>());

		//SemanticScreenReader.Announce(btnApodPage.Text);
	}
}
