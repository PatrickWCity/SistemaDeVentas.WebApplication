using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace MvcApplication1.Models
{
    public class Jsonp : JsonResult
    {
        object data = null;
        public Jsonp()
        {
        }
        public Jsonp(object data)
        {
            this.data = data;
        }
        public override void ExecuteResult(ControllerContext context)
        {
            HttpResponseBase Response = context.HttpContext.Response;
            HttpRequestBase Request = context.HttpContext.Request;
            string callbackfunction = Request["callback"];
            if (callbackfunction != null)
            {
                Response.ContentType = "application/x-javascript";
                if (data != null)
                {
                    string json = JsonConvert.SerializeObject(data);
                    Response.Write(string.Format("{0}({1})", callbackfunction, json));


                    JavaScriptSerializer serialize = new JavaScriptSerializer();
                    Response.Write(string.Format("{0}({1})", callbackfunction, serialize.Serialize(data)));
                }
            }
        }
    }
}