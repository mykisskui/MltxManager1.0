using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Web;

namespace MltxManager.Models.DBModel
{
    public class Mltx_Ticket
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 提货券号 具有唯一性
        /// </summary>
        public string TicketId { get; set; }
        /// <summary>
        /// 提货密码
        /// </summary>
        public string Pwd { get; set; }
        /// <summary>
        /// 卡券状态0 未发放1 未使用2 已验证使用3 已过期 泛型(Card)
        /// </summary>
        public Card State { get; set; }
        /// <summary>
        /// 有效期结束时间
        /// </summary>
        public DateTime Time { get; set; }
        /// <summary>
        /// 商品id 外键 关联商城商品表商品id
        /// </summary>
        public int GoodsId { get; set; }
        /// <summary>
        /// 商城商品表商品id
        /// </summary>
        [ForeignKey("GoodsId")]
        public Mltx_GoodsInfo_shop Goods { get; set; }
    }
    /// <summary>
    /// 卡券状态
    /// </summary>
    public enum Card
    {
        未发放 = 0,
        未使用 = 1,
        已验证使用 = 2,
        已过期 = 3
    }
}