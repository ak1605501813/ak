using HelpClassLibrary.Dto;
using HelpClassLibrary.Tool;
using Jinxi.DTO;
using Jinxi.Entity;
using Jinxi.IService;
using Jinxi.Tool;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Jinxi.Service
{
    public class EasyToForgetAccountService : IEasyToForgetAccountService
    {
        private SqlsugarTool _sqlsugarTool;
        PrincipalUser _user;
        public EasyToForgetAccountService(SqlsugarTool sqlsugarTool, IHttpContextAccessor httpContextAccessor)
        {
            _sqlsugarTool = sqlsugarTool;
            _user = httpContextAccessor.CurrentUser();
        }
        public dynamic GetAll(QueryParametersDto input)
        {
            try
            {
                //var user = _httpContextAccessor?.HttpContext?.User;
                 #region 条件过滤
                 if (input.Conditions != null && input.Conditions.Count > 0)
                 {
                    input.Conditions = input.Conditions.Where(i => i.Value != null).ToList();
                 }
                 #endregion
                 int totalCount = 0;
                 List<AccountDetails> employeeInfos = null;
                 var sql = "1=1 " + SqlTool.MysqlStr(input.Conditions);
                 var queryData = _sqlsugarTool.GetDb().Queryable<AccountDetails>().Where(sql);//.WithCache()
                if (input.OrderBys.Count() > 0)
                 {
                     var orderBys = SqlTool.ParseOrderBy(input.OrderBys);
                     queryData = queryData.OrderBy(orderBys);
                 }
                 if (input.PageIndex > 0 && input.PageSize > 0)
                 {
                     employeeInfos = queryData.ToPageList(input.PageIndex, input.PageSize, ref totalCount);
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
        public IActionResult ADD(AccountDetails input)
        {
            try
            {
                input.Createuser = _user.Userid;
                input.Createdatetime = DateTime.Now;
                _sqlsugarTool.GetDb().Insertable<AccountDetails>(input).ExecuteCommand();
                return MstResultTool.Success("添加成功");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        public IActionResult Delete(PublicIdsDTO input)
        {
            try
            {
                _sqlsugarTool.GetDb().Deleteable<AccountDetails>().Where(x=>input.Ids.Contains(x.Id)).ExecuteCommand();
                return MstResultTool.Success("删除成功");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        public IActionResult Update(AccountDetails input)
        {
            try
            {
                input.Modifyuser = _user.Userid;
                input.Modifydatetime = DateTime.Now;
                _sqlsugarTool.GetDb().Updateable<AccountDetails>(input).ExecuteCommand();
                return MstResultTool.Success("修改成功");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
