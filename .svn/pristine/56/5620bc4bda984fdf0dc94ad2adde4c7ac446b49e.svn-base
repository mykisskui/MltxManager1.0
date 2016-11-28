using MltxManager.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MltxManager.WebService.UserInterface
{
    /// <summary>
    /// ModifyUserinfo 的摘要说明 完善修改用户资料
    /// </summary>
    /// http://sms.tfresh.cn/UserInterface/ModifyUserinfo.ashx?membername=昵称&tel=18514221&sex=1&birthday=1990-02-16&address=xx
    public class ModifyUserinfo : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string msg = null;
            InterfaceData idata = new InterfaceData();
            string membername = context.Request.Params["membername"];
            string tel = context.Request.Params["tel"];
            int sex = int.Parse(context.Request.Params["sex"]);
            DateTime birthday = DateTime.Parse(context.Request.Params["birthday"].ToString());
            string address = context.Request.Params["address"];
            
            string rmsg = idata._modifyuserinfo(membername,tel,sex,birthday,address);
            if (rmsg == "0")//成功
            {
                msg = "{\"errcode\":\"0\",\"errmsg\":\"成功\",\"datajson\":\"\"}";
            }
            else
            {
                msg = "{\"errcode\":\"-1\",\"errmsg\":\"修改信息异常\",\"datajson\":\"\"}";
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