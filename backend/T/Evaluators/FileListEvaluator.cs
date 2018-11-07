using System.IO;
using System.Linq;
using T.Models;

namespace T.Evaluators
{
    public class FileListEvaluator:Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            var dir = @"..\..\T\gen\";
            return Directory.GetFiles(dir, "*List.html").Select(s=>s.Replace(dir, "http://localhost/T/gen/".Replace("\\","/")));
        }
    }
}