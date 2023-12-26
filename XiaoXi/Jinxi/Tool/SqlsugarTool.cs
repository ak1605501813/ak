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
                IsAutoCloseConnection = true,//自动释放和关闭数据库连接，如果有事务事务结束时关闭，否则每次操作后关闭//是否自动释放数据库(默认false)，设为true我们不需要close或者Using的操作
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
