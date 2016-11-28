using MltxManager.Data;
using MltxManager.Data.MethodHepler;
using MltxManager.Models.DBHelper;
using MltxManager.Models.DBHelper.Mode;
using MltxManager.Models.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MltxManager.Controllers
{
    [SigninAuthority]
    public class MltxShopController : Controller
    {
        //
        // GET: /MltxShop/
        ShopData sd = new ShopData();


        /// <summary>
        /// 获取门店列表
        /// </summary>
        /// <param name="id"></param>
        /// <param name="kword"></param>
        /// <returns></returns>
        //[PageAuthority(ModeId = Modular.门店管理, QuanId = Quanxian.查看)]
        public ActionResult Index(int id = 1, string kword = null)
        {
            ReturnMsg rmsg = sd.GetShopInfo(id, kword);
            if (rmsg.errcode == 0)
            {
                ViewBag.id = id;
                return View(rmsg.shopinfolist);
            }
            else
            {
                return RedirectToAction("ErrorPage", "ToOtherPage", new { errmsg = "程序发生异常，请稍候重试！！" });
            }
        }

        /// <summary>
        /// 删除门店
        /// </summary>
        /// <param name="shopid">门店号</param>
        /// <returns></returns>
        //[PageAuthority(ModeId = Modular.门店管理, QuanId = Quanxian.删除, Leixing=false)]
        public string delShop(int shopid)
        {
            return sd._delShop(shopid);
        }

        /// <summary>
        /// 创建门店 get
        /// </summary>
        /// <returns></returns>
        //[PageAuthority(ModeId = Modular.门店管理, QuanId = Quanxian.创建)]
        [HttpGet]
        public ActionResult Create()
        {
            Mltx_ShopInfo model = new Mltx_ShopInfo();
            return View(model);
        }

        /// <summary>
        /// 创建门店信息 post
        /// </summary>
        /// <param name="sinfo"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(Mltx_ShopInfo sinfo,FormCollection collection)
        {
            string location_p = collection["location_p"];//省
            string location_c = collection["location_c"];//市
            string location_a = collection["location_a"];//县区

            string addr = location_p + "|" + location_c + "|" + location_a + "|";
            string rmsg = sd._carateOreditShopinfo(sinfo, addr);
            if (rmsg == "0")
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("ErrorPage", "ToOtherPage", new { errmsg = "程序发生异常，请稍候重试！！" });
            }
        }


        /// <summary>
        /// 编辑门店 get
        /// </summary>
        /// <param name="shopid"></param>
        /// <returns></returns>
        //[PageAuthority(ModeId = Modular.门店管理, QuanId = Quanxian.修改)]
        [HttpGet]
        public ActionResult Edit(int shopid)
        {
            ReturnMsg rmsg = sd.GetShopInfoByShopid(shopid);
            if (rmsg.errcode == 0)
            {
                ViewBag.la = rmsg.shopinfo.Shop_x;
                ViewBag.ln = rmsg.shopinfo.Shop_y;

                return View(rmsg.shopinfo);
            }
            else
            {
                return RedirectToAction("ErrorPage", "ToOtherPage", new { errmsg = "程序发生异常，请稍候重试！！" });
            }
        }

        /// <summary>
        /// 编辑门店 post
        /// </summary>
        /// <param name="sinfo"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(Mltx_ShopInfo sinfo, FormCollection collection)
        {
            string location_p = collection["location_p"];//省
            string location_c = collection["location_c"];//市
            string location_a = collection["location_a"];//县区

            string addr = location_p + "|" + location_c + "|" + location_a + "|";
            string rmsg = sd._carateOreditShopinfo(sinfo, addr, false);
            if (rmsg == "0")
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("ErrorPage", "ToOtherPage", new { errmsg = "程序发生异常，请稍候重试！！" });
            }
        }
    }
}
