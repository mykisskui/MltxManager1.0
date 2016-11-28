using MltxManager.Data;
using MltxManager.Models.DBModel;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace MltxManager.Controllers
{
    public class WeiXinPayController : Controller
    {
        public string PayURL = "https://api.mch.weixin.qq.com/pay/unifiedorder";
        //
        // GET: /WeiXinPay/

        public ActionResult Index()
        {

            string md5 = PayConfig.MD5("20158000");

            //15168013
            return Content(md5 + ":" + md5.Length);
        }

        static ReaderWriterLockSlim _rw = new ReaderWriterLockSlim();
        /// <summary>
        /// 微信支付成功通知
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public void paysuccess()
        {
            string post = string.Empty;
            HttpRequestBase request = Request;
            using (System.IO.Stream stream = request.InputStream)
            {
                byte[] bytes = new byte[stream.Length];
                stream.Read(bytes, 0, (Int32)stream.Length);
                post = System.Text.Encoding.UTF8.GetString(bytes);
            }

            int a = paylock(post);
            if (a == 0)
            {
                Data.MethodHepler.MHepler.writelog_err(string.Format("支付成功,详情数据:{0}", post));
            }
            else
            {
                Data.MethodHepler.MHepler.writelog_err(string.Format("支付失败,详情数据:{0}", post));
            }
            Data.MethodHepler.MHepler.writelog_err("返回了:" + post,false);
            StringBuilder sb = new StringBuilder();
            sb.Append("<xml>");
            sb.Append("<return_code><![CDATA[SUCCESS]]></return_code>");
            sb.Append("</xml>");
            Response.Write(sb.ToString());
            Response.End();



            //    new Thread(paylock).Start(post);
        }
        /// <summary>
        /// 测试lock线程
        /// </summary>
        public int paylock(object post)
        {
            try
            {
                int ss = 0;
                _rw.EnterWriteLock();//获取写锁
                //处理
                XmlSerializer xml = new XmlSerializer(typeof(PayConfig.WeiXinPost));
                StringReader reader = new StringReader(post.ToString());
                PayConfig.WeiXinPost weixinpost = xml.Deserialize(reader) as PayConfig.WeiXinPost;
                if (weixinpost.return_code == "SUCCESS" && weixinpost.result_code == "SUCCESS")
                {
                    /*
                     * 微信支付成功,获取json查看基础数据
                     */
                    if (weixinpost.json != string.Empty)
                    {
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        PayConfig.WeiXinPostJsonData json = js.Deserialize<PayConfig.WeiXinPostJsonData>(weixinpost.json);
                        switch (json.type)
                        {
                            case 0:
                                //商城 
                                //Data.MethodHepler.MHepler.writelog_err(json.openid, false);
                                ss = WeiXinPaySuccessEdit(json.order);
                                if (ss == -1)
                                {
                                    ss = WeiXinPaySuccessEdit(json.order);
                                }
                                return 0;
                            case 1:
                               
                                return 0;
                            case 2:
                              
                                return 0;

                        }
                    }

                    //返回无误

                }

                Data.MethodHepler.MHepler.writelog_err("处理了",false);
            }
            catch (Exception e)
            {
                Data.MethodHepler.MHepler.writelog_err(e.ToString());
            }
            finally
            {
                Data.MethodHepler.MHepler.writelog_err("关闭了", false);
                _rw.ExitWriteLock();
            }

            return -1;
            // Thread.Sleep(500);

        }

        /// <summary>
        /// 支付状态变更
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public int WeiXinPaySuccessEdit(string order)
        {
            //支付成功
            int gid = 0;//记录第一条商品编号
            using (MltxDbContext db = new MltxDbContext())
            {
                DbConnection con = ((IObjectContextAdapter)db).ObjectContext.Connection;
                con.Open();
                DbTransaction dt = con.BeginTransaction();
                try
                {
                    Mltx_Order_main main = db.order_m.Where(f => f.OrderId == order).FirstOrDefault();
                    if (main == null) return 1;
                    if (main.State == State.待发货 || main.State == State.已预付款) return 0;

                    if (main.State == State.未付款)
                    {
                        main.PayTime = DateTime.Now;
                        main.State = State.待发货;
                    }
                    else if(main.State == State.未预付款)
                    {

                        main.PayTime = DateTime.Now;
                        main.State = State.已预付款;
                    }

                    db.Entry(main).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    dt.Commit();

                    return 0;
                }
                catch
                {
                    Data.MethodHepler.MHepler.writelog_err("编辑支付状态,事物回滚");
                    dt.Rollback();
                    return -1;
                }
            }
        }

        /// <summary>
        /// 请求微信支付
        /// </summary>
        /// <param name="type">产品类型:
        /// </param>
        /// <returns></returns> 
        public string weixinpay(string order,string loginid, string openid, int type, string code)
        {

            PayConfig pay = new PayConfig();
            string signcode = pay.WeiXinPay(order, loginid, openid, type, code, "20158000");
            Data.MethodHepler.MHepler.writelog_err("返回数据weixinpay：" + signcode);
            if (signcode.Contains("error"))
            {
                //Response.Redirect("/HotelTemplate/successAnderror?msg=&no=2");
                //Response.End();
                return "1|支付失败";
            }
            try
            {
                string result = PostAndGet.PostWebRequest(PayURL, signcode, "utf-8");
                Data.MethodHepler.MHepler.writelog_err("返回数据weixinpay-PostWebRequest：" + result);
                XmlSerializer xml = new XmlSerializer(typeof(PayConfig.pCodepay));
                StringReader reader = new StringReader(result);
                PayConfig.pCodepay pcodes = xml.Deserialize(reader) as PayConfig.pCodepay;
                if (pcodes.return_code == "SUCCESS" && pcodes.result_code == "SUCCESS")
                {
                    string pagejson = pay.PayConfigSuccess(pcodes);
                    if (pagejson == string.Empty) return "1|支付时出现错误,请重新尝试";

                    return "0|" + pagejson;
                }
                Data.MethodHepler.MHepler.writelog_err(result);
                return "1|支付参数错误,请刷新重试";
            }
            catch (Exception e)
            {
                return "1|支付参数异常：" + e.Message;
            }
        }


    }
}
