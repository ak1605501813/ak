using Microsoft.Extensions.Configuration;
using SqlSugar;
using System;

namespace Jinxi.Tool
{
    public class SqlsugarTool
    {
        private readonly string connStr;
        public SqlsugarTool(string connStr)
        {
            this.connStr = connStr;
        }
        public SqlSugarClient GetDb()
        {
            ICacheService myCache = new SqlSugarRedisCacheTool();//这个类如何建看标题5
            SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = connStr,
                DbType = DbType.MySql,
                IsAutoCloseConnection = true,
                ConfigureExternalServices = new ConfigureExternalServices()
                {
                    DataInfoCacheService = myCache //配置我们创建的缓存类，具体用法看标题5
                }
            });

            //调试SQL事件，可以删掉
            db.Aop.OnLogExecuting = (sql, pars) =>
            {
                Console.WriteLine(sql);
            };
            return db;
        }
    }
}
