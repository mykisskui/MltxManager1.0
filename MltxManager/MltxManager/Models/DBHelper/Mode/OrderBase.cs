using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MltxManager.Models.DBHelper.Mode
{
    /// <summary>
    /// 我的订单列表类
    /// </summary>
    public class OrderBase
    {
        public string OrderId { get; set; }
        public string GoodsPic { get; set; }
        public string GoodsName { get; set; }
        public string Guige { get; set; }
        public int GoodsCnt { get; set; }
        public decimal OrderTotals { get; set; }
        public int State { get; set; }
        public int SumCount { get; set; }
    }
}