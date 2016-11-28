using MltxManager.Models.DBHelper;
using MltxManager.Models.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Webdiyer.WebControls.Mvc;

namespace MltxManager.Data
{
    public class Slide
    {

        MltxDbContext db = new MltxDbContext();
        ReturnMsg msg = new ReturnMsg();
        /// <summary>
        /// 幻灯片列表
        /// </summary>
        /// <returns></returns>
        public ReturnMsg SlideIndex(int page = 1) {

            try {
                 msg.slides = db._slide.OrderByDescending(f => f.sort).ToPagedList(page, 10);

            }
            catch { }
            return msg;
        }
        /// <summary>
        /// 幻灯片添加and编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ReturnMsg SlideAddEditList(int id = 0) {
            msg.slide = new Mltx_Slide();
            try {
                msg.slide = db._slide.Where(f => f.Id == id).FirstOrDefault();
            }
            catch { }
            return msg;

        }
        /// <summary>
        ///幻灯片添加编辑
        /// </summary>
        /// <param name="slide"></param>
        /// <returns></returns>
        public ReturnMsg SlideAddEditAction(Mltx_Slide slide) {
            Mltx_Slide slideAdd = null;
            try {
                slideAdd = db._slide.Where(f => f.Id == slide.Id).FirstOrDefault();
                if (slideAdd == null)
                {

                    slideAdd = new Mltx_Slide();
                    slideAdd.Title = slide.Title;
                    slideAdd.URL = slide.URL;
                    slideAdd.Img = slide.Img;
                    slideAdd.Location = slide.Location;
                    slideAdd.SlideCls = slide.SlideCls;
                    slideAdd.Type = slide.Type;
                    slideAdd.Value = slide.Value;
                    slideAdd.sort = slide.sort;
                    slideAdd.Time = DateTime.Now;
                    db._slide.Add(slideAdd);
                }
                else {
                    slideAdd.Title = slide.Title;
                    slideAdd.URL = slide.URL;
                    slideAdd.Img = slide.Img;
                    slideAdd.Location = slide.Location;
                    slideAdd.SlideCls = slide.SlideCls;
                    slideAdd.Type = slide.Type;
                    slideAdd.Value = slide.Value;
                    slideAdd.sort = slide.sort;
                    slideAdd.Time = DateTime.Now;
                }
                db.SaveChanges();
                msg.errcode = 0;
                msg.errmsg = string.Format("{0}", "保存成功");
            }
            catch {
                msg.errcode = 1;
                msg.errmsg = string.Format("{0}", "保存失败");
            }
            return msg;
        }
        public ReturnMsg SlideRemove(int id = 0) {
            Mltx_Slide slide = null;
            try {

                slide = db._slide.Where(f => f.Id == id).FirstOrDefault();
                if (slide == null)
                {
                    msg.errcode = 1;
                    msg.errmsg = string.Format("{0}", "删除的幻灯片不存在");
                }
                else {
                    db._slide.Remove(slide);
                }

                db.SaveChanges();
                msg.errcode = 0;
                msg.errmsg = string.Format("{0}", "删除成功");
                    
            }
            catch {
                msg.errcode = 1;
                msg.errmsg = string.Format("{0}", "删除失败");
            }
            return msg;
        }
    }
}