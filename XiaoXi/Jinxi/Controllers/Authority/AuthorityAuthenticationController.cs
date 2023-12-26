using Jinxi.DTO;
using Jinxi.IService;
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
        IAuthorityAuthenticationService _authorityAuthenticationService;
        public AuthorityAuthenticationController(JwtCreateTool jwtCreateTool, IAuthorityAuthenticationService authorityAuthenticationService) 
        {
            _jwtCreateTool=jwtCreateTool;
            _authorityAuthenticationService=authorityAuthenticationService;
        }
        /// <summary>
        /// 获取token
        /// </summary>
        /// <param name="input">筛选条件和排序</param>
        /// <returns>返回客户信息</returns>
        [HttpGet]
        [Route("GetToken")]
        [AllowAnonymous]
        public IActionResult GetToken()
        {
            return MstResultTool.Success(_jwtCreateTool.CreateToken(new LoginDTO() { User="admin",Userid= "XBD_0001" }));
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="input">筛选条件和排序</param>
        /// <returns>返回客户信息</returns>
        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public IActionResult Login(LoginDTO input)
        {
            return _authorityAuthenticationService.Login(input);
        }
    }
}
