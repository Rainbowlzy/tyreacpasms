using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
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
        private const string MANAGER_EMAIL_ADDRESS = "zhengyao.lu@qq.com";

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

//            response.AddHeader("Access-Control-Allow-Origin", "http://localhost:8080");
            var validationResults = Validation.Validate(crequest);
            foreach (ValidationResult result in validationResults)
            {
                crequest.context = null;
                response.ContentType = "text/plain";
                error(result.Message);
                response.Write(result.Message);
                return;
            }
            var evaluator = "evaluator";
            crequest.method = crequest.method.ToLower();
            if (crequest.method.Contains(evaluator))
                crequest.method = crequest.method.Substring(0, crequest.method.IndexOf(evaluator, StringComparison.Ordinal));
            crequest.method += evaluator;
            response.ContentType = "application/json";
            using (IEvaluator ieval = Evaluator.Make(crequest))
            {
                var val = ieval.Eval(crequest);
                var json = JsonConvert.SerializeObject(val);
                response.Write(json);
            }
        }

        public void error(string message)
        {
            try
            {
                File.WriteAllText($"{@"D:\errors\"}{DateTime.Now.Ticks}.log", message);
                var address = new MailAddress(MANAGER_EMAIL_ADDRESS, "路正遥", Encoding.UTF8);
                var mail = new MailMessage(MANAGER_EMAIL_ADDRESS, MANAGER_EMAIL_ADDRESS, "系统线上异常", message);
                var client = new SmtpClient("smtp.qq.com", 993);
                client.ClientCertificates.Add(new System.Security.Cryptography.X509Certificates.X509Certificate());
                client.Credentials = new NetworkCredential("user", "password");
                client.Send(mail);
            }
            catch (Exception)
            {
                return;
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