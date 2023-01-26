using CRMManager.Common;
using Nancy.Json;
using RestSharp;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.Json;

namespace APIConnector
{
    
    public static class Helper
    {
        public static string ToJSON(this object obj)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(obj);
        }

        public static T ToObject<T>(this string jsonString)
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
        public static FileResultInfo OLDUploadFile(UploadFileInfo uploadFileInfo)
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
                    var request = new RestRequest();
                    request.Method = Method.Post;
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
                    var response = client.Execute(request);
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        result.Status = false;
                        result.Message = response.ErrorMessage;
                        return result;
                    }

                    result = response.Content.ToObject<FileResultInfo>();
                }

                #endregion
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                //log error
                throw;
            }

            return result;
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
                    // Ghi dữ liệu và form request
                    var form = new MultipartFormDataContent();
                    var fileContent = new ByteArrayContent(File.ReadAllBytes(
                        uploadFileInfo.File));
                    form.Add(fileContent, Param.File, Path.GetFileName(
                        uploadFileInfo.File));
                    if (!string.IsNullOrWhiteSpace(uploadFileInfo.DirectoryPath))
                    {
                        form.Add(new StringContent(uploadFileInfo.DirectoryPath
                            .Replace("/", "\\")), Param.Directory);
                    }

                    if (!string.IsNullOrWhiteSpace(uploadFileInfo.ImageFrame))
                    {
                        form.Add(new StringContent(uploadFileInfo.ImageFrame
                            .Replace("/", "\\")), Param.ImageFrame);
                    }

                    if (uploadFileInfo.MBytes > 0)
                    {
                        form.Add(new StringContent(uploadFileInfo.MBytes
                            .ToString()), Param.ImageSize);
                    }

                    if (!string.IsNullOrWhiteSpace(uploadFileInfo.SaveAsName))
                    {
                        form.Add(new StringContent(uploadFileInfo.SaveAsName),
                            Param.FileName);
                    }

                    var client = new HttpClient
                    {
                        BaseAddress = new Uri(uploadFileInfo.BaseImageUrl),
                        Timeout = TimeSpan.FromMilliseconds(3600000)
                    };

                    var response = client.PostAsync(uploadFileInfo
                        .UploadFunction, form).Result;
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        result.Status = false;
                        result.Message = response.StatusCode.ToString();
                        return result;
                    }

                    result = JsonSerializer.Deserialize<FileResultInfo>(
                        response.Content.ReadAsStringAsync().Result);
                }

                #endregion
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                //log error
                throw;
            }

            return result;
        }
    }
}
