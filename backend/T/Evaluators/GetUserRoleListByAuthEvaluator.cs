using System.Collections.Generic;
using System.Linq;
using T.Models;
using TEntities.EF;
using TENtities.EF;

namespace T.Evaluators
{
    public partial class GetUserRoleListByAuthEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            if (Sessions.Count == 0)
                return new CommonOutputT<string>()
                {
                    success = false,
                    message = "请登录"
                };
            using (var ctx = new DefaultContext())
            {
                var user = Sessions[nameof(UserInformation)] as UserInformation;
                return new CommonOutputT<List<UserRole>>()
                {
                    success = true,
                    data =
                        ctx.UserRole.Where(
                            p => p.URLoginName == user.UILoginName && p.IsDeleted == 0).ToList(),
                    message = "查询成功"
                };
            }
        }
    }
}