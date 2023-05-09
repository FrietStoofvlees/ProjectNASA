using System.Net.Http.Json;

namespace ProjectNASA.Services
{
    public class WTIAService : IWTIAService
    {
        ISS iss;
        readonly HttpClient httpClient;

        public WTIAService()
        {
            iss = new()
            { 
                Id = 25544, // ISS NORAD catalog id = 25544
            };
            httpClient = new();
        }

        public async Task<ISS> GetISSCurrentLocationAsync()
        {
            HttpResponseMessage response = await httpClient.GetAsync("https://api.wheretheiss.at/v1/satellites/" + iss.Id.ToString());

            if (response.IsSuccessStatusCode)
            {
                iss = await response.Content.ReadFromJsonAsync<ISS>();
            }

            httpClient.Dispose();
            response.Dispose();

            return iss;
        }
    }
}
