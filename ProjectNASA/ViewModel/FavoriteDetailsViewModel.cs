using CommunityToolkit.Mvvm.ComponentModel;

namespace ProjectNASA.ViewModel
{
    [QueryProperty(nameof(Apod), "Apod")]
    public partial class FavoriteDetailsViewModel : BaseViewModel
    {
        [ObservableProperty]
        Apod apod;
    }
}
