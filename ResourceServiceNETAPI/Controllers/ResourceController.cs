using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using ImageService.Models;
using ResourceService.Services;

namespace ResourceServiceAPI.Controllers
{
    public class ResourceController : ApiController
    {
        [HttpPost]
        [Route("api/Resource/UploadImage")]
        [ResponseType(typeof(FileResultInfo))]
        public IHttpActionResult UploadImage()
        {
            FileResultInfo result = new FileResultInfo();

            //if (!Request.Content.IsMimeMultipartContent())
            //{
            //    result.Status = false;
            //    result.Message = HttpStatusCode.UnsupportedMediaType.ToString();
            //    return Ok(result);
            //}
            
            if (!HttpContext.Current.Request.Files.AllKeys.Contains(Param.File))
            {
                result.Status = false;
                result.Message = HttpStatusCode.NoContent.ToString();
                return Ok(result);
            }

            HttpPostedFile file = HttpContext.Current.Request.Files[Param.File];
            if (file == null)
            {
                result.Status = false;
                result.Message = HttpStatusCode.NoContent.ToString();
                return Ok(result);
            }

            string directory = string.Empty;
            if(HttpContext.Current.Request.Form.AllKeys.Contains(Param
                .Directory))
            {
                directory = HttpContext.Current.Request.Form.GetValues(Param
                    .Directory).FirstOrDefault();
            }
            directory = Path.Combine(Param.CommonPath, directory);

            string fileName = string.Empty;
            if (HttpContext.Current.Request.Form.AllKeys.Contains(Param
                .FileName))
            {
                fileName = HttpContext.Current.Request.Form.GetValues(Param
                    .FileName).FirstOrDefault();
            }
            if (string.IsNullOrWhiteSpace(fileName))
            {
                fileName = DateTime.Now.ToString(Format.File);
            }

            int width = 0;
            int height = 0;
            double mBytes = 0;
            string imageFrame = string.Empty;
            if (HttpContext.Current.Request.Form.AllKeys.Contains(Param
                .ImageFrame))
            {
                imageFrame = HttpContext.Current.Request.Form.GetValues(
                    Param.ImageFrame).FirstOrDefault();
            }
            if (!string.IsNullOrWhiteSpace(imageFrame))
            {
                string[] imageFrameValues = imageFrame.Split(Param
                    .SplitFrame[0]);
                if (imageFrameValues.Length == 2)
                {
                    fileName += "_" + imageFrame;
                    int.TryParse(imageFrameValues[0], out width);
                    int.TryParse(imageFrameValues[1], out height);
                }
            }

            string imageSize = string.Empty;
            if (HttpContext.Current.Request.Form.AllKeys.Contains(Param
                .ImageSize))
            {
                imageSize = HttpContext.Current.Request.Form.GetValues(
                    Param.ImageSize).FirstOrDefault();
            }
            if (!string.IsNullOrWhiteSpace(imageSize))
            {
                double.TryParse(imageSize, out mBytes);
            }

            string startupPath = HttpContext.Current.Server.MapPath("/");
            string filePath = Path.Combine(startupPath, directory);
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            string savePath = Path.Combine(directory, fileName);
            result.Status = true;
            if (file.ContentLength > 0)
            {
                string saveFile = savePath + Path.GetExtension(
                    file.FileName);

                byte[] postedFile = null;
                using (var binaryReader = new BinaryReader(HttpContext.Current
                    .Request.Files[Param.File]
                    .InputStream))
                {
                    postedFile = binaryReader.ReadBytes(HttpContext.Current
                        .Request.Files[Param.File]
                        .ContentLength);
                }

                using (Image image = Image.FromStream(new MemoryStream(
                    postedFile)))
                {
                    image.Save(Path.Combine(startupPath, saveFile));
                    image.Dispose();
                    Utility.Instance.ResizeImage(Path.Combine(startupPath, 
                        saveFile), width, height,
                        mBytes);

                    //result.FilePath = saveFile.Replace("\\", "/");
                    result.FilePath = fileName + Path.GetExtension(
                        file.FileName);
                }
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("api/Resource/UploadVideo")]
        [ResponseType(typeof(FileResultInfo))]
        public IHttpActionResult UploadVideo()
        {
            FileResultInfo result = new FileResultInfo();

            //if (!Request.Content.IsMimeMultipartContent())
            //{
            //    result.Status = false;
            //    result.Message = HttpStatusCode.UnsupportedMediaType.ToString();
            //    return Ok(result);
            //}

            if (!HttpContext.Current.Request.Files.AllKeys.Contains(Param.File))
            {
                result.Status = false;
                result.Message = HttpStatusCode.NoContent.ToString();
                return Ok(result);
            }

            HttpPostedFile file = HttpContext.Current.Request.Files[Param.File];
            if (file == null)
            {
                result.Status = false;
                result.Message = HttpStatusCode.NoContent.ToString();
                return Ok(result);
            }

            string directory = string.Empty;
            if (HttpContext.Current.Request.Form.AllKeys.Contains(Param
                .Directory))
            {
                directory = HttpContext.Current.Request.Form.GetValues(Param
                    .Directory).FirstOrDefault();
            }
            directory = Path.Combine(Param.CommonPath, directory);

            string fileName = string.Empty;
            if (HttpContext.Current.Request.Form.AllKeys.Contains(Param
                .FileName))
            {
                fileName = HttpContext.Current.Request.Form.GetValues(Param
                    .FileName).FirstOrDefault();
            }
            if (string.IsNullOrWhiteSpace(fileName))
            {
                fileName = DateTime.Now.ToString(Format.File);
            }

            string startupPath = HttpContext.Current.Server.MapPath("/");
            string filePath = Path.Combine(startupPath, directory);
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            string savePath = Path.Combine(directory, fileName);
            result.Status = true;
            if (file.ContentLength > 0)
            {
                string saveFile = savePath + Path.GetExtension(
                    file.FileName);

                file.SaveAs(Path.Combine(startupPath, saveFile));
                result.FilePath = fileName + Path.GetExtension(
                    file.FileName);
            }

            return Ok(result);
        }
    }
}
