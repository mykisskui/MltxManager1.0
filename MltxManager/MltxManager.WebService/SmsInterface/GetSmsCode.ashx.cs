using MltxManager.Data;
using MltxManager.Models.DBHelper;
using MltxManager.Models.DBModel;
using MltxManager.WebService.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MltxManager.WebService.SmsInterface
{
    /// <summary>
    /// GetSmsCode 的摘要说明
    /// </summary>
    /// http://sms.tfresh.cn/SmsInterface/GetSmsCode?uphone=15151532535&uname=xxx&openid=xxx
    public class GetSmsCode : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            ErrMsg_Info errmsg = new ErrMsg_Info();
            ReturnMsg rmsg = new ReturnMsg();
            MltxDbContext db = new MltxDbContext();
            SmsData sd = new SmsData();
            string msg = null; string sms_content = null;
            string User_Phone = context.Request.Params["uphone"];
            string User_Name = context.Request.Params["uname"];
            string Openid = context.Request.Params["openid"];
            string User_Code = MltxManager.Data.MethodHepler.MHepler.GetCode();

            WebserviceDemo.Webservice.SDKService sdkService = new global::WebserviceDemo.Webservice.SDKService();
            if (string.IsNullOrEmpty(User_Phone))
            {
                msg = "{\"errcode\":\"-1\",\"errmsg\":\"参数缺失\",\"datajson\":\"\"}";
            }
            else
            {
                rmsg = sd.GetSmsInfo();
                if (rmsg.errcode == 1)//没有注册短信序列号
                {
                     msg = "{\"errcode\":\"-1\",\"errmsg\":\"短信系统未启动\",\"datajson\":\"\"}";
                }
                else if (rmsg.errcode == -1)//查询序列号时异常
                {
                    msg = "{\"errcode\":\"-1\",\"errmsg\":\"短信系统异常，请通知客服支持\",\"datajson\":\"\"}";
                }
                else//正常
                {
                    string emsg = sd.AddOrUpdateMemberInfo(User_Phone, User_Code, Openid);
                    if (emsg == "error")
                    {
                        msg = "{\"errcode\":\"-1\",\"errmsg\":\"获取验证码时异常，请通知客服支持\",\"datajson\":\"\"}";
                    }
                    else
                    {
                        sms_content = rmsg.smsinfo.SmsContent.Replace("{验证码}", User_Code);
                        string[] mobiles = { User_Phone };
                        try
                        {
                            errmsg.ErrCode = sdkService.sendSMS(rmsg.smsinfo.S_No, rmsg.smsinfo.S_Key, "", mobiles, sms_content, "", "GBK", 3, Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmssfff")));

                            if (errmsg.ErrCode == 0)
                            {
                                msg = "{\"errcode\":\"0\",\"errmsg\":\"成功\",\"datajson\":" + User_Code + "}";
                            }
                            else
                            {
                                msg = "{\"errcode\":\"-1\",\"errmsg\":短信系统出现异常1:" + errmsg.ErrCode + ",\"datajson\":\"\"}";
                            }
                        }
                        catch (Exception ex)
                        {
                            msg = "{\"errcode\":\"-1\",\"errmsg\": 短信系统出现异常2，请通知客服支持" + ex.Message.ToString() + ",\"datajson\":\"\"}";
                        }
                    }

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