using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace FIT_AI.Services
{
    public class AIService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "BURAYA_GEMINI_API_KEYINIZ_GELECEK";

        public AIService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> GetOutfitSuggestionAsync(string weatherInfo, string colors, string style, string eventType, string vibe)
        {
            try
            {
                string url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent?key={_apiKey}";

                string prompt = $"Hava Durumu: {weatherInfo}. Kullanıcı Tercihleri -> Renkler: {colors}, Tarz: {style}, Etkinlik: {eventType}, Hava/Vibe: {vibe}. Bu bilgilere uygun, şık ve kısa bir kıyafet kombin önerisi hazırla. Maddeler halinde yaz.";

                var requestBody = new
                {
                    contents = new[]
                    {
                        new { parts = new[] { new { text = prompt } } }
                    }
                };

                string jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(responseString);
                    return json["candidates"][0]["content"]["parts"][0]["text"].ToString();
                }

                return "Yapay zekâ şu an yanıt veremiyor, lütfen daha sonra tekrar deneyin.";
            }
            catch
            {
                return "Bağlantı hatası: Kombin önerisi oluşturulamadı.";
            }
        }
    }
}
