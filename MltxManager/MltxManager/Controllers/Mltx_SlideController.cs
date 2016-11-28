using MltxManager.Data;
using MltxManager.Models.DBHelper;
using MltxManager.Models.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MltxManager.Controllers
{
    public class Mltx_SlideController : Controller
    {
        Slide cs = new Slide();
        JavaScriptSerializer js = new JavaScriptSerializer();
        ReturnMsg msg = new ReturnMsg();
        //
        // GET: /Mltx_Slide/

        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 幻灯片
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult Slide(int page = 1) {

            try {
                msg = cs.SlideIndex(page);
            }
            catch { }
            return View(msg.slides);
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult SlideAddEdit(int id = 0 ) {
            try {
                msg = cs.SlideAddEditList(id);
            }
            catch { }
            return View(msg.slide);
        }
        public string SlideAddEditAction(Mltx_Slide slide) {
            try {
                msg = cs.SlideAddEditAction(slide);
            }
            catch {
                msg.errcode = 1;
                msg.errmsg = string.Format("{0}", "保存失败");
            }
            string result = js.Serialize(msg);
            return result;
        }
        public string SlideRemove(string data) { 
            string result = string.Empty;

            try {
                int id = int.Parse(data);      
                msg = cs.SlideRemove(id);
            }
            catch {
                msg.errcode = 1;
                msg.errmsg = string.Format("{0}", "删除失败");
            }
            result = js.Serialize(msg);
            return result;
        }
    }
}
