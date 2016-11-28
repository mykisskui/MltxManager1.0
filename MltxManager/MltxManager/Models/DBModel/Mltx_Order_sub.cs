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
    /// 订单从表
    /// </summary>
    public class Mltx_Order_sub
    {
        [Key]
        public int Id { get; set; }

        public string OrderId_id { get; set; }
        /// <summary>
        /// 订单id（关联主表OrderId）
        /// </summary>
        [ForeignKey("OrderId_id")]
        public Mltx_Order_main OrderId { get; set; }
        /// <summary>
        /// 商品id
        /// </summary>
        public int GoodsId { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string GoodsName { get; set; }
        /// <summary>
        /// 商品图片
        /// </summary>
        public string GoodsPic { get; set; }
        /// <summary>
        /// 商品价格
        /// </summary>
        public decimal GoodsPrice { get; set; }
        /// <summary>
        /// 商品数量
        /// </summary>
        public int GoodsCnt { get; set; }
        /// <summary>
        /// 是否已评价 
        /// </summary>
        public IsCommens IsCommens { get; set; }
        /// <summary>
        /// 此商品积分
        /// </summary>
        public int Integral { get; set; }
        public string Remark { get; set; }

    }

    public enum IsCommens
    {
        是=0,
        否=1
    }
}
