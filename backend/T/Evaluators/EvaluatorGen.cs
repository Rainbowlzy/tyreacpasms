
/* ------------------------------------------------------------ *
 * 此文件由生成器引擎根据既有规则生成，所有手工的更改将会被覆盖
 * 生成时间：11/14/2018 19:13:16
 * 生成版本：11/14/2018 16:32:59 
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
                var search = @params["search"] ?? searchModel.SearchKey;
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
					query = query.Where(t=>t.CNameOfGoods.Contains(search));
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
    /// 【采购单】7次查询分别得到七天内记录的条数，形成柱状图，折线图
    /// </summary>
    public partial class PurchaseUnitPriceCountEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
                var now = Parse(request.context.Request.Params["date"] ?? Now.ToString("yyyy-MM-dd"));
                var dates = Range(1, 7).Select(p => now.AddDays(-1 * p)).ToList();
                var list = dates.Select(p => ctx.PurchaseUnitPrice.Count(m => m.CreateOn==p)).ToList();
                return new
                {
                    sum = list.Sum(),
                    xaxis = dates.Select(p => p.ToString("yyyy-MM-dd")).ToList(),
                    series = list
                };
            }
        }
        public override string Comments=> "【采购单】7次查询分别得到七天内记录的条数，形成柱状图，折线图";
    }
	public partial class TruncatePurchaseUnitPriceEvaluator : Evaluator
	{
        protected override object Evaluate(CommonRequest request)
		{
            using (var ctx = new DefaultContext())
			{
                ctx.PurchaseUnitPrice.RemoveRange(ctx.PurchaseUnitPrice);
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
    /// 删除【采购单】
    /// </summary>
    public partial class DeletePurchaseUnitPriceEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            var data = JsonConvert.DeserializeObject<PurchaseUnitPrice>(HttpUtility.UrlDecode(request.data));
            if (data == null)
                return new CommonOutputT<string>
                {
                    success = false,
                    message = "参数错误"
                };
            using (var ctx = new DefaultContext())
            {
                var one = ctx.PurchaseUnitPrice.Find(data.id);
				if(one==null){
					return new CommonOutputT<string>
					{
						success = false,
						message = "未找到需要删除的数据"
					};
				}
                one.IsDeleted = 1;
				ctx.PurchaseUnitPrice.AddOrUpdate(one);
				ctx.SaveChanges();
				return new CommonOutputT<string>
				{
					success = true,
					message = "删除成功"
				};
            }
			
        }
        public override string Comments=> "删除一条采购单记录";
    }
	
    /// <summary>
    /// 保存【采购单】
    /// </summary>
    public partial class SavePurchaseUnitPriceEvaluator : Evaluator
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
			PurchaseUnitPrice entity = null;
			try
			{
				entity = JsonConvert.DeserializeObject<PurchaseUnitPrice>(HttpUtility.UrlDecode(s));
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
                    var one = ctx.PurchaseUnitPrice.FirstOrDefault(p=>p.id==entity.id);
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
					ctx.PurchaseUnitPrice.AddOrUpdate(one);
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
				entity.PUPAmount = HttpUtility.UrlDecode(entity.PUPAmount);
					// NVARCHAR(50) 价格
				entity.PUPPrice = HttpUtility.UrlDecode(entity.PUPPrice);
					// NVARCHAR(50) 备注
				entity.PUPRemarks = HttpUtility.UrlDecode(entity.PUPRemarks);
	
                entity.CreateBy = entity.CreateBy ?? user?.UILoginName ?? "未登录用户";
                entity.UpdateBy = user?.UILoginName ?? "未登录用户";
                entity.CreateOn = entity.CreateOn ?? Now;
                entity.TransactionID = transactionId;
                entity.UpdateOn = Now;
                entity.IsDeleted = 0;
	            entity.VersionNo = entity.VersionNo ?? 0;
                entity.DataLevel = entity.DataLevel ?? user?.DataLevel ?? "019999";
				ctx.PurchaseUnitPrice.AddOrUpdate(entity);
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
        public override string Comments=> "保存一条PurchaseUnitPrice记录";
    }
	
    /// <summary>
    /// 查询空的【采购单】
    /// </summary>
    public partial class GetPurchaseUnitPriceEmptyEvaluator:Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            return new PurchaseUnitPrice();
        }
        public override string Comments=> "获取空的采购单记录";
    }
	
    /// <summary>
    /// 查询【采购单】列表
    /// </summary>
    public partial class GetPurchaseUnitPriceListEvaluator : Evaluator
    {
        public override string Comments=> "获取PurchaseUnitPrice列表 ";
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
				var datalevel = CurrentUserInformation?.DataLevel;
                var searchModel = HttpUtility.UrlDecode(request.data).Deserialize<PurchaseUnitPriceSearchModel>() ?? new PurchaseUnitPriceSearchModel();
                var query = ctx.PurchaseUnitPrice.Where(t=>t.IsDeleted==0);
                var @params = request.context.Request.Params;
                searchModel.PageSize = (@params["limit"] ?? searchModel.PageSize.ToString()).ToInt();
				searchModel.PageSize = searchModel.PageSize==0?4000:searchModel.PageSize;
                searchModel.PageIndex = (@params["offset"]).ToInt()/ searchModel.PageSize;
                var isordered = false;
                var search = @params["search"] ?? searchModel.SearchKey;
                var sort = searchModel.Sort ?? @params["sort"];
				var order = @params["order"];
				// PUPSupplierNumber INT 供应商编号 
                if(searchModel.MinPUPSupplierNumber!=null) query = query.Where(t=>t.PUPSupplierNumber>=searchModel.MinPUPSupplierNumber);
                if(searchModel.MaxPUPSupplierNumber!=null) query = query.Where(t=>t.PUPSupplierNumber<=searchModel.MaxPUPSupplierNumber);
                if(sort=="PUPSupplierNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.PUPSupplierNumber):query.OrderByDescending(t=>t.PUPSupplierNumber);
                    isordered = true;
                }
				// PUPCargoNumber INT 货物编号 
                if(searchModel.MinPUPCargoNumber!=null) query = query.Where(t=>t.PUPCargoNumber>=searchModel.MinPUPCargoNumber);
                if(searchModel.MaxPUPCargoNumber!=null) query = query.Where(t=>t.PUPCargoNumber<=searchModel.MaxPUPCargoNumber);
                if(sort=="PUPCargoNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.PUPCargoNumber):query.OrderByDescending(t=>t.PUPCargoNumber);
                    isordered = true;
                }
				// PUPPurchasingStaffNumber INT 采购员工号 
                if(searchModel.MinPUPPurchasingStaffNumber!=null) query = query.Where(t=>t.PUPPurchasingStaffNumber>=searchModel.MinPUPPurchasingStaffNumber);
                if(searchModel.MaxPUPPurchasingStaffNumber!=null) query = query.Where(t=>t.PUPPurchasingStaffNumber<=searchModel.MaxPUPPurchasingStaffNumber);
                if(sort=="PUPPurchasingStaffNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.PUPPurchasingStaffNumber):query.OrderByDescending(t=>t.PUPPurchasingStaffNumber);
                    isordered = true;
                }
				// PUPDate DATETIME 日期 
                if(searchModel.FromPUPDate!=null) query = query.Where(t=>t.PUPDate>=searchModel.FromPUPDate);
                if(searchModel.ToPUPDate!=null) query = query.Where(t=>t.PUPDate<=searchModel.ToPUPDate);
                if(sort=="PUPDate")
                {
					query = order=="asc"?query.OrderBy(t=>t.PUPDate):query.OrderByDescending(t=>t.PUPDate);
                    isordered = true;
                }
				// PUPAmount NVARCHAR(50) 数量 
                if(!string.IsNullOrEmpty(searchModel.PUPAmount)) query = query.Where(t=>t.PUPAmount.Contains(searchModel.PUPAmount));
                if(sort=="PUPAmount")
                {
					query = order=="asc"?query.OrderBy(t=>t.PUPAmount):query.OrderByDescending(t=>t.PUPAmount);
                    isordered = true;
                }
				// PUPPrice NVARCHAR(50) 价格 
                if(!string.IsNullOrEmpty(searchModel.PUPPrice)) query = query.Where(t=>t.PUPPrice.Contains(searchModel.PUPPrice));
                if(sort=="PUPPrice")
                {
					query = order=="asc"?query.OrderBy(t=>t.PUPPrice):query.OrderByDescending(t=>t.PUPPrice);
                    isordered = true;
                }
				// PUPRemarks NVARCHAR(50) 备注 
                if(!string.IsNullOrEmpty(searchModel.PUPRemarks)) query = query.Where(t=>t.PUPRemarks.Contains(searchModel.PUPRemarks));
                if(sort=="PUPRemarks")
                {
					query = order=="asc"?query.OrderBy(t=>t.PUPRemarks):query.OrderByDescending(t=>t.PUPRemarks);
                    isordered = true;
                }
				if(!string.IsNullOrEmpty(search)){
					query = query.Where(t=>t.PUPAmount.Contains(search)||t.PUPPrice.Contains(search)||t.PUPRemarks.Contains(search));
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
                return new CommonOutputList<PurchaseUnitPrice>
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
                var search = @params["search"] ?? searchModel.SearchKey;
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
					query = query.Where(t=>t.SName.Contains(search)||t.SEducation.Contains(search)||t.SCommonModeOfContact.Contains(search));
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
                var search = @params["search"] ?? searchModel.SearchKey;
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
					query = query.Where(t=>t.CName.Contains(search)||t.CCommonModeOfContact.Contains(search));
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
                var search = @params["search"] ?? searchModel.SearchKey;
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
				// GSCapacity INT 容量 
                if(searchModel.MinGSCapacity!=null) query = query.Where(t=>t.GSCapacity>=searchModel.MinGSCapacity);
                if(searchModel.MaxGSCapacity!=null) query = query.Where(t=>t.GSCapacity<=searchModel.MaxGSCapacity);
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
					query = query.Where(t=>t.GSLocality.Contains(search));
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
                var search = @params["search"] ?? searchModel.SearchKey;
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
				// WCapacity INT 容量 
                if(searchModel.MinWCapacity!=null) query = query.Where(t=>t.WCapacity>=searchModel.MinWCapacity);
                if(searchModel.MaxWCapacity!=null) query = query.Where(t=>t.WCapacity<=searchModel.MaxWCapacity);
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
					query = query.Where(t=>t.WLocality.Contains(search));
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
                var search = @params["search"] ?? searchModel.SearchKey;
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
					query = query.Where(t=>t.SSupplierName.Contains(search)||t.SCommonModeOfContact.Contains(search)||t.SOfficeLocation.Contains(search));
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
    /// 【销售单】7次查询分别得到七天内记录的条数，形成柱状图，折线图
    /// </summary>
    public partial class SalesUnitPriceCountEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
                var now = Parse(request.context.Request.Params["date"] ?? Now.ToString("yyyy-MM-dd"));
                var dates = Range(1, 7).Select(p => now.AddDays(-1 * p)).ToList();
                var list = dates.Select(p => ctx.SalesUnitPrice.Count(m => m.CreateOn==p)).ToList();
                return new
                {
                    sum = list.Sum(),
                    xaxis = dates.Select(p => p.ToString("yyyy-MM-dd")).ToList(),
                    series = list
                };
            }
        }
        public override string Comments=> "【销售单】7次查询分别得到七天内记录的条数，形成柱状图，折线图";
    }
	public partial class TruncateSalesUnitPriceEvaluator : Evaluator
	{
        protected override object Evaluate(CommonRequest request)
		{
            using (var ctx = new DefaultContext())
			{
                ctx.SalesUnitPrice.RemoveRange(ctx.SalesUnitPrice);
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
    /// 删除【销售单】
    /// </summary>
    public partial class DeleteSalesUnitPriceEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            var data = JsonConvert.DeserializeObject<SalesUnitPrice>(HttpUtility.UrlDecode(request.data));
            if (data == null)
                return new CommonOutputT<string>
                {
                    success = false,
                    message = "参数错误"
                };
            using (var ctx = new DefaultContext())
            {
                var one = ctx.SalesUnitPrice.Find(data.id);
				if(one==null){
					return new CommonOutputT<string>
					{
						success = false,
						message = "未找到需要删除的数据"
					};
				}
                one.IsDeleted = 1;
				ctx.SalesUnitPrice.AddOrUpdate(one);
				ctx.SaveChanges();
				return new CommonOutputT<string>
				{
					success = true,
					message = "删除成功"
				};
            }
			
        }
        public override string Comments=> "删除一条销售单记录";
    }
	
    /// <summary>
    /// 保存【销售单】
    /// </summary>
    public partial class SaveSalesUnitPriceEvaluator : Evaluator
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
			SalesUnitPrice entity = null;
			try
			{
				entity = JsonConvert.DeserializeObject<SalesUnitPrice>(HttpUtility.UrlDecode(s));
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
                    var one = ctx.SalesUnitPrice.FirstOrDefault(p=>p.id==entity.id);
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
					ctx.SalesUnitPrice.AddOrUpdate(one);
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
				entity.SUPAmount = HttpUtility.UrlDecode(entity.SUPAmount);
					// NVARCHAR(50) 价格
				entity.SUPPrice = HttpUtility.UrlDecode(entity.SUPPrice);
					// NVARCHAR(50) 备注
				entity.SUPRemarks = HttpUtility.UrlDecode(entity.SUPRemarks);
	
                entity.CreateBy = entity.CreateBy ?? user?.UILoginName ?? "未登录用户";
                entity.UpdateBy = user?.UILoginName ?? "未登录用户";
                entity.CreateOn = entity.CreateOn ?? Now;
                entity.TransactionID = transactionId;
                entity.UpdateOn = Now;
                entity.IsDeleted = 0;
	            entity.VersionNo = entity.VersionNo ?? 0;
                entity.DataLevel = entity.DataLevel ?? user?.DataLevel ?? "019999";
				ctx.SalesUnitPrice.AddOrUpdate(entity);
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
        public override string Comments=> "保存一条SalesUnitPrice记录";
    }
	
    /// <summary>
    /// 查询空的【销售单】
    /// </summary>
    public partial class GetSalesUnitPriceEmptyEvaluator:Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            return new SalesUnitPrice();
        }
        public override string Comments=> "获取空的销售单记录";
    }
	
    /// <summary>
    /// 查询【销售单】列表
    /// </summary>
    public partial class GetSalesUnitPriceListEvaluator : Evaluator
    {
        public override string Comments=> "获取SalesUnitPrice列表 ";
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
				var datalevel = CurrentUserInformation?.DataLevel;
                var searchModel = HttpUtility.UrlDecode(request.data).Deserialize<SalesUnitPriceSearchModel>() ?? new SalesUnitPriceSearchModel();
                var query = ctx.SalesUnitPrice.Where(t=>t.IsDeleted==0);
                var @params = request.context.Request.Params;
                searchModel.PageSize = (@params["limit"] ?? searchModel.PageSize.ToString()).ToInt();
				searchModel.PageSize = searchModel.PageSize==0?4000:searchModel.PageSize;
                searchModel.PageIndex = (@params["offset"]).ToInt()/ searchModel.PageSize;
                var isordered = false;
                var search = @params["search"] ?? searchModel.SearchKey;
                var sort = searchModel.Sort ?? @params["sort"];
				var order = @params["order"];
				// SUPCargoNumber INT 货物编号 
                if(searchModel.MinSUPCargoNumber!=null) query = query.Where(t=>t.SUPCargoNumber>=searchModel.MinSUPCargoNumber);
                if(searchModel.MaxSUPCargoNumber!=null) query = query.Where(t=>t.SUPCargoNumber<=searchModel.MaxSUPCargoNumber);
                if(sort=="SUPCargoNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.SUPCargoNumber):query.OrderByDescending(t=>t.SUPCargoNumber);
                    isordered = true;
                }
				// SUPCustomerNumber INT 客户编号 
                if(searchModel.MinSUPCustomerNumber!=null) query = query.Where(t=>t.SUPCustomerNumber>=searchModel.MinSUPCustomerNumber);
                if(searchModel.MaxSUPCustomerNumber!=null) query = query.Where(t=>t.SUPCustomerNumber<=searchModel.MaxSUPCustomerNumber);
                if(sort=="SUPCustomerNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.SUPCustomerNumber):query.OrderByDescending(t=>t.SUPCustomerNumber);
                    isordered = true;
                }
				// SUPSalesStaffNumber INT 销售员工号 
                if(searchModel.MinSUPSalesStaffNumber!=null) query = query.Where(t=>t.SUPSalesStaffNumber>=searchModel.MinSUPSalesStaffNumber);
                if(searchModel.MaxSUPSalesStaffNumber!=null) query = query.Where(t=>t.SUPSalesStaffNumber<=searchModel.MaxSUPSalesStaffNumber);
                if(sort=="SUPSalesStaffNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.SUPSalesStaffNumber):query.OrderByDescending(t=>t.SUPSalesStaffNumber);
                    isordered = true;
                }
				// SUPDate DATETIME 日期 
                if(searchModel.FromSUPDate!=null) query = query.Where(t=>t.SUPDate>=searchModel.FromSUPDate);
                if(searchModel.ToSUPDate!=null) query = query.Where(t=>t.SUPDate<=searchModel.ToSUPDate);
                if(sort=="SUPDate")
                {
					query = order=="asc"?query.OrderBy(t=>t.SUPDate):query.OrderByDescending(t=>t.SUPDate);
                    isordered = true;
                }
				// SUPAmount NVARCHAR(50) 数量 
                if(!string.IsNullOrEmpty(searchModel.SUPAmount)) query = query.Where(t=>t.SUPAmount.Contains(searchModel.SUPAmount));
                if(sort=="SUPAmount")
                {
					query = order=="asc"?query.OrderBy(t=>t.SUPAmount):query.OrderByDescending(t=>t.SUPAmount);
                    isordered = true;
                }
				// SUPPrice NVARCHAR(50) 价格 
                if(!string.IsNullOrEmpty(searchModel.SUPPrice)) query = query.Where(t=>t.SUPPrice.Contains(searchModel.SUPPrice));
                if(sort=="SUPPrice")
                {
					query = order=="asc"?query.OrderBy(t=>t.SUPPrice):query.OrderByDescending(t=>t.SUPPrice);
                    isordered = true;
                }
				// SUPRemarks NVARCHAR(50) 备注 
                if(!string.IsNullOrEmpty(searchModel.SUPRemarks)) query = query.Where(t=>t.SUPRemarks.Contains(searchModel.SUPRemarks));
                if(sort=="SUPRemarks")
                {
					query = order=="asc"?query.OrderBy(t=>t.SUPRemarks):query.OrderByDescending(t=>t.SUPRemarks);
                    isordered = true;
                }
				if(!string.IsNullOrEmpty(search)){
					query = query.Where(t=>t.SUPAmount.Contains(search)||t.SUPPrice.Contains(search)||t.SUPRemarks.Contains(search));
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
                return new CommonOutputList<SalesUnitPrice>
                {
                    success = true, rows = rows, total = total, message="查询成功"
                };
            }
        }
    }


    /// <summary>
    /// 【供货单】7次查询分别得到七天内记录的条数，形成柱状图，折线图
    /// </summary>
    public partial class SupplyListCountEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
                var now = Parse(request.context.Request.Params["date"] ?? Now.ToString("yyyy-MM-dd"));
                var dates = Range(1, 7).Select(p => now.AddDays(-1 * p)).ToList();
                var list = dates.Select(p => ctx.SupplyList.Count(m => m.CreateOn==p)).ToList();
                return new
                {
                    sum = list.Sum(),
                    xaxis = dates.Select(p => p.ToString("yyyy-MM-dd")).ToList(),
                    series = list
                };
            }
        }
        public override string Comments=> "【供货单】7次查询分别得到七天内记录的条数，形成柱状图，折线图";
    }
	public partial class TruncateSupplyListEvaluator : Evaluator
	{
        protected override object Evaluate(CommonRequest request)
		{
            using (var ctx = new DefaultContext())
			{
                ctx.SupplyList.RemoveRange(ctx.SupplyList);
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
    /// 删除【供货单】
    /// </summary>
    public partial class DeleteSupplyListEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            var data = JsonConvert.DeserializeObject<SupplyList>(HttpUtility.UrlDecode(request.data));
            if (data == null)
                return new CommonOutputT<string>
                {
                    success = false,
                    message = "参数错误"
                };
            using (var ctx = new DefaultContext())
            {
                var one = ctx.SupplyList.Find(data.id);
				if(one==null){
					return new CommonOutputT<string>
					{
						success = false,
						message = "未找到需要删除的数据"
					};
				}
                one.IsDeleted = 1;
				ctx.SupplyList.AddOrUpdate(one);
				ctx.SaveChanges();
				return new CommonOutputT<string>
				{
					success = true,
					message = "删除成功"
				};
            }
			
        }
        public override string Comments=> "删除一条供货单记录";
    }
	
    /// <summary>
    /// 保存【供货单】
    /// </summary>
    public partial class SaveSupplyListEvaluator : Evaluator
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
			SupplyList entity = null;
			try
			{
				entity = JsonConvert.DeserializeObject<SupplyList>(HttpUtility.UrlDecode(s));
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
                    var one = ctx.SupplyList.FirstOrDefault(p=>p.id==entity.id);
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
					ctx.SupplyList.AddOrUpdate(one);
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
				entity.SLAmount = HttpUtility.UrlDecode(entity.SLAmount);
					// NVARCHAR(50) 备注
				entity.SLRemarks = HttpUtility.UrlDecode(entity.SLRemarks);
	
                entity.CreateBy = entity.CreateBy ?? user?.UILoginName ?? "未登录用户";
                entity.UpdateBy = user?.UILoginName ?? "未登录用户";
                entity.CreateOn = entity.CreateOn ?? Now;
                entity.TransactionID = transactionId;
                entity.UpdateOn = Now;
                entity.IsDeleted = 0;
	            entity.VersionNo = entity.VersionNo ?? 0;
                entity.DataLevel = entity.DataLevel ?? user?.DataLevel ?? "019999";
				ctx.SupplyList.AddOrUpdate(entity);
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
        public override string Comments=> "保存一条SupplyList记录";
    }
	
    /// <summary>
    /// 查询空的【供货单】
    /// </summary>
    public partial class GetSupplyListEmptyEvaluator:Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            return new SupplyList();
        }
        public override string Comments=> "获取空的供货单记录";
    }
	
    /// <summary>
    /// 查询【供货单】列表
    /// </summary>
    public partial class GetSupplyListListEvaluator : Evaluator
    {
        public override string Comments=> "获取SupplyList列表 ";
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
				var datalevel = CurrentUserInformation?.DataLevel;
                var searchModel = HttpUtility.UrlDecode(request.data).Deserialize<SupplyListSearchModel>() ?? new SupplyListSearchModel();
                var query = ctx.SupplyList.Where(t=>t.IsDeleted==0);
                var @params = request.context.Request.Params;
                searchModel.PageSize = (@params["limit"] ?? searchModel.PageSize.ToString()).ToInt();
				searchModel.PageSize = searchModel.PageSize==0?4000:searchModel.PageSize;
                searchModel.PageIndex = (@params["offset"]).ToInt()/ searchModel.PageSize;
                var isordered = false;
                var search = @params["search"] ?? searchModel.SearchKey;
                var sort = searchModel.Sort ?? @params["sort"];
				var order = @params["order"];
				// SLWarehouseNumber INT 仓库编号 
                if(searchModel.MinSLWarehouseNumber!=null) query = query.Where(t=>t.SLWarehouseNumber>=searchModel.MinSLWarehouseNumber);
                if(searchModel.MaxSLWarehouseNumber!=null) query = query.Where(t=>t.SLWarehouseNumber<=searchModel.MaxSLWarehouseNumber);
                if(sort=="SLWarehouseNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.SLWarehouseNumber):query.OrderByDescending(t=>t.SLWarehouseNumber);
                    isordered = true;
                }
				// SLCargoNumber INT 货物编号 
                if(searchModel.MinSLCargoNumber!=null) query = query.Where(t=>t.SLCargoNumber>=searchModel.MinSLCargoNumber);
                if(searchModel.MaxSLCargoNumber!=null) query = query.Where(t=>t.SLCargoNumber<=searchModel.MaxSLCargoNumber);
                if(sort=="SLCargoNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.SLCargoNumber):query.OrderByDescending(t=>t.SLCargoNumber);
                    isordered = true;
                }
				// SLWarehouseManagementStaffNumber INT 仓库管理员工号 
                if(searchModel.MinSLWarehouseManagementStaffNumber!=null) query = query.Where(t=>t.SLWarehouseManagementStaffNumber>=searchModel.MinSLWarehouseManagementStaffNumber);
                if(searchModel.MaxSLWarehouseManagementStaffNumber!=null) query = query.Where(t=>t.SLWarehouseManagementStaffNumber<=searchModel.MaxSLWarehouseManagementStaffNumber);
                if(sort=="SLWarehouseManagementStaffNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.SLWarehouseManagementStaffNumber):query.OrderByDescending(t=>t.SLWarehouseManagementStaffNumber);
                    isordered = true;
                }
				// SLDate DATETIME 日期 
                if(searchModel.FromSLDate!=null) query = query.Where(t=>t.SLDate>=searchModel.FromSLDate);
                if(searchModel.ToSLDate!=null) query = query.Where(t=>t.SLDate<=searchModel.ToSLDate);
                if(sort=="SLDate")
                {
					query = order=="asc"?query.OrderBy(t=>t.SLDate):query.OrderByDescending(t=>t.SLDate);
                    isordered = true;
                }
				// SLAmount NVARCHAR(50) 数量 
                if(!string.IsNullOrEmpty(searchModel.SLAmount)) query = query.Where(t=>t.SLAmount.Contains(searchModel.SLAmount));
                if(sort=="SLAmount")
                {
					query = order=="asc"?query.OrderBy(t=>t.SLAmount):query.OrderByDescending(t=>t.SLAmount);
                    isordered = true;
                }
				// SLRemarks NVARCHAR(50) 备注 
                if(!string.IsNullOrEmpty(searchModel.SLRemarks)) query = query.Where(t=>t.SLRemarks.Contains(searchModel.SLRemarks));
                if(sort=="SLRemarks")
                {
					query = order=="asc"?query.OrderBy(t=>t.SLRemarks):query.OrderByDescending(t=>t.SLRemarks);
                    isordered = true;
                }
				if(!string.IsNullOrEmpty(search)){
					query = query.Where(t=>t.SLAmount.Contains(search)||t.SLRemarks.Contains(search));
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
                return new CommonOutputList<SupplyList>
                {
                    success = true, rows = rows, total = total, message="查询成功"
                };
            }
        }
    }


    /// <summary>
    /// 【补货单】7次查询分别得到七天内记录的条数，形成柱状图，折线图
    /// </summary>
    public partial class ReplenishmentBillCountEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
                var now = Parse(request.context.Request.Params["date"] ?? Now.ToString("yyyy-MM-dd"));
                var dates = Range(1, 7).Select(p => now.AddDays(-1 * p)).ToList();
                var list = dates.Select(p => ctx.ReplenishmentBill.Count(m => m.CreateOn==p)).ToList();
                return new
                {
                    sum = list.Sum(),
                    xaxis = dates.Select(p => p.ToString("yyyy-MM-dd")).ToList(),
                    series = list
                };
            }
        }
        public override string Comments=> "【补货单】7次查询分别得到七天内记录的条数，形成柱状图，折线图";
    }
	public partial class TruncateReplenishmentBillEvaluator : Evaluator
	{
        protected override object Evaluate(CommonRequest request)
		{
            using (var ctx = new DefaultContext())
			{
                ctx.ReplenishmentBill.RemoveRange(ctx.ReplenishmentBill);
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
    /// 删除【补货单】
    /// </summary>
    public partial class DeleteReplenishmentBillEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            var data = JsonConvert.DeserializeObject<ReplenishmentBill>(HttpUtility.UrlDecode(request.data));
            if (data == null)
                return new CommonOutputT<string>
                {
                    success = false,
                    message = "参数错误"
                };
            using (var ctx = new DefaultContext())
            {
                var one = ctx.ReplenishmentBill.Find(data.id);
				if(one==null){
					return new CommonOutputT<string>
					{
						success = false,
						message = "未找到需要删除的数据"
					};
				}
                one.IsDeleted = 1;
				ctx.ReplenishmentBill.AddOrUpdate(one);
				ctx.SaveChanges();
				return new CommonOutputT<string>
				{
					success = true,
					message = "删除成功"
				};
            }
			
        }
        public override string Comments=> "删除一条补货单记录";
    }
	
    /// <summary>
    /// 保存【补货单】
    /// </summary>
    public partial class SaveReplenishmentBillEvaluator : Evaluator
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
			ReplenishmentBill entity = null;
			try
			{
				entity = JsonConvert.DeserializeObject<ReplenishmentBill>(HttpUtility.UrlDecode(s));
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
                    var one = ctx.ReplenishmentBill.FirstOrDefault(p=>p.id==entity.id);
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
					ctx.ReplenishmentBill.AddOrUpdate(one);
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
				entity.RBAmount = HttpUtility.UrlDecode(entity.RBAmount);
					// NVARCHAR(50) 备注
				entity.RBRemarks = HttpUtility.UrlDecode(entity.RBRemarks);
	
                entity.CreateBy = entity.CreateBy ?? user?.UILoginName ?? "未登录用户";
                entity.UpdateBy = user?.UILoginName ?? "未登录用户";
                entity.CreateOn = entity.CreateOn ?? Now;
                entity.TransactionID = transactionId;
                entity.UpdateOn = Now;
                entity.IsDeleted = 0;
	            entity.VersionNo = entity.VersionNo ?? 0;
                entity.DataLevel = entity.DataLevel ?? user?.DataLevel ?? "019999";
				ctx.ReplenishmentBill.AddOrUpdate(entity);
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
        public override string Comments=> "保存一条ReplenishmentBill记录";
    }
	
    /// <summary>
    /// 查询空的【补货单】
    /// </summary>
    public partial class GetReplenishmentBillEmptyEvaluator:Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            return new ReplenishmentBill();
        }
        public override string Comments=> "获取空的补货单记录";
    }
	
    /// <summary>
    /// 查询【补货单】列表
    /// </summary>
    public partial class GetReplenishmentBillListEvaluator : Evaluator
    {
        public override string Comments=> "获取ReplenishmentBill列表 ";
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
				var datalevel = CurrentUserInformation?.DataLevel;
                var searchModel = HttpUtility.UrlDecode(request.data).Deserialize<ReplenishmentBillSearchModel>() ?? new ReplenishmentBillSearchModel();
                var query = ctx.ReplenishmentBill.Where(t=>t.IsDeleted==0);
                var @params = request.context.Request.Params;
                searchModel.PageSize = (@params["limit"] ?? searchModel.PageSize.ToString()).ToInt();
				searchModel.PageSize = searchModel.PageSize==0?4000:searchModel.PageSize;
                searchModel.PageIndex = (@params["offset"]).ToInt()/ searchModel.PageSize;
                var isordered = false;
                var search = @params["search"] ?? searchModel.SearchKey;
                var sort = searchModel.Sort ?? @params["sort"];
				var order = @params["order"];
				// RBShelfNumber INT 货架编号 
                if(searchModel.MinRBShelfNumber!=null) query = query.Where(t=>t.RBShelfNumber>=searchModel.MinRBShelfNumber);
                if(searchModel.MaxRBShelfNumber!=null) query = query.Where(t=>t.RBShelfNumber<=searchModel.MaxRBShelfNumber);
                if(sort=="RBShelfNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.RBShelfNumber):query.OrderByDescending(t=>t.RBShelfNumber);
                    isordered = true;
                }
				// RBCargoNumber INT 货物编号 
                if(searchModel.MinRBCargoNumber!=null) query = query.Where(t=>t.RBCargoNumber>=searchModel.MinRBCargoNumber);
                if(searchModel.MaxRBCargoNumber!=null) query = query.Where(t=>t.RBCargoNumber<=searchModel.MaxRBCargoNumber);
                if(sort=="RBCargoNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.RBCargoNumber):query.OrderByDescending(t=>t.RBCargoNumber);
                    isordered = true;
                }
				// RBWarehouseManagementStaffNumber INT 仓库管理员工号 
                if(searchModel.MinRBWarehouseManagementStaffNumber!=null) query = query.Where(t=>t.RBWarehouseManagementStaffNumber>=searchModel.MinRBWarehouseManagementStaffNumber);
                if(searchModel.MaxRBWarehouseManagementStaffNumber!=null) query = query.Where(t=>t.RBWarehouseManagementStaffNumber<=searchModel.MaxRBWarehouseManagementStaffNumber);
                if(sort=="RBWarehouseManagementStaffNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.RBWarehouseManagementStaffNumber):query.OrderByDescending(t=>t.RBWarehouseManagementStaffNumber);
                    isordered = true;
                }
				// RBDate DATETIME 日期 
                if(searchModel.FromRBDate!=null) query = query.Where(t=>t.RBDate>=searchModel.FromRBDate);
                if(searchModel.ToRBDate!=null) query = query.Where(t=>t.RBDate<=searchModel.ToRBDate);
                if(sort=="RBDate")
                {
					query = order=="asc"?query.OrderBy(t=>t.RBDate):query.OrderByDescending(t=>t.RBDate);
                    isordered = true;
                }
				// RBAmount NVARCHAR(50) 数量 
                if(!string.IsNullOrEmpty(searchModel.RBAmount)) query = query.Where(t=>t.RBAmount.Contains(searchModel.RBAmount));
                if(sort=="RBAmount")
                {
					query = order=="asc"?query.OrderBy(t=>t.RBAmount):query.OrderByDescending(t=>t.RBAmount);
                    isordered = true;
                }
				// RBRemarks NVARCHAR(50) 备注 
                if(!string.IsNullOrEmpty(searchModel.RBRemarks)) query = query.Where(t=>t.RBRemarks.Contains(searchModel.RBRemarks));
                if(sort=="RBRemarks")
                {
					query = order=="asc"?query.OrderBy(t=>t.RBRemarks):query.OrderByDescending(t=>t.RBRemarks);
                    isordered = true;
                }
				if(!string.IsNullOrEmpty(search)){
					query = query.Where(t=>t.RBAmount.Contains(search)||t.RBRemarks.Contains(search));
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
                return new CommonOutputList<ReplenishmentBill>
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
                var search = @params["search"] ?? searchModel.SearchKey;
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
					query = query.Where(t=>t.MCCaption.Contains(search)||t.MCParentTitle.Contains(search)||t.MCLink.Contains(search)||t.MCMenuType.Contains(search)||t.MCDisplayName.Contains(search)||t.MCPicture.Contains(search));
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
                var search = @params["search"] ?? searchModel.SearchKey;
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
					query = query.Where(t=>t.RMRoleName.Contains(search)||t.RMMenuTitle.Contains(search));
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
                var search = @params["search"] ?? searchModel.SearchKey;
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
					query = query.Where(t=>t.URRoleName.Contains(search)||t.URLoginName.Contains(search));
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
                var search = @params["search"] ?? searchModel.SearchKey;
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
					query = query.Where(t=>t.RCRoleName.Contains(search)||t.RCAffiliatedOrganization.Contains(search));
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
                var search = @params["search"] ?? searchModel.SearchKey;
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
					query = query.Where(t=>t.UILoginName.Contains(search)||t.UINickname.Contains(search)||t.UIRealName.Contains(search)||t.UIHeadPortrait.Contains(search)||t.UIDepartment.Contains(search)||t.UIPost.Contains(search)||t.UIBooth.Contains(search)||t.UIPhoto.Contains(search)||t.UICustomerType.Contains(search)||t.UIUserLevel.Contains(search)||t.UICode.Contains(search));
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
                var search = @params["search"] ?? searchModel.SearchKey;
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
					query = query.Where(t=>t.LRLoginName.Contains(search));
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
                var search = @params["search"] ?? searchModel.SearchKey;
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
					query = query.Where(t=>t.UMLoginName.Contains(search)||t.UMCaption.Contains(search));
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
                var search = @params["search"] ?? searchModel.SearchKey;
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
					query = query.Where(t=>t.SCKey.Contains(search)||t.SCAccrued.Contains(search));
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
