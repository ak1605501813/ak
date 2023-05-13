using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Jinxi.Entity
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("cm_department")]
    public partial class CmDepartment
    {
           public CmDepartment(){


           }
           /// <summary>
           /// Desc:主键
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,ColumnName="department_code")]
           public string DepartmentCode {get;set;}

           /// <summary>
           /// Desc:公司码
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(ColumnName="companyid")]
           public string Companyid {get;set;}

           /// <summary>
           /// Desc:名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnName="department_name")]
           public string DepartmentName {get;set;}

           /// <summary>
           /// Desc:上级部门
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnName="superior_dep")]
           public string SuperiorDep {get;set;}

           /// <summary>
           /// Desc:是否第三方公司
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnName="isThirdParty")]
           public byte? IsThirdParty {get;set;}

           /// <summary>
           /// Desc:分管领导---userid
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnName="leader")]
           public string Leader {get;set;}

           /// <summary>
           /// Desc:用于排序
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnName="sort_number")]
           public int? SortNumber {get;set;}

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
           /// Desc:部门英文名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnName="department_name_en")]
           public string DepartmentNameEn {get;set;}

           /// <summary>
           /// Desc:数据来源1:人事系统
           /// Default:0
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnName="source")]
           public int? Source {get;set;}

           /// <summary>
           /// Desc:有效日期
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnName="validdate")]
           public DateTime? Validdate {get;set;}

           /// <summary>
           /// Desc:失效标识
           /// Default:0
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnName="invalidflag")]
           public short? Invalidflag {get;set;}

           /// <summary>
           /// Desc:所有的上级id,使用逗号分隔
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnName="pids")]
           public string Pids {get;set;}

           /// <summary>
           /// Desc:级数
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnName="level")]
           public int? Level {get;set;}

           /// <summary>
           /// Desc:是否跟公司 1:是、0：否，部门
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnName="isbranch")]
           public byte? Isbranch {get;set;}

           /// <summary>
           /// Desc:负责人
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnName="headid")]
           public string Headid {get;set;}

    }
}
