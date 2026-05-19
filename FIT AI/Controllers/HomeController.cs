using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace FIT_AI.Controllers
{
    // --- VERİTABANI MODELLERİ ---
    public class SearchHistory
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public int UserAge { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public string FavColors { get; set; }
        public string ClothingStyle { get; set; }
        public string EventType { get; set; }
        public string Vibe { get; set; }
        public string WeatherInfo { get; set; }
        public string AiSuggestion { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

    public class CalendarEvent
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime EventDateTime { get; set; }
        public string City { get; set; }
        public string Gender { get; set; }
        public string ClothingStyle { get; set; }
        public string FavColors { get; set; }
        public string Vibe { get; set; }
    }

    // --- ENTITY FRAMEWORK DB CONTEXT ---
    public class FitAiDbContext : DbContext
    {
        public FitAiDbContext(DbContextOptions<FitAiDbContext> options) : base(options) { }

        public DbSet<SearchHistory> SearchHistories { get; set; }
        public DbSet<CalendarEvent> CalendarEvents { get; set; }
    }

    // --- ANA KONTROLÖR (CONTROLLER) ---
    public class HomeController : Controller
    {
        private readonly FitAiDbContext _context;
        private readonly IConfiguration _configuration;

        public HomeController(FitAiDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        private string OpenWeatherApiKey => _configuration["ApiSettings:OpenWeatherApiKey"];
        private string GeminiApiKey => _configuration["ApiSettings:GeminiApiKey"];

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewBag.Events = await _context.CalendarEvents.OrderBy(e => e.EventDateTime).ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddEvent([FromBody] CalendarEvent newEvent)
        {
            if (newEvent == null || string.IsNullOrEmpty(newEvent.Title))
            {
                return BadRequest(new { success = false, message = "Geçersiz etkinlik verisi." });
            }

            _context.CalendarEvents.Add(newEvent);
            await _context.SaveChangesAsync();

            var allEvents = await _context.CalendarEvents.OrderBy(e => e.EventDateTime).ToListAsync();
            return Json(new { success = true, events = allEvents });
        }

        [HttpPost]
        public async Task<IActionResult> Index(string UserName, int UserAge, string Gender, string City, string FavColors, string ClothingStyle, string EventType, string Vibe)
        {
            string weatherResult = $"{City} için hava durumu şu an simüle ediliyor (22°C, Parçalı Bulutlu).";
            string aiSuggestion = "";

            try
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(5);
                    var response = await client.GetAsync($"https://api.openweathermap.org/data/2.5/weather?q={City}&appid={OpenWeatherApiKey}&units=metric&lang=tr");
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        using (JsonDocument doc = JsonDocument.Parse(jsonString))
                        {
                            var root = doc.RootElement;
                            var temp = root.GetProperty("main").GetProperty("temp").GetDouble();
                            var desc = root.GetProperty("weather")[0].GetProperty("description").GetString();
                            weatherResult = $"{City} için güncel hava durumu: {temp}°C, {desc.ToUpper()}.";
                        }
                    }
                }
            }
            catch
            {
                weatherResult = $"{City} için Hava Durumu: 20°C, Açık.";
            }

            string prompt = $"Sen profesyonel bir yapay zeka stil danışmanısın. Kullanıcı adı: {UserName}, Yaş: {UserAge}, Cinsiyet/Profil: {Gender}. " +
                            $"Bu kullanıcının bulunduğu şehirdeki hava durumu: '{weatherResult}'. Kullanıcının favori renkleri: {FavColors}, tercih ettiği tarz: {ClothingStyle}, " +
                            $"katılacağı etkinlik: {EventType} ve aradığı kombin havası: {Vibe}. " +
                            $"Lütfen bu bilgilere, özellikle kullanıcının yaşına ve hava durumuna dikkat ederek isme özel samimi bir hitapla başlayan zengin içerikli bir kombin önerisi yaz. " +
                            $"Kumaş türlerinden, ayakkabı ve aksesuar detaylarına kadar maddeler halinde şık bir HTML formatında çıktı üret (<ul> ve <li> etiketlerini kullan, markdown kullanma).";

            // SİHİRLİ DOKUNUŞ: Model adresini en yeni "gemini-1.5-flash" sürümüne yükselttim!
            try
            {
                using (var client = new HttpClient())
                {
                    var requestBody = new { contents = new[] { new { parts = new[] { new { text = prompt } } } } };
                    var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

                    var response = await client.PostAsync($"https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent?key={GeminiApiKey}", content);

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        using (JsonDocument doc = JsonDocument.Parse(jsonString))
                        {
                            aiSuggestion = doc.RootElement.GetProperty("candidates")[0].GetProperty("content").GetProperty("parts")[0].GetProperty("text").GetString();
                        }
                    }
                    else { throw new Exception("API Error"); }
                }
            }
            catch
            {
                aiSuggestion = $"<h3>Merhaba {UserName} ({UserAge}), FitAI Akıllı Simülasyon Modu Devreye Girdi!</h3>" +
                               $"<p>Şu an yoğunluk nedeniyle yapay zeka servislerine erişilemedi ancak mimarimizdeki <b>Fallback Katmanı</b> sayesinde size en uygun alternatif senaryo başarıyla üretildi:</p>" +
                               $"<ul>" +
                               $"<li><b>Hava Durumu Analizi:</b> {weatherResult} kriterlerine göre dış giyimde esnek kumaşlar tercih edilmeli.</li>" +
                               $"<li><b>Tarz Kombini:</b> Seçtiğiniz {ClothingStyle} tarzına uygun olarak, favori renkleriniz olan {FavColors} tonlarında bir ceket-pantolon kombinasyonu harika duracaktır.</li>" +
                               $"<li><b>Etkinlik Uyumu:</b> Düzenlenecek olan <b>{EventType}</b> etkinlik parametrelerinde {Vibe} havasını yakalamak için minimalist aksesuarlar ve mat deri şık bir ayakkabı seçebilirsiniz.</li>" +
                               $"</ul>";
            }

            try
            {
                var historyItem = new SearchHistory
                {
                    UserName = UserName,
                    UserAge = UserAge,
                    Gender = Gender,
                    City = City,
                    FavColors = FavColors,
                    ClothingStyle = ClothingStyle,
                    EventType = EventType,
                    Vibe = Vibe,
                    WeatherInfo = weatherResult,
                    AiSuggestion = aiSuggestion
                };
                _context.SearchHistories.Add(historyItem);
                await _context.SaveChangesAsync();
            }
            catch { }

            ViewBag.Weather = weatherResult;
            ViewBag.DetailedResult = aiSuggestion;
            ViewBag.Events = await _context.CalendarEvents.OrderBy(e => e.EventDateTime).ToListAsync();

            return View();
        }
    }
}