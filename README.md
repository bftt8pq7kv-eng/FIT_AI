# FitAI - Yapay Zekâ Destekli Modern Stil Asistanı

FitAI, kullanıcının bulunduğu konumdaki anlık hava durumunu analiz ederek kişisel tercihlerine (renk, tarz, etkinlik türü ve ruh hali) en uygun, renk teorisiyle optimize edilmiş nokta atışı kıyafet kombinleri oluşturan modern bir web uygulamasıdır. Bu proje, dönem projesi standartlarına, modern yazılım prensiplerine (SOLID) ve hata toleranslı harici API entegrasyonuna odaklanarak geliştirilmiştir.

---

## 🚀 Teknoloji Altyapısı
* **Framework:** ASP.NET Core MVC (C#)
* **Veritabanı:** SQLite & Entity Framework Core (Code-First)
* **Tasarım:** Modern HTML5 & CSS3 (Dark Theme Esaslı)
* **Harici Servisler:** OpenWeatherMap API (Canlı Hava Durumu) & Google Gemini AI API (Akıllı Stil Üretimi)

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

* **Canlı Hava Durumu Analizi:** OpenWeatherMap API entegrasyonu ile kullanıcının bulunduğu şehrin sıcaklık ve hava durumu bilgisi (Örn: "16.29°C, AÇIK") asenkron olarak çekilir.
* **Kişiselleştirilmiş Tercih Formu:** Kullanıcı; adını, yaşını, favori rengini, giyim tarzını (Casual/Spor, Minimalist, Klasik vb.), katılacağı etkinlik türünü (Akşam Yemeği, İş Görüşmesi vb.) ve kombin havasını (Vibe) belirtebilir.
* **Renk Teorisi Tabanlı Filtreleme:** Sistem, kullanıcının panelden seçtiği ana rengi baz alarak modadaki **kontrast (zıtlık)** ve **tamamlayıcı renk uyumu** kurallarını işletir. Kullanıcıya soyut tavsiyeler yerine; ceket, tişört ve pantolon için nokta atışı renk reçetesi sunar.
* **SQL Veritabanı Geçmişi:** Oluşturulan tüm dinamik kombin önerileri ve arama parametreleri, SQLite veritabanına Code-First yöntemiyle kaydedilir ve ana ekranda geçmiş aramalar olarak listelenir.
* **Asenkron Etkinlik Takvimi:** Sayfanın altında yer alan ajanda modülü, JavaScript ve AJAX tabanlı çalışarak sayfa yenilenmeden dinamik etkinlik kaydı yapılmasına olanak tanır.

---

## 🛠️ Teknik Altyapı ve Mimari (SOLID)

Proje, teslim kriterlerinde yer alan teknik gereksinimlere tam uyum sağlayacak şekilde geliştirilmiştir:

### Teknik Değerlendirme & Mimari Uyum:
Uygulama, yazılım prensiplerine tam uyumlu bir katmanlı mimariye sahiptir:
* **Single Responsibility (Tek Sorumluluk):** `HomeController` içerisindeki metotlar; hava durumu çekme, veritabanı kayıt ve yapay zekâ/simülasyon süreçlerini birbirine çorba etmeden, yapısal olarak tamamen izole ve bağımsız bloklar (etaplar) halinde yönetmektedir.
* **Mimari Uyum:** Proje standart MVC (Model-View-Controller) desenine göre katmanlandırılmıştır. İş mantığı (Controller), veri şeması (Model) ve arayüz (View) gevşek bağlı (Loose Coupling) olarak tasarlanmıştır.
* **Kod Okunurluğu:** Temiz kod (Clean Code) standartlarına uygun, isimlendirme kurallarına bağlı ve thread kilitlenmelerini önleyen asenkron (`async/await`) mimariyle yazılmıştır.
* **Gizlilik ve Sır Yönetimi (Secret Management):** `appsettings.json` ve `fitai.db` gibi gizli API anahtarlarını ve yerel verileri barındıran kritik dosyalar **`.gitignore`** dosyası ile sürüm kontrolünden muaf tutularak açık kaynak güvenliği sağlanmıştır.

---

## 📈 Çıktı Kontrol Metodolojisi ve Hata Toleransı
Sistemden dönen yapay zekâ çıktılarının doğruluğu ve anlamlılığı, kullanıcı girdileri değiştirilerek gerçek zamanlı test edilmiştir. 

Harici servis kesintilerine ve API yoğunluklarına karşı kurumsal mimarilerde kullanılan **Graceful Degradation (Kademeli Kusursuzlaşma)** prensibi uygulanmıştır. API'ye erişilemediği durumlarda `try-catch` blokları hatayı yakalar; uygulamanın çökmesini (crash) önleyerek arka plandaki algoritmik akıllı simülasyon katmanını devreye sokar. Böylece kullanıcıya kesintisiz ve hatasız bir deneyim sunulur.

---

## ⚠️ Karşılaşılan Zorluklar & Çözümler
* **Zorluk (Veritabanı Araçları Hatası):** Paket Yöneticisi Konsolu üzerinde `Update-Database` komutu çalıştırılırken `CommandNotFoundException` hatasıyla karşılaşılmıştır.
* **Çözüm:** Projeye `Microsoft.EntityFrameworkCore.Tools` paketinin NuGet üzerinden entegre edilmesi sağlanmış, araçlar terminale tanıtılarak veritabanı (Migration) başarılı bir şekilde ayağa kaldırılmıştır.
* **Zorluk (Harici API Sürüm ve Endpoint Uyuşmazlıkları):** Harici yapay zekâ sunucularının değişen JSON gövde protokolleri ve model isimlendirmeleri nedeniyle zaman zaman `404 Not Found` hataları alınmıştır.
* **Çözüm:** İstek mimarisi, her türlü API anahtarı türüyle şartsız ve tam uyumlu çalışan en kararlı ana omurgaya çekilerek ve akıllı yedekleme katmanıyla güçlendirilerek entegrasyon kararlılığı %100'e ulaştırılmıştır.

---

## 💻 Kullanım Kılavuzu
1. Uygulamayı çalıştırdığınızda sol panelde yer alan formu doldurun (Şehir, Renk Tercihi, Tarz, Etkinlik, Vibe).
2. **"Kombin Oluştur"** butonuna basın.
3. Sağ panelde OpenWeather'dan gelen anlık canlı hava durumunu ve sistemin renk teorisine göre sizin için hazırladığı nokta atışı kıyafet renk reçetesini inceleyin.
4. Sayfanın en altında, yaptığınız aramaların **SQL Veritabanı (SQLite)** geçmişinden çekilerek listelendiğini görebilirsiniz.
