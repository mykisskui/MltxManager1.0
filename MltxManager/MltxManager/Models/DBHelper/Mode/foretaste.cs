﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MltxManager.Models.DBModel;

namespace MltxManager.Models.DBHelper.Mode
{
    /// <summary>
    /// 我的试吃列表
    /// </summary>
    public class Foretaste
    {
        public int  Id  { get; set; }
        /// <summary>
        /// 商品编号
        /// </summary>
        public int GoodsId { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 试吃结束时间
        /// </summary>
        public DateTime ETime { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public States States { get; set; }
        /// <summary>
        /// 试吃期数
        /// </summary>
        public int Sort { get; set; }
    }
}