using Microsoft.Extensions.Configuration;

namespace Jinxi.Tool
{
    public class ConfigTool
    {
        static IConfiguration _configuration;
        public static void InitConfig(IConfiguration configuration) 
        {
            _configuration = configuration;
        }
        public static string GetConfig(string key) 
        {
            return _configuration[key];
        }
    }
}
