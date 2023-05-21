using System.Globalization;
using System.Net;
using System.Net.Http.Json;

namespace ProjectNASA.Services
{
    public class ApodService : IApodService
    {
        readonly HttpClient httpClient;

        public ApodService()
        {
            httpClient = new();
        }

        public async Task<Apod> GetAstronomyPictureofGivenDateAsync(DateTime dateTime)
        {
            if (dateTime == DateTime.Today)
            {
                return await GetAstronomyPictureoftheDayAsync();
            }

            Apod apod = new();

            string formattedDate = dateTime.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo);

            HttpResponseMessage response = await httpClient.GetAsync("https://api.nasa.gov/planetary/apod?api_key=DEMO_KEY&start_date=" + formattedDate);

            if (response.IsSuccessStatusCode)
            {
                apod = (await response.Content.ReadFromJsonAsync<List<Apod>>()).FirstOrDefault();

                response = await httpClient.GetAsync($"{apod.Hdurl}");

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    apod.Hdurl = apod.Url;
                }
            }

            response.Dispose();
            return apod;
        }

        public async Task<Apod> GetAstronomyPictureoftheDayAsync()
        {
            Apod apod = new();

            HttpResponseMessage response = await httpClient.GetAsync("https://api.nasa.gov/planetary/apod?api_key=DEMO_KEY");

            if (response.IsSuccessStatusCode)
            {
                apod = await response.Content.ReadFromJsonAsync<Apod>();

                response = await httpClient.GetAsync($"{apod.Hdurl}");

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    apod.Hdurl = apod.Url;
                }
            }

            response.Dispose();
            return apod;
        }
    }
}
