namespace ProjectNASA.View;

public partial class GalleryPage : ContentPage
{
	public GalleryPage(GalleryPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}