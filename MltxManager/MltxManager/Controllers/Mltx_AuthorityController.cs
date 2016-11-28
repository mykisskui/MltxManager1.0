using System.Web.Mvc;
using MltxManager.Data;
using MltxManager.Data.MethodHepler;
using MltxManager.Models.DBHelper.Mode;
using MltxManager.Models.DBModel;

namespace MltxManager.Controllers
{
    [SigninAuthority]
    public class Mltx_AuthorityController : Controller
    {
        //
        // GET: /Mltx_Authority/


        #region 人员分组
        /// <summary>
        /// 人员分组（初始获取全部分组和模块）
        /// </summary>
        /// <returns></returns>
        public ActionResult Admin()             
        {
            var re = new GetBiz().GetAdmin();
            if (re.errcode == 0)
            {
                return View(re);
            }
            else
            {
                return RedirectToAction("ErrorPage", "ToOtherPage", new { errmsg = "程序发生异常，请稍候重试！！" });
            }
        }
        /// <summary>
        /// 人员分组 获取某个分组模块的权限
        /// </summary>
        /// 
        /// <param name="fenzhu">分组ID</param>
        /// <param name="mode">模块ID</param>
        /// <returns></returns>
        public ActionResult GetModeAuth(string fenzhu, string mode)
        {
            var r = int.Parse(mode);
            var re = new GetBiz().GetModeAuth(fenzhu, r);
            if (re.errcode == 0)
            {
                return PartialView("ModeAuth", re.DoubleBases);
            }
            else
            {
                return RedirectToAction("ErrorPage", "ToOtherPage", new { errmsg = "程序发生异常，请稍候重试！！" });
            }
        }
        /// <summary>
        /// 人员分组 更改分组模块权限
        /// </summary>
        /// <param name="quanxian">权限ID</param>
        /// <param name="fenzhu">分组ID</param>
        /// <param name="mode">模块ID</param>
        /// <returns></returns>
        [PageAuthority(ModeId = Modular.人员分组,QuanId = Quanxian.修改,Leixing = false)]
        public int UpdateModeAuth(string quanxian, string fenzhu, string mode)
        {
            var r = quanxian.Split(',');
            var re = new GetBiz().UpdateModeAuth(r, fenzhu, mode);
            return re.errcode == 0 ? 0 : 1;
        }
        /// <summary>
        /// 人员分组 添加分组
        /// </summary>
        /// <param name="name">分组名称</param>
        /// <returns></returns>
        [PageAuthority(ModeId = Modular.人员分组,QuanId = Quanxian.增加,Leixing = false)]
        public int InsertGrouping(string name)
        {
            return new GetBiz().InsertGrouping(name);
        }

        public ActionResult GetYongHuFenZhu()
        {
            var re = new GetBiz().GetYongHuFenZhu();
            if (re.errcode == 0)
            {
                return PartialView(re.DoubleBases);
            }
            else
            {
                return RedirectToAction("ErrorPage", "ToOtherPage", new { errmsg = "程序发生异常，请稍候重试！！" });
            }
        }
        /// <summary>
        /// 人员分组 分组修改
        /// </summary>
        /// <param name="id">分组ID</param>
        /// <param name="name">分组名称</param>
        /// <returns></returns>
         [PageAuthority(ModeId = Modular.人员分组, QuanId = Quanxian.修改,Leixing = false)]
        public int UpdateGrouping(int id,string name)
        {
            return new GetBiz().UpdateGrouping(id, name) ? 0 : 1;
        }

        #endregion

        #region 人员管理

        /// <summary>
        /// 人员管理页面
        /// </summary>
        /// <param name="id"></param>
        /// <param name="kword">查询信息</param>
        /// <returns></returns>
        public ActionResult Index(int id = 1, string kword = null)
        {
            
            var re = new GetBiz().GetPersonnel(id,kword);
            if (re.errcode == 0)
            {
                return PartialView(re.MltxUserInfos);
            }
            else
            {
                return RedirectToAction("ErrorPage", "ToOtherPage", new { errmsg = "程序发生异常，请稍候重试！！" });
            }

        }
        /// <summary>
        /// 人员管理 新建用户页面
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectClass()
        {
            var re = new GetBiz().GetGrouping();
            if (re.errcode == 0)
            {
                return View(re.mltx_userGroup);
            }
            else
            {
                return RedirectToAction("ErrorPage", "ToOtherPage", new { errmsg = "程序发生异常，请稍候重试！！" });
            }

        }
        /// <summary>
        /// 人员管理 新建用户
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="fen">分组</param>
        /// <param name="zhuang">状态</param>
        /// <returns></returns>
        [PageAuthority(ModeId = Modular.人员管理,QuanId = Quanxian.增加,Leixing = false)]
        public int InsertUserName(string name, string fen, string zhuang)
        {
            return new GetBiz().InsertUserName(name, fen, int.Parse(zhuang))?0:1;
        }
        /// <summary>
        /// 人员管理 人员编辑 
        /// </summary>
        /// <param name="goodId"></param>
        /// <returns></returns>
        [PageAuthority(ModeId = Modular.人员管理,QuanId = Quanxian.修改)]
        public ActionResult UpdateUserPage(int goodId)
        {
            var re = new GetBiz().UpdateUserPage(goodId);
            if (re.errcode == 0)
            {
                return PartialView(re);
            }
            else
            {
                return RedirectToAction("ErrorPage", "ToOtherPage", new { errmsg = "程序发生异常，请稍候重试！！" });
            }

        }
        /// <summary>
        /// 人员管理 用户编辑
        /// </summary>
        /// <param name="name">用户名</param>
        /// <param name="fen">分组</param>
        /// <param name="zhuang">状态</param>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        [PageAuthority(ModeId = Modular.人员管理,QuanId = Quanxian.修改,Leixing = false)]
        public int UpdateUserName(string name, string fen, string zhuang, int userId)
        {
            return new GetBiz().UpdateUserName(name, fen, zhuang, userId)?0:1;
        }
        /// <summary>
        /// 人员管理 注销用户
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <param name="r">判断类型</param>
        /// <returns></returns>
        [PageAuthority(ModeId = Modular.人员管理,QuanId = Quanxian.删除,Leixing = false)]
        public int DeleteUser(int id,int r=0)
        {
            return new GetBiz().DeleteUser(id,r)?0:1;
        }
        #endregion


       

    }
}
