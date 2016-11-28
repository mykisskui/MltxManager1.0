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
    public class MltxCommentsController : Controller
    {
        //
        // GET: /MltxComments/
        OrderData or = new OrderData();

        /// <summary>
        /// 评论信息列表
        /// </summary>
        /// <param name="id">页数</param>
        /// <param name="kword">搜素关键字</param>
        /// <returns></returns>
        [PageAuthority(ModeId = Modular.商品评论, QuanId = Quanxian.查看)]
        public ActionResult Index(int id = 1, string kword = null)
        {
            ReturnMsg rmsg = or.GetComments(id, kword);
            if (rmsg.errcode == 0)//成功返回
            {
                ViewBag.id = id;
                return View( rmsg.commentlist);
            }
            else
            {
                //错误页
                return RedirectToAction("ErrorPage", "ToOtherPage", new { errmsg = "程序发生异常，请稍候重试！！" });
            }
        }

        /// <summary>
        /// 获取某个商品的所有评论信息
        /// </summary>
        /// <param name="gid"></param>
        /// <param name="id">页数 </param>
        /// <returns></returns>
        [PageAuthority(ModeId = Modular.商品评论, QuanId = Quanxian.查看)]
        public ActionResult Comment_Goods(int gid,string gname, int id = 1)
        {
            ReturnMsg rmsg = or.GetCommentsByGid(gid, id);
            if (rmsg.errcode == 0)
            {
                ViewBag.gname = gname;

                return View(rmsg.commnetlist_goods);
            }
            else
            {
                return RedirectToAction("ErrorPage", "ToOtherPage", new { errmsg = "程序发生异常，请稍候重试！！" });
            }
        }

        /// <summary>
        /// 删除评论
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [PageAuthority(ModeId = Modular.商品评论, QuanId = Quanxian.删除, Leixing = false)]
        public string delcomments(int id)
        {
            return or._delcomments(id);
        }
    }
}
