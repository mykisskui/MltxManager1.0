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
    /// 商品信息表（鲜果单）
    /// </summary>
    public class Mltx_GoodsInfo_fresh
    {
       
        [Key]
        public int GoodsId { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string GoodsName { get; set; }
        /// <summary>
        /// 商品规格
        /// </summary>
        public string GoodsGuige { get; set; }
        /// <summary>
        /// 销售价
        /// </summary>
        public decimal Rprice { get; set; }
        /// <summary>
        /// 市场价
        /// </summary>
        public decimal Mprice { get; set; }
        /// <summary>
        /// 商品库存 
        /// </summary>
        public int GoodsStock { get; set; }
        /// <summary>
        /// 总销量
        /// </summary>
        public int GoodsSales { get; set; }
        /// <summary>
        /// 商品图片（多张）
        /// </summary>
        public string GoodsPic { get; set; }
        /// <summary>
        /// 列表图片
        /// </summary>
        public string GoodsListPic { get; set; }
        /// <summary>
        /// 商品所属分组（鲜果单分组）Id
        /// </summary>
        public int GroupId { get; set; }
        [ForeignKey("GroupId")]
        public Mltx_Group_fresh Group { get; set; }
        /// <summary>
        /// 商品详情描述
        /// </summary>
        public string GoodsInfo { get; set; }
        /// <summary>
        /// 库存计算方式 
        /// </summary>
        public StockMethod StockMethod { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CTime { get; set; }
        /// <summary>
        /// 编辑时间
        /// </summary>
        public DateTime ETime { get; set; }
        /// <summary>
        /// 编辑人员id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 积分兑换比率 1表示1倍积分
        /// </summary>
        public int Rate { get; set; }
        /// <summary>
        /// 是否上架
        /// </summary>
        public Added Enable { get; set; }
        /// <summary>
        /// 商品评星 初始为0 表示没有评价过
        /// </summary>
        public double GoodsStar { get; set; }
        public string Remark { get; set; }
    }
}
