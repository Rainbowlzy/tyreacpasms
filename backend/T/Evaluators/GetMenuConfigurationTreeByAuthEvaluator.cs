using System.Linq;
using EF.Entities;
using T.Models;
using TEntities.CodeTemplates;
using TEntities.EF;


namespace T.Evaluators
{
    public class GetMenuConfigurationTreeByAuthEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            if (Sessions.Count == 0)
            {
                return new CommonOutputT<string>
                {
                    success = false,
                    message = "请登录",
                    data = null
                };
            }
            var user = Sessions[nameof(UserInformation)] as UserInformation;
            if (user == null)
            {
                return new CommonOutputT<string>
                {
                    success = false,
                    message = "请登录",
                    data = null
                };
            }
            using (var ctx = new DefaultContext())
            {
                var role = ctx.UserRole.FirstOrDefault(p => p.URLoginName == user.UILoginName && p.IsDeleted == 0);
                var topmenu =
                    ctx.RoleMenu.Where(p => p.IsDeleted == 0 && p.RMRoleName == role.URRoleName)
                        .GroupBy(p => p.RMMenuTitle)
                        .Select(p => p.Key)
                        .Join(ctx.MenuConfiguration.Where(p => p.MCParentTitle == "后台首页"), s => s,
                            menu => menu.MCCaption, (s, configuration) => configuration)
                        .OrderBy(p => p.MCSequence)
                        .ToList();

                var leftmenu =
                    ctx.RoleMenu.Where(p => p.IsDeleted == 0 && p.RMRoleName == role.URRoleName)
                        .GroupBy(p => p.RMMenuTitle)
                        .Select(p => p.Key)
                        .Join(ctx.MenuConfiguration.Where(p => p.MCParentTitle == "后台首页左侧"), s => s,
                            menu => menu.MCCaption, (s, configuration) => configuration)
                        .OrderBy(p => p.MCSequence)
                        .ToList();

                var menus = ctx.MenuConfiguration.ToList();
                return new
                {
                    //rows = topmenu.ToArray(),
                    //topmenu = topmenu.ToArray(),
                    //leftmenu = leftmenu.ToArray(),
                    //background = backgroundimagecode?.val,

                    menu = menus.Select(t => new TreeNode<MenuConfiguration>
                    {
                        title = t.MCCaption,
                        data = t,
                        children = menus.Where(p => p.MCParentTitle == t.MCCaption).Select(k => new TreeNode<MenuConfiguration>
                        {
                            title = k.MCCaption,
                            data = t,
                            children = menus.Where(p => p.MCParentTitle == t.MCCaption).Select(l => new TreeNode<MenuConfiguration>
                            {
                                title = l.MCCaption,
                                data = t
                            }).ToList()
                        }).ToList()
                    }).Where(t=>t.children.Any()).ToList(),
                    success = true,
                    message = "查询成功"
                };
            }
        }
    }
}