using MltxManager.Models.DBHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace MltxManager.WebService.Helper
{
    public class JsonHelper
    {
       /// <summary>
       /// 数据转json
       /// </summary>
       /// <param name="o"></param>
       /// <returns></returns>
        public string DataToJson(object o)
        {
            if (o == null)//为空直接返回
            {
                return "{\"errcode\":\"0\",\"errmsg\":\"成功\",\"datajson\":\"\"}";
            }

            try
            {
                var jer = new JavaScriptSerializer();
                string datajson = jer.Serialize(o);

                return "{\"errcode\":\"0\",\"errmsg\":\"成功\",\"datajson\":" + datajson + "}";
            }
            catch (Exception ex)
            {
                return "{\"errcode\":\"-1\",\"errmsg\":" + ex.Message + ",\"datajson\":\"\"}";
            }
        }


        /// <summary>
        /// 数据转json 未用到
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="data">数据</param>
        /// <param name="_isList">是否是多条数据</param>
        /// <returns></returns>
        public string _DataToJson<T>(IList<T> data, bool _isList = true)
        {
            StringBuilder sb = new StringBuilder();
            string Datajson = "";

            try
            {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
                if (_isList)
                {
                    sb.Append("[");
                }
                foreach (T item in data)
                {
                    sb.Append("{");
                    for (int i = 0; i < properties.Count; i++)
                    {
                        if (i == properties.Count - 1)
                        {
                            sb.Append(string.Format("\"{0}\":\"{1}\"", properties[i].Name, properties[i].GetValue(item)));
                        }
                        else
                        {
                            sb.Append(string.Format("\"{0}\":\"{1}\",", properties[i].Name, properties[i].GetValue(item)));
                        }
                    }
                    sb.Append("},");
                }
                sb.Remove(sb.Length - 1, 1);
                if (_isList)
                {
                    sb.Append("]");
                }

                Datajson = "{\"errcode\":\"0\",\"errmsg\":\"成功\",\"datajson\":" + sb.ToString() + "}";
            }
            catch (Exception ex)
            {
                Datajson = "{\"errcode\":\"-1\",\"errmsg\":" + ex.Message + ",\"datajson\":\"\"}";
            }

            return Datajson;
        }

    }
}