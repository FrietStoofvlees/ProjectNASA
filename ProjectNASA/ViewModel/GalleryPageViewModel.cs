using CommunityToolkit.Maui.Alerts;
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
                if (!FavoriteApods.Contains(value))
                {
                    FavoriteApods.Add(value);
                }
                apod = null;
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
                {"SelectedDate", SelectedDate }
            });
        }

        [RelayCommand]
        void RemoveApod(Apod apod)
        {
            FavoriteApods.Remove(apod);
        }
    }
}
