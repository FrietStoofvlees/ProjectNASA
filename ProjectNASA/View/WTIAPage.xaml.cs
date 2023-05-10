namespace ProjectNASA.View;

public partial class WTIAPage : ContentPage
{
	public WTIAPage(WTIAViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}