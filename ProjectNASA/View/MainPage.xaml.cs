namespace ProjectNASA.View;

public partial class MainPage : ContentPage
{
	public MainPage(WtiaViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
