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
            var method = request.Params["method"];
            var qmethod = request.QueryString["method"];
            var data = HttpUtility.UrlDecode(request.Params["data"]);
            var qdata = request.QueryString["data"];
            var crequest = new CommonRequest
            {
                auth = auth,
                method = method ?? qmethod ?? "",
                data = data ?? qdata ?? "",
                context = context
            };
            response.Cookies.Add(new HttpCookie("auth_user", auth));
            response.ContentType = "application/json";

            //            response.AddHeader("Access-Control-Allow-Origin", "http://localhost:8080");
            var validationResults = Validation.Validate(crequest);
            foreach (ValidationResult result in validationResults)
            {
                crequest.context = null;
                error(result.Message);
                response.Write(JsonConvert.SerializeObject(new
                {
                    success = false,
                    message = result.Message
                }));
                return;
            }

            var evaluator = "evaluator";
            crequest.method = crequest.method.ToLower();
            if (crequest.method.Contains(evaluator))
                crequest.method =
                    crequest.method.Substring(0, crequest.method.IndexOf(evaluator, StringComparison.Ordinal));
            crequest.method += evaluator;
            using (IEvaluator ieval = Evaluator.Build(crequest))
            {
                var val = ieval.Eval(crequest);
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