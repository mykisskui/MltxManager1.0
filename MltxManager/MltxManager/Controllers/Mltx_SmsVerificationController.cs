using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MltxManager.Data;
using MltxManager.Data.MethodHepler;
using MltxManager.Models.DBHelper.Mode;
using MltxManager.Models.DBModel;

namespace MltxManager.Controllers
{
    public class Mltx_SmsVerificationController : Controller
    {
        // GET: /WelcomeReply/
        #region 主页面显示
        /// <summary>
        /// 页面显示
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var re = new GetBiz().GetSmsinfo();
            if (re.errcode == 0)
            {
                ViewBag.ShuMu = true;
                ViewBag.Id = re.smsinfo.Id;
                string num = GetBalance();//获取短信条数
                if (num.Split('|')[0] == "0")
                {
                    ViewBag.balance = num.Split('|')[1];
                }
                else
                {
                    ViewBag.balance = "N/A";
                }

                string price = GetPrice();//获取单价
                if (price.Split('|')[0] == "0")
                {
                    ViewBag.price = price.Split('|')[1];
                }
                else
                {
                    ViewBag.price = "N/A";
                }
            }
            else
            {
                ViewBag.ShuMu = false;
                ViewBag.Id = null;
                ViewBag.price = null;
                ViewBag.balance = null;
            }

            return View();
        }
        #endregion
        #region 注册页面
        /// <summary>
        /// 短信验证 注册页面
        /// </summary>
        /// <returns></returns>
        [PageAuthority(ModeId = Modular.短信设置,QuanId = Quanxian.增加)]
        public ActionResult GetRegNo()
        {
            return View("RegNo");
        }
        /// <summary>
        /// 短信验证 注册
        /// </summary>
        /// <param name="serialNo">短信序列号</param>
        /// <param name="serialpass">密码</param>
        /// <param name="remark">短信内容</param>
        /// <returns></returns>
        [PageAuthority(Leixing = false,ModeId = Modular.短信设置,QuanId = Quanxian.增加)]
        public int InsertSms(string serialNo, string serialpass, string remark)
        {
            var re = new GetBiz().InsertSms(serialNo,serialpass,remark);
            if (re)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }
        /// <summary>
        /// 短信验证删除
        /// </summary>
        /// <param name="id">验证ID</param>
        /// <returns></returns>
        [PageAuthority(Leixing = false,ModeId = Modular.短信设置,QuanId = Quanxian.增加)]
        public int DeleteSms(int id)
        {
            var re = new GetBiz().DeleteSms(id);
            if (re)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }
        #endregion


        #region 短信剩余查询
        public string GetBalance()
        {
            WebserviceDemo.Webservice.SDKService sdkService = new global::WebserviceDemo.Webservice.SDKService();//亿美短信webservice
        
            using (MltxDbContext db = new MltxDbContext())
            {
                try
                {
                    var sms = db.smsinfo.SingleOrDefault();
                    if (sms == null)
                    {
                        return "0|N/A";
                    }

                    string sn = sms.S_No;
                    string key = sms.S_Key;
                    try
                    {
                        double balance = sdkService.getBalance(sn, key);
                        if (balance < 0)
                        {
                            return "1|短信查询失败0！";
                        }
                        else
                        {
                            return "0|" + (balance * 10).ToString() + " 条";
                        }
                    }
                    catch (Exception ex)
                    {
                        Data.MethodHepler.MHepler.writelog_err("SysSettingController:GetBalance--查询短信剩余错误1" + ex.Message);
                        return "1|查询短信失败1！";
                    }
                }
                catch (Exception exs)
                {
                    Data.MethodHepler.MHepler.writelog_err("SysSettingController:GetBalance--查询短信剩余错误2" + exs.Message);
                    return "1|查询短信失败2！";
                }
            }
        } 
        #endregion

        #region 短信单价查询
        public string GetPrice()
        {
            WebserviceDemo.Webservice.SDKService sdkService = new global::WebserviceDemo.Webservice.SDKService();//亿美短信webservice
        
            using (MltxDbContext db = new MltxDbContext())
            {
                try
                {
                    var sms = db.smsinfo.SingleOrDefault();
                    if (sms == null)
                    {
                        return "0|N/A";
                    }

                    string sn = sms.S_No;
                    string key = sms.S_Key;
                    try
                    {
                        double balance = sdkService.getEachFee(sn, key);
                        if (balance < 0)
                        {
                            return "1|单价查询失败0！";
                        }
                        else
                        {
                            return "0|" + balance.ToString() + "元/条";
                        }

                    }
                    catch (Exception ex)
                    {
                        Data.MethodHepler.MHepler.writelog_err("SysSettingController:GetPrice--查询单价错误1" + ex.Message);
                        return "1|查询单价失败1！";
                    }

                }
                catch (Exception exs)
                {
                    Data.MethodHepler.MHepler.writelog_err("SysSettingController:GetPrice--查询单价错误2" + exs.Message);
                    return "1|查询单价失败2！";
                }
            }
        }
        #endregion

        #region 修改短信签名
        public string ChangeSign(int weid, string sign)
        {
            using (MltxDbContext db = new MltxDbContext())
            {
                try
                {
                    Mltx_SmsInfo sms = db.smsinfo.SingleOrDefault();
                    if (sms == null)
                    {
                        return "|";
                    }
                    sms.Remark = sign;

                    db.Entry(sms).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    return "0|修改成功！";
                }
                catch (Exception ex)
                {
                    Data.MethodHepler.MHepler.writelog_err("SysSettingController:ChangeSign--修改短信签名错误" + ex.Message);
                    return "1|修改失败！";
                }
            }
        }
        #endregion

    }
}
