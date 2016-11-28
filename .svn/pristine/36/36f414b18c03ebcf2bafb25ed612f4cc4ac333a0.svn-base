using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MltxManager.Models.DBModel
{
    /// <summary>
    /// 后台登陆人员管理表
    /// </summary>
    public class Mltx_UserInfo
    {
        [Key]
        public int UserId { get; set; }
        /// <summary>
        /// 管理人员用户名
        /// </summary>
        [MaxLength(100)]
        public string UserName { get; set; }
        /// <summary>
        /// 管理人员登陆密码
        /// </summary>
        [MaxLength(100)]
        public string Password { get; set; }
        /// <summary>
        /// 管理人员所属分组
        /// </summary>
        [ForeignKey("GroupId")]
        public Mltx_UserGroup Group { get; set; }

        public int GroupId { get; set; }
        /// <summary>
        /// 是否注销
        /// </summary>
        public Status Enable { get; set; }
        public string Remark { get; set; }
    }
    public enum Status : byte
    {
        可用 = 0,
        已注销 = 1,
    }
}
