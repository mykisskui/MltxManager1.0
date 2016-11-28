using MltxManager.Data;
using MltxManager.Models.DBHelper;
using MltxManager.WebService.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MltxManager.WebService.UserInterface
{
    /// <summary>
    /// GetMemberRankByTel 的摘要说明
    /// </summary>
    public class GetMemberRankByTel : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string msg = null;
            string tel = context.Request.Params["tel"];
            ReturnMsg rmsg = new ReturnMsg();
            InterfaceData idata = new InterfaceData();
            JsonHelper jsonhelper = new JsonHelper();

            if (string.IsNullOrEmpty(tel))
            {
                msg = "{\"errcode\":\"-1\",\"errmsg\":\"参数缺失\",\"datajson\":\"\"}";
            }
            else
            {
                rmsg = idata._getMemberRankByTel(tel);
                if (rmsg.errcode == 0)
                {
                    msg = jsonhelper.DataToJson(rmsg.memberrankinfo);
                }
                else
                {
                    msg = "{\"errcode\":\"-1\",\"errmsg\":" + rmsg.errmsg + ",\"datajson\":\"\"}";
                }
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