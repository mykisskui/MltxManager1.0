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
    /// GetOrderDetailsByOid 的摘要说明 根据订单号获取订单详情
    /// </summary>
    /// http://xxx/OrderInterface/GetOrderDetailsByOid.ashx?orderid=12315125
    public class GetOrderDetailsByOid : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string msg = null;
            string orderid = context.Request.Params["orderid"];
            ReturnMsg rmsg = new ReturnMsg();
            InterfaceData idata = new InterfaceData();
            JsonHelper jsonhelper = new JsonHelper();

            rmsg = idata._getOrderdetailsByoid(orderid);

            if (rmsg.errcode == 0)
            {
                msg = jsonhelper.DataToJson(rmsg.mltx_order_sub);
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