using System.Web;
using System.Web.Mvc;

namespace MltxManager.Data
{
    /// <summary>
    /// 判断用户是否登入
    /// </summary>
    public class SigninAuthority : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["User"] != null) return;
            var content = new ContentResult
            {
                Content = string.Format(
                    "<script type='text/javascript'>alert('请先登录！');top.location.href='{0}';</script>", "/home/login")
            };
            filterContext.Result = content;
        }
    }
}