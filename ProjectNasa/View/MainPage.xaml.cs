namespace ProjectNasa.View;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

	private void OnApodPageClicked(object sender, EventArgs e)
	{
        ((AppShell)Application.Current.MainPage).SwitchtoTab(TabPages.ApodPage);
        //await Shell.Current.GoToAsync(nameof(ApodPage), true, new Dictionary<string, object>());

		SemanticScreenReader.Announce(btnApodPage.Text);
	}
}

