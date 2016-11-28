using MltxManager.Data;
using MltxManager.Models.DBHelper;
using MltxManager.WebService.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace MltxManager.WebService.GroupInterface
{
    /// <summary>
    /// GetGroupInfo 的摘要说明
    /// </summary>
    public class GetGroupInfo : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string type = context.Request.Params["type"];//查询类型
            string msg = null;
            InterfaceData idata = new InterfaceData();
            ReturnMsg rmsg = new ReturnMsg();
            JsonHelper jsonhelper = new JsonHelper();

            rmsg = idata._getGroupList(type);
            if (rmsg.errcode == 0)
            {
                if (type == "0")
                {
                    msg = jsonhelper.DataToJson(rmsg.shopgroupdata);
                }
                else if (type == "1")
                {
                    msg = jsonhelper.DataToJson(rmsg.MltxGroupFreshes);
                }
                else
                {
                    msg = "{\"errcode\":\"-1\",\"errmsg\":\"type参数缺失\",\"datajson\":\"\"}";
                }
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