using T.Models;

namespace T.Evaluators
{
    public class GetCurrentUserInformationEvaluator: Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            return CurrentUserInformation;
        }
    }
}