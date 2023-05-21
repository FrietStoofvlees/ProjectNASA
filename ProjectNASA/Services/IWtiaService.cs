namespace ProjectNASA.Services
{
    public interface IWtiaService
    {
        Task<Iss> GetIssCurrentLocationAsync();
    }
}
