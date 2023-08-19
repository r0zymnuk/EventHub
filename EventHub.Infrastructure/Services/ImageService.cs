using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        string fileName = Path.GetFileName(imageFile.FileName);
        string imagePath = Path.Combine(_imageFolderPath, fileName);

        using (var stream = new FileStream(imagePath, FileMode.Create))
        {
            await imageFile.CopyToAsync(stream);
        }

        return fileName;
    }

    public async Task<byte[]> GetImageAsync(string imagePath)
    {
        imagePath = Path.Combine(_imageFolderPath, imagePath);
        return await File.ReadAllBytesAsync(imagePath);
    }
}