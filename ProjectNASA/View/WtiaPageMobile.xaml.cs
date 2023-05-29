namespace ProjectNASA.View;

public partial class WtiaPageMobile : ContentPage
{
    public WtiaPageMobile(WtiaViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }
}
