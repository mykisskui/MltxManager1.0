﻿using MltxManager.Data;
using MltxManager.Models.DBHelper;
using MltxManager.Models.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MltxManager.Controllers
{
    public class Mltx_MenuController : Controller
    {
        Menu cs = new Menu();
        JavaScriptSerializer js = new JavaScriptSerializer();
        ReturnMsg msg = new ReturnMsg();
        //
        // GET: /Mltx_Menu/

        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 幻灯片
        /// </summary>
        /// <returns></returns>
        public ActionResult Slide() {

            return View();
        }
        /// <summary>
        /// 菜单
        /// </summary>
        /// <returns></returns>
        public ActionResult Menu() {
            IEnumerable<Mltx_Menu> menus = null;
            try {
                msg = cs.MenuIndex();
                menus = msg.menus;
            }
            catch { 
            
            }
            return View(menus);
        }
        /// <summary>
        /// 菜单删除
        /// </summary>
        /// <returns></returns>
        public string MenuRemove(int id=-1,int lid=-1) {
            string result = string.Empty;
            try {
                msg = cs.MenuRemove(id, lid);
            }
            catch { }
            result = js.Serialize(msg);
            return result;
        }
        /// <summary>
        /// 首页菜单添加and编辑
        /// </summary>
        /// <returns></returns>
        public string MenuAddEdit(string AppType, string AppValue, string Name, string URL, string Image, int Sort = -1, int Type = -1, int Mid = -1, int Class = -1)
        {

            string result = string.Empty;
            try {

                if (string.IsNullOrEmpty(Name)) {
                    msg.errcode = 1;
                    msg.errmsg = string.Format("{0}", "菜单名称格式错误");
                }
                else if (string.IsNullOrEmpty(URL)) {
                    msg.errcode = 1;
                    msg.errmsg = string.Format("{0}", "菜单路径错误");
                }
                else if (string.IsNullOrEmpty(Sort.ToString()) || Sort.GetType() != typeof(int))
                {
                    msg.errcode = 1;
                    msg.errmsg = string.Format("{0}", "菜单排序错误");
                }
                else if (string.IsNullOrEmpty(Type.ToString()) || Type.GetType() != typeof(int))
                {
                    msg.errcode = 1;
                    msg.errmsg = string.Format("{0}", "类型选择错误");
                }
                else if (string.IsNullOrEmpty(Image))
                {
                    msg.errcode = 1;
                    msg.errmsg = string.Format("{0}", "请选择图片");
                }
                else if (string.IsNullOrEmpty(Class.ToString()) || Type.GetType() != typeof(int)) {

                    msg.errcode = 1;
                    msg.errmsg = string.Format("{0}", "菜单类型选项错误");
                }
                else
                {
                    if (Class != 0) {
                        AppType = string.Empty;
                        AppValue = string.Empty;
                    }

                    msg = cs.MenuAddEdit(AppType,AppValue,Name, URL, Image, Sort, Type, Mid,Class);
                }
               
                       
            }
            catch {
                msg.errcode = 1;
                msg.errmsg = string.Format("{0}", "保存失败");
            }
            result = js.Serialize(msg);
            return result;
        }
        /// <summary>
        /// 浮动菜单添加and编辑
        /// </summary>
        /// <returns></returns>
        public string MenuFloatAddEdit(int mid = -1, int id = -1, string image = "")
        { 
            string result = string.Empty;
            string title = string.Empty; string url = string.Empty;
            try {
                if (id != -1)
                {
                    switch (id) { 
                       case 0:
                            title = "首页";
                            url = "/MltxTemplate/Index";
                            break;
                        case 1:
                            title = "购物车";
                            url = "/MltxTemplate/shoppingcart";
                            break;
                        case 2:
                            title = "分类";
                            url = "/MltxTemplate/classify";
                            break;
                        case 3:
                            title = "我的信息";
                            url = "/MltxTemplate/userinfo";
                            break;
                        default:
                            title = string.Empty;
                            url = string.Empty;
                            break;

                    }
                    //数据
                    msg = cs.MenuFloatAddEdit(mid, id,title,url,image);
                }
                else {

                    msg.errcode = 1;
                    msg.errmsg = string.Format("操作失败,请刷新重试");
                }
            }
            catch {
                msg.errcode = 1;
                msg.errmsg = string.Format("操作失败,请刷新重试");
            }

            result = js.Serialize(msg);

            return result;
        }
        /// <summary>
        /// 首页模块
        /// </summary>
        /// <returns></returns>
        public ActionResult MenuModule(int page = 1) {

            try {
                msg = cs.MenuModule(page);
               
            }
            catch { }
            return View(msg.indexmodules);

        }
        /// <summary>
        /// 首页模块删除
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string ModuleRemove(string data) { 
        string result = string.Empty;
        try {
            int id = int.Parse(data);
            msg = cs.MenuModuleRemove(id);
        }
        catch {
            msg.errcode = 1;
            msg.errmsg = string.Format("{0}", "删除失败");
        }
        result = js.Serialize(msg);

        return result;
        }
        /// <summary>
        /// 首页模块添加编辑
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult MenuModuleAddEdit(int id=0) {

            try {
                msg = cs.MenuModuleFirst(id);
            }
            catch { }

            ViewBag.id = id;
            return View(msg.indexmodule);
        }
        /// <summary>
        /// 首页模块添加编辑
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public string MenuModuleAddEdit(string Title,string data, int Modules_0 = -1, int Modules_1 = -1, int Modules_3 = -1, int Modules_6 = 0)
        {
            string result = string.Empty;
            
            try {
                if (string.IsNullOrEmpty(Title)) {
                    msg.errcode = 1;
                    msg.errmsg = string.Format("{0}", "请填写标题");
                }
                else if (string.IsNullOrEmpty(data))
                {
                    msg.errcode = 1;
                    msg.errmsg = string.Format("{0}", "模块设置填写错误");
                }
                else {
                    List<MltxManager.Data.Menu.MenuModuleC> menumodules = null;
                    menumodules = js.Deserialize<List<MltxManager.Data.Menu.MenuModuleC>>(data);
                    msg = cs.MenuModulesAddEdit(menumodules,Title, Modules_0, Modules_1, Modules_6);
                }
            }
            catch { }

            result = js.Serialize(msg);
            return result;
        }

    }
}
