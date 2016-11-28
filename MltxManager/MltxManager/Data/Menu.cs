using MltxManager.Models.DBHelper;
using MltxManager.Models.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using Webdiyer.WebControls.Mvc;

namespace MltxManager.Data
{
    public class Menu
    {

        MltxDbContext db = new MltxDbContext();
        ReturnMsg msg = new ReturnMsg();
        public ReturnMsg MenuIndex() {
            try {
                msg.menus = db._menu.OrderByDescending(f => f.Location);
            }
            catch {
                msg.errcode = 1;
                msg.errmsg = string.Format("{0}", "查询菜单列表失败");
            }

            return msg;
        }
        public ReturnMsg MenuAddEdit(string app,string value,string Name,string URL,string Image,int Sort = -1,int Type = -1,int Mid = -1,int Class=-1) {

            Mltx_Menu menu = null;
            try {
                menu = db._menu.Where(f => f.Id == Mid).Where(f => (int)f.Location == Type).FirstOrDefault();
                if (menu == null)
                {
                    menu = new Mltx_Menu();
                    menu.Name = Name;
                    menu.URL = URL;
                    menu.MenuCls = Class == -1 ? 0 : 1 ;
                    menu.Img = Image;
                    menu.Location = (Location)Type;
                    menu.Sort = Sort;
                    menu.Type = app;
                    menu.Value = value;
                    menu.Time = DateTime.Now;
                    db._menu.Add(menu);
                }
                else {
                    menu.Name = Name;
                    menu.URL = URL;
                    menu.MenuCls = Class == -1 ? 0 : 1;
                    menu.Img = Image;
                    menu.Location = (Location)Type;
                    menu.Sort = Sort;
                    menu.Type = app;
                    menu.Value = value;
                    menu.Time = DateTime.Now;
                }

                    db.SaveChanges();
                    msg.errcode = 0;
                    msg.errmsg = string.Format("{0}", "保存成功");

                    IEnumerable<Mltx_Menu> menus = db._menu.Where(f => (int)f.Location == Type).OrderBy(f => f.Sort).Take(8);
                    msg.menus = menus;
            }
            catch {
                msg.errcode = 1;
                msg.errmsg = string.Format("{0}", "保存失败");
            }
            return msg;
            
        }
        /// <summary>
        /// 菜单删除
        /// </summary>
        /// <param name="id"></param>
        /// <param name="lid"></param>
        /// <returns></returns>
        public ReturnMsg MenuRemove(int id=-1,int lid=-1) {
            Mltx_Menu menu = null;
            try {
                menu = db._menu.Where(f => f.Id == id).Where(f => (int)f.Location == lid).FirstOrDefault();
                if (menu == null)
                {
                    msg.errcode = 1;
                    msg.errmsg = string.Format("{0}", "删除的菜单不存在");
                }
                else {

                    db._menu.Remove(menu);

                    if (db.SaveChanges() > 0)
                    {
                        msg.errcode = 0;
                        msg.errmsg = string.Format("{0}", "删除菜单成功");
                        IEnumerable<Mltx_Menu> menus = db._menu.Where(f => (int)f.Location == lid).OrderByDescending(f => f.Location).Take(5);
                        msg.menus = menus;
                    }
                    else {
                        msg.errcode = 1;
                        msg.errmsg = string.Format("{0}", "删除菜单失败");
                    }
                }
            }
            catch { }
            return msg;
        }
        /// <summary>
        /// 查询全部
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ReturnMsg MenuModule(int page = 1) {

            PagedList<Mltx_IndexModule> indexmodules = null;
            try {
                indexmodules = db.index_m.OrderByDescending(f=>f.Id).ToPagedList(page,10);
                msg.indexmodules = indexmodules;
            }
            catch { }
            return msg;
        }
        /// <summary>
        /// 单一
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ReturnMsg MenuModuleFirst(int id = 0) {

            Mltx_IndexModule indexmodule = new Mltx_IndexModule();
            try {
                indexmodule = db.index_m.Where(f => f.Id == id).FirstOrDefault();
                msg.indexmodule = indexmodule;
            }
            catch { }
            return msg;
        }
        /// <summary>
        /// 首页模块删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ReturnMsg MenuModuleRemove(int id) {
            Mltx_IndexModule indexmodule = null;
            try {
                indexmodule = db.index_m.Where(f => f.Id == id).FirstOrDefault();
                if (indexmodule == null)
                {
                    msg.errcode = 1;
                    msg.errmsg = string.Format("{0}", "删除的模块不存在");
                }
                else {
                    db.index_m.Remove(indexmodule);
                }
                db.SaveChanges();
                msg.errcode = 0;
                msg.errmsg = string.Format("{0}", "删除成功");
            }
            catch { }
            return msg;
        }
        /// <summary>
        /// 首页模块添加编辑
        /// </summary>
        /// <returns></returns>
        public ReturnMsg MenuModulesAddEdit(List<MenuModuleC> menumodules,string Title,int Modules_0 = 0,int Modules_1 = 0 ,int Modules_6 = 0 ) {
            Mltx_IndexModule indexmodule = null;
            try {

                JavaScriptSerializer js = new JavaScriptSerializer();
                if (Modules_6 == 0)
                {
                    indexmodule = new Mltx_IndexModule();
                    indexmodule.Type = Modules_0;
                    indexmodule.Value = js.Serialize(menumodules);
                    indexmodule.IndexCls = Modules_1;
                    indexmodule.Title = Title;
                    db.index_m.Add(indexmodule);
                }
                else {
                    indexmodule = db.index_m.Where(f => f.Id == Modules_6).FirstOrDefault();
                    indexmodule.Type = Modules_0;
                    indexmodule.Value = js.Serialize(menumodules);
                    indexmodule.IndexCls = Modules_1;
                    indexmodule.Title = Title;
                }

                db.SaveChanges();
                msg.errcode = 0;
                msg.errmsg = string.Format("{0}", "保存成功");
            }
            catch { }
            return msg;
        }

        public class MenuModuleC {
            public int ID { get; set; }
            public string Image { get; set; }
            public string URL { get; set; }
            public int WebApp { get; set; }
            public string Type { get; set; }
            public string Value { get; set; }
        }
        public ReturnMsg MenuFloatAddEdit(int mid =  -1,int id = -1,string title = "",string url = "",string image = "") {


            Mltx_Menu menu = null;
            try {
                if (mid == -1)
                {

                    menu = new Mltx_Menu();
                    menu.Name = title;
                    menu.URL = url;
                    menu.MenuCls = 0;
                    menu.Img = image;
                    menu.Location = 0;
                    menu.Sort = 0;
                    menu.Time = DateTime.Now;
                    db._menu.Add(menu);
                }
                else {
                    menu = db._menu.Where(f => f.Id == mid).FirstOrDefault();
                    if (menu == null)
                    {
                        msg.errcode = 1;
                        msg.errmsg = string.Format("{0}", "编辑的菜单不存在,请刷新重试");
                    }
                    else {
                        menu.Img = image;
                        menu.Time = DateTime.Now;
                    }
                }

                db.SaveChanges();
                msg.errcode = 0;
                msg.errmsg = string.Format("{0}", "保存成功");
                IEnumerable<Mltx_Menu> menus = db._menu.Where(f => f.Location == 0).OrderByDescending(f=>f.Location).Take(5);
                msg.menus = menus;

            }
            catch {
                msg.errcode = 1;
                msg.errmsg = string.Format("{0}","保存失败,请刷新重试");
            }

            return msg;

        }
    }
}