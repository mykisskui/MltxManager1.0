﻿using MltxManager.Data;
using MltxManager.Models.DBHelper;
using MltxManager.WebService.Helper;
using System;
using MltxManager.Models.DBModel;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MltxManager.WebService.GoodsInterface
{
    /// <summary>
    /// GetGoodsInfoByGid 的摘要说明 根据商品id获取商品信息
    /// </summary>
    /// http://sms.tfresh.cn/GoodsInterface/GetGoodsInfoByGid.ashx?goodsid=1111&tag=0
    public class GetGoodsInfoByGid : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int goodsid = int.Parse(context.Request.Params["goodsid"].ToString());
            string tag = context.Request.Params["tag"];
            string msg = null;
            ReturnMsg rmsg = new ReturnMsg();
            InterfaceData idata = new InterfaceData();
            JsonHelper jsonhelper = new JsonHelper();
            if (string.IsNullOrEmpty(tag))
            {
                msg = "{\"errcode\":\"-1\",\"errmsg\":\"缺少参数tag\",\"datajson\":\"\"}";
            }
            else
            {
                rmsg = idata._getGoodsinfobyGid(goodsid, tag);
                if (rmsg.errcode == 0)//返回数据成功
                {
                    if (tag == "0")
                    {
                        msg = jsonhelper.DataToJson(rmsg.MltxGoodsInfoShop);
                    }
                    else
                    {
                        msg = jsonhelper.DataToJson(rmsg.MltxGoodsInfoFresh);
                    }
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