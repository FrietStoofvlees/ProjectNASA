using Newtonsoft.Json;
using System.Globalization;
using System.Net;
using System.Net.Http.Json;

namespace ProjectNASA.Services
{
    public class ApodService : IApodService
    {
        readonly HttpClient httpClient;

        Apod apod;

        public ApodService()
        {
            httpClient = new();
        }

        public async Task<Apod> GetAstronomyPictureOfGivenDateAsync(DateTime dateTime)
        {
            if (dateTime == DateTime.Today)
            {
                return await GetAstronomyPictureOftheDayAsync();
            }

            string formattedDate = dateTime.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo);

            HttpResponseMessage response = await httpClient.GetAsync($"https://api.nasa.gov/planetary/apod?api_key={Constants.ApodApiKey}&date={formattedDate}");

            if (response.IsSuccessStatusCode)
            {
                return await ConvertJsonToApodAsync(response);
            }
            return null;
        }

        public async Task<Apod> GetAstronomyPictureOftheDayAsync()
        {
            if (apod is not null)
                return apod;

            HttpResponseMessage response = await httpClient.GetAsync($"https://api.nasa.gov/planetary/apod?api_key={Constants.ApodApiKey}");

            if (response.IsSuccessStatusCode)
            {
                //apod = await response.Content.ReadFromJsonAsync<Apod>();

                //response = await httpClient.GetAsync($"{apod.Hdurl}");

                //if (response.StatusCode == HttpStatusCode.NotFound)
                //{
                //    apod.Hdurl = apod.Url;
                //}

                apod = await ConvertJsonToApodAsync(response);
                return apod;
            }

            await Shell.Current.DisplayAlert($"HTTP response was not succesfull:", response.ReasonPhrase, "OK");

            return null;
        }

        async Task<Apod> ConvertJsonToApodAsync(HttpResponseMessage response)
        {
            Apod apod;

            string content = await response.Content.ReadAsStringAsync();

            try
            {
                apod = JsonConvert.DeserializeObject<Apod>(content, new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                });
            }
            catch (JsonSerializationException ex)
            {
                await Shell.Current.DisplayAlert("The following error occured during deserialization: ", ex.Message, "OK");
                return null;
            }

            response = await httpClient.GetAsync($"{apod.Hdurl}");

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                apod.Hdurl = apod.Url;
            }

            response.Dispose();
            return apod;
        }
    }
}
