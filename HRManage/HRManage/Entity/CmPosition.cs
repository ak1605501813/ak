using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace HRManage.Entity
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("cm_position")]
    public partial class CmPosition
    {
           public CmPosition(){


           }
           /// <summary>
           /// Desc:岗位Id
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,ColumnName="position_code")]
           public string PositionCode {get;set;}

           /// <summary>
           /// Desc:公司码
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(ColumnName="companyid")]
           public string Companyid {get;set;}

           /// <summary>
           /// Desc:岗位描述
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnName="position_describe")]
           public string PositionDescribe {get;set;}

           /// <summary>
           /// Desc:岗位名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnName="position_name")]
           public string PositionName {get;set;}

           /// <summary>
           /// Desc:岗位名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnName="position_name_en")]
           public string PositionNameEn {get;set;}

           /// <summary>
           /// Desc:录入人
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnName="createuser")]
           public string Createuser {get;set;}

           /// <summary>
           /// Desc:录入时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnName="createdate")]
           public DateTime? Createdate {get;set;}

           /// <summary>
           /// Desc:修改人
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnName="modifier")]
           public string Modifier {get;set;}

           /// <summary>
           /// Desc:修改时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnName="modifydate")]
           public DateTime? Modifydate {get;set;}

           /// <summary>
           /// Desc:排序号
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnName="sort_number")]
           public int? SortNumber {get;set;}

           /// <summary>
           /// Desc:数据来源1:人事系统
           /// Default:0
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnName="source")]
           public int? Source {get;set;}

           /// <summary>
           /// Desc:子公司id
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnName="subcompanyid")]
           public string Subcompanyid {get;set;}

    }
}
