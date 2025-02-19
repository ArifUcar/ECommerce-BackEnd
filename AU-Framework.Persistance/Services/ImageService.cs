using AU_Framework.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace AU_Framework.Persistance.Services;

public sealed class ImageService : IImageService
{
    private readonly ILogger<ImageService> _logger;
    private const string UploadDirectory = "wwwroot/images/products";

    public ImageService(ILogger<ImageService> logger)
    {
        _logger = logger;
    }

    public string ConvertToBase64(IFormFile file)
    {
        try
        {
            using var ms = new MemoryStream();
            file.CopyTo(ms);
            var fileBytes = ms.ToArray();
            return Convert.ToBase64String(fileBytes);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Dosya Base64'e dönüştürülürken hata oluştu");
            throw new Exception("Dosya Base64'e dönüştürülürken hata oluştu", ex);
        }
    }

    public string SaveBase64Image(string base64String, string fileName)
    {
        try
        {
            if (string.IsNullOrEmpty(base64String))
                throw new ArgumentNullException(nameof(base64String), "Base64 string boş olamaz!");

            // Base64 string'in başındaki "data:image/..." kısmını temizle
            if (base64String.Contains(","))
            {
                base64String = base64String.Split(',')[1];
            }

            if (!IsBase64Valid(base64String))
                throw new Exception("Geçersiz Base64 formatı!");

            // Upload dizinini oluştur
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), UploadDirectory);
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            // Dosya adını güvenli hale getir
            var safeFileName = Path.GetFileNameWithoutExtension(fileName)
                .Replace(" ", "_")
                .Replace("-", "_");
            var fileNameWithGuid = $"{Guid.NewGuid()}_{safeFileName}.jpg";
            var filePath = Path.Combine(UploadDirectory, fileNameWithGuid);

            var imageBytes = Convert.FromBase64String(base64String);
            File.WriteAllBytes(filePath, imageBytes);

            _logger.LogInformation($"Görsel başarıyla kaydedildi: {filePath}");
            return filePath;
        }
        catch (FormatException ex)
        {
            _logger.LogError(ex, "Geçersiz Base64 formatı");
            throw new Exception("Geçersiz Base64 formatı. Lütfen doğru formatta bir görsel yükleyin.", ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Base64 görsel kaydedilirken hata oluştu");
            throw new Exception("Görsel kaydedilirken bir hata oluştu.", ex);
        }
    }

    public void DeleteImage(string imagePath)
    {
        try
        {
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), imagePath);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
                _logger.LogInformation($"Görsel silindi: {imagePath}");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Görsel silinirken hata oluştu");
            throw new Exception("Görsel silinirken bir hata oluştu.", ex);
        }
    }

    public bool IsBase64Valid(string base64String)
    {
        if (string.IsNullOrWhiteSpace(base64String)) return false;
        
        try
        {
            // Base64 string uzunluğu 4'ün katı olmalı
            if (base64String.Length % 4 != 0) return false;

            // Base64 string sadece geçerli karakterleri içermeli
            if (!Regex.IsMatch(base64String, @"^[a-zA-Z0-9\+/]*={0,3}$")) return false;

            Convert.FromBase64String(base64String);
            return true;
        }
        catch
        {
            return false;
        }
    }
} 