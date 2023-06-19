using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProjectNASA.Data;
using System.Collections.ObjectModel;

namespace ProjectNASA.ViewModel
{
    [QueryProperty(nameof(Apod), "Apod")]
    public partial class GalleryPageViewModel : BaseViewModel
    {
        readonly IAuthService authService;
        readonly IUserRepository userRepository;

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
                    FavoriteApods.Add(apod);
                    FavoriteApods.OrderByDate();
                    Task.Run(SaveFavoriteApods);
                }
            }
        }

        public GalleryPageViewModel(IAuthService authService, IUserRepository userRepository)
        {
            this.authService = authService;
            this.userRepository = userRepository;
            SelectedDate = DateTime.Today;
        }

        [RelayCommand]
        async Task FillGalleryAsync()
        {
            if (await authService.HasAuthenticationAsync())
            {
                FavoriteApods.Clear();
                foreach (Apod favApod in AppHelpers.User.FavoriteApods)
                {
                    FavoriteApods.Add(favApod);
                }
            }
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
        async Task RemoveApodAsync(Apod apod)
        {
            if (apod == null)
                return;

            FavoriteApods.Remove(apod);
            await SaveFavoriteApods();
        }

        async Task SaveFavoriteApods()
        {
            try
            {
                if (await authService.HasAuthenticationAsync())
                {
                    AppHelpers.User.FavoriteApods = FavoriteApods.ToList();
                    userRepository.SaveUser(AppHelpers.User);
                }
            }
            catch (Exception)
            {
                await Toast.Make("Your profile could not be updated in the database!").Show();
            }
        }
    }
}
