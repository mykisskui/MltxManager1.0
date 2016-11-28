using MltxManager.Models.DBHelper;
using MltxManager.Models.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Webdiyer.WebControls.Mvc;

namespace MltxManager.Data
{
    public class ShopData
    {
        /// <summary>
        /// 获取门店信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="kword"></param>
        /// <returns></returns>
        public ReturnMsg GetShopInfo(int id = 1, string kword = null)
        {
            ReturnMsg rmsg = new ReturnMsg();
            using (MltxDbContext db = new MltxDbContext())
            {
                try
                {
                    var model = db.shopinfo.AsQueryable().Where(s => s.Enable == Enable.激活);
                    if (!string.IsNullOrWhiteSpace(kword))
                    {
                        model = model.Where(s => s.ShopName.Contains(kword));
                    }

                    PagedList<Mltx_ShopInfo> list = model.OrderBy(s=>s.ShopId).ToPagedList(id, 10);

                    rmsg.errcode = 0;
                    rmsg.shopinfolist = list;
                }
                catch (Exception ex)
                {
                    Data.MethodHepler.MHepler.writelog_err("获取门店信息时异常:" + ex.Message);
                    rmsg.errcode = -1;
                    rmsg.errmsg = ex.Message;
                }

                return rmsg;
            }
        }

        /// <summary>
        /// 删除门店信息
        /// </summary>
        /// <param name="shopid">门店号</param>
        /// <returns></returns>
        public string _delShop(int shopid)
        {
            using (MltxDbContext db = new MltxDbContext())
            {
                try
                {
                    Mltx_ShopInfo shop = db.shopinfo.Where(s => s.ShopId == shopid).SingleOrDefault();
                    shop.Enable = Enable.未激活;

                    db.Entry(shop).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    return "0";
                }
                catch(Exception ex)
                {
                    Data.MethodHepler.MHepler.writelog_err("删除门店信息时异常:" + ex.Message);
                    return "error";
                }
            }
        }

        /// <summary>
        /// 编辑或创建门店信息
        /// </summary>
        /// <param name="sinfo">门店信息数据</param>
        /// <param name="addr">省市区信息 用于拼接字符串</param>
        /// <param name="_iscreate">是否是创建 默认true 创建</param>
        /// <returns></returns>
        public string _carateOreditShopinfo(Mltx_ShopInfo sinfo, string addr, bool _iscreate = true)
        {
            using (MltxDbContext db = new MltxDbContext())
            {
                try
                {
                    if (_iscreate)//创建
                    {
                        Mltx_ShopInfo model = new Mltx_ShopInfo();
                        model.ShopName = sinfo.ShopName;
                        model.ShopTel = sinfo.ShopTel;
                        model.ShopAddr = addr + sinfo.ShopAddr;
                        model.Shop_x = sinfo.Shop_x;
                        model.Shop_y = sinfo.Shop_y;
                        model.Enable = Enable.激活;

                        db.shopinfo.Add(model);
                        db.SaveChanges();

                    }
                    else//编辑
                    {
                        Mltx_ShopInfo ss = db.shopinfo.SingleOrDefault(s => s.ShopId == sinfo.ShopId);
                        ss.ShopName = sinfo.ShopName;
                        ss.ShopTel = sinfo.ShopTel;
                        ss.ShopAddr = addr + sinfo.ShopAddr;
                        ss.Shop_x = sinfo.Shop_x;
                        ss.Shop_y = sinfo.Shop_y;
                        ss.Enable = Enable.激活;

                        db.Entry(ss).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }

                    return "0";
                }
                catch (Exception ex)
                {
                    Data.MethodHepler.MHepler.writelog_err("创建编辑门店信息时异常:" + ex.Message);
                    return "error";
                }
            }
        }

        /// <summary>
        /// 根据id获取门店信息
        /// </summary>
        /// <param name="shopid"></param>
        /// <returns></returns>
        public ReturnMsg GetShopInfoByShopid(int shopid)
        {
            ReturnMsg rmsg = new ReturnMsg();
            using (MltxDbContext db = new MltxDbContext())
            {
                try
                {
                    Mltx_ShopInfo shop = db.shopinfo.SingleOrDefault(s => s.ShopId == shopid);

                    rmsg.errcode = 0;
                    rmsg.shopinfo = shop;
                }
                catch (Exception ex)
                {
                    Data.MethodHepler.MHepler.writelog_err("根据id获取门店信息时出错:" + ex.Message);
                    rmsg.errcode = -1;
                    rmsg.errmsg = ex.Message;
                }

                return rmsg;
            }
        }
    }
}