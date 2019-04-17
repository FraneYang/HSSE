using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using System.Runtime.Serialization;

namespace FineUIPro.Web.Hazard
{
    public class JsonHelper
    {
        public static string JsonSerializer<T>(T t)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, t);
            string jsonString = Encoding.UTF8.GetString(ms.ToArray());
            ms.Close();
            return jsonString;
        }
        public static T JsonDeserialize<T>(string jsonString)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            T obj = (T)ser.ReadObject(ms);
            return obj;
        }
    }
    [DataContract]
    class SDev
    {
        [DataMember]
        public string ID { get; set; }
        [DataMember]
        public int RiskLevel { get; set; }
        [DataMember]
        public int STATUS { get; set; }
    };
    [DataContract]
    class ListSDev
    {
        [DataMember]
        public List<SDev> LIST { get; set; }
    };
    /// <summary>
    /// devstatus 的摘要说明
    /// </summary>
    public class devstatus : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
            if (context.Request.HttpMethod == "POST")
            {
                //
                DoGetDevStatus(context);
            }
        }
        //模拟返回设备状态JSON，返回格式
        // list:[ {id:"A0",status:1},{id:"B1",status:2}]
        public void DoGetDevStatus(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //请求格式 devid=A1,B2,C3,D4,F5,E6
            //
            string devid = context.Request.Form["devid"];
            try
            {
                string[] aryid = devid.Split(',');
                //do sql query
                //此语句不安全
                //string sql = "select status from devstatus where id in (" + aryid + ")";

                ListSDev v = new ListSDev
                {
                    LIST = new List<SDev>()
                };
                //var rnd = new Random();

                foreach (var id in aryid)
                {
                    SDev sdev = new SDev
                    {
                        ID = id
                    };
                    var eui = BLL.Funs.DB.Base_Euipment.FirstOrDefault(x => x.EuipmentCode == id);
                    if (eui != null && !string.IsNullOrEmpty(eui.RiskLevel))
                    {
                        sdev.STATUS = BLL.Funs.GetNewIntOrZero(eui.RiskLevel) - 1;//rnd.Next(0,4);                      
                    }
                    else
                    {
                        sdev.STATUS = 0;
                    }

                    
                    v.LIST.Add(sdev);
                }

                context.Response.Write(JsonHelper.JsonSerializer(v));
            }
            catch (Exception ex)
            {
                context.Response.Write("[]"+ex.ToString());
            }
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