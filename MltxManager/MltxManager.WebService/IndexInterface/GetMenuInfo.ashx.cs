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
    /// GetMenuTopInfo 的摘要说明
    /// </summary>
    public class GetMenuInfo : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string msg = null;
            string tag = context.Request.Params["tag"];//标识 商城or鲜果
            ReturnMsg rmsg = new ReturnMsg();
            InterfaceData idata = new InterfaceData();
            JsonHelper jsonhelper = new JsonHelper();

            rmsg = idata._getMenuInfo(tag);
            if (rmsg.errcode == 0)
            {
                msg = jsonhelper.DataToJson(rmsg.menubaseList);
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