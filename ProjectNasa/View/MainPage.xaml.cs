namespace ProjectNasa.View;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

	private async void OnApodPageClicked(object sender, EventArgs e)
	{

		await Shell.Current.GoToAsync(nameof(ApodPage), true, new Dictionary<string, object>());

		SemanticScreenReader.Announce(btnApodPage.Text);
	}
}

