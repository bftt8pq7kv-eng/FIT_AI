using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace FIT_AI.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "BURAYA_HAVA_DURUMU_API_KEYINIZ_GELECEK";

        public WeatherService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> GetWeatherAsync(string city)
        {
            try
            {
                string url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={_apiKey}&units=metric&lang=tr";
                var response = await _httpClient.GetStringAsync(url);
                var json = JObject.Parse(response);

                string temp = json["main"]["temp"].ToString();
                string description = json["weather"][0]["description"].ToString();

                return $"{city} şu an {temp}°C ve hava {description}.";
            }
            catch
            {
                return $"{city} için hava durumu alınamadı (Varsayılan: 20°C, Açık).";
            }
        }
    }
}