using HelpClassLibrary.Dto;
using Jinxi.DTO;
using Jinxi.Entity;
using Jinxi.IService;
using Jinxi.Tool;
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
        /// 查询详情信息
        /// </summary>
        /// <param name="input">筛选条件和排序</param>
        /// <returns>返回客户信息</returns>
        [HttpPost]
        [Route("GetAll")]
        public IActionResult GetAll(QueryParametersDto input)
        {
            return MstResultTool.Success(_easyToForgetAccountService.GetAll(input));
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
        /// <summary>
        /// 删除详情信息
        /// </summary>
        /// <param name="input">筛选条件和排序</param>
        /// <returns>返回客户信息</returns>
        [HttpPost]
        [Route("Delete")]
        public IActionResult Delete(PublicIdsDTO input)
        {
            return _easyToForgetAccountService.Delete(input);
        }
        /// <summary>
        /// 修改详情信息
        /// </summary>
        /// <param name="input">筛选条件和排序</param>
        /// <returns>返回客户信息</returns>
        [HttpPost]
        [Route("Update")]
        public IActionResult Update(AccountDetails input)
        {
            return _easyToForgetAccountService.Update(input);
        }
       
    }
}
