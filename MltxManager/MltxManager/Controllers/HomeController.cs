﻿using System.Web.Mvc;
using MltxManager.Data;
using MltxManager.Data.MethodHepler;
using MltxManager.Models.DBModel;

namespace MltxManager.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Login()
        {
            return View("Login");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(FormCollection collection)
        {
            var username = collection["username"];
            var password = collection["password"];
            var pas = MHepler.GetMD5(password);
            var user = new GetBiz().Login(int.Parse(username), pas);

            if (user == null)
            {
                ViewBag.OK = "OK";
                ViewBag.Message = "登录账号不正确";
                return View();
            }
            else
            {
                ViewBag.Highest = false;
                if (user.UserId == 20158000)
                {
                    ViewBag.Highest = true;
                }
                else if (user.Enable==Status.已注销)
                {
                    ViewBag.OK = "OK";
                    ViewBag.Message = "帐户已注销,请联系管理员开通.";
                    return View();
                }
                else
                {
                    var fenzhu = user.Group.GroupId;
                   
                    var re = new GetBiz().GetModel(fenzhu);
                    if (re.errcode == 0)
                    {
                        ViewBag.PersonnelGrouping = false;
                        ViewBag.PersonnelManagement = false;
                        ViewBag.PacketInformation = false;
                        ViewBag.CommodityManagement = false;
                        ViewBag.CommodityOrder = false;
                        ViewBag.FreshOrders = false;
                        ViewBag.CommodityReview = false;
                        ViewBag.MemberInformation = false;
                        ViewBag.Slides = false;
                        ViewBag.SMS = false;
                        ViewBag.FreshPacket = false;
                        ViewBag.FreshGoods = false;
                        ViewBag.foretaste = false;
                        ViewBag.foretaster = false;
                        ViewBag.Ticket = false;
                        foreach (var r in re.DoubleBases)
                        {
                            switch (r.Val)
                            {
                                case "人员分组":
                                    ViewBag.PersonnelGrouping = true;
                                    break;
                                case "人员管理":
                                    ViewBag.PersonnelManagement = true;
                                    break;
                                case "分组信息":
                                    ViewBag.PacketInformation = true;
                                    break;
                                case "商品管理":
                                    ViewBag.CommodityManagement = true;
                                    break;
                                case "商品订单":
                                    ViewBag.CommodityOrder = true;
                                    break;
                                case "鲜果订单":
                                    ViewBag.FreshOrders = true;
                                    break;
                                case "商品评论":
                                    ViewBag.CommodityReview = true;
                                    break;
                                case "会员信息":
                                    ViewBag.MemberInformation = true;
                                    break;
                                case "幻灯片":
                                    ViewBag.Slides = true;
                                    break;
                                case "短信设置":
                                    ViewBag.SMS = true;
                                    break;
                                case "鲜果分组信息":
                                    ViewBag.FreshPacket = true;
                                    break;
                                case "鲜果商品管理":
                                    ViewBag.FreshGoods = true;
                                    break;
                                case "试吃商品":
                                    ViewBag.foretaste = true;
                                    break;
                                case "试吃报告":
                                    ViewBag.foretaster = true;
                                    break;
                                case "提货券管理":
                                    ViewBag.Ticket = true;
                                    break;
                            }
                        }
                    }
                    else
                    {
                        return RedirectToAction("ErrorPage", "ToOtherPage", new { errmsg = "程序发生异常，请稍候重试！！" }); 
                    }
                   
                }
                ViewBag.UserName = user.UserName;
                //User 用户ID
                Session["User"] = user.UserId;
                return View("index", user);
            }

        }
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
