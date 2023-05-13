using HelpClassLibrary.Dto;
using Jinxi.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Jinxi.Controllers.VocationalWork
{
    [Route("api/[controller]")]
    [ApiController]
    public class StructureStatisticsController : ControllerBase
    {
        IStructureStatisticsService structureStatistics;
        public StructureStatisticsController(IStructureStatisticsService structureStatistics) 
        {
            this.structureStatistics = structureStatistics;
        }

        [HttpGet]
        [Route("TestSqlSugar")]
        public IActionResult TestSqlSugar() 
        {
            structureStatistics.TestSqlSugar();
            return Ok("Success");
        }
        /// <summary>
        /// 查询人员信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("QueryAll")]
        public IActionResult QueryAll(QueryParametersDto model)
        {
            return Ok(structureStatistics.QueryAll(model));
        }
    }
}
