namespace ProjectNasa.View;

public partial class ApodPage : ContentPage
{
	public ApodPage(ApodViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}

