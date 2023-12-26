using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Jinxi.Entity
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("user_info")]
    public partial class UserInfo
    {
        public UserInfo()
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
        /// Desc:登录账号
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnName = "user")]
        public string User { get; set; }

        /// <summary>
        /// Desc:登录密码
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnName = "password")]
        public string Password { get; set; }

        /// <summary>
        /// Desc:是否有权限登录
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnName = "isau")]
        public byte? Isau { get; set; }

        /// <summary>
        /// Desc:注册日期
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnName = "regdate")]
        public DateTime? Regdate { get; set; }

        /// <summary>
        /// Desc:用户id
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnName = "userid")]
        public string Userid { get; set; }

    }
}
