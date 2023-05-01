using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ProjectNASA.ViewModel
{
    [QueryProperty(nameof(Apod), "Apod")]
    public partial class ImageViewModel : BaseViewModel
    {
        readonly IFileSaver fileSaver;
        CancellationTokenSource cancellationTokenSource = new();

        [ObservableProperty]
        Apod apod;
        [ObservableProperty]
        bool isVisible;

        byte[] imageArray;

        public ImageViewModel(IFileSaver fileSaver)
        {
            this.fileSaver = fileSaver;
            IsBusy = false;
            IsVisible = true;
        }

        partial void OnApodChanged(Apod value)
        {
            Task.Run(GetImageBytesAsync);
        }

        async Task<byte[]> GetImageBytesAsync()
        {
            while (imageArray == null)
            {
                using HttpClient client = new();
                imageArray = await client.GetByteArrayAsync(Apod.Hdurl);
            }
            return imageArray;
        }

        [RelayCommand]
        async Task SavePictureAsync()
        {
             IsBusy = true;

#if ANDROID33_0
            await Toast.Make("This is function is currently not supported on Android 13").Show();
                return;
#endif

#if !ANDROID33_0
            try
            {
                if (imageArray == null)
                {
                    await GetImageBytesAsync();
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
            finally
            {
                IsBusy = false;
            }
#endif
        }
    }
}
