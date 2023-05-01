using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.HotReload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectNASA.ViewModel
{
    public partial class ApodViewModel : BaseViewModel
    {
        readonly IApodService apodService;
        readonly IConnectivity connectivity;

        [ObservableProperty]
        Apod apod;

        [ObservableProperty]
        bool isRefreshing;

        public ApodViewModel(IApodService apodService, IConnectivity connectivity)
        {
            this.apodService = apodService;
            this.connectivity = connectivity;

            Title = "Astronomy Picture of the Day!";

            Task.Run(GetApodAsync);
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
                    return;
                }

                IsBusy = true;

                if (Apod != null)
                {
                    if (Apod.Date < DateOnly.FromDateTime(DateTime.Now))
                        Apod = await apodService.GetAstronomyPictureoftheDayAsync();
                    else return;
                }
                else
                    Apod = await apodService.GetAstronomyPictureoftheDayAsync();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("The following error occured:", ex.Message, "OK");
            }
            finally 
            {
                IsBusy = false;
                IsRefreshing = false;
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
    }
}
