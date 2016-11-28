﻿using MltxManager.Data;
using MltxManager.Models.DBHelper;
using MltxManager.Models.DBHelper.Mode;
using MltxManager.Models.DBModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MltxManager.Controllers
{
    public class Mltx_MallClassController : Controller
    {
        /// <summary>
        /// 获取授权路径
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="codes"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public string OAuthGet(string appid, string codes, string url)
        {
            MallConfig config = new MallConfig();
            return config.OAuthURL(appid, codes, url);
        }

        /// <summary>
        /// 请求授权
        /// </summary>
        /// <returns></returns>
        public string OAuthToken(string code, string url, string state)
        {
            Data.MethodHepler.MHepler.writelog_err("进入oauth：--code:"+code+"--url:"+url+"---state:"+state, false);
            MallConfig config = new MallConfig();
            string redirect = string.Empty;
            string result = config.OAuth(code, url, state);
            Data.MethodHepler.MHepler.writelog_err("跳转的支付链接：" + result, false);
            if (result.Split('|')[0] == "0")
            {
                Response.Redirect(result.Split('|')[1]);
                Response.End();
                return string.Empty;
            }
            else
            {
                return result.Split('|')[1];
            }

        }

        //
        // GET: /Mltx_MallClass/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 品类页面
        /// </summary>
        /// <returns></returns>
        public ActionResult MallClass(string userid)
        {
            ViewBag.userid = string.IsNullOrEmpty(userid) ? "youke" : userid;
            try
            {
                ViewBag.menus = new InterfaceData()._getMenu().otherJson;//获取菜单

                StringBuilder sb = new StringBuilder();

                IList<Mltx_Group_first> firsts = null;
                IList<Mltx_Group_second> seconds = null;
                firsts = new GetBiz().GetCommodityGrouping().MltxGroupFirst;

                seconds = new GetBiz().GetCommodityGrouping().MltxGroupSecond;

                sb.Append("{");
                sb.Append("\"data\":[");
                foreach (Mltx_Group_first first in firsts)
                {
                    sb.Append("{");
                    sb.Append(string.Format("\"id\":\"{0}\",", first.GroupId));
                    sb.Append(string.Format("\"name\":\"{0}\",", first.GroupName));
                    sb.Append(string.Format("\"mark\":\"{0}\",", first.Remark));
                    sb.Append("\"data\":[");
                    foreach (Mltx_Group_second second in seconds.Where(f => f.OwnerId == first.GroupId))
                    {
                        sb.Append("{");
                        sb.Append(string.Format("\"id\":\"{0}\",", second.GroupId));
                        sb.Append(string.Format("\"name\":\"{0}\",", second.GroupName));
                        sb.Append(string.Format("\"oid\":\"{0}\",", second.OwnerId));
                        sb.Append(string.Format("\"pic\":\"{0}\",", second.GroupPic));
                        sb.Append(string.Format("\"mark\":\"{0}\"", second.Remark));
                        sb.Append("},");
                    }
                    sb.Append("]},");
                    sb = sb.Replace("},]}", "}]}");
                }

                sb.Append("]}");

                ViewBag.classData = sb.ToString().Replace("]},]}", "]}]}");
            }
            catch (Exception ex)
            {
                //Response.Redirect("");//跳转到错误页
                return RedirectToAction("WebappErrorPage", "ToOtherPage", new { errmsg = "分类数据获取异常！" });
            }
            return View();

        }

        /// <summary>
        /// 列表页
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="groupid">分组id</param>
        /// <returns></returns>
        public ActionResult MallList(string title, string groupid, string tag)
        {
            //ViewBag.msg = Request.Cookies["mltxshopcart"] == null ? "" : Request.Cookies["mltxshopcart"].Value.ToString();

            tag = string.IsNullOrEmpty(tag) ? "1" : tag;
            ViewBag.userid = "mltxshopcart";//购物车key
            ViewBag.groupid = string.IsNullOrEmpty(groupid) ? "alllist" : groupid;
            ViewBag.listtitle = string.IsNullOrEmpty(title) ? "列表" : title;
            List<Mltx_GoodsInfo_shop> goodlist = null;
            try
            {
                StringBuilder sb = new StringBuilder();
                goodlist = new ShopGoodsData().GetShopGoodsDataListBytag(groupid, tag).goodslist;

                sb.Append("[");
                foreach (Mltx_GoodsInfo_shop goods in goodlist)
                {
                    sb.Append("{");
                    sb.Append(string.Format("\"gid\":\"{0}\",", goods.GoodsId));
                    sb.Append(string.Format("\"gname\":\"{0}\",", goods.GoodsName));
                    sb.Append(string.Format("\"guige\":\"{0}\",", goods.GoodsGuige));
                    sb.Append(string.Format("\"rprice\":\"{0}\",", goods.Rprice));
                    sb.Append(string.Format("\"mprice\":\"{0}\",", goods.Mprice));
                    sb.Append(string.Format("\"gpic\":\"{0}\"", goods.GoodsListPic));
                    sb.Append("},");
                }

                sb.Append("]");

                ViewBag.goodsData = sb.ToString().Replace("},]", "}]");
            }
            catch (Exception ex)
            {
                //跳转异常页面
                return RedirectToAction("WebappErrorPage", "ToOtherPage", new { errmsg = "列表数据加载异常！" });
            }

            return View();
        }

        /// <summary>
        /// 排序ajax
        /// </summary>
        /// <param name="title"></param>
        /// <param name="groupid"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        [HttpPost]
        public string Sort(string title, string groupid, string tag)
        {
            tag = string.IsNullOrEmpty(tag) ? "1" : tag;
            List<Mltx_GoodsInfo_shop> goodlist = null;
            try
            {
                StringBuilder sb = new StringBuilder();
                goodlist = new ShopGoodsData().GetShopGoodsDataListBytag(groupid, tag).goodslist;

                sb.Append("[");
                foreach (Mltx_GoodsInfo_shop goods in goodlist)
                {
                    sb.Append("{");
                    sb.Append(string.Format("\"gid\":\"{0}\",", goods.GoodsId));
                    sb.Append(string.Format("\"gname\":\"{0}\",", goods.GoodsName));
                    sb.Append(string.Format("\"guige\":\"{0}\",", goods.GoodsGuige));
                    sb.Append(string.Format("\"rprice\":\"{0}\",", goods.Rprice));
                    sb.Append(string.Format("\"mprice\":\"{0}\",", goods.Mprice));
                    sb.Append(string.Format("\"gpic\":\"{0}\"", goods.GoodsListPic));
                    sb.Append("},");
                }

                sb.Append("]");

                return sb.ToString().Replace("},]", "}]");
            }
            catch (Exception ex)
            {
                return "error";
            }
        }

        /// <summary>
        /// 商品详情页
        /// </summary>
        /// <returns></returns>
        public ActionResult MallDetails(string goodid)
        {
            int gid;


            Mltx_GoodsInfo_shop ginfo = new Mltx_GoodsInfo_shop();
            ViewBag.userid = "mltxshopcart";
            if (string.IsNullOrEmpty(goodid))
            {
                //跳转到异常页 参数缺失
                return RedirectToAction("WebappErrorPage", "ToOtherPage", new { errmsg = "获取数据参数缺失！" });
            }
            else
            {
                gid = int.Parse(goodid);

                ReturnMsg rmsg = new ShopGoodsData().GetShopGoodsInfoByGid(gid);

                if (rmsg.errcode == 0)
                {
                    ginfo = rmsg.MltxGoodsInfoShop;
                }
                else
                {
                    //跳转到异常页面 获取数据时异常
                    return RedirectToAction("WebappErrorPage", "ToOtherPage", new { errmsg = "获取商品数据时异常！" });
                }
            }

            return View(ginfo);
        }

        /// <summary>
        /// 评论页
        /// </summary>
        /// <returns></returns>
        public ActionResult MallComment(string goodid, string skip)
        {
            ViewBag.goodid = goodid;
            int gid, s;
            s = string.IsNullOrEmpty(skip) ? 0 : int.Parse(skip);//为空 默认第一条开始
            if (string.IsNullOrEmpty(goodid))
            {
                //跳转到异常页 参数缺失
                return RedirectToAction("WebappErrorPage", "ToOtherPage", new { errmsg = "获取评论参数缺失！" });
            }
            else
            {
                gid = int.Parse(goodid);
                ReturnMsg rmsg = new ShopGoodsData().GetShopCommentByGid(gid, 20, s);//获取20条记录
                if (rmsg.errcode == 0)
                {
                    StringBuilder sb = new StringBuilder();
                    List<Mltx_Comments> clist = rmsg.com_list;

                    sb.Append("[");
                    foreach (Mltx_Comments item in clist)
                    {
                        sb.Append("{");
                        sb.Append(string.Format("\"c_time\":\"{0}\",", item.CommentTime.ToString("yyyy-MM-dd")));
                        sb.Append(string.Format("\"customer\":\"{0}\",", item.Customer));
                        sb.Append(string.Format("\"comments\":\"{0}\",", item.Comments));
                        sb.Append(string.Format("\"star\":\"{0}\"", item.CommentStar));
                        sb.Append("},");
                    }
                    sb.Append("]");

                    ViewBag.comData = sb.ToString().Replace("},]", "}]");
                }
                else
                {
                    //跳转到异常页面 获取数据时异常
                    return RedirectToAction("WebappErrorPage", "ToOtherPage", new { errmsg = "获取商品评价时异常！" });

                }
            }

            return View();
        }

        /// <summary>
        /// ajax 获取更多评价信息
        /// </summary>
        /// <param name="goodid"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        [HttpPost]
        public string getMoreComments(string goodid, string skip)
        {
            int gid, p, s;
            s = string.IsNullOrEmpty(skip) ? 0 : int.Parse(skip);//为空 默认第一条开始
            if (string.IsNullOrEmpty(goodid))
            {
                //跳转到异常页 参数缺失
                return "error";
            }
            else
            {
                gid = int.Parse(goodid);
                ReturnMsg rmsg = new ShopGoodsData().GetShopCommentByGid(gid, 10, s);//获取20条记录
                if (rmsg.errcode == 0)
                {
                    StringBuilder sb = new StringBuilder();
                    List<Mltx_Comments> clist = rmsg.com_list;

                    if (clist.Count() == 0)
                    {
                        return "nodata";
                    }
                    else
                    {
                        sb.Append("[");
                        foreach (Mltx_Comments item in clist)
                        {
                            sb.Append("{");
                            sb.Append(string.Format("\"c_time\":\"{0}\",", item.CommentTime.ToString("yyyy-MM-dd")));
                            sb.Append(string.Format("\"customer\":\"{0}\",", item.Customer));
                            sb.Append(string.Format("\"comments\":\"{0}\",", item.Comments));
                            sb.Append(string.Format("\"star\":\"{0}\"", item.CommentStar));
                            sb.Append("},");
                        }
                        sb.Append("]");

                        return sb.ToString().Replace("},]", "}]");
                    }
                }
                else
                {
                    return "exception";
                }
            }

        }

        /// <summary>
        /// 商城购物车页面
        /// </summary>
        /// <returns></returns>
        public ActionResult MallShopCar()
        {
            ViewBag.menus = new InterfaceData()._getMenu().otherJson;//获取菜单
            ViewBag.userid = "mltxshopcart";
            string _rmsg = _getShopcarCookie();
            if (_rmsg == "error")
            {
                return RedirectToAction("WebappErrorPage", "ToOtherPage", new { errmsg = "购物车获取数据时异常！" });
            }
            else
            {
                ViewBag.shopcarData = _rmsg;
            }

            return View();
        }

        /// <summary>
        /// 获取购物车cookie
        /// </summary>
        /// <returns></returns>
        public string _getShopcarCookie()
        {
            StringBuilder sb = new StringBuilder();
            JavaScriptSerializer js = new JavaScriptSerializer();
            List<shopCarData> s_data = new List<shopCarData>();
            string _shopcar = Request.Cookies["mltxshopcart"] == null ? "" : Request.Cookies["mltxshopcart"].Value.ToString();

            if (string.IsNullOrEmpty(_shopcar))//没有数据
            {
                sb.Append("{\"data\":[],\"sumPrice\":\"0\"}");
                return sb.ToString();
            }
            else
            {
                _shopcar = Server.UrlDecode(_shopcar);
                s_data = js.Deserialize<List<shopCarData>>(_shopcar);
                decimal sum_price = 0;
                sb.Append("{");
                sb.Append("\"data\":[");

                foreach (shopCarData item in s_data)
                {
                    int gid = item.gid;
                    ReturnMsg rmsg = new ShopGoodsData().GetShopGoodsInfoByGid(gid);
                    if (rmsg.errcode == 0)
                    {
                        if (rmsg.MltxGoodsInfoShop == null)//没有数据
                        {
                            continue;
                        }
                        else
                        {
                            sum_price = sum_price + (rmsg.MltxGoodsInfoShop.Rprice * item.count);
                            sb.Append("{");
                            sb.Append(string.Format("\"gid\":\"{0}\",", item.gid));
                            sb.Append(string.Format("\"gname\":\"{0}\",", rmsg.MltxGoodsInfoShop.GoodsName));
                            sb.Append(string.Format("\"guige\":\"{0}\",", rmsg.MltxGoodsInfoShop.GoodsGuige));
                            sb.Append(string.Format("\"gpic\":\"{0}\",", rmsg.MltxGoodsInfoShop.GoodsListPic));
                            sb.Append(string.Format("\"rprice\":\"{0}\",", rmsg.MltxGoodsInfoShop.Rprice));
                            sb.Append(string.Format("\"count\":\"{0}\"", item.count));//end 最后一条，添加新记录在之前添加
                            sb.Append("},");
                        }
                    }
                    else
                    {
                        return "error";
                    }
                }

                sb.Append("],");
                sb.Append(string.Format("\"sumPrice\":\"{0}\"", sum_price));
                sb.Append("}");

                return sb.ToString().Replace("},]", "}]");
            }

        }//end _getShopcarCookie

        /// <summary>
        /// 订单确定页
        /// </summary>
        /// <param name="login_id"></param>
        /// <returns></returns>
        public ActionResult DoOrder(string login_id)
        {
            if (string.IsNullOrEmpty(login_id))
            {
                return RedirectToAction("WebappErrorPage", "ToOtherPage", new { errmsg = "参数缺失！" });
            }

            try
            {
                ViewBag.PayPrice = ConfigurationManager.AppSettings["PayPrice"];//不满78邮费或者货到付款预付款
            }
            catch (Exception)
            {
                throw;
            }

            ViewBag.login_id = login_id; 
            Mltx_Address addr = new Mltx_Address();
            ReturnMsg rmsg = new ShopGoodsData().GetUserAddressByTel(login_id);
            if (rmsg.errcode == 0)
            {
                if (rmsg.address == null)
                {
                    ViewBag.addr = "no";
                }
                else
                {
                      addr = rmsg.address;
                }

                string _otherdata = _getShopcarCookie();
                if (_otherdata.Contains("[]"))
                {
                    return RedirectToAction("WebappErrorPage", "ToOtherPage", new { errmsg = "购物车数据为空，请重新添加！" });
                }
                else if (_otherdata == "error")
                {
                    return RedirectToAction("WebappErrorPage", "ToOtherPage", new { errmsg = "获取订单数据时异常！" });
                }
                else
                {
                    ViewBag.otherdata = _otherdata;
                }
            }
            else {
                return RedirectToAction("WebappErrorPage", "ToOtherPage", new { errmsg = "获取数据异常！" });
            }

            return View(addr);
        }

        /// <summary>
        /// 获取地址库列表
        /// </summary>
        /// <param name="login_id"></param>
        /// <returns></returns>
        [HttpPost]
        public string MallAddressList(string login_id)
        {
            if (string.IsNullOrEmpty(login_id))
            {
                return "-1";//参数缺失
            }
            ReturnMsg rmsg = new ShopGoodsData().GetAddrListByTel(login_id);
            if (rmsg.errcode == 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("[");
                foreach (Mltx_Address item in rmsg.addressList)
                {
                    sb.Append("{");
                    sb.Append(string.Format("\"id\":\"{0}\",", item.Id));
                    sb.Append(string.Format("\"username\":\"{0}\",", item.UserName));
                    sb.Append(string.Format("\"tel\":\"{0}\",", item.Tel));
                    sb.Append(string.Format("\"openid\":\"{0}\",", item.Openid));
                    sb.Append(string.Format("\"prov\":\"{0}\",", item.Province));
                    sb.Append(string.Format("\"city\":\"{0}\",", item.City));
                    sb.Append(string.Format("\"district\":\"{0}\",", item.District));
                    sb.Append(string.Format("\"area\":\"{0}\",", item.Area));
                    sb.Append(string.Format("\"isdefault\":\"{0}\",", item.IsDefault));
                    sb.Append(string.Format("\"addcls\":\"{0}\"", item.AddCls));
                    sb.Append("},");
                }
                sb.Append("]");

                return sb.ToString().Replace("},]", "}]");
            }
            else
            {
                return "-2";//异常
            }
            
        }

        /// <summary>
        /// 删除地址库 id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public string deleteAddr(int id)
        {
            ReturnMsg rmsg = new ShopGoodsData().DeleteAddrById(id);
            if (rmsg.errcode == 0)
            {
                return "0";
            }
            else {
                return "-1";
            }
        }

        /// <summary>
        /// 编辑或创建地址库
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string createOrediteAddr(string id,string login_id ,string username, string prov, string city, string dist, string area,string tel, string isdefault, string tag)
        {
             ReturnMsg rmsg = new ShopGoodsData().CreateOrEditAddr(id,login_id,username,prov,city,dist,area,tel,isdefault,tag);
             return rmsg.errcode.ToString();
        }


        /// <summary>
        /// 我的果园页
        /// </summary>
        /// <returns></returns>
        public ActionResult MallMyHome()
        {
            ViewBag.menus = new InterfaceData()._getMenu().otherJson;//获取菜单
            MemberBase minfo = new MemberBase();
            string login_id = Request.Cookies["mltxuserid"] == null ? "" : Request.Cookies["mltxuserid"].Value.ToString();
            if (string.IsNullOrEmpty(login_id))
            {
                ViewBag.loginid = "";
                minfo.Balance = 0;
                minfo.Integral = 0;
            }
            else
            {
                ReturnMsg rmsg = new InterfaceData()._getUserinfoBytel(login_id);
                if (rmsg.userbaseinfo == null)
                {
                    return RedirectToAction("WebappErrorPage", "ToOtherPage", new { errmsg = "会员数据异常，请联系商家！" });
                }
                else
                {
                    ViewBag.loginid = login_id;
                    minfo = rmsg.userbaseinfo;
                }
            }

            return View(minfo);
        }

        /// <summary>
        /// 登陆页
        /// </summary>
        /// <returns></returns>
        public ActionResult MallLogin()
        {
            return View();
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="tel"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public string _Login(string tel, string pwd) 
        {
            return new InterfaceData()._loginApp(tel, pwd);
        }

        /// <summary>
        /// 注册页
        /// </summary>
        /// <returns></returns>
        public ActionResult MallReg()
        {
            return View();
        }

        /// <summary>
        /// 获取验证码code
        /// </summary>
        /// <param name="tel"></param>
        /// <returns></returns>
        public string _getCode(string tel)
        {
            string codeurl = ConfigurationManager.AppSettings["Smsweburl"];
            codeurl = codeurl + "GetSmsCode.ashx?uphone=" + tel;

            Data.MethodHepler.MHepler.writelog_err("获取验证码codeurl:" + codeurl, false);
            return _smsGetresult(codeurl);//请求获取验证码接口
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="tel"></param>
        /// <param name="pwd"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public string _Reg(string tel, string pwd, string code)
        {
            string regurl = ConfigurationManager.AppSettings["Smsweburl"];
            regurl = regurl + "RegSmsCode.ashx?uphone=" + tel + "&ucode=" + code + "&pwd=" + pwd;
            Data.MethodHepler.MHepler.writelog_err("注册请求regurl:" + regurl, false);
            return _smsGetresult(regurl);//请求注册接口
        }

        /// <summary>
        /// 短信请求方法
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string _smsGetresult(string url)
        {            
            try
            {
                string result = PostAndGet.GetResponseString(url);
                JavaScriptSerializer js = new JavaScriptSerializer();

                Data.MethodHepler.MHepler.writelog_err("返回json:" + result, false);
                WebReturnMsg jsonmsg = js.Deserialize<WebReturnMsg>(result);
                if (jsonmsg.errcode == "0")//成功
                {
                    return "0";
                }
                else
                {
                    return jsonmsg.errmsg;
                }
            }
            catch (Exception ex)
            {
                return "系统异常！";
            }
        }

        /// <summary>
        /// 我的订单页   
        /// </summary>
        /// <returns></returns>
        public ActionResult MallMyOrder(string uid)
        {
             ReturnMsg rmsg = new InterfaceData()._getOrderListByState(uid, 1);
             if (rmsg.errcode == 0)
             {
                 try
                 {
                     ViewBag.PayPrice = ConfigurationManager.AppSettings["PayPrice"];//不满78邮费或者货到付款预付款
                 }
                 catch (Exception)
                 {
                     throw;
                 }

                 StringBuilder sb = new StringBuilder();
                 sb.Append("[");
                 foreach (OrderBase item in rmsg.orderbaseList)
                 {
                     sb.Append("{");
                     sb.Append(string.Format("\"oid\":\"{0}\",", item.OrderId));
                     sb.Append(string.Format("\"oname\":\"{0}\",", item.GoodsName));
                     sb.Append(string.Format("\"count\":\"{0}\",", item.GoodsCnt));
                     sb.Append(string.Format("\"sumcount\":\"{0}\",", item.SumCount));
                     sb.Append(string.Format("\"gpic\":\"{0}\",", item.GoodsPic));
                     sb.Append(string.Format("\"state\":\"{0}\",", item.State));
                     sb.Append(string.Format("\"guige\":\"{0}\",", item.Guige));
                     sb.Append(string.Format("\"totals\":\"{0}\"", item.OrderTotals));
                     sb.Append("},");
                 }
                 sb.Append("]");
                 ViewBag.orderData = sb.ToString().Replace("},]", "}]"); 
             }
             else {
                 return RedirectToAction("WebappErrorPage", "ToOtherPage", new { errmsg = "获取订单异常！" });
             }
            
            return View();
        }

        /// <summary>
        /// ajax获取订单
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [HttpPost]
        public string _getmyorderByState(string uid,string state)
        {
            if (string.IsNullOrEmpty(state) || string.IsNullOrEmpty(uid))
            {
                return "1";//缺失参数 
            }
            else { 
                int s = int.Parse(state);
                ReturnMsg rmsg = new InterfaceData()._getOrderListByState(uid, s);
                if (rmsg.errcode == 0)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("[");
                    foreach (OrderBase item in rmsg.orderbaseList)
                    {
                        sb.Append("{");
                        sb.Append(string.Format("\"oid\":\"{0}\",", item.OrderId));
                        sb.Append(string.Format("\"oname\":\"{0}\",", item.GoodsName));
                        sb.Append(string.Format("\"count\":\"{0}\",", item.GoodsCnt));
                        sb.Append(string.Format("\"sumcount\":\"{0}\",", item.SumCount));
                        sb.Append(string.Format("\"gpic\":\"{0}\",", item.GoodsPic));
                        sb.Append(string.Format("\"state\":\"{0}\",", item.State));
                        sb.Append(string.Format("\"guige\":\"{0}\",", item.Guige));
                        sb.Append(string.Format("\"totals\":\"{0}\"", item.OrderTotals));
                        sb.Append("},");
                    }
                    sb.Append("]");

                    return sb.ToString().Replace("},]", "}]");
                    
                }
                else {
                    return "-1";//异常
                }
            }
        }

        /// <summary>
        /// 订单详情页
        /// </summary>
        /// <returns></returns>
        public ActionResult MallMyOrderDetails(string oid) 
        {
           ReturnMsg rmsg = new InterfaceData()._getOrderdetailsByoid(oid);
           if (rmsg.errcode == 0)
           {
               ViewBag.orderdetailsJson = rmsg.orderdetailsJson;
               try
               {
                   ViewBag.PayPrice = ConfigurationManager.AppSettings["PayPrice"];//不满78邮费或者货到付款预付款
               }
               catch (Exception)
               {
                   throw;
               }
           }
           else
           {
               return RedirectToAction("WebappErrorPage", "ToOtherPage", new { errmsg = "获取订单详情异常！" });
           }

            return View();
        }

        /// <summary>
        /// 待评价订单
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public ActionResult MallMyOrder_Comm(string uid)
        {
            ReturnMsg rmsg = new InterfaceData()._getOrderListByState(uid, 2);//获取待评价
            if (rmsg.errcode == 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("[");
                foreach (OrderBase item in rmsg.orderbaseList)
                {
                    sb.Append("{");
                    sb.Append(string.Format("\"oid\":\"{0}\",", item.OrderId));
                    sb.Append(string.Format("\"oname\":\"{0}\",", item.GoodsName));
                    sb.Append(string.Format("\"count\":\"{0}\",", item.GoodsCnt));
                    sb.Append(string.Format("\"sumcount\":\"{0}\",", item.SumCount));
                    sb.Append(string.Format("\"gpic\":\"{0}\",", item.GoodsPic));
                    sb.Append(string.Format("\"state\":\"{0}\",", item.State));
                    sb.Append(string.Format("\"guige\":\"{0}\",", item.Guige));
                    sb.Append(string.Format("\"totals\":\"{0}\"", item.OrderTotals));
                    sb.Append("},");
                }
                sb.Append("]");
                ViewBag.orderData = sb.ToString().Replace("},]", "}]");
            }
            else
            {
                return RedirectToAction("WebappErrorPage", "ToOtherPage", new { errmsg = "获取订单异常！" });
            }

            return View();
        }

        /// <summary>
        /// 生成订单
        /// </summary>
        /// <returns></returns>
        public string _createOrder(string name, string addr, string tel, string rem, string yunprice, string saleprice, string payment, string otherdata, string loginid)
        {
            if (string.IsNullOrEmpty(otherdata))
            {
                Data.MethodHepler.MHepler.writelog_err("生成订单异常，订单主要信息为空");
                return "-1";//数据异常 订单主要信息缺失
            } 
            else 
            {
                try
                {
                    Mltx_Order_main main = new Mltx_Order_main();
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    orderData _odata = js.Deserialize<orderData>(otherdata);
                    string orderid = Data.MethodHepler.MHepler.ConvertDateTimeInt(DateTime.Now).ToString() + new Random().Next(100, 999).ToString();//订单号 时间戳+三位随机数
                    string ordername = "";//订单名
                    decimal gtotals = 0;//商品总价
                    decimal otheradjust = 0;//价格调整
                    decimal transfrefee = decimal.Parse(yunprice);//邮费
                    decimal favourable = decimal.Parse(saleprice);//优惠
                    int gcnt = 0;
                    Payment _payment = (Payment)int.Parse(payment);//支付方式
                    foreach (_Data item in _odata.data)
                    {
                        ordername += item.gname + item.guige + " ";
                        gtotals += item.rprice * item.count;
                        gcnt += item.count;
                    }
                    decimal ototals = gtotals + otheradjust + transfrefee + favourable;//订单总价


                    main.OrderId = orderid;
                    main.OrderName = ordername.Length > 100 ? ordername.Substring(0, 99) : ordername;
                    main.GoodsTotals = gtotals;
                    main.OtherAdjust = otheradjust;
                    main.TransferFee = transfrefee;
                    main.Favourable = favourable;
                    main.OrderTotals = ototals;
                    main.GoodsCnt = gcnt;
                    main.Openid = loginid;
                    main.Customer = name;
                    main.CustomerAddr = addr;
                    main.CustomerTel = tel;
                    main.CustomerRem = rem;
                    main.OrderCls = OrderCls.商城;
                    main.Payment = _payment;
                    main.ShopId = 1;//默认
                    main.OrderTime = DateTime.Now;
                    main.FetchTime = DateTime.Now;
                    main.PayTime = DateTime.Parse("1970-01-01");
                    if (payment == "0")//微信支付
                    {
                        main.State = State.未付款;
                    }
                    else if (payment == "3")//货到付款
                    {
                        main.State = State.未预付款;
                    }

                    ///生成订单
                    ReturnMsg rmsg = new InterfaceData()._CreateOrder(main, _odata, orderid);

                    return rmsg.errcode.ToString() + "|" + orderid;
                }
                catch (Exception ex)
                {
                    Data.MethodHepler.MHepler.writelog_err("生成订单时组装数据时异常:" + ex.Message);
                    return "-3|生成订单异常";//生成数据时异常
                }
            }

        }

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="oid"></param>
        /// <returns></returns>
        public string _cancelOrder(string oid)
        {
            if (string.IsNullOrEmpty(oid))
            {
                return "-2";//缺失参数
            }
            else
            {
                ReturnMsg rmsg = new InterfaceData().CancelMyOrder(oid);
                return rmsg.errcode.ToString();
            }
        }

        /// <summary>
        /// 去评价
        /// </summary>
        /// <param name="oid">订单号</param>
        /// <returns></returns>
        public ActionResult MallDoComment(string oid)
        {
            ViewBag.oid = oid;
            if (string.IsNullOrEmpty(oid))
            {
                return RedirectToAction("WebappErrorPage", "ToOtherPage", new { errmsg = "参数缺失！" });
            }
            else
            {
                ReturnMsg rmsg = new InterfaceData()._getOrderCommentByOid(oid);
                if (rmsg.errcode == 0)
                {
                    ViewBag.orderdata = rmsg.orderdetailsJson;
                }
                else {
                    return RedirectToAction("WebappErrorPage", "ToOtherPage", new { errmsg = "获取数据异常！" });
                }
            }
            return View();
        }

        [HttpPost]
        /// <summary>
        /// ajax评价
        /// </summary>
        /// <param name="gid"></param>
        /// <param name="star"></param>
        /// <param name="_content"></param>
        /// <returns></returns>
        public string _doComments(string oid ,string gid,string gname,string customer, string star, string _content,bool _tag)
        {
            if (string.IsNullOrEmpty(gid) || string.IsNullOrEmpty(star) || string.IsNullOrEmpty(_content))
            {
                return "-2";//参数缺失 
            }
            else
            {
                ReturnMsg rmsg = new InterfaceData()._doCommentsBygid(oid, gid, gname, customer, star, _content, _tag);
                return rmsg.errcode.ToString();
            }
        }

        /// <summary>
        /// 确认收货
        /// </summary>
        /// <param name="oid"></param>
        /// <returns></returns>
        public string CommplateOrder(string oid)
        {
            if (string.IsNullOrEmpty(oid))
            {
                return "-2";//参数缺失 
            }
            else
            {
              ReturnMsg rmsg = new InterfaceData()._complateOrder(oid);
              return rmsg.errcode.ToString();
            }
        }

        /// <summary>
        /// 支付授权地址
        /// </summary>
        /// <param name="loginid"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public string _wxpay(string loginid,string order)
        {
            string loginid_order = "{\"login_id\":" + loginid + ",\"order\":" + order + "}";
            string oauthrul = ConfigurationManager.AppSettings["OAuthUrl"];
            
            string appid = ConfigurationManager.AppSettings["AppId"];
            string url_autho = oauthrul + "/Mltx_MallClass/WxPay?loginid_order=" + loginid_order;
            string url_autho_Encode = System.Web.HttpUtility.UrlEncode(url_autho);
            string upurl = string.Format("{0}/Mltx_MallClass/OAuthGet?appid={1}&codes={2}&url={3}", oauthrul, appid, "20158000", url_autho_Encode);

            Data.MethodHepler.MHepler.writelog_err("_wxpay 请求url:" + upurl);
             string result = PostAndGet.GetResponseString(upurl);

             Data.MethodHepler.MHepler.writelog_err("_wxpay 返回:" + result);
             if (result.StartsWith("http"))
             {
                 Response.Redirect(System.Web.HttpUtility.UrlDecode(result));
                 Response.End();

                 Data.MethodHepler.MHepler.writelog_err("end",false);
                 return "0";
             }
             else
             {
                 return "支付页面出错";               
             }
        }

        /// <summary>
        /// 支付页
        /// </summary>
        /// <param name="loginid"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public ActionResult WxPay(string loginid_order, string openid)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            string loginid = js.Deserialize<LoginIdOrder>(loginid_order).login_id;
            string order = js.Deserialize<LoginIdOrder>(loginid_order).order;
            Data.MethodHepler.MHepler.writelog_err("买家id：" + loginid + "----订单编号：" + order, false);
            using (MltxDbContext db = new MltxDbContext())
            {
                try
                {
                    string result = string.Empty;
                    WeiXinPayController paycontroller = new WeiXinPayController();

                    /*
                        查询订单
                     */
                    Mltx_Order_main main = null;
                    try
                    {
                        main = db.order_m.Where(f => f.Openid == loginid).Where(f => f.OrderId == order).FirstOrDefault();
                        if (main == null) return Content(error(1, "订单不存在"));
                        // paycontroller.weixinpay(order,openid,weid,0);
                        string PayPrice = ConfigurationManager.AppSettings["PayPrice"];
                        ViewBag.price = main.State == State.未预付款 ? float.Parse(PayPrice) : float.Parse(main.OrderTotals.ToString());
                        ViewBag.pricecount = main.State != State.未预付款 ? 0 : float.Parse(main.OrderTotals.ToString());
                        ViewBag.orderTime = main.OrderTime;
                        //获取支付参数
                        result = paycontroller.weixinpay(order,loginid, openid,0,"");
                        Data.MethodHepler.MHepler.writelog_err("返回：" + result, false);
                    }
                    catch { }
                    ViewBag.payjson = result.Split('|')[1];
                    ViewBag.openid = openid;
                    ViewBag.orderID = order;
                    ViewBag.loginid = loginid;
                }
                catch { }
                return View();
            }
        }


        public string error(int value0, string value1)
        {
            return "{\"return\":" + value0 + ",\"msg\":\"" + value1 + "\"}";
        }

    }//end  Controller-MltxMallClass

    /// <summary>
    /// 购物车cookie类
    /// </summary>
    public class shopCarData
    {
        public int gid { get; set; }
        public int count { get; set; }
    }

    public class orderData
    {
        public List<_Data> data { get; set; }
        public decimal sumPrice { get; set; }
    }
    
    public class _Data 
    {
        public int gid { get; set; }
        public string gname { get; set; }
        public string guige { get; set; }
        public string gpic { get; set; }
        public decimal rprice { get; set; }
        public int count { get; set; }
    }

    public class LoginIdOrder 
    {
        public string login_id { get; set; }
        public string order { get; set; }
    }

}