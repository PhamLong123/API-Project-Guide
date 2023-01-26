using ImageService.Models;
using RestSharp;
using System;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;

namespace APIConnectorNET
{
    public static class Helper
    {
        public static string ToJSON(this object obj)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(obj);
        }

        public static T ToObj<T>(this string jsonString)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Deserialize<T>(jsonString);
        }

        /// <summary>
        /// Hàm upload file
        /// </summary>
        /// <param name="imageApiUrl">Địa chỉ API upload file</param>
        /// <param name="directoryPath">Thư mục lưu file</param>
        /// <param name="fileName">Tên file upload</param>
        /// <param name="imageFrame">Khung hình ảnh (width x height)</param>
        /// <param name="mBytes">Dung lượng hình ảnh</param>
        /// <param name="saveAsName">Tên file lưu (mặc định hệ thống tự tạo)</param>
        /// <returns></returns>
        public static FileResultInfo UploadFile(UploadFileInfo uploadFileInfo)
        {
            FileResultInfo result = new FileResultInfo();
            try
            {
                #region Upload
                if (uploadFileInfo != null && !string.IsNullOrEmpty(
                    uploadFileInfo.BaseImageUrl) && !string.IsNullOrEmpty(
                        uploadFileInfo.UploadFunction))
                {
                    string imageApiUrl = Path.Combine(uploadFileInfo
                        .BaseImageUrl, uploadFileInfo.UploadFunction);
                    var client = new RestClient(imageApiUrl.Replace("\\", "/"));
                    var request = new RestRequest(Method.POST);
                    request.Timeout = 3600000;
                    request.AddHeader("content-type", "multipart/form-data; application/json;");
                    request.AddFile(Param.File, uploadFileInfo.File);
                    request.AddParameter(Param.Directory, uploadFileInfo
                        .DirectoryPath.Replace
                        ("/", "\\"));
                    request.AddParameter(Param.ImageFrame, uploadFileInfo
                        .ImageFrame);
                    request.AddParameter(Param.ImageSize, uploadFileInfo
                        .MBytes);
                    request.AddParameter(Param.FileName, uploadFileInfo
                        .SaveAsName);
                    IRestResponse response = client.Execute(request);
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        result.Status = false;
                        result.Message = response.ErrorMessage;
                        return result;
                    }

                    result = response.Content.ToObj<FileResultInfo>();
                }

                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
    }
}
