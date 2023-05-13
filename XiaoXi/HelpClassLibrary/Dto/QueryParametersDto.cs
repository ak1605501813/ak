using System;
using System.Collections.Generic;
using System.Text;

namespace HelpClassLibrary.Dto
{
    public class QueryParametersDto
    {
        /// <summary>
        /// 行数
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public List<OrderByConditionDto> OrderBys { get; set; }
        /// <summary>
        /// 条件
        /// </summary>
        public List<QueryConditionDto> Conditions { get; set; }
    }
}
