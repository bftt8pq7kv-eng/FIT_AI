namespace FIT_AI.Models
{
    public class UserPreference
    {
        public string City { get; set; }
        public string FavColors { get; set; }
        public string ClothingStyle { get; set; }
        public string EventType { get; set; }
        public string Vibe { get; set; }
        public string Gender { get; set; }

        // === YENİ PROFİL ALANLARI ===
        public string UserName { get; set; }
        public int UserAge { get; set; }
    }
}