using APIConnector;
using DemoUploadImage.Models;
using ImageService.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DemoUploadImage.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHostingEnvironment _env;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IHostingEnvironment env)
        {
            _logger = logger;
            _env = env;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult UploadImage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UploadImage(ImageModel model)
        {
            UploadFileInfo uploadFileInfo = new UploadFileInfo
            {
                BaseImageUrl = "http://192.168.99.9:8000", // Địa chỉ app server hình ảnh, thay đổi theo triển khai
                UploadFunction = "api/Resource/Upload", // Hàm upload file, không thay đổi
                DirectoryPath = "Test", // Thư mục lưu ảnh, không bắt buộc, khi không khai báo sẽ lấy thư mục gốc
                SaveAsName = "test" // Tên file ảnh, không bắt buộc, khi không khai báo hệ thống tự sinh file đảm bảo duy nhất
            };

            string path = SaveTmp(model.FormFile, uploadFileInfo.SaveAsName);
            if(string.IsNullOrWhiteSpace(path))
            {
                ViewBag.Err = "Không thể lưu file tạm.";
                return ViewBag();
            }
            uploadFileInfo.File = path;
            FileResultInfo fileResultInfo = Helper.UploadFile(uploadFileInfo);
            if(fileResultInfo.Status)
            {
                ViewBag.Url = Path.Combine(uploadFileInfo.BaseImageUrl, fileResultInfo.FilePath).Replace("\\", "/");
                DeleteTmp(path);
            }
            else
            {
                ViewBag.Err = fileResultInfo.Message;
            }    
            return View();
        }

        private string SaveTmp(IFormFile file, string saveAsName)
        {
            string result = string.Empty;

            if (file.Length > 0)
            {
                if(string.IsNullOrWhiteSpace(saveAsName))
                {
                    saveAsName = file.FileName;
                }

                result = Path.Combine(_env.WebRootPath, saveAsName + Path
                    .GetExtension(file.FileName));
                using (var fileStream = new FileStream(result,
                    FileMode.Create, FileAccess.Write))
                {
                    file.CopyTo(fileStream);
                }
            }

            return result;
        }

        private void DeleteTmp(string filePath)
        {
            System.IO.File.Delete(filePath);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
