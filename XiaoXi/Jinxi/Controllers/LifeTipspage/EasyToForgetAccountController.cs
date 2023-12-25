using Jinxi.Entity;
using Jinxi.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Jinxi.Controllers.LifeTipspage
{
    [Route("api/[controller]")]
    [ApiController]
    public class EasyToForgetAccountController : ControllerBase
    {
        IEasyToForgetAccountService _easyToForgetAccountService;
        public EasyToForgetAccountController(IEasyToForgetAccountService easyToForgetAccountService) 
        {
            _easyToForgetAccountService= easyToForgetAccountService;
        }
        /// <summary>
        /// 添加详情信息
        /// </summary>
        /// <param name="input">筛选条件和排序</param>
        /// <returns>返回客户信息</returns>
        [HttpPost]
        [Route("ADD")]
        public IActionResult ADD(AccountDetails input)
        {
            return _easyToForgetAccountService.ADD(input);
        }
    }
}
