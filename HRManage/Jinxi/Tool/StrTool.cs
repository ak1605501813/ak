using System;
using System.Text;

namespace Jinxi.Tool
{
    public class StrTool
    {
        #region 根据数据库表生成实体类相关
        /// <summary>
        /// 根据数据库表生成实体类相关
        /// </summary>
        /// <param name="name">关联名称</param>
        /// <param name="delimiter">分割符</param>
        /// <returns></returns>
        public static string ToCamelName(string name, char delimiter = '_')
        {
            StringBuilder result = new StringBuilder();
            // 快速检查
            if (name == null || string.IsNullOrEmpty(name))
            {
                // 没必要转换
                return "";
            }
            else if (!name.Contains("_"))
            {
                // 不含下划线，仅将首字母大写
                return name.Substring(0, 1).ToUpper() + name.Substring(1);
            }
            // 用下划线将原始字符串分割
            String[] camels = name.Split(delimiter);
            foreach (String camel in camels)
            {
                // 跳过原始字符串中开头、结尾的下换线或双重下划线
                if (string.IsNullOrWhiteSpace(camel))
                {
                    continue;
                }
                result.Append(camel.Substring(0, 1).ToUpper());
                result.Append(camel.Substring(1).ToLower());

            }

            return result.ToString();
        }
        #endregion
    }
}
