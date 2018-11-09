
/* ------------------------------------------------------------ *
 * 此文件由生成器引擎根据既有规则生成，所有手工的更改将会被覆盖
 * 生成时间：11/09/2018 22:46:32
 * 生成版本：11/08/2018 11:49:42 
 * 作者：路正遥
 * ------------------------------------------------------------ */

using TEntities.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using T.Models;
using Newtonsoft.Json;
using System.Data.Entity.Migrations;
using static System.Data.Objects.EntityFunctions;
using static System.DateTime;
using static System.Linq.Enumerable;
using EF.Entities;
using Generator.Tools;
using Validation = Microsoft.Practices.EnterpriseLibrary.Validation.Validation;
using ValidationResult = Microsoft.Practices.EnterpriseLibrary.Validation.ValidationResult;

namespace T.Evaluators
{

    /// <summary>
    /// 【角色配置】7次查询分别得到七天内记录的条数，形成柱状图，折线图
    /// </summary>
    public partial class RoleConfigurationCountEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
                var now = Parse(request.context.Request.Params["date"] ?? Now.ToString("yyyy-MM-dd"));
                var dates = Range(1, 7).Select(p => now.AddDays(-1 * p)).ToList();
                var list = dates.Select(p => ctx.RoleConfiguration.Count(m => m.CreateOn==p)).ToList();
                return new
                {
                    sum = list.Sum(),
                    xaxis = dates.Select(p => p.ToString("yyyy-MM-dd")).ToList(),
                    series = list
                };
            }
        }
        public override string Comments=> "【角色配置】7次查询分别得到七天内记录的条数，形成柱状图，折线图";
    }
	public partial class TruncateRoleConfigurationEvaluator : Evaluator
	{
        protected override object Evaluate(CommonRequest request)
		{
            using (var ctx = new DefaultContext())
			{
                ctx.RoleConfiguration.RemoveRange(ctx.RoleConfiguration);
				ctx.SaveChanges();
			}
			return new
			{
				success = true,
				message = "操作成功"
			};
		}
	}
    /// <summary>
    /// 删除【角色配置】
    /// </summary>
    public partial class DeleteRoleConfigurationEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            var data = JsonConvert.DeserializeObject<RoleConfiguration>(HttpUtility.UrlDecode(request.data));
            if (data == null)
                return new CommonOutputT<string>
                {
                    success = false,
                    message = "删除成功"
                };
            using (var ctx = new DefaultContext())
            {
                var one = ctx.RoleConfiguration.Find(data.id);
				if(one==null){
					return new CommonOutputT<string>
					{
						success = false,
						message = "未找到需要删除的数据"
					};
				}
                one.IsDeleted = 1;
				ctx.RoleConfiguration.AddOrUpdate(one);
				ctx.SaveChanges();
				return new CommonOutputT<string>
				{
					success = true,
					message = "删除成功"
				};
            }
			
        }
        public override string Comments=> "删除一条角色配置记录";
    }
	
    /// <summary>
    /// 保存【角色配置】
    /// </summary>
    public partial class SaveRoleConfigurationEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            var user = CurrentUserInformation;
            if (user==null)
            {
                return new
                {
                    success = false,
                    message = "请登录"
                };
            }
			var s = request.data;
            if (s==null)
            {
                return new
                {
                    success = false,
                    message = "缺少参数"
                };
            }
			RoleConfiguration entity = null;
			try
			{
				entity = JsonConvert.DeserializeObject<RoleConfiguration>(HttpUtility.UrlDecode(s));
			}
			catch(Exception exception)
			{
				return new
				{
					success = false,
					message = $"填写内容格式错误：{exception.Message}",
					input = s
				};
			}
            if (entity==null)
            {
                return new
                {
                    success = false,
                    message = "参数格式不正确"
                };
            }

			try
			{
				foreach (ValidationResult result in Validation.Validate(entity))
				{
					return new
					{
						success = false,
						message = result.Message
					};
				}
			}
			catch(Exception exception)
			{
				return new
				{
					success = false,
					message = exception.Message
				};
			}
			
            using (var ctx = new DefaultContext())
            {
				// 行级排他锁开始
                var transactionId = Guid.NewGuid().ToString();
                var isnew = entity.TransactionID == null;
                if (!isnew)
                {
                    var one = ctx.RoleConfiguration.FirstOrDefault(p=>p.id==entity.id);
                    if(one==null) return new
                    {
                        success = false,
                        message = "编辑错误，未找到ID"
                    };
                    if (one.VersionNo != entity.VersionNo) return new
                    {
                        success = false,
                        message = "发生数据写冲突"
                    };
                    one.VersionNo++;
                    one.TransactionID = transactionId;
					ctx.RoleConfiguration.AddOrUpdate(one);
					try
					{
						ctx.SaveChanges();
					}
					catch(Exception exception)
					{
						// 遇到数据库中的脏数据，走到这里，前面的Entity数据合法，直接跳过这里的数据校验阶段，使用最先到达的正确数据。
						// return new
						// {
						// 	 success = false,
						// 	 message = exception.Message,
						// 	 exception, one, transactionId, entity
						// };
					}
                    entity.VersionNo = one.VersionNo;
                }
				

								// NVARCHAR(50) 角色名称
				entity.RCRoleName = HttpUtility.UrlDecode(entity.RCRoleName);
					// NVARCHAR(50) 所属组织
				entity.RCAffiliatedOrganization = HttpUtility.UrlDecode(entity.RCAffiliatedOrganization);
	
                entity.CreateBy = entity.CreateBy ?? user?.UILoginName ?? "未登录用户";
                entity.UpdateBy = user?.UILoginName ?? "未登录用户";
                entity.CreateOn = entity.CreateOn ?? Now;
                entity.TransactionID = transactionId;
                entity.UpdateOn = Now;
                entity.IsDeleted = 0;
	            entity.VersionNo = entity.VersionNo ?? 0;
                entity.DataLevel = entity.DataLevel ?? user?.DataLevel ?? "019999";
				ctx.RoleConfiguration.AddOrUpdate(entity);
				try
				{
					ctx.SaveChanges();
				}
				catch(Exception exception)
				{
					return new
					{
						success = false,
						message = exception.Message,
						exception, transactionId, entity
					};
				}
				// 行级排他锁结束
                return new
                {
                    success = true,
                    message = "操作成功"
                };
            }
        }
        public override string Comments=> "保存一条RoleConfiguration记录";
    }
	
    /// <summary>
    /// 查询空的【角色配置】
    /// </summary>
    public partial class GetRoleConfigurationEmptyEvaluator:Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            return new RoleConfiguration();
        }
        public override string Comments=> "获取空的角色配置记录";
    }
	
    /// <summary>
    /// 查询【角色配置】列表
    /// </summary>
    public partial class GetRoleConfigurationListEvaluator : Evaluator
    {
        public override string Comments=> "获取RoleConfiguration列表 ";
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
				var datalevel = CurrentUserInformation?.DataLevel;
                var searchModel = HttpUtility.UrlDecode(request.data).Deserialize<RoleConfigurationSearchModel>() ?? new RoleConfigurationSearchModel();
                var query = ctx.RoleConfiguration.Where(t=>t.IsDeleted==0 && t.DataLevel.StartsWith(datalevel));
                var @params = request.context.Request.Params;
                searchModel.PageSize = (@params["limit"] ?? searchModel.PageSize.ToString()).ToInt();
				searchModel.PageSize = searchModel.PageSize==0?4000:searchModel.PageSize;
                searchModel.PageIndex = (@params["offset"]).ToInt()/ searchModel.PageSize;
                var isordered = false;
                var search = searchModel.SearchKey ?? @params["search"];
                var sort = searchModel.Sort ?? @params["sort"];
				var order = @params["order"];
				// RCRoleName NVARCHAR(50) 角色名称 
                if(!string.IsNullOrEmpty(searchModel.RCRoleName)) query = query.Where(t=>t.RCRoleName.Contains(searchModel.RCRoleName));
                if(sort=="RCRoleName")
                {
					query = order=="asc"?query.OrderBy(t=>t.RCRoleName):query.OrderByDescending(t=>t.RCRoleName);
                    isordered = true;
                }
				// RCAffiliatedOrganization NVARCHAR(50) 所属组织 
                if(!string.IsNullOrEmpty(searchModel.RCAffiliatedOrganization)) query = query.Where(t=>t.RCAffiliatedOrganization.Contains(searchModel.RCAffiliatedOrganization));
                if(sort=="RCAffiliatedOrganization")
                {
					query = order=="asc"?query.OrderBy(t=>t.RCAffiliatedOrganization):query.OrderByDescending(t=>t.RCAffiliatedOrganization);
                    isordered = true;
                }
				if(!string.IsNullOrEmpty(search)){
					query = query.Where(t=>t.id!=-1||t.RCRoleName.Contains(search)||t.RCAffiliatedOrganization.Contains(search));
				}
                if(sort=="ord")
                {
					query = order=="asc"?query.OrderBy(t=>t.ord):query.OrderByDescending(t=>t.ord);
                    isordered = true;
                }

                if(!isordered) query = query.OrderByDescending(t=>t.UpdateOn);
                var rows = query.Skip((searchModel.PageIndex)*searchModel.PageSize).Take(searchModel.PageSize).ToList();
                var total = query.Count();
                var sql = query.ToString();
                return new CommonOutputList<RoleConfiguration>
                {
                    success = true, rows = rows, total = total, message="查询成功"
                };
            }
        }
    }


    /// <summary>
    /// 【用户菜单】7次查询分别得到七天内记录的条数，形成柱状图，折线图
    /// </summary>
    public partial class UserMenuCountEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
                var now = Parse(request.context.Request.Params["date"] ?? Now.ToString("yyyy-MM-dd"));
                var dates = Range(1, 7).Select(p => now.AddDays(-1 * p)).ToList();
                var list = dates.Select(p => ctx.UserMenu.Count(m => m.CreateOn==p)).ToList();
                return new
                {
                    sum = list.Sum(),
                    xaxis = dates.Select(p => p.ToString("yyyy-MM-dd")).ToList(),
                    series = list
                };
            }
        }
        public override string Comments=> "【用户菜单】7次查询分别得到七天内记录的条数，形成柱状图，折线图";
    }
	public partial class TruncateUserMenuEvaluator : Evaluator
	{
        protected override object Evaluate(CommonRequest request)
		{
            using (var ctx = new DefaultContext())
			{
                ctx.UserMenu.RemoveRange(ctx.UserMenu);
				ctx.SaveChanges();
			}
			return new
			{
				success = true,
				message = "操作成功"
			};
		}
	}
    /// <summary>
    /// 删除【用户菜单】
    /// </summary>
    public partial class DeleteUserMenuEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            var data = JsonConvert.DeserializeObject<UserMenu>(HttpUtility.UrlDecode(request.data));
            if (data == null)
                return new CommonOutputT<string>
                {
                    success = false,
                    message = "删除成功"
                };
            using (var ctx = new DefaultContext())
            {
                var one = ctx.UserMenu.Find(data.id);
				if(one==null){
					return new CommonOutputT<string>
					{
						success = false,
						message = "未找到需要删除的数据"
					};
				}
                one.IsDeleted = 1;
				ctx.UserMenu.AddOrUpdate(one);
				ctx.SaveChanges();
				return new CommonOutputT<string>
				{
					success = true,
					message = "删除成功"
				};
            }
			
        }
        public override string Comments=> "删除一条用户菜单记录";
    }
	
    /// <summary>
    /// 保存【用户菜单】
    /// </summary>
    public partial class SaveUserMenuEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            var user = CurrentUserInformation;
            if (user==null)
            {
                return new
                {
                    success = false,
                    message = "请登录"
                };
            }
			var s = request.data;
            if (s==null)
            {
                return new
                {
                    success = false,
                    message = "缺少参数"
                };
            }
			UserMenu entity = null;
			try
			{
				entity = JsonConvert.DeserializeObject<UserMenu>(HttpUtility.UrlDecode(s));
			}
			catch(Exception exception)
			{
				return new
				{
					success = false,
					message = $"填写内容格式错误：{exception.Message}",
					input = s
				};
			}
            if (entity==null)
            {
                return new
                {
                    success = false,
                    message = "参数格式不正确"
                };
            }

			try
			{
				foreach (ValidationResult result in Validation.Validate(entity))
				{
					return new
					{
						success = false,
						message = result.Message
					};
				}
			}
			catch(Exception exception)
			{
				return new
				{
					success = false,
					message = exception.Message
				};
			}
			
            using (var ctx = new DefaultContext())
            {
				// 行级排他锁开始
                var transactionId = Guid.NewGuid().ToString();
                var isnew = entity.TransactionID == null;
                if (!isnew)
                {
                    var one = ctx.UserMenu.FirstOrDefault(p=>p.id==entity.id);
                    if(one==null) return new
                    {
                        success = false,
                        message = "编辑错误，未找到ID"
                    };
                    if (one.VersionNo != entity.VersionNo) return new
                    {
                        success = false,
                        message = "发生数据写冲突"
                    };
                    one.VersionNo++;
                    one.TransactionID = transactionId;
					ctx.UserMenu.AddOrUpdate(one);
					try
					{
						ctx.SaveChanges();
					}
					catch(Exception exception)
					{
						// 遇到数据库中的脏数据，走到这里，前面的Entity数据合法，直接跳过这里的数据校验阶段，使用最先到达的正确数据。
						// return new
						// {
						// 	 success = false,
						// 	 message = exception.Message,
						// 	 exception, one, transactionId, entity
						// };
					}
                    entity.VersionNo = one.VersionNo;
                }
				

								// NVARCHAR(50) 登录名
				entity.UMLoginName = HttpUtility.UrlDecode(entity.UMLoginName);
					// NVARCHAR(50) 标题
				entity.UMCaption = HttpUtility.UrlDecode(entity.UMCaption);
	
                entity.CreateBy = entity.CreateBy ?? user?.UILoginName ?? "未登录用户";
                entity.UpdateBy = user?.UILoginName ?? "未登录用户";
                entity.CreateOn = entity.CreateOn ?? Now;
                entity.TransactionID = transactionId;
                entity.UpdateOn = Now;
                entity.IsDeleted = 0;
	            entity.VersionNo = entity.VersionNo ?? 0;
                entity.DataLevel = entity.DataLevel ?? user?.DataLevel ?? "019999";
				ctx.UserMenu.AddOrUpdate(entity);
				try
				{
					ctx.SaveChanges();
				}
				catch(Exception exception)
				{
					return new
					{
						success = false,
						message = exception.Message,
						exception, transactionId, entity
					};
				}
				// 行级排他锁结束
                return new
                {
                    success = true,
                    message = "操作成功"
                };
            }
        }
        public override string Comments=> "保存一条UserMenu记录";
    }
	
    /// <summary>
    /// 查询空的【用户菜单】
    /// </summary>
    public partial class GetUserMenuEmptyEvaluator:Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            return new UserMenu();
        }
        public override string Comments=> "获取空的用户菜单记录";
    }
	
    /// <summary>
    /// 查询【用户菜单】列表
    /// </summary>
    public partial class GetUserMenuListEvaluator : Evaluator
    {
        public override string Comments=> "获取UserMenu列表 ";
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
				var datalevel = CurrentUserInformation?.DataLevel;
                var searchModel = HttpUtility.UrlDecode(request.data).Deserialize<UserMenuSearchModel>() ?? new UserMenuSearchModel();
                var query = ctx.UserMenu.Where(t=>t.IsDeleted==0 && t.DataLevel.StartsWith(datalevel));
                var @params = request.context.Request.Params;
                searchModel.PageSize = (@params["limit"] ?? searchModel.PageSize.ToString()).ToInt();
				searchModel.PageSize = searchModel.PageSize==0?4000:searchModel.PageSize;
                searchModel.PageIndex = (@params["offset"]).ToInt()/ searchModel.PageSize;
                var isordered = false;
                var search = searchModel.SearchKey ?? @params["search"];
                var sort = searchModel.Sort ?? @params["sort"];
				var order = @params["order"];
				// UMLoginName NVARCHAR(50) 登录名 
                if(!string.IsNullOrEmpty(searchModel.UMLoginName)) query = query.Where(t=>t.UMLoginName.Contains(searchModel.UMLoginName));
                if(sort=="UMLoginName")
                {
					query = order=="asc"?query.OrderBy(t=>t.UMLoginName):query.OrderByDescending(t=>t.UMLoginName);
                    isordered = true;
                }
				// UMCaption NVARCHAR(50) 标题 
                if(!string.IsNullOrEmpty(searchModel.UMCaption)) query = query.Where(t=>t.UMCaption.Contains(searchModel.UMCaption));
                if(sort=="UMCaption")
                {
					query = order=="asc"?query.OrderBy(t=>t.UMCaption):query.OrderByDescending(t=>t.UMCaption);
                    isordered = true;
                }
				if(!string.IsNullOrEmpty(search)){
					query = query.Where(t=>t.id!=-1||t.UMLoginName.Contains(search)||t.UMCaption.Contains(search));
				}
                if(sort=="ord")
                {
					query = order=="asc"?query.OrderBy(t=>t.ord):query.OrderByDescending(t=>t.ord);
                    isordered = true;
                }

                if(!isordered) query = query.OrderByDescending(t=>t.UpdateOn);
                var rows = query.Skip((searchModel.PageIndex)*searchModel.PageSize).Take(searchModel.PageSize).ToList();
                var total = query.Count();
                var sql = query.ToString();
                return new CommonOutputList<UserMenu>
                {
                    success = true, rows = rows, total = total, message="查询成功"
                };
            }
        }
    }


    /// <summary>
    /// 【用户角色】7次查询分别得到七天内记录的条数，形成柱状图，折线图
    /// </summary>
    public partial class UserRoleCountEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
                var now = Parse(request.context.Request.Params["date"] ?? Now.ToString("yyyy-MM-dd"));
                var dates = Range(1, 7).Select(p => now.AddDays(-1 * p)).ToList();
                var list = dates.Select(p => ctx.UserRole.Count(m => m.CreateOn==p)).ToList();
                return new
                {
                    sum = list.Sum(),
                    xaxis = dates.Select(p => p.ToString("yyyy-MM-dd")).ToList(),
                    series = list
                };
            }
        }
        public override string Comments=> "【用户角色】7次查询分别得到七天内记录的条数，形成柱状图，折线图";
    }
	public partial class TruncateUserRoleEvaluator : Evaluator
	{
        protected override object Evaluate(CommonRequest request)
		{
            using (var ctx = new DefaultContext())
			{
                ctx.UserRole.RemoveRange(ctx.UserRole);
				ctx.SaveChanges();
			}
			return new
			{
				success = true,
				message = "操作成功"
			};
		}
	}
    /// <summary>
    /// 删除【用户角色】
    /// </summary>
    public partial class DeleteUserRoleEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            var data = JsonConvert.DeserializeObject<UserRole>(HttpUtility.UrlDecode(request.data));
            if (data == null)
                return new CommonOutputT<string>
                {
                    success = false,
                    message = "删除成功"
                };
            using (var ctx = new DefaultContext())
            {
                var one = ctx.UserRole.Find(data.id);
				if(one==null){
					return new CommonOutputT<string>
					{
						success = false,
						message = "未找到需要删除的数据"
					};
				}
                one.IsDeleted = 1;
				ctx.UserRole.AddOrUpdate(one);
				ctx.SaveChanges();
				return new CommonOutputT<string>
				{
					success = true,
					message = "删除成功"
				};
            }
			
        }
        public override string Comments=> "删除一条用户角色记录";
    }
	
    /// <summary>
    /// 保存【用户角色】
    /// </summary>
    public partial class SaveUserRoleEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            var user = CurrentUserInformation;
            if (user==null)
            {
                return new
                {
                    success = false,
                    message = "请登录"
                };
            }
			var s = request.data;
            if (s==null)
            {
                return new
                {
                    success = false,
                    message = "缺少参数"
                };
            }
			UserRole entity = null;
			try
			{
				entity = JsonConvert.DeserializeObject<UserRole>(HttpUtility.UrlDecode(s));
			}
			catch(Exception exception)
			{
				return new
				{
					success = false,
					message = $"填写内容格式错误：{exception.Message}",
					input = s
				};
			}
            if (entity==null)
            {
                return new
                {
                    success = false,
                    message = "参数格式不正确"
                };
            }

			try
			{
				foreach (ValidationResult result in Validation.Validate(entity))
				{
					return new
					{
						success = false,
						message = result.Message
					};
				}
			}
			catch(Exception exception)
			{
				return new
				{
					success = false,
					message = exception.Message
				};
			}
			
            using (var ctx = new DefaultContext())
            {
				// 行级排他锁开始
                var transactionId = Guid.NewGuid().ToString();
                var isnew = entity.TransactionID == null;
                if (!isnew)
                {
                    var one = ctx.UserRole.FirstOrDefault(p=>p.id==entity.id);
                    if(one==null) return new
                    {
                        success = false,
                        message = "编辑错误，未找到ID"
                    };
                    if (one.VersionNo != entity.VersionNo) return new
                    {
                        success = false,
                        message = "发生数据写冲突"
                    };
                    one.VersionNo++;
                    one.TransactionID = transactionId;
					ctx.UserRole.AddOrUpdate(one);
					try
					{
						ctx.SaveChanges();
					}
					catch(Exception exception)
					{
						// 遇到数据库中的脏数据，走到这里，前面的Entity数据合法，直接跳过这里的数据校验阶段，使用最先到达的正确数据。
						// return new
						// {
						// 	 success = false,
						// 	 message = exception.Message,
						// 	 exception, one, transactionId, entity
						// };
					}
                    entity.VersionNo = one.VersionNo;
                }
				

								// NVARCHAR(50) 角色名称
				entity.URRoleName = HttpUtility.UrlDecode(entity.URRoleName);
					// NVARCHAR(50) 登录名
				entity.URLoginName = HttpUtility.UrlDecode(entity.URLoginName);
	
                entity.CreateBy = entity.CreateBy ?? user?.UILoginName ?? "未登录用户";
                entity.UpdateBy = user?.UILoginName ?? "未登录用户";
                entity.CreateOn = entity.CreateOn ?? Now;
                entity.TransactionID = transactionId;
                entity.UpdateOn = Now;
                entity.IsDeleted = 0;
	            entity.VersionNo = entity.VersionNo ?? 0;
                entity.DataLevel = entity.DataLevel ?? user?.DataLevel ?? "019999";
				ctx.UserRole.AddOrUpdate(entity);
				try
				{
					ctx.SaveChanges();
				}
				catch(Exception exception)
				{
					return new
					{
						success = false,
						message = exception.Message,
						exception, transactionId, entity
					};
				}
				// 行级排他锁结束
                return new
                {
                    success = true,
                    message = "操作成功"
                };
            }
        }
        public override string Comments=> "保存一条UserRole记录";
    }
	
    /// <summary>
    /// 查询空的【用户角色】
    /// </summary>
    public partial class GetUserRoleEmptyEvaluator:Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            return new UserRole();
        }
        public override string Comments=> "获取空的用户角色记录";
    }
	
    /// <summary>
    /// 查询【用户角色】列表
    /// </summary>
    public partial class GetUserRoleListEvaluator : Evaluator
    {
        public override string Comments=> "获取UserRole列表 ";
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
				var datalevel = CurrentUserInformation?.DataLevel;
                var searchModel = HttpUtility.UrlDecode(request.data).Deserialize<UserRoleSearchModel>() ?? new UserRoleSearchModel();
                var query = ctx.UserRole.Where(t=>t.IsDeleted==0 && t.DataLevel.StartsWith(datalevel));
                var @params = request.context.Request.Params;
                searchModel.PageSize = (@params["limit"] ?? searchModel.PageSize.ToString()).ToInt();
				searchModel.PageSize = searchModel.PageSize==0?4000:searchModel.PageSize;
                searchModel.PageIndex = (@params["offset"]).ToInt()/ searchModel.PageSize;
                var isordered = false;
                var search = searchModel.SearchKey ?? @params["search"];
                var sort = searchModel.Sort ?? @params["sort"];
				var order = @params["order"];
				// URRoleName NVARCHAR(50) 角色名称 
                if(!string.IsNullOrEmpty(searchModel.URRoleName)) query = query.Where(t=>t.URRoleName.Contains(searchModel.URRoleName));
                if(sort=="URRoleName")
                {
					query = order=="asc"?query.OrderBy(t=>t.URRoleName):query.OrderByDescending(t=>t.URRoleName);
                    isordered = true;
                }
				// URLoginName NVARCHAR(50) 登录名 
                if(!string.IsNullOrEmpty(searchModel.URLoginName)) query = query.Where(t=>t.URLoginName.Contains(searchModel.URLoginName));
                if(sort=="URLoginName")
                {
					query = order=="asc"?query.OrderBy(t=>t.URLoginName):query.OrderByDescending(t=>t.URLoginName);
                    isordered = true;
                }
				if(!string.IsNullOrEmpty(search)){
					query = query.Where(t=>t.id!=-1||t.URRoleName.Contains(search)||t.URLoginName.Contains(search));
				}
                if(sort=="ord")
                {
					query = order=="asc"?query.OrderBy(t=>t.ord):query.OrderByDescending(t=>t.ord);
                    isordered = true;
                }

                if(!isordered) query = query.OrderByDescending(t=>t.UpdateOn);
                var rows = query.Skip((searchModel.PageIndex)*searchModel.PageSize).Take(searchModel.PageSize).ToList();
                var total = query.Count();
                var sql = query.ToString();
                return new CommonOutputList<UserRole>
                {
                    success = true, rows = rows, total = total, message="查询成功"
                };
            }
        }
    }


    /// <summary>
    /// 【用户信息】7次查询分别得到七天内记录的条数，形成柱状图，折线图
    /// </summary>
    public partial class UserInformationCountEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
                var now = Parse(request.context.Request.Params["date"] ?? Now.ToString("yyyy-MM-dd"));
                var dates = Range(1, 7).Select(p => now.AddDays(-1 * p)).ToList();
                var list = dates.Select(p => ctx.UserInformation.Count(m => m.CreateOn==p)).ToList();
                return new
                {
                    sum = list.Sum(),
                    xaxis = dates.Select(p => p.ToString("yyyy-MM-dd")).ToList(),
                    series = list
                };
            }
        }
        public override string Comments=> "【用户信息】7次查询分别得到七天内记录的条数，形成柱状图，折线图";
    }
	public partial class TruncateUserInformationEvaluator : Evaluator
	{
        protected override object Evaluate(CommonRequest request)
		{
            using (var ctx = new DefaultContext())
			{
                ctx.UserInformation.RemoveRange(ctx.UserInformation);
				ctx.SaveChanges();
			}
			return new
			{
				success = true,
				message = "操作成功"
			};
		}
	}
    /// <summary>
    /// 删除【用户信息】
    /// </summary>
    public partial class DeleteUserInformationEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            var data = JsonConvert.DeserializeObject<UserInformation>(HttpUtility.UrlDecode(request.data));
            if (data == null)
                return new CommonOutputT<string>
                {
                    success = false,
                    message = "删除成功"
                };
            using (var ctx = new DefaultContext())
            {
                var one = ctx.UserInformation.Find(data.id);
				if(one==null){
					return new CommonOutputT<string>
					{
						success = false,
						message = "未找到需要删除的数据"
					};
				}
                one.IsDeleted = 1;
				ctx.UserInformation.AddOrUpdate(one);
				ctx.SaveChanges();
				return new CommonOutputT<string>
				{
					success = true,
					message = "删除成功"
				};
            }
			
        }
        public override string Comments=> "删除一条用户信息记录";
    }
	
    /// <summary>
    /// 保存【用户信息】
    /// </summary>
    public partial class SaveUserInformationEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            var user = CurrentUserInformation;
            if (user==null)
            {
                return new
                {
                    success = false,
                    message = "请登录"
                };
            }
			var s = request.data;
            if (s==null)
            {
                return new
                {
                    success = false,
                    message = "缺少参数"
                };
            }
			UserInformation entity = null;
			try
			{
				entity = JsonConvert.DeserializeObject<UserInformation>(HttpUtility.UrlDecode(s));
			}
			catch(Exception exception)
			{
				return new
				{
					success = false,
					message = $"填写内容格式错误：{exception.Message}",
					input = s
				};
			}
            if (entity==null)
            {
                return new
                {
                    success = false,
                    message = "参数格式不正确"
                };
            }

			try
			{
				foreach (ValidationResult result in Validation.Validate(entity))
				{
					return new
					{
						success = false,
						message = result.Message
					};
				}
			}
			catch(Exception exception)
			{
				return new
				{
					success = false,
					message = exception.Message
				};
			}
			
            using (var ctx = new DefaultContext())
            {
				// 行级排他锁开始
                var transactionId = Guid.NewGuid().ToString();
                var isnew = entity.TransactionID == null;
                if (!isnew)
                {
                    var one = ctx.UserInformation.FirstOrDefault(p=>p.id==entity.id);
                    if(one==null) return new
                    {
                        success = false,
                        message = "编辑错误，未找到ID"
                    };
                    if (one.VersionNo != entity.VersionNo) return new
                    {
                        success = false,
                        message = "发生数据写冲突"
                    };
                    one.VersionNo++;
                    one.TransactionID = transactionId;
					ctx.UserInformation.AddOrUpdate(one);
					try
					{
						ctx.SaveChanges();
					}
					catch(Exception exception)
					{
						// 遇到数据库中的脏数据，走到这里，前面的Entity数据合法，直接跳过这里的数据校验阶段，使用最先到达的正确数据。
						// return new
						// {
						// 	 success = false,
						// 	 message = exception.Message,
						// 	 exception, one, transactionId, entity
						// };
					}
                    entity.VersionNo = one.VersionNo;
                }
				

								// NVARCHAR(50) 工号
				entity.UIJobNumber = HttpUtility.UrlDecode(entity.UIJobNumber);
					// NVARCHAR(50) 登录名
				entity.UILoginName = HttpUtility.UrlDecode(entity.UILoginName);
					// NVARCHAR(50) 昵称
				entity.UINickname = HttpUtility.UrlDecode(entity.UINickname);
					// NVARCHAR(50) 真实姓名
				entity.UIRealName = HttpUtility.UrlDecode(entity.UIRealName);
					// NVARCHAR(50) 头像
				entity.UIHeadPortrait = HttpUtility.UrlDecode(entity.UIHeadPortrait);
					// NVARCHAR(50) 部门
				entity.UIDepartment = HttpUtility.UrlDecode(entity.UIDepartment);
					// NVARCHAR(50) 职位
				entity.UIPost = HttpUtility.UrlDecode(entity.UIPost);
					// NVARCHAR(50) 电话
				entity.UIBooth = HttpUtility.UrlDecode(entity.UIBooth);
					// NVARCHAR(50) 照片
				entity.UIPhoto = HttpUtility.UrlDecode(entity.UIPhoto);
					// NVARCHAR(50) 用户类型
				entity.UICustomerType = HttpUtility.UrlDecode(entity.UICustomerType);
					// NVARCHAR(50) 用户级别
				entity.UIUserLevel = HttpUtility.UrlDecode(entity.UIUserLevel);
					// NVARCHAR(50) 密码
				entity.UICode = HttpUtility.UrlDecode(entity.UICode);
	
                entity.CreateBy = entity.CreateBy ?? user?.UILoginName ?? "未登录用户";
                entity.UpdateBy = user?.UILoginName ?? "未登录用户";
                entity.CreateOn = entity.CreateOn ?? Now;
                entity.TransactionID = transactionId;
                entity.UpdateOn = Now;
                entity.IsDeleted = 0;
	            entity.VersionNo = entity.VersionNo ?? 0;
                entity.DataLevel = entity.DataLevel ?? user?.DataLevel ?? "019999";
				ctx.UserInformation.AddOrUpdate(entity);
				try
				{
					ctx.SaveChanges();
				}
				catch(Exception exception)
				{
					return new
					{
						success = false,
						message = exception.Message,
						exception, transactionId, entity
					};
				}
				// 行级排他锁结束
                return new
                {
                    success = true,
                    message = "操作成功"
                };
            }
        }
        public override string Comments=> "保存一条UserInformation记录";
    }
	
    /// <summary>
    /// 查询空的【用户信息】
    /// </summary>
    public partial class GetUserInformationEmptyEvaluator:Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            return new UserInformation();
        }
        public override string Comments=> "获取空的用户信息记录";
    }
	
    /// <summary>
    /// 查询【用户信息】列表
    /// </summary>
    public partial class GetUserInformationListEvaluator : Evaluator
    {
        public override string Comments=> "获取UserInformation列表 ";
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
				var datalevel = CurrentUserInformation?.DataLevel;
                var searchModel = HttpUtility.UrlDecode(request.data).Deserialize<UserInformationSearchModel>() ?? new UserInformationSearchModel();
                var query = ctx.UserInformation.Where(t=>t.IsDeleted==0 && t.DataLevel.StartsWith(datalevel));
                var @params = request.context.Request.Params;
                searchModel.PageSize = (@params["limit"] ?? searchModel.PageSize.ToString()).ToInt();
				searchModel.PageSize = searchModel.PageSize==0?4000:searchModel.PageSize;
                searchModel.PageIndex = (@params["offset"]).ToInt()/ searchModel.PageSize;
                var isordered = false;
                var search = searchModel.SearchKey ?? @params["search"];
                var sort = searchModel.Sort ?? @params["sort"];
				var order = @params["order"];
				// UIJobNumber NVARCHAR(50) 工号 
                if(!string.IsNullOrEmpty(searchModel.UIJobNumber)) query = query.Where(t=>t.UIJobNumber.Contains(searchModel.UIJobNumber));
                if(sort=="UIJobNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.UIJobNumber):query.OrderByDescending(t=>t.UIJobNumber);
                    isordered = true;
                }
				// UILoginName NVARCHAR(50) 登录名 
                if(!string.IsNullOrEmpty(searchModel.UILoginName)) query = query.Where(t=>t.UILoginName.Contains(searchModel.UILoginName));
                if(sort=="UILoginName")
                {
					query = order=="asc"?query.OrderBy(t=>t.UILoginName):query.OrderByDescending(t=>t.UILoginName);
                    isordered = true;
                }
				// UINickname NVARCHAR(50) 昵称 
                if(!string.IsNullOrEmpty(searchModel.UINickname)) query = query.Where(t=>t.UINickname.Contains(searchModel.UINickname));
                if(sort=="UINickname")
                {
					query = order=="asc"?query.OrderBy(t=>t.UINickname):query.OrderByDescending(t=>t.UINickname);
                    isordered = true;
                }
				// UIRealName NVARCHAR(50) 真实姓名 
                if(!string.IsNullOrEmpty(searchModel.UIRealName)) query = query.Where(t=>t.UIRealName.Contains(searchModel.UIRealName));
                if(sort=="UIRealName")
                {
					query = order=="asc"?query.OrderBy(t=>t.UIRealName):query.OrderByDescending(t=>t.UIRealName);
                    isordered = true;
                }
				// UIHeadPortrait NVARCHAR(50) 头像 
                if(!string.IsNullOrEmpty(searchModel.UIHeadPortrait)) query = query.Where(t=>t.UIHeadPortrait.Contains(searchModel.UIHeadPortrait));
                if(sort=="UIHeadPortrait")
                {
					query = order=="asc"?query.OrderBy(t=>t.UIHeadPortrait):query.OrderByDescending(t=>t.UIHeadPortrait);
                    isordered = true;
                }
				// UIDepartment NVARCHAR(50) 部门 
                if(!string.IsNullOrEmpty(searchModel.UIDepartment)) query = query.Where(t=>t.UIDepartment.Contains(searchModel.UIDepartment));
                if(sort=="UIDepartment")
                {
					query = order=="asc"?query.OrderBy(t=>t.UIDepartment):query.OrderByDescending(t=>t.UIDepartment);
                    isordered = true;
                }
				// UIPost NVARCHAR(50) 职位 
                if(!string.IsNullOrEmpty(searchModel.UIPost)) query = query.Where(t=>t.UIPost.Contains(searchModel.UIPost));
                if(sort=="UIPost")
                {
					query = order=="asc"?query.OrderBy(t=>t.UIPost):query.OrderByDescending(t=>t.UIPost);
                    isordered = true;
                }
				// UIBooth NVARCHAR(50) 电话 
                if(!string.IsNullOrEmpty(searchModel.UIBooth)) query = query.Where(t=>t.UIBooth.Contains(searchModel.UIBooth));
                if(sort=="UIBooth")
                {
					query = order=="asc"?query.OrderBy(t=>t.UIBooth):query.OrderByDescending(t=>t.UIBooth);
                    isordered = true;
                }
				// UIPhoto NVARCHAR(50) 照片 
                if(!string.IsNullOrEmpty(searchModel.UIPhoto)) query = query.Where(t=>t.UIPhoto.Contains(searchModel.UIPhoto));
                if(sort=="UIPhoto")
                {
					query = order=="asc"?query.OrderBy(t=>t.UIPhoto):query.OrderByDescending(t=>t.UIPhoto);
                    isordered = true;
                }
				// UICustomerType NVARCHAR(50) 用户类型 
                if(!string.IsNullOrEmpty(searchModel.UICustomerType)) query = query.Where(t=>t.UICustomerType.Contains(searchModel.UICustomerType));
                if(sort=="UICustomerType")
                {
					query = order=="asc"?query.OrderBy(t=>t.UICustomerType):query.OrderByDescending(t=>t.UICustomerType);
                    isordered = true;
                }
				// UIUserLevel NVARCHAR(50) 用户级别 
                if(!string.IsNullOrEmpty(searchModel.UIUserLevel)) query = query.Where(t=>t.UIUserLevel.Contains(searchModel.UIUserLevel));
                if(sort=="UIUserLevel")
                {
					query = order=="asc"?query.OrderBy(t=>t.UIUserLevel):query.OrderByDescending(t=>t.UIUserLevel);
                    isordered = true;
                }
				// UITimeOfEntry DATETIME 入职时间 
                if(searchModel.FromUITimeOfEntry!=null) query = query.Where(t=>t.UITimeOfEntry>=searchModel.FromUITimeOfEntry);
                if(searchModel.ToUITimeOfEntry!=null) query = query.Where(t=>t.UITimeOfEntry<=searchModel.ToUITimeOfEntry);
                if(sort=="UITimeOfEntry")
                {
					query = order=="asc"?query.OrderBy(t=>t.UITimeOfEntry):query.OrderByDescending(t=>t.UITimeOfEntry);
                    isordered = true;
                }
				// UIDepartureTime DATETIME 离职时间 
                if(searchModel.FromUIDepartureTime!=null) query = query.Where(t=>t.UIDepartureTime>=searchModel.FromUIDepartureTime);
                if(searchModel.ToUIDepartureTime!=null) query = query.Where(t=>t.UIDepartureTime<=searchModel.ToUIDepartureTime);
                if(sort=="UIDepartureTime")
                {
					query = order=="asc"?query.OrderBy(t=>t.UIDepartureTime):query.OrderByDescending(t=>t.UIDepartureTime);
                    isordered = true;
                }
				// UICode NVARCHAR(50) 密码 
                if(!string.IsNullOrEmpty(searchModel.UICode)) query = query.Where(t=>t.UICode.Contains(searchModel.UICode));
                if(sort=="UICode")
                {
					query = order=="asc"?query.OrderBy(t=>t.UICode):query.OrderByDescending(t=>t.UICode);
                    isordered = true;
                }
				if(!string.IsNullOrEmpty(search)){
					query = query.Where(t=>t.id!=-1||t.UIJobNumber.Contains(search)||t.UILoginName.Contains(search)||t.UINickname.Contains(search)||t.UIRealName.Contains(search)||t.UIHeadPortrait.Contains(search)||t.UIDepartment.Contains(search)||t.UIPost.Contains(search)||t.UIBooth.Contains(search)||t.UIPhoto.Contains(search)||t.UICustomerType.Contains(search)||t.UIUserLevel.Contains(search)||t.UICode.Contains(search));
				}
                if(sort=="ord")
                {
					query = order=="asc"?query.OrderBy(t=>t.ord):query.OrderByDescending(t=>t.ord);
                    isordered = true;
                }

                if(!isordered) query = query.OrderByDescending(t=>t.UpdateOn);
                var rows = query.Skip((searchModel.PageIndex)*searchModel.PageSize).Take(searchModel.PageSize).ToList();
                var total = query.Count();
                var sql = query.ToString();
                return new CommonOutputList<UserInformation>
                {
                    success = true, rows = rows, total = total, message="查询成功"
                };
            }
        }
    }


    /// <summary>
    /// 【菜单配置】7次查询分别得到七天内记录的条数，形成柱状图，折线图
    /// </summary>
    public partial class MenuConfigurationCountEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
                var now = Parse(request.context.Request.Params["date"] ?? Now.ToString("yyyy-MM-dd"));
                var dates = Range(1, 7).Select(p => now.AddDays(-1 * p)).ToList();
                var list = dates.Select(p => ctx.MenuConfiguration.Count(m => m.CreateOn==p)).ToList();
                return new
                {
                    sum = list.Sum(),
                    xaxis = dates.Select(p => p.ToString("yyyy-MM-dd")).ToList(),
                    series = list
                };
            }
        }
        public override string Comments=> "【菜单配置】7次查询分别得到七天内记录的条数，形成柱状图，折线图";
    }
	public partial class TruncateMenuConfigurationEvaluator : Evaluator
	{
        protected override object Evaluate(CommonRequest request)
		{
            using (var ctx = new DefaultContext())
			{
                ctx.MenuConfiguration.RemoveRange(ctx.MenuConfiguration);
				ctx.SaveChanges();
			}
			return new
			{
				success = true,
				message = "操作成功"
			};
		}
	}
    /// <summary>
    /// 删除【菜单配置】
    /// </summary>
    public partial class DeleteMenuConfigurationEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            var data = JsonConvert.DeserializeObject<MenuConfiguration>(HttpUtility.UrlDecode(request.data));
            if (data == null)
                return new CommonOutputT<string>
                {
                    success = false,
                    message = "删除成功"
                };
            using (var ctx = new DefaultContext())
            {
                var one = ctx.MenuConfiguration.Find(data.id);
				if(one==null){
					return new CommonOutputT<string>
					{
						success = false,
						message = "未找到需要删除的数据"
					};
				}
                one.IsDeleted = 1;
				ctx.MenuConfiguration.AddOrUpdate(one);
				ctx.SaveChanges();
				return new CommonOutputT<string>
				{
					success = true,
					message = "删除成功"
				};
            }
			
        }
        public override string Comments=> "删除一条菜单配置记录";
    }
	
    /// <summary>
    /// 保存【菜单配置】
    /// </summary>
    public partial class SaveMenuConfigurationEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            var user = CurrentUserInformation;
            if (user==null)
            {
                return new
                {
                    success = false,
                    message = "请登录"
                };
            }
			var s = request.data;
            if (s==null)
            {
                return new
                {
                    success = false,
                    message = "缺少参数"
                };
            }
			MenuConfiguration entity = null;
			try
			{
				entity = JsonConvert.DeserializeObject<MenuConfiguration>(HttpUtility.UrlDecode(s));
			}
			catch(Exception exception)
			{
				return new
				{
					success = false,
					message = $"填写内容格式错误：{exception.Message}",
					input = s
				};
			}
            if (entity==null)
            {
                return new
                {
                    success = false,
                    message = "参数格式不正确"
                };
            }

			try
			{
				foreach (ValidationResult result in Validation.Validate(entity))
				{
					return new
					{
						success = false,
						message = result.Message
					};
				}
			}
			catch(Exception exception)
			{
				return new
				{
					success = false,
					message = exception.Message
				};
			}
			
            using (var ctx = new DefaultContext())
            {
				// 行级排他锁开始
                var transactionId = Guid.NewGuid().ToString();
                var isnew = entity.TransactionID == null;
                if (!isnew)
                {
                    var one = ctx.MenuConfiguration.FirstOrDefault(p=>p.id==entity.id);
                    if(one==null) return new
                    {
                        success = false,
                        message = "编辑错误，未找到ID"
                    };
                    if (one.VersionNo != entity.VersionNo) return new
                    {
                        success = false,
                        message = "发生数据写冲突"
                    };
                    one.VersionNo++;
                    one.TransactionID = transactionId;
					ctx.MenuConfiguration.AddOrUpdate(one);
					try
					{
						ctx.SaveChanges();
					}
					catch(Exception exception)
					{
						// 遇到数据库中的脏数据，走到这里，前面的Entity数据合法，直接跳过这里的数据校验阶段，使用最先到达的正确数据。
						// return new
						// {
						// 	 success = false,
						// 	 message = exception.Message,
						// 	 exception, one, transactionId, entity
						// };
					}
                    entity.VersionNo = one.VersionNo;
                }
				

								// NVARCHAR(50) 标题
				entity.MCCaption = HttpUtility.UrlDecode(entity.MCCaption);
					// NVARCHAR(50) 父级标题
				entity.MCParentTitle = HttpUtility.UrlDecode(entity.MCParentTitle);
					// NVARCHAR(50) 链接
				entity.MCLink = HttpUtility.UrlDecode(entity.MCLink);
					// NVARCHAR(50) 菜单类型
				entity.MCMenuType = HttpUtility.UrlDecode(entity.MCMenuType);
					// NVARCHAR(50) 显示名称
				entity.MCDisplayName = HttpUtility.UrlDecode(entity.MCDisplayName);
					// NVARCHAR(50) 图片
				entity.MCPicture = HttpUtility.UrlDecode(entity.MCPicture);
	
                entity.CreateBy = entity.CreateBy ?? user?.UILoginName ?? "未登录用户";
                entity.UpdateBy = user?.UILoginName ?? "未登录用户";
                entity.CreateOn = entity.CreateOn ?? Now;
                entity.TransactionID = transactionId;
                entity.UpdateOn = Now;
                entity.IsDeleted = 0;
	            entity.VersionNo = entity.VersionNo ?? 0;
                entity.DataLevel = entity.DataLevel ?? user?.DataLevel ?? "019999";
				ctx.MenuConfiguration.AddOrUpdate(entity);
				try
				{
					ctx.SaveChanges();
				}
				catch(Exception exception)
				{
					return new
					{
						success = false,
						message = exception.Message,
						exception, transactionId, entity
					};
				}
				// 行级排他锁结束
                return new
                {
                    success = true,
                    message = "操作成功"
                };
            }
        }
        public override string Comments=> "保存一条MenuConfiguration记录";
    }
	
    /// <summary>
    /// 查询空的【菜单配置】
    /// </summary>
    public partial class GetMenuConfigurationEmptyEvaluator:Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            return new MenuConfiguration();
        }
        public override string Comments=> "获取空的菜单配置记录";
    }
	
    /// <summary>
    /// 查询【菜单配置】列表
    /// </summary>
    public partial class GetMenuConfigurationListEvaluator : Evaluator
    {
        public override string Comments=> "获取MenuConfiguration列表 ";
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
				var datalevel = CurrentUserInformation?.DataLevel;
                var searchModel = HttpUtility.UrlDecode(request.data).Deserialize<MenuConfigurationSearchModel>() ?? new MenuConfigurationSearchModel();
                var query = ctx.MenuConfiguration.Where(t=>t.IsDeleted==0 && t.DataLevel.StartsWith(datalevel));
                var @params = request.context.Request.Params;
                searchModel.PageSize = (@params["limit"] ?? searchModel.PageSize.ToString()).ToInt();
				searchModel.PageSize = searchModel.PageSize==0?4000:searchModel.PageSize;
                searchModel.PageIndex = (@params["offset"]).ToInt()/ searchModel.PageSize;
                var isordered = false;
                var search = searchModel.SearchKey ?? @params["search"];
                var sort = searchModel.Sort ?? @params["sort"];
				var order = @params["order"];
				// MCCaption NVARCHAR(50) 标题 
                if(!string.IsNullOrEmpty(searchModel.MCCaption)) query = query.Where(t=>t.MCCaption.Contains(searchModel.MCCaption));
                if(sort=="MCCaption")
                {
					query = order=="asc"?query.OrderBy(t=>t.MCCaption):query.OrderByDescending(t=>t.MCCaption);
                    isordered = true;
                }
				// MCParentTitle NVARCHAR(50) 父级标题 
                if(!string.IsNullOrEmpty(searchModel.MCParentTitle)) query = query.Where(t=>t.MCParentTitle.Contains(searchModel.MCParentTitle));
                if(sort=="MCParentTitle")
                {
					query = order=="asc"?query.OrderBy(t=>t.MCParentTitle):query.OrderByDescending(t=>t.MCParentTitle);
                    isordered = true;
                }
				// MCLink NVARCHAR(50) 链接 
                if(!string.IsNullOrEmpty(searchModel.MCLink)) query = query.Where(t=>t.MCLink.Contains(searchModel.MCLink));
                if(sort=="MCLink")
                {
					query = order=="asc"?query.OrderBy(t=>t.MCLink):query.OrderByDescending(t=>t.MCLink);
                    isordered = true;
                }
				// MCMenuType NVARCHAR(50) 菜单类型 
                if(!string.IsNullOrEmpty(searchModel.MCMenuType)) query = query.Where(t=>t.MCMenuType.Contains(searchModel.MCMenuType));
                if(sort=="MCMenuType")
                {
					query = order=="asc"?query.OrderBy(t=>t.MCMenuType):query.OrderByDescending(t=>t.MCMenuType);
                    isordered = true;
                }
				// MCSequence INT 顺序 
                if(searchModel.MinMCSequence!=null) query = query.Where(t=>t.MCSequence>=searchModel.MinMCSequence);
                if(searchModel.MaxMCSequence!=null) query = query.Where(t=>t.MCSequence<=searchModel.MaxMCSequence);
                if(sort=="MCSequence")
                {
					query = order=="asc"?query.OrderBy(t=>t.MCSequence):query.OrderByDescending(t=>t.MCSequence);
                    isordered = true;
                }
				// MCDisplayName NVARCHAR(50) 显示名称 
                if(!string.IsNullOrEmpty(searchModel.MCDisplayName)) query = query.Where(t=>t.MCDisplayName.Contains(searchModel.MCDisplayName));
                if(sort=="MCDisplayName")
                {
					query = order=="asc"?query.OrderBy(t=>t.MCDisplayName):query.OrderByDescending(t=>t.MCDisplayName);
                    isordered = true;
                }
				// MCPicture NVARCHAR(50) 图片 
                if(!string.IsNullOrEmpty(searchModel.MCPicture)) query = query.Where(t=>t.MCPicture.Contains(searchModel.MCPicture));
                if(sort=="MCPicture")
                {
					query = order=="asc"?query.OrderBy(t=>t.MCPicture):query.OrderByDescending(t=>t.MCPicture);
                    isordered = true;
                }
				if(!string.IsNullOrEmpty(search)){
					query = query.Where(t=>t.id!=-1||t.MCCaption.Contains(search)||t.MCParentTitle.Contains(search)||t.MCLink.Contains(search)||t.MCMenuType.Contains(search)||t.MCDisplayName.Contains(search)||t.MCPicture.Contains(search));
				}
                if(sort=="ord")
                {
					query = order=="asc"?query.OrderBy(t=>t.ord):query.OrderByDescending(t=>t.ord);
                    isordered = true;
                }

                if(!isordered) query = query.OrderByDescending(t=>t.UpdateOn);
                var rows = query.Skip((searchModel.PageIndex)*searchModel.PageSize).Take(searchModel.PageSize).ToList();
                var total = query.Count();
                var sql = query.ToString();
                return new CommonOutputList<MenuConfiguration>
                {
                    success = true, rows = rows, total = total, message="查询成功"
                };
            }
        }
    }


    /// <summary>
    /// 【角色菜单】7次查询分别得到七天内记录的条数，形成柱状图，折线图
    /// </summary>
    public partial class RoleMenuCountEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
                var now = Parse(request.context.Request.Params["date"] ?? Now.ToString("yyyy-MM-dd"));
                var dates = Range(1, 7).Select(p => now.AddDays(-1 * p)).ToList();
                var list = dates.Select(p => ctx.RoleMenu.Count(m => m.CreateOn==p)).ToList();
                return new
                {
                    sum = list.Sum(),
                    xaxis = dates.Select(p => p.ToString("yyyy-MM-dd")).ToList(),
                    series = list
                };
            }
        }
        public override string Comments=> "【角色菜单】7次查询分别得到七天内记录的条数，形成柱状图，折线图";
    }
	public partial class TruncateRoleMenuEvaluator : Evaluator
	{
        protected override object Evaluate(CommonRequest request)
		{
            using (var ctx = new DefaultContext())
			{
                ctx.RoleMenu.RemoveRange(ctx.RoleMenu);
				ctx.SaveChanges();
			}
			return new
			{
				success = true,
				message = "操作成功"
			};
		}
	}
    /// <summary>
    /// 删除【角色菜单】
    /// </summary>
    public partial class DeleteRoleMenuEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            var data = JsonConvert.DeserializeObject<RoleMenu>(HttpUtility.UrlDecode(request.data));
            if (data == null)
                return new CommonOutputT<string>
                {
                    success = false,
                    message = "删除成功"
                };
            using (var ctx = new DefaultContext())
            {
                var one = ctx.RoleMenu.Find(data.id);
				if(one==null){
					return new CommonOutputT<string>
					{
						success = false,
						message = "未找到需要删除的数据"
					};
				}
                one.IsDeleted = 1;
				ctx.RoleMenu.AddOrUpdate(one);
				ctx.SaveChanges();
				return new CommonOutputT<string>
				{
					success = true,
					message = "删除成功"
				};
            }
			
        }
        public override string Comments=> "删除一条角色菜单记录";
    }
	
    /// <summary>
    /// 保存【角色菜单】
    /// </summary>
    public partial class SaveRoleMenuEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            var user = CurrentUserInformation;
            if (user==null)
            {
                return new
                {
                    success = false,
                    message = "请登录"
                };
            }
			var s = request.data;
            if (s==null)
            {
                return new
                {
                    success = false,
                    message = "缺少参数"
                };
            }
			RoleMenu entity = null;
			try
			{
				entity = JsonConvert.DeserializeObject<RoleMenu>(HttpUtility.UrlDecode(s));
			}
			catch(Exception exception)
			{
				return new
				{
					success = false,
					message = $"填写内容格式错误：{exception.Message}",
					input = s
				};
			}
            if (entity==null)
            {
                return new
                {
                    success = false,
                    message = "参数格式不正确"
                };
            }

			try
			{
				foreach (ValidationResult result in Validation.Validate(entity))
				{
					return new
					{
						success = false,
						message = result.Message
					};
				}
			}
			catch(Exception exception)
			{
				return new
				{
					success = false,
					message = exception.Message
				};
			}
			
            using (var ctx = new DefaultContext())
            {
				// 行级排他锁开始
                var transactionId = Guid.NewGuid().ToString();
                var isnew = entity.TransactionID == null;
                if (!isnew)
                {
                    var one = ctx.RoleMenu.FirstOrDefault(p=>p.id==entity.id);
                    if(one==null) return new
                    {
                        success = false,
                        message = "编辑错误，未找到ID"
                    };
                    if (one.VersionNo != entity.VersionNo) return new
                    {
                        success = false,
                        message = "发生数据写冲突"
                    };
                    one.VersionNo++;
                    one.TransactionID = transactionId;
					ctx.RoleMenu.AddOrUpdate(one);
					try
					{
						ctx.SaveChanges();
					}
					catch(Exception exception)
					{
						// 遇到数据库中的脏数据，走到这里，前面的Entity数据合法，直接跳过这里的数据校验阶段，使用最先到达的正确数据。
						// return new
						// {
						// 	 success = false,
						// 	 message = exception.Message,
						// 	 exception, one, transactionId, entity
						// };
					}
                    entity.VersionNo = one.VersionNo;
                }
				

								// NVARCHAR(50) 角色名称
				entity.RMRoleName = HttpUtility.UrlDecode(entity.RMRoleName);
					// NVARCHAR(50) 菜单标题
				entity.RMMenuTitle = HttpUtility.UrlDecode(entity.RMMenuTitle);
	
                entity.CreateBy = entity.CreateBy ?? user?.UILoginName ?? "未登录用户";
                entity.UpdateBy = user?.UILoginName ?? "未登录用户";
                entity.CreateOn = entity.CreateOn ?? Now;
                entity.TransactionID = transactionId;
                entity.UpdateOn = Now;
                entity.IsDeleted = 0;
	            entity.VersionNo = entity.VersionNo ?? 0;
                entity.DataLevel = entity.DataLevel ?? user?.DataLevel ?? "019999";
				ctx.RoleMenu.AddOrUpdate(entity);
				try
				{
					ctx.SaveChanges();
				}
				catch(Exception exception)
				{
					return new
					{
						success = false,
						message = exception.Message,
						exception, transactionId, entity
					};
				}
				// 行级排他锁结束
                return new
                {
                    success = true,
                    message = "操作成功"
                };
            }
        }
        public override string Comments=> "保存一条RoleMenu记录";
    }
	
    /// <summary>
    /// 查询空的【角色菜单】
    /// </summary>
    public partial class GetRoleMenuEmptyEvaluator:Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            return new RoleMenu();
        }
        public override string Comments=> "获取空的角色菜单记录";
    }
	
    /// <summary>
    /// 查询【角色菜单】列表
    /// </summary>
    public partial class GetRoleMenuListEvaluator : Evaluator
    {
        public override string Comments=> "获取RoleMenu列表 ";
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
				var datalevel = CurrentUserInformation?.DataLevel;
                var searchModel = HttpUtility.UrlDecode(request.data).Deserialize<RoleMenuSearchModel>() ?? new RoleMenuSearchModel();
                var query = ctx.RoleMenu.Where(t=>t.IsDeleted==0 && t.DataLevel.StartsWith(datalevel));
                var @params = request.context.Request.Params;
                searchModel.PageSize = (@params["limit"] ?? searchModel.PageSize.ToString()).ToInt();
				searchModel.PageSize = searchModel.PageSize==0?4000:searchModel.PageSize;
                searchModel.PageIndex = (@params["offset"]).ToInt()/ searchModel.PageSize;
                var isordered = false;
                var search = searchModel.SearchKey ?? @params["search"];
                var sort = searchModel.Sort ?? @params["sort"];
				var order = @params["order"];
				// RMRoleName NVARCHAR(50) 角色名称 
                if(!string.IsNullOrEmpty(searchModel.RMRoleName)) query = query.Where(t=>t.RMRoleName.Contains(searchModel.RMRoleName));
                if(sort=="RMRoleName")
                {
					query = order=="asc"?query.OrderBy(t=>t.RMRoleName):query.OrderByDescending(t=>t.RMRoleName);
                    isordered = true;
                }
				// RMMenuTitle NVARCHAR(50) 菜单标题 
                if(!string.IsNullOrEmpty(searchModel.RMMenuTitle)) query = query.Where(t=>t.RMMenuTitle.Contains(searchModel.RMMenuTitle));
                if(sort=="RMMenuTitle")
                {
					query = order=="asc"?query.OrderBy(t=>t.RMMenuTitle):query.OrderByDescending(t=>t.RMMenuTitle);
                    isordered = true;
                }
				if(!string.IsNullOrEmpty(search)){
					query = query.Where(t=>t.id!=-1||t.RMRoleName.Contains(search)||t.RMMenuTitle.Contains(search));
				}
                if(sort=="ord")
                {
					query = order=="asc"?query.OrderBy(t=>t.ord):query.OrderByDescending(t=>t.ord);
                    isordered = true;
                }

                if(!isordered) query = query.OrderByDescending(t=>t.UpdateOn);
                var rows = query.Skip((searchModel.PageIndex)*searchModel.PageSize).Take(searchModel.PageSize).ToList();
                var total = query.Count();
                var sql = query.ToString();
                return new CommonOutputList<RoleMenu>
                {
                    success = true, rows = rows, total = total, message="查询成功"
                };
            }
        }
    }


    /// <summary>
    /// 【登录记录】7次查询分别得到七天内记录的条数，形成柱状图，折线图
    /// </summary>
    public partial class LogonRecordCountEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
                var now = Parse(request.context.Request.Params["date"] ?? Now.ToString("yyyy-MM-dd"));
                var dates = Range(1, 7).Select(p => now.AddDays(-1 * p)).ToList();
                var list = dates.Select(p => ctx.LogonRecord.Count(m => m.CreateOn==p)).ToList();
                return new
                {
                    sum = list.Sum(),
                    xaxis = dates.Select(p => p.ToString("yyyy-MM-dd")).ToList(),
                    series = list
                };
            }
        }
        public override string Comments=> "【登录记录】7次查询分别得到七天内记录的条数，形成柱状图，折线图";
    }
	public partial class TruncateLogonRecordEvaluator : Evaluator
	{
        protected override object Evaluate(CommonRequest request)
		{
            using (var ctx = new DefaultContext())
			{
                ctx.LogonRecord.RemoveRange(ctx.LogonRecord);
				ctx.SaveChanges();
			}
			return new
			{
				success = true,
				message = "操作成功"
			};
		}
	}
    /// <summary>
    /// 删除【登录记录】
    /// </summary>
    public partial class DeleteLogonRecordEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            var data = JsonConvert.DeserializeObject<LogonRecord>(HttpUtility.UrlDecode(request.data));
            if (data == null)
                return new CommonOutputT<string>
                {
                    success = false,
                    message = "删除成功"
                };
            using (var ctx = new DefaultContext())
            {
                var one = ctx.LogonRecord.Find(data.id);
				if(one==null){
					return new CommonOutputT<string>
					{
						success = false,
						message = "未找到需要删除的数据"
					};
				}
                one.IsDeleted = 1;
				ctx.LogonRecord.AddOrUpdate(one);
				ctx.SaveChanges();
				return new CommonOutputT<string>
				{
					success = true,
					message = "删除成功"
				};
            }
			
        }
        public override string Comments=> "删除一条登录记录记录";
    }
	
    /// <summary>
    /// 保存【登录记录】
    /// </summary>
    public partial class SaveLogonRecordEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            var user = CurrentUserInformation;
            if (user==null)
            {
                return new
                {
                    success = false,
                    message = "请登录"
                };
            }
			var s = request.data;
            if (s==null)
            {
                return new
                {
                    success = false,
                    message = "缺少参数"
                };
            }
			LogonRecord entity = null;
			try
			{
				entity = JsonConvert.DeserializeObject<LogonRecord>(HttpUtility.UrlDecode(s));
			}
			catch(Exception exception)
			{
				return new
				{
					success = false,
					message = $"填写内容格式错误：{exception.Message}",
					input = s
				};
			}
            if (entity==null)
            {
                return new
                {
                    success = false,
                    message = "参数格式不正确"
                };
            }

			try
			{
				foreach (ValidationResult result in Validation.Validate(entity))
				{
					return new
					{
						success = false,
						message = result.Message
					};
				}
			}
			catch(Exception exception)
			{
				return new
				{
					success = false,
					message = exception.Message
				};
			}
			
            using (var ctx = new DefaultContext())
            {
				// 行级排他锁开始
                var transactionId = Guid.NewGuid().ToString();
                var isnew = entity.TransactionID == null;
                if (!isnew)
                {
                    var one = ctx.LogonRecord.FirstOrDefault(p=>p.id==entity.id);
                    if(one==null) return new
                    {
                        success = false,
                        message = "编辑错误，未找到ID"
                    };
                    if (one.VersionNo != entity.VersionNo) return new
                    {
                        success = false,
                        message = "发生数据写冲突"
                    };
                    one.VersionNo++;
                    one.TransactionID = transactionId;
					ctx.LogonRecord.AddOrUpdate(one);
					try
					{
						ctx.SaveChanges();
					}
					catch(Exception exception)
					{
						// 遇到数据库中的脏数据，走到这里，前面的Entity数据合法，直接跳过这里的数据校验阶段，使用最先到达的正确数据。
						// return new
						// {
						// 	 success = false,
						// 	 message = exception.Message,
						// 	 exception, one, transactionId, entity
						// };
					}
                    entity.VersionNo = one.VersionNo;
                }
				

								// NVARCHAR(50) 登录名
				entity.LRLoginName = HttpUtility.UrlDecode(entity.LRLoginName);
	
                entity.CreateBy = entity.CreateBy ?? user?.UILoginName ?? "未登录用户";
                entity.UpdateBy = user?.UILoginName ?? "未登录用户";
                entity.CreateOn = entity.CreateOn ?? Now;
                entity.TransactionID = transactionId;
                entity.UpdateOn = Now;
                entity.IsDeleted = 0;
	            entity.VersionNo = entity.VersionNo ?? 0;
                entity.DataLevel = entity.DataLevel ?? user?.DataLevel ?? "019999";
				ctx.LogonRecord.AddOrUpdate(entity);
				try
				{
					ctx.SaveChanges();
				}
				catch(Exception exception)
				{
					return new
					{
						success = false,
						message = exception.Message,
						exception, transactionId, entity
					};
				}
				// 行级排他锁结束
                return new
                {
                    success = true,
                    message = "操作成功"
                };
            }
        }
        public override string Comments=> "保存一条LogonRecord记录";
    }
	
    /// <summary>
    /// 查询空的【登录记录】
    /// </summary>
    public partial class GetLogonRecordEmptyEvaluator:Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            return new LogonRecord();
        }
        public override string Comments=> "获取空的登录记录记录";
    }
	
    /// <summary>
    /// 查询【登录记录】列表
    /// </summary>
    public partial class GetLogonRecordListEvaluator : Evaluator
    {
        public override string Comments=> "获取LogonRecord列表 ";
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
				var datalevel = CurrentUserInformation?.DataLevel;
                var searchModel = HttpUtility.UrlDecode(request.data).Deserialize<LogonRecordSearchModel>() ?? new LogonRecordSearchModel();
                var query = ctx.LogonRecord.Where(t=>t.IsDeleted==0 && t.DataLevel.StartsWith(datalevel));
                var @params = request.context.Request.Params;
                searchModel.PageSize = (@params["limit"] ?? searchModel.PageSize.ToString()).ToInt();
				searchModel.PageSize = searchModel.PageSize==0?4000:searchModel.PageSize;
                searchModel.PageIndex = (@params["offset"]).ToInt()/ searchModel.PageSize;
                var isordered = false;
                var search = searchModel.SearchKey ?? @params["search"];
                var sort = searchModel.Sort ?? @params["sort"];
				var order = @params["order"];
				// LRLoginName NVARCHAR(50) 登录名 
                if(!string.IsNullOrEmpty(searchModel.LRLoginName)) query = query.Where(t=>t.LRLoginName.Contains(searchModel.LRLoginName));
                if(sort=="LRLoginName")
                {
					query = order=="asc"?query.OrderBy(t=>t.LRLoginName):query.OrderByDescending(t=>t.LRLoginName);
                    isordered = true;
                }
				// LRLoginTime DATETIME 登录时间 
                if(searchModel.FromLRLoginTime!=null) query = query.Where(t=>t.LRLoginTime>=searchModel.FromLRLoginTime);
                if(searchModel.ToLRLoginTime!=null) query = query.Where(t=>t.LRLoginTime<=searchModel.ToLRLoginTime);
                if(sort=="LRLoginTime")
                {
					query = order=="asc"?query.OrderBy(t=>t.LRLoginTime):query.OrderByDescending(t=>t.LRLoginTime);
                    isordered = true;
                }
				if(!string.IsNullOrEmpty(search)){
					query = query.Where(t=>t.id!=-1||t.LRLoginName.Contains(search));
				}
                if(sort=="ord")
                {
					query = order=="asc"?query.OrderBy(t=>t.ord):query.OrderByDescending(t=>t.ord);
                    isordered = true;
                }

                if(!isordered) query = query.OrderByDescending(t=>t.UpdateOn);
                var rows = query.Skip((searchModel.PageIndex)*searchModel.PageSize).Take(searchModel.PageSize).ToList();
                var total = query.Count();
                var sql = query.ToString();
                return new CommonOutputList<LogonRecord>
                {
                    success = true, rows = rows, total = total, message="查询成功"
                };
            }
        }
    }


    /// <summary>
    /// 【系统配置】7次查询分别得到七天内记录的条数，形成柱状图，折线图
    /// </summary>
    public partial class SystemConfigurationCountEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
                var now = Parse(request.context.Request.Params["date"] ?? Now.ToString("yyyy-MM-dd"));
                var dates = Range(1, 7).Select(p => now.AddDays(-1 * p)).ToList();
                var list = dates.Select(p => ctx.SystemConfiguration.Count(m => m.CreateOn==p)).ToList();
                return new
                {
                    sum = list.Sum(),
                    xaxis = dates.Select(p => p.ToString("yyyy-MM-dd")).ToList(),
                    series = list
                };
            }
        }
        public override string Comments=> "【系统配置】7次查询分别得到七天内记录的条数，形成柱状图，折线图";
    }
	public partial class TruncateSystemConfigurationEvaluator : Evaluator
	{
        protected override object Evaluate(CommonRequest request)
		{
            using (var ctx = new DefaultContext())
			{
                ctx.SystemConfiguration.RemoveRange(ctx.SystemConfiguration);
				ctx.SaveChanges();
			}
			return new
			{
				success = true,
				message = "操作成功"
			};
		}
	}
    /// <summary>
    /// 删除【系统配置】
    /// </summary>
    public partial class DeleteSystemConfigurationEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            var data = JsonConvert.DeserializeObject<SystemConfiguration>(HttpUtility.UrlDecode(request.data));
            if (data == null)
                return new CommonOutputT<string>
                {
                    success = false,
                    message = "删除成功"
                };
            using (var ctx = new DefaultContext())
            {
                var one = ctx.SystemConfiguration.Find(data.id);
				if(one==null){
					return new CommonOutputT<string>
					{
						success = false,
						message = "未找到需要删除的数据"
					};
				}
                one.IsDeleted = 1;
				ctx.SystemConfiguration.AddOrUpdate(one);
				ctx.SaveChanges();
				return new CommonOutputT<string>
				{
					success = true,
					message = "删除成功"
				};
            }
			
        }
        public override string Comments=> "删除一条系统配置记录";
    }
	
    /// <summary>
    /// 保存【系统配置】
    /// </summary>
    public partial class SaveSystemConfigurationEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            var user = CurrentUserInformation;
            if (user==null)
            {
                return new
                {
                    success = false,
                    message = "请登录"
                };
            }
			var s = request.data;
            if (s==null)
            {
                return new
                {
                    success = false,
                    message = "缺少参数"
                };
            }
			SystemConfiguration entity = null;
			try
			{
				entity = JsonConvert.DeserializeObject<SystemConfiguration>(HttpUtility.UrlDecode(s));
			}
			catch(Exception exception)
			{
				return new
				{
					success = false,
					message = $"填写内容格式错误：{exception.Message}",
					input = s
				};
			}
            if (entity==null)
            {
                return new
                {
                    success = false,
                    message = "参数格式不正确"
                };
            }

			try
			{
				foreach (ValidationResult result in Validation.Validate(entity))
				{
					return new
					{
						success = false,
						message = result.Message
					};
				}
			}
			catch(Exception exception)
			{
				return new
				{
					success = false,
					message = exception.Message
				};
			}
			
            using (var ctx = new DefaultContext())
            {
				// 行级排他锁开始
                var transactionId = Guid.NewGuid().ToString();
                var isnew = entity.TransactionID == null;
                if (!isnew)
                {
                    var one = ctx.SystemConfiguration.FirstOrDefault(p=>p.id==entity.id);
                    if(one==null) return new
                    {
                        success = false,
                        message = "编辑错误，未找到ID"
                    };
                    if (one.VersionNo != entity.VersionNo) return new
                    {
                        success = false,
                        message = "发生数据写冲突"
                    };
                    one.VersionNo++;
                    one.TransactionID = transactionId;
					ctx.SystemConfiguration.AddOrUpdate(one);
					try
					{
						ctx.SaveChanges();
					}
					catch(Exception exception)
					{
						// 遇到数据库中的脏数据，走到这里，前面的Entity数据合法，直接跳过这里的数据校验阶段，使用最先到达的正确数据。
						// return new
						// {
						// 	 success = false,
						// 	 message = exception.Message,
						// 	 exception, one, transactionId, entity
						// };
					}
                    entity.VersionNo = one.VersionNo;
                }
				

								// NVARCHAR(50) 键
				entity.SCKey = HttpUtility.UrlDecode(entity.SCKey);
					// NVARCHAR(50) 值
				entity.SCAccrued = HttpUtility.UrlDecode(entity.SCAccrued);
	
                entity.CreateBy = entity.CreateBy ?? user?.UILoginName ?? "未登录用户";
                entity.UpdateBy = user?.UILoginName ?? "未登录用户";
                entity.CreateOn = entity.CreateOn ?? Now;
                entity.TransactionID = transactionId;
                entity.UpdateOn = Now;
                entity.IsDeleted = 0;
	            entity.VersionNo = entity.VersionNo ?? 0;
                entity.DataLevel = entity.DataLevel ?? user?.DataLevel ?? "019999";
				ctx.SystemConfiguration.AddOrUpdate(entity);
				try
				{
					ctx.SaveChanges();
				}
				catch(Exception exception)
				{
					return new
					{
						success = false,
						message = exception.Message,
						exception, transactionId, entity
					};
				}
				// 行级排他锁结束
                return new
                {
                    success = true,
                    message = "操作成功"
                };
            }
        }
        public override string Comments=> "保存一条SystemConfiguration记录";
    }
	
    /// <summary>
    /// 查询空的【系统配置】
    /// </summary>
    public partial class GetSystemConfigurationEmptyEvaluator:Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            return new SystemConfiguration();
        }
        public override string Comments=> "获取空的系统配置记录";
    }
	
    /// <summary>
    /// 查询【系统配置】列表
    /// </summary>
    public partial class GetSystemConfigurationListEvaluator : Evaluator
    {
        public override string Comments=> "获取SystemConfiguration列表 ";
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
				var datalevel = CurrentUserInformation?.DataLevel;
                var searchModel = HttpUtility.UrlDecode(request.data).Deserialize<SystemConfigurationSearchModel>() ?? new SystemConfigurationSearchModel();
                var query = ctx.SystemConfiguration.Where(t=>t.IsDeleted==0 && t.DataLevel.StartsWith(datalevel));
                var @params = request.context.Request.Params;
                searchModel.PageSize = (@params["limit"] ?? searchModel.PageSize.ToString()).ToInt();
				searchModel.PageSize = searchModel.PageSize==0?4000:searchModel.PageSize;
                searchModel.PageIndex = (@params["offset"]).ToInt()/ searchModel.PageSize;
                var isordered = false;
                var search = searchModel.SearchKey ?? @params["search"];
                var sort = searchModel.Sort ?? @params["sort"];
				var order = @params["order"];
				// SCKey NVARCHAR(50) 键 
                if(!string.IsNullOrEmpty(searchModel.SCKey)) query = query.Where(t=>t.SCKey.Contains(searchModel.SCKey));
                if(sort=="SCKey")
                {
					query = order=="asc"?query.OrderBy(t=>t.SCKey):query.OrderByDescending(t=>t.SCKey);
                    isordered = true;
                }
				// SCAccrued NVARCHAR(50) 值 
                if(!string.IsNullOrEmpty(searchModel.SCAccrued)) query = query.Where(t=>t.SCAccrued.Contains(searchModel.SCAccrued));
                if(sort=="SCAccrued")
                {
					query = order=="asc"?query.OrderBy(t=>t.SCAccrued):query.OrderByDescending(t=>t.SCAccrued);
                    isordered = true;
                }
				if(!string.IsNullOrEmpty(search)){
					query = query.Where(t=>t.id!=-1||t.SCKey.Contains(search)||t.SCAccrued.Contains(search));
				}
                if(sort=="ord")
                {
					query = order=="asc"?query.OrderBy(t=>t.ord):query.OrderByDescending(t=>t.ord);
                    isordered = true;
                }

                if(!isordered) query = query.OrderByDescending(t=>t.UpdateOn);
                var rows = query.Skip((searchModel.PageIndex)*searchModel.PageSize).Take(searchModel.PageSize).ToList();
                var total = query.Count();
                var sql = query.ToString();
                return new CommonOutputList<SystemConfiguration>
                {
                    success = true, rows = rows, total = total, message="查询成功"
                };
            }
        }
    }


    /// <summary>
    /// 【客户】7次查询分别得到七天内记录的条数，形成柱状图，折线图
    /// </summary>
    public partial class CustomertypeCountEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
                var now = Parse(request.context.Request.Params["date"] ?? Now.ToString("yyyy-MM-dd"));
                var dates = Range(1, 7).Select(p => now.AddDays(-1 * p)).ToList();
                var list = dates.Select(p => ctx.Customertype.Count(m => m.CreateOn==p)).ToList();
                return new
                {
                    sum = list.Sum(),
                    xaxis = dates.Select(p => p.ToString("yyyy-MM-dd")).ToList(),
                    series = list
                };
            }
        }
        public override string Comments=> "【客户】7次查询分别得到七天内记录的条数，形成柱状图，折线图";
    }
	public partial class TruncateCustomertypeEvaluator : Evaluator
	{
        protected override object Evaluate(CommonRequest request)
		{
            using (var ctx = new DefaultContext())
			{
                ctx.Customertype.RemoveRange(ctx.Customertype);
				ctx.SaveChanges();
			}
			return new
			{
				success = true,
				message = "操作成功"
			};
		}
	}
    /// <summary>
    /// 删除【客户】
    /// </summary>
    public partial class DeleteCustomertypeEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            var data = JsonConvert.DeserializeObject<Customertype>(HttpUtility.UrlDecode(request.data));
            if (data == null)
                return new CommonOutputT<string>
                {
                    success = false,
                    message = "删除成功"
                };
            using (var ctx = new DefaultContext())
            {
                var one = ctx.Customertype.Find(data.id);
				if(one==null){
					return new CommonOutputT<string>
					{
						success = false,
						message = "未找到需要删除的数据"
					};
				}
                one.IsDeleted = 1;
				ctx.Customertype.AddOrUpdate(one);
				ctx.SaveChanges();
				return new CommonOutputT<string>
				{
					success = true,
					message = "删除成功"
				};
            }
			
        }
        public override string Comments=> "删除一条客户记录";
    }
	
    /// <summary>
    /// 保存【客户】
    /// </summary>
    public partial class SaveCustomertypeEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            var user = CurrentUserInformation;
            if (user==null)
            {
                return new
                {
                    success = false,
                    message = "请登录"
                };
            }
			var s = request.data;
            if (s==null)
            {
                return new
                {
                    success = false,
                    message = "缺少参数"
                };
            }
			Customertype entity = null;
			try
			{
				entity = JsonConvert.DeserializeObject<Customertype>(HttpUtility.UrlDecode(s));
			}
			catch(Exception exception)
			{
				return new
				{
					success = false,
					message = $"填写内容格式错误：{exception.Message}",
					input = s
				};
			}
            if (entity==null)
            {
                return new
                {
                    success = false,
                    message = "参数格式不正确"
                };
            }

			try
			{
				foreach (ValidationResult result in Validation.Validate(entity))
				{
					return new
					{
						success = false,
						message = result.Message
					};
				}
			}
			catch(Exception exception)
			{
				return new
				{
					success = false,
					message = exception.Message
				};
			}
			
            using (var ctx = new DefaultContext())
            {
				// 行级排他锁开始
                var transactionId = Guid.NewGuid().ToString();
                var isnew = entity.TransactionID == null;
                if (!isnew)
                {
                    var one = ctx.Customertype.FirstOrDefault(p=>p.id==entity.id);
                    if(one==null) return new
                    {
                        success = false,
                        message = "编辑错误，未找到ID"
                    };
                    if (one.VersionNo != entity.VersionNo) return new
                    {
                        success = false,
                        message = "发生数据写冲突"
                    };
                    one.VersionNo++;
                    one.TransactionID = transactionId;
					ctx.Customertype.AddOrUpdate(one);
					try
					{
						ctx.SaveChanges();
					}
					catch(Exception exception)
					{
						// 遇到数据库中的脏数据，走到这里，前面的Entity数据合法，直接跳过这里的数据校验阶段，使用最先到达的正确数据。
						// return new
						// {
						// 	 success = false,
						// 	 message = exception.Message,
						// 	 exception, one, transactionId, entity
						// };
					}
                    entity.VersionNo = one.VersionNo;
                }
				

								// NVARCHAR(50) 客户编号
				entity.CCustomerNumber = HttpUtility.UrlDecode(entity.CCustomerNumber);
					// NVARCHAR(50) 姓名
				entity.CName = HttpUtility.UrlDecode(entity.CName);
					// NVARCHAR(50) 性别
				entity.CChairperson = HttpUtility.UrlDecode(entity.CChairperson);
					// NVARCHAR(50) 称呼
				entity.CCall = HttpUtility.UrlDecode(entity.CCall);
					// NVARCHAR(50) 联系方式
				entity.CCommonModeOfContact = HttpUtility.UrlDecode(entity.CCommonModeOfContact);
					// NVARCHAR(50) 地址
				entity.CAddress = HttpUtility.UrlDecode(entity.CAddress);
	
                entity.CreateBy = entity.CreateBy ?? user?.UILoginName ?? "未登录用户";
                entity.UpdateBy = user?.UILoginName ?? "未登录用户";
                entity.CreateOn = entity.CreateOn ?? Now;
                entity.TransactionID = transactionId;
                entity.UpdateOn = Now;
                entity.IsDeleted = 0;
	            entity.VersionNo = entity.VersionNo ?? 0;
                entity.DataLevel = entity.DataLevel ?? user?.DataLevel ?? "019999";
				ctx.Customertype.AddOrUpdate(entity);
				try
				{
					ctx.SaveChanges();
				}
				catch(Exception exception)
				{
					return new
					{
						success = false,
						message = exception.Message,
						exception, transactionId, entity
					};
				}
				// 行级排他锁结束
                return new
                {
                    success = true,
                    message = "操作成功"
                };
            }
        }
        public override string Comments=> "保存一条Customertype记录";
    }
	
    /// <summary>
    /// 查询空的【客户】
    /// </summary>
    public partial class GetCustomertypeEmptyEvaluator:Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            return new Customertype();
        }
        public override string Comments=> "获取空的客户记录";
    }
	
    /// <summary>
    /// 查询【客户】列表
    /// </summary>
    public partial class GetCustomertypeListEvaluator : Evaluator
    {
        public override string Comments=> "获取Customertype列表 ";
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
				var datalevel = CurrentUserInformation?.DataLevel;
                var searchModel = HttpUtility.UrlDecode(request.data).Deserialize<CustomertypeSearchModel>() ?? new CustomertypeSearchModel();
                var query = ctx.Customertype.Where(t=>t.IsDeleted==0 && t.DataLevel.StartsWith(datalevel));
                var @params = request.context.Request.Params;
                searchModel.PageSize = (@params["limit"] ?? searchModel.PageSize.ToString()).ToInt();
				searchModel.PageSize = searchModel.PageSize==0?4000:searchModel.PageSize;
                searchModel.PageIndex = (@params["offset"]).ToInt()/ searchModel.PageSize;
                var isordered = false;
                var search = searchModel.SearchKey ?? @params["search"];
                var sort = searchModel.Sort ?? @params["sort"];
				var order = @params["order"];
				// CCustomerNumber NVARCHAR(50) 客户编号 
                if(!string.IsNullOrEmpty(searchModel.CCustomerNumber)) query = query.Where(t=>t.CCustomerNumber.Contains(searchModel.CCustomerNumber));
                if(sort=="CCustomerNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.CCustomerNumber):query.OrderByDescending(t=>t.CCustomerNumber);
                    isordered = true;
                }
				// CName NVARCHAR(50) 姓名 
                if(!string.IsNullOrEmpty(searchModel.CName)) query = query.Where(t=>t.CName.Contains(searchModel.CName));
                if(sort=="CName")
                {
					query = order=="asc"?query.OrderBy(t=>t.CName):query.OrderByDescending(t=>t.CName);
                    isordered = true;
                }
				// CChairperson NVARCHAR(50) 性别 
                if(!string.IsNullOrEmpty(searchModel.CChairperson)) query = query.Where(t=>t.CChairperson.Contains(searchModel.CChairperson));
                if(sort=="CChairperson")
                {
					query = order=="asc"?query.OrderBy(t=>t.CChairperson):query.OrderByDescending(t=>t.CChairperson);
                    isordered = true;
                }
				// CCall NVARCHAR(50) 称呼 
                if(!string.IsNullOrEmpty(searchModel.CCall)) query = query.Where(t=>t.CCall.Contains(searchModel.CCall));
                if(sort=="CCall")
                {
					query = order=="asc"?query.OrderBy(t=>t.CCall):query.OrderByDescending(t=>t.CCall);
                    isordered = true;
                }
				// CCommonModeOfContact NVARCHAR(50) 联系方式 
                if(!string.IsNullOrEmpty(searchModel.CCommonModeOfContact)) query = query.Where(t=>t.CCommonModeOfContact.Contains(searchModel.CCommonModeOfContact));
                if(sort=="CCommonModeOfContact")
                {
					query = order=="asc"?query.OrderBy(t=>t.CCommonModeOfContact):query.OrderByDescending(t=>t.CCommonModeOfContact);
                    isordered = true;
                }
				// CAddress NVARCHAR(50) 地址 
                if(!string.IsNullOrEmpty(searchModel.CAddress)) query = query.Where(t=>t.CAddress.Contains(searchModel.CAddress));
                if(sort=="CAddress")
                {
					query = order=="asc"?query.OrderBy(t=>t.CAddress):query.OrderByDescending(t=>t.CAddress);
                    isordered = true;
                }
				if(!string.IsNullOrEmpty(search)){
					query = query.Where(t=>t.id!=-1||t.CCustomerNumber.Contains(search)||t.CName.Contains(search)||t.CChairperson.Contains(search)||t.CCall.Contains(search)||t.CCommonModeOfContact.Contains(search)||t.CAddress.Contains(search));
				}
                if(sort=="ord")
                {
					query = order=="asc"?query.OrderBy(t=>t.ord):query.OrderByDescending(t=>t.ord);
                    isordered = true;
                }

                if(!isordered) query = query.OrderByDescending(t=>t.UpdateOn);
                var rows = query.Skip((searchModel.PageIndex)*searchModel.PageSize).Take(searchModel.PageSize).ToList();
                var total = query.Count();
                var sql = query.ToString();
                return new CommonOutputList<Customertype>
                {
                    success = true, rows = rows, total = total, message="查询成功"
                };
            }
        }
    }


    /// <summary>
    /// 【供应商】7次查询分别得到七天内记录的条数，形成柱状图，折线图
    /// </summary>
    public partial class SupplierCountEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
                var now = Parse(request.context.Request.Params["date"] ?? Now.ToString("yyyy-MM-dd"));
                var dates = Range(1, 7).Select(p => now.AddDays(-1 * p)).ToList();
                var list = dates.Select(p => ctx.Supplier.Count(m => m.CreateOn==p)).ToList();
                return new
                {
                    sum = list.Sum(),
                    xaxis = dates.Select(p => p.ToString("yyyy-MM-dd")).ToList(),
                    series = list
                };
            }
        }
        public override string Comments=> "【供应商】7次查询分别得到七天内记录的条数，形成柱状图，折线图";
    }
	public partial class TruncateSupplierEvaluator : Evaluator
	{
        protected override object Evaluate(CommonRequest request)
		{
            using (var ctx = new DefaultContext())
			{
                ctx.Supplier.RemoveRange(ctx.Supplier);
				ctx.SaveChanges();
			}
			return new
			{
				success = true,
				message = "操作成功"
			};
		}
	}
    /// <summary>
    /// 删除【供应商】
    /// </summary>
    public partial class DeleteSupplierEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            var data = JsonConvert.DeserializeObject<Supplier>(HttpUtility.UrlDecode(request.data));
            if (data == null)
                return new CommonOutputT<string>
                {
                    success = false,
                    message = "删除成功"
                };
            using (var ctx = new DefaultContext())
            {
                var one = ctx.Supplier.Find(data.id);
				if(one==null){
					return new CommonOutputT<string>
					{
						success = false,
						message = "未找到需要删除的数据"
					};
				}
                one.IsDeleted = 1;
				ctx.Supplier.AddOrUpdate(one);
				ctx.SaveChanges();
				return new CommonOutputT<string>
				{
					success = true,
					message = "删除成功"
				};
            }
			
        }
        public override string Comments=> "删除一条供应商记录";
    }
	
    /// <summary>
    /// 保存【供应商】
    /// </summary>
    public partial class SaveSupplierEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            var user = CurrentUserInformation;
            if (user==null)
            {
                return new
                {
                    success = false,
                    message = "请登录"
                };
            }
			var s = request.data;
            if (s==null)
            {
                return new
                {
                    success = false,
                    message = "缺少参数"
                };
            }
			Supplier entity = null;
			try
			{
				entity = JsonConvert.DeserializeObject<Supplier>(HttpUtility.UrlDecode(s));
			}
			catch(Exception exception)
			{
				return new
				{
					success = false,
					message = $"填写内容格式错误：{exception.Message}",
					input = s
				};
			}
            if (entity==null)
            {
                return new
                {
                    success = false,
                    message = "参数格式不正确"
                };
            }

			try
			{
				foreach (ValidationResult result in Validation.Validate(entity))
				{
					return new
					{
						success = false,
						message = result.Message
					};
				}
			}
			catch(Exception exception)
			{
				return new
				{
					success = false,
					message = exception.Message
				};
			}
			
            using (var ctx = new DefaultContext())
            {
				// 行级排他锁开始
                var transactionId = Guid.NewGuid().ToString();
                var isnew = entity.TransactionID == null;
                if (!isnew)
                {
                    var one = ctx.Supplier.FirstOrDefault(p=>p.id==entity.id);
                    if(one==null) return new
                    {
                        success = false,
                        message = "编辑错误，未找到ID"
                    };
                    if (one.VersionNo != entity.VersionNo) return new
                    {
                        success = false,
                        message = "发生数据写冲突"
                    };
                    one.VersionNo++;
                    one.TransactionID = transactionId;
					ctx.Supplier.AddOrUpdate(one);
					try
					{
						ctx.SaveChanges();
					}
					catch(Exception exception)
					{
						// 遇到数据库中的脏数据，走到这里，前面的Entity数据合法，直接跳过这里的数据校验阶段，使用最先到达的正确数据。
						// return new
						// {
						// 	 success = false,
						// 	 message = exception.Message,
						// 	 exception, one, transactionId, entity
						// };
					}
                    entity.VersionNo = one.VersionNo;
                }
				

								// NVARCHAR(50) 供应商编号
				entity.SSupplierNumber = HttpUtility.UrlDecode(entity.SSupplierNumber);
					// NVARCHAR(50) 供应商名称
				entity.SSupplierName = HttpUtility.UrlDecode(entity.SSupplierName);
					// NVARCHAR(50) 联系方式
				entity.SCommonModeOfContact = HttpUtility.UrlDecode(entity.SCommonModeOfContact);
					// NVARCHAR(50) 办公地点
				entity.SOfficeLocation = HttpUtility.UrlDecode(entity.SOfficeLocation);
					// NVARCHAR(50) 经营范围
				entity.SScopeOfOperation = HttpUtility.UrlDecode(entity.SScopeOfOperation);
	
                entity.CreateBy = entity.CreateBy ?? user?.UILoginName ?? "未登录用户";
                entity.UpdateBy = user?.UILoginName ?? "未登录用户";
                entity.CreateOn = entity.CreateOn ?? Now;
                entity.TransactionID = transactionId;
                entity.UpdateOn = Now;
                entity.IsDeleted = 0;
	            entity.VersionNo = entity.VersionNo ?? 0;
                entity.DataLevel = entity.DataLevel ?? user?.DataLevel ?? "019999";
				ctx.Supplier.AddOrUpdate(entity);
				try
				{
					ctx.SaveChanges();
				}
				catch(Exception exception)
				{
					return new
					{
						success = false,
						message = exception.Message,
						exception, transactionId, entity
					};
				}
				// 行级排他锁结束
                return new
                {
                    success = true,
                    message = "操作成功"
                };
            }
        }
        public override string Comments=> "保存一条Supplier记录";
    }
	
    /// <summary>
    /// 查询空的【供应商】
    /// </summary>
    public partial class GetSupplierEmptyEvaluator:Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            return new Supplier();
        }
        public override string Comments=> "获取空的供应商记录";
    }
	
    /// <summary>
    /// 查询【供应商】列表
    /// </summary>
    public partial class GetSupplierListEvaluator : Evaluator
    {
        public override string Comments=> "获取Supplier列表 ";
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
				var datalevel = CurrentUserInformation?.DataLevel;
                var searchModel = HttpUtility.UrlDecode(request.data).Deserialize<SupplierSearchModel>() ?? new SupplierSearchModel();
                var query = ctx.Supplier.Where(t=>t.IsDeleted==0 && t.DataLevel.StartsWith(datalevel));
                var @params = request.context.Request.Params;
                searchModel.PageSize = (@params["limit"] ?? searchModel.PageSize.ToString()).ToInt();
				searchModel.PageSize = searchModel.PageSize==0?4000:searchModel.PageSize;
                searchModel.PageIndex = (@params["offset"]).ToInt()/ searchModel.PageSize;
                var isordered = false;
                var search = searchModel.SearchKey ?? @params["search"];
                var sort = searchModel.Sort ?? @params["sort"];
				var order = @params["order"];
				// SSupplierNumber NVARCHAR(50) 供应商编号 
                if(!string.IsNullOrEmpty(searchModel.SSupplierNumber)) query = query.Where(t=>t.SSupplierNumber.Contains(searchModel.SSupplierNumber));
                if(sort=="SSupplierNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.SSupplierNumber):query.OrderByDescending(t=>t.SSupplierNumber);
                    isordered = true;
                }
				// SSupplierName NVARCHAR(50) 供应商名称 
                if(!string.IsNullOrEmpty(searchModel.SSupplierName)) query = query.Where(t=>t.SSupplierName.Contains(searchModel.SSupplierName));
                if(sort=="SSupplierName")
                {
					query = order=="asc"?query.OrderBy(t=>t.SSupplierName):query.OrderByDescending(t=>t.SSupplierName);
                    isordered = true;
                }
				// SCommonModeOfContact NVARCHAR(50) 联系方式 
                if(!string.IsNullOrEmpty(searchModel.SCommonModeOfContact)) query = query.Where(t=>t.SCommonModeOfContact.Contains(searchModel.SCommonModeOfContact));
                if(sort=="SCommonModeOfContact")
                {
					query = order=="asc"?query.OrderBy(t=>t.SCommonModeOfContact):query.OrderByDescending(t=>t.SCommonModeOfContact);
                    isordered = true;
                }
				// SOfficeLocation NVARCHAR(50) 办公地点 
                if(!string.IsNullOrEmpty(searchModel.SOfficeLocation)) query = query.Where(t=>t.SOfficeLocation.Contains(searchModel.SOfficeLocation));
                if(sort=="SOfficeLocation")
                {
					query = order=="asc"?query.OrderBy(t=>t.SOfficeLocation):query.OrderByDescending(t=>t.SOfficeLocation);
                    isordered = true;
                }
				// SScopeOfOperation NVARCHAR(50) 经营范围 
                if(!string.IsNullOrEmpty(searchModel.SScopeOfOperation)) query = query.Where(t=>t.SScopeOfOperation.Contains(searchModel.SScopeOfOperation));
                if(sort=="SScopeOfOperation")
                {
					query = order=="asc"?query.OrderBy(t=>t.SScopeOfOperation):query.OrderByDescending(t=>t.SScopeOfOperation);
                    isordered = true;
                }
				if(!string.IsNullOrEmpty(search)){
					query = query.Where(t=>t.id!=-1||t.SSupplierNumber.Contains(search)||t.SSupplierName.Contains(search)||t.SCommonModeOfContact.Contains(search)||t.SOfficeLocation.Contains(search)||t.SScopeOfOperation.Contains(search));
				}
                if(sort=="ord")
                {
					query = order=="asc"?query.OrderBy(t=>t.ord):query.OrderByDescending(t=>t.ord);
                    isordered = true;
                }

                if(!isordered) query = query.OrderByDescending(t=>t.UpdateOn);
                var rows = query.Skip((searchModel.PageIndex)*searchModel.PageSize).Take(searchModel.PageSize).ToList();
                var total = query.Count();
                var sql = query.ToString();
                return new CommonOutputList<Supplier>
                {
                    success = true, rows = rows, total = total, message="查询成功"
                };
            }
        }
    }


    /// <summary>
    /// 【货物种类】7次查询分别得到七天内记录的条数，形成柱状图，折线图
    /// </summary>
    public partial class TypeOfGoodsCountEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
                var now = Parse(request.context.Request.Params["date"] ?? Now.ToString("yyyy-MM-dd"));
                var dates = Range(1, 7).Select(p => now.AddDays(-1 * p)).ToList();
                var list = dates.Select(p => ctx.TypeOfGoods.Count(m => m.CreateOn==p)).ToList();
                return new
                {
                    sum = list.Sum(),
                    xaxis = dates.Select(p => p.ToString("yyyy-MM-dd")).ToList(),
                    series = list
                };
            }
        }
        public override string Comments=> "【货物种类】7次查询分别得到七天内记录的条数，形成柱状图，折线图";
    }
	public partial class TruncateTypeOfGoodsEvaluator : Evaluator
	{
        protected override object Evaluate(CommonRequest request)
		{
            using (var ctx = new DefaultContext())
			{
                ctx.TypeOfGoods.RemoveRange(ctx.TypeOfGoods);
				ctx.SaveChanges();
			}
			return new
			{
				success = true,
				message = "操作成功"
			};
		}
	}
    /// <summary>
    /// 删除【货物种类】
    /// </summary>
    public partial class DeleteTypeOfGoodsEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            var data = JsonConvert.DeserializeObject<TypeOfGoods>(HttpUtility.UrlDecode(request.data));
            if (data == null)
                return new CommonOutputT<string>
                {
                    success = false,
                    message = "删除成功"
                };
            using (var ctx = new DefaultContext())
            {
                var one = ctx.TypeOfGoods.Find(data.id);
				if(one==null){
					return new CommonOutputT<string>
					{
						success = false,
						message = "未找到需要删除的数据"
					};
				}
                one.IsDeleted = 1;
				ctx.TypeOfGoods.AddOrUpdate(one);
				ctx.SaveChanges();
				return new CommonOutputT<string>
				{
					success = true,
					message = "删除成功"
				};
            }
			
        }
        public override string Comments=> "删除一条货物种类记录";
    }
	
    /// <summary>
    /// 保存【货物种类】
    /// </summary>
    public partial class SaveTypeOfGoodsEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            var user = CurrentUserInformation;
            if (user==null)
            {
                return new
                {
                    success = false,
                    message = "请登录"
                };
            }
			var s = request.data;
            if (s==null)
            {
                return new
                {
                    success = false,
                    message = "缺少参数"
                };
            }
			TypeOfGoods entity = null;
			try
			{
				entity = JsonConvert.DeserializeObject<TypeOfGoods>(HttpUtility.UrlDecode(s));
			}
			catch(Exception exception)
			{
				return new
				{
					success = false,
					message = $"填写内容格式错误：{exception.Message}",
					input = s
				};
			}
            if (entity==null)
            {
                return new
                {
                    success = false,
                    message = "参数格式不正确"
                };
            }

			try
			{
				foreach (ValidationResult result in Validation.Validate(entity))
				{
					return new
					{
						success = false,
						message = result.Message
					};
				}
			}
			catch(Exception exception)
			{
				return new
				{
					success = false,
					message = exception.Message
				};
			}
			
            using (var ctx = new DefaultContext())
            {
				// 行级排他锁开始
                var transactionId = Guid.NewGuid().ToString();
                var isnew = entity.TransactionID == null;
                if (!isnew)
                {
                    var one = ctx.TypeOfGoods.FirstOrDefault(p=>p.id==entity.id);
                    if(one==null) return new
                    {
                        success = false,
                        message = "编辑错误，未找到ID"
                    };
                    if (one.VersionNo != entity.VersionNo) return new
                    {
                        success = false,
                        message = "发生数据写冲突"
                    };
                    one.VersionNo++;
                    one.TransactionID = transactionId;
					ctx.TypeOfGoods.AddOrUpdate(one);
					try
					{
						ctx.SaveChanges();
					}
					catch(Exception exception)
					{
						// 遇到数据库中的脏数据，走到这里，前面的Entity数据合法，直接跳过这里的数据校验阶段，使用最先到达的正确数据。
						// return new
						// {
						// 	 success = false,
						// 	 message = exception.Message,
						// 	 exception, one, transactionId, entity
						// };
					}
                    entity.VersionNo = one.VersionNo;
                }
				

								// NVARCHAR(50) 货品种类编号
				entity.TOGCategoryNumberOfGoods = HttpUtility.UrlDecode(entity.TOGCategoryNumberOfGoods);
					// NVARCHAR(50) 货物名称
				entity.TOGNameOfGoods = HttpUtility.UrlDecode(entity.TOGNameOfGoods);
					// NVARCHAR(50) 货物类别
				entity.TOGCategoryOfGoods = HttpUtility.UrlDecode(entity.TOGCategoryOfGoods);
					// NVARCHAR(50) 货物子类别
				entity.TOGCargoSubcategory = HttpUtility.UrlDecode(entity.TOGCargoSubcategory);
					// NVARCHAR(50) 体积
				entity.TOGBulk = HttpUtility.UrlDecode(entity.TOGBulk);
					// NVARCHAR(50) 颜色
				entity.TOGColor = HttpUtility.UrlDecode(entity.TOGColor);
					// NVARCHAR(50) 型号
				entity.TOGModel = HttpUtility.UrlDecode(entity.TOGModel);
					// NVARCHAR(50) 别称
				entity.TOGAlias = HttpUtility.UrlDecode(entity.TOGAlias);
					// NVARCHAR(50) 采购单价
				entity.TOGPurchaseUnitPrice = HttpUtility.UrlDecode(entity.TOGPurchaseUnitPrice);
					// NVARCHAR(50) 销售单价
				entity.TOGSalesUnitPrice = HttpUtility.UrlDecode(entity.TOGSalesUnitPrice);
	
                entity.CreateBy = entity.CreateBy ?? user?.UILoginName ?? "未登录用户";
                entity.UpdateBy = user?.UILoginName ?? "未登录用户";
                entity.CreateOn = entity.CreateOn ?? Now;
                entity.TransactionID = transactionId;
                entity.UpdateOn = Now;
                entity.IsDeleted = 0;
	            entity.VersionNo = entity.VersionNo ?? 0;
                entity.DataLevel = entity.DataLevel ?? user?.DataLevel ?? "019999";
				ctx.TypeOfGoods.AddOrUpdate(entity);
				try
				{
					ctx.SaveChanges();
				}
				catch(Exception exception)
				{
					return new
					{
						success = false,
						message = exception.Message,
						exception, transactionId, entity
					};
				}
				// 行级排他锁结束
                return new
                {
                    success = true,
                    message = "操作成功"
                };
            }
        }
        public override string Comments=> "保存一条TypeOfGoods记录";
    }
	
    /// <summary>
    /// 查询空的【货物种类】
    /// </summary>
    public partial class GetTypeOfGoodsEmptyEvaluator:Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            return new TypeOfGoods();
        }
        public override string Comments=> "获取空的货物种类记录";
    }
	
    /// <summary>
    /// 查询【货物种类】列表
    /// </summary>
    public partial class GetTypeOfGoodsListEvaluator : Evaluator
    {
        public override string Comments=> "获取TypeOfGoods列表 ";
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
				var datalevel = CurrentUserInformation?.DataLevel;
                var searchModel = HttpUtility.UrlDecode(request.data).Deserialize<TypeOfGoodsSearchModel>() ?? new TypeOfGoodsSearchModel();
                var query = ctx.TypeOfGoods.Where(t=>t.IsDeleted==0 && t.DataLevel.StartsWith(datalevel));
                var @params = request.context.Request.Params;
                searchModel.PageSize = (@params["limit"] ?? searchModel.PageSize.ToString()).ToInt();
				searchModel.PageSize = searchModel.PageSize==0?4000:searchModel.PageSize;
                searchModel.PageIndex = (@params["offset"]).ToInt()/ searchModel.PageSize;
                var isordered = false;
                var search = searchModel.SearchKey ?? @params["search"];
                var sort = searchModel.Sort ?? @params["sort"];
				var order = @params["order"];
				// TOGCategoryNumberOfGoods NVARCHAR(50) 货品种类编号 
                if(!string.IsNullOrEmpty(searchModel.TOGCategoryNumberOfGoods)) query = query.Where(t=>t.TOGCategoryNumberOfGoods.Contains(searchModel.TOGCategoryNumberOfGoods));
                if(sort=="TOGCategoryNumberOfGoods")
                {
					query = order=="asc"?query.OrderBy(t=>t.TOGCategoryNumberOfGoods):query.OrderByDescending(t=>t.TOGCategoryNumberOfGoods);
                    isordered = true;
                }
				// TOGNameOfGoods NVARCHAR(50) 货物名称 
                if(!string.IsNullOrEmpty(searchModel.TOGNameOfGoods)) query = query.Where(t=>t.TOGNameOfGoods.Contains(searchModel.TOGNameOfGoods));
                if(sort=="TOGNameOfGoods")
                {
					query = order=="asc"?query.OrderBy(t=>t.TOGNameOfGoods):query.OrderByDescending(t=>t.TOGNameOfGoods);
                    isordered = true;
                }
				// TOGCategoryOfGoods NVARCHAR(50) 货物类别 
                if(!string.IsNullOrEmpty(searchModel.TOGCategoryOfGoods)) query = query.Where(t=>t.TOGCategoryOfGoods.Contains(searchModel.TOGCategoryOfGoods));
                if(sort=="TOGCategoryOfGoods")
                {
					query = order=="asc"?query.OrderBy(t=>t.TOGCategoryOfGoods):query.OrderByDescending(t=>t.TOGCategoryOfGoods);
                    isordered = true;
                }
				// TOGCargoSubcategory NVARCHAR(50) 货物子类别 
                if(!string.IsNullOrEmpty(searchModel.TOGCargoSubcategory)) query = query.Where(t=>t.TOGCargoSubcategory.Contains(searchModel.TOGCargoSubcategory));
                if(sort=="TOGCargoSubcategory")
                {
					query = order=="asc"?query.OrderBy(t=>t.TOGCargoSubcategory):query.OrderByDescending(t=>t.TOGCargoSubcategory);
                    isordered = true;
                }
				// TOGBulk NVARCHAR(50) 体积 
                if(!string.IsNullOrEmpty(searchModel.TOGBulk)) query = query.Where(t=>t.TOGBulk.Contains(searchModel.TOGBulk));
                if(sort=="TOGBulk")
                {
					query = order=="asc"?query.OrderBy(t=>t.TOGBulk):query.OrderByDescending(t=>t.TOGBulk);
                    isordered = true;
                }
				// TOGColor NVARCHAR(50) 颜色 
                if(!string.IsNullOrEmpty(searchModel.TOGColor)) query = query.Where(t=>t.TOGColor.Contains(searchModel.TOGColor));
                if(sort=="TOGColor")
                {
					query = order=="asc"?query.OrderBy(t=>t.TOGColor):query.OrderByDescending(t=>t.TOGColor);
                    isordered = true;
                }
				// TOGModel NVARCHAR(50) 型号 
                if(!string.IsNullOrEmpty(searchModel.TOGModel)) query = query.Where(t=>t.TOGModel.Contains(searchModel.TOGModel));
                if(sort=="TOGModel")
                {
					query = order=="asc"?query.OrderBy(t=>t.TOGModel):query.OrderByDescending(t=>t.TOGModel);
                    isordered = true;
                }
				// TOGAlias NVARCHAR(50) 别称 
                if(!string.IsNullOrEmpty(searchModel.TOGAlias)) query = query.Where(t=>t.TOGAlias.Contains(searchModel.TOGAlias));
                if(sort=="TOGAlias")
                {
					query = order=="asc"?query.OrderBy(t=>t.TOGAlias):query.OrderByDescending(t=>t.TOGAlias);
                    isordered = true;
                }
				// TOGPurchaseUnitPrice NVARCHAR(50) 采购单价 
                if(!string.IsNullOrEmpty(searchModel.TOGPurchaseUnitPrice)) query = query.Where(t=>t.TOGPurchaseUnitPrice.Contains(searchModel.TOGPurchaseUnitPrice));
                if(sort=="TOGPurchaseUnitPrice")
                {
					query = order=="asc"?query.OrderBy(t=>t.TOGPurchaseUnitPrice):query.OrderByDescending(t=>t.TOGPurchaseUnitPrice);
                    isordered = true;
                }
				// TOGSalesUnitPrice NVARCHAR(50) 销售单价 
                if(!string.IsNullOrEmpty(searchModel.TOGSalesUnitPrice)) query = query.Where(t=>t.TOGSalesUnitPrice.Contains(searchModel.TOGSalesUnitPrice));
                if(sort=="TOGSalesUnitPrice")
                {
					query = order=="asc"?query.OrderBy(t=>t.TOGSalesUnitPrice):query.OrderByDescending(t=>t.TOGSalesUnitPrice);
                    isordered = true;
                }
				if(!string.IsNullOrEmpty(search)){
					query = query.Where(t=>t.id!=-1||t.TOGCategoryNumberOfGoods.Contains(search)||t.TOGNameOfGoods.Contains(search)||t.TOGCategoryOfGoods.Contains(search)||t.TOGCargoSubcategory.Contains(search)||t.TOGBulk.Contains(search)||t.TOGColor.Contains(search)||t.TOGModel.Contains(search)||t.TOGAlias.Contains(search)||t.TOGPurchaseUnitPrice.Contains(search)||t.TOGSalesUnitPrice.Contains(search));
				}
                if(sort=="ord")
                {
					query = order=="asc"?query.OrderBy(t=>t.ord):query.OrderByDescending(t=>t.ord);
                    isordered = true;
                }

                if(!isordered) query = query.OrderByDescending(t=>t.UpdateOn);
                var rows = query.Skip((searchModel.PageIndex)*searchModel.PageSize).Take(searchModel.PageSize).ToList();
                var total = query.Count();
                var sql = query.ToString();
                return new CommonOutputList<TypeOfGoods>
                {
                    success = true, rows = rows, total = total, message="查询成功"
                };
            }
        }
    }


    /// <summary>
    /// 【供货渠道】7次查询分别得到七天内记录的条数，形成柱状图，折线图
    /// </summary>
    public partial class SupplyChannelCountEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
                var now = Parse(request.context.Request.Params["date"] ?? Now.ToString("yyyy-MM-dd"));
                var dates = Range(1, 7).Select(p => now.AddDays(-1 * p)).ToList();
                var list = dates.Select(p => ctx.SupplyChannel.Count(m => m.CreateOn==p)).ToList();
                return new
                {
                    sum = list.Sum(),
                    xaxis = dates.Select(p => p.ToString("yyyy-MM-dd")).ToList(),
                    series = list
                };
            }
        }
        public override string Comments=> "【供货渠道】7次查询分别得到七天内记录的条数，形成柱状图，折线图";
    }
	public partial class TruncateSupplyChannelEvaluator : Evaluator
	{
        protected override object Evaluate(CommonRequest request)
		{
            using (var ctx = new DefaultContext())
			{
                ctx.SupplyChannel.RemoveRange(ctx.SupplyChannel);
				ctx.SaveChanges();
			}
			return new
			{
				success = true,
				message = "操作成功"
			};
		}
	}
    /// <summary>
    /// 删除【供货渠道】
    /// </summary>
    public partial class DeleteSupplyChannelEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            var data = JsonConvert.DeserializeObject<SupplyChannel>(HttpUtility.UrlDecode(request.data));
            if (data == null)
                return new CommonOutputT<string>
                {
                    success = false,
                    message = "删除成功"
                };
            using (var ctx = new DefaultContext())
            {
                var one = ctx.SupplyChannel.Find(data.id);
				if(one==null){
					return new CommonOutputT<string>
					{
						success = false,
						message = "未找到需要删除的数据"
					};
				}
                one.IsDeleted = 1;
				ctx.SupplyChannel.AddOrUpdate(one);
				ctx.SaveChanges();
				return new CommonOutputT<string>
				{
					success = true,
					message = "删除成功"
				};
            }
			
        }
        public override string Comments=> "删除一条供货渠道记录";
    }
	
    /// <summary>
    /// 保存【供货渠道】
    /// </summary>
    public partial class SaveSupplyChannelEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            var user = CurrentUserInformation;
            if (user==null)
            {
                return new
                {
                    success = false,
                    message = "请登录"
                };
            }
			var s = request.data;
            if (s==null)
            {
                return new
                {
                    success = false,
                    message = "缺少参数"
                };
            }
			SupplyChannel entity = null;
			try
			{
				entity = JsonConvert.DeserializeObject<SupplyChannel>(HttpUtility.UrlDecode(s));
			}
			catch(Exception exception)
			{
				return new
				{
					success = false,
					message = $"填写内容格式错误：{exception.Message}",
					input = s
				};
			}
            if (entity==null)
            {
                return new
                {
                    success = false,
                    message = "参数格式不正确"
                };
            }

			try
			{
				foreach (ValidationResult result in Validation.Validate(entity))
				{
					return new
					{
						success = false,
						message = result.Message
					};
				}
			}
			catch(Exception exception)
			{
				return new
				{
					success = false,
					message = exception.Message
				};
			}
			
            using (var ctx = new DefaultContext())
            {
				// 行级排他锁开始
                var transactionId = Guid.NewGuid().ToString();
                var isnew = entity.TransactionID == null;
                if (!isnew)
                {
                    var one = ctx.SupplyChannel.FirstOrDefault(p=>p.id==entity.id);
                    if(one==null) return new
                    {
                        success = false,
                        message = "编辑错误，未找到ID"
                    };
                    if (one.VersionNo != entity.VersionNo) return new
                    {
                        success = false,
                        message = "发生数据写冲突"
                    };
                    one.VersionNo++;
                    one.TransactionID = transactionId;
					ctx.SupplyChannel.AddOrUpdate(one);
					try
					{
						ctx.SaveChanges();
					}
					catch(Exception exception)
					{
						// 遇到数据库中的脏数据，走到这里，前面的Entity数据合法，直接跳过这里的数据校验阶段，使用最先到达的正确数据。
						// return new
						// {
						// 	 success = false,
						// 	 message = exception.Message,
						// 	 exception, one, transactionId, entity
						// };
					}
                    entity.VersionNo = one.VersionNo;
                }
				

								// NVARCHAR(50) 供应商
				entity.SCSupplier = HttpUtility.UrlDecode(entity.SCSupplier);
					// NVARCHAR(50) 货物种类
				entity.SCTypeOfGoods = HttpUtility.UrlDecode(entity.SCTypeOfGoods);
	
                entity.CreateBy = entity.CreateBy ?? user?.UILoginName ?? "未登录用户";
                entity.UpdateBy = user?.UILoginName ?? "未登录用户";
                entity.CreateOn = entity.CreateOn ?? Now;
                entity.TransactionID = transactionId;
                entity.UpdateOn = Now;
                entity.IsDeleted = 0;
	            entity.VersionNo = entity.VersionNo ?? 0;
                entity.DataLevel = entity.DataLevel ?? user?.DataLevel ?? "019999";
				ctx.SupplyChannel.AddOrUpdate(entity);
				try
				{
					ctx.SaveChanges();
				}
				catch(Exception exception)
				{
					return new
					{
						success = false,
						message = exception.Message,
						exception, transactionId, entity
					};
				}
				// 行级排他锁结束
                return new
                {
                    success = true,
                    message = "操作成功"
                };
            }
        }
        public override string Comments=> "保存一条SupplyChannel记录";
    }
	
    /// <summary>
    /// 查询空的【供货渠道】
    /// </summary>
    public partial class GetSupplyChannelEmptyEvaluator:Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            return new SupplyChannel();
        }
        public override string Comments=> "获取空的供货渠道记录";
    }
	
    /// <summary>
    /// 查询【供货渠道】列表
    /// </summary>
    public partial class GetSupplyChannelListEvaluator : Evaluator
    {
        public override string Comments=> "获取SupplyChannel列表 ";
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
				var datalevel = CurrentUserInformation?.DataLevel;
                var searchModel = HttpUtility.UrlDecode(request.data).Deserialize<SupplyChannelSearchModel>() ?? new SupplyChannelSearchModel();
                var query = ctx.SupplyChannel.Where(t=>t.IsDeleted==0 && t.DataLevel.StartsWith(datalevel));
                var @params = request.context.Request.Params;
                searchModel.PageSize = (@params["limit"] ?? searchModel.PageSize.ToString()).ToInt();
				searchModel.PageSize = searchModel.PageSize==0?4000:searchModel.PageSize;
                searchModel.PageIndex = (@params["offset"]).ToInt()/ searchModel.PageSize;
                var isordered = false;
                var search = searchModel.SearchKey ?? @params["search"];
                var sort = searchModel.Sort ?? @params["sort"];
				var order = @params["order"];
				// SCSupplier NVARCHAR(50) 供应商 
                if(!string.IsNullOrEmpty(searchModel.SCSupplier)) query = query.Where(t=>t.SCSupplier.Contains(searchModel.SCSupplier));
                if(sort=="SCSupplier")
                {
					query = order=="asc"?query.OrderBy(t=>t.SCSupplier):query.OrderByDescending(t=>t.SCSupplier);
                    isordered = true;
                }
				// SCTypeOfGoods NVARCHAR(50) 货物种类 
                if(!string.IsNullOrEmpty(searchModel.SCTypeOfGoods)) query = query.Where(t=>t.SCTypeOfGoods.Contains(searchModel.SCTypeOfGoods));
                if(sort=="SCTypeOfGoods")
                {
					query = order=="asc"?query.OrderBy(t=>t.SCTypeOfGoods):query.OrderByDescending(t=>t.SCTypeOfGoods);
                    isordered = true;
                }
				if(!string.IsNullOrEmpty(search)){
					query = query.Where(t=>t.id!=-1||t.SCSupplier.Contains(search)||t.SCTypeOfGoods.Contains(search));
				}
                if(sort=="ord")
                {
					query = order=="asc"?query.OrderBy(t=>t.ord):query.OrderByDescending(t=>t.ord);
                    isordered = true;
                }

                if(!isordered) query = query.OrderByDescending(t=>t.UpdateOn);
                var rows = query.Skip((searchModel.PageIndex)*searchModel.PageSize).Take(searchModel.PageSize).ToList();
                var total = query.Count();
                var sql = query.ToString();
                return new CommonOutputList<SupplyChannel>
                {
                    success = true, rows = rows, total = total, message="查询成功"
                };
            }
        }
    }


    /// <summary>
    /// 【订单】7次查询分别得到七天内记录的条数，形成柱状图，折线图
    /// </summary>
    public partial class OrderCountEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
                var now = Parse(request.context.Request.Params["date"] ?? Now.ToString("yyyy-MM-dd"));
                var dates = Range(1, 7).Select(p => now.AddDays(-1 * p)).ToList();
                var list = dates.Select(p => ctx.Order.Count(m => m.CreateOn==p)).ToList();
                return new
                {
                    sum = list.Sum(),
                    xaxis = dates.Select(p => p.ToString("yyyy-MM-dd")).ToList(),
                    series = list
                };
            }
        }
        public override string Comments=> "【订单】7次查询分别得到七天内记录的条数，形成柱状图，折线图";
    }
	public partial class TruncateOrderEvaluator : Evaluator
	{
        protected override object Evaluate(CommonRequest request)
		{
            using (var ctx = new DefaultContext())
			{
                ctx.Order.RemoveRange(ctx.Order);
				ctx.SaveChanges();
			}
			return new
			{
				success = true,
				message = "操作成功"
			};
		}
	}
    /// <summary>
    /// 删除【订单】
    /// </summary>
    public partial class DeleteOrderEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            var data = JsonConvert.DeserializeObject<Order>(HttpUtility.UrlDecode(request.data));
            if (data == null)
                return new CommonOutputT<string>
                {
                    success = false,
                    message = "删除成功"
                };
            using (var ctx = new DefaultContext())
            {
                var one = ctx.Order.Find(data.id);
				if(one==null){
					return new CommonOutputT<string>
					{
						success = false,
						message = "未找到需要删除的数据"
					};
				}
                one.IsDeleted = 1;
				ctx.Order.AddOrUpdate(one);
				ctx.SaveChanges();
				return new CommonOutputT<string>
				{
					success = true,
					message = "删除成功"
				};
            }
			
        }
        public override string Comments=> "删除一条订单记录";
    }
	
    /// <summary>
    /// 保存【订单】
    /// </summary>
    public partial class SaveOrderEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            var user = CurrentUserInformation;
            if (user==null)
            {
                return new
                {
                    success = false,
                    message = "请登录"
                };
            }
			var s = request.data;
            if (s==null)
            {
                return new
                {
                    success = false,
                    message = "缺少参数"
                };
            }
			Order entity = null;
			try
			{
				entity = JsonConvert.DeserializeObject<Order>(HttpUtility.UrlDecode(s));
			}
			catch(Exception exception)
			{
				return new
				{
					success = false,
					message = $"填写内容格式错误：{exception.Message}",
					input = s
				};
			}
            if (entity==null)
            {
                return new
                {
                    success = false,
                    message = "参数格式不正确"
                };
            }

			try
			{
				foreach (ValidationResult result in Validation.Validate(entity))
				{
					return new
					{
						success = false,
						message = result.Message
					};
				}
			}
			catch(Exception exception)
			{
				return new
				{
					success = false,
					message = exception.Message
				};
			}
			
            using (var ctx = new DefaultContext())
            {
				// 行级排他锁开始
                var transactionId = Guid.NewGuid().ToString();
                var isnew = entity.TransactionID == null;
                if (!isnew)
                {
                    var one = ctx.Order.FirstOrDefault(p=>p.id==entity.id);
                    if(one==null) return new
                    {
                        success = false,
                        message = "编辑错误，未找到ID"
                    };
                    if (one.VersionNo != entity.VersionNo) return new
                    {
                        success = false,
                        message = "发生数据写冲突"
                    };
                    one.VersionNo++;
                    one.TransactionID = transactionId;
					ctx.Order.AddOrUpdate(one);
					try
					{
						ctx.SaveChanges();
					}
					catch(Exception exception)
					{
						// 遇到数据库中的脏数据，走到这里，前面的Entity数据合法，直接跳过这里的数据校验阶段，使用最先到达的正确数据。
						// return new
						// {
						// 	 success = false,
						// 	 message = exception.Message,
						// 	 exception, one, transactionId, entity
						// };
					}
                    entity.VersionNo = one.VersionNo;
                }
				

								// NVARCHAR(50) 订单编号
				entity.OOrderNumber = HttpUtility.UrlDecode(entity.OOrderNumber);
					// NVARCHAR(50) 供应商编号
				entity.OSupplierNumber = HttpUtility.UrlDecode(entity.OSupplierNumber);
					// NVARCHAR(50) 提交人
				entity.OSubmitter = HttpUtility.UrlDecode(entity.OSubmitter);
					// NVARCHAR(50) 提交人联系方式
				entity.OAuthorsContactInformation = HttpUtility.UrlDecode(entity.OAuthorsContactInformation);
					// NVARCHAR(50) 订单状态
				entity.OOrderStatus = HttpUtility.UrlDecode(entity.OOrderStatus);
					// NVARCHAR(50) 备注
				entity.ORemarks = HttpUtility.UrlDecode(entity.ORemarks);
	
                entity.CreateBy = entity.CreateBy ?? user?.UILoginName ?? "未登录用户";
                entity.UpdateBy = user?.UILoginName ?? "未登录用户";
                entity.CreateOn = entity.CreateOn ?? Now;
                entity.TransactionID = transactionId;
                entity.UpdateOn = Now;
                entity.IsDeleted = 0;
	            entity.VersionNo = entity.VersionNo ?? 0;
                entity.DataLevel = entity.DataLevel ?? user?.DataLevel ?? "019999";
				ctx.Order.AddOrUpdate(entity);
				try
				{
					ctx.SaveChanges();
				}
				catch(Exception exception)
				{
					return new
					{
						success = false,
						message = exception.Message,
						exception, transactionId, entity
					};
				}
				// 行级排他锁结束
                return new
                {
                    success = true,
                    message = "操作成功"
                };
            }
        }
        public override string Comments=> "保存一条Order记录";
    }
	
    /// <summary>
    /// 查询空的【订单】
    /// </summary>
    public partial class GetOrderEmptyEvaluator:Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            return new Order();
        }
        public override string Comments=> "获取空的订单记录";
    }
	
    /// <summary>
    /// 查询【订单】列表
    /// </summary>
    public partial class GetOrderListEvaluator : Evaluator
    {
        public override string Comments=> "获取Order列表 ";
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
				var datalevel = CurrentUserInformation?.DataLevel;
                var searchModel = HttpUtility.UrlDecode(request.data).Deserialize<OrderSearchModel>() ?? new OrderSearchModel();
                var query = ctx.Order.Where(t=>t.IsDeleted==0 && t.DataLevel.StartsWith(datalevel));
                var @params = request.context.Request.Params;
                searchModel.PageSize = (@params["limit"] ?? searchModel.PageSize.ToString()).ToInt();
				searchModel.PageSize = searchModel.PageSize==0?4000:searchModel.PageSize;
                searchModel.PageIndex = (@params["offset"]).ToInt()/ searchModel.PageSize;
                var isordered = false;
                var search = searchModel.SearchKey ?? @params["search"];
                var sort = searchModel.Sort ?? @params["sort"];
				var order = @params["order"];
				// OOrderNumber NVARCHAR(50) 订单编号 
                if(!string.IsNullOrEmpty(searchModel.OOrderNumber)) query = query.Where(t=>t.OOrderNumber.Contains(searchModel.OOrderNumber));
                if(sort=="OOrderNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.OOrderNumber):query.OrderByDescending(t=>t.OOrderNumber);
                    isordered = true;
                }
				// OSupplierNumber NVARCHAR(50) 供应商编号 
                if(!string.IsNullOrEmpty(searchModel.OSupplierNumber)) query = query.Where(t=>t.OSupplierNumber.Contains(searchModel.OSupplierNumber));
                if(sort=="OSupplierNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.OSupplierNumber):query.OrderByDescending(t=>t.OSupplierNumber);
                    isordered = true;
                }
				// OExpectedArrivalDate DATETIME 期望到达日期 
                if(searchModel.FromOExpectedArrivalDate!=null) query = query.Where(t=>t.OExpectedArrivalDate>=searchModel.FromOExpectedArrivalDate);
                if(searchModel.ToOExpectedArrivalDate!=null) query = query.Where(t=>t.OExpectedArrivalDate<=searchModel.ToOExpectedArrivalDate);
                if(sort=="OExpectedArrivalDate")
                {
					query = order=="asc"?query.OrderBy(t=>t.OExpectedArrivalDate):query.OrderByDescending(t=>t.OExpectedArrivalDate);
                    isordered = true;
                }
				// ODateOfSubmission DATETIME 提交日期 
                if(searchModel.FromODateOfSubmission!=null) query = query.Where(t=>t.ODateOfSubmission>=searchModel.FromODateOfSubmission);
                if(searchModel.ToODateOfSubmission!=null) query = query.Where(t=>t.ODateOfSubmission<=searchModel.ToODateOfSubmission);
                if(sort=="ODateOfSubmission")
                {
					query = order=="asc"?query.OrderBy(t=>t.ODateOfSubmission):query.OrderByDescending(t=>t.ODateOfSubmission);
                    isordered = true;
                }
				// OSubmitter NVARCHAR(50) 提交人 
                if(!string.IsNullOrEmpty(searchModel.OSubmitter)) query = query.Where(t=>t.OSubmitter.Contains(searchModel.OSubmitter));
                if(sort=="OSubmitter")
                {
					query = order=="asc"?query.OrderBy(t=>t.OSubmitter):query.OrderByDescending(t=>t.OSubmitter);
                    isordered = true;
                }
				// OAuthorsContactInformation NVARCHAR(50) 提交人联系方式 
                if(!string.IsNullOrEmpty(searchModel.OAuthorsContactInformation)) query = query.Where(t=>t.OAuthorsContactInformation.Contains(searchModel.OAuthorsContactInformation));
                if(sort=="OAuthorsContactInformation")
                {
					query = order=="asc"?query.OrderBy(t=>t.OAuthorsContactInformation):query.OrderByDescending(t=>t.OAuthorsContactInformation);
                    isordered = true;
                }
				// OOrderStatus NVARCHAR(50) 订单状态 
                if(!string.IsNullOrEmpty(searchModel.OOrderStatus)) query = query.Where(t=>t.OOrderStatus.Contains(searchModel.OOrderStatus));
                if(sort=="OOrderStatus")
                {
					query = order=="asc"?query.OrderBy(t=>t.OOrderStatus):query.OrderByDescending(t=>t.OOrderStatus);
                    isordered = true;
                }
				// ORemarks NVARCHAR(50) 备注 
                if(!string.IsNullOrEmpty(searchModel.ORemarks)) query = query.Where(t=>t.ORemarks.Contains(searchModel.ORemarks));
                if(sort=="ORemarks")
                {
					query = order=="asc"?query.OrderBy(t=>t.ORemarks):query.OrderByDescending(t=>t.ORemarks);
                    isordered = true;
                }
				if(!string.IsNullOrEmpty(search)){
					query = query.Where(t=>t.id!=-1||t.OOrderNumber.Contains(search)||t.OSupplierNumber.Contains(search)||t.OSubmitter.Contains(search)||t.OAuthorsContactInformation.Contains(search)||t.OOrderStatus.Contains(search)||t.ORemarks.Contains(search));
				}
                if(sort=="ord")
                {
					query = order=="asc"?query.OrderBy(t=>t.ord):query.OrderByDescending(t=>t.ord);
                    isordered = true;
                }

                if(!isordered) query = query.OrderByDescending(t=>t.UpdateOn);
                var rows = query.Skip((searchModel.PageIndex)*searchModel.PageSize).Take(searchModel.PageSize).ToList();
                var total = query.Count();
                var sql = query.ToString();
                return new CommonOutputList<Order>
                {
                    success = true, rows = rows, total = total, message="查询成功"
                };
            }
        }
    }


    /// <summary>
    /// 【订单明细】7次查询分别得到七天内记录的条数，形成柱状图，折线图
    /// </summary>
    public partial class OrderDetailsCountEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
                var now = Parse(request.context.Request.Params["date"] ?? Now.ToString("yyyy-MM-dd"));
                var dates = Range(1, 7).Select(p => now.AddDays(-1 * p)).ToList();
                var list = dates.Select(p => ctx.OrderDetails.Count(m => m.CreateOn==p)).ToList();
                return new
                {
                    sum = list.Sum(),
                    xaxis = dates.Select(p => p.ToString("yyyy-MM-dd")).ToList(),
                    series = list
                };
            }
        }
        public override string Comments=> "【订单明细】7次查询分别得到七天内记录的条数，形成柱状图，折线图";
    }
	public partial class TruncateOrderDetailsEvaluator : Evaluator
	{
        protected override object Evaluate(CommonRequest request)
		{
            using (var ctx = new DefaultContext())
			{
                ctx.OrderDetails.RemoveRange(ctx.OrderDetails);
				ctx.SaveChanges();
			}
			return new
			{
				success = true,
				message = "操作成功"
			};
		}
	}
    /// <summary>
    /// 删除【订单明细】
    /// </summary>
    public partial class DeleteOrderDetailsEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            var data = JsonConvert.DeserializeObject<OrderDetails>(HttpUtility.UrlDecode(request.data));
            if (data == null)
                return new CommonOutputT<string>
                {
                    success = false,
                    message = "删除成功"
                };
            using (var ctx = new DefaultContext())
            {
                var one = ctx.OrderDetails.Find(data.id);
				if(one==null){
					return new CommonOutputT<string>
					{
						success = false,
						message = "未找到需要删除的数据"
					};
				}
                one.IsDeleted = 1;
				ctx.OrderDetails.AddOrUpdate(one);
				ctx.SaveChanges();
				return new CommonOutputT<string>
				{
					success = true,
					message = "删除成功"
				};
            }
			
        }
        public override string Comments=> "删除一条订单明细记录";
    }
	
    /// <summary>
    /// 保存【订单明细】
    /// </summary>
    public partial class SaveOrderDetailsEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            var user = CurrentUserInformation;
            if (user==null)
            {
                return new
                {
                    success = false,
                    message = "请登录"
                };
            }
			var s = request.data;
            if (s==null)
            {
                return new
                {
                    success = false,
                    message = "缺少参数"
                };
            }
			OrderDetails entity = null;
			try
			{
				entity = JsonConvert.DeserializeObject<OrderDetails>(HttpUtility.UrlDecode(s));
			}
			catch(Exception exception)
			{
				return new
				{
					success = false,
					message = $"填写内容格式错误：{exception.Message}",
					input = s
				};
			}
            if (entity==null)
            {
                return new
                {
                    success = false,
                    message = "参数格式不正确"
                };
            }

			try
			{
				foreach (ValidationResult result in Validation.Validate(entity))
				{
					return new
					{
						success = false,
						message = result.Message
					};
				}
			}
			catch(Exception exception)
			{
				return new
				{
					success = false,
					message = exception.Message
				};
			}
			
            using (var ctx = new DefaultContext())
            {
				// 行级排他锁开始
                var transactionId = Guid.NewGuid().ToString();
                var isnew = entity.TransactionID == null;
                if (!isnew)
                {
                    var one = ctx.OrderDetails.FirstOrDefault(p=>p.id==entity.id);
                    if(one==null) return new
                    {
                        success = false,
                        message = "编辑错误，未找到ID"
                    };
                    if (one.VersionNo != entity.VersionNo) return new
                    {
                        success = false,
                        message = "发生数据写冲突"
                    };
                    one.VersionNo++;
                    one.TransactionID = transactionId;
					ctx.OrderDetails.AddOrUpdate(one);
					try
					{
						ctx.SaveChanges();
					}
					catch(Exception exception)
					{
						// 遇到数据库中的脏数据，走到这里，前面的Entity数据合法，直接跳过这里的数据校验阶段，使用最先到达的正确数据。
						// return new
						// {
						// 	 success = false,
						// 	 message = exception.Message,
						// 	 exception, one, transactionId, entity
						// };
					}
                    entity.VersionNo = one.VersionNo;
                }
				

								// NVARCHAR(50) 订单编号
				entity.ODOrderNumber = HttpUtility.UrlDecode(entity.ODOrderNumber);
					// NVARCHAR(50) 货物种类
				entity.ODTypeOfGoods = HttpUtility.UrlDecode(entity.ODTypeOfGoods);
					// NVARCHAR(50) 货物数量
				entity.ODQuantityOfGoods = HttpUtility.UrlDecode(entity.ODQuantityOfGoods);
					// NVARCHAR(50) 采购单价
				entity.ODPurchaseUnitPrice = HttpUtility.UrlDecode(entity.ODPurchaseUnitPrice);
	
                entity.CreateBy = entity.CreateBy ?? user?.UILoginName ?? "未登录用户";
                entity.UpdateBy = user?.UILoginName ?? "未登录用户";
                entity.CreateOn = entity.CreateOn ?? Now;
                entity.TransactionID = transactionId;
                entity.UpdateOn = Now;
                entity.IsDeleted = 0;
	            entity.VersionNo = entity.VersionNo ?? 0;
                entity.DataLevel = entity.DataLevel ?? user?.DataLevel ?? "019999";
				ctx.OrderDetails.AddOrUpdate(entity);
				try
				{
					ctx.SaveChanges();
				}
				catch(Exception exception)
				{
					return new
					{
						success = false,
						message = exception.Message,
						exception, transactionId, entity
					};
				}
				// 行级排他锁结束
                return new
                {
                    success = true,
                    message = "操作成功"
                };
            }
        }
        public override string Comments=> "保存一条OrderDetails记录";
    }
	
    /// <summary>
    /// 查询空的【订单明细】
    /// </summary>
    public partial class GetOrderDetailsEmptyEvaluator:Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            return new OrderDetails();
        }
        public override string Comments=> "获取空的订单明细记录";
    }
	
    /// <summary>
    /// 查询【订单明细】列表
    /// </summary>
    public partial class GetOrderDetailsListEvaluator : Evaluator
    {
        public override string Comments=> "获取OrderDetails列表 ";
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
				var datalevel = CurrentUserInformation?.DataLevel;
                var searchModel = HttpUtility.UrlDecode(request.data).Deserialize<OrderDetailsSearchModel>() ?? new OrderDetailsSearchModel();
                var query = ctx.OrderDetails.Where(t=>t.IsDeleted==0 && t.DataLevel.StartsWith(datalevel));
                var @params = request.context.Request.Params;
                searchModel.PageSize = (@params["limit"] ?? searchModel.PageSize.ToString()).ToInt();
				searchModel.PageSize = searchModel.PageSize==0?4000:searchModel.PageSize;
                searchModel.PageIndex = (@params["offset"]).ToInt()/ searchModel.PageSize;
                var isordered = false;
                var search = searchModel.SearchKey ?? @params["search"];
                var sort = searchModel.Sort ?? @params["sort"];
				var order = @params["order"];
				// ODOrderNumber NVARCHAR(50) 订单编号 
                if(!string.IsNullOrEmpty(searchModel.ODOrderNumber)) query = query.Where(t=>t.ODOrderNumber.Contains(searchModel.ODOrderNumber));
                if(sort=="ODOrderNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.ODOrderNumber):query.OrderByDescending(t=>t.ODOrderNumber);
                    isordered = true;
                }
				// ODTypeOfGoods NVARCHAR(50) 货物种类 
                if(!string.IsNullOrEmpty(searchModel.ODTypeOfGoods)) query = query.Where(t=>t.ODTypeOfGoods.Contains(searchModel.ODTypeOfGoods));
                if(sort=="ODTypeOfGoods")
                {
					query = order=="asc"?query.OrderBy(t=>t.ODTypeOfGoods):query.OrderByDescending(t=>t.ODTypeOfGoods);
                    isordered = true;
                }
				// ODQuantityOfGoods NVARCHAR(50) 货物数量 
                if(!string.IsNullOrEmpty(searchModel.ODQuantityOfGoods)) query = query.Where(t=>t.ODQuantityOfGoods.Contains(searchModel.ODQuantityOfGoods));
                if(sort=="ODQuantityOfGoods")
                {
					query = order=="asc"?query.OrderBy(t=>t.ODQuantityOfGoods):query.OrderByDescending(t=>t.ODQuantityOfGoods);
                    isordered = true;
                }
				// ODPurchaseUnitPrice NVARCHAR(50) 采购单价 
                if(!string.IsNullOrEmpty(searchModel.ODPurchaseUnitPrice)) query = query.Where(t=>t.ODPurchaseUnitPrice.Contains(searchModel.ODPurchaseUnitPrice));
                if(sort=="ODPurchaseUnitPrice")
                {
					query = order=="asc"?query.OrderBy(t=>t.ODPurchaseUnitPrice):query.OrderByDescending(t=>t.ODPurchaseUnitPrice);
                    isordered = true;
                }
				if(!string.IsNullOrEmpty(search)){
					query = query.Where(t=>t.id!=-1||t.ODOrderNumber.Contains(search)||t.ODTypeOfGoods.Contains(search)||t.ODQuantityOfGoods.Contains(search)||t.ODPurchaseUnitPrice.Contains(search));
				}
                if(sort=="ord")
                {
					query = order=="asc"?query.OrderBy(t=>t.ord):query.OrderByDescending(t=>t.ord);
                    isordered = true;
                }

                if(!isordered) query = query.OrderByDescending(t=>t.UpdateOn);
                var rows = query.Skip((searchModel.PageIndex)*searchModel.PageSize).Take(searchModel.PageSize).ToList();
                var total = query.Count();
                var sql = query.ToString();
                return new CommonOutputList<OrderDetails>
                {
                    success = true, rows = rows, total = total, message="查询成功"
                };
            }
        }
    }


    /// <summary>
    /// 【入库记录】7次查询分别得到七天内记录的条数，形成柱状图，折线图
    /// </summary>
    public partial class WarehousingRecordCountEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
                var now = Parse(request.context.Request.Params["date"] ?? Now.ToString("yyyy-MM-dd"));
                var dates = Range(1, 7).Select(p => now.AddDays(-1 * p)).ToList();
                var list = dates.Select(p => ctx.WarehousingRecord.Count(m => m.CreateOn==p)).ToList();
                return new
                {
                    sum = list.Sum(),
                    xaxis = dates.Select(p => p.ToString("yyyy-MM-dd")).ToList(),
                    series = list
                };
            }
        }
        public override string Comments=> "【入库记录】7次查询分别得到七天内记录的条数，形成柱状图，折线图";
    }
	public partial class TruncateWarehousingRecordEvaluator : Evaluator
	{
        protected override object Evaluate(CommonRequest request)
		{
            using (var ctx = new DefaultContext())
			{
                ctx.WarehousingRecord.RemoveRange(ctx.WarehousingRecord);
				ctx.SaveChanges();
			}
			return new
			{
				success = true,
				message = "操作成功"
			};
		}
	}
    /// <summary>
    /// 删除【入库记录】
    /// </summary>
    public partial class DeleteWarehousingRecordEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            var data = JsonConvert.DeserializeObject<WarehousingRecord>(HttpUtility.UrlDecode(request.data));
            if (data == null)
                return new CommonOutputT<string>
                {
                    success = false,
                    message = "删除成功"
                };
            using (var ctx = new DefaultContext())
            {
                var one = ctx.WarehousingRecord.Find(data.id);
				if(one==null){
					return new CommonOutputT<string>
					{
						success = false,
						message = "未找到需要删除的数据"
					};
				}
                one.IsDeleted = 1;
				ctx.WarehousingRecord.AddOrUpdate(one);
				ctx.SaveChanges();
				return new CommonOutputT<string>
				{
					success = true,
					message = "删除成功"
				};
            }
			
        }
        public override string Comments=> "删除一条入库记录记录";
    }
	
    /// <summary>
    /// 保存【入库记录】
    /// </summary>
    public partial class SaveWarehousingRecordEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            var user = CurrentUserInformation;
            if (user==null)
            {
                return new
                {
                    success = false,
                    message = "请登录"
                };
            }
			var s = request.data;
            if (s==null)
            {
                return new
                {
                    success = false,
                    message = "缺少参数"
                };
            }
			WarehousingRecord entity = null;
			try
			{
				entity = JsonConvert.DeserializeObject<WarehousingRecord>(HttpUtility.UrlDecode(s));
			}
			catch(Exception exception)
			{
				return new
				{
					success = false,
					message = $"填写内容格式错误：{exception.Message}",
					input = s
				};
			}
            if (entity==null)
            {
                return new
                {
                    success = false,
                    message = "参数格式不正确"
                };
            }

			try
			{
				foreach (ValidationResult result in Validation.Validate(entity))
				{
					return new
					{
						success = false,
						message = result.Message
					};
				}
			}
			catch(Exception exception)
			{
				return new
				{
					success = false,
					message = exception.Message
				};
			}
			
            using (var ctx = new DefaultContext())
            {
				// 行级排他锁开始
                var transactionId = Guid.NewGuid().ToString();
                var isnew = entity.TransactionID == null;
                if (!isnew)
                {
                    var one = ctx.WarehousingRecord.FirstOrDefault(p=>p.id==entity.id);
                    if(one==null) return new
                    {
                        success = false,
                        message = "编辑错误，未找到ID"
                    };
                    if (one.VersionNo != entity.VersionNo) return new
                    {
                        success = false,
                        message = "发生数据写冲突"
                    };
                    one.VersionNo++;
                    one.TransactionID = transactionId;
					ctx.WarehousingRecord.AddOrUpdate(one);
					try
					{
						ctx.SaveChanges();
					}
					catch(Exception exception)
					{
						// 遇到数据库中的脏数据，走到这里，前面的Entity数据合法，直接跳过这里的数据校验阶段，使用最先到达的正确数据。
						// return new
						// {
						// 	 success = false,
						// 	 message = exception.Message,
						// 	 exception, one, transactionId, entity
						// };
					}
                    entity.VersionNo = one.VersionNo;
                }
				

								// NVARCHAR(50) 订单编号
				entity.WROrderNumber = HttpUtility.UrlDecode(entity.WROrderNumber);
					// NVARCHAR(50) 经办人
				entity.WRAgent = HttpUtility.UrlDecode(entity.WRAgent);
					// NVARCHAR(50) 经办人联系方式
				entity.WROperatorContact = HttpUtility.UrlDecode(entity.WROperatorContact);
					// NVARCHAR(50) 备注
				entity.WRRemarks = HttpUtility.UrlDecode(entity.WRRemarks);
	
                entity.CreateBy = entity.CreateBy ?? user?.UILoginName ?? "未登录用户";
                entity.UpdateBy = user?.UILoginName ?? "未登录用户";
                entity.CreateOn = entity.CreateOn ?? Now;
                entity.TransactionID = transactionId;
                entity.UpdateOn = Now;
                entity.IsDeleted = 0;
	            entity.VersionNo = entity.VersionNo ?? 0;
                entity.DataLevel = entity.DataLevel ?? user?.DataLevel ?? "019999";
				ctx.WarehousingRecord.AddOrUpdate(entity);
				try
				{
					ctx.SaveChanges();
				}
				catch(Exception exception)
				{
					return new
					{
						success = false,
						message = exception.Message,
						exception, transactionId, entity
					};
				}
				// 行级排他锁结束
                return new
                {
                    success = true,
                    message = "操作成功"
                };
            }
        }
        public override string Comments=> "保存一条WarehousingRecord记录";
    }
	
    /// <summary>
    /// 查询空的【入库记录】
    /// </summary>
    public partial class GetWarehousingRecordEmptyEvaluator:Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            return new WarehousingRecord();
        }
        public override string Comments=> "获取空的入库记录记录";
    }
	
    /// <summary>
    /// 查询【入库记录】列表
    /// </summary>
    public partial class GetWarehousingRecordListEvaluator : Evaluator
    {
        public override string Comments=> "获取WarehousingRecord列表 ";
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
				var datalevel = CurrentUserInformation?.DataLevel;
                var searchModel = HttpUtility.UrlDecode(request.data).Deserialize<WarehousingRecordSearchModel>() ?? new WarehousingRecordSearchModel();
                var query = ctx.WarehousingRecord.Where(t=>t.IsDeleted==0 && t.DataLevel.StartsWith(datalevel));
                var @params = request.context.Request.Params;
                searchModel.PageSize = (@params["limit"] ?? searchModel.PageSize.ToString()).ToInt();
				searchModel.PageSize = searchModel.PageSize==0?4000:searchModel.PageSize;
                searchModel.PageIndex = (@params["offset"]).ToInt()/ searchModel.PageSize;
                var isordered = false;
                var search = searchModel.SearchKey ?? @params["search"];
                var sort = searchModel.Sort ?? @params["sort"];
				var order = @params["order"];
				// WROrderNumber NVARCHAR(50) 订单编号 
                if(!string.IsNullOrEmpty(searchModel.WROrderNumber)) query = query.Where(t=>t.WROrderNumber.Contains(searchModel.WROrderNumber));
                if(sort=="WROrderNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.WROrderNumber):query.OrderByDescending(t=>t.WROrderNumber);
                    isordered = true;
                }
				// WRDateOfArrival DATETIME 到货日期 
                if(searchModel.FromWRDateOfArrival!=null) query = query.Where(t=>t.WRDateOfArrival>=searchModel.FromWRDateOfArrival);
                if(searchModel.ToWRDateOfArrival!=null) query = query.Where(t=>t.WRDateOfArrival<=searchModel.ToWRDateOfArrival);
                if(sort=="WRDateOfArrival")
                {
					query = order=="asc"?query.OrderBy(t=>t.WRDateOfArrival):query.OrderByDescending(t=>t.WRDateOfArrival);
                    isordered = true;
                }
				// WRAgent NVARCHAR(50) 经办人 
                if(!string.IsNullOrEmpty(searchModel.WRAgent)) query = query.Where(t=>t.WRAgent.Contains(searchModel.WRAgent));
                if(sort=="WRAgent")
                {
					query = order=="asc"?query.OrderBy(t=>t.WRAgent):query.OrderByDescending(t=>t.WRAgent);
                    isordered = true;
                }
				// WROperatorContact NVARCHAR(50) 经办人联系方式 
                if(!string.IsNullOrEmpty(searchModel.WROperatorContact)) query = query.Where(t=>t.WROperatorContact.Contains(searchModel.WROperatorContact));
                if(sort=="WROperatorContact")
                {
					query = order=="asc"?query.OrderBy(t=>t.WROperatorContact):query.OrderByDescending(t=>t.WROperatorContact);
                    isordered = true;
                }
				// WRRemarks NVARCHAR(50) 备注 
                if(!string.IsNullOrEmpty(searchModel.WRRemarks)) query = query.Where(t=>t.WRRemarks.Contains(searchModel.WRRemarks));
                if(sort=="WRRemarks")
                {
					query = order=="asc"?query.OrderBy(t=>t.WRRemarks):query.OrderByDescending(t=>t.WRRemarks);
                    isordered = true;
                }
				if(!string.IsNullOrEmpty(search)){
					query = query.Where(t=>t.id!=-1||t.WROrderNumber.Contains(search)||t.WRAgent.Contains(search)||t.WROperatorContact.Contains(search)||t.WRRemarks.Contains(search));
				}
                if(sort=="ord")
                {
					query = order=="asc"?query.OrderBy(t=>t.ord):query.OrderByDescending(t=>t.ord);
                    isordered = true;
                }

                if(!isordered) query = query.OrderByDescending(t=>t.UpdateOn);
                var rows = query.Skip((searchModel.PageIndex)*searchModel.PageSize).Take(searchModel.PageSize).ToList();
                var total = query.Count();
                var sql = query.ToString();
                return new CommonOutputList<WarehousingRecord>
                {
                    success = true, rows = rows, total = total, message="查询成功"
                };
            }
        }
    }


    /// <summary>
    /// 【仓库】7次查询分别得到七天内记录的条数，形成柱状图，折线图
    /// </summary>
    public partial class WarehouseCountEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
                var now = Parse(request.context.Request.Params["date"] ?? Now.ToString("yyyy-MM-dd"));
                var dates = Range(1, 7).Select(p => now.AddDays(-1 * p)).ToList();
                var list = dates.Select(p => ctx.Warehouse.Count(m => m.CreateOn==p)).ToList();
                return new
                {
                    sum = list.Sum(),
                    xaxis = dates.Select(p => p.ToString("yyyy-MM-dd")).ToList(),
                    series = list
                };
            }
        }
        public override string Comments=> "【仓库】7次查询分别得到七天内记录的条数，形成柱状图，折线图";
    }
	public partial class TruncateWarehouseEvaluator : Evaluator
	{
        protected override object Evaluate(CommonRequest request)
		{
            using (var ctx = new DefaultContext())
			{
                ctx.Warehouse.RemoveRange(ctx.Warehouse);
				ctx.SaveChanges();
			}
			return new
			{
				success = true,
				message = "操作成功"
			};
		}
	}
    /// <summary>
    /// 删除【仓库】
    /// </summary>
    public partial class DeleteWarehouseEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            var data = JsonConvert.DeserializeObject<Warehouse>(HttpUtility.UrlDecode(request.data));
            if (data == null)
                return new CommonOutputT<string>
                {
                    success = false,
                    message = "删除成功"
                };
            using (var ctx = new DefaultContext())
            {
                var one = ctx.Warehouse.Find(data.id);
				if(one==null){
					return new CommonOutputT<string>
					{
						success = false,
						message = "未找到需要删除的数据"
					};
				}
                one.IsDeleted = 1;
				ctx.Warehouse.AddOrUpdate(one);
				ctx.SaveChanges();
				return new CommonOutputT<string>
				{
					success = true,
					message = "删除成功"
				};
            }
			
        }
        public override string Comments=> "删除一条仓库记录";
    }
	
    /// <summary>
    /// 保存【仓库】
    /// </summary>
    public partial class SaveWarehouseEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            var user = CurrentUserInformation;
            if (user==null)
            {
                return new
                {
                    success = false,
                    message = "请登录"
                };
            }
			var s = request.data;
            if (s==null)
            {
                return new
                {
                    success = false,
                    message = "缺少参数"
                };
            }
			Warehouse entity = null;
			try
			{
				entity = JsonConvert.DeserializeObject<Warehouse>(HttpUtility.UrlDecode(s));
			}
			catch(Exception exception)
			{
				return new
				{
					success = false,
					message = $"填写内容格式错误：{exception.Message}",
					input = s
				};
			}
            if (entity==null)
            {
                return new
                {
                    success = false,
                    message = "参数格式不正确"
                };
            }

			try
			{
				foreach (ValidationResult result in Validation.Validate(entity))
				{
					return new
					{
						success = false,
						message = result.Message
					};
				}
			}
			catch(Exception exception)
			{
				return new
				{
					success = false,
					message = exception.Message
				};
			}
			
            using (var ctx = new DefaultContext())
            {
				// 行级排他锁开始
                var transactionId = Guid.NewGuid().ToString();
                var isnew = entity.TransactionID == null;
                if (!isnew)
                {
                    var one = ctx.Warehouse.FirstOrDefault(p=>p.id==entity.id);
                    if(one==null) return new
                    {
                        success = false,
                        message = "编辑错误，未找到ID"
                    };
                    if (one.VersionNo != entity.VersionNo) return new
                    {
                        success = false,
                        message = "发生数据写冲突"
                    };
                    one.VersionNo++;
                    one.TransactionID = transactionId;
					ctx.Warehouse.AddOrUpdate(one);
					try
					{
						ctx.SaveChanges();
					}
					catch(Exception exception)
					{
						// 遇到数据库中的脏数据，走到这里，前面的Entity数据合法，直接跳过这里的数据校验阶段，使用最先到达的正确数据。
						// return new
						// {
						// 	 success = false,
						// 	 message = exception.Message,
						// 	 exception, one, transactionId, entity
						// };
					}
                    entity.VersionNo = one.VersionNo;
                }
				

								// NVARCHAR(50) 仓库编号
				entity.WWarehouseNumber = HttpUtility.UrlDecode(entity.WWarehouseNumber);
					// NVARCHAR(50) 容积
				entity.WCapacity = HttpUtility.UrlDecode(entity.WCapacity);
					// NVARCHAR(50) 位置
				entity.WLocation = HttpUtility.UrlDecode(entity.WLocation);
					// NVARCHAR(50) 负责人工号
				entity.WResponsibleForManualNumber = HttpUtility.UrlDecode(entity.WResponsibleForManualNumber);
	
                entity.CreateBy = entity.CreateBy ?? user?.UILoginName ?? "未登录用户";
                entity.UpdateBy = user?.UILoginName ?? "未登录用户";
                entity.CreateOn = entity.CreateOn ?? Now;
                entity.TransactionID = transactionId;
                entity.UpdateOn = Now;
                entity.IsDeleted = 0;
	            entity.VersionNo = entity.VersionNo ?? 0;
                entity.DataLevel = entity.DataLevel ?? user?.DataLevel ?? "019999";
				ctx.Warehouse.AddOrUpdate(entity);
				try
				{
					ctx.SaveChanges();
				}
				catch(Exception exception)
				{
					return new
					{
						success = false,
						message = exception.Message,
						exception, transactionId, entity
					};
				}
				// 行级排他锁结束
                return new
                {
                    success = true,
                    message = "操作成功"
                };
            }
        }
        public override string Comments=> "保存一条Warehouse记录";
    }
	
    /// <summary>
    /// 查询空的【仓库】
    /// </summary>
    public partial class GetWarehouseEmptyEvaluator:Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            return new Warehouse();
        }
        public override string Comments=> "获取空的仓库记录";
    }
	
    /// <summary>
    /// 查询【仓库】列表
    /// </summary>
    public partial class GetWarehouseListEvaluator : Evaluator
    {
        public override string Comments=> "获取Warehouse列表 ";
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
				var datalevel = CurrentUserInformation?.DataLevel;
                var searchModel = HttpUtility.UrlDecode(request.data).Deserialize<WarehouseSearchModel>() ?? new WarehouseSearchModel();
                var query = ctx.Warehouse.Where(t=>t.IsDeleted==0 && t.DataLevel.StartsWith(datalevel));
                var @params = request.context.Request.Params;
                searchModel.PageSize = (@params["limit"] ?? searchModel.PageSize.ToString()).ToInt();
				searchModel.PageSize = searchModel.PageSize==0?4000:searchModel.PageSize;
                searchModel.PageIndex = (@params["offset"]).ToInt()/ searchModel.PageSize;
                var isordered = false;
                var search = searchModel.SearchKey ?? @params["search"];
                var sort = searchModel.Sort ?? @params["sort"];
				var order = @params["order"];
				// WWarehouseNumber NVARCHAR(50) 仓库编号 
                if(!string.IsNullOrEmpty(searchModel.WWarehouseNumber)) query = query.Where(t=>t.WWarehouseNumber.Contains(searchModel.WWarehouseNumber));
                if(sort=="WWarehouseNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.WWarehouseNumber):query.OrderByDescending(t=>t.WWarehouseNumber);
                    isordered = true;
                }
				// WCapacity NVARCHAR(50) 容积 
                if(!string.IsNullOrEmpty(searchModel.WCapacity)) query = query.Where(t=>t.WCapacity.Contains(searchModel.WCapacity));
                if(sort=="WCapacity")
                {
					query = order=="asc"?query.OrderBy(t=>t.WCapacity):query.OrderByDescending(t=>t.WCapacity);
                    isordered = true;
                }
				// WLocation NVARCHAR(50) 位置 
                if(!string.IsNullOrEmpty(searchModel.WLocation)) query = query.Where(t=>t.WLocation.Contains(searchModel.WLocation));
                if(sort=="WLocation")
                {
					query = order=="asc"?query.OrderBy(t=>t.WLocation):query.OrderByDescending(t=>t.WLocation);
                    isordered = true;
                }
				// WResponsibleForManualNumber NVARCHAR(50) 负责人工号 
                if(!string.IsNullOrEmpty(searchModel.WResponsibleForManualNumber)) query = query.Where(t=>t.WResponsibleForManualNumber.Contains(searchModel.WResponsibleForManualNumber));
                if(sort=="WResponsibleForManualNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.WResponsibleForManualNumber):query.OrderByDescending(t=>t.WResponsibleForManualNumber);
                    isordered = true;
                }
				if(!string.IsNullOrEmpty(search)){
					query = query.Where(t=>t.id!=-1||t.WWarehouseNumber.Contains(search)||t.WCapacity.Contains(search)||t.WLocation.Contains(search)||t.WResponsibleForManualNumber.Contains(search));
				}
                if(sort=="ord")
                {
					query = order=="asc"?query.OrderBy(t=>t.ord):query.OrderByDescending(t=>t.ord);
                    isordered = true;
                }

                if(!isordered) query = query.OrderByDescending(t=>t.UpdateOn);
                var rows = query.Skip((searchModel.PageIndex)*searchModel.PageSize).Take(searchModel.PageSize).ToList();
                var total = query.Count();
                var sql = query.ToString();
                return new CommonOutputList<Warehouse>
                {
                    success = true, rows = rows, total = total, message="查询成功"
                };
            }
        }
    }


    /// <summary>
    /// 【货架】7次查询分别得到七天内记录的条数，形成柱状图，折线图
    /// </summary>
    public partial class GoodsShelvesCountEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
                var now = Parse(request.context.Request.Params["date"] ?? Now.ToString("yyyy-MM-dd"));
                var dates = Range(1, 7).Select(p => now.AddDays(-1 * p)).ToList();
                var list = dates.Select(p => ctx.GoodsShelves.Count(m => m.CreateOn==p)).ToList();
                return new
                {
                    sum = list.Sum(),
                    xaxis = dates.Select(p => p.ToString("yyyy-MM-dd")).ToList(),
                    series = list
                };
            }
        }
        public override string Comments=> "【货架】7次查询分别得到七天内记录的条数，形成柱状图，折线图";
    }
	public partial class TruncateGoodsShelvesEvaluator : Evaluator
	{
        protected override object Evaluate(CommonRequest request)
		{
            using (var ctx = new DefaultContext())
			{
                ctx.GoodsShelves.RemoveRange(ctx.GoodsShelves);
				ctx.SaveChanges();
			}
			return new
			{
				success = true,
				message = "操作成功"
			};
		}
	}
    /// <summary>
    /// 删除【货架】
    /// </summary>
    public partial class DeleteGoodsShelvesEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            var data = JsonConvert.DeserializeObject<GoodsShelves>(HttpUtility.UrlDecode(request.data));
            if (data == null)
                return new CommonOutputT<string>
                {
                    success = false,
                    message = "删除成功"
                };
            using (var ctx = new DefaultContext())
            {
                var one = ctx.GoodsShelves.Find(data.id);
				if(one==null){
					return new CommonOutputT<string>
					{
						success = false,
						message = "未找到需要删除的数据"
					};
				}
                one.IsDeleted = 1;
				ctx.GoodsShelves.AddOrUpdate(one);
				ctx.SaveChanges();
				return new CommonOutputT<string>
				{
					success = true,
					message = "删除成功"
				};
            }
			
        }
        public override string Comments=> "删除一条货架记录";
    }
	
    /// <summary>
    /// 保存【货架】
    /// </summary>
    public partial class SaveGoodsShelvesEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            var user = CurrentUserInformation;
            if (user==null)
            {
                return new
                {
                    success = false,
                    message = "请登录"
                };
            }
			var s = request.data;
            if (s==null)
            {
                return new
                {
                    success = false,
                    message = "缺少参数"
                };
            }
			GoodsShelves entity = null;
			try
			{
				entity = JsonConvert.DeserializeObject<GoodsShelves>(HttpUtility.UrlDecode(s));
			}
			catch(Exception exception)
			{
				return new
				{
					success = false,
					message = $"填写内容格式错误：{exception.Message}",
					input = s
				};
			}
            if (entity==null)
            {
                return new
                {
                    success = false,
                    message = "参数格式不正确"
                };
            }

			try
			{
				foreach (ValidationResult result in Validation.Validate(entity))
				{
					return new
					{
						success = false,
						message = result.Message
					};
				}
			}
			catch(Exception exception)
			{
				return new
				{
					success = false,
					message = exception.Message
				};
			}
			
            using (var ctx = new DefaultContext())
            {
				// 行级排他锁开始
                var transactionId = Guid.NewGuid().ToString();
                var isnew = entity.TransactionID == null;
                if (!isnew)
                {
                    var one = ctx.GoodsShelves.FirstOrDefault(p=>p.id==entity.id);
                    if(one==null) return new
                    {
                        success = false,
                        message = "编辑错误，未找到ID"
                    };
                    if (one.VersionNo != entity.VersionNo) return new
                    {
                        success = false,
                        message = "发生数据写冲突"
                    };
                    one.VersionNo++;
                    one.TransactionID = transactionId;
					ctx.GoodsShelves.AddOrUpdate(one);
					try
					{
						ctx.SaveChanges();
					}
					catch(Exception exception)
					{
						// 遇到数据库中的脏数据，走到这里，前面的Entity数据合法，直接跳过这里的数据校验阶段，使用最先到达的正确数据。
						// return new
						// {
						// 	 success = false,
						// 	 message = exception.Message,
						// 	 exception, one, transactionId, entity
						// };
					}
                    entity.VersionNo = one.VersionNo;
                }
				

								// NVARCHAR(50) 货架编号
				entity.GSShelfNumber = HttpUtility.UrlDecode(entity.GSShelfNumber);
					// NVARCHAR(50) 容积 
				entity.GSVolume = HttpUtility.UrlDecode(entity.GSVolume);
					// NVARCHAR(50) 位置
				entity.GSLocation = HttpUtility.UrlDecode(entity.GSLocation);
					// NVARCHAR(50) 负责人工号
				entity.GSResponsibleForManualNumber = HttpUtility.UrlDecode(entity.GSResponsibleForManualNumber);
	
                entity.CreateBy = entity.CreateBy ?? user?.UILoginName ?? "未登录用户";
                entity.UpdateBy = user?.UILoginName ?? "未登录用户";
                entity.CreateOn = entity.CreateOn ?? Now;
                entity.TransactionID = transactionId;
                entity.UpdateOn = Now;
                entity.IsDeleted = 0;
	            entity.VersionNo = entity.VersionNo ?? 0;
                entity.DataLevel = entity.DataLevel ?? user?.DataLevel ?? "019999";
				ctx.GoodsShelves.AddOrUpdate(entity);
				try
				{
					ctx.SaveChanges();
				}
				catch(Exception exception)
				{
					return new
					{
						success = false,
						message = exception.Message,
						exception, transactionId, entity
					};
				}
				// 行级排他锁结束
                return new
                {
                    success = true,
                    message = "操作成功"
                };
            }
        }
        public override string Comments=> "保存一条GoodsShelves记录";
    }
	
    /// <summary>
    /// 查询空的【货架】
    /// </summary>
    public partial class GetGoodsShelvesEmptyEvaluator:Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            return new GoodsShelves();
        }
        public override string Comments=> "获取空的货架记录";
    }
	
    /// <summary>
    /// 查询【货架】列表
    /// </summary>
    public partial class GetGoodsShelvesListEvaluator : Evaluator
    {
        public override string Comments=> "获取GoodsShelves列表 ";
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
				var datalevel = CurrentUserInformation?.DataLevel;
                var searchModel = HttpUtility.UrlDecode(request.data).Deserialize<GoodsShelvesSearchModel>() ?? new GoodsShelvesSearchModel();
                var query = ctx.GoodsShelves.Where(t=>t.IsDeleted==0 && t.DataLevel.StartsWith(datalevel));
                var @params = request.context.Request.Params;
                searchModel.PageSize = (@params["limit"] ?? searchModel.PageSize.ToString()).ToInt();
				searchModel.PageSize = searchModel.PageSize==0?4000:searchModel.PageSize;
                searchModel.PageIndex = (@params["offset"]).ToInt()/ searchModel.PageSize;
                var isordered = false;
                var search = searchModel.SearchKey ?? @params["search"];
                var sort = searchModel.Sort ?? @params["sort"];
				var order = @params["order"];
				// GSShelfNumber NVARCHAR(50) 货架编号 
                if(!string.IsNullOrEmpty(searchModel.GSShelfNumber)) query = query.Where(t=>t.GSShelfNumber.Contains(searchModel.GSShelfNumber));
                if(sort=="GSShelfNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.GSShelfNumber):query.OrderByDescending(t=>t.GSShelfNumber);
                    isordered = true;
                }
				// GSVolume NVARCHAR(50) 容积  
                if(!string.IsNullOrEmpty(searchModel.GSVolume)) query = query.Where(t=>t.GSVolume.Contains(searchModel.GSVolume));
                if(sort=="GSVolume")
                {
					query = order=="asc"?query.OrderBy(t=>t.GSVolume):query.OrderByDescending(t=>t.GSVolume);
                    isordered = true;
                }
				// GSLocation NVARCHAR(50) 位置 
                if(!string.IsNullOrEmpty(searchModel.GSLocation)) query = query.Where(t=>t.GSLocation.Contains(searchModel.GSLocation));
                if(sort=="GSLocation")
                {
					query = order=="asc"?query.OrderBy(t=>t.GSLocation):query.OrderByDescending(t=>t.GSLocation);
                    isordered = true;
                }
				// GSResponsibleForManualNumber NVARCHAR(50) 负责人工号 
                if(!string.IsNullOrEmpty(searchModel.GSResponsibleForManualNumber)) query = query.Where(t=>t.GSResponsibleForManualNumber.Contains(searchModel.GSResponsibleForManualNumber));
                if(sort=="GSResponsibleForManualNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.GSResponsibleForManualNumber):query.OrderByDescending(t=>t.GSResponsibleForManualNumber);
                    isordered = true;
                }
				if(!string.IsNullOrEmpty(search)){
					query = query.Where(t=>t.id!=-1||t.GSShelfNumber.Contains(search)||t.GSVolume.Contains(search)||t.GSLocation.Contains(search)||t.GSResponsibleForManualNumber.Contains(search));
				}
                if(sort=="ord")
                {
					query = order=="asc"?query.OrderBy(t=>t.ord):query.OrderByDescending(t=>t.ord);
                    isordered = true;
                }

                if(!isordered) query = query.OrderByDescending(t=>t.UpdateOn);
                var rows = query.Skip((searchModel.PageIndex)*searchModel.PageSize).Take(searchModel.PageSize).ToList();
                var total = query.Count();
                var sql = query.ToString();
                return new CommonOutputList<GoodsShelves>
                {
                    success = true, rows = rows, total = total, message="查询成功"
                };
            }
        }
    }


    /// <summary>
    /// 【补货申请单】7次查询分别得到七天内记录的条数，形成柱状图，折线图
    /// </summary>
    public partial class ReplenishmentApplicationFormCountEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
                var now = Parse(request.context.Request.Params["date"] ?? Now.ToString("yyyy-MM-dd"));
                var dates = Range(1, 7).Select(p => now.AddDays(-1 * p)).ToList();
                var list = dates.Select(p => ctx.ReplenishmentApplicationForm.Count(m => m.CreateOn==p)).ToList();
                return new
                {
                    sum = list.Sum(),
                    xaxis = dates.Select(p => p.ToString("yyyy-MM-dd")).ToList(),
                    series = list
                };
            }
        }
        public override string Comments=> "【补货申请单】7次查询分别得到七天内记录的条数，形成柱状图，折线图";
    }
	public partial class TruncateReplenishmentApplicationFormEvaluator : Evaluator
	{
        protected override object Evaluate(CommonRequest request)
		{
            using (var ctx = new DefaultContext())
			{
                ctx.ReplenishmentApplicationForm.RemoveRange(ctx.ReplenishmentApplicationForm);
				ctx.SaveChanges();
			}
			return new
			{
				success = true,
				message = "操作成功"
			};
		}
	}
    /// <summary>
    /// 删除【补货申请单】
    /// </summary>
    public partial class DeleteReplenishmentApplicationFormEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            var data = JsonConvert.DeserializeObject<ReplenishmentApplicationForm>(HttpUtility.UrlDecode(request.data));
            if (data == null)
                return new CommonOutputT<string>
                {
                    success = false,
                    message = "删除成功"
                };
            using (var ctx = new DefaultContext())
            {
                var one = ctx.ReplenishmentApplicationForm.Find(data.id);
				if(one==null){
					return new CommonOutputT<string>
					{
						success = false,
						message = "未找到需要删除的数据"
					};
				}
                one.IsDeleted = 1;
				ctx.ReplenishmentApplicationForm.AddOrUpdate(one);
				ctx.SaveChanges();
				return new CommonOutputT<string>
				{
					success = true,
					message = "删除成功"
				};
            }
			
        }
        public override string Comments=> "删除一条补货申请单记录";
    }
	
    /// <summary>
    /// 保存【补货申请单】
    /// </summary>
    public partial class SaveReplenishmentApplicationFormEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            var user = CurrentUserInformation;
            if (user==null)
            {
                return new
                {
                    success = false,
                    message = "请登录"
                };
            }
			var s = request.data;
            if (s==null)
            {
                return new
                {
                    success = false,
                    message = "缺少参数"
                };
            }
			ReplenishmentApplicationForm entity = null;
			try
			{
				entity = JsonConvert.DeserializeObject<ReplenishmentApplicationForm>(HttpUtility.UrlDecode(s));
			}
			catch(Exception exception)
			{
				return new
				{
					success = false,
					message = $"填写内容格式错误：{exception.Message}",
					input = s
				};
			}
            if (entity==null)
            {
                return new
                {
                    success = false,
                    message = "参数格式不正确"
                };
            }

			try
			{
				foreach (ValidationResult result in Validation.Validate(entity))
				{
					return new
					{
						success = false,
						message = result.Message
					};
				}
			}
			catch(Exception exception)
			{
				return new
				{
					success = false,
					message = exception.Message
				};
			}
			
            using (var ctx = new DefaultContext())
            {
				// 行级排他锁开始
                var transactionId = Guid.NewGuid().ToString();
                var isnew = entity.TransactionID == null;
                if (!isnew)
                {
                    var one = ctx.ReplenishmentApplicationForm.FirstOrDefault(p=>p.id==entity.id);
                    if(one==null) return new
                    {
                        success = false,
                        message = "编辑错误，未找到ID"
                    };
                    if (one.VersionNo != entity.VersionNo) return new
                    {
                        success = false,
                        message = "发生数据写冲突"
                    };
                    one.VersionNo++;
                    one.TransactionID = transactionId;
					ctx.ReplenishmentApplicationForm.AddOrUpdate(one);
					try
					{
						ctx.SaveChanges();
					}
					catch(Exception exception)
					{
						// 遇到数据库中的脏数据，走到这里，前面的Entity数据合法，直接跳过这里的数据校验阶段，使用最先到达的正确数据。
						// return new
						// {
						// 	 success = false,
						// 	 message = exception.Message,
						// 	 exception, one, transactionId, entity
						// };
					}
                    entity.VersionNo = one.VersionNo;
                }
				

								// NVARCHAR(50) 申请单编号
				entity.RAFApplicationNumber = HttpUtility.UrlDecode(entity.RAFApplicationNumber);
					// NVARCHAR(50) 货架编号
				entity.RAFShelfNumber = HttpUtility.UrlDecode(entity.RAFShelfNumber);
					// NVARCHAR(50) 仓库编号
				entity.RAFWarehouseNumber = HttpUtility.UrlDecode(entity.RAFWarehouseNumber);
					// NVARCHAR(50) 申请人工号
				entity.RAFApplicationManualNumber = HttpUtility.UrlDecode(entity.RAFApplicationManualNumber);
					// NVARCHAR(50) 货品种类编号
				entity.RAFCategoryNumberOfGoods = HttpUtility.UrlDecode(entity.RAFCategoryNumberOfGoods);
					// NVARCHAR(50) 货品数量
				entity.RAFQuantityOfGoods = HttpUtility.UrlDecode(entity.RAFQuantityOfGoods);
					// NVARCHAR(50) 申请单状态
				entity.RAFApplicationStatus = HttpUtility.UrlDecode(entity.RAFApplicationStatus);
					// NVARCHAR(50) 备注
				entity.RAFRemarks = HttpUtility.UrlDecode(entity.RAFRemarks);
	
                entity.CreateBy = entity.CreateBy ?? user?.UILoginName ?? "未登录用户";
                entity.UpdateBy = user?.UILoginName ?? "未登录用户";
                entity.CreateOn = entity.CreateOn ?? Now;
                entity.TransactionID = transactionId;
                entity.UpdateOn = Now;
                entity.IsDeleted = 0;
	            entity.VersionNo = entity.VersionNo ?? 0;
                entity.DataLevel = entity.DataLevel ?? user?.DataLevel ?? "019999";
				ctx.ReplenishmentApplicationForm.AddOrUpdate(entity);
				try
				{
					ctx.SaveChanges();
				}
				catch(Exception exception)
				{
					return new
					{
						success = false,
						message = exception.Message,
						exception, transactionId, entity
					};
				}
				// 行级排他锁结束
                return new
                {
                    success = true,
                    message = "操作成功"
                };
            }
        }
        public override string Comments=> "保存一条ReplenishmentApplicationForm记录";
    }
	
    /// <summary>
    /// 查询空的【补货申请单】
    /// </summary>
    public partial class GetReplenishmentApplicationFormEmptyEvaluator:Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            return new ReplenishmentApplicationForm();
        }
        public override string Comments=> "获取空的补货申请单记录";
    }
	
    /// <summary>
    /// 查询【补货申请单】列表
    /// </summary>
    public partial class GetReplenishmentApplicationFormListEvaluator : Evaluator
    {
        public override string Comments=> "获取ReplenishmentApplicationForm列表 ";
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
				var datalevel = CurrentUserInformation?.DataLevel;
                var searchModel = HttpUtility.UrlDecode(request.data).Deserialize<ReplenishmentApplicationFormSearchModel>() ?? new ReplenishmentApplicationFormSearchModel();
                var query = ctx.ReplenishmentApplicationForm.Where(t=>t.IsDeleted==0 && t.DataLevel.StartsWith(datalevel));
                var @params = request.context.Request.Params;
                searchModel.PageSize = (@params["limit"] ?? searchModel.PageSize.ToString()).ToInt();
				searchModel.PageSize = searchModel.PageSize==0?4000:searchModel.PageSize;
                searchModel.PageIndex = (@params["offset"]).ToInt()/ searchModel.PageSize;
                var isordered = false;
                var search = searchModel.SearchKey ?? @params["search"];
                var sort = searchModel.Sort ?? @params["sort"];
				var order = @params["order"];
				// RAFApplicationNumber NVARCHAR(50) 申请单编号 
                if(!string.IsNullOrEmpty(searchModel.RAFApplicationNumber)) query = query.Where(t=>t.RAFApplicationNumber.Contains(searchModel.RAFApplicationNumber));
                if(sort=="RAFApplicationNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.RAFApplicationNumber):query.OrderByDescending(t=>t.RAFApplicationNumber);
                    isordered = true;
                }
				// RAFShelfNumber NVARCHAR(50) 货架编号 
                if(!string.IsNullOrEmpty(searchModel.RAFShelfNumber)) query = query.Where(t=>t.RAFShelfNumber.Contains(searchModel.RAFShelfNumber));
                if(sort=="RAFShelfNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.RAFShelfNumber):query.OrderByDescending(t=>t.RAFShelfNumber);
                    isordered = true;
                }
				// RAFWarehouseNumber NVARCHAR(50) 仓库编号 
                if(!string.IsNullOrEmpty(searchModel.RAFWarehouseNumber)) query = query.Where(t=>t.RAFWarehouseNumber.Contains(searchModel.RAFWarehouseNumber));
                if(sort=="RAFWarehouseNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.RAFWarehouseNumber):query.OrderByDescending(t=>t.RAFWarehouseNumber);
                    isordered = true;
                }
				// RAFApplicationManualNumber NVARCHAR(50) 申请人工号 
                if(!string.IsNullOrEmpty(searchModel.RAFApplicationManualNumber)) query = query.Where(t=>t.RAFApplicationManualNumber.Contains(searchModel.RAFApplicationManualNumber));
                if(sort=="RAFApplicationManualNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.RAFApplicationManualNumber):query.OrderByDescending(t=>t.RAFApplicationManualNumber);
                    isordered = true;
                }
				// RAFCategoryNumberOfGoods NVARCHAR(50) 货品种类编号 
                if(!string.IsNullOrEmpty(searchModel.RAFCategoryNumberOfGoods)) query = query.Where(t=>t.RAFCategoryNumberOfGoods.Contains(searchModel.RAFCategoryNumberOfGoods));
                if(sort=="RAFCategoryNumberOfGoods")
                {
					query = order=="asc"?query.OrderBy(t=>t.RAFCategoryNumberOfGoods):query.OrderByDescending(t=>t.RAFCategoryNumberOfGoods);
                    isordered = true;
                }
				// RAFQuantityOfGoods NVARCHAR(50) 货品数量 
                if(!string.IsNullOrEmpty(searchModel.RAFQuantityOfGoods)) query = query.Where(t=>t.RAFQuantityOfGoods.Contains(searchModel.RAFQuantityOfGoods));
                if(sort=="RAFQuantityOfGoods")
                {
					query = order=="asc"?query.OrderBy(t=>t.RAFQuantityOfGoods):query.OrderByDescending(t=>t.RAFQuantityOfGoods);
                    isordered = true;
                }
				// RAFApplicationDate DATETIME 申请日期 
                if(searchModel.FromRAFApplicationDate!=null) query = query.Where(t=>t.RAFApplicationDate>=searchModel.FromRAFApplicationDate);
                if(searchModel.ToRAFApplicationDate!=null) query = query.Where(t=>t.RAFApplicationDate<=searchModel.ToRAFApplicationDate);
                if(sort=="RAFApplicationDate")
                {
					query = order=="asc"?query.OrderBy(t=>t.RAFApplicationDate):query.OrderByDescending(t=>t.RAFApplicationDate);
                    isordered = true;
                }
				// RAFApplicationStatus NVARCHAR(50) 申请单状态 
                if(!string.IsNullOrEmpty(searchModel.RAFApplicationStatus)) query = query.Where(t=>t.RAFApplicationStatus.Contains(searchModel.RAFApplicationStatus));
                if(sort=="RAFApplicationStatus")
                {
					query = order=="asc"?query.OrderBy(t=>t.RAFApplicationStatus):query.OrderByDescending(t=>t.RAFApplicationStatus);
                    isordered = true;
                }
				// RAFRemarks NVARCHAR(50) 备注 
                if(!string.IsNullOrEmpty(searchModel.RAFRemarks)) query = query.Where(t=>t.RAFRemarks.Contains(searchModel.RAFRemarks));
                if(sort=="RAFRemarks")
                {
					query = order=="asc"?query.OrderBy(t=>t.RAFRemarks):query.OrderByDescending(t=>t.RAFRemarks);
                    isordered = true;
                }
				if(!string.IsNullOrEmpty(search)){
					query = query.Where(t=>t.id!=-1||t.RAFApplicationNumber.Contains(search)||t.RAFShelfNumber.Contains(search)||t.RAFWarehouseNumber.Contains(search)||t.RAFApplicationManualNumber.Contains(search)||t.RAFCategoryNumberOfGoods.Contains(search)||t.RAFQuantityOfGoods.Contains(search)||t.RAFApplicationStatus.Contains(search)||t.RAFRemarks.Contains(search));
				}
                if(sort=="ord")
                {
					query = order=="asc"?query.OrderBy(t=>t.ord):query.OrderByDescending(t=>t.ord);
                    isordered = true;
                }

                if(!isordered) query = query.OrderByDescending(t=>t.UpdateOn);
                var rows = query.Skip((searchModel.PageIndex)*searchModel.PageSize).Take(searchModel.PageSize).ToList();
                var total = query.Count();
                var sql = query.ToString();
                return new CommonOutputList<ReplenishmentApplicationForm>
                {
                    success = true, rows = rows, total = total, message="查询成功"
                };
            }
        }
    }


    /// <summary>
    /// 【补货记录】7次查询分别得到七天内记录的条数，形成柱状图，折线图
    /// </summary>
    public partial class ReplenishmentRecordCountEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
                var now = Parse(request.context.Request.Params["date"] ?? Now.ToString("yyyy-MM-dd"));
                var dates = Range(1, 7).Select(p => now.AddDays(-1 * p)).ToList();
                var list = dates.Select(p => ctx.ReplenishmentRecord.Count(m => m.CreateOn==p)).ToList();
                return new
                {
                    sum = list.Sum(),
                    xaxis = dates.Select(p => p.ToString("yyyy-MM-dd")).ToList(),
                    series = list
                };
            }
        }
        public override string Comments=> "【补货记录】7次查询分别得到七天内记录的条数，形成柱状图，折线图";
    }
	public partial class TruncateReplenishmentRecordEvaluator : Evaluator
	{
        protected override object Evaluate(CommonRequest request)
		{
            using (var ctx = new DefaultContext())
			{
                ctx.ReplenishmentRecord.RemoveRange(ctx.ReplenishmentRecord);
				ctx.SaveChanges();
			}
			return new
			{
				success = true,
				message = "操作成功"
			};
		}
	}
    /// <summary>
    /// 删除【补货记录】
    /// </summary>
    public partial class DeleteReplenishmentRecordEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            var data = JsonConvert.DeserializeObject<ReplenishmentRecord>(HttpUtility.UrlDecode(request.data));
            if (data == null)
                return new CommonOutputT<string>
                {
                    success = false,
                    message = "删除成功"
                };
            using (var ctx = new DefaultContext())
            {
                var one = ctx.ReplenishmentRecord.Find(data.id);
				if(one==null){
					return new CommonOutputT<string>
					{
						success = false,
						message = "未找到需要删除的数据"
					};
				}
                one.IsDeleted = 1;
				ctx.ReplenishmentRecord.AddOrUpdate(one);
				ctx.SaveChanges();
				return new CommonOutputT<string>
				{
					success = true,
					message = "删除成功"
				};
            }
			
        }
        public override string Comments=> "删除一条补货记录记录";
    }
	
    /// <summary>
    /// 保存【补货记录】
    /// </summary>
    public partial class SaveReplenishmentRecordEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            var user = CurrentUserInformation;
            if (user==null)
            {
                return new
                {
                    success = false,
                    message = "请登录"
                };
            }
			var s = request.data;
            if (s==null)
            {
                return new
                {
                    success = false,
                    message = "缺少参数"
                };
            }
			ReplenishmentRecord entity = null;
			try
			{
				entity = JsonConvert.DeserializeObject<ReplenishmentRecord>(HttpUtility.UrlDecode(s));
			}
			catch(Exception exception)
			{
				return new
				{
					success = false,
					message = $"填写内容格式错误：{exception.Message}",
					input = s
				};
			}
            if (entity==null)
            {
                return new
                {
                    success = false,
                    message = "参数格式不正确"
                };
            }

			try
			{
				foreach (ValidationResult result in Validation.Validate(entity))
				{
					return new
					{
						success = false,
						message = result.Message
					};
				}
			}
			catch(Exception exception)
			{
				return new
				{
					success = false,
					message = exception.Message
				};
			}
			
            using (var ctx = new DefaultContext())
            {
				// 行级排他锁开始
                var transactionId = Guid.NewGuid().ToString();
                var isnew = entity.TransactionID == null;
                if (!isnew)
                {
                    var one = ctx.ReplenishmentRecord.FirstOrDefault(p=>p.id==entity.id);
                    if(one==null) return new
                    {
                        success = false,
                        message = "编辑错误，未找到ID"
                    };
                    if (one.VersionNo != entity.VersionNo) return new
                    {
                        success = false,
                        message = "发生数据写冲突"
                    };
                    one.VersionNo++;
                    one.TransactionID = transactionId;
					ctx.ReplenishmentRecord.AddOrUpdate(one);
					try
					{
						ctx.SaveChanges();
					}
					catch(Exception exception)
					{
						// 遇到数据库中的脏数据，走到这里，前面的Entity数据合法，直接跳过这里的数据校验阶段，使用最先到达的正确数据。
						// return new
						// {
						// 	 success = false,
						// 	 message = exception.Message,
						// 	 exception, one, transactionId, entity
						// };
					}
                    entity.VersionNo = one.VersionNo;
                }
				

								// NVARCHAR(50) 申请单编号
				entity.RRApplicationNumber = HttpUtility.UrlDecode(entity.RRApplicationNumber);
					// NVARCHAR(50) 备注
				entity.RRRemarks = HttpUtility.UrlDecode(entity.RRRemarks);
	
                entity.CreateBy = entity.CreateBy ?? user?.UILoginName ?? "未登录用户";
                entity.UpdateBy = user?.UILoginName ?? "未登录用户";
                entity.CreateOn = entity.CreateOn ?? Now;
                entity.TransactionID = transactionId;
                entity.UpdateOn = Now;
                entity.IsDeleted = 0;
	            entity.VersionNo = entity.VersionNo ?? 0;
                entity.DataLevel = entity.DataLevel ?? user?.DataLevel ?? "019999";
				ctx.ReplenishmentRecord.AddOrUpdate(entity);
				try
				{
					ctx.SaveChanges();
				}
				catch(Exception exception)
				{
					return new
					{
						success = false,
						message = exception.Message,
						exception, transactionId, entity
					};
				}
				// 行级排他锁结束
                return new
                {
                    success = true,
                    message = "操作成功"
                };
            }
        }
        public override string Comments=> "保存一条ReplenishmentRecord记录";
    }
	
    /// <summary>
    /// 查询空的【补货记录】
    /// </summary>
    public partial class GetReplenishmentRecordEmptyEvaluator:Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            return new ReplenishmentRecord();
        }
        public override string Comments=> "获取空的补货记录记录";
    }
	
    /// <summary>
    /// 查询【补货记录】列表
    /// </summary>
    public partial class GetReplenishmentRecordListEvaluator : Evaluator
    {
        public override string Comments=> "获取ReplenishmentRecord列表 ";
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
				var datalevel = CurrentUserInformation?.DataLevel;
                var searchModel = HttpUtility.UrlDecode(request.data).Deserialize<ReplenishmentRecordSearchModel>() ?? new ReplenishmentRecordSearchModel();
                var query = ctx.ReplenishmentRecord.Where(t=>t.IsDeleted==0 && t.DataLevel.StartsWith(datalevel));
                var @params = request.context.Request.Params;
                searchModel.PageSize = (@params["limit"] ?? searchModel.PageSize.ToString()).ToInt();
				searchModel.PageSize = searchModel.PageSize==0?4000:searchModel.PageSize;
                searchModel.PageIndex = (@params["offset"]).ToInt()/ searchModel.PageSize;
                var isordered = false;
                var search = searchModel.SearchKey ?? @params["search"];
                var sort = searchModel.Sort ?? @params["sort"];
				var order = @params["order"];
				// RRApplicationNumber NVARCHAR(50) 申请单编号 
                if(!string.IsNullOrEmpty(searchModel.RRApplicationNumber)) query = query.Where(t=>t.RRApplicationNumber.Contains(searchModel.RRApplicationNumber));
                if(sort=="RRApplicationNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.RRApplicationNumber):query.OrderByDescending(t=>t.RRApplicationNumber);
                    isordered = true;
                }
				// RRDateOfArrival DATETIME 到货日期 
                if(searchModel.FromRRDateOfArrival!=null) query = query.Where(t=>t.RRDateOfArrival>=searchModel.FromRRDateOfArrival);
                if(searchModel.ToRRDateOfArrival!=null) query = query.Where(t=>t.RRDateOfArrival<=searchModel.ToRRDateOfArrival);
                if(sort=="RRDateOfArrival")
                {
					query = order=="asc"?query.OrderBy(t=>t.RRDateOfArrival):query.OrderByDescending(t=>t.RRDateOfArrival);
                    isordered = true;
                }
				// RRRemarks NVARCHAR(50) 备注 
                if(!string.IsNullOrEmpty(searchModel.RRRemarks)) query = query.Where(t=>t.RRRemarks.Contains(searchModel.RRRemarks));
                if(sort=="RRRemarks")
                {
					query = order=="asc"?query.OrderBy(t=>t.RRRemarks):query.OrderByDescending(t=>t.RRRemarks);
                    isordered = true;
                }
				if(!string.IsNullOrEmpty(search)){
					query = query.Where(t=>t.id!=-1||t.RRApplicationNumber.Contains(search)||t.RRRemarks.Contains(search));
				}
                if(sort=="ord")
                {
					query = order=="asc"?query.OrderBy(t=>t.ord):query.OrderByDescending(t=>t.ord);
                    isordered = true;
                }

                if(!isordered) query = query.OrderByDescending(t=>t.UpdateOn);
                var rows = query.Skip((searchModel.PageIndex)*searchModel.PageSize).Take(searchModel.PageSize).ToList();
                var total = query.Count();
                var sql = query.ToString();
                return new CommonOutputList<ReplenishmentRecord>
                {
                    success = true, rows = rows, total = total, message="查询成功"
                };
            }
        }
    }


    /// <summary>
    /// 【销售记录】7次查询分别得到七天内记录的条数，形成柱状图，折线图
    /// </summary>
    public partial class SalesRecordCountEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
                var now = Parse(request.context.Request.Params["date"] ?? Now.ToString("yyyy-MM-dd"));
                var dates = Range(1, 7).Select(p => now.AddDays(-1 * p)).ToList();
                var list = dates.Select(p => ctx.SalesRecord.Count(m => m.CreateOn==p)).ToList();
                return new
                {
                    sum = list.Sum(),
                    xaxis = dates.Select(p => p.ToString("yyyy-MM-dd")).ToList(),
                    series = list
                };
            }
        }
        public override string Comments=> "【销售记录】7次查询分别得到七天内记录的条数，形成柱状图，折线图";
    }
	public partial class TruncateSalesRecordEvaluator : Evaluator
	{
        protected override object Evaluate(CommonRequest request)
		{
            using (var ctx = new DefaultContext())
			{
                ctx.SalesRecord.RemoveRange(ctx.SalesRecord);
				ctx.SaveChanges();
			}
			return new
			{
				success = true,
				message = "操作成功"
			};
		}
	}
    /// <summary>
    /// 删除【销售记录】
    /// </summary>
    public partial class DeleteSalesRecordEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            var data = JsonConvert.DeserializeObject<SalesRecord>(HttpUtility.UrlDecode(request.data));
            if (data == null)
                return new CommonOutputT<string>
                {
                    success = false,
                    message = "删除成功"
                };
            using (var ctx = new DefaultContext())
            {
                var one = ctx.SalesRecord.Find(data.id);
				if(one==null){
					return new CommonOutputT<string>
					{
						success = false,
						message = "未找到需要删除的数据"
					};
				}
                one.IsDeleted = 1;
				ctx.SalesRecord.AddOrUpdate(one);
				ctx.SaveChanges();
				return new CommonOutputT<string>
				{
					success = true,
					message = "删除成功"
				};
            }
			
        }
        public override string Comments=> "删除一条销售记录记录";
    }
	
    /// <summary>
    /// 保存【销售记录】
    /// </summary>
    public partial class SaveSalesRecordEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            var user = CurrentUserInformation;
            if (user==null)
            {
                return new
                {
                    success = false,
                    message = "请登录"
                };
            }
			var s = request.data;
            if (s==null)
            {
                return new
                {
                    success = false,
                    message = "缺少参数"
                };
            }
			SalesRecord entity = null;
			try
			{
				entity = JsonConvert.DeserializeObject<SalesRecord>(HttpUtility.UrlDecode(s));
			}
			catch(Exception exception)
			{
				return new
				{
					success = false,
					message = $"填写内容格式错误：{exception.Message}",
					input = s
				};
			}
            if (entity==null)
            {
                return new
                {
                    success = false,
                    message = "参数格式不正确"
                };
            }

			try
			{
				foreach (ValidationResult result in Validation.Validate(entity))
				{
					return new
					{
						success = false,
						message = result.Message
					};
				}
			}
			catch(Exception exception)
			{
				return new
				{
					success = false,
					message = exception.Message
				};
			}
			
            using (var ctx = new DefaultContext())
            {
				// 行级排他锁开始
                var transactionId = Guid.NewGuid().ToString();
                var isnew = entity.TransactionID == null;
                if (!isnew)
                {
                    var one = ctx.SalesRecord.FirstOrDefault(p=>p.id==entity.id);
                    if(one==null) return new
                    {
                        success = false,
                        message = "编辑错误，未找到ID"
                    };
                    if (one.VersionNo != entity.VersionNo) return new
                    {
                        success = false,
                        message = "发生数据写冲突"
                    };
                    one.VersionNo++;
                    one.TransactionID = transactionId;
					ctx.SalesRecord.AddOrUpdate(one);
					try
					{
						ctx.SaveChanges();
					}
					catch(Exception exception)
					{
						// 遇到数据库中的脏数据，走到这里，前面的Entity数据合法，直接跳过这里的数据校验阶段，使用最先到达的正确数据。
						// return new
						// {
						// 	 success = false,
						// 	 message = exception.Message,
						// 	 exception, one, transactionId, entity
						// };
					}
                    entity.VersionNo = one.VersionNo;
                }
				

								// NVARCHAR(50) 销售批次号
				entity.SRSalesLotNumber = HttpUtility.UrlDecode(entity.SRSalesLotNumber);
					// NVARCHAR(50) 货物种类
				entity.SRTypeOfGoods = HttpUtility.UrlDecode(entity.SRTypeOfGoods);
					// NVARCHAR(50) 数量
				entity.SRAmount = HttpUtility.UrlDecode(entity.SRAmount);
					// NVARCHAR(50) 单价
				entity.SRUnitPrice = HttpUtility.UrlDecode(entity.SRUnitPrice);
					// NVARCHAR(50) 销售工号
				entity.SRSalesNumber = HttpUtility.UrlDecode(entity.SRSalesNumber);
					// NVARCHAR(50) 发票编号
				entity.SRInvoiceNumber = HttpUtility.UrlDecode(entity.SRInvoiceNumber);
					// NVARCHAR(50) 发票抬头
				entity.SRInvoicesAreRaised = HttpUtility.UrlDecode(entity.SRInvoicesAreRaised);
					// NVARCHAR(50) 税号
				entity.SRDutyParagraph = HttpUtility.UrlDecode(entity.SRDutyParagraph);
					// NVARCHAR(50) 备注
				entity.SRRemarks = HttpUtility.UrlDecode(entity.SRRemarks);
	
                entity.CreateBy = entity.CreateBy ?? user?.UILoginName ?? "未登录用户";
                entity.UpdateBy = user?.UILoginName ?? "未登录用户";
                entity.CreateOn = entity.CreateOn ?? Now;
                entity.TransactionID = transactionId;
                entity.UpdateOn = Now;
                entity.IsDeleted = 0;
	            entity.VersionNo = entity.VersionNo ?? 0;
                entity.DataLevel = entity.DataLevel ?? user?.DataLevel ?? "019999";
				ctx.SalesRecord.AddOrUpdate(entity);
				try
				{
					ctx.SaveChanges();
				}
				catch(Exception exception)
				{
					return new
					{
						success = false,
						message = exception.Message,
						exception, transactionId, entity
					};
				}
				// 行级排他锁结束
                return new
                {
                    success = true,
                    message = "操作成功"
                };
            }
        }
        public override string Comments=> "保存一条SalesRecord记录";
    }
	
    /// <summary>
    /// 查询空的【销售记录】
    /// </summary>
    public partial class GetSalesRecordEmptyEvaluator:Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            return new SalesRecord();
        }
        public override string Comments=> "获取空的销售记录记录";
    }
	
    /// <summary>
    /// 查询【销售记录】列表
    /// </summary>
    public partial class GetSalesRecordListEvaluator : Evaluator
    {
        public override string Comments=> "获取SalesRecord列表 ";
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
				var datalevel = CurrentUserInformation?.DataLevel;
                var searchModel = HttpUtility.UrlDecode(request.data).Deserialize<SalesRecordSearchModel>() ?? new SalesRecordSearchModel();
                var query = ctx.SalesRecord.Where(t=>t.IsDeleted==0 && t.DataLevel.StartsWith(datalevel));
                var @params = request.context.Request.Params;
                searchModel.PageSize = (@params["limit"] ?? searchModel.PageSize.ToString()).ToInt();
				searchModel.PageSize = searchModel.PageSize==0?4000:searchModel.PageSize;
                searchModel.PageIndex = (@params["offset"]).ToInt()/ searchModel.PageSize;
                var isordered = false;
                var search = searchModel.SearchKey ?? @params["search"];
                var sort = searchModel.Sort ?? @params["sort"];
				var order = @params["order"];
				// SRSalesLotNumber NVARCHAR(50) 销售批次号 
                if(!string.IsNullOrEmpty(searchModel.SRSalesLotNumber)) query = query.Where(t=>t.SRSalesLotNumber.Contains(searchModel.SRSalesLotNumber));
                if(sort=="SRSalesLotNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.SRSalesLotNumber):query.OrderByDescending(t=>t.SRSalesLotNumber);
                    isordered = true;
                }
				// SRTypeOfGoods NVARCHAR(50) 货物种类 
                if(!string.IsNullOrEmpty(searchModel.SRTypeOfGoods)) query = query.Where(t=>t.SRTypeOfGoods.Contains(searchModel.SRTypeOfGoods));
                if(sort=="SRTypeOfGoods")
                {
					query = order=="asc"?query.OrderBy(t=>t.SRTypeOfGoods):query.OrderByDescending(t=>t.SRTypeOfGoods);
                    isordered = true;
                }
				// SRAmount NVARCHAR(50) 数量 
                if(!string.IsNullOrEmpty(searchModel.SRAmount)) query = query.Where(t=>t.SRAmount.Contains(searchModel.SRAmount));
                if(sort=="SRAmount")
                {
					query = order=="asc"?query.OrderBy(t=>t.SRAmount):query.OrderByDescending(t=>t.SRAmount);
                    isordered = true;
                }
				// SRUnitPrice NVARCHAR(50) 单价 
                if(!string.IsNullOrEmpty(searchModel.SRUnitPrice)) query = query.Where(t=>t.SRUnitPrice.Contains(searchModel.SRUnitPrice));
                if(sort=="SRUnitPrice")
                {
					query = order=="asc"?query.OrderBy(t=>t.SRUnitPrice):query.OrderByDescending(t=>t.SRUnitPrice);
                    isordered = true;
                }
				// SRSalesNumber NVARCHAR(50) 销售工号 
                if(!string.IsNullOrEmpty(searchModel.SRSalesNumber)) query = query.Where(t=>t.SRSalesNumber.Contains(searchModel.SRSalesNumber));
                if(sort=="SRSalesNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.SRSalesNumber):query.OrderByDescending(t=>t.SRSalesNumber);
                    isordered = true;
                }
				// SRInvoiceNumber NVARCHAR(50) 发票编号 
                if(!string.IsNullOrEmpty(searchModel.SRInvoiceNumber)) query = query.Where(t=>t.SRInvoiceNumber.Contains(searchModel.SRInvoiceNumber));
                if(sort=="SRInvoiceNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.SRInvoiceNumber):query.OrderByDescending(t=>t.SRInvoiceNumber);
                    isordered = true;
                }
				// SRInvoicesAreRaised NVARCHAR(50) 发票抬头 
                if(!string.IsNullOrEmpty(searchModel.SRInvoicesAreRaised)) query = query.Where(t=>t.SRInvoicesAreRaised.Contains(searchModel.SRInvoicesAreRaised));
                if(sort=="SRInvoicesAreRaised")
                {
					query = order=="asc"?query.OrderBy(t=>t.SRInvoicesAreRaised):query.OrderByDescending(t=>t.SRInvoicesAreRaised);
                    isordered = true;
                }
				// SRDutyParagraph NVARCHAR(50) 税号 
                if(!string.IsNullOrEmpty(searchModel.SRDutyParagraph)) query = query.Where(t=>t.SRDutyParagraph.Contains(searchModel.SRDutyParagraph));
                if(sort=="SRDutyParagraph")
                {
					query = order=="asc"?query.OrderBy(t=>t.SRDutyParagraph):query.OrderByDescending(t=>t.SRDutyParagraph);
                    isordered = true;
                }
				// SRRemarks NVARCHAR(50) 备注 
                if(!string.IsNullOrEmpty(searchModel.SRRemarks)) query = query.Where(t=>t.SRRemarks.Contains(searchModel.SRRemarks));
                if(sort=="SRRemarks")
                {
					query = order=="asc"?query.OrderBy(t=>t.SRRemarks):query.OrderByDescending(t=>t.SRRemarks);
                    isordered = true;
                }
				if(!string.IsNullOrEmpty(search)){
					query = query.Where(t=>t.id!=-1||t.SRSalesLotNumber.Contains(search)||t.SRTypeOfGoods.Contains(search)||t.SRAmount.Contains(search)||t.SRUnitPrice.Contains(search)||t.SRSalesNumber.Contains(search)||t.SRInvoiceNumber.Contains(search)||t.SRInvoicesAreRaised.Contains(search)||t.SRDutyParagraph.Contains(search)||t.SRRemarks.Contains(search));
				}
                if(sort=="ord")
                {
					query = order=="asc"?query.OrderBy(t=>t.ord):query.OrderByDescending(t=>t.ord);
                    isordered = true;
                }

                if(!isordered) query = query.OrderByDescending(t=>t.UpdateOn);
                var rows = query.Skip((searchModel.PageIndex)*searchModel.PageSize).Take(searchModel.PageSize).ToList();
                var total = query.Count();
                var sql = query.ToString();
                return new CommonOutputList<SalesRecord>
                {
                    success = true, rows = rows, total = total, message="查询成功"
                };
            }
        }
    }

}
