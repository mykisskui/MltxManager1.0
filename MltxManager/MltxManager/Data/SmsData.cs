using MltxManager.Models.DBHelper;
using MltxManager.Models.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MltxManager.Data
{
    public class SmsData
    {
        /// <summary>
        /// 获取短信配置信息
        /// </summary>
        /// <returns></returns>
        public ReturnMsg GetSmsInfo()
        {
            ReturnMsg rmsg = new ReturnMsg();
            using (MltxDbContext db = new MltxDbContext())
            {
                try
                {
                    Mltx_SmsInfo sinfo = db.smsinfo.SingleOrDefault();
                    if (sinfo == null)
                    {
                        rmsg.errcode = 1;
                        rmsg.errmsg = "NULL";
                    }
                    else
                    {
                        rmsg.errcode = 0;
                        rmsg.smsinfo = sinfo;
                    }
                }
                catch(Exception ex)
                {
                    Data.MethodHepler.MHepler.writelog_err("获取短息配置信息时异常:" + ex.Message);
                    rmsg.errcode = -1;
                    rmsg.errmsg = ex.Message;
                }

                return rmsg;
            }
        }

        /// <summary>
        /// 注册会员 信息
        /// </summary>
        /// <param name="phoneno"></param>
        /// <param name="usercode">验证码</param>
        /// <param name="openid">微信标识 可空</param>
        /// <returns></returns>
        public string AddOrUpdateMemberInfo(string phoneno, string usercode,string pwd,string openid)
        {

           
            using (MltxDbContext db = new MltxDbContext())
            {
                try
                {
                    int cnt = GetMemberByPhoneNo(phoneno);
                    Data.MethodHepler.MHepler.writelog_err("count:" + cnt, false);

                    if (cnt == -1)//异常
                    {
                        return "error";
                    }
                    else if (cnt == 0)//新增
                    {
                        Mltx_MemberInfo mm = new Mltx_MemberInfo();
                        string uid = Data.MethodHepler.MHepler.ConvertDateTimeInt(DateTime.Now).ToString().Substring(4,6);
                        Random r = new Random();
                        int _uid = int.Parse(uid + r.Next(100, 999).ToString());
                        mm.UID = _uid;
                        mm.Password = Data.MethodHepler.MHepler.GetMD5("111111");//获取验证码时默认密码111111  确认注册后修改为pwd
                        mm.Tel = phoneno;
                        mm.sex = 2;//性别未知
                        mm.Birthday = DateTime.Parse("1970-01-01");
                        mm.Address = "";
                        mm.Balance = 0;
                        mm.Consume = 0;
                        mm.Consumenumber = 0;
                        mm.Integral = 0;
                        mm.Openid = "";
                        mm.Time = DateTime.Now;
                        mm.Enable = 0;//未激活状态
                        mm.RankId = db.memberrank.FirstOrDefault().Id;
                        mm.UserCode = usercode;//验证码
                        mm.UserPic = "";

                        db.memberinfo.Add(mm);
                        db.SaveChanges();
                        return "0";
                    }
                    else if (cnt == 1) //已有 更新状态可用
                    {
                        Mltx_MemberInfo mmm = db.memberinfo.Where(m => m.Tel == phoneno).SingleOrDefault();
                        mmm.UserCode = usercode;//验证码
                        mmm.Password = Data.MethodHepler.MHepler.GetMD5(pwd);
                        mmm.Enable = 1;

                        db.Entry(mmm).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        return "0";
                    }
                    else if (cnt == -1)
                    {
                        return "-1";
                    }
                    else if (cnt == 2)//超时
                    {
                        return "";
                    }
                    else
                    {
                        return "error";
                    }


                }
                catch (Exception ex)
                {
                    Data.MethodHepler.MHepler.writelog_err("新增会员时异常:" + ex.Message);
                    return "error";
                }
            }
        }

        /// <summary>
        /// 判断此手机号是否已注册到会员表
        /// </summary>
        /// <param name="phoneno"></param>
        /// <returns></returns>
        public static int GetMemberByPhoneNo(string phoneno)
        {
            using (MltxDbContext db = new MltxDbContext())
            {
                try
                {
                    Mltx_MemberInfo minfo = db.memberinfo.Where(m => m.Tel == phoneno).SingleOrDefault();
                    if (minfo == null)
                    {
                        return 0;//没有表示新增
                    }
                    else
                    {
                        if (minfo.Enable == 0)//已获取验证码，需要激活
                        {
                            if ((DateTime.Compare(minfo.Time.AddMinutes(5), DateTime.Now) < 0))//超过5分钟了，重新获取code
                            {
                                return 2;
                            }
                            else//没超时
                            {
                                return 1;
                            }
                        }
                        else {
                            return -1;//该手机号已经注册过了
                        }
                    }
                }
                catch (Exception ex)
                {
                    Data.MethodHepler.MHepler.writelog_err("判断手机号是否注册时异常:" + ex.Message);
                    return -1;
                }
            }
        }

        /// <summary>
        /// 获取会员信息
        /// </summary>
        /// <param name="phoneno"></param>
        /// <returns></returns>
        public ReturnMsg GetMemberInfo(string phoneno)
        {
            ReturnMsg rmsg = new ReturnMsg();
            using (MltxDbContext db = new MltxDbContext())
            {
                try
                {
                    Mltx_MemberInfo minfo = db.memberinfo.Where(m => m.Tel == phoneno).SingleOrDefault();

                    rmsg.errcode = 0;
                    rmsg.MemberInfo = minfo;
                }
                catch (Exception ex)
                {
                    Data.MethodHepler.MHepler.writelog_err("接口获取会员信息异常:" + ex.Message);
                    rmsg.errcode = -1;
                    rmsg.errmsg = ex.Message;
                }

                return rmsg;
            }
        }

    }
}