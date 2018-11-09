using System.Web;

namespace T
{
    /// <summary>
    /// DefaultHandler 的摘要说明
    /// </summary>
    public class Version : IHttpHandler
    {
        /// <summary>
        /// 处理机构
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            var request = context.Request;
            var response = context.Response;
            response.ContentType = "text/plain";
            response.Write($"1.0");
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