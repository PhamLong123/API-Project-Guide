using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Http.Description;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ImageService.Models;
using ResourceService.Services;
using Microsoft.AspNetCore.Hosting;

namespace ImageServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourceController : ControllerBase
    {
        private readonly IWebHostEnvironment env;
        public ResourceController(IWebHostEnvironment env)
        {
            this.env = env;
        }

        [HttpPost]
        [Route("Upload")]
        [ResponseType(typeof(FileResultInfo))]
        public IActionResult Upload()
        {
            FileResultInfo result = new FileResultInfo();

            if (!Request.HasFormContentType || !Request.Form.Files.Any())
            {
                result.Status = false;
                result.Message = HttpStatusCode.UnsupportedMediaType.ToString();
                return Ok(result);
            }

            IFormFile file = Request.Form.Files.GetFiles(
                Param.File).FirstOrDefault();
            if(file == null)
            {
                result.Status = false;
                result.Message = HttpStatusCode.NoContent.ToString();
            }

            string directory = string.Empty;
            if (Request.Form.ContainsKey(Param.Directory))
            {
                directory = Request.Form[Param.Directory].FirstOrDefault();
                if(directory == null)
                {
                    directory = string.Empty;
                }
            }
            directory = Path.Combine(Param.CommonPath, directory);

            string fileName = string.Empty;
            if (Request.Form.ContainsKey(Param.FileName))
            {
                fileName = Request.Form[Param.FileName].FirstOrDefault();
            }
            if (string.IsNullOrWhiteSpace(fileName))
            {
                fileName = DateTime.Now.ToString(Format.File);
            }

            int width = 0;
            int height = 0;
            double mBytes = 0;
            if(Request.Form.ContainsKey(Param.ImageFrame))
            {
                string imageFrame = Request.Form[Param.ImageFrame]
                    .FirstOrDefault();
                if(!string.IsNullOrWhiteSpace(imageFrame))
                {
                    string[] imageFrameValues = imageFrame.Split(Param
                        .SplitFrame[0]);
                    if(imageFrameValues.Length == 2)
                    {
                        fileName += "_" + imageFrame;
                        int.TryParse(imageFrameValues[0], out width);
                        int.TryParse(imageFrameValues[1], out height);
                    }
                }
            }

            if (Request.Form.ContainsKey(Param.ImageSize))
            {
                string imageSize = Request.Form[Param.ImageSize]
                    .FirstOrDefault();
                if (!string.IsNullOrWhiteSpace(imageSize))
                {
                    double.TryParse(imageSize, out mBytes);
                }
            }

            string startupPath = env.ContentRootPath;
            string filePath = Path.Combine(startupPath, directory);
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            string savePath = Path.Combine(directory, fileName);
            result.Status = true;
            if (file.Length > 0)
            {
                string saveFile = savePath + Path.GetExtension(
                    file.FileName);
                using (Stream fileStream = new FileStream(Path.Combine(
                    startupPath, saveFile), FileMode.Create))
                {
                    file.CopyTo(fileStream);

                    Utility.Instance.ResizeImage(saveFile, width, height,
                        mBytes);

                    //result.FilePath = saveFile.Replace("\\", "/");
                    result.FilePath = fileName;
                }
            }

            return Ok(result);
        }
    }
}
