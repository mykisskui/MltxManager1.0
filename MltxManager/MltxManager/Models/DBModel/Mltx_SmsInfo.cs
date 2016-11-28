using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MltxManager.Models.DBModel
{
    /// <summary>
    /// 短信设置
    /// </summary>
    public class Mltx_SmsInfo
    {
        /// <summary>
        /// 系统编码
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 短息序列号
        /// </summary>
        public string S_No { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string S_Key { get; set; }
        /// <summary>
        /// 短信内容
        /// </summary>
        public string SmsContent { get; set; }
        public string Remark { get; set; }
    }
}