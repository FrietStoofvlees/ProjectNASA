using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ProjectNasa.Services
{
    public class ApodService : IApodService
    {
        Apod apod;
        readonly HttpClient httpClient;

        public ApodService()
        {
            apod = new();
            httpClient = new();
        }

        public async Task<Apod> GetAstronomyPictureoftheDayAsync()
        {
            HttpResponseMessage response = await httpClient.GetAsync("https://api.nasa.gov/planetary/apod?api_key=y2gF3d8nbF9WMcNSgvYkXqCbtqaHgeNBZP9ZQCZ1");

            if (response.IsSuccessStatusCode)
            {
                apod = await response.Content.ReadFromJsonAsync<Apod>();
            }

            return apod;
        }
    }
}
