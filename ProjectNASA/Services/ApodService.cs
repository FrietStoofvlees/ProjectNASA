﻿using Newtonsoft.Json;
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

            string formattedDate = dateTime.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo);

            HttpResponseMessage response = await httpClient.GetAsync("https://api.nasa.gov/planetary/apod?api_key=DEMO_KEY&date=" + formattedDate);

            if (response.IsSuccessStatusCode)
            {
                return await ConvertJsonToApodAsync(response);
            }
            return null;
        }

        public async Task<Apod> GetAstronomyPictureoftheDayAsync()
        {
            HttpResponseMessage response = await httpClient.GetAsync("https://api.nasa.gov/planetary/apod?api_key=y2gF3d8nbF9WMcNSgvYkXqCbtqaHgeNBZP9ZQCZ1");

            if (response.IsSuccessStatusCode)
            {
                //apod = await response.Content.ReadFromJsonAsync<Apod>();

                //response = await httpClient.GetAsync($"{apod.Hdurl}");

                //if (response.StatusCode == HttpStatusCode.NotFound)
                //{
                //    apod.Hdurl = apod.Url;
                //}

                return await ConvertJsonToApodAsync(response);
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
                    MissingMemberHandling = MissingMemberHandling.Error,
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
