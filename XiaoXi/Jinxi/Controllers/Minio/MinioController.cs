using Jinxi.Tool;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Minio;
using Minio.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Jinxi.Controllers.Minio
{
    [Route("api/[controller]")]
    [ApiController]
    public class MinioController : ControllerBase
    {
        MinioTool _minioTool;
        public MinioController(MinioTool minioTool)
        {
            _minioTool= minioTool;
        }
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        [HttpPost("UploadFile")]
        public async Task<dynamic> UploadFile(IFormFile formFile)
        {
            return await _minioTool.UploadFile(formFile);
        }
        /// <summary>
        /// 下载附件
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [HttpGet("DownloadFile")]
        public async Task<IActionResult> DownloadFile(string fileName)
        {
            Console.WriteLine(ConfigTool.GetConfig("MinIO:AccessKey"));
            return await _minioTool.DownloadFile(fileName);
        }
    }
}
