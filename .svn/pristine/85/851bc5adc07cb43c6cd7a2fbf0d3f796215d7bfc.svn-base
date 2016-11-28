using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MltxManager.Models.DBModel
{
    /// <summary>
    /// 商品分组表(一级分组)
    /// </summary>
    public class Mltx_Group_first
    {
        [Key]
        public int GroupId { get; set; }
        /// <summary>
        /// 分组名称
        /// </summary>
        [MaxLength(30)]
        public string GroupName { get; set; }

        public string Remark { get; set; }
        /// <summary>
        /// Enable 类型状态
        /// </summary>
        public Enable Enable { get; set; }
    }
}
