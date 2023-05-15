using CommunityToolkit.Maui.Core.Platform;

namespace ProjectNASA.View;

public partial class ProfilePage : ContentPage
{
	public ProfilePage(ProfilePageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
