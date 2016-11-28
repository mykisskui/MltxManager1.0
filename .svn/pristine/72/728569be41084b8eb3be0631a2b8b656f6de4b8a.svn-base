using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MltxManager.Models.DBModel
{
    /// <summary>
    /// 分组模块权限表
    /// </summary>
    public class Mltx_Group_Auth
    {
        public int  Id { get; set; }
        /// <summary>
        /// 关联分组表 GroupId
        /// </summary>
        [ForeignKey("GroupId")]
        public Mltx_UserGroup Group { get; set; }

        public int GroupId { get; set; }
        /// <summary>
        /// 关联模块权限表，以Id为外键确定当前分组在某个模块的权限
        /// </summary>
        [ForeignKey("MAId")]
        public Mltx_Model_Auth MA { get; set; }

        public int MAId { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public Enable Enable { get; set; }
        public string Remark { get; set; }
    }

    public enum Enable
    {
        未激活=0,
        激活=1
    }
}
