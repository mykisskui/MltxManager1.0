using System;
using System.Linq;
using MltxManager.Data.MethodHepler;
using MltxManager.Models.DBHelper;
using MltxManager.Models.DBHelper.Mode;
using MltxManager.Models.DBModel;
using Webdiyer.WebControls.Mvc;

namespace MltxManager.Data
{
    /// <summary>
    /// 鲜果管理
    /// </summary>
    public class FruitData
    {
        private readonly MltxDbContext _db = new MltxDbContext();
        #region 鲜果分组管理
        /// <summary>
        /// 取出鲜果分组
        /// </summary>
        /// <returns></returns>
        public ReturnMsg GetFreshPacket()
        {
            var re = new ReturnMsg();
            try
            {
                re.MltxGroupFreshes = _db.group_fresh.Where(a => a.Enable == Enable.激活).ToList();
            }
            catch (Exception e)
            {
                re.errcode = -1;
                re.errmsg = e.Message;
                MHepler.writelog_err("鲜果分组显示：" + e.Message);
                return re;
            }
            re.errcode = 0;
            return re;
        }
        /// <summary>
        /// 鲜果分组添加
        /// </summary>
        /// <param name="name">商品名称</param>
        /// <returns></returns>
        public bool InsertFreshPacket(string name)
        {
            try
            {
                var re = new Mltx_Group_fresh
                {
                    GroupName = name,
                    Enable = Enable.激活
                };
                _db.group_fresh.Add(re);
                return _db.SaveChanges() > 0;
                
            }
            catch (Exception e)
            {
                MHepler.writelog_err("鲜果分组添加：" + e.Message);
                return false;
            }
        }
        /// <summary>
        /// 鲜果数据修改
        /// </summary>
        /// <param name="name">修改名称</param>
        /// <param name="id">修改ID</param>
        /// <returns></returns>
        public bool UpdateFreshPacket(string name, int id)
        {
            try
            {
                var re = _db.group_fresh.SingleOrDefault(a => a.GroupId == id);
                if (re != null)
                {
                    re.GroupName = name;
                    return _db.SaveChanges() > 0;
                }
                else
                {
                    MHepler.writelog_err("鲜果分组修改：为查询到数据", false);
                    return false;
                }
            }
            catch (Exception e)
            {
                MHepler.writelog_err("鲜果分组修改：" + e.Message);
                return false;
            }
        }
        /// <summary>
        /// 鲜果分组删除
        /// </summary>
        /// <param name="id">鲜果分组ID</param>
        /// <returns></returns>
        public bool DeletFreshPacket(int id)
        {
            try
            {
                var re = _db.group_fresh.SingleOrDefault(a => a.GroupId == id);
                if (re != null)
                {
                    re.Enable = Enable.未激活;
                    return _db.SaveChanges() > 0;

                }
                else
                {
                    MHepler.writelog_err("鲜果分组删除：为查询到数据", false);
                    return false;
                }
            }
            catch (Exception e)
            {
                MHepler.writelog_err("鲜果分组删除：" + e.Message);
                return false;
            }
        }
        #endregion
        #region 鲜果商品管理
        /// <summary>
        /// 查询所有商品信息
        /// </summary>
        /// <returns></returns>
        public ReturnMsg GetInformation(int id, string kword)
        {
            var re = new ReturnMsg();
            try
            {
                if (!string.IsNullOrEmpty(kword))
                {
                    re.MltxGoodsInfoFreshes =
                        _db.goodsinfo_fresh.Include("Group").Where(a => a.GoodsName.Contains(kword))
                            .OrderByDescending(a => a.GoodsId)
                            .ToPagedList(id, 10);
                }
                else
                {
                    re.MltxGoodsInfoFreshes = _db.goodsinfo_fresh.Include("Group").OrderByDescending(a => a.GoodsId).ToPagedList(id, 10);
                }

            }
            catch (Exception e)
            {
                re.errcode = -1;
                re.errmsg = e.Message;
                MHepler.writelog_err("查询所有鲜果商品信息：" + e.Message);
                return re;
            }
            re.errcode = 0;
            return re;
        }

        /// <summary>
        /// 添加商品信息
        /// </summary>
        /// <param name="name">商品名称</param>
        /// <param name="guige">商品规格</param>
        /// <param name="kuchun">商品库存</param>
        /// <param name="tupian">商品图片</param>
        /// <param name="liebiao">商品列表图片</param>
        /// <param name="fenzu">商品分组ID</param>
        /// <param name="mprice">市场价</param>
        /// <param name="rprice">销售价</param>
        /// <param name="xiangxi">详细信息</param>
        /// <param name="fangs">库存计算方式</param>
        /// <param name="jifen">积分计算方式</param>
        /// <param name="zhuangtai">状态</param>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public int InsertCommodityAdded(string name, string guige, int kuchun, string tupian, string liebiao, int fenzu, decimal rprice, decimal mprice
            , string xiangxi, int fangs, int jifen, int zhuangtai, int userId)
        {
            try
            {
                var r = _db.group_fresh.SingleOrDefault(a => a.GroupId == fenzu);
                var r1 = _db.userinfo.SingleOrDefault(a => a.UserId == userId);
                if (r != null && r1 != null)
                {
                    var re = new Mltx_GoodsInfo_fresh
                    {
                        GoodsName = name,
                        GoodsGuige = guige,
                        GoodsStock = kuchun,
                        GoodsPic = tupian,
                        GoodsListPic = liebiao,
                        GroupId = fenzu,
                        Rprice = rprice,
                        Mprice = mprice,
                        GoodsInfo = xiangxi,
                        StockMethod = (StockMethod)fangs,
                        CTime = DateTime.Now,
                        ETime = DateTime.Now,
                        UserId = userId,
                        Rate = jifen,
                        Enable = (Added)zhuangtai
                    };
                    _db.goodsinfo_fresh.Add(re);
                    return _db.SaveChanges() > 0 ? 0 : -1;

                }
                else
                {
                    MHepler.writelog_err("添加鲜果商品信息：没有分组,或者用户为NULL", false);
                    return 1;
                }
            }
            catch (Exception e)
            {

                MHepler.writelog_err("添加鲜果商品信息：" + e.Message);
                return -1;
            }
        }
        /// <summary>
        /// 删除商品信息
        /// </summary>
        /// <param name="i">商品ID</param>
        /// <param name="r">判断类型</param>
        /// <returns></returns>
        public int DeleteCommodity(int i, int r)
        {
            try
            {
                var re = _db.goodsinfo_fresh.SingleOrDefault(a => a.GoodsId == i);
                if (re != null)
                {
                    re.Enable = r == 0 ? Added.下架 : Added.上架;
                    return _db.SaveChanges() > 0 ? 0 : 1;
                   
                }
                else
                {
                    MHepler.writelog_err("删除商品信息：未查询到商品信息", false);
                    return 1;
                }
            }
            catch (Exception e)
            {
                MHepler.writelog_err("删除商品信息：" + e.Message);
                return 1;

            }
        }
        /// <summary>
        /// 商品修改读取数据
        /// </summary>
        /// <param name="goodId">商品ID</param>
        /// <returns></returns>
        public ReturnMsg UpdateCommodityPage(int goodId)
        {
            var re = new ReturnMsg();
            try
            {
                re.MltxGoodsInfoFresh = _db.goodsinfo_fresh.SingleOrDefault(a => a.GoodsId == goodId);
                re.DoubleBases = _db.group_fresh.Where(a => a.Enable == Enable.激活).Select(a => new DoubleBase
                {
                    Id = a.GroupId,
                    Val = a.GroupName
                }).ToList();
                if (re.MltxGoodsInfoFresh != null)
                {
                    re.errcode = 0;


                    return re;
                }
                else
                {
                    re.errcode = -1;
                    MHepler.writelog_err("商品修改读取数据：未能读取到数据", false);
                    return re;
                }
            }
            catch (Exception e)
            {
                re.errcode = -1;
                re.errmsg = e.Message;
                MHepler.writelog_err("商品修改读取数据：" + e.Message);
                return re;
            }
        }
        /// <summary>
        /// 商品修改
        /// </summary>
        /// <param name="name">商品名称</param>
        /// <param name="guige">商品规格</param>
        /// <param name="kuchun">商品库存</param>
        /// <param name="tupian">商品图片</param>
        /// <param name="mprice">市场价</param>
        /// <param name="rprice">销售价</param>
        /// <param name="liebiao">商品列表图片</param>
        /// <param name="fenzu">商品分组ID</param>
        /// <param name="xiangxi">详细信息</param>
        /// <param name="fangs">库存计算方式</param>
        /// <param name="jifen">积分计算方式</param>
        /// <param name="zhuangtai">状态</param>
        /// <param name="i">用户ID</param>
        /// <param name="groupId">商品ID</param>
        /// <returns></returns>
        public int UpdateCommodityModify(string name, string guige, int kuchun, string tupian, string liebiao, int fenzu, decimal rprice, decimal mprice, string xiangxi, int fangs, int jifen, int zhuangtai, int i, int groupId)
        {
            try
            {
                var re = _db.goodsinfo_fresh.SingleOrDefault(a => a.GoodsId == groupId);
                if (re != null)
                {
                    re.GoodsName = name;
                    re.GoodsGuige = guige;
                    re.GoodsStock = kuchun;
                    re.GoodsPic = tupian;
                    re.GoodsListPic = liebiao;
                    re.GroupId = fenzu;
                    re.GoodsInfo = xiangxi;
                    re.Rprice = rprice;
                    re.Mprice = mprice;
                    re.StockMethod = (StockMethod)fangs;
                    re.ETime = DateTime.Now;
                    re.UserId = i;
                    re.Rate = jifen;
                    re.Enable = (Added)zhuangtai;
                    return _db.SaveChanges() > 0 ? 0 : 1;
                    
                }
                else
                {
                    MHepler.writelog_err("商品修改：未能读取到数据", false);
                    return 1;
                }
            }
            catch (Exception e)
            {
                MHepler.writelog_err("商品修改：" + e.Message);
                throw;
            }
        }
        #endregion


    }
}