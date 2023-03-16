using HelpClassLibrary.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelpClassLibrary.Tool
{
    public class SqlTool
    {
        public static string MysqlStrFormmate(string sql)
        {
            sql = sql.Replace("'", "\\'" + "");
            sql = sql.Replace("\"", "\\" + "\"");
            return sql;
        }

        public static string ParseOrderBy(List<OrderByConditionDto> orderBys)
        {
            var conds = "";
            foreach (var con in orderBys)
            {
                switch (con.Order)
                {
                    case OrderSequenceDto.Asc:
                        conds += $"{con.Sort} asc,";
                        break;
                    case OrderSequenceDto.Desc:
                        conds += $"{con.Sort} desc,";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return conds.TrimEnd(',');
        }
        public static Boolean IsEmpty(Object obj)
        {
            return IsEmpty(obj, true);
        }
        public static Boolean IsEmpty(Object obj, bool isTrim)
        {
            if (obj == DBNull.Value || null == obj)
                return true;
            if (obj is string)
            {
                var str = Convert.ToString(obj);
                if (isTrim)
                    str = str.Trim();
                return str.Length == 0;
            }
            else if (obj is Array array)
            {
                return array.Length == 0;
            }
            return false;
        }
        /// <summary>
        /// 查询参数数据变成SQL语句字符串
        /// </summary>
        /// <param name="Conditions"></param>
        /// <returns></returns>
        public static string MysqlStr(List<QueryConditionDto> Conditions)
        {
            Conditions = Conditions.Where(p => !string.IsNullOrEmpty(p.Value.ToString())).ToList();
            var retSql = "";
            if (Conditions is null)
            {
                return "";
            }
            var s = 0;
            foreach (var item in Conditions)
            {
                if (item.Character == QueryCharacterDto.And && IsEmpty(item.Value))
                {
                    retSql += "  and ";
                }
                else
                {
                    if (s == 0)
                    {
                        retSql += "  and ";
                    }
                    else
                    {
                        retSql += "  or ";
                    }
                }
                s++;
                switch (item.Operator)
                {
                    case QueryOperatorDto.Equal:
                        if (!IsEmpty(item.Value))
                        {
                            retSql += $" {item.Key}='{MysqlStrFormmate(item.Value.ToString())}'  ";
                        }
                        else
                        {
                            retSql += "(" + retSql + "1=1)";
                        }
                        break;
                    case QueryOperatorDto.Like:
                        if (!IsEmpty(item.Value))
                        {
                            retSql += "(";
                            retSql += $"  {item.Key} like  '%{MysqlStrFormmate(item.Value.ToString())}%'  ";
                            retSql += ")";

                        }
                        else
                        {
                            retSql += "(" + retSql + "1=1)";
                        }
                        break;
                    case QueryOperatorDto.GreaterThan:
                        if (!IsEmpty(item.Value))
                        {
                            retSql += $" {item.Key}>'{MysqlStrFormmate(item.Value.ToString())}'  ";

                        }
                        else
                        {
                            retSql += "(" + retSql + "1=1)";
                        }
                        break;
                    case QueryOperatorDto.GreaterThanOrEqual:
                        if (!IsEmpty(item.Value))
                        {
                            retSql += $" {item.Key}>='{MysqlStrFormmate(item.Value.ToString())}'  ";
                        }
                        else
                        {
                            retSql += "(" + retSql + "1=1)";
                        }
                        break;
                    case QueryOperatorDto.LessThan:
                        if (!IsEmpty(item.Value))
                        {
                            retSql += $" {item.Key}<'{MysqlStrFormmate(item.Value.ToString())}'  ";
                        }
                        else
                        {
                            retSql += "(" + retSql + "1=1)";
                        }
                        break;
                    case QueryOperatorDto.LessThanOrEqual:
                        if (!IsEmpty(item.Value))
                        {
                            retSql += $" {item.Key}<='{MysqlStrFormmate(item.Value.ToString())}'  ";
                        }
                        else
                        {
                            retSql += "(" + retSql + "1=1)";
                        }
                        break;
                    case QueryOperatorDto.In:
                        if (!IsEmpty(item.Value))
                        {
                            retSql += "(";

                            retSql += $" {item.Key} in ('{ string.Join("','", MysqlStrFormmate(item.Value.ToString()).Split(","))}')  ";
                            retSql += ")";

                        }
                        else
                        {
                            retSql += "(" + retSql + "1=1)";
                        }
                        break;
                    case QueryOperatorDto.NotIn:
                        if (!IsEmpty(item.Value))
                        {
                            retSql += "(";

                            retSql += $" {item.Key} not in ('{ string.Join("','", MysqlStrFormmate(item.Value.ToString()).Split(","))}')  ";
                            retSql += ")";

                        }
                        else
                        {
                            retSql += "(" + retSql + "1=1)";
                        }
                        break;
                    case QueryOperatorDto.LikeLeft:
                        if (!IsEmpty(item.Value))
                        {
                            retSql += "(";
                            retSql += $"  {item.Key} like  '%{MysqlStrFormmate(item.Value.ToString())}'  ";
                            retSql += ")";
                        }
                        else
                        {
                            retSql += "(" + retSql + "1=1)";
                        }
                        break;
                    case QueryOperatorDto.LikeRight:
                        if (!IsEmpty(item.Value))
                        {
                            retSql += "(";
                            retSql += $"  {item.Key} like  '{MysqlStrFormmate(item.Value.ToString())}%'  ";
                            retSql += ")";
                        }
                        else
                        {
                            retSql += "(" + retSql + "1=1)";
                        }
                        break;
                    case QueryOperatorDto.NoEqual:
                        if (!IsEmpty(item.Value))
                        {
                            retSql += "(";

                            retSql += $" {item.Key}!='{MysqlStrFormmate(item.Value.ToString())}'  ";
                            retSql += ")";

                        }
                        else
                        {
                            retSql += "(" + retSql + "1=1)";
                        }
                        break;
                    case QueryOperatorDto.IsNullOrEmpty:
                        if (!IsEmpty(item.Value))
                        {
                            retSql += "(";

                            retSql += $" ({item.Key} is null or {item.Key}='') ";
                            retSql += ")";

                        }
                        else
                        {
                            retSql += "(" + retSql + "1=1)";
                        }
                        break;
                    case QueryOperatorDto.IsNot:
                        if (!IsEmpty(item.Value))
                        {
                            retSql += "(";

                            retSql += $" ({item.Key} is not null  and {item.Key}!='') ";
                            retSql += ")";

                        }
                        else
                        {
                            retSql += "(" + retSql + "1=1)";
                        }
                        break;
                    case QueryOperatorDto.DateRange:
                        var strDate = item.Key.Split("|");
                        if (!IsEmpty(item.Value))
                        {
                            retSql += "(";

                            retSql += $" {item.Key} BETWEEN   {strDate[0]} and  {strDate[1]}   ";
                            retSql += ")";

                        }
                        else
                        {
                            retSql += "(" + retSql + "1=1)";
                        }
                        break;
                    case QueryOperatorDto.NoLike:

                        if (!IsEmpty(item.Value))
                        {
                            retSql += "(";

                            retSql += $" {item.Key}  not like '%{MysqlStrFormmate(item.Value.ToString())}%' ";
                            retSql += ")";

                        }
                        else
                        {
                            retSql += "(" + retSql + "1=1)";
                        }
                        break;
                    default:
                        break;
                }
            }
            return retSql;
        }
    }
}
