namespace ProjectNASA.Services
{
    public interface IApodService
    {
        Task<Apod> GetAstronomyPictureoftheDayAsync();

    }
}
