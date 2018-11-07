using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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