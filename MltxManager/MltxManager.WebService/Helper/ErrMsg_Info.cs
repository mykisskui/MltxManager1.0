using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MltxManager.WebService.Helper
{
    /// <summary>
    /// 短信接口返回信息
    /// </summary>
    public class ErrMsg_Info
    {
        public int ID { get; set; }
        public int ErrCode { get; set; }
        public string ErrMsg { get; set; }
    }
}