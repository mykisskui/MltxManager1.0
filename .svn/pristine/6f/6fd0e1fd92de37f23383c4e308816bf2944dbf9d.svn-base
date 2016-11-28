using MltxManager.Models.DBModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace MltxManager.Data
{
    public class PayConfig
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        /// <summary>
        /// 微信支付成功通知
        /// </summary>
        /// <param name="weid"></param>
        /// <param name="openid"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public string paysuccess(string weid, string openid, string order)
        {
            return string.Empty;
        }
        /// <summary>
        /// 微信支付查询
        /// </summary>
        /// <param name="loginid"></param>
        /// <returns></returns>
        public string payselect(string loginid, string order, string weid = "20158000")
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            int or = 0; string payconfig = string.Empty; string result = string.Empty;
         
            try
            {
                payconfig = PayConfigInfo();
                if (payconfig == string.Empty) return "{\"error\":\"商户配置错误\"}";
                ims_payinfo pay = js.Deserialize<ims_payinfo>(payconfig);
                dic.Add("appid", pay.Appid);
                dic.Add("mch_id", pay.Mchid);
                dic.Add("out_trade_no", order);
                dic.Add("nonce_str", MD5(weid));
                dic.Add("sign", SignS(dic, pay.KeyValue));

            }
            catch (Exception e)
            {
                Data.MethodHepler.MHepler.writelog_err("查询订单状态异常:" + e.ToString());
            }
            return PayXML(dic);
        }
        /// <summary>
        /// 微信支付提交
        /// </summary>
        /// <param name="loginid">购买用户</param>
        /// <param name="order">订单号</param>
        /// <param name="title">商品标题</param>
        /// <param name="weid">公众号</param>
        /// <returns></returns>
        public string WeiXinPay(string order, string loginid, string openid, int type, string code, string weid)
        {
            Data.MethodHepler.MHepler.writelog_err("weixinPay进入：");
            float markprice = 0; string marktitle = string.Empty;
            //获取商户支付配置
            string payconfig = PayConfigInfo();
            //验证商户支付配置
            if (payconfig == string.Empty) return "{\"error\":\"商户配置错误\"}";
            ims_payinfo pay = js.Deserialize<ims_payinfo>(payconfig);
            //验证订单号码
            //判断传递进来的类型 ,
            /// <param name="type">产品类型:
            ///                             0 鲜果订单
            ///                             1 果切订单
            using (MltxDbContext db = new MltxDbContext())
            {
                try
                {
                    switch (type)
                    {
                        case 0:
                            Mltx_Order_main main = db.order_m.Where(f => f.Openid == loginid).Where(f => f.OrderId == order).OrderBy(f => f.Openid).FirstOrDefault();
                            if (main.State == State.未预付款)
                            {
                                string PayPrice = ConfigurationManager.AppSettings["PayPrice"];//预付款
                                markprice = float.Parse(PayPrice);
                            }
                            else
                            {
                                markprice = float.Parse(main.OrderTotals.ToString());
                            }
                            marktitle = main.OrderId;
                            break;

                    }
                    //获取当前域名
                    string host = HttpContext.Current.Request.Url.Host.ToString();
                    // 拼接返回支付成功通知链接地址 ,所有微信支付相关方法目录/WeiXinPay/控制器下
                    string url = host.Contains("http://") ? string.Format("{0}/WeiXinPay/paysuccess", host) : string.Format("http://{0}/WeiXinPay/paysuccess", host);
                    //string url = host.Contains("http://") ? string.Format("{0}/WeiXinPay/paysuccess?weid={1}&openid={2}&order={3}", host, wid, openid, order) : string.Format("http://{0}/WeiXinPay/paysuccess?weid={1}&openid={2}&order={3}", host,wid,openid,order);
                    //订单总价
                    //Convert.ToString(float.Parse(ordermain.OrderTotal.ToString()) * 100)
                    string price = Convert.ToString(markprice * 100);

                    //生成sign签名
                    string signcode = SignConfig(type, weid, pay.Appid, pay.Mchid, MD5(weid), marktitle, openid, order, price, url, "127.0.0.1", pay.KeyValue, code);
                    return signcode;
                }
                catch(Exception ex) {
                    Data.MethodHepler.MHepler.writelog_err("WeiXinPay异常：" + ex.Message);
                    return "{\"error\":"+ex.Message+"}";
                }
            }
        }
        /// <summary>
        /// 获取商户支付配置
        /// </summary>
        /// <returns></returns>
        public string PayConfigInfo()
        {
            try
            {
                ims_payinfo payinfo = new ims_payinfo();
                string appid = ConfigurationManager.AppSettings["AppId"];
                string appsecret = ConfigurationManager.AppSettings["Appsecret"];
                string mchid = ConfigurationManager.AppSettings["MchId"];
                string partkey = ConfigurationManager.AppSettings["PartnerKey"];
                payinfo.Appid = appid;
                payinfo.Appsecret = appsecret;
                payinfo.Mchid = mchid;
                payinfo.KeyValue = partkey;

                string result = js.Serialize(payinfo);
                return result;
            }
            catch (Exception e)
            {
                Data.MethodHepler.MHepler.writelog_err("获取商户支付配置错误,错误信息:" + e.ToString());
                return string.Empty;
            }
            
        }
        /// <summary>
        /// 签名配置
        /// </summary>
        /// <param name="key">必备参数</param>
        /// <returns></returns>
        public string SignConfig(int type,string weid , string appid, string mch_id, string nonce_str, string body, string openid, string out_trade_no, string total_fee, string notify_url, string spbill_create_ip, string key, string code = "")
        {

            Data.MethodHepler.MHepler.writelog_err("进入SignConfig");
            if (key == string.Empty || key == null) { Data.MethodHepler.MHepler.writelog_err("主要参数key错误请检查"); return "{\"error\":\"key error\"}"; }
            string json = "{\"weid\":\"" + weid + "\",\"order\":\"" + out_trade_no + "\",\"openid\":\"" + openid + "\",\"type\":" + type + ",\"code\":\"" + code + "\"}";
            //字典序
            Dictionary<string, string> sign = new Dictionary<string, string>();
            try
            {
                sign.Add("appid", appid);
                sign.Add("mch_id", mch_id);
                sign.Add("nonce_str", MD5(weid));
                sign.Add("body", body);
                sign.Add("out_trade_no", out_trade_no);
                sign.Add("total_fee", total_fee);
                sign.Add("spbill_create_ip", spbill_create_ip);
                sign.Add("notify_url", notify_url + "?json=" + HttpUtility.UrlEncode(json));
                sign.Add("trade_type", "JSAPI");
                sign.Add("openid", openid);
                sign.Add("sign", SignS(sign, key));//拼接
            }
            catch (Exception e)
            {
                Data.MethodHepler.MHepler.writelog_err("拼接参数错误" + e.ToString());
                return "{\"error\":\"SignConfig error\"}";
            }
            return PayXML(sign);
        }
        /// <summary>
        /// 拼接支付最终参数
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public string PayConfigSuccess(pCodepay pcodes)
        {
            string json = string.Empty;
            try
            {
                //获取商户支付配置
                string payconfig = PayConfigInfo();
                //验证商户支付配置
                if (payconfig == string.Empty) return "{\"error\":\"商户配置错误\"}";
                ims_payinfo pay = js.Deserialize<ims_payinfo>(payconfig);

                string pid = string.Format("prepay_id={0}", pcodes.prepay_id);
                //字典序,拼接sign
                Dictionary<string, string> sign = new Dictionary<string, string>();
                sign.Add("appId", pcodes.appid);
                sign.Add("timeStamp", Convert.ToString(Data.MethodHepler.MHepler.ConvertDateTimeInt(DateTime.Now)));
                sign.Add("nonceStr", pcodes.nonce_str);
                sign.Add("package", pid);
                sign.Add("signType", "MD5");
                sign.Add("paySign", SignS(sign, pay.KeyValue));
                json = js.Serialize(sign);
            }
            catch (Exception e)
            {
                Data.MethodHepler.MHepler.writelog_err("拼接支付参数失败PayConfigSuccess:" + e.ToString());
                return string.Empty;
            }
            return json;
        }
        /// <summary>
        /// 拼接提交XML格式
        /// </summary>
        /// <param name="sign"></param>
        /// <returns></returns>
        public string PayXML(Dictionary<string, string> sign)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                sb.Append("<xml>");
                foreach (var key in sign.Keys)
                {
                    sb.Append("<" + key + ">" + sign[key] + "</" + key + ">");
                }
                sb.Append("</xml>");
            }
            catch (Exception e)
            {
                Data.MethodHepler.MHepler.writelog_err("拼接XML异常:" + e.ToString());
                return "{\"error\":\"XML error\"}";
            }
            return sb.ToString();
        }
        /// <summary>
        /// 拼接签名
        /// </summary>
        /// <returns></returns>
        public string SignS(Dictionary<string, string> sign, string key)
        {
            string ctr = string.Empty;
            try
            {
                var result = from i in sign orderby i.Key select i;
                foreach (KeyValuePair<string, string> item in result)
                {
                    string title = item.Key;
                    string value = item.Value;
                    ctr += title + "=" + value + "&";
                }
                ctr = ctr.TrimEnd('&');
                ctr += "&key=" + key;
            }
            catch (Exception e)
            {
                Data.MethodHepler.MHepler.writelog_err("拼接键值对签名失败," + e.ToString());
                return "{\"error\":\"SignDictionary error\"}";
            }
            return MD5(ctr).ToUpper();
        }
        /// <summary>
        /// 签名MD5
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static String MD5(String s)
        {
            char[] hexDigits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                'A', 'B', 'C', 'D', 'E', 'F' };
            try
            {

                byte[] btInput = System.Text.Encoding.UTF8.GetBytes(s);
                // 获得MD5摘要算法的 MessageDigest 对象
                System.Security.Cryptography.MD5 mdInst = System.Security.Cryptography.MD5.Create();
                // 使用指定的字节更新摘要
                mdInst.ComputeHash(btInput);
                // 获得密文
                byte[] md = mdInst.Hash;
                // 把密文转换成十六进制的字符串形式
                int j = md.Length;
                char[] str = new char[j * 2];
                int k = 0;
                for (int i = 0; i < j; i++)
                {
                    byte byte0 = md[i];
                    str[k++] = hexDigits[(int)(((byte)byte0) >> 4) & 0xf];
                    str[k++] = hexDigits[byte0 & 0xf];
                }
                return new string(str);//编码转换
            }
            catch (Exception e)
            {
                Data.MethodHepler.MHepler.writelog_err("调用签名md5异常:" + e.Message);
                return null;
            }
        }
        /// <summary>
        /// sign签名类
        /// </summary>
        public class SignCode
        {
            public string appid { get; set; }
            public string mch_id { get; set; }
            public string nonce_str { get; set; }
            public string body { get; set; }
            public string openid { get; set; }
            public string out_trade_no { get; set; }
            public string total_fee { get; set; }
            public string notify_url { get; set; }
            public string spbill_create_ip { get; set; }
        }
        /// <summary>
        /// 获取支付参数
        /// </summary>
        [XmlRoot("xml")]
        public class pCodepay
        {
            [XmlElement]
            public string return_code { get; set; }
            [XmlElement]
            public string return_msg { get; set; }
            [XmlElement]
            public string appid { get; set; }
            [XmlElement]
            public string mch_id { get; set; }
            [XmlElement]
            public string nonce_str { get; set; }
            [XmlElement]
            public string sign { get; set; }
            [XmlElement]
            public string result_code { get; set; }
            [XmlElement]
            public string prepay_id { get; set; }
            [XmlElement]
            public string trade_type { get; set; }
        }
        /// <summary>
        /// 微信支付返回基础数据
        /// </summary>
        public class WeiXinPostJsonData
        {
            public string weid { get; set; }
            public string order { get; set; }
            public string openid { get; set; }
            public int type { get; set; }
            public string code { get; set; }
        }
        /// <summary>
        /// 微信支付查询
        /// </summary>
        [XmlRoot("xml")]
        public class WeiXinSelect
        {
            [XmlElement]
            public string appid { get; set; }
            [XmlElement]
            public string return_msg { get; set; }
            [XmlElement]
            public string return_code { get; set; }
            [XmlElement]
            public string err_code_des { get; set; }
            [XmlElement]
            public string trade_state { get; set; }
            [XmlElement]
            public string result_code { get; set; }
            [XmlElement]
            public string mch_id { get; set; }
            [XmlElement]
            public string nonce_str { get; set; }
            [XmlElement]
            public string sign { get; set; }
            [XmlElement]
            public string err_code { get; set; }
            [XmlElement]
            public string device_info { get; set; }
            [XmlElement]
            public string openid { get; set; }
            [XmlElement]
            public string is_subscribe { get; set; }
            [XmlElement]
            public string trade_type { get; set; }
            [XmlElement]
            public string bank_type { get; set; }
            [XmlElement]
            public int total_fee { get; set; }
            [XmlElement]
            public int coupon_fee { get; set; }
            [XmlElement]
            public string fee_type { get; set; }
            [XmlElement]
            public string transaction_id { get; set; }
            [XmlElement]
            public string out_trade_no { get; set; }
            [XmlElement]
            public string attach { get; set; }
            [XmlElement]
            public string time_end { get; set; }
            [XmlElement]
            public string trade_state_desc { get; set; }

        }
        /// <summary>
        /// 微信支付返回信息
        /// </summary>
        [XmlRoot("xml")]
        public class WeiXinPost
        {
            [XmlElement]
            public string appid { get; set; }
            [XmlElement]
            public string bank_type { get; set; }
            [XmlElement]
            public string cash_fee { get; set; }
            [XmlElement]
            public string fee_type { get; set; }
            [XmlElement]
            public string is_subscribe { get; set; }
            [XmlElement]
            public string json { get; set; }
            [XmlElement]
            public string mch_id { get; set; }
            [XmlElement]
            public string nonce_str { get; set; }
            [XmlElement]
            public string openid { get; set; }
            [XmlElement]
            public string out_trade_no { get; set; }
            [XmlElement]
            public string result_code { get; set; }
            [XmlElement]
            public string return_code { get; set; }
            [XmlElement]
            public string time_end { get; set; }
            [XmlElement]
            public string total_fee { get; set; }
            [XmlElement]
            public string trade_type { get; set; }
            [XmlElement]
            public string transaction_id { get; set; }
        }

    }

    public class ims_payinfo
    {
        public string Appid { get; set; }
        public string Mchid { get; set; }
        public string Appsecret { get; set; }
        public string KeyValue { get; set; }
    }
}