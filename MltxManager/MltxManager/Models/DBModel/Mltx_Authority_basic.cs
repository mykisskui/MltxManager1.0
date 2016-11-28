using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MltxManager.Models.DBModel
{
    /// <summary>
    /// 基础权限信息表
    /// </summary>
    public class Mltx_Authority_basic
    {
        public int Id { get; set; }
        public Quanxian Quan { get; set; }
        public string Remark { get; set; }
    }

    public enum Quanxian
    {
        查看 = 1,
        修改 = 2,
        删除 = 3,
        增加 = 4
    }
}
