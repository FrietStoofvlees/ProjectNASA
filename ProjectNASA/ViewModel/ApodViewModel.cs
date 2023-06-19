using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ProjectNASA.ViewModel
{
    [QueryProperty(nameof(SelectedDate), "SelectedDate")]
    public partial class ApodViewModel : BaseViewModel
    {
        readonly IApodService apodService;
        readonly IConnectivity connectivity;

        [ObservableProperty]
        Apod apod;

        [ObservableProperty]
        DateTime selectedDate;

        public ApodViewModel(IApodService apodService, IConnectivity connectivity)
        {
            this.apodService = apodService;
            this.connectivity = connectivity;

            Title = "Astronomy Picture of the Day!";
        }

        [RelayCommand]
        async Task GetApodAsync()
        {
            try
            {
                if (IsBusy) return;

                if (connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await Shell.Current.DisplayAlert("No connectivity!", "Check your internet connection", "OK");
                    await Shell.Current.GoToAsync($"..", true);
                    return;
                }

                IsBusy = true;
                Apod = await apodService.GetAstronomyPictureOfGivenDateAsync(SelectedDate);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("The following error occured:", ex.Message, "OK");
            }
            finally 
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        async Task OnImageTappedAsync(Apod apod)
        {
            if (apod == null)
                return;

            await Shell.Current.GoToAsync(nameof(ImagePage), true, new Dictionary<string, object>
            {
                {"Apod", apod},
            });

            SemanticScreenReader.Announce("Picture in High Definition:" + apod.Title);  
        }

        [RelayCommand]
        async Task FavoriteApodAsync()
        {
            await Shell.Current.GoToAsync("..", true, new Dictionary<string, object>
            {
                {"Apod", Apod }
            });

            Apod = null;

            SemanticScreenReader.Announce("Picture has been saved to your favorites!");
        }
    }
}
