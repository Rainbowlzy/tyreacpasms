using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using Newtonsoft.Json;
using T.Evaluators;
using T.Models;

namespace T
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class ImageUpload : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string result;
            try
            {
                var request = context.Request;
                var auth_user = request.Params["auth_user"] ?? "非登录用户";
                if (!string.IsNullOrEmpty(auth_user)) auth_user = auth_user.Split(',').FirstOrDefault();
                var evaluator = Evaluator.Build(new CommonRequest
                {
                    auth = auth_user,
                    method = nameof(GetMenuConfigurationByAuthEvaluator)
                }) as Evaluator;
                var userInformation = evaluator?.CurrentUserInformation;
                var now = DateTime.Now.ToString("yy-MM-dd").Replace("-", String.Empty);
                var uiLoginName = userInformation?.UILoginName;
                var path = context.Server.MapPath($"~/Upload/Image/{uiLoginName}_{userInformation?.id}/{now}");
                var arr = new List<string>();
                for (var i = 0; i < request.Files.Count; i++)
                {
                    var requestFile = request.Files[i];
                    var extension = requestFile.FileName.Split(new[] {'.'}, StringSplitOptions.RemoveEmptyEntries)
                        .LastOrDefault()?.ToLower();
                    if (extension == "png")
                        arr.Add(SavePngFile(path, requestFile)
                            .Replace(context.Server.MapPath("~/").Replace("\\", "/"), ""));
                    else
                        arr.Add(SaveAttachmentFile(path, requestFile)
                            .Replace(context.Server.MapPath("~/").Replace("\\", "/"), ""));
                }

                result = JsonConvert.SerializeObject(new
                {
                    IsOk = 1,
                    Msg = "上传成功",
                    imageURL = arr.FirstOrDefault(),
                    arr
                });
            }
            catch (Exception ex)
            {
                result = JsonConvert.SerializeObject(new
                {
                    IsOk = 0,
                    Msg = "上传失败" + ex.Message,
                    exception = ex
                });
            }

            context.Response.Write(result);
            context.Response.End();
        }

        public bool IsReusable
        {
            get { return false; }
        }

        private static string SavePngFile(string path, HttpPostedFile f)
        {
            var id = Guid.NewGuid().ToString();
            CreateDirIfNotExist(path);
            var filePath = path + "\\" + id + ".png";
            DeleteIfExist(filePath);
            var imageUrl = "/T/" +
                           filePath.Replace("\\", "/");
            f.SaveAs(filePath); //存储图片完毕
            return imageUrl;
        }

        private static string SaveAttachmentFile(string path, HttpPostedFile f)
        {
            var id = Guid.NewGuid().ToString();
            CreateDirIfNotExist(path);
            var ext = f.FileName.Split(new[] {'.'}, StringSplitOptions.RemoveEmptyEntries).LastOrDefault()?.ToLower();
            var filePath = path + "\\" + id + "." + ext;
            DeleteIfExist(filePath);
            var attachmentFile = "/T/" +
                                 filePath.Replace("\\", "/");
            f.SaveAs(filePath); //存储图片完毕
            return attachmentFile;
        }

        private static void CreateDirIfNotExist(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        private static void DeleteIfExist(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}