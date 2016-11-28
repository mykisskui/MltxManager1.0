using System.Web.Mvc;
using MltxManager.Data;
using MltxManager.Data.MethodHepler;
using MltxManager.Models.DBHelper.Mode;
using MltxManager.Models.DBModel;

namespace MltxManager.Controllers
{
    [SigninAuthority]
    public class Mltx_TicketController : Controller
    {
        //
        // GET: /Mltx_Ticket/
        #region 提货卷管理页面
        /// <summary>
        /// 提货卷管理页面
        /// </summary>
        /// <param name="id"></param>
        /// <param name="kword"></param>
        /// <returns></returns>
        [PageAuthority(ModeId = Modular.提货券管理,QuanId = Quanxian.查看)]
        public ActionResult Index(int id = 1, string kword = null)
        {
            var re=new TicketData().InsertTicket(id,kword);
            if (re.MltxTickets != null)
            {
                return View(re.MltxTickets);
            }
            else
            {
                return RedirectToAction("ErrorPage", "ToOtherPage", new { errmsg = "程序发生异常，请稍候重试！！" });
            }
            
        }
        /// <summary>
        /// 提货卷添加页面显示
        /// </summary>
        /// <returns></returns>
        [PageAuthority(ModeId = Modular.提货券管理, QuanId = Quanxian.查看)]
        public ActionResult InsertTicket()
        {
            return View();
        }
        /// <summary>
        /// 添加提货卷数据
        /// </summary>
        /// <param name="sp">商品ID</param>
        /// <param name="sj">时间</param>
        /// <param name="sm">数目</param>
        /// <returns></returns>
        [PageAuthority(ModeId = Modular.提货券管理, QuanId = Quanxian.增加)]
        public int InsertTicketData(int sp,string sj,int sm)
        {
            var re = new TicketData().InsertTicketData(sp,sj,sm);
            return re ? 0 : 1;
        }
        /// <summary>
        /// 修改提货卷状态
        /// </summary>
        /// <param name="id">提货卷 ID</param>
        /// <param name="zt">修改状态</param>
        /// <returns></returns>
        [PageAuthority(ModeId = Modular.提货券管理, QuanId = Quanxian.修改)]
        public int UpdateForetaste(string id, int zt)
        {
            var re = new TicketData().UpdateForetaste(id,zt);
            return re ? 0 : 1;
        }
        /// <summary>
        /// 查询单个提货券是否有
        /// </summary>
        /// <param name="id">提货券号</param>
        /// <returns></returns>
        [PageAuthority(ModeId = Modular.提货券管理,QuanId = Quanxian.查看)]
        public ActionResult UpdateTicketData(string id)
        {
            var re = new TicketData().UpdateTicketData(id);
            return re != null ? PartialView(re) : null;
        }

        /// <summary>
        /// 修改单个状态
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        [PageAuthority(ModeId = Modular.提货券管理,QuanId = Quanxian.修改)]
        public int UpdateTicket(int id)
        {
            var re = new TicketData().UpdateTicket(id);
            return re ? 0 : 1;
        }
        #endregion
    }
}
