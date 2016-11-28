using MltxManager.Data;
using MltxManager.Models.DBHelper;
using MltxManager.Models.DBModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Webdiyer.WebControls.Mvc;

namespace MltxManager.Controllers
{
    [SigninAuthority]
    public class Mltx_MemberController : Controller
    {

        MemberData cs = new MemberData();
        JavaScriptSerializer js = new JavaScriptSerializer();
        ReturnMsg msg = new ReturnMsg();

        //
        // GET: /Mltx_Member/

        /// <summary>
        /// 会员信息
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int id = 1,string search = "",string key = "")
        {
            PagedList<Mltx_MemberInfo> infos = new List<Mltx_MemberInfo>() as PagedList<Mltx_MemberInfo>;
            try {
                msg = cs.Info(search,key,id);
                infos = msg.MemberInfos;
            }
            catch {
            }
            return View(infos);
        }
        /// <summary>
        /// 会员信息编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult IndexEdit(string id) {
            Mltx_MemberInfo info = new Mltx_MemberInfo();
            try {

                msg = cs.Info(int.Parse(id));
                info = msg.MemberInfo;
            }
            catch { }
            return View(info);
        }
        /// <summary>
        /// 会员信息编辑修改
        /// </summary>
        /// <param name="data"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        [HttpPost]
        public string IndexEdit(string data ,string config="") {
            string result = string.Empty;
            string rx = string.Empty;
            try {
                Mltx_MemberInfo info = js.Deserialize<Mltx_MemberInfo>(data);
                rx = @"((\+|-|)[0-9]+)|0{1}$";
                if (!Regex.IsMatch(info.Integral.ToString(), rx)) {
                    msg.errcode = 1;
                    msg.errmsg = string.Format("{0}", "输入积分不正确");
                }
                rx = @"((\+|-|)[0-9]+)|0{1}$";
                if (!Regex.IsMatch(info.Balance.ToString("F2"), rx)) {
                    msg.errcode = 1;
                    msg.errmsg = string.Format("{0}", "输入余额不正确");
                }
                rx = @"^[a-zA-Z0-9\u4e00-\u9fa5]+$";
                if (!Regex.IsMatch(info.MemberName, rx)) {
                    msg.errcode = 1;
                    msg.errmsg = string.Format("{0}", "会员名称不正确");
                }
                if (msg.errcode != 1)
                {
                    msg = cs.IndexEdit(info);
                }
            }
            catch {
                msg.errcode = 1;
                msg.errmsg = string.Format("{0}", "会员信息编辑错误,请刷新重试");
            }
            result = js.Serialize(msg);
            return result;
        }
        /// <summary>
        /// 会员等级
        /// </summary>
        /// <returns></returns>
        public ActionResult Level(int id =1) {
            PagedList<Mltx_MemberRank> level = new List<Mltx_MemberRank>() as PagedList<Mltx_MemberRank>;
            try {
                msg = cs.Level(id);
                level = msg.Level;
            }
            catch {
            
            }
            return View(level);
        }
        /// <summary>
        /// 会员等级删除
        /// </summary>
        /// <returns></returns>
        public string LevelRemove(string data)
        {
            string result = string.Empty;
            try
            {
                MatchCollection match = Regex.Matches(data, "\"\\d+\"", RegexOptions.IgnoreCase);
                int[] arr = new int[match.Count];
                int i = 0;
                     foreach(Match item in match){
                         arr[i] =int.Parse(item.Value.TrimStart('"').TrimEnd('"'));
                         i++;
                     }
                     msg = cs.LevelRemove(arr);
            }
            catch {
                msg.errcode = 1;
                msg.errmsg = string.Format("{0}","会员等级删除失败,请刷新重试");
            }
            result = js.Serialize(msg);
            return result;
            
        }
        public string LogRemove(string data) {
            string result = string.Empty;
            try
            {
                MatchCollection match = Regex.Matches(data, "\"\\d+\"", RegexOptions.IgnoreCase);
                int[] arr = new int[match.Count];
                int i = 0;
                foreach (Match item in match)
                {
                    arr[i] = int.Parse(item.Value.TrimStart('"').TrimEnd('"'));
                    i++;
                }
                msg = cs.LogRemove(arr);
            }
            catch
            {
                msg.errcode = 1;
                msg.errmsg = string.Format("{0}", "会员明细删除失败,请刷新重试");
            }
            result = js.Serialize(msg);
            return result;
        }
        /// <summary>
        /// 会员等级添加
        /// </summary>
        /// <returns></returns>
        public ActionResult LevelAdd(string id,string config) {
            Mltx_MemberRank level = new Mltx_MemberRank();
            try {
                string rx = @"^[0-9]+";
                if (Regex.IsMatch(id, rx)) {
                    msg = cs.Level(int.Parse(id), null);
                    level = msg.Level_n;
                }
            }
            catch {
            }
            return View(level);    
        }
        [HttpPost]
        public string LevelAdd(string data)
        {
            string result = string.Empty;
            string rx = string.Empty;
            try {
                MltxManager.Data.MemberData.LevelAddcs add = js.Deserialize<MltxManager.Data.MemberData.LevelAddcs>(data);
                try {
                    rx = @"(^[1-9][0-9]$)|(^[1-9]$)|(^100)$";
                    if (!Regex.IsMatch(add.leveldiscount.ToString(), rx))
                    {
                        msg.errcode = -1;
                        msg.errmsg = string.Format("{0}", "折扣率必须为[1-100]整数");
                    }
                }
                catch {
                    msg.errcode = -1;
                    msg.errmsg = string.Format("{0}", "折扣率必须为[1-100]整数");
                }
                try {
                    rx = @"[0-9]+-[0-9]+$";
                    if (!Regex.IsMatch(add.levelset, rx)) {
                        msg.errcode = 1;
                        msg.errmsg = string.Format("{0}", "额度格式:1-1000 必须为整数");
                    }
                }
                catch {
                    msg.errcode = -1;
                    msg.errmsg = string.Format("{0}", "额度格式:1-1000 必须为整数");
                }
                try {
                    rx = @"^[a-zA-Z0-9\u4e00-\u9fa5]+$";
                    if (!Regex.IsMatch(add.levelname, rx)) {
                        msg.errcode = -1;
                        msg.errmsg = string.Format("{0}", "会员等级名称不合法");
                    }
                }
                catch {
                    msg.errcode = -1;
                    msg.errmsg = string.Format("{0}", "会员等级名称不合法");
                }

                if (msg.errcode != -1) {

                    msg = cs.LevelAdd(add);         

                }
            }
            catch {
                msg.errcode = 1;
                msg.errmsg = string.Format("{0}", "数据错误,请重新输入");
            }

            result = js.Serialize(msg);
            return result;
        }
        /// <summary>
        /// 会员明细
        /// </summary>
        /// <returns></returns>
        public ActionResult Log(int id = 1,string search = "",string key="") {
            PagedList<Mltx_MemberLog> logs = new List<Mltx_MemberLog>() as PagedList<Mltx_MemberLog>;

            try {
                msg = cs.Log(id, search, key);
                logs = msg.MemberLogs;
            }
            catch { }
            return View(logs);
        }
        /// <summary>
        /// 会员状态
        /// </summary>
        /// <returns></returns>
        public string StatusEdit(string data) {
            string result = string.Empty;
            try {
                if (string.IsNullOrEmpty(data))
                {
                    msg.errcode = 1;
                    msg.errmsg = string.Format("{0}", "会员状态变更失败,请刷新重试");
                }
                else {
                    int id = int.Parse(data);
                    msg = cs.StatusEdit(id);
                }
            }
            catch {
                msg.errcode = 1;
                msg.errmsg = string.Format("{0}", "会员状态变更失败,请刷新重试"); 
            }
            result = js.Serialize(msg);

            return result;
        }



    }
}
