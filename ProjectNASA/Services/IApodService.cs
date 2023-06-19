namespace ProjectNASA.Services
{
    public interface IApodService
    {
        Task<Apod> GetAstronomyPictureOftheDayAsync();
        Task<Apod> GetAstronomyPictureOfGivenDateAsync(DateTime dateTime);
    }
}
