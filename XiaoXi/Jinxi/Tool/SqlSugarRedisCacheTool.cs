using Microsoft.Extensions.Configuration;
using SqlSugar;
using SugarRedis;
using System;
using System.Collections.Generic;

namespace Jinxi.Tool
{
    public class SqlSugarRedisCacheTool : ICacheService
    {
        //NUGET安装 SugarRedis  （也可以自个实现）   
        //注意:SugarRedis 不要扔到构造函数里面， 一定要单例模式  
        //public static SugarRedisClient _service = new SugarRedisClient();

        public static SugarRedisClient _service = new SugarRedisClient("114.115.141.102:6379,password=123456,connectTimeout=3000,connectRetry=1,syncTimeout=10000,DefaultDatabase=0");
        //+1重载 new SugarRedisClient(字符串)
        //默认:127.0.0.1:6379,password=,connectTimeout=3000,connectRetry=1,syncTimeout=10000,DefaultDatabase=0

        public void Add<V>(string key, V value)
        {
            _service.Set(key, value);
        }

        public void Add<V>(string key, V value, int cacheDurationInSeconds)
        {
            //SugarRedis单位是分钟
            //其他Redis可以不需要转换
            cacheDurationInSeconds = Convert.ToInt32(cacheDurationInSeconds / 60);
            _service.Set(key, value, cacheDurationInSeconds);
        }

        public bool ContainsKey<V>(string key)
        {
            return _service.Exists(key);
        }

        public V Get<V>(string key)
        {
            return _service.Get<V>(key);
        }

        public IEnumerable<string> GetAllKey<V>()
        {

            return _service.SearchCacheRegex("SqlSugarDataCache.*");
        }

        public V GetOrCreate<V>(string cacheKey, Func<V> create, int cacheDurationInSeconds = int.MaxValue)
        {
            //SugarRedis单位是分钟
            //其他Redis可以不需要转换
            cacheDurationInSeconds = Convert.ToInt32(cacheDurationInSeconds / 60);
            if (this.ContainsKey<V>(cacheKey))
            {
                var result = this.Get<V>(cacheKey);
                if (result == null)
                {
                    return create();
                }
                else
                {
                    return result;
                }
            }
            else
            {
                var result = create();
                this.Add(cacheKey, result, cacheDurationInSeconds);
                return result;
            }
        }
        public void Remove<V>(string key)
        {
            _service.Remove(key);
        }
    }
}
