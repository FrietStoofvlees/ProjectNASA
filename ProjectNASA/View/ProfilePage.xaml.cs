namespace ProjectNASA.View;

public partial class ProfilePage : ContentPage
{
	public ProfilePage(ProfilePageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();

		await (BindingContext as ProfilePageViewModel).CheckUserLogin();
    }
}