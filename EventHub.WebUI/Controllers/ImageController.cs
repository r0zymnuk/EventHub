using EventHub.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventHub.WebUI.Controllers;
[Route("image")]
public class ImageController : Controller
{
    private readonly IImageService _imageService;

    public ImageController(IImageService imageService)
    {
        _imageService = imageService;
    }

    [HttpGet("get/{imageName}")]
    public async Task<IActionResult> GetImage(string imageName)
    {
        var image = await _imageService.GetImageAsync(imageName);
        return File(image, "image/jpeg");
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadImage(IFormFile image)
    {
        var imageName = await _imageService.UploadImageAsync(image);
        if (imageName == null)
        {
            return BadRequest();
        }
        return Ok(imageName);
    }

    [HttpPost("delete/{imageName}")]
    public async Task<IActionResult> DeleteImage(string imageName)
    {
        var isDeleted = await _imageService.DeleteImageAsync(imageName);
        return Ok(isDeleted);
    }
}
