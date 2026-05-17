namespace FIT_AI.Models
{
    public class UserPreference
    {
        public string City { get; set; }        // Hava durumu için şehir
        public string FavColors { get; set; }   // Favori renkler
        public string ClothingStyle { get; set; } // Spor, Klasik vb.
        public string EventType { get; set; }   // Düğün, İş görüşmesi vb.
        public string Vibe { get; set; }        // Kombin havası (Enerjik, cool)
    }
}