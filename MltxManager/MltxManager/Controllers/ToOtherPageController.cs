using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MltxManager.Controllers
{
    public class ToOtherPageController : Controller
    {
        //跳转的其他页面 包括异常 权限等
        // GET: /ToOtherPage/

        /// <summary>
        /// 异常错误页
        /// </summary>
        /// <param name="errmsg">错误信息</param>
        /// <returns></returns>
        public ActionResult ErrorPage(string errmsg)
        {
            ViewBag.errmsg = errmsg;
            return View();
        }

        /// <summary>
        /// 移动端页面异常页
        /// </summary>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public ActionResult WebappErrorPage(string errmsg)
        {
            ViewBag.msg = errmsg;
            return View();
        }
    }
}
