﻿using MltxManager.Data;
using MltxManager.Models.DBHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MltxManager.WebService.UserInterface
{
    /// <summary>
    /// LoginApp 的摘要说明 登陆app
    /// </summary>
    /// http://sms.tfresh.cn/UserInterface/LoginApp.ashx?tel=15151151515&pwd=xxx
    public class LoginApp : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string tel = context.Request.Params["tel"];
            string pwd = context.Request.Params["pwd"];
            string msg = null;
            InterfaceData idata = new InterfaceData();
            if (string.IsNullOrEmpty(tel))
            {
                msg = "{\"errcode\":\"-1\",\"errmsg\":\"参数缺失\",\"datajson\":\"\"}";
            }
            else
            {
                switch (idata._loginApp(tel, pwd))
                {
                    case "0": msg = "{\"errcode\":\"0\",\"errmsg\":\"成功\",\"datajson\":\"\"}"; break;
                    case "1": msg = "{\"errcode\":\"1\",\"errmsg\":\"此手机号未注册，请先注册\",\"datajson\":\"\"}"; break;
                    case "2": msg = "{\"errcode\":\"2\",\"errmsg\":\"账户或密码错误，请检查重试\",\"datajson\":\"\"}"; break;
                    case "-1": msg = "{\"errcode\":\"-1\",\"errmsg\":\"登陆出现异常\",\"datajson\":\"\"}"; break;
                    default: msg = "{\"errcode\":\"-1\",\"errmsg\":\"服务器异常\",\"datajson\":\"\"}"; break;
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