﻿using MltxManager.Data.MethodHepler;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.Configuration;
using System.Web.Script.Serialization;

namespace MltxManager.Data
{
    public class wxapi
    {
        private JavaScriptSerializer js = new JavaScriptSerializer();
        private string appid = WebConfigurationManager.AppSettings["AppId"].ToString();
        private string secret = WebConfigurationManager.AppSettings["Appsecret"].ToString();


        
        /// <summary>
        /// 调用方法
        /// </summary>
        /// <returns></returns>
        public jsapi_sdk jssdk() {
            jsapi_sdk sdk = new jsapi_sdk();
            sdk.appId = appid;
            sdk.timestamp = MHepler.ConvertDateTimeInt(DateTime.Now);
            sdk.nonceStr = "FDSJFKLADJKLFDASJKLJFKLSDJFKLSD";
            string URL = HttpContext.Current.Request.Url.AbsoluteUri;
            Access_token token = Get_access_token();
            string signValue = sign(sdk.nonceStr, Get_jsapi_ticket(token.access_token).ticket, sdk.timestamp.ToString(), URL);
            sdk.signature = signValue;
            return sdk;
            
        }
        /// <summary>
        /// 获取access_token
        /// 微信第一步token
        /// </summary>
        /// <returns></returns>
        public Access_token Get_access_token()
        {
            Access_token token = null;
            if (HttpContext.Current.Cache["access_token"] == null)
            {
                string param = string.Format("?grant_type={0}&appid={1}&secret={2}", "client_credential", appid, secret);
                string URL = "https://api.weixin.qq.com/cgi-bin/token" + param;
                token = HttpGet<Access_token>(URL, new Access_token());
                if (token.errcode == 0)
                {
                    HttpContext.Current.Cache.Add("access_token", token, null, Cache.NoAbsoluteExpiration, TimeSpan.FromSeconds(token.expires_in), CacheItemPriority.Normal, null);
                }
            }
            else
            {
                token = HttpContext.Current.Cache.Get("access_token") as Access_token;
            }
            return token;
        }
        /// <summary>
        /// 获取jssdk token
        /// 微信jsapi必备token
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public jsapi_ticket Get_jsapi_ticket(string accessToken) {
            jsapi_ticket jsapi = null;
            if (HttpContext.Current.Cache["jsapi_ticket"] == null)
            {
                string param = string.Format("?access_token={0}&type=jsapi", accessToken);
                string URL = "https://api.weixin.qq.com/cgi-bin/ticket/getticket"+param;
                jsapi = HttpGet<jsapi_ticket>(URL, new jsapi_ticket());
                if (jsapi.errcode == 0) {
                    HttpContext.Current.Cache.Add("jsapi_ticket", jsapi, null, Cache.NoAbsoluteExpiration, TimeSpan.FromSeconds(jsapi.expires_in), CacheItemPriority.Normal, null);
                }
            }
            else {
                jsapi = HttpContext.Current.Cache.Get("jsapi_ticket") as jsapi_ticket;
            }
            return jsapi;
            
        }
        /// <summary>
        /// sign签名
        /// jssdk
        /// </summary>
        /// <returns></returns>
        public string sign(string noncestr,string jstoken,string time,string url) {

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("noncestr", noncestr);
            dic.Add("jsapi_ticket", jstoken);
            dic.Add("timestamp", time);
            dic.Add("url", url);
            string str = string.Empty;
            var result = from i in dic orderby i.Key select i;
            foreach (var item in result) {
                str += item.Key+"="+item.Value+"&";
            }
            str = str.TrimEnd('&');
            return SHA1_Encrypt(str);
        }
        /// <summary>
        /// 对字符串进行SHA1加密
        /// </summary>
        /// <param name="strIN">需要加密的字符串</param>
        /// <returns>密文</returns>
        public string SHA1_Encrypt(string Source_String)
        {
            byte[] StrRes = Encoding.Default.GetBytes(Source_String);
            HashAlgorithm iSHA = new SHA1CryptoServiceProvider();
            StrRes = iSHA.ComputeHash(StrRes);
            StringBuilder EnText = new StringBuilder();
            foreach (byte iByte in StrRes)
            {
                EnText.AppendFormat("{0:x2}", iByte);
            }
            return EnText.ToString();
        }
        /// <summary>
        /// 泛型请求httpget
        /// </summary>
        /// <typeparam name="T">泛型对象</typeparam>
        /// <param name="url">路径</param>
        /// <param name="t">参数</param>
        /// <returns></returns>
        public T HttpGet<T>(string url,T t) {
            string result = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            //request.ContentLength = 0;
            request.Timeout = 20000;
            HttpWebResponse response = null;
            try {
                response = (HttpWebResponse)request.GetResponse();
                Stream str = response.GetResponseStream();
                using (StreamReader reader = new StreamReader(str, Encoding.UTF8))
                {
                    t = js.Deserialize<T>(reader.ReadToEnd());
                }
            }
            catch {
                
            }
            return t;
        }
        /// <summary>
        /// 判断经纬度距离
        /// </summary>
        /// <param name="lng1"></param>
        /// <param name="lat1"></param>
        /// <param name="lng2"></param>
        /// <param name="lat2"></param>
        /// <param name="gs">距离</param>
        /// <returns></returns>
        public static double DistanceOfTwoPoints(double lng1, double lat1, double lng2, double lat2, GaussSphere gs)
        {
            double radLat1 = Rad(lat1);
            double radLat2 = Rad(lat2);
            double a = radLat1 - radLat2;
            double b = Rad(lng1) - Rad(lng2);
            double s = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) +
             Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(b / 2), 2)));
            s = s * (gs == GaussSphere.WGS84 ? 6378137.0 : (gs == GaussSphere.Xian80 ? 6378140.0 : 6378245.0));
            s = Math.Round(s * 10000) / 10000;
            return s;
        }

        private static double Rad(double d)
        {
            return d * Math.PI / 180.0;
        }
        public enum GaussSphere
        {
            Beijing54,
            Xian80,
            WGS84,
        }
    }
    /// <summary>
    /// 微信核心token
    /// </summary>
    public class Access_token {
        /// <summary>
        /// tokne
        /// </summary>
        public string access_token { get; set; }
        /// <summary>
        /// 有效期
        /// </summary>
        public int expires_in { get; set; }
        /// <summary>
        /// 错误代码
        /// </summary>
        public int errcode { get; set; }
        /// <summary>
        /// 错误提示
        /// </summary>
        public string errmsg { get; set; }
    }
    /// <summary>
    /// 微信jssdk token
    /// </summary>
    public class jsapi_ticket {
        public int errcode { get; set; }
        public string errmsg { get; set; }
        public string ticket { get; set; }
        public int expires_in { get; set; }
    }
    /// <summary>
    /// jssdk权限验证
    /// </summary>
    public class jsapi_sdk {
        public string appId { get; set; }
        public int timestamp { get; set; }
        public string nonceStr { get; set; }
        public string signature { get; set; }

    }
}