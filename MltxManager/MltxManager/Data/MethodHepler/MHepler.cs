using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace MltxManager.Data.MethodHepler
{
    public class MHepler
    {
        /// <summary>
        /// 获取字符串的MD5值
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetMD5(string str)
        {
            string pwd = "";
            MD5 md5 = MD5.Create();//实例化一个md5对像
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符

                pwd = pwd + s[i].ToString("X");

            }
            return pwd;
        }

        /// <summary>
        /// 时间戳转为C#格式时间
        /// </summary>
        /// <param name=”timeStamp”></param>
        /// <returns></returns>
        static public DateTime GetTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime); return dtStart.Add(toNow);
        }

        /// <summary>
        /// DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        static public int ConvertDateTimeInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }

        /// <summary>
        /// 记录日志 
        /// </summary>
        /// <param name="msg"></param>
        ///<param name="is_err">是否是异常错误信息 默认是</param>
        public static void writelog_err(string msg, bool is_err = true)
        {
            try
            {
                string directory = MapPath("/logs");
                if (directory != "failed")
                {
                    if (Directory.Exists(directory) == false)//如果不存在就创建file文件夹
                    {
                        Directory.CreateDirectory(directory);
                    }
                }

                //Directory.Delete(Server.MapPath("~/upimg/hufu"), true);//删除文件夹以及文件夹中的子目录，文件    
                string url = "";
                if (is_err)
                {
                     url = MapPath("/logs/logs_err.txt");   //服务器域名
                }
                else
                {
                    url = MapPath("/logs/logs_info.txt");   //服务器域名
                }

                if (url != "failed")
                {
                    msg = DateTime.Now.ToString() + "\t" + msg + "\r\n";
                    //判断文件的存在
                    if (File.Exists(url) == false)
                    {
                        File.AppendAllText(url, msg);
                    }
                    else
                    {
                        File.AppendAllText(url, msg);
                    }
                }
            }
            catch
            {

            }
        }

        public static string MapPath(string strPath)
        {
            string path = "";
            try
            {
                //在多线程里面使用HttpContext.Current,HttpContext.Current是得到null的.
                if (HttpContext.Current != null)
                {
                    return HttpContext.Current.Server.MapPath(strPath);
                }
                else //非web程序引用
                {
                    strPath = strPath.Replace("/", "\\");
                    if (strPath.StartsWith("\\"))
                    {
                        //strPath = strPath.Substring(strPath.IndexOf('\\', 1)).TrimStart('\\');
                        strPath = strPath.TrimStart('\\');
                    }
                    path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
                }
            }
            catch
            {
                path = "failed";
            }
            return path;
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        public static string GetCode()
        {
            int number;
            char code;
            string checkCode = String.Empty;
            Random random = new Random();
            for (int i = 0; i < 6; i++)
            {
                number = random.Next();
                code = (char)('0' + (char)(number % 10));
                checkCode += code.ToString();
            }

            return checkCode;
        }
    }
}