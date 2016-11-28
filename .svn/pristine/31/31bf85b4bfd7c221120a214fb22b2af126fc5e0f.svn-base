using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace MltxManager.Data
{
    public class MallConfig
    {
        /// <summary>
        /// 授权
        /// </summary>
        /// <param name="code">code主要参数</param>
        /// <param name="state">自定义参数</param>
        /// <returns></returns>
        public string OAuth(string code, string url, string state)
        {
            string URL = string.Empty;
            string result = string.Empty;
            try
            {
                string appid = ConfigurationManager.AppSettings["AppId"];
                string appsecret = ConfigurationManager.AppSettings["Appsecret"];

                //查询缓存查看token是否过期
                if (HttpContext.Current.Cache[state] == null || HttpContext.Current.Cache[state].ToString() == string.Empty)
                {
                    //获取token
                    URL = string.Format("https://api.weixin.qq.com/sns/oauth2/access_token" +
                    "?appid={0}&secret={1}&code={2}&grant_type=authorization_code", appid, appsecret, code);
                    Data.MethodHepler.MHepler.writelog_err("记录获取token路径:" + URL);
                }

                try
                {
                    result = PostAndGet.GetResponseString(URL);
                }
                catch
                {
                    return "1|发送请求路径错误";
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                AccessToken token = js.Deserialize<AccessToken>(result);
                if (token.errcode == 0)
                {
                    //保存accesstoken到cache
                    //HttpContext.Current.Cache.Add(state, token.refresh_token, null, DateTime.Now.AddSeconds(7000), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Normal, null);
                    //跳转获取到的url
                    string decodeurl = HttpUtility.UrlDecode(url);
                    decodeurl = decodeurl.IndexOf('?') == -1 ? string.Format("{0}?openid={1}", decodeurl, token.openid) : string.Format("{0}&openid={1}", decodeurl,token.openid);
                    Data.MethodHepler.MHepler.writelog_err("授权最终跳转链接" + decodeurl);
                    return "0|" + decodeurl;
                }
                else
                {
                    return "1|" + token.errmsg;
                }
            }
            catch
            {
                return "1|config Error";
            }
            
        }

        /// <summary>
        /// 获取授权路径
        /// </summary>
        /// <param name="appid">公众号标识</param>
        /// <param name="codes">传递weid</param>
        /// <param name="url">授权成功后重定向链接地址</param>
        /// <returns></returns>
        public string OAuthURL(string appid, string codes, string url)
        {
            StringBuilder sb = new StringBuilder();
            string http = string.Empty;
            try
            {
                http = HttpContext.Current.Request.Url.Host.ToString();//域名
            }
            catch (Exception e)
            {//获取域名错误
                return "1|" + e.ToString();
            }
            http = !http.Contains("http://") ? http = "http://" + http : http;
            string OAuths = "/Mltx_MallClass/OAuthToken";//处理路径 

            http = string.Format("{0}{1}?url={2}", http, OAuths, HttpUtility.UrlEncode(url));
            sb.Append("https://open.weixin.qq.com/connect/oauth2/authorize");
            sb.Append(string.Format("?appid={0}", appid));
            sb.Append(string.Format("&redirect_uri={0}", http));//不需要urlencode编码
            sb.Append(string.Format("&response_type={0}", "code"));
            sb.Append(string.Format("&scope={0}", "snsapi_base"));
            sb.Append("#wechat_redirect");
            //sb.Append(string.Format("&state={0}#wechat_redirect", codes));//自定义参数

            return sb.ToString();
        }
    }

    /// <summary>
    /// 获取授权accesstoken类
    /// </summary>
    public class AccessToken
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string refresh_token { get; set; }
        public string openid { get; set; }
        public string scope { get; set; }
        public string unionid { get; set; }
        public int errcode { get; set; }
        public string errmsg { get; set; }
    }
}