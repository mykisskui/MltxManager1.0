using System.Web;
using System.Web.Mvc;
using MltxManager.Models.DBHelper.Mode;
using MltxManager.Models.DBModel;


namespace MltxManager.Data.MethodHepler
{
    /// <summary>
    /// 权限验证
    /// </summary>
    public class PageAuthority : ActionFilterAttribute
    {
        /// <summary>
        /// 拿到模块ID
        /// </summary>
        public Modular ModeId { get; set; }
        /// <summary>
        /// 权限ID
        /// </summary>
        public Quanxian QuanId { get; set; }

        /// <summary>
        /// ajax请改为false默认为true
        /// </summary>
        public bool Leixing = true;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                if (HttpContext.Current.Session["User"] == null) return;
                var a = HttpContext.Current.Session["User"].ToString();
                var re = new GetBiz().GetUser(int.Parse(a));
                if (re == 0)
                {
                    var content = new ContentResult
                    {
                        Content = string.Format(
                            "<script type='text/javascript'>alert('请重新登入在进行使用！');top.location.href='{0}';</script>",
                            "/home/login")
                    };
                    filterContext.Result = content;
                }
                else if (re != 20150000)
                {
                    var mode = (int)ModeId;
                    var quan = (int)QuanId;
                    var r = new GetBiz().GetModeAuthority(re, mode, quan);
                    if (r) return;
                    if (Leixing)
                    {
                        var content = new ContentResult
                        {
                            Content = "<script type=\'text/javascript\'>alert(\'你没有权限！请和管理员联系\');</script>"
                        };
                        filterContext.Result = content;
                    }
                    else
                    {
                        var content = new ContentResult {Content = "ajaxerror"};
                        filterContext.Result = content;
                    }
                }
            }
            else
            {
                var content = new ContentResult
                {
                    Content = string.Format(
                        "<script type='text/javascript'>alert('请先登录！');top.location.href='{0}';;</script>",
                        "/home/login")
                };
                filterContext.Result = content;
            }
        }
    }
}