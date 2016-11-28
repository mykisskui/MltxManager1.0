using MltxManager.Models.DBHelper;
using MltxManager.Models.DBModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Webdiyer.WebControls.Mvc;

namespace MltxManager.Data
{
    public class MemberData
    {

        MltxDbContext db = new MltxDbContext();
        ReturnMsg msg = new ReturnMsg();


        /// <summary>
        /// 会员信息
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ReturnMsg Info(string search = "", string key = "", int page = 1)
        {

            try {
                string rx = string.Empty;
                PagedList<Mltx_MemberInfo> infos = new List<Mltx_MemberInfo>() as PagedList<Mltx_MemberInfo>;
                if (!string.IsNullOrEmpty(search) && !string.IsNullOrEmpty(key))
                {
                    //存在关键字
                    switch (key) { 
                        case "0":
                            infos = db.memberinfo.Where(f => f.Tel.Contains(search)).OrderByDescending(f=>f.Id).ToPagedList(page,10);
                            break;
                        case "1":
                            int UID = int.Parse(search);
                            infos = db.memberinfo.Where(f=>f.UID == UID).OrderByDescending(f => f.Id).ToPagedList(page, 10);
                            break;
                    }
                }
                else
                {
                    infos = db.memberinfo.OrderByDescending(f => f.Id).ToPagedList(page, 10);
                }
                msg.MemberInfos = infos;
                msg.errcode = 0;
            }
            catch {
                msg.errcode = 1;
                msg.errmsg = string.Format("{0}", "查询会员信息错误,请刷新重试");
            }
            return msg;
        }
        /// <summary>
        /// 编辑会员信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ReturnMsg Info(int id = 0 ) {
            Mltx_MemberInfo info = new Mltx_MemberInfo();
            try {
                info = db.memberinfo.Where(f => f.Id == id).FirstOrDefault();
                msg.MemberInfo = info;
            }
            catch {
                msg.errcode = 1;
                msg.errmsg = string.Format("{0}","编辑会员信息错误,请重新尝试");
            }
            return msg;
        }
        /// <summary>
        /// 执行编辑会员信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ReturnMsg IndexEdit(Mltx_MemberInfo data) {

            try {
                Mltx_MemberInfo info = db.memberinfo.Where(f => f.Id == data.Id).FirstOrDefault();
                if (info == null)
                {
                    msg.errcode = 1;
                    msg.errmsg = string.Format("{0}", "会员信息不存在");
                }
                else
                {
                    int k = 0;
                    Dictionary<string, int> dic = new Dictionary<string, int>();
                    StringBuilder sb = new StringBuilder();

                    if (data.Balance.ToString().IndexOf('+') != -1 || data.Balance != 0 && data.Balance.ToString().IndexOf('-') == -1)
                        {
                            info.Balance = info.Balance + decimal.Parse(data.Balance.ToString().TrimStart('+'));
                            dic.Add("money", 0);
                        }
                        else if (data.Balance.ToString().IndexOf('-') != -1)
                        {
                            if (info.Balance >= decimal.Parse(data.Balance.ToString().TrimStart('-')))
                            {
                                info.Balance = info.Balance - decimal.Parse(data.Balance.ToString().TrimStart('-'));
                                info.Consume = info.Consume + decimal.Parse(data.Balance.ToString().TrimStart('-'));
                                info.Consumenumber = info.Consumenumber + 1;
                                dic.Add("money", 1);
                            }
                            else
                            {
                                k = -1;
                                sb.Append(string.Format("{0}", "余额"));
                            }
                        }

                    if (data.Integral.ToString().IndexOf('+') != -1 || data.Integral != 0 && data.Integral.ToString().IndexOf('-') == -1)
                        {
                            info.Integral = info.Integral + int.Parse(data.Integral.ToString().TrimStart('+'));
                            dic.Add("integal", 0);
                        }
                        else if (data.Integral.ToString().IndexOf('-') != -1)
                        {
                            if (info.Integral >= int.Parse(data.Integral.ToString().TrimStart('-')))
                            {
                                info.Integral = info.Integral - int.Parse(data.Integral.ToString().TrimStart('-'));
                                dic.Add("integal", 1);
                            }
                            else
                            {
                                if (k == -1)
                                {
                                    sb.Append(string.Format("{0}", "及积分"));
                                }
                                else {
                                    sb.Append(string.Format("{0}", "积分"));
                                }
                                k = -2;
                            }
                        }
                        if (k == -1 || k == -2) { 
                            sb.Append(string.Format("{0}","不足,请重新操作"));
                            msg.errcode = 1;
                            msg.errmsg = sb.ToString();
                            return msg;
                        }
                    info.MemberName = data.MemberName;

                    string UserID = HttpContext.Current.Session["User"] == null ? "admin" : HttpContext.Current.Session["User"].ToString();
                    MethodHepler.MHepler.writelog_err(string.Format("管理员编号{0}修改了会员{1}信息", UserID, info.Id));
                    db.SaveChanges();
                                    
                    //记录明细
                    MemberLog(info, dic, data.Balance, data.Integral);
                    //更新会员等级
                    MemberLevelEdit(info);
                            
                    msg.errcode = 0;
                    msg.errmsg = string.Format("{0}", "保存成功");
                }

      

            }
            catch {
                msg.errcode = 1;
                msg.errmsg = string.Format("{0}", "编辑会员信息错误,请刷新重试");
            }
            return msg;
        }

        /// <summary>
        /// 会员明细查询
        /// </summary>
        /// <returns></returns>
        public ReturnMsg Log(int page = 1,string search="",string key="") {
            try {
                PagedList<Mltx_MemberLog> logs = new List<Mltx_MemberLog>() as PagedList<Mltx_MemberLog>;
                if (!string.IsNullOrEmpty(search) && !string.IsNullOrEmpty(key))
                {
                    switch (key)
                    {
                        case "0":

                                //手机号码
                            logs = db.memberlog.Where(f => f.Tel.Contains(search)).OrderByDescending(f => f.Id).ToPagedList(page, 10);

                            break;
                        case "1":
                            int uid = int.Parse(search);
                            logs = db.memberlog.Where(f => f.UID == uid).OrderByDescending(f => f.Id).ToPagedList(page, 10);
                            break;
                    }
                }
                else { 
                    //null
                    logs = db.memberlog.OrderByDescending(f => f.Id).ToPagedList(page, 10);
                }
                msg.errcode = 0;
                msg.MemberLogs = logs;
            }
            catch {
                msg.errcode = 1;
                msg.errmsg = string.Format("{0}", "查询会员明细错误,请刷新重试");
            }
            return msg;
        }
        /// <summary>
        /// 会员等级
        /// </summary>
        /// <param name="id"></param>
        /// <param name="_config"></param>
        /// <returns></returns>
        public ReturnMsg Level(int id = 1,string _config = "") {
            try {
                Mltx_MemberRank level = db.memberrank.Where(f => f.Id == id).FirstOrDefault();
                msg.Level_n = level;
                msg.errcode = 0;
            }
            catch {
                msg.errcode = 1;
                msg.errmsg = string.Format("{0}", "编辑会员等级错误,请刷新重试");
            }
            return msg;
        }
        /// <summary>
        /// 会员等级查询
        /// </summary>
        /// <returns></returns>
        public ReturnMsg Level(int page = 1) {
            try
            {
                PagedList<Mltx_MemberRank> levels = db.memberrank.OrderByDescending(d=>d.Id).ToPagedList(page,10);
                msg.Level = levels;
                msg.errcode = 0;
            }
            catch {
                msg.errcode = 1;
                msg.errmsg = string.Format("{0}", "查询会员等级错误,请刷新重新尝试");
            }
            return msg;
            
        }
        public ReturnMsg LevelRemove(int[] data) {

            try {
                IEnumerable<Mltx_MemberRank> ranks = db.memberrank.Where(f=>data.Contains(f.Id));
                db.memberrank.RemoveRange(ranks);
                if (db.SaveChanges() > 0)
                {
                    msg.errcode = 0;
                    msg.errmsg = string.Format("{0}", "删除成功");
                }
                else {
                    msg.errcode = 1;
                    msg.errmsg = string.Format("{0}", "会员等级删除失败,请刷新重试");
                }

            }
            catch {
                msg.errcode = 1;
                msg.errmsg = string.Format("{0}", "会员等级删除失败,请刷新重试");
            }
            return msg;
        }
        public ReturnMsg LogRemove(int[] data) {

            try
            {
                IEnumerable<Mltx_MemberLog> ranks = db.memberlog.Where(f => data.Contains(f.Id));
                db.memberlog.RemoveRange(ranks);
                if (db.SaveChanges() > 0)
                {
                    msg.errcode = 0;
                    msg.errmsg = string.Format("{0}", "删除成功");
                }
                else
                {
                    msg.errcode = 1;
                    msg.errmsg = string.Format("{0}", "会员明细删除失败,请刷新重试");
                }

            }
            catch
            {
                msg.errcode = 1;
                msg.errmsg = string.Format("{0}", "会员等级删除失败,请刷新重试");
            }
            return msg;
        }
        /// <summary>
        ///  会员等级添加编辑
        /// </summary>
        /// <returns></returns>
        public  ReturnMsg LevelAdd(LevelAddcs data) {
            try {
                Mltx_MemberRank rank = db.memberrank.Where(f => f.Id == data.id).FirstOrDefault();
                if (rank == null)
                {
                    rank = new Mltx_MemberRank();
                    rank.Name = data.levelname;
                    rank.Value = data.levelset;
                    rank.Discount = data.leveldiscount;
                    rank.Time = DateTime.Now;
                    db.memberrank.Add(rank);
                }
                else{
                    rank.Name = data.levelname;
                    rank.Value = data.levelset;
                    rank.Discount = data.leveldiscount;
                    rank.Time = DateTime.Now;
                }
                if (db.SaveChanges() > 0) {
                    msg.errcode = 0;
                    msg.errmsg = string.Format("{0}", "保存成功");
                }
             }
            catch {
                msg.errcode = 1;
                msg.errmsg = string.Format("{0}", "服务器出现故障,请刷新重新尝试");
            }
            return msg;

        }
        /// <summary>
        /// 会员状态变更
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ReturnMsg StatusEdit(int data) {

            try {
                Mltx_MemberInfo infos = db.memberinfo.Where(f => f.Id == data).FirstOrDefault();
                if (infos == null)
                {
                    msg.errcode = 1;
                    msg.errmsg = string.Format("{0}", "会员信息不存在");
                }
                else {
                    infos.Enable = infos.Enable == 0 ? 1 : 0;
                }

                if (db.SaveChanges() > 0) {
                    msg.errcode = 0;
                    msg.errmsg = string.Format("{0}", "操作成功");
                }
            }
            catch {
                msg.errcode = 1;
                msg.errmsg = string.Format("{0}", "会员状态变更失败,请刷新重试");
            }

            return msg;


        }
        /// <summary>
        /// 会员明细添加
        /// </summary>
        /// <param name="info">集合</param>
        /// <param name="dic">需修改的键值对</param>
        /// <param name="integalvalue">值</param>
        /// <param name="moneyvalue">值</param>
        /// <returns></returns>
        public void MemberLog(Mltx_MemberInfo info,Dictionary<string,int> dic,decimal moneyvalue,int integalvalue) {

            Mltx_MemberLog log = null;
            try {

                foreach (var item in dic) {
                    log = new Mltx_MemberLog();

                    log.UID = info.UID;
                    log.Openid = info.Openid;
                    log.Tel = info.Tel;
                    log.Time = DateTime.Now;
                    if (item.Key == "money") {
                        log.Value = moneyvalue;
                        if (item.Value == 0)
                        { //+
                            /*
                                明细类型
                             * 0 增加积分
                             * 1 消费积分
                             * 2 余额消费
                             * 3 增加余额
                             */
                            log.Type = 3;
                            log.Way = "增加余额";
                        }
                        else
                        { //-
                            log.Type = 2;
                            log.Way = "使用余额";
                        }
                    }
                    else if (item.Key == "integal") {
                        log.Value = integalvalue;
                        if (item.Value == 0)
                        { //+
                            /*
                                明细类型
                             * 0 增加积分
                             * 1 消费积分
                             * 2 余额消费
                             * 3 增加余额
                             */
                            log.Type = 0;
                            log.Way = "增加积分";
                        }
                        else
                        { //-
                            log.Type = 1;
                            log.Way = "使用积分";
                        }
                    }



                    db.memberlog.Add(log);
                }

                db.SaveChanges();
            
            }
            catch {
                MethodHepler.MHepler.writelog_err(string.Format("添加会员明细错误,会员编号{0}",info.UID));
            }

        }
        /// <summary>
        /// 更新会员等级
        /// </summary>
        public void MemberLevelEdit(Mltx_MemberInfo info) {


            try {
                //查看会员消费值
                decimal money = info.Consume;
                decimal logMoney = 0;
                int start = 0; int end = 0;
                List<Mltx_MemberRank> lelvellog = new List<Mltx_MemberRank>();
                List<Mltx_MemberRank> level = db.memberrank.OrderByDescending(f=>f.Value).ToList();
                //查看当前会员等级积分
                foreach (var item in level) {
                    start = MatchStartEnd(item.Value)[0];
                    end = MatchStartEnd(item.Value)[1];
                    if (start <= money && money < end) {
                        lelvellog.Add(item);
                    }
                    
                }
                level = lelvellog.OrderByDescending(f => f.Value).ToList();
                Dictionary<int, decimal> dic = new Dictionary<int, decimal>();
                lelvellog = new List<Mltx_MemberRank>();
                foreach (var item in level) {
                    Mltx_MemberRank rank = new Mltx_MemberRank();
                    rank.Id = item.Id;
                    rank.Value = Convert.ToString(MatchStartEnd(item.Value)[1] - money);
                    lelvellog.Add(rank);
                }
                var result = lelvellog.OrderBy(f => f.Value).FirstOrDefault();
                info.RankId = result.Id;

                db.SaveChanges();
            }
            catch { 
            
            }
        }
        public int[] MatchStartEnd(string value) {
            int[] result = new int[2];
            Regex re_start = new Regex(@"[0-9]+-");//前面的值
            Regex re_end = new Regex(@"-[0-9]+$");//后面的值
            MatchCollection match_start = re_start.Matches(value);
            MatchCollection match_end = re_end.Matches(value);
            result[0] = int.Parse(match_start[0].Value.TrimEnd('-'));
            result[1] = int.Parse(match_end[0].Value.TrimStart('-'));
            return result;
        }




        public class LevelAddcs {
            public int id { get; set; }
            public string levelname { get; set; }
            public string levelset { get; set; }
            public int leveldiscount { get; set; }
        }
    }
}