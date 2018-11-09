using System.Linq;
using T.Models;
using TEntities.EF;
using TENtities.EF;

namespace T.Evaluators
{
    public class GetMenuConfigurationByAuthEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            if (Sessions?.Count == 0)
            {
                return new CommonOutputT<string>
                {
                    success = false,
                    message = "请登录",
                    data = null
                };
            }

            var user = CurrentUserInformation;
            if (user == null)
            {
                return new CommonOutputT<string>
                {
                    success = false,
                    message = "请登录",
                    data = null
                };
            }

            var key = request.context.Request.Params["key"] ?? "后台首页";
            using (var ctx = new DefaultContext())
            {
                var role = ctx.UserRole.FirstOrDefault(p => p.URLoginName == user.UILoginName && p.IsDeleted == 0);
                if (role == null)
                {
                    return new CommonOutputT<string>
                    {
                        success = false,
                        message = "请登录",
                        data = null
                    };
                }

                var mainMenu = ctx.MenuConfiguration.Where(p => p.MCParentTitle == key && p.IsDeleted == 0);
                var roleName = role.URRoleName;
                var topMenu =
                    ctx.RoleMenu.Where(p => p.IsDeleted == 0 && p.RMRoleName == roleName)
                        .GroupBy(p => p.RMMenuTitle)
                        .Select(p => p.Key)
                        .Join(mainMenu, s => s, menu => menu.MCCaption, (s, configuration) => configuration)
                        .OrderBy(p => p.MCSequence)
                        .ToList();
                var allLeftMenu = ctx.MenuConfiguration.Where(p => p.MCParentTitle == "后台首页左侧" && p.IsDeleted == 0);
                var leftMenu =
                    ctx.RoleMenu.Where(p => p.IsDeleted == 0 && p.RMRoleName == roleName)
                        .GroupBy(p => p.RMMenuTitle)
                        .Select(p => p.Key)
                        .Join(allLeftMenu, s => s, menu => menu.MCCaption, (s, configuration) => configuration)
                        .OrderBy(p => p.MCSequence)
                        .ToList();
                return new
                {
                    rows = topMenu.ToArray(),
                    topmenu = topMenu.ToArray(),
                    leftmenu = leftMenu.ToArray(),
                    success = true,
                    message = "查询成功"
                };
            }
        }
    }
}