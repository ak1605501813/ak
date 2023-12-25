using Jinxi.Entity;
using Jinxi.IService;
using Jinxi.Tool;
using Microsoft.AspNetCore.Mvc;

namespace Jinxi.Service
{
    public class EasyToForgetAccountService : IEasyToForgetAccountService
    {
        private SqlsugarTool sqlsugarTool;
        public EasyToForgetAccountService(SqlsugarTool sqlsugarTool)
        {
            this.sqlsugarTool = sqlsugarTool;
        }
        public IActionResult ADD(AccountDetails input)
        {

            throw new System.NotImplementedException();
        }
    }
}
