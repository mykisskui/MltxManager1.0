using MltxManager.Data;
using MltxManager.Models.DBHelper;
using MltxManager.WebService.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MltxManager.WebService.GoodsInterface
{
    /// <summary>
    /// GetGoodsListByPage 的摘要说明
    /// </summary>
    public class GetGoodsListByGroupId : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string page = context.Request.Params["page"];//第几页 从0开始表示第一页
            string num = context.Request.Params["num"];//每页显示数据条数
            string groupid = context.Request.Params["groupid"];//为空表示获取全部数据
            string orderbyway = context.Request.Params["orderbyway"];//排序方式
            string tag = context.Request.Params["tag"];//查询类型 0 商城 1 果切
            ReturnMsg rmsg = new ReturnMsg();
            InterfaceData idata = new InterfaceData();
            JsonHelper jsonhelper = new JsonHelper();
            string msg = null;

            if (string.IsNullOrEmpty(page) || string.IsNullOrEmpty(num) || string.IsNullOrEmpty(tag))
            {
                msg = "{\"errcode\":\"-1\",\"errmsg\":\"参数缺失\",\"datajson\":\"\"}";
            }
            else
            {
                rmsg = idata._getGoodsinfoListByGroupid(groupid, page, num, orderbyway, tag);
                if (rmsg.errcode == 0)
                {
                    msg = jsonhelper.DataToJson(rmsg.goodsbaseList);
                }
                else
                {
                    msg = "{\"errcode\":\"-1\",\"errmsg\":" + rmsg.errmsg + ",\"datajson\":\"\"}";
                }
            }

            context.Response.Write(msg);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}