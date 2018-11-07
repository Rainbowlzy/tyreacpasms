using T.Interfaces;
using T.Models;

namespace T.Evaluators
{
    public class ShowEvaluator:IEvaluator
    {
        public ShowEvaluator(object target)
        {
            this.target = target;
        }

        public object target { get; set; }

        public void Dispose()
        {
            target = null;
        }

        public object Eval(CommonRequest request)
        {
            return target;
        }
    }
}