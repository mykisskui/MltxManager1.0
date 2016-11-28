using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MltxManager.Models.DBModel
{
    /// <summary>
    /// 会员明细表
    /// </summary>
    public class Mltx_MemberLog
    {
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 会员编号
        /// </summary>
        public int UID { get; set; }
        /// <summary>
        /// 微信标识
        /// </summary>
        public string Openid { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Tel { get; set; }
        /// <summary>
        /// 数值
        /// </summary>
        public decimal Value { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime Time { get; set; }
        /// <summary>
        /// 类型 积分,消费
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 消费方式
        /// </summary>
        public string Way { get; set; }
    }
}
