namespace ProjectNasa.View;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

	private void OnApodPageClicked(object sender, EventArgs e)
	{
        ((AppShell)App.Current.MainPage).SwitchtoTab(1);
        //await Shell.Current.GoToAsync(nameof(ApodPage), true, new Dictionary<string, object>());

		SemanticScreenReader.Announce(btnApodPage.Text);
	}
}

