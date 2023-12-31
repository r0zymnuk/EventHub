﻿using Microsoft.AspNetCore.Http;

namespace EventHub.Application.Services;
public interface IImageService
{
    Task<string> UploadImageAsync(IFormFile imageFile);
    Task<byte[]> GetImageAsync(string imageName);
    Task<bool> DeleteImageAsync(string imageName);
}
