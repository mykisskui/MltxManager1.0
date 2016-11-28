using MltxManager.Data;
using MltxManager.Data.MethodHepler;
using MltxManager.Models.DBHelper;
using MltxManager.Models.DBHelper.Mode;
using MltxManager.Models.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MltxManager.Controllers
{
    [SigninAuthority]
    public class MltxOrderManagerController : Controller
    {
        //
        // GET: /MltxOrderManager/
        OrderData or = new OrderData();

        /// <summary>
        /// 商城订单
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="id"></param>
        /// <param name="kword"></param>
        /// <returns></returns>
        [PageAuthority(ModeId = Modular.商品订单, QuanId = Quanxian.查看)]
        public ActionResult Index(string tag, int id = 1, string kword = null)
        {
            ReturnMsg rmsg = or.GetOrderMain(0, tag, id, kword);
            if (rmsg.errcode == 0)//成功返回
            {
                ViewBag.id = id;
                return View(rmsg.plist_order_m);
            }
            else
            {
                //错误页
                return RedirectToAction("ErrorPage", "ToOtherPage", new { errmsg = "程序发生异常，请稍候重试！！" });
            }
        }

        /// <summary>
        /// 鲜果订单
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="id"></param>
        /// <param name="kword"></param>
        /// <returns></returns>
        [PageAuthority(ModeId = Modular.商品订单, QuanId = Quanxian.查看)]
        public ActionResult Index_fresh(string tag, int id = 1, string kword = null)
        {
            ReturnMsg rmsg = or.GetOrderMain(1, tag, id, kword);
            if (rmsg.errcode == 0)//成功返回
            {
                ViewBag.id = id;
                return View("Index",rmsg.plist_order_m);
            }
            else
            {
                //错误页
                return RedirectToAction("ErrorPage", "ToOtherPage", new { errmsg = "程序发生异常，请稍候重试！！" });
            }
        }

        /// <summary>
        /// 查看详细订单
        /// </summary>
        /// <param name="oid"></param>
        /// <returns></returns>
         [PageAuthority(ModeId = Modular.商品订单, QuanId = Quanxian.查看,Leixing=false)]
        public string OrderDetails(string oid)
        {
            ReturnMsg rmsg = or.GetOrderDetails(oid);

            if (rmsg.errcode == 0)//成功返回
            {
                List<Mltx_Order_sub> list = rmsg.mltx_order_sub;
                StringBuilder sb = new StringBuilder();
                sb.Append("{\"o_s_info\":[");
                foreach (Mltx_Order_sub model in list)
                {
                    sb.Append("{");
                    sb.Append(string.Format("\"gpic\":\"{0}\",", model.GoodsPic));
                    sb.Append(string.Format("\"gname\":\"{0}\",", model.GoodsName));
                    sb.Append(string.Format("\"gprice\":\"{0}\",", model.GoodsPrice));
                    sb.Append(string.Format("\"gcount\":\"{0}\"", model.GoodsCnt));
                    sb.Append("},");
                }
                sb.Remove(sb.Length-1, 1);
                sb.Append("],");

                sb.Append(string.Format("\"orderid\":\"{0}\",", list[0].OrderId.OrderId));
                sb.Append(string.Format("\"customer\":\"{0}\",", list[0].OrderId.Customer));
                sb.Append(string.Format("\"payment\":\"{0}\",", list[0].OrderId.Payment.ToString()));
                sb.Append(string.Format("\"ftime\":\"{0}\",", list[0].OrderId.FetchTime));
                sb.Append(string.Format("\"ptime\":\"{0}\",", list[0].OrderId.PayTime));
                sb.Append(string.Format("\"otime\":\"{0}\",", list[0].OrderId.OrderTime));
                sb.Append(string.Format("\"gaijia\":\"{0}\",", Math.Abs(list[0].OrderId.OtherAdjust)));
                sb.Append(string.Format("\"youhui\":\"{0}\",", Math.Abs(list[0].OrderId.Favourable)));
                sb.Append(string.Format("\"youfei\":\"{0}\",", list[0].OrderId.TransferFee));
                sb.Append(string.Format("\"tel\":\"{0}\",", list[0].OrderId.CustomerTel));
                sb.Append(string.Format("\"addr\":\"{0}\",", list[0].OrderId.CustomerAddr));
                sb.Append(string.Format("\"pay\":\"{0}\",", list[0].OrderId.OrderTotals));
                sb.Append(string.Format("\"state\":\"{0}\",", (int)list[0].OrderId.State));
                sb.Append(string.Format("\"yaoqiu\":\"{0}\"", list[0].OrderId.CustomerRem));//买家要求 json 结束
                sb.Append("}");

                return sb.ToString();
            }
            else
            {
                //错误
                Data.MethodHepler.MHepler.writelog_err("【MltxOrderManagerController】-【OrderDetails】:" + rmsg.errmsg);
                return "error";
            }
        }

        /// <summary>
        /// 修改价格
        /// </summary>
        /// <param name="oid">订单号</param>
        /// <param name="num">修改数值</param>
        /// <returns></returns>
        [PageAuthority(ModeId = Modular.商品订单, QuanId = Quanxian.修改, Leixing = false)]
        public string ChangePrice(string oid, string num)
        {
            return or._ChangePrice(oid, num);
            
        }

        /// <summary>
        /// 订单处理状态
        /// </summary>
        /// <param name="oid">订单编号</param>
        /// <param name="state">订单状态</param>
        /// <returns></returns>
        [PageAuthority(ModeId = Modular.商品订单, QuanId = Quanxian.修改, Leixing = false)]
        public string DoOrder(string state, string oid)
        {
            State s = (State)int.Parse(state);
            return or._do_order(s, oid);
        }

        /// <summary>
        /// 删除订单
        /// </summary>
        /// <param name="oid">订单编号</param>
        /// <param name="state">订单状态</param>
        /// <returns></returns>
        [PageAuthority(ModeId = Modular.商品订单, QuanId = Quanxian.删除, Leixing = false)]
        public string delorder(string state, string oid)
        {
            State s = (State)int.Parse(state);
            return or._do_order(s, oid);
        }
    }
}
