namespace ProjectNASA.Services
{
    public interface IWTIAService
    {
        Task<ISS> GetISSCurrentLocationAsync();
    }
}
