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
        async Task SavePictureAsync()
        {
            //TODO: werkt niet op android 13 -> geen READ/WRITE_EXTERNAL_STORAGE permissions meer
#if ANDROID33_0
            await Toast.Make("This is function is currently not supported on Android 13").Show();
                return;
#endif

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
            catch (Exception ex)
            {
                await Toast.Make(ex.Message).Show(cancellationTokenSource.Token);
            }
        }
    }
}
