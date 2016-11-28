using MltxManager.Controllers;
using MltxManager.Models.DBHelper;
using MltxManager.Models.DBHelper.Mode;
using MltxManager.Models.DBModel;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace MltxManager.Data
{
    //接口程序数据获取方法
    public class InterfaceData
    {
        #region 商品信息
        /// <summary>
        /// 根据商品id获取商品信息
        /// </summary>
        /// <param name="goodsid"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public ReturnMsg _getGoodsinfobyGid(int goodsid, string tag)
        {
            ReturnMsg rmsg = new ReturnMsg();
            using (MltxDbContext db = new MltxDbContext())
            {
                try
                {
                    if (tag == "0")
                    {
                        Mltx_GoodsInfo_shop ginfo_s = db.goodsinfo_shop.Where(g => g.GoodsId == goodsid).SingleOrDefault();

                        rmsg.errcode = 0;
                        rmsg.MltxGoodsInfoShop = ginfo_s;
                    }
                    else
                    {
                        Mltx_GoodsInfo_fresh ginfo_f = db.goodsinfo_fresh.Where(g => g.GoodsId == goodsid).SingleOrDefault();

                        rmsg.errcode = 0;
                        rmsg.MltxGoodsInfoFresh = ginfo_f;
                    }
                }
                catch (Exception ex)
                {
                    Data.MethodHepler.MHepler.writelog_err("接口-根据gid获取商品信息时异常:" + ex.Message);
                    rmsg.errcode = -1;
                    rmsg.errmsg = ex.Message;
                }

                return rmsg;
            }
        }
       
        /// <summary>
        /// 根据分组id获取商品信息 分页
        /// </summary>
        /// <param name="groupid">分组id</param>
        /// <param name="page">第几页</param>
        /// <param name="num">每页条数</param>
        /// <param name="orderbyway">排序 0 价格降序 1 价格升序</param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public ReturnMsg _getGoodsinfoListByGroupid(string groupid, string page, string num, string orderbyway,string tag)
        {
            ReturnMsg rmsg = new ReturnMsg();
            int n = int.Parse(num);//每页数据条数
            int p = int.Parse(page);//页数
            int cnt = n * p;//跳过数据条数
            using (MltxDbContext db = new MltxDbContext())
            {
                try
                {
                    List<GoodsBase> list = new List<GoodsBase>();
                    IQueryable<Mltx_GoodsInfo_shop> list_s;
                    IQueryable<Mltx_GoodsInfo_fresh> list_f; 
                    if(tag == "0")//商城
                    {
                        if (!string.IsNullOrEmpty(groupid))//按分组查
                        {
                            int gid = int.Parse(groupid);
                            list_s = db.goodsinfo_shop.Where(g => g.GroupId == gid);
                        }
                        else//查询全部
                        {
                            list_s = db.goodsinfo_shop;                           
                        }
                        //排序
                        if (orderbyway == "0")
                        {
                            list_s = list_s.OrderByDescending(l => l.Rprice);
                        }
                        else if (orderbyway == "1")
                        {
                            list_s = list_s.OrderBy(l => l.Rprice);
                        }

                        list = list_s.Skip(cnt).Take(n).Select(a => new GoodsBase
                        {
                            GoodsId = a.GoodsId,
                            GoodsName = a.GoodsName,
                            GoodsPic = a.GoodsListPic,
                            GoodsGuige = a.GoodsGuige,
                            Rprice = a.Rprice
                        }).ToList();
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(groupid))//按分组查
                        {
                            int gid = int.Parse(groupid);
                            list_f = db.goodsinfo_fresh.Where(g => g.GroupId == gid);

                        }
                        else
                        {
                            list_f = db.goodsinfo_fresh;                            
                        }

                        //排序
                        if (orderbyway == "0")
                        {
                            list_f = list_f.OrderByDescending(l => l.Rprice);
                        }
                        else if(orderbyway == "1")
                        {
                            list_f = list_f.OrderBy(l => l.Rprice);
                        }

                        list = list_f.Skip(cnt).Take(n).Select(a => new GoodsBase
                        {
                            GoodsId = a.GoodsId,
                            GoodsName = a.GoodsName,
                            GoodsPic = a.GoodsListPic,
                            GoodsGuige = a.GoodsGuige,
                            Rprice = a.Rprice
                        }).ToList();
                    }

                    rmsg.errcode = 0;
                    rmsg.goodsbaseList = list;
                }
                catch (Exception ex)
                {
                    Data.MethodHepler.MHepler.writelog_err("接口-根据分组id获取商品信息 分页:" + ex.Message);
                    rmsg.errcode = -1;
                    rmsg.errmsg = ex.Message;
                }

                return rmsg;
            }
        }

        #endregion

        #region 订单信息
        /// <summary>
        /// 根据订单号获取订单详情
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public ReturnMsg _getOrderdetailsByoid(string orderid)
        {
            ReturnMsg rmsg = new ReturnMsg();
            using (MltxDbContext db = new MltxDbContext())
            {
                try
                {
                    List<Mltx_Order_sub> orderlist = db.order_s.Include("OrderId").Where(o => o.OrderId_id == orderid).ToList();

                    StringBuilder sb = new StringBuilder();
                    sb.Append("{");
                    sb.Append("\"details\":[");
                    foreach (Mltx_Order_sub item in orderlist)
                    {
                        string guige = (item.OrderId.OrderCls == 0 ? db.goodsinfo_shop.Where(s => s.GoodsId == item.GoodsId).FirstOrDefault().GoodsGuige : db.goodsinfo_fresh.Where(s => s.GoodsId == item.GoodsId).FirstOrDefault().GoodsGuige);
                        sb.Append("{");
                        sb.Append(string.Format("\"gid\":\"{0}\",", item.GoodsId));
                        sb.Append(string.Format("\"gname\":\"{0}\",", item.GoodsName));
                        sb.Append(string.Format("\"gpic\":\"{0}\",", item.GoodsPic));
                        sb.Append(string.Format("\"gcount\":\"{0}\",", item.GoodsCnt));
                        sb.Append(string.Format("\"gprice\":\"{0}\",", item.GoodsPrice));
                        sb.Append(string.Format("\"guige\":\"{0}\"", guige));
                        sb.Append("},");
                    }
                    sb.Append("],");
                    sb = sb.Replace("},],", "}],");

                    sb.Append(string.Format("\"oid\":\"{0}\",", orderlist[0].OrderId_id));
                    sb.Append(string.Format("\"state\":\"{0}\",", orderlist[0].OrderId.State));
                    sb.Append(string.Format("\"addr\":\"{0}\",", orderlist[0].OrderId.CustomerAddr));
                    sb.Append(string.Format("\"customer\":\"{0}\",", orderlist[0].OrderId.Customer));
                    sb.Append(string.Format("\"tel\":\"{0}\",", orderlist[0].OrderId.CustomerTel));
                    sb.Append(string.Format("\"ordertotals\":\"{0}\",", orderlist[0].OrderId.OrderTotals));
                    sb.Append(string.Format("\"payment\":\"{0}\",", orderlist[0].OrderId.Payment));
                    sb.Append(string.Format("\"otime\":\"{0}\",", orderlist[0].OrderId.OrderTime.ToString("yyyy-MM-dd HH:mm:ss")));
                    sb.Append(string.Format("\"goodstotals\":\"{0}\",", orderlist[0].OrderId.GoodsTotals));
                    sb.Append(string.Format("\"yunfei\":\"{0}\",", orderlist[0].OrderId.TransferFee));
                    sb.Append(string.Format("\"tiaozheng\":\"{0}\",", orderlist[0].OrderId.OtherAdjust));
                    sb.Append(string.Format("\"youhui\":\"{0}\"", orderlist[0].OrderId.Favourable));
                    sb.Append("}");

                    rmsg.errcode = 0;
                    rmsg.orderdetailsJson = sb.ToString();
                }
                catch (Exception ex)
                {
                    Data.MethodHepler.MHepler.writelog_err("接口-根据订单id获取订单详情时异常:" + ex.Message);
                    rmsg.errcode = -1;
                    rmsg.errmsg = ex.Message;
                }

                return rmsg;
            }
        }
        
        /// <summary>
        /// 根据订单状态获取
        /// </summary>
        /// <param name="state">状态 0 已完成 1 未完成 -1取消 2 待评价</param>
        /// <returns></returns>
        public ReturnMsg _getOrderListByState(string uid, int state)
        {
            ReturnMsg rmsg = new ReturnMsg();
         
            using (MltxDbContext db = new MltxDbContext())
            {
                try
                {
                    IQueryable<Mltx_Order_main> list_m = db.order_m.Where(o => o.Openid == uid);
                    switch (state)
                    {
                        case 0: list_m = list_m.Where(o => o.State == State.完成 || o.State == State.待评价).OrderByDescending(o => o.OrderTime); break;
                        case 1: list_m = list_m.Where(o => o.State == State.待发货 || o.State == State.等待自提 || o.State == State.发货中 || o.State == State.未预付款 || o.State == State.已预付款 || o.State == State.未付款).OrderByDescending(o => o.OrderTime); break;
                        case -1: list_m = list_m.Where(o => o.State == State.取消).OrderByDescending(o => o.OrderTime); break;
                        case 2: list_m = list_m.Where(o => o.State == State.待评价).OrderByDescending(o => o.OrderTime); break;
                        default: list_m = list_m.OrderByDescending(o => o.OrderTime); break;
                    }

                    if (list_m.Count() == 0)
                    { 
                        rmsg.errcode = 0;
                        rmsg.orderbaseList = new List<OrderBase>() ;
                    }
                    else
                    {

                        List<OrderBase> list = list_m.Select(a => new OrderBase
                        {
                            OrderId = a.OrderId,
                            SumCount = a.GoodsCnt,
                            OrderTotals = a.OrderTotals,
                            State = (int)a.State,
                            GoodsCnt = db.order_s.Where(s => s.OrderId_id == a.OrderId).FirstOrDefault().GoodsCnt,
                            GoodsPic = db.order_s.Where(s => s.OrderId_id == a.OrderId).FirstOrDefault().GoodsPic,
                            GoodsName = db.order_s.Where(s => s.OrderId_id == a.OrderId).FirstOrDefault().GoodsName,
                            Guige = (a.OrderCls == 0 ? db.goodsinfo_shop.Where(s => s.GoodsId == db.order_s.Where(ss => ss.OrderId_id == (list_m.FirstOrDefault(aa => aa.OrderId == a.OrderId).OrderId)).FirstOrDefault().GoodsId).FirstOrDefault().GoodsGuige : db.goodsinfo_fresh.Where(s => s.GoodsId == db.order_s.Where(ss => ss.OrderId_id == (list_m.FirstOrDefault(aa => aa.OrderId == a.OrderId).OrderId)).FirstOrDefault().GoodsId).FirstOrDefault().GoodsGuige),
                        }).ToList();

                        rmsg.errcode = 0;
                        rmsg.orderbaseList = list;
                    }
                }
                catch (Exception ex)
                {
                    Data.MethodHepler.MHepler.writelog_err("接口-获取订单列表异常:" + ex.Message);
                    rmsg.errcode = -1;
                    rmsg.errmsg = ex.Message;
                }

                return rmsg;
            }
        }
        #endregion

        #region 会员登录 信息
        /// <summary>
        /// 登陆app 
        /// </summary>
        /// <param name="tel"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public string _loginApp(string tel, string pwd)
        {
            using (MltxDbContext db = new MltxDbContext())
            {
                try
                {
                    Mltx_MemberInfo info = db.memberinfo.SingleOrDefault(m => m.Tel == tel);
                    if (info == null)
                    {
                        return "1";//没有注册信息
                    }
                    else
                    {
                        if (Data.MethodHepler.MHepler.GetMD5(pwd) == info.Password)
                            return "0";//登陆成功
                        else
                            return "2";//密码错误
                    }
                }
                catch (Exception ex)
                {
                    Data.MethodHepler.MHepler.writelog_err("接口-登陆app时异常:" + ex.Message);
                    return "-1";
                }
            }
        }

        /// <summary>
        /// 修改完善用户信息
        /// </summary>
        /// <param name="membername"></param>
        /// <param name="tel"></param>
        /// <param name="sex"></param>
        /// <param name="birthday"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public string _modifyuserinfo(string membername, string tel, int sex, DateTime birthday, string address)
        {
            using (MltxDbContext db = new MltxDbContext())
            {
                try
                {
                    Mltx_MemberInfo minfo = db.memberinfo.SingleOrDefault(m => m.Tel == tel);
                    if (string.IsNullOrEmpty(membername))
                    {
                        minfo.MemberName = tel;
                    }
                    else
                    {
                        minfo.MemberName = membername;
                    }
                    minfo.sex = sex;
                    minfo.Birthday = birthday;
                    minfo.Address = address;
                    minfo.UserPic = "/";//默认头像

                    db.Entry(minfo).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    return "0";
                }
                catch (Exception ex)
                {
                    Data.MethodHepler.MHepler.writelog_err("接口-修改完善用户资料时异常:" + ex.Message);
                    return "-1";
                }
            }
        }

        /// <summary>
        /// 根据tel获取用户信息
        /// </summary>
        /// <param name="tel"></param>
        /// <returns></returns>
        public ReturnMsg _getUserinfoBytel(string tel)
        {
            ReturnMsg rmsg = new ReturnMsg();
            using (MltxDbContext db = new MltxDbContext())
            {
                try
                {
                    MemberBase minfo = db.memberinfo.Where(m => m.Tel == tel).Select(m => new MemberBase
                    {
                        MemberName = m.MemberName,
                        Tel = m.Tel,
                        sex = m.sex,
                        UserPic = m.UserPic,
                        Rank = m.RankId,
                        Integral = m.Integral,
                        Birthday = m.Birthday,
                        Balance = m.Balance
                    }).SingleOrDefault();

                    rmsg.errcode = 0;
                    rmsg.userbaseinfo = minfo;
                }
                catch (Exception ex)
                {
                    Data.MethodHepler.MHepler.writelog_err("接口-根据电话获取用户信息异常:" + ex.Message);
                    rmsg.errcode = -1;
                    rmsg.errmsg = ex.Message;
                }

                return rmsg;
            }
        }

        /// <summary>
        /// 根据电话获取会员等级信息 消费金额 次数
        /// </summary>
        /// <param name="tel">电话号码</param>
        /// <returns></returns>
        public ReturnMsg _getMemberRankByTel(string tel)
        {
            ReturnMsg rmsg = new ReturnMsg();
            using (MltxDbContext db = new MltxDbContext())
            {
                try
                {
                    MemberRankBase info = db.memberinfo.Where(m => m.Tel == tel).Select(m => new MemberRankBase {
                        Consume =m.Consume,
                        Consumenumber = m.Consumenumber,
                        Rank = m.RankId
                    }).SingleOrDefault();

                    rmsg.errcode = 0;
                    rmsg.memberrankinfo = info;
                }
                catch (Exception ex)
                {
                    Data.MethodHepler.MHepler.writelog_err("接口-根据电话获取会员等级消费信息异常:" + ex.Message);
                    rmsg.errcode = -1;
                    rmsg.errmsg = ex.Message;
                }

                return rmsg;
            }
        }

        #endregion

        #region 商品分组信息
        /// <summary>
        /// 获取分组信息 
        /// </summary>
        /// <param name="type">类型 0 商城 1 鲜果</param>
        /// <returns></returns>
        public ReturnMsg _getGroupList(string type)
        {
            ReturnMsg rmsg = new ReturnMsg();
            using (MltxDbContext db = new MltxDbContext())
            {
                try
                {
                    if (type == "0")
                    {
                        List<Mltx_Group_first> list_f = db.group_first.ToList();
                        List<ShopGroupData> list1 = new List<ShopGroupData>();
                        List<SecondGroupData> list2 = new List<SecondGroupData>();

                        foreach (Mltx_Group_first item in list_f)//一级分组
                        {
                            List<Mltx_Group_second> list_s = db.group_second.Where(g => g.OwnerId == item.GroupId).ToList();
                            ShopGroupData data_f = new ShopGroupData();
                            foreach (Mltx_Group_second item1 in list_s)//二级分组
                            {
                                SecondGroupData data_s = new SecondGroupData();
                                data_s.secondId = item1.GroupId;
                                data_s.secondName = item1.GroupName;
                                data_s.picpath = item1.GroupPic;
                                list2.Add(data_s);
                            }

                            data_f.firstId = item.GroupId;
                            data_f.firstName = item.GroupName;
                            data_f.secondData = list2;

                            list1.Add(data_f);
                        }

                        rmsg.errcode = 0;
                        rmsg.shopgroupdata = list1;
                    }
                    else//获取鲜果单数据
                    {
                        List<Mltx_Group_fresh> list = db.group_fresh.ToList();
                        rmsg.errcode = 0;
                        rmsg.MltxGroupFreshes = list;
                    }
                }
                catch (Exception ex)
                {
                    Data.MethodHepler.MHepler.writelog_err("接口-获取分组信息时异常:" + ex.Message);
                    rmsg.errcode = -1;
                    rmsg.errmsg = ex.Message;
                }

                return rmsg;
            }
        } 
        #endregion

        #region 地址库信息

        /// <summary>
        /// 根据电话tel获取地址库信息
        /// </summary>
        /// <param name="tel"></param>
        /// <returns></returns>
        public ReturnMsg _getAddressInfoByTel(string tel)
        {
            ReturnMsg rmsg = new ReturnMsg();
            using (MltxDbContext db = new MltxDbContext())
            {
                try
                {
                    List<Mltx_Address> list = db.address.Where(a => a.Tel == tel).ToList();

                    rmsg.errcode = 0;
                    rmsg.addressList = list;
                }
                catch (Exception ex)
                {
                    Data.MethodHepler.MHepler.writelog_err("接口-获取地址库时异常:" + ex.Message);
                    rmsg.errcode = -1;
                    rmsg.errmsg = ex.Message;
                }

                return rmsg;
            }
        }
        #endregion

        #region 首页部分信息

        /// <summary>
        /// 获取幻灯片列表
        /// </summary>
        /// <param name="tag">标识 1表示鲜果幻灯片 null或者0 表示商城</param>
        /// <returns></returns>
        public ReturnMsg _getSlideinfo(string tag)
        {
            ReturnMsg rmsg = new ReturnMsg();

            if(string.IsNullOrEmpty(tag))
            {
                tag = "0";
            }
            using (MltxDbContext db = new MltxDbContext())
            {
                try
                {
                    int t = int.Parse(tag);
                    List<SlideBase> list = db._slide.Where(s => s.Location == t).Select(s => new SlideBase
                    {
                        Id = s.Id,
                        Img = s.Img,
                        SlideCls = s.SlideCls,
                        sort = s.sort,
                        Type = s.Type,
                        URL = s.URL,
                        Value = s.Value
                    }).ToList();

                    rmsg.errcode = 0;
                    rmsg.slidebaseList = list;
                }
                catch (Exception ex)
                {
                    Data.MethodHepler.MHepler.writelog_err("接口-获取幻灯片列表异常:" + ex.Message);
                    rmsg.errcode = -1;
                    rmsg.errmsg = ex.Message;
                }

                return rmsg;
            }

        }

        /// <summary>
        /// 获取首页菜单列表
        /// </summary>
        /// <param name="tag">标识 0 底部浮动菜单 1或null 顶部商城 2 顶部鲜果</param>
        /// <returns></returns>
        public ReturnMsg _getMenuInfo(string tag)
        {
            ReturnMsg rmsg = new ReturnMsg();

            if (string.IsNullOrEmpty(tag))
            {
                tag = "1";//默认查询商城
            }
            using (MltxDbContext db = new MltxDbContext())
            {
                try
                {
                    Location t =(Location) int.Parse(tag);
                    List<MenuBase> list = db._menu.Where(m => m.Location == t).Select(m => new MenuBase { 
                    
                        Img = m.Img,
                        MenuCls = m.MenuCls,
                        Name = m.Name,
                        Sort = m.Sort,
                        Type = m.Type,
                        URL = m.URL,
                        Value = m.Value
                    }).ToList();

                    rmsg.errcode = 0;
                    rmsg.menubaseList = list;
                }
                catch (Exception ex)
                {
                    Data.MethodHepler.MHepler.writelog_err("接口-获取菜单列表异常:" + ex.Message);
                    rmsg.errcode = -1;
                    rmsg.errmsg = ex.Message;
                }

                return rmsg;
            }
        }

        /// <summary>
        /// 获取首页模块列表 
        /// </summary>
        /// <param name="tag">标识 0或null 商城 1 鲜果</param>
        /// <returns></returns>
        public ReturnMsg _getIndexModule(string tag)
        {
            ReturnMsg rmsg = new ReturnMsg();

            if (string.IsNullOrEmpty(tag))
            {
                tag = "0";
            }
            using (MltxDbContext db = new MltxDbContext())
            {
                try
                {
                    int t = int.Parse(tag);
                    List<Mltx_IndexModule> list = db.index_m.Where(m => m.IndexCls == t).ToList();

                    rmsg.errcode = 0;
                    rmsg.indexmodleBaselist = list;
                }
                catch (Exception ex)
                {
                    Data.MethodHepler.MHepler.writelog_err("接口-获取首页模块列表异常:" + ex.Message);
                    rmsg.errcode = -1;
                    rmsg.errmsg = ex.Message;
                }

                return rmsg;
            }
        }

        #endregion

        #region 生成订单
        
        /// <summary>
        ///生成订单 
        /// </summary>
        /// <returns></returns>
        public ReturnMsg _CreateOrder(Mltx_Order_main main, orderData _odata, string orderid)
        {
            ReturnMsg rmsg = new ReturnMsg();

            using (MltxDbContext db = new MltxDbContext())
            {
                DbConnection con = ((IObjectContextAdapter)db).ObjectContext.Connection;
                con.Open();

                DbTransaction tran = con.BeginTransaction();
                try
                {
                    db.order_m.Add(main);
                    db.SaveChanges();
                    foreach (_Data sub in _odata.data)
                    {
                        Mltx_Order_sub _sub = new Mltx_Order_sub();
                        _sub.OrderId_id = orderid;
                        _sub.GoodsId = sub.gid;
                        _sub.GoodsName = sub.gname;
                        _sub.GoodsPic = sub.gpic;
                        _sub.GoodsPrice = sub.rprice;
                        _sub.GoodsCnt = sub.count;
                        _sub.IsCommens = IsCommens.否;
                        _sub.Integral = 0;//积分
                        db.order_s.Add(_sub);
                        db.SaveChanges();
                    }

                    tran.Commit();
                    SubGoodsStock(_odata);//减库存
                    rmsg.errcode = 0;
                }
                catch (Exception)
                {
                    tran.Rollback();
                    rmsg.errcode = -2;
                    //订单生产异常
                }
            }

            return rmsg;
        }

        /// <summary>
        /// 减库存
        /// </summary>
        /// <returns></returns>
        public ReturnMsg SubGoodsStock(orderData _data)
        {
            using (MltxDbContext db = new MltxDbContext())
            {

                ReturnMsg rmsg = new ReturnMsg();
                foreach (_Data item in _data.data)
                {
                    int gid = item.gid;
                    int cnt = item.count; 
                    try
                    {
                        Mltx_GoodsInfo_shop ginfo = db.goodsinfo_shop.Where(s => s.GoodsId == gid).SingleOrDefault();
                        ginfo.GoodsStock = ginfo.GoodsStock - cnt;//减库存
                        ginfo.GoodsSales = ginfo.GoodsSales + cnt;//加销量

                        db.Entry(ginfo).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();

                        rmsg.errcode = 0;
                        rmsg.errmsg = "SUCCESS";
                    }
                    catch (Exception ex)
                    {
                        rmsg.errcode = -1;
                        rmsg.errmsg = ex.Message;
                    }
                }

                return rmsg;
            }

        }

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="oid"></param>
        /// <returns></returns>
        public ReturnMsg CancelMyOrder(string oid)
        {
            ReturnMsg rmsg = new ReturnMsg();

            using (MltxDbContext db = new MltxDbContext())
            {
                try
                {
                    Mltx_Order_main main = db.order_m.Where(m => m.OrderId == oid).SingleOrDefault();

                    main.State = State.取消;

                    db.Entry(main).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    rmsg.errcode = 0;
                    rmsg.errmsg = "SUCCESS";
                }
                catch (Exception ex)
                {
                    rmsg.errcode = -1;
                    rmsg.errmsg = ex.Message;
                }

                return rmsg;
            }
        }

        #endregion

        #region 评论接口

        /// <summary>
        /// 获取需要评论的订单详情
        /// </summary>
        /// <param name="oid"></param>
        /// <returns></returns>
        public ReturnMsg _getOrderCommentByOid(string oid)
        {
            ReturnMsg rmsg = new ReturnMsg();

            using (MltxDbContext db = new MltxDbContext())
            {
                try
                {
                    List<Mltx_Order_sub> orderlist = db.order_s.Include("OrderId").Where(o => o.OrderId_id == oid && o.IsCommens == IsCommens.否).ToList();

                    StringBuilder sb = new StringBuilder();
                    sb.Append("[");
                    foreach (Mltx_Order_sub item in orderlist)
                    {
                        string guige = (item.OrderId.OrderCls == 0 ? db.goodsinfo_shop.Where(s => s.GoodsId == item.GoodsId).FirstOrDefault().GoodsGuige : db.goodsinfo_fresh.Where(s => s.GoodsId == item.GoodsId).FirstOrDefault().GoodsGuige);
                        sb.Append("{");
                        sb.Append(string.Format("\"gid\":\"{0}\",", item.GoodsId));
                        sb.Append(string.Format("\"gname\":\"{0}\",", item.GoodsName));
                        sb.Append(string.Format("\"gpic\":\"{0}\",", item.GoodsPic));
                        sb.Append(string.Format("\"gcount\":\"{0}\",", item.GoodsCnt));
                        sb.Append(string.Format("\"count\":\"{0}\",", item.GoodsCnt));
                        sb.Append(string.Format("\"guige\":\"{0}\"", guige));
                        sb.Append("},");
                    }
                    sb.Append("]");
                    sb = sb.Replace("},]", "}]");

                    rmsg.errcode = 0;
                    rmsg.errmsg = "SUCCESS";
                    rmsg.orderdetailsJson = sb.ToString();
                }
                catch (Exception ex)
                {
                    rmsg.errcode = -1;
                    rmsg.errmsg = ex.Message;
                }

                return rmsg;
            }
        }

        /// <summary>
        /// 单个商品进行评论
        /// </summary>
        /// <param name="gid"></param>
        /// <param name="star"></param>
        /// <param name="_content"></param>
        /// <param name="_tag"></param>
        /// <returns></returns>
        public ReturnMsg _doCommentsBygid(string oid,string gid,string gname,string customer, string star, string _content, bool _tag)
        {
            ReturnMsg rmsg = new ReturnMsg();
            int goodid = int.Parse(gid);

            using (MltxDbContext db = new MltxDbContext())
            {
                DbConnection con = ((IObjectContextAdapter)db).ObjectContext.Connection;
                con.Open();

                DbTransaction tran = con.BeginTransaction();

                try
                {
                    Mltx_Comments comm = new Mltx_Comments();
                    comm.GoodsId = goodid;
                    comm.GoodsName = gname;
                    comm.Customer = customer;
                    comm.CommentTime = DateTime.Now;
                    comm.ComType = 0;//默认可看
                    comm.CommentStar = int.Parse(star);
                    comm.Comments = _content;

                    db.comments.Add(comm);
                    db.SaveChanges();

                    if (gid.IndexOf('3') == 0)//商城 修改商品表评价信息
                    {
                        Mltx_GoodsInfo_shop ginfo = db.goodsinfo_shop.Where(s => s.GoodsId == goodid).SingleOrDefault();
                        int _cnt = db.comments.Where(c => c.GoodsId == goodid).Count();

                        float _star = (ginfo.GoodsStar + float.Parse(star)) / _cnt;
                        ginfo.GoodsStar = float.Parse(_star.ToString("f2"));
                        db.Entry(ginfo).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    else {
                        Mltx_GoodsInfo_fresh ginfo = db.goodsinfo_fresh.Where(s => s.GoodsId == goodid).SingleOrDefault();
                        int _cnt = db.comments.Where(c => c.GoodsId == goodid).Count();

                        double _star = (ginfo.GoodsStar + double.Parse(star)) / _cnt;
                        ginfo.GoodsStar = double.Parse(_star.ToString("f2"));

                        db.Entry(ginfo).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }

                    //修改从表商品状态

                    Mltx_Order_sub sub = db.order_s.Where(s => s.OrderId_id == oid && s.GoodsId == goodid).SingleOrDefault();
                    sub.IsCommens = IsCommens.是;
                    db.Entry(sub).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    if (_tag)//最后一个，需要修改主表状态
                    {
                        Mltx_Order_main main = db.order_m.Where(m => m.OrderId == oid).SingleOrDefault();
                        main.State = State.完成;

                        db.Entry(main).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }

                    tran.Commit();

                    rmsg.errcode = 0;
                    rmsg.errmsg = "SUCCESS";
                    
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    rmsg.errcode = -1;
                    rmsg.errmsg = ex.Message;
                }

                return rmsg;
            }
        }

        #endregion 

        #region 获取menu菜单

        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <returns></returns>
        public ReturnMsg _getMenu()
        {
            ReturnMsg rmsg = new ReturnMsg();
            IEnumerable<Mltx_Menu> menus = null;
            using (MltxDbContext db = new MltxDbContext())
            {
                try
                {
                    menus = db._menu.OrderBy(f => f.Sort);//所有菜单数据
                    string menujson = json<Mltx_Menu>(menus.ToList());
                    rmsg.errcode = 0;
                    rmsg.otherJson = menujson;
                }
                catch (Exception ex)
                {
                    rmsg.errcode = -1;
                    rmsg.errmsg = ex.Message;
                }

                return rmsg;
            }
        }

        /// <summary>
        /// 泛型转换json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public string json<T>(List<T> t)
        {
            string result = string.Empty;
            try
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                result = js.Serialize(t);
            }
            catch
            {
                result = string.Format("{0}", "error");
            }
            return result;
        }
        #endregion

        #region 确认收货
        
        public ReturnMsg _complateOrder(string oid)
        {
            ReturnMsg rmsg = new ReturnMsg();
            using (MltxDbContext db = new MltxDbContext())
            {
                try
                {
                    Mltx_Order_main main = db.order_m.Where(o => o.OrderId == oid).SingleOrDefault();

                    if (main == null)//没找到订单
                    {
                        rmsg.errcode = 1;
                        rmsg.errmsg = "订单没找到";
                    }
                    else {
                        main.State = State.待评价;

                        db.Entry(main).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();

                        rmsg.errcode = 0;
                    }
                }
                catch (Exception ex)
                {
                    rmsg.errcode = -1;
                    rmsg.errmsg = ex.Message;
                }

                return rmsg;
            }

        }
        #endregion
        
        #region 其他接口

        /// <summary>
        /// 根据提货券号获取提货券信息
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        public ReturnMsg _getTicketInfo(string tid)
        {
            ReturnMsg rmsg = new ReturnMsg();

            using (MltxDbContext db = new MltxDbContext())
            {
                try
                {
                    TicketBase tinfo = db.tickets.Include("Goods").Where(t => t.TicketId == tid).Select(t => new TicketBase { 
                        GoodsId = t.Goods.GoodsId,
                        GoodsName = t.Goods.GoodsName,
                        Rprice = t.Goods.Rprice,
                        State = (int)t.State,
                        TicketId = t.TicketId,
                        Time = t.Time
                    }).SingleOrDefault();
                    rmsg.ticketbase = tinfo;
                    rmsg.errcode = 0;
                }
                catch (Exception ex)
                {
                    Data.MethodHepler.MHepler.writelog_err("接口-获取提货券信息异常:" + ex.Message);
                    rmsg.errcode = -1;
                    rmsg.errmsg = ex.Message;
                }

                return rmsg;
            }
        }
        #endregion
    }
}