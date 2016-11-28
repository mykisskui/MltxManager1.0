using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MltxManager.Models.DBHelper.Mode
{
    /// <summary>
    /// 菜单
    /// </summary>
    public class MenuBase
    {
        public string Name { get; set; }
        /// <summary>
        /// h5 路径
        /// </summary>
        public string URL { get; set; }
        /// <summary>
        /// 跳转类型 
        /// </summary>
        public int MenuCls { get; set; }
        /// <summary>
        /// 图标路径
        /// </summary>
        public string Img { get; set; }
        public int Sort { get; set; }
        /// <summary>
        /// 安卓类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 安卓参数
        /// </summary>
        public string Value { get; set; }
    }
}