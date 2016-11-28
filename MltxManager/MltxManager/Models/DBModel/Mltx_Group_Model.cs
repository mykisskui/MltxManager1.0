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
    /// 分组模块信息表
    /// </summary>
    public class Mltx_Group_Model
    {
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 关联分组表 GroupId
        /// </summary>
         public int GroupId { get; set; }
        [ForeignKey("GroupId")]
        public Mltx_UserGroup Group { get; set; }

        
        /// <summary>
        /// 关联模块表 Id
        /// </summary>
        public int ModelId { get; set; }
        [ForeignKey("ModelId")]
        public Mltx_Model_basic Model { get; set; }

        
    }
}
