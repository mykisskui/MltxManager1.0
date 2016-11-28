using MltxManager.Data;
using MltxManager.Models.DBHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MltxManager.WebService.SmsInterface
{
    /// <summary>
    /// RegSmsCode 的摘要说明
    /// </summary>
    /// http://sms.tfresh.cn/SmsInterface/RegSmsCode?uphone=15151532535&ucode=xxx
    public class RegSmsCode : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string msg = null;
            string User_Phone = context.Request.Params["uphone"];
            string User_Code = context.Request.Params["ucode"];
            ReturnMsg rmsg = new ReturnMsg();
            SmsData sd = new SmsData();
            if (string.IsNullOrEmpty(User_Phone) || string.IsNullOrEmpty(User_Code))
            {
                msg = "{\"errcode\":\"-1\",\"errmsg\":\"参数缺失\",\"datajson\":\"\"}";
            }
            else
            {
                rmsg = sd.GetMemberInfo(User_Phone);
                if (rmsg.errcode == 0)//有数据
                {
                    if (rmsg.MemberInfo.UserCode != User_Code)
                    {
                        msg = "{\"errcode\":\"-1\",\"errmsg\":\"验证码不对\",\"datajson\":\"\"}";
                    }
                    else
                    {
                        string emsg = sd.AddOrUpdateMemberInfo(User_Phone, User_Code, null);
                        if (emsg == "error")
                        {
                            msg = "{\"errcode\":\"-1\",\"errmsg\":\"验证失败，请重新获取验证码\",\"datajson\":\"\"}";
                        }
                        else
                        {
                            msg = "{\"errcode\":\"0\",\"errmsg\":\"验证成功\",\"datajson\":\"\"}";
                        }
                    }
                }
                else if (rmsg.errcode == 1)//没有信息 请先获取验证码
                {
                    msg = "{\"errcode\":\"-1\",\"errmsg\":\"先进行验证码申请，才能完成注册\",\"datajson\":\"\"}";
                }
                else
                {
                    msg = "{\"errcode\":\"-1\",\"errmsg\": 验证异常" + rmsg.errmsg + ",\"datajson\":\"\"}";
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