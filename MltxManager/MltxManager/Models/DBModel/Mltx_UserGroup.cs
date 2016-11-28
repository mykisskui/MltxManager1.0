using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MltxManager.Models.DBModel
{
    /// <summary>
    /// 后台登陆人员分组管理表
    /// </summary>
    public class Mltx_UserGroup
    {
        [Key]
        public int GroupId { get; set; }
        [MaxLength(20)]
        public string GroupName { get; set; }
        public string Remark { get; set; }
    }
}
