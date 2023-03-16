using System;
using System.Collections.Generic;
using System.Text;

namespace HelpClassLibrary.Dto
{
    public class OrderByConditionDto
    {
        /// <summary>
        /// 排序字段
        /// </summary>
        public string Sort { get; set; } 
        /// <summary>
        /// 排序类型
        /// </summary>
        public OrderSequenceDto Order { get; set; }
    }
}
