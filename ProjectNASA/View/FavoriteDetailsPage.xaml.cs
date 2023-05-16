namespace ProjectNASA.View;

public partial class FavoriteDetailsPage : ContentPage
{
    public FavoriteDetailsPage(FavoriteDetailsViewModel favoriteDetailsViewModel)
    {
        InitializeComponent();
        BindingContext = favoriteDetailsViewModel;
    }
}
