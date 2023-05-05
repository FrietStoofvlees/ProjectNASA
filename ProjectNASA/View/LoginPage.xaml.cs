namespace ProjectNASA.View;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginPageViewModel viewmodel)
	{
		InitializeComponent();
		BindingContext = viewmodel;
	}
}