using System;

namespace FIT_AI.Models
{
    public class OutfitHistory
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Colors { get; set; }
        public string Style { get; set; }
        public string EventType { get; set; }
        public string Vibe { get; set; }
        public string WeatherInfo { get; set; }
        public string AiSuggestion { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}