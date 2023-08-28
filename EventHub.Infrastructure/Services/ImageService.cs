using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace EventHub.Infrastructure.Services;
public class ImageService : IImageService
{
    private readonly string _imageFolderPath;

    public ImageService(string imageFolderPath)
    {
        _imageFolderPath = imageFolderPath;
    }

    public async Task<string> UploadImageAsync(IFormFile imageFile)
    {
        if (imageFile == null || imageFile.Length == 0)
        {
            return null!;
        }

        string fileName = $"{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";
        string imagePath = Path.Combine(_imageFolderPath, fileName);

        using (var stream = new FileStream(imagePath, FileMode.Create))
        {
            await imageFile.CopyToAsync(stream);
        }

        return fileName;
    }

    public async Task<byte[]> GetImageAsync(string imageName)
    {
        imageName = Path.Combine(_imageFolderPath, imageName);
        return await File.ReadAllBytesAsync(imageName);
    }

    public async Task<bool> DeleteImageAsync(string imageName)
    {
        imageName = Path.Combine(_imageFolderPath, imageName);
        if (File.Exists(imageName))
        {
            await Task.Run(() => File.Delete(imageName));
            return true;
        }
        return false;
    }
}