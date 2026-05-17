using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using FIT_AI.Data;
using FIT_AI.Models;
using FIT_AI.Services;
using Microsoft.EntityFrameworkCore;

namespace FIT_AI.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly WeatherService _weatherService;
        private readonly AIService _aiService;

        // SOLID: Servisleri dışarıdan enjekte (Dependency Injection) ediyoruz.
        public HomeController(AppDbContext context)
        {
            _context = context;
            _weatherService = new WeatherService();
            _aiService = new AIService();
        }

        // Kullanıcı sayfayı ilk açtığında çalışacak olan bölüm (GET)
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Geçmiş kombin önerilerini veritabanından çekip listeliyoruz
            var history = await _context.OutfitHistories
                .OrderByDescending(x => x.CreatedAt)
                .Take(5)
                .ToListAsync();

            ViewBag.History = history;
            return View();
        }

        // Kullanıcı formdaki "Kombin Oluştur" butonuna bastığında çalışacak bölüm (POST)
        [HttpPost]
        public async Task<IActionResult> Index(UserPreference preference)
        {
            // 1. Canlı Hava Durumunu Getir
            string weatherInfo = await _weatherService.GetWeatherAsync(preference.City);

            // 2. Yapay Zekadan Kombin Önerisi Al
            string aiSuggestion = await _aiService.GetOutfitSuggestionAsync(
                weatherInfo,
                preference.FavColors,
                preference.ClothingStyle,
                preference.EventType,
                preference.Vibe
            );

            // 3. Verileri SQL Veritabanına Kaydet
            var historyRecord = new OutfitHistory
            {
                City = preference.City,
                Colors = preference.FavColors,
                Style = preference.ClothingStyle,
                EventType = preference.EventType,
                Vibe = preference.Vibe,
                WeatherInfo = weatherInfo,
                AiSuggestion = aiSuggestion
            };

            _context.OutfitHistories.Add(historyRecord);
            await _context.SaveChangesAsync();

            // Ekran görüntüsünde sonucu basmak için ViewBag kullanıyoruz
            ViewBag.Result = aiSuggestion;
            ViewBag.Weather = weatherInfo;

            // Güncel geçmiş listesini tekrar yükle
            ViewBag.History = await _context.OutfitHistories
                .OrderByDescending(x => x.CreatedAt)
                .Take(5)
                .ToListAsync();

            return View();
        }
    }
}