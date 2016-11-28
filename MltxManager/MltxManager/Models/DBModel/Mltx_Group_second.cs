using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace MltxManager.Models.DBModel
{
    /// <summary>
    /// 商品分组表(二级分组)
    /// </summary>
    public class Mltx_Group_second
    {
        [Key]
        public int GroupId { get; set; }
        /// <summary>
        /// 分组名称
        /// </summary>
        [MaxLength(30)]
        public string GroupName { get; set; }
        /// <summary>
        /// 所属分组 （管理一级分组GroupId）
        /// </summary>
        [ForeignKey("OwnerId")]
        public Mltx_Group_first Owner { get;set;  }

        public int OwnerId { get; set; }
        /// <summary>
        /// 二级分组图片
        /// </summary>
        public string GroupPic { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public Enable Enable { get; set; }
        public string Remark { get; set; }
    }
}
