namespace ProjectNASA.View;

public partial class SignInPage : ContentPage
{
	public SignInPage(SignInPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

	protected override bool OnBackButtonPressed()
	{ 
		return true;
	}
}
