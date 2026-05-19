# FitAI - Yapay Zekâ Destekli Modern Stil Asistanı

FitAI, kullanıcının bulunduğu konumdaki anlık hava durumunu analiz ederek kişisel tercihlerine (renk, tarz, etkinlik türü ve ruh hali) en uygun, kişiselleştirilmiş kıyafet kombinleri oluşturan modern bir web uygulamasıdır.

## 🚀 Teknoloji Altyapısı
* **Framework:** ASP.NET Core MVC (C#)
* **Veritabanı:** SQLite & Entity Framework Core (Code-First)
* **Tasarım:** Modern HTML5 & CSS3 (Dark Theme Esaslı)
* **Harici Servisler:** OpenWeatherMap API (Canlı Hava Durumu Entegrasyonu)
# FitAI - Yapay Zekâ Destekli Modern Stil Asistanı

FitAI, kullanıcının bulunduğu konumdaki anlık hava durumunu analiz ederek kişisel tercihlerine (renk, tarz, etkinlik türü ve ruh hali) en uygun, kişiselleştirilmiş kıyafet kombinleri oluşturan modern bir web uygulamasıdır. Bu proje, dönem projesi standartlarına, modern yazılım prensiplerine (SOLID) ve dil modeli entegrasyonuna odaklanarak geliştirilmiştir.

---

## 🚀 Proje Görünümü (Tasarım Mockup'ları)

Aşağıdaki görseller, FitAI uygulamasının modern ve minimalist tasarım vizyonunu yansıtan arayüz tasarım örneklerini (kadın ve erkek kullanıcılar için) temsil etmektedir. Gerçek uygulama, bu görsellerdeki tasarım dilini ve işlevsel akışı esas alarak kodlanmıştır.

### 👩 Kadın Kullanıcı Arayüzü Tasarım Örneği (Elbise & Pastel Tonlar)
<img src="images/dashboard_female.png" alt="FitAI Dashboard Design Mockup - Female User" width="100%" />

*Görsel 1: FitAI Kadın Kullanıcı Arayüzü ve Elbise Kombini Tasarım Örneği*

---

### 👨 Erkek Kullanıcı Arayüzü Tasarım Örneği (Smart Casual)
<img src="images/dashboard_male.png" alt="FitAI Dashboard Design Mockup - Male User" width="100%" />

*Görsel 2: FitAI Erkek Kullanıcı Arayüzü Tasarım Örneği*

---

## 📖 Temel Özellikler
Uygulama, kullanıcının stil danışmanlığını üstlenerek şu işlevleri sunar:

* **Canlı Hava Durumu Analizi:** OpenWeatherMap API entegrasyonu ile kullanıcının bulunduğu şehrin sıcaklık ve hava durumu bilgisi (Örn: "18°C, Parçalı Bulutlu") anlık olarak çekilir.
* **Kişiselleştirilmiş Tercih Formu:** Kullanıcı; favori renklerini, giyim tarzını (Spor, Klasik, Modern vb.), katılacağı etkinlik türünü (İş Görüşmesi, Akşam Yemeği vb.) ve kombin havasını (Vibe) belirtebilir.
* **Yapay Zekâ Entegrasyonu (Gemini):** Tüm girdiler, projenin "zekâ" katmanını oluşturan Google Gemini dil modeline (gemini-1.5-flash) gönderilir. Model, bir moda uzmanı gibi analiz yaparak nokta atışı kombin reçeteleri üretir.
* **SQL Veritabanı Geçmişi:** Oluşturulan tüm kombin önerileri, hocanın kriterleri doğrultusunda SQLite veritabanına Code-First yöntemiyle kaydedilir ve ana ekranda geçmiş aramalar olarak listelenir.

---

## 🛠️ Teknik Altyapı ve Mimari (SOLID)

Proje, teslim kriterlerinde yer alan teknik gereksinimlere tam uyum sağlayacak şekilde geliştirilmiştir:

* **Framework:** ASP.NET Core MVC (C#)
* **Veritabanı:** SQLite & Entity Framework Core (Code-First)
* **Tasarım:** Modern HTML5 & CSS3 (Dark Theme Esaslı)
* **Harici Servisler:** OpenWeatherMap API (Canlı Hava Durumu)

### Teknik Değerlendirme & Mimari Uyum:
Uygulama, yazılım prensiplerine tam uyumlu bir katmanlı mimariye sahiptir:
* **Single Responsibility (Tek Sorumluluk):** `WeatherService` yalnızca hava durumu verisinden, `AIService` yalnızca yapay zekâ üretiminden sorumludur. Sorumluluklar birbirine karıştırılmamıştır.
* **Mimari Uyum:** Proje standart MVC (Model-View-Controller) desenine göre katmanlandırılmıştır.
* **Kod Okunurluğu:** Temiz kod (Clean Code) standartlarına uygun, isimlendirme kurallarına bağlı ve asenkron (async/await) mimariyle yazılmıştır.
* **Çalışır Olması:** Proje yerel sunucuda SQL veritabanı bağlantısı ve canlı API akışlarıyla eksiksiz çalışmaktadır.

---

## 📈 Çıktı Kontrol Metodolojisi
Sistemden dönen yapay zekâ çıktılarının doğruluğu ve anlamlılığı, kullanıcı girdileri değiştirilerek (Örn: Şehir değiştirildiğinde sıcaklığa göre mont/tişört dengesinin kontrolü) test edilmiştir. Boş veya hatalı API yanıtlarına karşı `try-catch` blokları ile hata yakalama mekanizmaları (Hata durumunda varsayılan kombin önerisi) kurulmuştur.

---

## ⚠️ Karşılaşılan Zorluklar & Çözümler
* **Zorluk (Veritabanı Araçları Hatası):** Paket Yöneticisi Konsolu üzerinde `Update-Database` komutu çalıştırılırken `CommandNotFoundException` hatasıyla karşılaşılmıştır.
* **Çözüm:** Projeye `Microsoft.EntityFrameworkCore.Tools` paketinin NuGet üzerinden entegre edilmesi sağlanmış, araçlar terminale tanıtılarak veritabanı (Migration) başarılı bir şekilde ayağa kaldırılmıştır.

---

## 💻 Kullanım Kılavuzu
1. Uygulamayı çalıştırdığınızda sol panelde yer alan formu doldurun (Şehir, Renk Tercihi, Tarz, Etkinlik, Vibe).
2. **"Kombin Oluştur"** butonuna basın.
3. Sağ panelde anlık hava durumunu ve yapay zekânın sizin için hazırladığı özel kombin reçetesini inceleyin.
4. Sayfanın en altında, yaptığınız aramaların **SQL Veritabanı** geçmişinden çekilerek listelendiğini görezilirsiniz.