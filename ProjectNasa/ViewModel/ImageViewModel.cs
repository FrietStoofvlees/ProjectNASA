using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ProjectNasa.ViewModel
{
    [QueryProperty(nameof(Apod), "Apod")]
    public partial class ImageViewModel : BaseViewModel
    {
        readonly IFileSaver fileSaver;
        CancellationTokenSource cancellationTokenSource = new();

        [ObservableProperty]
        Apod apod;

        public ImageViewModel(IFileSaver fileSaver)
        {
            this.fileSaver = fileSaver;
        }

        [RelayCommand]
        async void SavePicture() 
        {
            try
            {
                byte[] imageArray;

                using (HttpClient client = new())
                {
                    imageArray = await client.GetByteArrayAsync(Apod.Hdurl);
                }

                using var stream = new MemoryStream(imageArray);
                var fileSaverResult = await fileSaver.SaveAsync($"{Apod.Title}.jpg", stream, cancellationTokenSource.Token);
                fileSaverResult.EnsureSuccess();
                await Toast.Make($"File is saved: {fileSaverResult.FilePath}").Show(cancellationTokenSource.Token);
            }
            catch (Exception)
            {
                //TODO
                throw new NotImplementedException();
            }
        }
    }
}
