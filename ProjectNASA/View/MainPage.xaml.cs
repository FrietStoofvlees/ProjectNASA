namespace ProjectNASA.View;

public partial class MainPage : ContentPage
{
    readonly MainPageViewModel viewModel;

	public MainPage(MainPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
        this.viewModel = viewModel;
    }

#if ANDROID
    //https://github.com/dotnet/maui/issues/14471
    protected override void OnAppearing()
    {
        base.OnAppearing();

        viewModel.Github14471Hack(nameof(viewModel.Apod));
    }
#endif
}
