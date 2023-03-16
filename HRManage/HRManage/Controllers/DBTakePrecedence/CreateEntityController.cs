using HRManage.Tool;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace HRManage.Controllers.DBTakePrecedence
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreateEntityController : ControllerBase
    {
        SqlsugarTool sqlsugarTool;
        public CreateEntityController(SqlsugarTool sqlsugarTool) 
        {
            this.sqlsugarTool=sqlsugarTool;
        }
        /// <summary>
        /// 创建实体
        /// </summary>
        /// <param name="tblName">数据库表名</param>
        /// <param name="savePath">存放路径</param>
        /// <returns>返回Http状态码</returns>
        [HttpGet]
        [Route("CreateEntity")]
        public IActionResult CreateEntity(string tblName, string savePath)
        {
            string nameSpace = "HRManage.Entity";
            var db = sqlsugarTool.GetDb();
            foreach (var item in db.DbMaintenance.GetTableInfoList())
            {
                string entityName = StrTool.ToCamelName(item.Name);
                db.MappingTables.Add(entityName, item.Name);
                foreach (var col in db.DbMaintenance.GetColumnInfosByTableName(item.Name))
                {
                    db.MappingColumns.Add(StrTool.ToCamelName(col.DbColumnName), col.DbColumnName, entityName);
                }
            }
            db.DbFirst.IsCreateAttribute().Where(it => tblName.Split(",").Contains(it)).CreateClassFile(savePath, nameSpace);
            return Ok();
        }
    }
}
