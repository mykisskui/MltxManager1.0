﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MltxManager.Models.DBModel
{
    /// <summary>
    /// 幻灯片
    /// </summary>
    public class Mltx_Slide
    {
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 路径
        /// </summary>
        public string URL { get; set; }
        /// <summary>
        /// 幻灯片跳转类型
        /// </summary>
        public int SlideCls { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string Img { get; set; }
        /// <summary>
        /// 0 商城幻灯片(默认值) 1 水果幻灯片
        /// </summary>
        public int Location { get; set; }
        /// <summary>
        /// 安卓访问类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 安卓参数
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 幻灯片排序
        /// </summary>
        public int sort { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime Time { get; set; }
    }
}
