﻿using System.Linq;
using Newtonsoft.Json;
using T.Models;
using TEntities.EF;
using TENtities.EF;

namespace T.Evaluators
{
    [CacheOptions(Timeout = 9999)]
    public class GetThirdMenuEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            var user = CurrentUserInformation;
            if (user == null)
            {
                return new CommonOutputT<string>
                {
                    success = false,
                    message = "请登录"
                };
            }
            if (request == null)
            {
                return new CommonOutputT<string>
                {
                    success = false,
                    message = "请求参数错误"
                };
            }
            var menu = JsonConvert.DeserializeObject<MenuConfiguration>(request.data);
            var caption = menu.MCCaption;

            using (var ctx = new DefaultContext())
            {
                var list = ctx.MenuConfiguration.Where(p => p.MCParentTitle == caption).ToList();
                return list;
            }
        }
    }
}