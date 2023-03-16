using System;
using System.Collections.Generic;
using System.Text;

namespace HelpClassLibrary.Dto
{
    public class QueryConditionDto
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 查询操作
        /// </summary>
        public QueryOperatorDto Operator { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public object Value { get; set; }
        /// <summary>
        /// 运算符
        /// </summary>
        public QueryCharacterDto Character { get; set; }
    }
}
