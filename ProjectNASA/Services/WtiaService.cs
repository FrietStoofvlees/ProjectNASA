using System.Globalization;
using System.Net.Http.Json;

namespace ProjectNASA.Services
{
    public class WtiaService : IWtiaService
    {
        readonly HttpClient httpClient;
        readonly NumberFormatInfo numberFormat = new CultureInfo("en-US", false).NumberFormat;

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

            response = await httpClient.GetAsync($"https://api.wheretheiss.at/v1/coordinates/{iss.Latitude.ToString(numberFormat)},{iss.Longitude.ToString(numberFormat)}");

            Coordinates coordinates = await response.Content.ReadFromJsonAsync<Coordinates>();

            iss.Coordinates = coordinates;

            response.Dispose();
            return iss;
        }
    }
}
