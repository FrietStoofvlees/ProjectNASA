namespace ProjectNASA.View;

public partial class ImagePage : ContentPage
{
	public ImagePage(ImageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
