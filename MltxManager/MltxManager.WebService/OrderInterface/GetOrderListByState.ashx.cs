using MltxManager.Data;
using MltxManager.Models.DBHelper;
using MltxManager.WebService.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MltxManager.WebService.OrderInterface
{
    /// <summary>
    /// GetOrderListByState 的摘要说明 根据状态获取订单列表
    /// </summary>
    public class GetOrderListByState : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string state = context.Request.Params["state"];
            ReturnMsg rmsg = new ReturnMsg();
            JsonHelper jsonhelper = new JsonHelper();
            InterfaceData idata = new InterfaceData();
            string msg = null;

            if (string.IsNullOrEmpty(state))
            {
                msg = "{\"errcode\":\"-1\",\"errmsg\":\"参数缺失\",\"datajson\":\"\"}";
            }
            else
            {
                int s = int.Parse(state);
                rmsg = idata._getOrderListByState("",s);
                if (rmsg.errcode == 0)
                {
                    msg = jsonhelper.DataToJson(rmsg.orderbaseList);
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