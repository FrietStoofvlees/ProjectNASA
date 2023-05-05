namespace ProjectNASA.View;

public partial class ImagePage : ContentPage
{
	public ImagePage(ImageViewModel viewmodel)
	{
		InitializeComponent();
		BindingContext = viewmodel;
	}
}