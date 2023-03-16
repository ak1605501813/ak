using HelpClassLibrary.Dto;
using HelpClassLibrary.Tool;
using HRManage.Entity;
using HRManage.IService;
using HRManage.Tool;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HRManage.Service
{
    public class StructureStatisticsService : IStructureStatisticsService
    {
        private SqlsugarTool sqlsugarTool;
        public StructureStatisticsService(SqlsugarTool sqlsugarTool)
        {
            this.sqlsugarTool = sqlsugarTool;
        }
        public object TestSqlSugar()
        {
            try
            {
                return sqlsugarTool.GetDb().Queryable<CmDepartment>().WithCache().ToList();
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.ToString());
            }
        }

        /// <summary>
        /// 查询人员信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public object QueryAll(QueryParametersDto model)
        {
            try
            {
                //var user = _httpContextAccessor?.HttpContext?.User;
                #region 条件过滤
                if (model.Conditions != null && model.Conditions.Count > 0)
                {
                    model.Conditions = model.Conditions.Where(i => i.Value != null).ToList();
                }
                #endregion
                int totalCount = 0;
                List<CmDepartment> employeeInfos = null;
                var sql = "1=1 " + SqlTool.MysqlStr(model.Conditions);
                var queryData = sqlsugarTool.GetDb().Queryable<CmDepartment>().Where(sql).WithCache();
                if (model.OrderBys.Count() > 0)
                {
                    var orderBys = SqlTool.ParseOrderBy(model.OrderBys);
                    queryData = queryData.OrderBy(orderBys);
                }
                if (model.PageIndex > 0 && model.PageSize > 0)
                {
                    employeeInfos = queryData.ToPageList(model.PageIndex, model.PageSize, ref totalCount);
                }
                else
                {
                    employeeInfos = queryData.ToList();
                    totalCount = employeeInfos.Count;
                }
                return new { data = employeeInfos, total = totalCount };
            }
            catch (Exception ex)
            {
                throw new Exception($"400|QueryAll Erro {ex}");
            }
        }
    }
}
