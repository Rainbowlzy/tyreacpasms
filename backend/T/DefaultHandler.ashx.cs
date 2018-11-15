using System;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using T.Evaluators;
using T.Interfaces;
using T.Models;
using Validation = Microsoft.Practices.EnterpriseLibrary.Validation.Validation;
using ValidationResult = Microsoft.Practices.EnterpriseLibrary.Validation.ValidationResult;

namespace T
{
    /// <summary>
    /// DefaultHandler 的摘要说明
    /// </summary>
    public class DefaultHandler : IHttpHandler
    {
        /// <summary>
        /// 处理机构
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            var request = context.Request;
            var response = context.Response;

            //浏览器应当在cookie中身份验证，APP可以使用http参数
            var authUser = request.Cookies["auth_user"];
            var auth = authUser == null ? "" : HttpUtility.UrlDecode(authUser.Value);
            if (string.IsNullOrEmpty(auth)) auth = request.Params["auth_user"] ?? "";
            if (!string.IsNullOrEmpty(auth)) auth = auth.Split(',').FirstOrDefault();
            var data = HttpUtility.UrlDecode(request.Params["data"])?? request.QueryString["data"]??"";
            var method = (request.Params["method"] ?? request.QueryString["method"] ?? "").Trim().ToLower();
            response.Cookies.Add(new HttpCookie("auth_user", auth));
            response.ContentType = "application/json";
            var validationResults = Validation.Validate(new CommonRequest
            {
                auth = auth,
                method = method,
                data = data
            });
            foreach (ValidationResult result in validationResults)
            {
                error(result.Message);
                response.Write(JsonConvert.SerializeObject(new
                {
                    success = false,
                    message = result.Message
                }));
                return;
            }

            using (IEvaluator ieval = Evaluator.Build(new CommonRequest
            {
                auth = auth,
                method = method,
                data = data,
                context = context
            }))
            {
                var val = ieval.Eval(new CommonRequest
                {
                    auth = auth,
                    method = method,
                    data = data,
                    context = context
                });
                var json = JsonConvert.SerializeObject(val);
                response.Write(json);
            }
        }

        private void error(string message)
        {
            var re = new ErrorReport();
            re.error(message);
        }

        public bool IsReusable
        {
            get { return false; }
        }
    }
}