namespace ProjectNASA.Services
{
    public interface IApodService
    {
        Task<Apod> GetAstronomyPictureoftheDayAsync();

        Task<Apod> GetAstronomyPictureofGivenDateAsync(DateTime dateTime);

    }
}
