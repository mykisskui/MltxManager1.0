using MltxManager.Data;
using MltxManager.Models.DBHelper;
using MltxManager.WebService.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MltxManager.WebService.OtherInterface
{
    /// <summary>
    /// CheckTicket 的摘要说明
    /// </summary>
    public class CheckTicket : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string msg = null;
            string ticketid = context.Request.Params["ticketid"];
            ReturnMsg rmsg = new ReturnMsg();
            InterfaceData idata = new InterfaceData();
            JsonHelper jsonhelper = new JsonHelper();

            if (string.IsNullOrEmpty(ticketid))
            {
                msg = "{\"errcode\":\"-1\",\"errmsg\":\"参数缺失\",\"datajson\":\"\"}";
            }
            else
            {
                rmsg = idata._getTicketInfo(ticketid);
                if (rmsg.errcode == 0)
                {
                    msg = jsonhelper.DataToJson(rmsg.ticketbase);
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