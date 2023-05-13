using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Minio;
using Minio.Exceptions;
using System;
using System.IO;
//using System.IO;
using System.Threading.Tasks;

namespace Jinxi.Tool
{
    public class MinioTool : ControllerBase
    {
        private static string bucketName = "xiaoxixi";//默认桶
        private readonly MinioClient _client;

        public MinioTool(MinioClient client)
        {
            _client = client;
        }
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        public async Task<object> UploadFile(IFormFile formFile)
        {
            //long size = files.Sum(f => f.Length);
            long size = formFile.Length;
            if (size == 0) { return "上传失败"; }     
            try
            {
                bool found = await _client.BucketExistsAsync(bucketName);
                //如果桶不存在则创建桶
                if (!found)
                {
                    await _client.MakeBucketAsync(bucketName);
                }
                string saveFileName = $"{Guid.NewGuid():N}{Path.GetExtension(formFile.FileName)}";//存储的文件名
                string objectName = $"/{DateTime.Now:yyyy/MM/dd}/{saveFileName}";//文件保存路径
                if (formFile.Length > 0)
                {
                    Stream stream = formFile.OpenReadStream();
                    await _client.PutObjectAsync(bucketName,
                             objectName,
                             stream,
                             formFile.Length,
                             formFile.ContentType);
                }
                #region 支持批量上传,目前业务不需要
                //foreach (var formFile in files)
                //{
                //    string saveFileName = $"{Guid.NewGuid():N}{Path.GetExtension(formFile.FileName)}";//存储的文件名
                //    string objectName = $"/{DateTime.Now:yyyy/MM/dd}/{saveFileName}";//文件保存路径
                //    if (formFile.Length > 0)
                //    {
                //        Stream stream = formFile.OpenReadStream();
                //        await _client.PutObjectAsync(bucketName,
                //                 objectName,
                //                 stream,
                //                 formFile.Length,
                //                 formFile.ContentType);
                //    }
                //}
                #endregion
                return new { SveFileName = saveFileName, ObjectName = objectName };
            }
            catch (MinioException e)
            {
                throw new MinioException($"文件上传错误: { e.Message}");
            }
        }
        /// <summary>
        /// 下载附件
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public async Task<IActionResult> DownloadFile(string fileName)
        {
            var memoryStream = new MemoryStream();
            try
            {
                await _client.StatObjectAsync(bucketName, fileName);
                await _client.GetObjectAsync(bucketName, fileName,
                                    (stream) =>
                                    {
                                        stream.CopyTo(memoryStream);
                                    });
                memoryStream.Position = 0;
            }
            catch (MinioException e)
            {
                throw new MinioException("下载附件发生错误: " + e);
            }

            return File(memoryStream, "application/octet-stream");
        }
        #region 暂时用不到
        private static string GetContentType(string fileName)
        {
            if (fileName.Contains(".jpg"))
            {
                return "image/jpg";
            }
            else if (fileName.Contains(".jpeg"))
            {
                return "image/jpeg";
            }
            else if (fileName.Contains(".png"))
            {
                return "image/png";
            }
            else if (fileName.Contains(".gif"))
            {
                return "image/gif";
            }
            else if (fileName.Contains(".pdf"))
            {
                return "application/pdf";
            }
            else
            {
                return "application/octet-stream";
            }
        }
        #endregion
    }
}
