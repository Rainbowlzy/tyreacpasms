
/* ------------------------------------------------------------ *
 * 此文件由生成器引擎根据既有规则生成，所有手工的更改将会被覆盖
 * 生成时间：11/12/2018 18:00:07
 * 生成版本：11/12/2018 18:00:02 
 * 作者：路正遥
 * ------------------------------------------------------------ */

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
using Validation = Microsoft.Practices.EnterpriseLibrary.Validation.Validation;
using ValidationResult = Microsoft.Practices.EnterpriseLibrary.Validation.ValidationResult;
using TENtities.EF;
using TENtities;
using EF.Entities;
using TEntities.EF;
namespace T.Evaluators
{

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
                    message = "参数错误"
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
				

								// NVARCHAR(50) 供应商名称
				entity.SSupplierName = HttpUtility.UrlDecode(entity.SSupplierName);
					// NVARCHAR(50) 联系方式
				entity.SCommonModeOfContact = HttpUtility.UrlDecode(entity.SCommonModeOfContact);
					// NVARCHAR(50) 办公地点
				entity.SOfficeLocation = HttpUtility.UrlDecode(entity.SOfficeLocation);
	
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
                var query = ctx.Supplier.Where(t=>t.IsDeleted==0);
                var @params = request.context.Request.Params;
                searchModel.PageSize = (@params["limit"] ?? searchModel.PageSize.ToString()).ToInt();
				searchModel.PageSize = searchModel.PageSize==0?4000:searchModel.PageSize;
                searchModel.PageIndex = (@params["offset"]).ToInt()/ searchModel.PageSize;
                var isordered = false;
                var search = searchModel.SearchKey ?? @params["search"];
                var sort = searchModel.Sort ?? @params["sort"];
				var order = @params["order"];
				// SSupplierNumber INT 供应商编号 
                if(searchModel.MinSSupplierNumber!=null) query = query.Where(t=>t.SSupplierNumber>=searchModel.MinSSupplierNumber);
                if(searchModel.MaxSSupplierNumber!=null) query = query.Where(t=>t.SSupplierNumber<=searchModel.MaxSSupplierNumber);
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
				if(!string.IsNullOrEmpty(search)){
					query = query.Where(t=>t.id!=-1||t.SSupplierName.Contains(search)||t.SCommonModeOfContact.Contains(search)||t.SOfficeLocation.Contains(search));
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
                    message = "参数错误"
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
				

								// NVARCHAR(50) 容量
				entity.WCapacity = HttpUtility.UrlDecode(entity.WCapacity);
					// NVARCHAR(50) 地点
				entity.WLocality = HttpUtility.UrlDecode(entity.WLocality);
	
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
                var query = ctx.Warehouse.Where(t=>t.IsDeleted==0);
                var @params = request.context.Request.Params;
                searchModel.PageSize = (@params["limit"] ?? searchModel.PageSize.ToString()).ToInt();
				searchModel.PageSize = searchModel.PageSize==0?4000:searchModel.PageSize;
                searchModel.PageIndex = (@params["offset"]).ToInt()/ searchModel.PageSize;
                var isordered = false;
                var search = searchModel.SearchKey ?? @params["search"];
                var sort = searchModel.Sort ?? @params["sort"];
				var order = @params["order"];
				// WWarehouseNumber INT 仓库编号 
                if(searchModel.MinWWarehouseNumber!=null) query = query.Where(t=>t.WWarehouseNumber>=searchModel.MinWWarehouseNumber);
                if(searchModel.MaxWWarehouseNumber!=null) query = query.Where(t=>t.WWarehouseNumber<=searchModel.MaxWWarehouseNumber);
                if(sort=="WWarehouseNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.WWarehouseNumber):query.OrderByDescending(t=>t.WWarehouseNumber);
                    isordered = true;
                }
				// WCapacity NVARCHAR(50) 容量 
                if(!string.IsNullOrEmpty(searchModel.WCapacity)) query = query.Where(t=>t.WCapacity.Contains(searchModel.WCapacity));
                if(sort=="WCapacity")
                {
					query = order=="asc"?query.OrderBy(t=>t.WCapacity):query.OrderByDescending(t=>t.WCapacity);
                    isordered = true;
                }
				// WLocality NVARCHAR(50) 地点 
                if(!string.IsNullOrEmpty(searchModel.WLocality)) query = query.Where(t=>t.WLocality.Contains(searchModel.WLocality));
                if(sort=="WLocality")
                {
					query = order=="asc"?query.OrderBy(t=>t.WLocality):query.OrderByDescending(t=>t.WLocality);
                    isordered = true;
                }
				// WResponsibleForManualNumber INT 负责人工号 
                if(searchModel.MinWResponsibleForManualNumber!=null) query = query.Where(t=>t.WResponsibleForManualNumber>=searchModel.MinWResponsibleForManualNumber);
                if(searchModel.MaxWResponsibleForManualNumber!=null) query = query.Where(t=>t.WResponsibleForManualNumber<=searchModel.MaxWResponsibleForManualNumber);
                if(sort=="WResponsibleForManualNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.WResponsibleForManualNumber):query.OrderByDescending(t=>t.WResponsibleForManualNumber);
                    isordered = true;
                }
				if(!string.IsNullOrEmpty(search)){
					query = query.Where(t=>t.id!=-1||t.WCapacity.Contains(search)||t.WLocality.Contains(search));
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
                    message = "参数错误"
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
				

								// NVARCHAR(50) 姓名
				entity.CName = HttpUtility.UrlDecode(entity.CName);
					// NVARCHAR(50) 联系方式
				entity.CCommonModeOfContact = HttpUtility.UrlDecode(entity.CCommonModeOfContact);
	
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
                var query = ctx.Customertype.Where(t=>t.IsDeleted==0);
                var @params = request.context.Request.Params;
                searchModel.PageSize = (@params["limit"] ?? searchModel.PageSize.ToString()).ToInt();
				searchModel.PageSize = searchModel.PageSize==0?4000:searchModel.PageSize;
                searchModel.PageIndex = (@params["offset"]).ToInt()/ searchModel.PageSize;
                var isordered = false;
                var search = searchModel.SearchKey ?? @params["search"];
                var sort = searchModel.Sort ?? @params["sort"];
				var order = @params["order"];
				// CCustomerNumber INT 客户编号 
                if(searchModel.MinCCustomerNumber!=null) query = query.Where(t=>t.CCustomerNumber>=searchModel.MinCCustomerNumber);
                if(searchModel.MaxCCustomerNumber!=null) query = query.Where(t=>t.CCustomerNumber<=searchModel.MaxCCustomerNumber);
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
				// CCommonModeOfContact NVARCHAR(50) 联系方式 
                if(!string.IsNullOrEmpty(searchModel.CCommonModeOfContact)) query = query.Where(t=>t.CCommonModeOfContact.Contains(searchModel.CCommonModeOfContact));
                if(sort=="CCommonModeOfContact")
                {
					query = order=="asc"?query.OrderBy(t=>t.CCommonModeOfContact):query.OrderByDescending(t=>t.CCommonModeOfContact);
                    isordered = true;
                }
				if(!string.IsNullOrEmpty(search)){
					query = query.Where(t=>t.id!=-1||t.CName.Contains(search)||t.CCommonModeOfContact.Contains(search));
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
    /// 【货物】7次查询分别得到七天内记录的条数，形成柱状图，折线图
    /// </summary>
    public partial class CargoCountEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
                var now = Parse(request.context.Request.Params["date"] ?? Now.ToString("yyyy-MM-dd"));
                var dates = Range(1, 7).Select(p => now.AddDays(-1 * p)).ToList();
                var list = dates.Select(p => ctx.Cargo.Count(m => m.CreateOn==p)).ToList();
                return new
                {
                    sum = list.Sum(),
                    xaxis = dates.Select(p => p.ToString("yyyy-MM-dd")).ToList(),
                    series = list
                };
            }
        }
        public override string Comments=> "【货物】7次查询分别得到七天内记录的条数，形成柱状图，折线图";
    }
	public partial class TruncateCargoEvaluator : Evaluator
	{
        protected override object Evaluate(CommonRequest request)
		{
            using (var ctx = new DefaultContext())
			{
                ctx.Cargo.RemoveRange(ctx.Cargo);
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
    /// 删除【货物】
    /// </summary>
    public partial class DeleteCargoEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            var data = JsonConvert.DeserializeObject<Cargo>(HttpUtility.UrlDecode(request.data));
            if (data == null)
                return new CommonOutputT<string>
                {
                    success = false,
                    message = "参数错误"
                };
            using (var ctx = new DefaultContext())
            {
                var one = ctx.Cargo.Find(data.id);
				if(one==null){
					return new CommonOutputT<string>
					{
						success = false,
						message = "未找到需要删除的数据"
					};
				}
                one.IsDeleted = 1;
				ctx.Cargo.AddOrUpdate(one);
				ctx.SaveChanges();
				return new CommonOutputT<string>
				{
					success = true,
					message = "删除成功"
				};
            }
			
        }
        public override string Comments=> "删除一条货物记录";
    }
	
    /// <summary>
    /// 保存【货物】
    /// </summary>
    public partial class SaveCargoEvaluator : Evaluator
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
			Cargo entity = null;
			try
			{
				entity = JsonConvert.DeserializeObject<Cargo>(HttpUtility.UrlDecode(s));
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
                    var one = ctx.Cargo.FirstOrDefault(p=>p.id==entity.id);
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
					ctx.Cargo.AddOrUpdate(one);
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
				

								// NVARCHAR(50) 货物名称
				entity.CNameOfGoods = HttpUtility.UrlDecode(entity.CNameOfGoods);
	
                entity.CreateBy = entity.CreateBy ?? user?.UILoginName ?? "未登录用户";
                entity.UpdateBy = user?.UILoginName ?? "未登录用户";
                entity.CreateOn = entity.CreateOn ?? Now;
                entity.TransactionID = transactionId;
                entity.UpdateOn = Now;
                entity.IsDeleted = 0;
	            entity.VersionNo = entity.VersionNo ?? 0;
                entity.DataLevel = entity.DataLevel ?? user?.DataLevel ?? "019999";
				ctx.Cargo.AddOrUpdate(entity);
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
        public override string Comments=> "保存一条Cargo记录";
    }
	
    /// <summary>
    /// 查询空的【货物】
    /// </summary>
    public partial class GetCargoEmptyEvaluator:Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            return new Cargo();
        }
        public override string Comments=> "获取空的货物记录";
    }
	
    /// <summary>
    /// 查询【货物】列表
    /// </summary>
    public partial class GetCargoListEvaluator : Evaluator
    {
        public override string Comments=> "获取Cargo列表 ";
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
				var datalevel = CurrentUserInformation?.DataLevel;
                var searchModel = HttpUtility.UrlDecode(request.data).Deserialize<CargoSearchModel>() ?? new CargoSearchModel();
                var query = ctx.Cargo.Where(t=>t.IsDeleted==0);
                var @params = request.context.Request.Params;
                searchModel.PageSize = (@params["limit"] ?? searchModel.PageSize.ToString()).ToInt();
				searchModel.PageSize = searchModel.PageSize==0?4000:searchModel.PageSize;
                searchModel.PageIndex = (@params["offset"]).ToInt()/ searchModel.PageSize;
                var isordered = false;
                var search = searchModel.SearchKey ?? @params["search"];
                var sort = searchModel.Sort ?? @params["sort"];
				var order = @params["order"];
				// CCargoNumber INT 货物编号 
                if(searchModel.MinCCargoNumber!=null) query = query.Where(t=>t.CCargoNumber>=searchModel.MinCCargoNumber);
                if(searchModel.MaxCCargoNumber!=null) query = query.Where(t=>t.CCargoNumber<=searchModel.MaxCCargoNumber);
                if(sort=="CCargoNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.CCargoNumber):query.OrderByDescending(t=>t.CCargoNumber);
                    isordered = true;
                }
				// CNameOfGoods NVARCHAR(50) 货物名称 
                if(!string.IsNullOrEmpty(searchModel.CNameOfGoods)) query = query.Where(t=>t.CNameOfGoods.Contains(searchModel.CNameOfGoods));
                if(sort=="CNameOfGoods")
                {
					query = order=="asc"?query.OrderBy(t=>t.CNameOfGoods):query.OrderByDescending(t=>t.CNameOfGoods);
                    isordered = true;
                }
				if(!string.IsNullOrEmpty(search)){
					query = query.Where(t=>t.id!=-1||t.CNameOfGoods.Contains(search));
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
                return new CommonOutputList<Cargo>
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
                    message = "参数错误"
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
				

								// NVARCHAR(50) 容量
				entity.GSCapacity = HttpUtility.UrlDecode(entity.GSCapacity);
					// NVARCHAR(50) 地点
				entity.GSLocality = HttpUtility.UrlDecode(entity.GSLocality);
	
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
                var query = ctx.GoodsShelves.Where(t=>t.IsDeleted==0);
                var @params = request.context.Request.Params;
                searchModel.PageSize = (@params["limit"] ?? searchModel.PageSize.ToString()).ToInt();
				searchModel.PageSize = searchModel.PageSize==0?4000:searchModel.PageSize;
                searchModel.PageIndex = (@params["offset"]).ToInt()/ searchModel.PageSize;
                var isordered = false;
                var search = searchModel.SearchKey ?? @params["search"];
                var sort = searchModel.Sort ?? @params["sort"];
				var order = @params["order"];
				// GSShelfNumber INT 货架编号 
                if(searchModel.MinGSShelfNumber!=null) query = query.Where(t=>t.GSShelfNumber>=searchModel.MinGSShelfNumber);
                if(searchModel.MaxGSShelfNumber!=null) query = query.Where(t=>t.GSShelfNumber<=searchModel.MaxGSShelfNumber);
                if(sort=="GSShelfNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.GSShelfNumber):query.OrderByDescending(t=>t.GSShelfNumber);
                    isordered = true;
                }
				// GSCapacity NVARCHAR(50) 容量 
                if(!string.IsNullOrEmpty(searchModel.GSCapacity)) query = query.Where(t=>t.GSCapacity.Contains(searchModel.GSCapacity));
                if(sort=="GSCapacity")
                {
					query = order=="asc"?query.OrderBy(t=>t.GSCapacity):query.OrderByDescending(t=>t.GSCapacity);
                    isordered = true;
                }
				// GSLocality NVARCHAR(50) 地点 
                if(!string.IsNullOrEmpty(searchModel.GSLocality)) query = query.Where(t=>t.GSLocality.Contains(searchModel.GSLocality));
                if(sort=="GSLocality")
                {
					query = order=="asc"?query.OrderBy(t=>t.GSLocality):query.OrderByDescending(t=>t.GSLocality);
                    isordered = true;
                }
				// GSResponsibleForManualNumber INT 负责人工号 
                if(searchModel.MinGSResponsibleForManualNumber!=null) query = query.Where(t=>t.GSResponsibleForManualNumber>=searchModel.MinGSResponsibleForManualNumber);
                if(searchModel.MaxGSResponsibleForManualNumber!=null) query = query.Where(t=>t.GSResponsibleForManualNumber<=searchModel.MaxGSResponsibleForManualNumber);
                if(sort=="GSResponsibleForManualNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.GSResponsibleForManualNumber):query.OrderByDescending(t=>t.GSResponsibleForManualNumber);
                    isordered = true;
                }
				if(!string.IsNullOrEmpty(search)){
					query = query.Where(t=>t.id!=-1||t.GSCapacity.Contains(search)||t.GSLocality.Contains(search));
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
    /// 【员工】7次查询分别得到七天内记录的条数，形成柱状图，折线图
    /// </summary>
    public partial class StaffnameCountEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
                var now = Parse(request.context.Request.Params["date"] ?? Now.ToString("yyyy-MM-dd"));
                var dates = Range(1, 7).Select(p => now.AddDays(-1 * p)).ToList();
                var list = dates.Select(p => ctx.Staffname.Count(m => m.CreateOn==p)).ToList();
                return new
                {
                    sum = list.Sum(),
                    xaxis = dates.Select(p => p.ToString("yyyy-MM-dd")).ToList(),
                    series = list
                };
            }
        }
        public override string Comments=> "【员工】7次查询分别得到七天内记录的条数，形成柱状图，折线图";
    }
	public partial class TruncateStaffnameEvaluator : Evaluator
	{
        protected override object Evaluate(CommonRequest request)
		{
            using (var ctx = new DefaultContext())
			{
                ctx.Staffname.RemoveRange(ctx.Staffname);
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
    /// 删除【员工】
    /// </summary>
    public partial class DeleteStaffnameEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            var data = JsonConvert.DeserializeObject<Staffname>(HttpUtility.UrlDecode(request.data));
            if (data == null)
                return new CommonOutputT<string>
                {
                    success = false,
                    message = "参数错误"
                };
            using (var ctx = new DefaultContext())
            {
                var one = ctx.Staffname.Find(data.id);
				if(one==null){
					return new CommonOutputT<string>
					{
						success = false,
						message = "未找到需要删除的数据"
					};
				}
                one.IsDeleted = 1;
				ctx.Staffname.AddOrUpdate(one);
				ctx.SaveChanges();
				return new CommonOutputT<string>
				{
					success = true,
					message = "删除成功"
				};
            }
			
        }
        public override string Comments=> "删除一条员工记录";
    }
	
    /// <summary>
    /// 保存【员工】
    /// </summary>
    public partial class SaveStaffnameEvaluator : Evaluator
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
			Staffname entity = null;
			try
			{
				entity = JsonConvert.DeserializeObject<Staffname>(HttpUtility.UrlDecode(s));
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
                    var one = ctx.Staffname.FirstOrDefault(p=>p.id==entity.id);
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
					ctx.Staffname.AddOrUpdate(one);
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
				

								// NVARCHAR(50) 姓名
				entity.SName = HttpUtility.UrlDecode(entity.SName);
					// NVARCHAR(50) 学历
				entity.SEducation = HttpUtility.UrlDecode(entity.SEducation);
					// NVARCHAR(50) 联系方式
				entity.SCommonModeOfContact = HttpUtility.UrlDecode(entity.SCommonModeOfContact);
	
                entity.CreateBy = entity.CreateBy ?? user?.UILoginName ?? "未登录用户";
                entity.UpdateBy = user?.UILoginName ?? "未登录用户";
                entity.CreateOn = entity.CreateOn ?? Now;
                entity.TransactionID = transactionId;
                entity.UpdateOn = Now;
                entity.IsDeleted = 0;
	            entity.VersionNo = entity.VersionNo ?? 0;
                entity.DataLevel = entity.DataLevel ?? user?.DataLevel ?? "019999";
				ctx.Staffname.AddOrUpdate(entity);
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
        public override string Comments=> "保存一条Staffname记录";
    }
	
    /// <summary>
    /// 查询空的【员工】
    /// </summary>
    public partial class GetStaffnameEmptyEvaluator:Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            return new Staffname();
        }
        public override string Comments=> "获取空的员工记录";
    }
	
    /// <summary>
    /// 查询【员工】列表
    /// </summary>
    public partial class GetStaffnameListEvaluator : Evaluator
    {
        public override string Comments=> "获取Staffname列表 ";
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
				var datalevel = CurrentUserInformation?.DataLevel;
                var searchModel = HttpUtility.UrlDecode(request.data).Deserialize<StaffnameSearchModel>() ?? new StaffnameSearchModel();
                var query = ctx.Staffname.Where(t=>t.IsDeleted==0);
                var @params = request.context.Request.Params;
                searchModel.PageSize = (@params["limit"] ?? searchModel.PageSize.ToString()).ToInt();
				searchModel.PageSize = searchModel.PageSize==0?4000:searchModel.PageSize;
                searchModel.PageIndex = (@params["offset"]).ToInt()/ searchModel.PageSize;
                var isordered = false;
                var search = searchModel.SearchKey ?? @params["search"];
                var sort = searchModel.Sort ?? @params["sort"];
				var order = @params["order"];
				// SJobNumber INT 工号 
                if(searchModel.MinSJobNumber!=null) query = query.Where(t=>t.SJobNumber>=searchModel.MinSJobNumber);
                if(searchModel.MaxSJobNumber!=null) query = query.Where(t=>t.SJobNumber<=searchModel.MaxSJobNumber);
                if(sort=="SJobNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.SJobNumber):query.OrderByDescending(t=>t.SJobNumber);
                    isordered = true;
                }
				// SName NVARCHAR(50) 姓名 
                if(!string.IsNullOrEmpty(searchModel.SName)) query = query.Where(t=>t.SName.Contains(searchModel.SName));
                if(sort=="SName")
                {
					query = order=="asc"?query.OrderBy(t=>t.SName):query.OrderByDescending(t=>t.SName);
                    isordered = true;
                }
				// SEducation NVARCHAR(50) 学历 
                if(!string.IsNullOrEmpty(searchModel.SEducation)) query = query.Where(t=>t.SEducation.Contains(searchModel.SEducation));
                if(sort=="SEducation")
                {
					query = order=="asc"?query.OrderBy(t=>t.SEducation):query.OrderByDescending(t=>t.SEducation);
                    isordered = true;
                }
				// SCommonModeOfContact NVARCHAR(50) 联系方式 
                if(!string.IsNullOrEmpty(searchModel.SCommonModeOfContact)) query = query.Where(t=>t.SCommonModeOfContact.Contains(searchModel.SCommonModeOfContact));
                if(sort=="SCommonModeOfContact")
                {
					query = order=="asc"?query.OrderBy(t=>t.SCommonModeOfContact):query.OrderByDescending(t=>t.SCommonModeOfContact);
                    isordered = true;
                }
				if(!string.IsNullOrEmpty(search)){
					query = query.Where(t=>t.id!=-1||t.SName.Contains(search)||t.SEducation.Contains(search)||t.SCommonModeOfContact.Contains(search));
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
                return new CommonOutputList<Staffname>
                {
                    success = true, rows = rows, total = total, message="查询成功"
                };
            }
        }
    }


    /// <summary>
    /// 【采购】7次查询分别得到七天内记录的条数，形成柱状图，折线图
    /// </summary>
    public partial class ProcureCountEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
                var now = Parse(request.context.Request.Params["date"] ?? Now.ToString("yyyy-MM-dd"));
                var dates = Range(1, 7).Select(p => now.AddDays(-1 * p)).ToList();
                var list = dates.Select(p => ctx.Procure.Count(m => m.CreateOn==p)).ToList();
                return new
                {
                    sum = list.Sum(),
                    xaxis = dates.Select(p => p.ToString("yyyy-MM-dd")).ToList(),
                    series = list
                };
            }
        }
        public override string Comments=> "【采购】7次查询分别得到七天内记录的条数，形成柱状图，折线图";
    }
	public partial class TruncateProcureEvaluator : Evaluator
	{
        protected override object Evaluate(CommonRequest request)
		{
            using (var ctx = new DefaultContext())
			{
                ctx.Procure.RemoveRange(ctx.Procure);
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
    /// 删除【采购】
    /// </summary>
    public partial class DeleteProcureEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            var data = JsonConvert.DeserializeObject<Procure>(HttpUtility.UrlDecode(request.data));
            if (data == null)
                return new CommonOutputT<string>
                {
                    success = false,
                    message = "参数错误"
                };
            using (var ctx = new DefaultContext())
            {
                var one = ctx.Procure.Find(data.id);
				if(one==null){
					return new CommonOutputT<string>
					{
						success = false,
						message = "未找到需要删除的数据"
					};
				}
                one.IsDeleted = 1;
				ctx.Procure.AddOrUpdate(one);
				ctx.SaveChanges();
				return new CommonOutputT<string>
				{
					success = true,
					message = "删除成功"
				};
            }
			
        }
        public override string Comments=> "删除一条采购记录";
    }
	
    /// <summary>
    /// 保存【采购】
    /// </summary>
    public partial class SaveProcureEvaluator : Evaluator
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
			Procure entity = null;
			try
			{
				entity = JsonConvert.DeserializeObject<Procure>(HttpUtility.UrlDecode(s));
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
                    var one = ctx.Procure.FirstOrDefault(p=>p.id==entity.id);
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
					ctx.Procure.AddOrUpdate(one);
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
				

								// NVARCHAR(50) 数量
				entity.PAmount = HttpUtility.UrlDecode(entity.PAmount);
					// NVARCHAR(50) 价格
				entity.PPrice = HttpUtility.UrlDecode(entity.PPrice);
					// NVARCHAR(50) 备注
				entity.PRemarks = HttpUtility.UrlDecode(entity.PRemarks);
	
                entity.CreateBy = entity.CreateBy ?? user?.UILoginName ?? "未登录用户";
                entity.UpdateBy = user?.UILoginName ?? "未登录用户";
                entity.CreateOn = entity.CreateOn ?? Now;
                entity.TransactionID = transactionId;
                entity.UpdateOn = Now;
                entity.IsDeleted = 0;
	            entity.VersionNo = entity.VersionNo ?? 0;
                entity.DataLevel = entity.DataLevel ?? user?.DataLevel ?? "019999";
				ctx.Procure.AddOrUpdate(entity);
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
        public override string Comments=> "保存一条Procure记录";
    }
	
    /// <summary>
    /// 查询空的【采购】
    /// </summary>
    public partial class GetProcureEmptyEvaluator:Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            return new Procure();
        }
        public override string Comments=> "获取空的采购记录";
    }
	
    /// <summary>
    /// 查询【采购】列表
    /// </summary>
    public partial class GetProcureListEvaluator : Evaluator
    {
        public override string Comments=> "获取Procure列表 ";
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
				var datalevel = CurrentUserInformation?.DataLevel;
                var searchModel = HttpUtility.UrlDecode(request.data).Deserialize<ProcureSearchModel>() ?? new ProcureSearchModel();
                var query = ctx.Procure.Where(t=>t.IsDeleted==0);
                var @params = request.context.Request.Params;
                searchModel.PageSize = (@params["limit"] ?? searchModel.PageSize.ToString()).ToInt();
				searchModel.PageSize = searchModel.PageSize==0?4000:searchModel.PageSize;
                searchModel.PageIndex = (@params["offset"]).ToInt()/ searchModel.PageSize;
                var isordered = false;
                var search = searchModel.SearchKey ?? @params["search"];
                var sort = searchModel.Sort ?? @params["sort"];
				var order = @params["order"];
				// PSupplierNumber INT 供应商编号 
                if(searchModel.MinPSupplierNumber!=null) query = query.Where(t=>t.PSupplierNumber>=searchModel.MinPSupplierNumber);
                if(searchModel.MaxPSupplierNumber!=null) query = query.Where(t=>t.PSupplierNumber<=searchModel.MaxPSupplierNumber);
                if(sort=="PSupplierNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.PSupplierNumber):query.OrderByDescending(t=>t.PSupplierNumber);
                    isordered = true;
                }
				// PCargoNumber INT 货物编号 
                if(searchModel.MinPCargoNumber!=null) query = query.Where(t=>t.PCargoNumber>=searchModel.MinPCargoNumber);
                if(searchModel.MaxPCargoNumber!=null) query = query.Where(t=>t.PCargoNumber<=searchModel.MaxPCargoNumber);
                if(sort=="PCargoNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.PCargoNumber):query.OrderByDescending(t=>t.PCargoNumber);
                    isordered = true;
                }
				// PPurchasingStaffNumber INT 采购员工号 
                if(searchModel.MinPPurchasingStaffNumber!=null) query = query.Where(t=>t.PPurchasingStaffNumber>=searchModel.MinPPurchasingStaffNumber);
                if(searchModel.MaxPPurchasingStaffNumber!=null) query = query.Where(t=>t.PPurchasingStaffNumber<=searchModel.MaxPPurchasingStaffNumber);
                if(sort=="PPurchasingStaffNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.PPurchasingStaffNumber):query.OrderByDescending(t=>t.PPurchasingStaffNumber);
                    isordered = true;
                }
				// PDate DATETIME 日期 
                if(searchModel.FromPDate!=null) query = query.Where(t=>t.PDate>=searchModel.FromPDate);
                if(searchModel.ToPDate!=null) query = query.Where(t=>t.PDate<=searchModel.ToPDate);
                if(sort=="PDate")
                {
					query = order=="asc"?query.OrderBy(t=>t.PDate):query.OrderByDescending(t=>t.PDate);
                    isordered = true;
                }
				// PAmount NVARCHAR(50) 数量 
                if(!string.IsNullOrEmpty(searchModel.PAmount)) query = query.Where(t=>t.PAmount.Contains(searchModel.PAmount));
                if(sort=="PAmount")
                {
					query = order=="asc"?query.OrderBy(t=>t.PAmount):query.OrderByDescending(t=>t.PAmount);
                    isordered = true;
                }
				// PPrice NVARCHAR(50) 价格 
                if(!string.IsNullOrEmpty(searchModel.PPrice)) query = query.Where(t=>t.PPrice.Contains(searchModel.PPrice));
                if(sort=="PPrice")
                {
					query = order=="asc"?query.OrderBy(t=>t.PPrice):query.OrderByDescending(t=>t.PPrice);
                    isordered = true;
                }
				// PRemarks NVARCHAR(50) 备注 
                if(!string.IsNullOrEmpty(searchModel.PRemarks)) query = query.Where(t=>t.PRemarks.Contains(searchModel.PRemarks));
                if(sort=="PRemarks")
                {
					query = order=="asc"?query.OrderBy(t=>t.PRemarks):query.OrderByDescending(t=>t.PRemarks);
                    isordered = true;
                }
				if(!string.IsNullOrEmpty(search)){
					query = query.Where(t=>t.id!=-1||t.PAmount.Contains(search)||t.PPrice.Contains(search)||t.PRemarks.Contains(search));
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
                return new CommonOutputList<Procure>
                {
                    success = true, rows = rows, total = total, message="查询成功"
                };
            }
        }
    }


    /// <summary>
    /// 【销售】7次查询分别得到七天内记录的条数，形成柱状图，折线图
    /// </summary>
    public partial class SalesCountEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
                var now = Parse(request.context.Request.Params["date"] ?? Now.ToString("yyyy-MM-dd"));
                var dates = Range(1, 7).Select(p => now.AddDays(-1 * p)).ToList();
                var list = dates.Select(p => ctx.Sales.Count(m => m.CreateOn==p)).ToList();
                return new
                {
                    sum = list.Sum(),
                    xaxis = dates.Select(p => p.ToString("yyyy-MM-dd")).ToList(),
                    series = list
                };
            }
        }
        public override string Comments=> "【销售】7次查询分别得到七天内记录的条数，形成柱状图，折线图";
    }
	public partial class TruncateSalesEvaluator : Evaluator
	{
        protected override object Evaluate(CommonRequest request)
		{
            using (var ctx = new DefaultContext())
			{
                ctx.Sales.RemoveRange(ctx.Sales);
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
    /// 删除【销售】
    /// </summary>
    public partial class DeleteSalesEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            var data = JsonConvert.DeserializeObject<Sales>(HttpUtility.UrlDecode(request.data));
            if (data == null)
                return new CommonOutputT<string>
                {
                    success = false,
                    message = "参数错误"
                };
            using (var ctx = new DefaultContext())
            {
                var one = ctx.Sales.Find(data.id);
				if(one==null){
					return new CommonOutputT<string>
					{
						success = false,
						message = "未找到需要删除的数据"
					};
				}
                one.IsDeleted = 1;
				ctx.Sales.AddOrUpdate(one);
				ctx.SaveChanges();
				return new CommonOutputT<string>
				{
					success = true,
					message = "删除成功"
				};
            }
			
        }
        public override string Comments=> "删除一条销售记录";
    }
	
    /// <summary>
    /// 保存【销售】
    /// </summary>
    public partial class SaveSalesEvaluator : Evaluator
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
			Sales entity = null;
			try
			{
				entity = JsonConvert.DeserializeObject<Sales>(HttpUtility.UrlDecode(s));
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
                    var one = ctx.Sales.FirstOrDefault(p=>p.id==entity.id);
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
					ctx.Sales.AddOrUpdate(one);
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
				

								// NVARCHAR(50) 数量
				entity.SAmount = HttpUtility.UrlDecode(entity.SAmount);
					// NVARCHAR(50) 价格
				entity.SPrice = HttpUtility.UrlDecode(entity.SPrice);
					// NVARCHAR(50) 备注
				entity.SRemarks = HttpUtility.UrlDecode(entity.SRemarks);
	
                entity.CreateBy = entity.CreateBy ?? user?.UILoginName ?? "未登录用户";
                entity.UpdateBy = user?.UILoginName ?? "未登录用户";
                entity.CreateOn = entity.CreateOn ?? Now;
                entity.TransactionID = transactionId;
                entity.UpdateOn = Now;
                entity.IsDeleted = 0;
	            entity.VersionNo = entity.VersionNo ?? 0;
                entity.DataLevel = entity.DataLevel ?? user?.DataLevel ?? "019999";
				ctx.Sales.AddOrUpdate(entity);
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
        public override string Comments=> "保存一条Sales记录";
    }
	
    /// <summary>
    /// 查询空的【销售】
    /// </summary>
    public partial class GetSalesEmptyEvaluator:Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            return new Sales();
        }
        public override string Comments=> "获取空的销售记录";
    }
	
    /// <summary>
    /// 查询【销售】列表
    /// </summary>
    public partial class GetSalesListEvaluator : Evaluator
    {
        public override string Comments=> "获取Sales列表 ";
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
				var datalevel = CurrentUserInformation?.DataLevel;
                var searchModel = HttpUtility.UrlDecode(request.data).Deserialize<SalesSearchModel>() ?? new SalesSearchModel();
                var query = ctx.Sales.Where(t=>t.IsDeleted==0);
                var @params = request.context.Request.Params;
                searchModel.PageSize = (@params["limit"] ?? searchModel.PageSize.ToString()).ToInt();
				searchModel.PageSize = searchModel.PageSize==0?4000:searchModel.PageSize;
                searchModel.PageIndex = (@params["offset"]).ToInt()/ searchModel.PageSize;
                var isordered = false;
                var search = searchModel.SearchKey ?? @params["search"];
                var sort = searchModel.Sort ?? @params["sort"];
				var order = @params["order"];
				// SCargoNumber INT 货物编号 
                if(searchModel.MinSCargoNumber!=null) query = query.Where(t=>t.SCargoNumber>=searchModel.MinSCargoNumber);
                if(searchModel.MaxSCargoNumber!=null) query = query.Where(t=>t.SCargoNumber<=searchModel.MaxSCargoNumber);
                if(sort=="SCargoNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.SCargoNumber):query.OrderByDescending(t=>t.SCargoNumber);
                    isordered = true;
                }
				// SCustomerNumber INT 客户编号 
                if(searchModel.MinSCustomerNumber!=null) query = query.Where(t=>t.SCustomerNumber>=searchModel.MinSCustomerNumber);
                if(searchModel.MaxSCustomerNumber!=null) query = query.Where(t=>t.SCustomerNumber<=searchModel.MaxSCustomerNumber);
                if(sort=="SCustomerNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.SCustomerNumber):query.OrderByDescending(t=>t.SCustomerNumber);
                    isordered = true;
                }
				// SSalesStaffNumber INT 销售员工号 
                if(searchModel.MinSSalesStaffNumber!=null) query = query.Where(t=>t.SSalesStaffNumber>=searchModel.MinSSalesStaffNumber);
                if(searchModel.MaxSSalesStaffNumber!=null) query = query.Where(t=>t.SSalesStaffNumber<=searchModel.MaxSSalesStaffNumber);
                if(sort=="SSalesStaffNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.SSalesStaffNumber):query.OrderByDescending(t=>t.SSalesStaffNumber);
                    isordered = true;
                }
				// SDate DATETIME 日期 
                if(searchModel.FromSDate!=null) query = query.Where(t=>t.SDate>=searchModel.FromSDate);
                if(searchModel.ToSDate!=null) query = query.Where(t=>t.SDate<=searchModel.ToSDate);
                if(sort=="SDate")
                {
					query = order=="asc"?query.OrderBy(t=>t.SDate):query.OrderByDescending(t=>t.SDate);
                    isordered = true;
                }
				// SAmount NVARCHAR(50) 数量 
                if(!string.IsNullOrEmpty(searchModel.SAmount)) query = query.Where(t=>t.SAmount.Contains(searchModel.SAmount));
                if(sort=="SAmount")
                {
					query = order=="asc"?query.OrderBy(t=>t.SAmount):query.OrderByDescending(t=>t.SAmount);
                    isordered = true;
                }
				// SPrice NVARCHAR(50) 价格 
                if(!string.IsNullOrEmpty(searchModel.SPrice)) query = query.Where(t=>t.SPrice.Contains(searchModel.SPrice));
                if(sort=="SPrice")
                {
					query = order=="asc"?query.OrderBy(t=>t.SPrice):query.OrderByDescending(t=>t.SPrice);
                    isordered = true;
                }
				// SRemarks NVARCHAR(50) 备注 
                if(!string.IsNullOrEmpty(searchModel.SRemarks)) query = query.Where(t=>t.SRemarks.Contains(searchModel.SRemarks));
                if(sort=="SRemarks")
                {
					query = order=="asc"?query.OrderBy(t=>t.SRemarks):query.OrderByDescending(t=>t.SRemarks);
                    isordered = true;
                }
				if(!string.IsNullOrEmpty(search)){
					query = query.Where(t=>t.id!=-1||t.SAmount.Contains(search)||t.SPrice.Contains(search)||t.SRemarks.Contains(search));
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
                return new CommonOutputList<Sales>
                {
                    success = true, rows = rows, total = total, message="查询成功"
                };
            }
        }
    }


    /// <summary>
    /// 【供应】7次查询分别得到七天内记录的条数，形成柱状图，折线图
    /// </summary>
    public partial class FurnishCountEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
                var now = Parse(request.context.Request.Params["date"] ?? Now.ToString("yyyy-MM-dd"));
                var dates = Range(1, 7).Select(p => now.AddDays(-1 * p)).ToList();
                var list = dates.Select(p => ctx.Furnish.Count(m => m.CreateOn==p)).ToList();
                return new
                {
                    sum = list.Sum(),
                    xaxis = dates.Select(p => p.ToString("yyyy-MM-dd")).ToList(),
                    series = list
                };
            }
        }
        public override string Comments=> "【供应】7次查询分别得到七天内记录的条数，形成柱状图，折线图";
    }
	public partial class TruncateFurnishEvaluator : Evaluator
	{
        protected override object Evaluate(CommonRequest request)
		{
            using (var ctx = new DefaultContext())
			{
                ctx.Furnish.RemoveRange(ctx.Furnish);
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
    /// 删除【供应】
    /// </summary>
    public partial class DeleteFurnishEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            var data = JsonConvert.DeserializeObject<Furnish>(HttpUtility.UrlDecode(request.data));
            if (data == null)
                return new CommonOutputT<string>
                {
                    success = false,
                    message = "参数错误"
                };
            using (var ctx = new DefaultContext())
            {
                var one = ctx.Furnish.Find(data.id);
				if(one==null){
					return new CommonOutputT<string>
					{
						success = false,
						message = "未找到需要删除的数据"
					};
				}
                one.IsDeleted = 1;
				ctx.Furnish.AddOrUpdate(one);
				ctx.SaveChanges();
				return new CommonOutputT<string>
				{
					success = true,
					message = "删除成功"
				};
            }
			
        }
        public override string Comments=> "删除一条供应记录";
    }
	
    /// <summary>
    /// 保存【供应】
    /// </summary>
    public partial class SaveFurnishEvaluator : Evaluator
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
			Furnish entity = null;
			try
			{
				entity = JsonConvert.DeserializeObject<Furnish>(HttpUtility.UrlDecode(s));
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
                    var one = ctx.Furnish.FirstOrDefault(p=>p.id==entity.id);
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
					ctx.Furnish.AddOrUpdate(one);
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
				

								// NVARCHAR(50) 数量
				entity.FAmount = HttpUtility.UrlDecode(entity.FAmount);
					// NVARCHAR(50) 备注
				entity.FRemarks = HttpUtility.UrlDecode(entity.FRemarks);
	
                entity.CreateBy = entity.CreateBy ?? user?.UILoginName ?? "未登录用户";
                entity.UpdateBy = user?.UILoginName ?? "未登录用户";
                entity.CreateOn = entity.CreateOn ?? Now;
                entity.TransactionID = transactionId;
                entity.UpdateOn = Now;
                entity.IsDeleted = 0;
	            entity.VersionNo = entity.VersionNo ?? 0;
                entity.DataLevel = entity.DataLevel ?? user?.DataLevel ?? "019999";
				ctx.Furnish.AddOrUpdate(entity);
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
        public override string Comments=> "保存一条Furnish记录";
    }
	
    /// <summary>
    /// 查询空的【供应】
    /// </summary>
    public partial class GetFurnishEmptyEvaluator:Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            return new Furnish();
        }
        public override string Comments=> "获取空的供应记录";
    }
	
    /// <summary>
    /// 查询【供应】列表
    /// </summary>
    public partial class GetFurnishListEvaluator : Evaluator
    {
        public override string Comments=> "获取Furnish列表 ";
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
				var datalevel = CurrentUserInformation?.DataLevel;
                var searchModel = HttpUtility.UrlDecode(request.data).Deserialize<FurnishSearchModel>() ?? new FurnishSearchModel();
                var query = ctx.Furnish.Where(t=>t.IsDeleted==0);
                var @params = request.context.Request.Params;
                searchModel.PageSize = (@params["limit"] ?? searchModel.PageSize.ToString()).ToInt();
				searchModel.PageSize = searchModel.PageSize==0?4000:searchModel.PageSize;
                searchModel.PageIndex = (@params["offset"]).ToInt()/ searchModel.PageSize;
                var isordered = false;
                var search = searchModel.SearchKey ?? @params["search"];
                var sort = searchModel.Sort ?? @params["sort"];
				var order = @params["order"];
				// FSupplierNumber INT 供应商编号 
                if(searchModel.MinFSupplierNumber!=null) query = query.Where(t=>t.FSupplierNumber>=searchModel.MinFSupplierNumber);
                if(searchModel.MaxFSupplierNumber!=null) query = query.Where(t=>t.FSupplierNumber<=searchModel.MaxFSupplierNumber);
                if(sort=="FSupplierNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.FSupplierNumber):query.OrderByDescending(t=>t.FSupplierNumber);
                    isordered = true;
                }
				// FCargoNumber INT 货物编号 
                if(searchModel.MinFCargoNumber!=null) query = query.Where(t=>t.FCargoNumber>=searchModel.MinFCargoNumber);
                if(searchModel.MaxFCargoNumber!=null) query = query.Where(t=>t.FCargoNumber<=searchModel.MaxFCargoNumber);
                if(sort=="FCargoNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.FCargoNumber):query.OrderByDescending(t=>t.FCargoNumber);
                    isordered = true;
                }
				// FDate DATETIME 日期 
                if(searchModel.FromFDate!=null) query = query.Where(t=>t.FDate>=searchModel.FromFDate);
                if(searchModel.ToFDate!=null) query = query.Where(t=>t.FDate<=searchModel.ToFDate);
                if(sort=="FDate")
                {
					query = order=="asc"?query.OrderBy(t=>t.FDate):query.OrderByDescending(t=>t.FDate);
                    isordered = true;
                }
				// FAmount NVARCHAR(50) 数量 
                if(!string.IsNullOrEmpty(searchModel.FAmount)) query = query.Where(t=>t.FAmount.Contains(searchModel.FAmount));
                if(sort=="FAmount")
                {
					query = order=="asc"?query.OrderBy(t=>t.FAmount):query.OrderByDescending(t=>t.FAmount);
                    isordered = true;
                }
				// FRemarks NVARCHAR(50) 备注 
                if(!string.IsNullOrEmpty(searchModel.FRemarks)) query = query.Where(t=>t.FRemarks.Contains(searchModel.FRemarks));
                if(sort=="FRemarks")
                {
					query = order=="asc"?query.OrderBy(t=>t.FRemarks):query.OrderByDescending(t=>t.FRemarks);
                    isordered = true;
                }
				if(!string.IsNullOrEmpty(search)){
					query = query.Where(t=>t.id!=-1||t.FAmount.Contains(search)||t.FRemarks.Contains(search));
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
                return new CommonOutputList<Furnish>
                {
                    success = true, rows = rows, total = total, message="查询成功"
                };
            }
        }
    }


    /// <summary>
    /// 【入库】7次查询分别得到七天内记录的条数，形成柱状图，折线图
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
        public override string Comments=> "【入库】7次查询分别得到七天内记录的条数，形成柱状图，折线图";
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
    /// 删除【入库】
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
                    message = "参数错误"
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
        public override string Comments=> "删除一条入库记录";
    }
	
    /// <summary>
    /// 保存【入库】
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
				

								// NVARCHAR(50) 数量
				entity.WRAmount = HttpUtility.UrlDecode(entity.WRAmount);
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
    /// 查询空的【入库】
    /// </summary>
    public partial class GetWarehousingRecordEmptyEvaluator:Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            return new WarehousingRecord();
        }
        public override string Comments=> "获取空的入库记录";
    }
	
    /// <summary>
    /// 查询【入库】列表
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
                var query = ctx.WarehousingRecord.Where(t=>t.IsDeleted==0);
                var @params = request.context.Request.Params;
                searchModel.PageSize = (@params["limit"] ?? searchModel.PageSize.ToString()).ToInt();
				searchModel.PageSize = searchModel.PageSize==0?4000:searchModel.PageSize;
                searchModel.PageIndex = (@params["offset"]).ToInt()/ searchModel.PageSize;
                var isordered = false;
                var search = searchModel.SearchKey ?? @params["search"];
                var sort = searchModel.Sort ?? @params["sort"];
				var order = @params["order"];
				// WRWarehouseNumber INT 仓库编号 
                if(searchModel.MinWRWarehouseNumber!=null) query = query.Where(t=>t.WRWarehouseNumber>=searchModel.MinWRWarehouseNumber);
                if(searchModel.MaxWRWarehouseNumber!=null) query = query.Where(t=>t.WRWarehouseNumber<=searchModel.MaxWRWarehouseNumber);
                if(sort=="WRWarehouseNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.WRWarehouseNumber):query.OrderByDescending(t=>t.WRWarehouseNumber);
                    isordered = true;
                }
				// WRCargoNumber INT 货物编号 
                if(searchModel.MinWRCargoNumber!=null) query = query.Where(t=>t.WRCargoNumber>=searchModel.MinWRCargoNumber);
                if(searchModel.MaxWRCargoNumber!=null) query = query.Where(t=>t.WRCargoNumber<=searchModel.MaxWRCargoNumber);
                if(sort=="WRCargoNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.WRCargoNumber):query.OrderByDescending(t=>t.WRCargoNumber);
                    isordered = true;
                }
				// WRWarehouseManagementStaffNumber INT 仓库管理员工号 
                if(searchModel.MinWRWarehouseManagementStaffNumber!=null) query = query.Where(t=>t.WRWarehouseManagementStaffNumber>=searchModel.MinWRWarehouseManagementStaffNumber);
                if(searchModel.MaxWRWarehouseManagementStaffNumber!=null) query = query.Where(t=>t.WRWarehouseManagementStaffNumber<=searchModel.MaxWRWarehouseManagementStaffNumber);
                if(sort=="WRWarehouseManagementStaffNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.WRWarehouseManagementStaffNumber):query.OrderByDescending(t=>t.WRWarehouseManagementStaffNumber);
                    isordered = true;
                }
				// WRDate DATETIME 日期 
                if(searchModel.FromWRDate!=null) query = query.Where(t=>t.WRDate>=searchModel.FromWRDate);
                if(searchModel.ToWRDate!=null) query = query.Where(t=>t.WRDate<=searchModel.ToWRDate);
                if(sort=="WRDate")
                {
					query = order=="asc"?query.OrderBy(t=>t.WRDate):query.OrderByDescending(t=>t.WRDate);
                    isordered = true;
                }
				// WRAmount NVARCHAR(50) 数量 
                if(!string.IsNullOrEmpty(searchModel.WRAmount)) query = query.Where(t=>t.WRAmount.Contains(searchModel.WRAmount));
                if(sort=="WRAmount")
                {
					query = order=="asc"?query.OrderBy(t=>t.WRAmount):query.OrderByDescending(t=>t.WRAmount);
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
					query = query.Where(t=>t.id!=-1||t.WRAmount.Contains(search)||t.WRRemarks.Contains(search));
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
    /// 【补货】7次查询分别得到七天内记录的条数，形成柱状图，折线图
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
        public override string Comments=> "【补货】7次查询分别得到七天内记录的条数，形成柱状图，折线图";
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
    /// 删除【补货】
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
                    message = "参数错误"
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
        public override string Comments=> "删除一条补货记录";
    }
	
    /// <summary>
    /// 保存【补货】
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
				

								// NVARCHAR(50) 数量
				entity.RAFAmount = HttpUtility.UrlDecode(entity.RAFAmount);
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
    /// 查询空的【补货】
    /// </summary>
    public partial class GetReplenishmentApplicationFormEmptyEvaluator:Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            return new ReplenishmentApplicationForm();
        }
        public override string Comments=> "获取空的补货记录";
    }
	
    /// <summary>
    /// 查询【补货】列表
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
                var query = ctx.ReplenishmentApplicationForm.Where(t=>t.IsDeleted==0);
                var @params = request.context.Request.Params;
                searchModel.PageSize = (@params["limit"] ?? searchModel.PageSize.ToString()).ToInt();
				searchModel.PageSize = searchModel.PageSize==0?4000:searchModel.PageSize;
                searchModel.PageIndex = (@params["offset"]).ToInt()/ searchModel.PageSize;
                var isordered = false;
                var search = searchModel.SearchKey ?? @params["search"];
                var sort = searchModel.Sort ?? @params["sort"];
				var order = @params["order"];
				// RAFShelfNumber INT 货架编号 
                if(searchModel.MinRAFShelfNumber!=null) query = query.Where(t=>t.RAFShelfNumber>=searchModel.MinRAFShelfNumber);
                if(searchModel.MaxRAFShelfNumber!=null) query = query.Where(t=>t.RAFShelfNumber<=searchModel.MaxRAFShelfNumber);
                if(sort=="RAFShelfNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.RAFShelfNumber):query.OrderByDescending(t=>t.RAFShelfNumber);
                    isordered = true;
                }
				// RAFCargoNumber INT 货物编号 
                if(searchModel.MinRAFCargoNumber!=null) query = query.Where(t=>t.RAFCargoNumber>=searchModel.MinRAFCargoNumber);
                if(searchModel.MaxRAFCargoNumber!=null) query = query.Where(t=>t.RAFCargoNumber<=searchModel.MaxRAFCargoNumber);
                if(sort=="RAFCargoNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.RAFCargoNumber):query.OrderByDescending(t=>t.RAFCargoNumber);
                    isordered = true;
                }
				// RAFWarehouseManagementStaffNumber INT 仓库管理员工号 
                if(searchModel.MinRAFWarehouseManagementStaffNumber!=null) query = query.Where(t=>t.RAFWarehouseManagementStaffNumber>=searchModel.MinRAFWarehouseManagementStaffNumber);
                if(searchModel.MaxRAFWarehouseManagementStaffNumber!=null) query = query.Where(t=>t.RAFWarehouseManagementStaffNumber<=searchModel.MaxRAFWarehouseManagementStaffNumber);
                if(sort=="RAFWarehouseManagementStaffNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.RAFWarehouseManagementStaffNumber):query.OrderByDescending(t=>t.RAFWarehouseManagementStaffNumber);
                    isordered = true;
                }
				// RAFDate DATETIME 日期 
                if(searchModel.FromRAFDate!=null) query = query.Where(t=>t.RAFDate>=searchModel.FromRAFDate);
                if(searchModel.ToRAFDate!=null) query = query.Where(t=>t.RAFDate<=searchModel.ToRAFDate);
                if(sort=="RAFDate")
                {
					query = order=="asc"?query.OrderBy(t=>t.RAFDate):query.OrderByDescending(t=>t.RAFDate);
                    isordered = true;
                }
				// RAFAmount NVARCHAR(50) 数量 
                if(!string.IsNullOrEmpty(searchModel.RAFAmount)) query = query.Where(t=>t.RAFAmount.Contains(searchModel.RAFAmount));
                if(sort=="RAFAmount")
                {
					query = order=="asc"?query.OrderBy(t=>t.RAFAmount):query.OrderByDescending(t=>t.RAFAmount);
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
					query = query.Where(t=>t.id!=-1||t.RAFAmount.Contains(search)||t.RAFRemarks.Contains(search));
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
                    message = "参数错误"
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
                var query = ctx.MenuConfiguration.Where(t=>t.IsDeleted==0);
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
                    message = "参数错误"
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
                var query = ctx.RoleMenu.Where(t=>t.IsDeleted==0);
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
                    message = "参数错误"
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
                var query = ctx.UserRole.Where(t=>t.IsDeleted==0);
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
                    message = "参数错误"
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
                var query = ctx.RoleConfiguration.Where(t=>t.IsDeleted==0);
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
                    message = "参数错误"
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
                var query = ctx.UserInformation.Where(t=>t.IsDeleted==0);
                var @params = request.context.Request.Params;
                searchModel.PageSize = (@params["limit"] ?? searchModel.PageSize.ToString()).ToInt();
				searchModel.PageSize = searchModel.PageSize==0?4000:searchModel.PageSize;
                searchModel.PageIndex = (@params["offset"]).ToInt()/ searchModel.PageSize;
                var isordered = false;
                var search = searchModel.SearchKey ?? @params["search"];
                var sort = searchModel.Sort ?? @params["sort"];
				var order = @params["order"];
				// UIJobNumber INT 工号 
                if(searchModel.MinUIJobNumber!=null) query = query.Where(t=>t.UIJobNumber>=searchModel.MinUIJobNumber);
                if(searchModel.MaxUIJobNumber!=null) query = query.Where(t=>t.UIJobNumber<=searchModel.MaxUIJobNumber);
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
					query = query.Where(t=>t.id!=-1||t.UILoginName.Contains(search)||t.UINickname.Contains(search)||t.UIRealName.Contains(search)||t.UIHeadPortrait.Contains(search)||t.UIDepartment.Contains(search)||t.UIPost.Contains(search)||t.UIBooth.Contains(search)||t.UIPhoto.Contains(search)||t.UICustomerType.Contains(search)||t.UIUserLevel.Contains(search)||t.UICode.Contains(search));
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
                    message = "参数错误"
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
                var query = ctx.LogonRecord.Where(t=>t.IsDeleted==0);
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
                    message = "参数错误"
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
                var query = ctx.UserMenu.Where(t=>t.IsDeleted==0);
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
                    message = "参数错误"
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
                var query = ctx.SystemConfiguration.Where(t=>t.IsDeleted==0);
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

}
