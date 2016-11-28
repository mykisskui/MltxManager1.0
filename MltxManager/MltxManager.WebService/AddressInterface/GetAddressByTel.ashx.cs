using MltxManager.Data;
using MltxManager.Models.DBHelper;
using MltxManager.WebService.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MltxManager.WebService.AddressInterface
{
    /// <summary>
    /// GetAddressByTel 的摘要说明 获取地址库
    /// </summary>
    public class GetAddressByTel : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string tel = context.Request.Params["tel"];
            ReturnMsg rmsg = new ReturnMsg();
            JsonHelper jsonhelper = new JsonHelper();
            InterfaceData idata = new InterfaceData();
            string msg = null;

            if (string.IsNullOrEmpty(tel))
            {
                msg = "{\"errcode\":\"-1\",\"errmsg\":\"参数缺失\",\"datajson\":\"\"}";
            }
            else
            {
                rmsg = idata._getAddressInfoByTel(tel);
                if (rmsg.errcode == 0)
                {
                    msg = jsonhelper.DataToJson(rmsg.addressList);
                }
                else
                {
                    msg = "{\"errcode\":\"-1\",\"errmsg\":" + rmsg.errmsg + ",\"datajson\":\"\"}";
                }
            }

            context.Response.Write(null);
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