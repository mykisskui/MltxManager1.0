using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MltxManager.Data;
using MltxManager.Models.DBHelper;
using MltxManager.WebService.Helper;

namespace MltxManager.WebService.IndexInterface
{
    /// <summary>
    /// GetIndexMoudle 的摘要说明
    /// </summary>
    public class GetIndexMoudle : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string msg = null;
            string tag = context.Request.Params["tag"];
            ReturnMsg rmsg = new ReturnMsg();
            InterfaceData idata = new InterfaceData();
            JsonHelper jsonhelper = new JsonHelper();

            rmsg = idata._getIndexModule(tag);
            if (rmsg.errcode == 0)
            {
                msg = jsonhelper.DataToJson(rmsg.indexmodleBaselist);
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