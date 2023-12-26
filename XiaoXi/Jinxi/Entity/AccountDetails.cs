using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Jinxi.Entity
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("account_details")]
    public partial class AccountDetails
    {
        public AccountDetails()
        {


        }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "id")]
        public int Id { get; set; }

        /// <summary>
        /// Desc:简称
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnName = "referred_to_as")]
        public string ReferredToAs { get; set; }

        /// <summary>
        /// Desc:0:系统；1:共享文件
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnName = "region")]
        public int? Region { get; set; }

        /// <summary>
        /// Desc:链接地址
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnName = "link")]
        public string Link { get; set; }

        /// <summary>
        /// Desc:用户名
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnName = "user")]
        public string User { get; set; }

        /// <summary>
        /// Desc:密码
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnName = "password")]
        public string Password { get; set; }

        /// <summary>
        /// Desc:级别（用于排序）
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnName = "level")]
        public int? Level { get; set; }
        /// <summary>
        /// Desc:创建用户
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnName = "createuser")]
        public string Createuser { get; set; }

        /// <summary>
        /// Desc:创建时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnName = "createdatetime")]
        public DateTime? Createdatetime { get; set; }

        /// <summary>
        /// Desc:修改用户
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnName = "modifyuser")]
        public string Modifyuser { get; set; }

        /// <summary>
        /// Desc:修改时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnName = "modifydatetime")]
        public DateTime? Modifydatetime { get; set; }

    }
}
