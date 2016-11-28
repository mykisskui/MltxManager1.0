using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MltxManager.Data;

namespace MltxManager.Controllers
{
    [SigninAuthority]
    public class Mltx_ForetasteController : Controller
    {
        //
        // GET: /Mltx_Foretaste/
        #region 试吃商品
        /// <summary>
        /// 试吃商品数据显示
        /// </summary>
        /// <param name="id">页数</param>
        /// <param name="kword">查询关键字</param>
        /// <returns></returns>
        public ActionResult Index(int id = 1, string kword = null)
        {
            var re = new ForetasteDate().GetForetaste(id, kword);
            if (re.errcode == 0)
            {
                return View(re.Foretastes);
            }
            else
            {
                return RedirectToAction("ErrorPage", "ToOtherPage", new { errmsg = "程序发生异常，请稍候重试！！" });
            }
        }
        /// <summary>
        /// 试吃商品添加
        /// </summary>
        /// <returns></returns>
        public ActionResult InsertForetaste()
        {
            return View();
        }
        /// <summary>
        /// 试吃商品添加
        /// </summary>
        /// <param name="sp">商品</param>
        /// <param name="sj">结束时间</param>
        /// <param name="qs">期数</param>
        /// <param name="zhuangtai">试吃状态</param>
        /// <returns></returns>
        public int InsertForetasteData(int sp, string sj, int qs, int zhuangtai)
        {
            var re = new ForetasteDate().InsertForetaste(sp, DateTime.Parse(sj), qs, zhuangtai);
            return re ? 0 : 1;
        }

        /// <summary>
        /// 试吃商品修改
        /// </summary>
        /// <param name="sp">商品</param>
        /// <param name="sj">结束时间</param>
        /// <param name="qs">期数</param>
        /// <param name="zhuangtai">试吃状态</param>
        /// <param name="id">试吃ID</param>
        /// <returns></returns>
        public int UpdateForetasteData(int sp, string sj, int qs, int zhuangtai, int id)
        {
            var re = new ForetasteDate().UpdateForetasteData(sp, DateTime.Parse(sj), qs, zhuangtai, id);
            return re ? 0 : 1;
        }
        /// <summary>
        /// 试吃商品选择
        /// </summary>
        /// <param name="id">页数</param>
        /// <param name="kword">查询关键字</param>
        /// <returns></returns>
        public ActionResult CreateiForetaste(int id = 1, string kword = null)
        {
            var re = new ForetasteDate().GetForetasteCommodity(id, kword);
            if (re.errcode == 0)
            {
                return View(re.MltxGoodsInfoShops);
            }
            else
            {
                return RedirectToAction("ErrorPage", "ToOtherPage", new { errmsg = "程序发生异常，请稍候重试！！" });
            }
        }
        /// <summary>
        /// 试吃商品状态修改
        /// </summary>
        /// <param name="i">试吃商品ID</param>
        /// <param name="r">试吃商品状态</param>
        /// <returns></returns>
        public int DeleteForetaste(int i, int r)
        {
            return new ForetasteDate().DeleteForetaste(i, r);
        }
        /// <summary>
        /// 修改试吃商品状态页面显示
        /// </summary>
        /// <param name="id">试吃ID</param>
        /// <returns></returns>
        public ActionResult UpdateForetastePage(int id)
        {
            var re = new ForetasteDate().UpdateForetastePage(id);
            if (re != null)
            {
                return View(re);
            }
            else
            {
                return RedirectToAction("ErrorPage", "ToOtherPage", new { errmsg = "程序发生异常，请稍候重试！！" });
            }
        }
        #endregion
        #region 试吃报告
        /// <summary>
        /// 试吃报告显示
        /// </summary>
        /// <param name="id">页数</param>
        /// <param name="kword">查询关键字</param>
        /// <returns></returns>
        public ActionResult Index_report(int id=1,string kword=null)
        {
            var re = new ForetasteDate().Index_report(id,kword);
            if (re.errcode == 0)
            {
                return View(re.MltxForetasteReports);
            }
            else
            {
                return RedirectToAction("ErrorPage", "ToOtherPage", new { errmsg = "程序发生异常，请稍候重试！！" });
            }

        }
        /// <summary>
        /// 查看试吃报告信息
        /// </summary>
        /// <param name="id">试吃报告ID</param>
        /// <returns></returns>
        public ActionResult InsertForetasteReport(int id)
        {
            var re = new ForetasteDate().InsertForetasteReport(id);
            return View(re);
        }
        #endregion
    }
}
