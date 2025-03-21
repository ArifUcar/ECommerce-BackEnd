E-Commerce FrontEnd Linki: https://github.com/ArifUcar/E-Commerce-Frontend/blob/master/README.md


E-Commerce BackEnd Dökümanatasyonu
Projede çalışabilmesi için src/AU-Framework.WebAPI/appsettings.json dosyasındaki ConnectionStrings kısmını aşağıdaki gibi düzenlemeniz gerekmektedir:
json
KopyalaDüzenle
"ConnectionStrings": {
  "SqlServer": "Server=DESKTOP-I79O76V;Database=E-CommerceDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
Bu şekilde bağlantı dizesini güncelleyebilirsiniz.
 
Proje Genel Yapısı
AU-Framework, katmanlı mimari (layered architecture) prensiplerine göre tasarlanmış bir .NET Core web uygulaması çerçevesidir. Proje, e-ticaret işlemlerini yönetmek için tasarlanmıştır.

Katmanlar
1. Domain Katmanı (AU-Framework.Domain)
 Temel varlıkları (entities) içerir
  BaseEntity sınıfı ile tüm varlıklar için ortak özellikleri tanımlar
  Soft Delete işlemleri içerir
  DTO (Data Transfer Objects) sınıflarını barındırır
  Varlık ilişkilerini tanımlar
2. Uygulama Katmanı (AU-Framework.Application)
 CQRS (Command Query Responsibility Segregation) pattern'i kullanır
  MediatR kütüphanesi ile komut ve sorguları yönetir
  Validation işlemleri için FluentValidation kullanır
  Servis arayüzlerini (interfaces) tanımlar
  Repository pattern arayüzlerini içerir
3. Persistence Katmanı (AU-Framework.Persistance)
  Entity Framework Core ile veritabanı işlemlerini yönetir
  Repository pattern implementasyonlarını içerir
  Servis implementasyonlarını barındırır
  Entity konfigürasyonlarını yönetir
  Veritabanı bağlantı ve işlemlerini gerçekleştirir
4. Presentation Katmanı (AU-Framework.Presentation)
  API Controller'ları içerir
  Temel API Controller yapısını tanımlar
  İstek yönlendirmelerini yönetir
5. WebAPI Katmanı (AU-Framework.WebAPI)
  Uygulamanın giriş noktasıdır
  Middleware'leri yapılandırır
  Bağımlılık enjeksiyonunu (Dependency Injection) yönetir
  JWT authentication yapılandırmasını içerir
  Swagger dokümantasyonunu yapılandırır
Önemli Özellikler
1. Güvenlik
  JWT tabanlı kimlik doğrulama
  Rol tabanlı yetkilendirme
  Şifre hashleme ve doğrulama servisleri
  Validation
  FluentValidation ile giriş doğrulama
  Custom validation kuralları
  Validation pipeline behavior
  Loglama
  Kapsamlı hata loglama
  İstek/yanıt loglama
  Özel exception middleware
 Resim İşleme
  Base64 görsel işleme
  Görsel kaydetme ve silme işlemleri
  Güvenli dosya adı oluşturma
  Veritabanı
  Entity Framework Core
  Code-first yaklaşımı
  İlişkisel veritabanı tasarımı
  Repository pattern
Temel İş Akışları
  Ürün Yönetimi
  Ürün ekleme, güncelleme, silme
  Ürün detayları yönetimi
  Stok takibi
  Kategori ilişkileri
  Kategori Yönetimi
  Kategori oluşturma ve düzenleme
  Kategori-ürün ilişkileri
  Soft-delete işlemleri
  Kullanıcı Yönetimi
  Kullanıcı kaydı ve girişi
  Rol tabanlı yetkilendirme
  Şifre yönetimi
  Sipariş Yönetimi
  Sipariş oluşturma
  Sipariş durumu takibi
  Sipariş detayları

Teknolojiler ve Kütüphaneler
•  .NET Core: Ana framework
•  Entity Framework Core: ORM
•  MediatR: CQRS implementasyonu
•  FluentValidation: Validation işlemleri
•  AutoMapper: Nesne dönüşümleri
•  JWT: Kimlik doğrulama
•  Swagger: API dokümantasyonu
