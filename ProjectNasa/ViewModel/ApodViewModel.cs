using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectNasa.ViewModel
{
    public partial class ApodViewModel : BaseViewModel
    {
        IApodService apodService;
        IConnectivity connectivity;

        [ObservableProperty]
        Apod apod;

        [ObservableProperty]
        bool isNotSet;

        public ApodViewModel(IApodService apodService, IConnectivity connectivity)
        {
            this.apodService = apodService;
            this.connectivity = connectivity;

            Title = "Astronomy Picture of the Day!";
            IsNotSet = true;
        }

        [RelayCommand]
        async Task GetApodAsync()
        {
            try
            {
                if (connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await Shell.Current.DisplayAlert("No connectivity!", "Check your internet connection", "OK");
                    return;
                }

                if (Apod != null)
                {
                    if (Apod.Date < DateOnly.FromDateTime(DateTime.Now))
                    {
                        Apod = await apodService.GetAstronomyPictureoftheDayAsync();
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    Apod = await apodService.GetAstronomyPictureoftheDayAsync();
                }

                IsNotSet = false;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("The following error occured:", ex.Message, "OK");
            }
        }
    }
}
