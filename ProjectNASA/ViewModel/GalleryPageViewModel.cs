using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace ProjectNASA.ViewModel
{
    [QueryProperty(nameof(Apod), "Apod")]
    public partial class GalleryPageViewModel : BaseViewModel
    {
        readonly IApodService apodService;

        [ObservableProperty]
        DateTime selectedDate;

        public ObservableCollection<Apod> FavoriteApods { get; } = new();

        Apod apod;
        public Apod Apod 
        {
            get { return apod; }
            set
            {
                apod = value;
                if (!FavoriteApods.Contains(apod))
                {
                    //TODO: combine both in custom Add method?
                    FavoriteApods.Add(apod);
                    FavoriteApods.OrderByDate();
                }
            }
        }

        public GalleryPageViewModel(IApodService apodService)
        {
            this.apodService = apodService;
            SelectedDate = DateTime.Today;
        }

        [RelayCommand]
        async Task GoToApodPageAsync()
        {
            await Shell.Current.GoToAsync(nameof(ApodPage), true, new Dictionary<string, object>()
            {
                { "SelectedDate", SelectedDate }
            });
        }

        [RelayCommand]
        async Task GoToFavoriteDetailsPageAsync(Apod apod)
        {
            if (apod == null)
                return;

            await Shell.Current.GoToAsync(nameof(FavoriteDetailsPage), true, new Dictionary<string, object>()
            {
                { "Apod", apod }
            });
        }

        [RelayCommand]
        void RemoveApod(Apod apod)
        {
            if (apod == null)
                return;

            FavoriteApods.Remove(apod);
        }
    }
}
