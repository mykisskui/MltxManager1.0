using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MltxManager.Models.DBModel
{
    /// <summary>
    /// 首页模块
    /// </summary>
    public class Mltx_IndexModule
    {
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 模块标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 模块类型
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 存储数据
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 0 商城模块(默认值) 1 水果模块
        /// </summary>
        public int IndexCls { get; set; }
    }

    public enum Type
    {
        单模块=0,
        三模块=1,
        四模块=2
    }
}
