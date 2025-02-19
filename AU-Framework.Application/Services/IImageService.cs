using Microsoft.AspNetCore.Http;

namespace AU_Framework.Application.Services;

public interface IImageService
{
    string ConvertToBase64(IFormFile file);
    string SaveBase64Image(string base64String, string fileName);
    void DeleteImage(string imagePath);
    bool IsBase64Valid(string base64String);
} 