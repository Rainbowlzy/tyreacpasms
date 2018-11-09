using System.Collections.Generic;
using System.IO;
using System.Linq;
using T.Models;

namespace T.Evaluators
{
    public class UploadedImageEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            var mapPath = request.context.Request.MapPath("~/Upload/Image");
            List<string> files = new List<string>();
            Stack<string> stack = new Stack<string>();
            stack.Push(mapPath);
            while (stack.Count!=0)
            {
                var currentdir = stack.Pop();
                foreach (var directory in Directory.GetDirectories(currentdir))
                {
                    stack.Push(directory);
                }
                files.AddRange(Directory.GetFiles(currentdir));
            }
            return files
                .OrderByDescending(File.GetCreationTime)
                .Select(s => s.Replace(mapPath, $"http://223.112.112.26:58080/T/Upload/Image")).ToList();
        }
    }
}