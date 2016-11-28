using MltxManager.Data;
using MltxManager.Models.DBHelper;
using MltxManager.WebService.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MltxManager.WebService.IndexInterface
{
    /// <summary>
    /// GetSlideInfo 的摘要说明
    /// </summary>
    public class GetSlideInfo : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string msg = null;
            string tag = context.Request.Params["tag"];//商城幻灯片和鲜果幻灯片区分标识 为空默认商城
            ReturnMsg rmsg = new ReturnMsg();
            InterfaceData idata = new InterfaceData();
            JsonHelper jsonhelper = new JsonHelper();

            rmsg = idata._getSlideinfo(tag);
            if (rmsg.errcode == 0)
            {
                msg = jsonhelper.DataToJson(rmsg.slidebaseList);
            }
            else
            {
                msg = "{\"errcode\":\"-1\",\"errmsg\":" + rmsg.errmsg + ",\"datajson\":\"\"}";
            }

            context.Response.Write(msg);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}