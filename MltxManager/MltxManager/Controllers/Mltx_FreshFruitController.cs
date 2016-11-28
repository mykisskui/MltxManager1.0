using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using MltxManager.Data;
using MltxManager.Data.MethodHepler;
using MltxManager.Models.DBHelper.Mode;
using MltxManager.Models.DBModel;

namespace MltxManager.Controllers
{
    [SigninAuthority]
    public class Mltx_FreshFruitController : Controller
    {
        //
        // GET: /Mltx_FreshFruit/
        #region 鲜果分组

        /// <summary>
        /// 分组页面显示
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 分组页面 鲜果显示
        /// </summary>
        /// <returns></returns>
        public ActionResult GetGroupingLocal()
        {
            var re = new FruitData().GetFreshPacket();
            if (re.errcode == 0)
            {
                return PartialView("GroupingLocal", re.MltxGroupFreshes);
            }
            else
            {
                return RedirectToAction("ErrorPage", "ToOtherPage", new { errmsg = "程序发生异常，请稍候重试！！" });
            }
        }
        /// <summary>
        /// 鲜果分组添加
        /// </summary>
        /// <param name="name">分组名称</param>
        /// <returns></returns>
        [PageAuthority(Leixing = false, ModeId = Modular.鲜果分组信息, QuanId = Quanxian.增加)]
        public int InsertFreshPacket(string name)
        {
            var re = new FruitData().InsertFreshPacket(name);
            return re ? 0 : 1;
        }
        /// <summary>
        /// 鲜果分组修改
        /// </summary>
        /// <param name="name">修改名称</param>
        /// <param name="id">修改ID</param>
        /// <returns></returns>
        [PageAuthority(Leixing = false, ModeId = Modular.鲜果分组信息, QuanId = Quanxian.修改)]
        public int UpdateFreshPacket(string name, int id)
        {
            var re = new FruitData().UpdateFreshPacket(name, id);
            return re ? 0 : 1;
        }
        /// <summary>
        /// 鲜果分组删除
        /// </summary>
        /// <param name="id">鲜果分组ID</param>
        /// <returns></returns>
        [PageAuthority(Leixing = false, ModeId = Modular.鲜果分组信息, QuanId = Quanxian.删除)]
        public int DeletFreshPacket(int id)
        {
            var re = new FruitData().DeletFreshPacket(id);
            return re ? 0 : 1;
        }
        #endregion
        #region 鲜果商品管理
        /// <summary>
        /// 鲜果商品页面显示
        /// </summary>
        /// <param name="id">页数</param>
        /// <param name="kword">查询关键字</param>
        /// <returns></returns>
        public ActionResult Information(int id = 1, string kword = null)
        {
            var re = new FruitData().GetInformation(id, kword);
            if (re.errcode == 0)
            {
                return View(re.MltxGoodsInfoFreshes);
            }
            else
            {
                return RedirectToAction("ErrorPage", "ToOtherPage", new { errmsg = "程序发生异常，请稍候重试！！" });
            }

        }
        /// <summary>
        /// 打开鲜果商品增加页面
        /// </summary>
        /// <returns></returns>
        [PageAuthority(Leixing = false, ModeId = Modular.鲜果商品管理, QuanId = Quanxian.增加)]
        public ActionResult InsertCommodity()
        {
            var re = new FruitData().GetFreshPacket();
            if (re.errcode == 0)
            {
                return View(re.MltxGroupFreshes);
            }
            else
            {
                return RedirectToAction("ErrorPage", "ToOtherPage", new { errmsg = "程序发生异常，请稍候重试！！" });
            }

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
        /// <returns></returns>
        [ValidateInput(false)]
        [PageAuthority(Leixing = false, ModeId = Modular.鲜果商品管理, QuanId = Quanxian.增加)]
        public int InsertCommodityAdded(string name, string guige, int kuchun, string tupian, string liebiao, int fenzu, decimal rprice, decimal mprice, string xiangxi, int fangs, int jifen, int zhuangtai)
        {
            var r = int.Parse(Session["User"].ToString());
            var re = new FruitData().InsertCommodityAdded(name, guige, kuchun, tupian, liebiao, fenzu,rprice,mprice, xiangxi, fangs, jifen, zhuangtai, r);
            return re;
        }
        /// <summary>
        /// 商品下架
        /// </summary>
        /// <param name="i">商品ID</param>
        /// <param name="r">判断类型</param>
        /// <returns></returns>
        [PageAuthority(Leixing = false, ModeId = Modular.鲜果商品管理, QuanId = Quanxian.删除)]
        public int DeleteCommodity(int i, int r)
        {
            var re = new FruitData().DeleteCommodity(i, r);
            return re;
        }
        /// <summary>
        /// 商品修改读取数据
        /// </summary>
        /// <param name="goodId">商品ID</param>
        /// <returns></returns>
        [PageAuthority(ModeId = Modular.鲜果商品管理, QuanId = Quanxian.修改)]
        public ActionResult UpdateCommodityPage(int goodId)
        {
            var re = new FruitData().UpdateCommodityPage(goodId);
            if (re.errcode == 0)
            {

                if (!string.IsNullOrWhiteSpace(re.MltxGoodsInfoFresh.GoodsPic))
                {
                    var r = re.MltxGoodsInfoFresh.GoodsPic.Split('|');
                    ViewBag.tupian = r;
                }
                else
                {
                    ViewBag.tupian = "";
                }


                ViewBag.yiji = re.DoubleBases;

                return View(re.MltxGoodsInfoFresh);
            }
            else
            {
                return RedirectToAction("ErrorPage", "ToOtherPage", new { errmsg = "程序发生异常，请稍候重试！！" });
            }
        }
        /// <summary>
        /// 商品修改
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
        /// <param name="groupId">商品ID</param>
        /// <returns></returns>
        [ValidateInput(false)]
        [PageAuthority(Leixing = false, ModeId = Modular.鲜果商品管理, QuanId = Quanxian.修改)]
        public int UpdateCommodityModify(string name, string guige, int kuchun, string tupian, string liebiao, int fenzu, decimal rprice, decimal mprice, string xiangxi, int fangs, int jifen, int zhuangtai, int groupId)
        {
            var r = int.Parse(Session["User"].ToString());
            var re = new FruitData().UpdateCommodityModify(name, guige, kuchun, tupian, liebiao, fenzu,rprice,mprice, xiangxi, fangs, jifen, zhuangtai, r, groupId);
            return re;

        }
        #endregion
        #region
        /// <summary>
        /// dropzone 图片上传
        /// </summary>
        /// <returns>返回所有图片路径中间由|分割</returns>
        public ActionResult FigureUploadedFile()
        {
            var isSavedSuccessfully = true;
            var count = 0;
            var msg = "";
            var lujing = "";
            var path = "";
            //这里是获取隐藏域中的数据
            var albumId = string.IsNullOrEmpty(Request.Params["shumu"]) ?
                0 : int.Parse(Request.Params["shumu"]);

            try
            {
                var directoryPath = Server.MapPath("~/Images/FreshFruitPicture");
                if (!Directory.Exists(directoryPath))
                    Directory.CreateDirectory(directoryPath);

                foreach (string f in Request.Files)
                {
                    var file = Request.Files[f];

                    if (file == null || file.ContentLength <= 0) continue;
                    var fileName = file.FileName;
                    var fileNewName = DateTime.Now.ToString("yyyyMMddhhmmss") + fileName;
                    var filePath = Path.Combine(directoryPath, fileNewName);
                    file.SaveAs(filePath);
                    const string lu = "/Images/FreshFruitPicture/";
                    path = Path.Combine(lu, fileNewName);
                    lujing = path + "|" + lujing;
                    count++;
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                isSavedSuccessfully = false;
            }
            if (albumId == 0)
            {
                return Json(new
                {
                    Result = isSavedSuccessfully,
                    Count = count,
                    Message = msg,
                    mingcheng = lujing
                });
            }
            else
            {
                return Json(new
                {
                    Result = isSavedSuccessfully,
                    Count = count,
                    Message = msg,
                    mingcheng = path
                });
            }

        }
        #endregion
    }
}
