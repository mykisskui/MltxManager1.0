using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MltxManager.Models.DBModel
{
    /// <summary>
    /// 菜单(所有菜单)
    /// </summary>
    public class Mltx_Menu
    {
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 菜单路径或内容
        /// </summary>
        public string URL { get; set; }
        /// <summary>
        /// 菜单跳转类型
        /// </summary>
        public int MenuCls { get; set; }
        /// <summary>
        /// 菜单图标
        /// </summary>
        public string Img { get; set; }
        /// <summary>
        /// 0底部浮动菜单1 商城菜单2 水果菜单
        /// </summary>
        public Location Location { get; set; }
        /// <summary>
        /// 安卓访问类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 安卓参数
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime Time { get; set; }
    }

    public enum Location
    {
        底部浮动菜单=0,
        商城菜单=1,
        水果菜单=2
    }
}
