using Kalender.Data;
using Microsoft.Maui.Devices.Sensors;
using System.Text.Json;

namespace Kalender.ViewModels
{
    internal class HomePageViewModel
    {
        public Models.WeatherApi Weather { get; set; }
        public string Fact { get; set; }

        public HomePageViewModel()
        {
            var task1 = Task.Run(() => GetWeather());
            task1.Wait();
            Weather = task1.Result;

            var task2 = Task.Run(() => GetDateFact());
            task2.Wait();
            Fact = task2.Result;

        }
        public static async Task<Models.WeatherApi> GetWeather()
        {
            var location = Singletons.Authorized.GetAuthStatus().UserLocation();
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://api.api-ninjas.com/");
            client.DefaultRequestHeaders.Add("X-Api-Key", "0SdiJM+HXPbdpg973bh43w==ZTSJKQf13WtqqxiM");

            Models.WeatherApi weatherResponse = null;

            HttpResponseMessage response = await client.GetAsync($"v1/weather?city={location}");
            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                weatherResponse = JsonSerializer.Deserialize<Models.WeatherApi>(responseString);
            }
            return weatherResponse;
        }
        public static async Task<string> GetDateFact()
        {
            var currentDate = DateTime.Now;
            string date = currentDate.ToString("MM'/'dd");
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://numbersapi.com/");

            string fact = null;

            HttpResponseMessage response = await client.GetAsync($"{date}");
            if (response.IsSuccessStatusCode)
            {
                fact = await response.Content.ReadAsStringAsync();
            }
            return fact;
        }
    }
}
