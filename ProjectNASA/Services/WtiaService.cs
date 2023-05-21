using System.Net.Http.Json;

namespace ProjectNASA.Services
{
    public class WtiaService : IWtiaService
    {
        readonly HttpClient httpClient;

        public WtiaService()
        {
            httpClient = new();
        }

        public async Task<Iss> GetIssCurrentLocationAsync()
        {
            Iss iss = new(); 

            HttpResponseMessage response = await httpClient.GetAsync("https://api.wheretheiss.at/v1/satellites/" + iss.Id.ToString());

            if (response.IsSuccessStatusCode)
            {
                iss = await response.Content.ReadFromJsonAsync<Iss>();
            }

            response.Dispose();
            return iss;
        }
    }
}
