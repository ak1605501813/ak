using Jinxi.Tool;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Jinxi.Controllers.Authority
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AuthorityAuthenticationController : ControllerBase
    {
        JwtCreateTool _jwtCreateTool;
        public AuthorityAuthenticationController(JwtCreateTool jwtCreateTool) 
        {
            _jwtCreateTool=jwtCreateTool;
        }
        /// <summary>
        /// 添加详情信息
        /// </summary>
        /// <param name="input">筛选条件和排序</param>
        /// <returns>返回客户信息</returns>
        [HttpGet]
        [Route("GetToken")]
        //[AllowAnonymous]
        public IActionResult GetToken()
        {
            return MstResultTool.Success(_jwtCreateTool.CreateToken());
        }
    }
}
