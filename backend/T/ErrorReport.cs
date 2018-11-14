using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace T
{
    public class ErrorReport
    {
        private const string MANAGER_EMAIL_ADDRESS = "zhengyao.lu@qq.com";
        private const string MANAGER_USER_NAME = "路正遥";
        private const string MESSAGE_SUBJECT = "系统线上异常";
        private const string MANAGER_USER = "user";
        private const string MANAGER_PASS = "password";
        private const string ERROR_REPORT_SMTP_SERVER = "smtp.qq.com";

        public void error(string message)
        {
            var now = NowDateTimeString();
            File.WriteAllText($"{@"D:\errors\"}{now}.log", message);
            var address = new MailAddress(MANAGER_EMAIL_ADDRESS, MANAGER_USER_NAME, Encoding.UTF8);
            var mail = new MailMessage(MANAGER_EMAIL_ADDRESS, MANAGER_EMAIL_ADDRESS, MESSAGE_SUBJECT, message);
            var client = new SmtpClient(ERROR_REPORT_SMTP_SERVER, 993);
            client.ClientCertificates.Add(new System.Security.Cryptography.X509Certificates.X509Certificate());
            client.Credentials = new NetworkCredential(MANAGER_USER, MANAGER_PASS);
            client.Send(mail);
        }

        private static string NowDateTimeString()
        {
            return string.Join("_", MatchesNow());
        }

        private static IEnumerable<string> MatchesNow()
        {
            foreach (Match match in Regex.Matches(DateTime.Now.ToString("s"), "(\\W|\\d)+")) yield return match.Value;
        }
    }
}