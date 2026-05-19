using FIT_AI.Controllers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// === SİHİRLİ DOKUNUŞ: DEPENDENCY INJECTION KAYDI ===
// FitAiDbContext'i SQLite veritabanı sağlayıcısı ile sisteme kaydediyoruz.
// Veritabanı dosyası proje klasöründe "fitai.db" adıyla otomatik oluşacaktır.
builder.Services.AddDbContext<FitAiDbContext>(options =>
    options.UseSqlite("Data Source=fitai.db"));

// MVC Controller ve View yapılarını sisteme ekliyoruz
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Veritabanının ve tabloların ilk açılışta otomatik oluşturulmasını garanti altına alıyoruz
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<FitAiDbContext>();
    dbContext.Database.EnsureCreated();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Varsayılan sayfa rotası (İlk olarak HomeController altındaki Index tetiklenir)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();