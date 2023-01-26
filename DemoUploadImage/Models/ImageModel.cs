using Microsoft.AspNetCore.Http;

namespace DemoUploadImage.Models
{
    public class ImageModel
    {
        public IFormFile FormFile { get; set; }
    }
}
