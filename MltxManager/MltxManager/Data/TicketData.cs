﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MltxManager.Data.MethodHepler;
using MltxManager.Models.DBHelper;
using MltxManager.Models.DBModel;
using Webdiyer.WebControls.Mvc;
using MltxManager.Data.MethodHepler;
using WebGrease.Css.Extensions;

namespace MltxManager.Data
{
    public class TicketData
    {

        private readonly MltxDbContext _db = new MltxDbContext();
        #region 提货卷管理页面
        /// <summary>
        /// 查询提货券
        /// </summary>
        /// <param name="id"></param>
        /// <param name="kword"></param>
        /// <returns></returns>
        public ReturnMsg InsertTicket(int id, string kword)
        {
            var re = new ReturnMsg();
            try
            {
                if (!string.IsNullOrEmpty(kword))
                {
                    re.MltxTickets =
                        _db.tickets.Include("Goods").Where(a => a.Goods.GoodsName.Contains(kword))
                            .OrderByDescending(a => a.Id)
                            .ToPagedList(id, 10);
                }
                else
                {
                    re.MltxTickets = _db.tickets.Include("Goods").OrderByDescending(a => a.Id).ToPagedList(id, 10);
                }

            }
            catch (Exception e)
            {
                re.errcode = -1;
                re.errmsg = e.Message;
                MHepler.writelog_err("查询提货券：" + e.Message);
                return re;
            }
            re.errcode = 0;
            return re;
        }
        /// <summary>
        /// 添加提货卷数据
        /// </summary>
        /// <param name="sp">商品ID</param>
        /// <param name="sj">时间</param>
        /// <param name="sm">数目</param>
        /// <returns></returns>
        public bool InsertTicketData(int sp, string sj, int sm)
        {
            try
            {
                var re = new List<Mltx_Ticket>();
                var strDateTimeNumber = MHepler.ConvertDateTimeInt(DateTime.Now).ToString();
                var strRandomResult = NextRandom(10000, 4, sm);
                var st = strRandomResult.Split(',');
                for (var i = 0; i < sm; i++)
                {
                    var xh = strDateTimeNumber + st[i];
                    var ticket = new Mltx_Ticket
                    {
                        GoodsId = sp,
                        Pwd = "1",
                        State = Card.未发放,
                        Time = DateTime.Parse(sj),
                        TicketId = xh,
                    };
                    re.Add(ticket);

                }
                _db.tickets.AddRange(re);
                return _db.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                MHepler.writelog_err("提货券生成：" + e.Message);
                return false;
            }
        }
        /// <summary>
        /// 修改提货卷状态
        /// </summary>
        /// <param name="id">提货卷 ID</param>
        /// <param name="zt">修改状态</param>
        /// <returns></returns>
        public bool UpdateForetaste(string id, int zt)
        {
            try
            {
                var r = id.Split(',');
                for (var i = 0; i < r.Length - 1; i++)
                {
                    var rd = int.Parse(r[i]);
                    var c = _db.tickets.SingleOrDefault(a => a.Id == rd);
                    if (c != null) c.State = (Card)zt;
                }
                return _db.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                MHepler.writelog_err("提货券生成：" + e.Message);
                return false;
            }
            
        }
        /// <summary>
        /// 查询单个提货券是否有
        /// </summary>
        /// <param name="id">提货券号</param>
        /// <returns></returns>
        public Mltx_Ticket UpdateTicketData(string id)
        {
            try
            {
                var re = _db.tickets.Include("Goods").SingleOrDefault(a => a.TicketId == id&&a.State!=Card.已验证使用);
                return re;
            }
            catch (Exception e)
            {
                MHepler.writelog_err("提货券生成：" + e.Message);
                return null;
                throw;
            }
        }
        /// <summary>
        /// 提货券修改单个状态
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        public bool UpdateTicket(int id)
        {
            try
            {
                var re = _db.tickets.SingleOrDefault(a => a.Id == id);
                if (re != null) re.State=Card.已验证使用;
                return _db.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                MHepler.writelog_err("提货券修改单个状态：" + e.Message);
                return false;
                throw;
            }
           

        }
        #endregion
        #region
        /// <summary>
        /// 四位随机数生成
        /// </summary>
        /// <param name="numSeeds">最大值</param>
        /// <param name="length">长度</param>
        /// <param name="cd">个数</param>
        /// <returns></returns>
        private string NextRandom(int numSeeds, int length, int cd)
        {
            // Create a byte array to hold the random value.  
            byte[] randomNumber = new byte[length];
            var shu = "";
            // Create a new instance of the RNGCryptoServiceProvider.  
            for (int y = 0; y < cd; y++)
            {
                System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
                // Fill the array with a random value.  
                rng.GetBytes(randomNumber);
                // Convert the byte to an uint value to make the modulus operation easier.  
                uint randomResult = 0x0;
                for (int i = 0; i < length; i++)
                {
                    randomResult |= ((uint)randomNumber[i] << ((length - 1 - i) * 8));
                }
                var re = ((randomResult % numSeeds) + 1).ToString();
                if (int.Parse(re) < 10)
                {
                    re = "000" + re;
                }
                else if (int.Parse(re) < 100)
                {
                    re = "00" + re;
                }
                else if (int.Parse(re) < 1000)
                {
                    re = "0" + re;
                }

                if (!shu.Contains(re))
                {
                    shu = re + "," + shu;

                }
                else
                {
                    y = y - 1;
                }
            }
            return shu;
        }
        #endregion

        
    }
}