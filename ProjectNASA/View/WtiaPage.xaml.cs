namespace ProjectNASA.View;

public partial class WtiaPage : ContentPage
{
	public WtiaPage(WtiaViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }
}
