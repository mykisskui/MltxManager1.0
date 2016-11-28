﻿using MltxManager.Data;
using MltxManager.Models.DBModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MltxManager.Controllers
{
    public class Mltx_TemplateController : Controller
    {

        /*
            水果路径 Mltx_Template/fruitHome
            
         */
        private MltxDbContext db = new MltxDbContext();
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            IEnumerable<Mltx_Slide> slides = null;
            IEnumerable<Mltx_Menu> menus = null;
            IEnumerable<Mltx_IndexModule> modules = null;

            try {
                slides = db._slide.OrderBy(f => f.sort);//所有幻灯片数据
                menus = db._menu.OrderBy(f => f.Sort);//所有菜单数据
                modules = db.index_m;//所有首页模块
                /*
                    微信端不区分(web,安卓)
                 */

                foreach (Mltx_IndexModule item in modules) {
                   // byte[] bytes = Encoding.UTF8.GetBytes(item.Value);
                  //  item.Value = Convert.ToBase64String(bytes);
                    item.Value = HttpUtility.UrlEncode(item.Value);
                    
                }

                ViewBag.slides = json<Mltx_Slide>(slides.ToList());
                ViewBag.menus = json<Mltx_Menu>(menus.ToList());
                ViewBag.modules = json<Mltx_IndexModule>(modules.ToList());

                //微信jssdk验证
                wxapi wxapi = new Data.wxapi();
                jsapi_sdk sdk = wxapi.jssdk();
                ViewBag.sdk = new JavaScriptSerializer().Serialize(sdk);
            }
            catch {
                    
            }
            
            
            return View();
        }
        /// <summary>
        /// 水果
        /// </summary>
        /// <param name="id">水果分类</param>
        /// <returns></returns>
        public ActionResult fruitHome(int id = 0) {
            IEnumerable<Mltx_GoodsInfo_fresh> freshs = null;
            string freshGroupName = string.Empty;
            try
            {
                freshs = db.goodsinfo_fresh.Where(f => f.GroupId == id).Where(f =>f.Enable == Added.上架);
                freshGroupName = db.group_fresh.Where(f => f.GroupId == id).FirstOrDefault().GroupName;
             

            }
            catch {
            
            }
            ViewBag.freshGroupName = freshGroupName;
            ViewBag.fresh = json<Mltx_GoodsInfo_fresh>(freshs.ToList());
            return View();
        }
        /// <summary>
        /// 水果详情
        /// </summary>
        /// <returns></returns>
        public ActionResult fruitDetail(int id = 0) {
            IEnumerable<Mltx_GoodsInfo_fresh> fresh = null;
            try {
                fresh = db.goodsinfo_fresh.Where(f => f.GoodsId == id);
            }
            catch { }

            ViewBag.fresh = json<Mltx_GoodsInfo_fresh>(fresh.ToList());
            return View();
        }
        /// <summary>
        /// 搜索
        /// </summary>
        /// <returns></returns>
        public ActionResult search()
        {
            try { }
            catch { }
            return View();
        }
        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string searchList(string value) {
            string result = string.Empty;
            IEnumerable<Mltx_GoodsInfo_shop> goods = null;
            try
            {
                if (string.IsNullOrEmpty(value))
                {
                    result = string.Format("{0}", -2);
                }
                else {
                    goods = db.goodsinfo_shop.Where(f => f.GoodsName.Contains(value));
                    if (goods.Count() == 0) { result = string.Format("{0}", -1); } else {
                        result = json<Mltx_GoodsInfo_shop>(goods.ToList());
                    }

                }
            }
            catch { 
                        
            }
            return result;
        }
        /// <summary>
        ///  经纬度计算
        /// </summary>
        /// <returns></returns>
        public double LatAndLong(double lat = 0,double lng = 0) {
            double result = -1;
            IEnumerable<Mltx_ShopInfo> shopinfos = null;
            shopinfos = db.shopinfo;
            //存在一条符合的门店就可以
            //距离限制为2000米 两公里
            foreach (Mltx_ShopInfo item in shopinfos) {
                result = wxapi.DistanceOfTwoPoints(double.Parse(item.Shop_y), double.Parse(item.Shop_x), lng, lat, wxapi.GaussSphere.WGS84);
                if (result <= 2000) {
                    break;
                }
            }
            result = (result > 2000 ? -1 : result );
            return result;
        }
        /// <summary>
        /// 泛型转换json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public string json<T>(List<T> t) {
            string result = string.Empty;
            try
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                result = js.Serialize(t);
            }
            catch {
                result = string.Format("{0}", "error");
            }
            return result;
        }
        /// <summary>
        /// 测试方法
        /// </summary>
        /// <returns></returns>
        public string test() {
            wxapi api = new wxapi();
            Access_token token = api.Get_access_token();
            return token.access_token;
        }

    }
}
