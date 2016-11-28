using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MltxManager.Models.DBModel
{
    /// <summary>
    /// 模块权限表
    /// </summary>
    public class Mltx_Model_Auth
    {
        public int Id { get; set; }
        /// <summary>
        /// 关联模块信息表Id
        /// </summary>
        [ForeignKey("ModelId")]
        public Mltx_Model_basic Model { get; set; }

        public int ModelId { get; set; }
        /// <summary>
        /// 关联权限信息表 Id
        /// </summary>
        [ForeignKey("AuthId")]
        public Mltx_Authority_basic Auth { get; set; }

        public int AuthId { get; set; }
        public string Remark { get; set; }
    }
}
