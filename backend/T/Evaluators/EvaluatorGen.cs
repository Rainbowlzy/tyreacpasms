
/* ------------------------------------------------------------ *
 * 此文件由生成器引擎根据既有规则生成，所有手工的更改将会被覆盖
 * 生成时间：11/17/2018 14:58:54
 * 生成版本：11/17/2018 14:58:50 
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
using TEntities.EF;

namespace T.Evaluators
{

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
					// NVARCHAR(50) 联系电话
				entity.CContactNumber = HttpUtility.UrlDecode(entity.CContactNumber);
					// NVARCHAR(50) 性别
				entity.CChairperson = HttpUtility.UrlDecode(entity.CChairperson);
					// NVARCHAR(50) 出生年月
				entity.CDateOfBirth = HttpUtility.UrlDecode(entity.CDateOfBirth);
					// NVARCHAR(50) 地址
				entity.CAddress = HttpUtility.UrlDecode(entity.CAddress);
					// NVARCHAR(50) 邮编
				entity.CZipCode = HttpUtility.UrlDecode(entity.CZipCode);
					// NVARCHAR(50) 备注
				entity.CRemarks = HttpUtility.UrlDecode(entity.CRemarks);
	
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
				// CContactNumber NVARCHAR(50) 联系电话 
                if(!string.IsNullOrEmpty(searchModel.CContactNumber)) query = query.Where(t=>t.CContactNumber.Contains(searchModel.CContactNumber));
                if(sort=="CContactNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.CContactNumber):query.OrderByDescending(t=>t.CContactNumber);
                    isordered = true;
                }
				// CChairperson NVARCHAR(50) 性别 
                if(!string.IsNullOrEmpty(searchModel.CChairperson)) query = query.Where(t=>t.CChairperson.Contains(searchModel.CChairperson));
                if(sort=="CChairperson")
                {
					query = order=="asc"?query.OrderBy(t=>t.CChairperson):query.OrderByDescending(t=>t.CChairperson);
                    isordered = true;
                }
				// CDateOfBirth NVARCHAR(50) 出生年月 
                if(!string.IsNullOrEmpty(searchModel.CDateOfBirth)) query = query.Where(t=>t.CDateOfBirth.Contains(searchModel.CDateOfBirth));
                if(sort=="CDateOfBirth")
                {
					query = order=="asc"?query.OrderBy(t=>t.CDateOfBirth):query.OrderByDescending(t=>t.CDateOfBirth);
                    isordered = true;
                }
				// CAddress NVARCHAR(50) 地址 
                if(!string.IsNullOrEmpty(searchModel.CAddress)) query = query.Where(t=>t.CAddress.Contains(searchModel.CAddress));
                if(sort=="CAddress")
                {
					query = order=="asc"?query.OrderBy(t=>t.CAddress):query.OrderByDescending(t=>t.CAddress);
                    isordered = true;
                }
				// CZipCode NVARCHAR(50) 邮编 
                if(!string.IsNullOrEmpty(searchModel.CZipCode)) query = query.Where(t=>t.CZipCode.Contains(searchModel.CZipCode));
                if(sort=="CZipCode")
                {
					query = order=="asc"?query.OrderBy(t=>t.CZipCode):query.OrderByDescending(t=>t.CZipCode);
                    isordered = true;
                }
				// CRemarks NVARCHAR(50) 备注 
                if(!string.IsNullOrEmpty(searchModel.CRemarks)) query = query.Where(t=>t.CRemarks.Contains(searchModel.CRemarks));
                if(sort=="CRemarks")
                {
					query = order=="asc"?query.OrderBy(t=>t.CRemarks):query.OrderByDescending(t=>t.CRemarks);
                    isordered = true;
                }
				if(!string.IsNullOrEmpty(search)){
					query = query.Where(t=>t.CName.Contains(search)||t.CContactNumber.Contains(search)||t.CChairperson.Contains(search)||t.CDateOfBirth.Contains(search)||t.CAddress.Contains(search)||t.CZipCode.Contains(search)||t.CRemarks.Contains(search));
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
					// NVARCHAR(50) 联系电话
				entity.SContactNumber = HttpUtility.UrlDecode(entity.SContactNumber);
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
				// SContactNumber NVARCHAR(50) 联系电话 
                if(!string.IsNullOrEmpty(searchModel.SContactNumber)) query = query.Where(t=>t.SContactNumber.Contains(searchModel.SContactNumber));
                if(sort=="SContactNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.SContactNumber):query.OrderByDescending(t=>t.SContactNumber);
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
					query = query.Where(t=>t.SSupplierName.Contains(search)||t.SContactNumber.Contains(search)||t.SOfficeLocation.Contains(search));
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
					// NVARCHAR(50) 型号
				entity.CModel = HttpUtility.UrlDecode(entity.CModel);
					// NVARCHAR(50) 有无配件
				entity.CHaveParts = HttpUtility.UrlDecode(entity.CHaveParts);
	
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
				// CModel NVARCHAR(50) 型号 
                if(!string.IsNullOrEmpty(searchModel.CModel)) query = query.Where(t=>t.CModel.Contains(searchModel.CModel));
                if(sort=="CModel")
                {
					query = order=="asc"?query.OrderBy(t=>t.CModel):query.OrderByDescending(t=>t.CModel);
                    isordered = true;
                }
				// CHaveParts NVARCHAR(50) 有无配件 
                if(!string.IsNullOrEmpty(searchModel.CHaveParts)) query = query.Where(t=>t.CHaveParts.Contains(searchModel.CHaveParts));
                if(sort=="CHaveParts")
                {
					query = order=="asc"?query.OrderBy(t=>t.CHaveParts):query.OrderByDescending(t=>t.CHaveParts);
                    isordered = true;
                }
				if(!string.IsNullOrEmpty(search)){
					query = query.Where(t=>t.CNameOfGoods.Contains(search)||t.CModel.Contains(search)||t.CHaveParts.Contains(search));
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
					// NVARCHAR(50) 联系电话
				entity.SContactNumber = HttpUtility.UrlDecode(entity.SContactNumber);
	
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
				// SContactNumber NVARCHAR(50) 联系电话 
                if(!string.IsNullOrEmpty(searchModel.SContactNumber)) query = query.Where(t=>t.SContactNumber.Contains(searchModel.SContactNumber));
                if(sort=="SContactNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.SContactNumber):query.OrderByDescending(t=>t.SContactNumber);
                    isordered = true;
                }
				if(!string.IsNullOrEmpty(search)){
					query = query.Where(t=>t.SName.Contains(search)||t.SEducation.Contains(search)||t.SContactNumber.Contains(search));
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
				// PUPAmount INT 数量 
                if(searchModel.MinPUPAmount!=null) query = query.Where(t=>t.PUPAmount>=searchModel.MinPUPAmount);
                if(searchModel.MaxPUPAmount!=null) query = query.Where(t=>t.PUPAmount<=searchModel.MaxPUPAmount);
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
					query = query.Where(t=>t.PUPPrice.Contains(search)||t.PUPRemarks.Contains(search));
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
				// SUPAmount INT 数量 
                if(searchModel.MinSUPAmount!=null) query = query.Where(t=>t.SUPAmount>=searchModel.MinSUPAmount);
                if(searchModel.MaxSUPAmount!=null) query = query.Where(t=>t.SUPAmount<=searchModel.MaxSUPAmount);
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
					query = query.Where(t=>t.SUPPrice.Contains(search)||t.SUPRemarks.Contains(search));
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
    /// 【供应入库单】7次查询分别得到七天内记录的条数，形成柱状图，折线图
    /// </summary>
    public partial class SupplyWarehousingListCountEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
                var now = Parse(request.context.Request.Params["date"] ?? Now.ToString("yyyy-MM-dd"));
                var dates = Range(1, 7).Select(p => now.AddDays(-1 * p)).ToList();
                var list = dates.Select(p => ctx.SupplyWarehousingList.Count(m => m.CreateOn==p)).ToList();
                return new
                {
                    sum = list.Sum(),
                    xaxis = dates.Select(p => p.ToString("yyyy-MM-dd")).ToList(),
                    series = list
                };
            }
        }
        public override string Comments=> "【供应入库单】7次查询分别得到七天内记录的条数，形成柱状图，折线图";
    }
	public partial class TruncateSupplyWarehousingListEvaluator : Evaluator
	{
        protected override object Evaluate(CommonRequest request)
		{
            using (var ctx = new DefaultContext())
			{
                ctx.SupplyWarehousingList.RemoveRange(ctx.SupplyWarehousingList);
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
    /// 删除【供应入库单】
    /// </summary>
    public partial class DeleteSupplyWarehousingListEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            var data = JsonConvert.DeserializeObject<SupplyWarehousingList>(HttpUtility.UrlDecode(request.data));
            if (data == null)
                return new CommonOutputT<string>
                {
                    success = false,
                    message = "参数错误"
                };
            using (var ctx = new DefaultContext())
            {
                var one = ctx.SupplyWarehousingList.Find(data.id);
				if(one==null){
					return new CommonOutputT<string>
					{
						success = false,
						message = "未找到需要删除的数据"
					};
				}
                one.IsDeleted = 1;
				ctx.SupplyWarehousingList.AddOrUpdate(one);
				ctx.SaveChanges();
				return new CommonOutputT<string>
				{
					success = true,
					message = "删除成功"
				};
            }
			
        }
        public override string Comments=> "删除一条供应入库单记录";
    }
	
    /// <summary>
    /// 保存【供应入库单】
    /// </summary>
    public partial class SaveSupplyWarehousingListEvaluator : Evaluator
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
			SupplyWarehousingList entity = null;
			try
			{
				entity = JsonConvert.DeserializeObject<SupplyWarehousingList>(HttpUtility.UrlDecode(s));
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
                    var one = ctx.SupplyWarehousingList.FirstOrDefault(p=>p.id==entity.id);
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
					ctx.SupplyWarehousingList.AddOrUpdate(one);
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
				

								// NVARCHAR(50) 备注
				entity.SWLRemarks = HttpUtility.UrlDecode(entity.SWLRemarks);
	
                entity.CreateBy = entity.CreateBy ?? user?.UILoginName ?? "未登录用户";
                entity.UpdateBy = user?.UILoginName ?? "未登录用户";
                entity.CreateOn = entity.CreateOn ?? Now;
                entity.TransactionID = transactionId;
                entity.UpdateOn = Now;
                entity.IsDeleted = 0;
	            entity.VersionNo = entity.VersionNo ?? 0;
                entity.DataLevel = entity.DataLevel ?? user?.DataLevel ?? "019999";
				ctx.SupplyWarehousingList.AddOrUpdate(entity);
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
        public override string Comments=> "保存一条SupplyWarehousingList记录";
    }
	
    /// <summary>
    /// 查询空的【供应入库单】
    /// </summary>
    public partial class GetSupplyWarehousingListEmptyEvaluator:Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            return new SupplyWarehousingList();
        }
        public override string Comments=> "获取空的供应入库单记录";
    }
	
    /// <summary>
    /// 查询【供应入库单】列表
    /// </summary>
    public partial class GetSupplyWarehousingListListEvaluator : Evaluator
    {
        public override string Comments=> "获取SupplyWarehousingList列表 ";
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
				var datalevel = CurrentUserInformation?.DataLevel;
                var searchModel = HttpUtility.UrlDecode(request.data).Deserialize<SupplyWarehousingListSearchModel>() ?? new SupplyWarehousingListSearchModel();
                var query = ctx.SupplyWarehousingList.Where(t=>t.IsDeleted==0);
                var @params = request.context.Request.Params;
                searchModel.PageSize = (@params["limit"] ?? searchModel.PageSize.ToString()).ToInt();
				searchModel.PageSize = searchModel.PageSize==0?4000:searchModel.PageSize;
                searchModel.PageIndex = (@params["offset"]).ToInt()/ searchModel.PageSize;
                var isordered = false;
                var search = @params["search"] ?? searchModel.SearchKey;
                var sort = searchModel.Sort ?? @params["sort"];
				var order = @params["order"];
				// SWLWarehouseNumber INT 仓库编号 
                if(searchModel.MinSWLWarehouseNumber!=null) query = query.Where(t=>t.SWLWarehouseNumber>=searchModel.MinSWLWarehouseNumber);
                if(searchModel.MaxSWLWarehouseNumber!=null) query = query.Where(t=>t.SWLWarehouseNumber<=searchModel.MaxSWLWarehouseNumber);
                if(sort=="SWLWarehouseNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.SWLWarehouseNumber):query.OrderByDescending(t=>t.SWLWarehouseNumber);
                    isordered = true;
                }
				// SWLCargoNumber INT 货物编号 
                if(searchModel.MinSWLCargoNumber!=null) query = query.Where(t=>t.SWLCargoNumber>=searchModel.MinSWLCargoNumber);
                if(searchModel.MaxSWLCargoNumber!=null) query = query.Where(t=>t.SWLCargoNumber<=searchModel.MaxSWLCargoNumber);
                if(sort=="SWLCargoNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.SWLCargoNumber):query.OrderByDescending(t=>t.SWLCargoNumber);
                    isordered = true;
                }
				// SWLSupplierNumber INT 供应商编号 
                if(searchModel.MinSWLSupplierNumber!=null) query = query.Where(t=>t.SWLSupplierNumber>=searchModel.MinSWLSupplierNumber);
                if(searchModel.MaxSWLSupplierNumber!=null) query = query.Where(t=>t.SWLSupplierNumber<=searchModel.MaxSWLSupplierNumber);
                if(sort=="SWLSupplierNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.SWLSupplierNumber):query.OrderByDescending(t=>t.SWLSupplierNumber);
                    isordered = true;
                }
				// SWLDate DATETIME 日期 
                if(searchModel.FromSWLDate!=null) query = query.Where(t=>t.SWLDate>=searchModel.FromSWLDate);
                if(searchModel.ToSWLDate!=null) query = query.Where(t=>t.SWLDate<=searchModel.ToSWLDate);
                if(sort=="SWLDate")
                {
					query = order=="asc"?query.OrderBy(t=>t.SWLDate):query.OrderByDescending(t=>t.SWLDate);
                    isordered = true;
                }
				// SWLAmount INT 数量 
                if(searchModel.MinSWLAmount!=null) query = query.Where(t=>t.SWLAmount>=searchModel.MinSWLAmount);
                if(searchModel.MaxSWLAmount!=null) query = query.Where(t=>t.SWLAmount<=searchModel.MaxSWLAmount);
                if(sort=="SWLAmount")
                {
					query = order=="asc"?query.OrderBy(t=>t.SWLAmount):query.OrderByDescending(t=>t.SWLAmount);
                    isordered = true;
                }
				// SWLRemarks NVARCHAR(50) 备注 
                if(!string.IsNullOrEmpty(searchModel.SWLRemarks)) query = query.Where(t=>t.SWLRemarks.Contains(searchModel.SWLRemarks));
                if(sort=="SWLRemarks")
                {
					query = order=="asc"?query.OrderBy(t=>t.SWLRemarks):query.OrderByDescending(t=>t.SWLRemarks);
                    isordered = true;
                }
				if(!string.IsNullOrEmpty(search)){
					query = query.Where(t=>t.SWLRemarks.Contains(search));
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
                return new CommonOutputList<SupplyWarehousingList>
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
				// RBWarehouseNumber INT 仓库编号 
                if(searchModel.MinRBWarehouseNumber!=null) query = query.Where(t=>t.RBWarehouseNumber>=searchModel.MinRBWarehouseNumber);
                if(searchModel.MaxRBWarehouseNumber!=null) query = query.Where(t=>t.RBWarehouseNumber<=searchModel.MaxRBWarehouseNumber);
                if(sort=="RBWarehouseNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.RBWarehouseNumber):query.OrderByDescending(t=>t.RBWarehouseNumber);
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
				// RBDate DATETIME 日期 
                if(searchModel.FromRBDate!=null) query = query.Where(t=>t.RBDate>=searchModel.FromRBDate);
                if(searchModel.ToRBDate!=null) query = query.Where(t=>t.RBDate<=searchModel.ToRBDate);
                if(sort=="RBDate")
                {
					query = order=="asc"?query.OrderBy(t=>t.RBDate):query.OrderByDescending(t=>t.RBDate);
                    isordered = true;
                }
				// RBAmount INT 数量 
                if(searchModel.MinRBAmount!=null) query = query.Where(t=>t.RBAmount>=searchModel.MinRBAmount);
                if(searchModel.MaxRBAmount!=null) query = query.Where(t=>t.RBAmount<=searchModel.MaxRBAmount);
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
					query = query.Where(t=>t.RBRemarks.Contains(search));
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
    /// 【仓库库存】7次查询分别得到七天内记录的条数，形成柱状图，折线图
    /// </summary>
    public partial class WarehouseStockCountEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
                var now = Parse(request.context.Request.Params["date"] ?? Now.ToString("yyyy-MM-dd"));
                var dates = Range(1, 7).Select(p => now.AddDays(-1 * p)).ToList();
                var list = dates.Select(p => ctx.WarehouseStock.Count(m => m.CreateOn==p)).ToList();
                return new
                {
                    sum = list.Sum(),
                    xaxis = dates.Select(p => p.ToString("yyyy-MM-dd")).ToList(),
                    series = list
                };
            }
        }
        public override string Comments=> "【仓库库存】7次查询分别得到七天内记录的条数，形成柱状图，折线图";
    }
	public partial class TruncateWarehouseStockEvaluator : Evaluator
	{
        protected override object Evaluate(CommonRequest request)
		{
            using (var ctx = new DefaultContext())
			{
                ctx.WarehouseStock.RemoveRange(ctx.WarehouseStock);
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
    /// 删除【仓库库存】
    /// </summary>
    public partial class DeleteWarehouseStockEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            var data = JsonConvert.DeserializeObject<WarehouseStock>(HttpUtility.UrlDecode(request.data));
            if (data == null)
                return new CommonOutputT<string>
                {
                    success = false,
                    message = "参数错误"
                };
            using (var ctx = new DefaultContext())
            {
                var one = ctx.WarehouseStock.Find(data.id);
				if(one==null){
					return new CommonOutputT<string>
					{
						success = false,
						message = "未找到需要删除的数据"
					};
				}
                one.IsDeleted = 1;
				ctx.WarehouseStock.AddOrUpdate(one);
				ctx.SaveChanges();
				return new CommonOutputT<string>
				{
					success = true,
					message = "删除成功"
				};
            }
			
        }
        public override string Comments=> "删除一条仓库库存记录";
    }
	
    /// <summary>
    /// 保存【仓库库存】
    /// </summary>
    public partial class SaveWarehouseStockEvaluator : Evaluator
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
			WarehouseStock entity = null;
			try
			{
				entity = JsonConvert.DeserializeObject<WarehouseStock>(HttpUtility.UrlDecode(s));
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
                    var one = ctx.WarehouseStock.FirstOrDefault(p=>p.id==entity.id);
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
					ctx.WarehouseStock.AddOrUpdate(one);
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
				

								// NVARCHAR(50) 备注
				entity.WSRemarks = HttpUtility.UrlDecode(entity.WSRemarks);
	
                entity.CreateBy = entity.CreateBy ?? user?.UILoginName ?? "未登录用户";
                entity.UpdateBy = user?.UILoginName ?? "未登录用户";
                entity.CreateOn = entity.CreateOn ?? Now;
                entity.TransactionID = transactionId;
                entity.UpdateOn = Now;
                entity.IsDeleted = 0;
	            entity.VersionNo = entity.VersionNo ?? 0;
                entity.DataLevel = entity.DataLevel ?? user?.DataLevel ?? "019999";
				ctx.WarehouseStock.AddOrUpdate(entity);
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
        public override string Comments=> "保存一条WarehouseStock记录";
    }
	
    /// <summary>
    /// 查询空的【仓库库存】
    /// </summary>
    public partial class GetWarehouseStockEmptyEvaluator:Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            return new WarehouseStock();
        }
        public override string Comments=> "获取空的仓库库存记录";
    }
	
    /// <summary>
    /// 查询【仓库库存】列表
    /// </summary>
    public partial class GetWarehouseStockListEvaluator : Evaluator
    {
        public override string Comments=> "获取WarehouseStock列表 ";
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
				var datalevel = CurrentUserInformation?.DataLevel;
                var searchModel = HttpUtility.UrlDecode(request.data).Deserialize<WarehouseStockSearchModel>() ?? new WarehouseStockSearchModel();
                var query = ctx.WarehouseStock.Where(t=>t.IsDeleted==0);
                var @params = request.context.Request.Params;
                searchModel.PageSize = (@params["limit"] ?? searchModel.PageSize.ToString()).ToInt();
				searchModel.PageSize = searchModel.PageSize==0?4000:searchModel.PageSize;
                searchModel.PageIndex = (@params["offset"]).ToInt()/ searchModel.PageSize;
                var isordered = false;
                var search = @params["search"] ?? searchModel.SearchKey;
                var sort = searchModel.Sort ?? @params["sort"];
				var order = @params["order"];
				// WSWarehouseNumber INT 仓库编号 
                if(searchModel.MinWSWarehouseNumber!=null) query = query.Where(t=>t.WSWarehouseNumber>=searchModel.MinWSWarehouseNumber);
                if(searchModel.MaxWSWarehouseNumber!=null) query = query.Where(t=>t.WSWarehouseNumber<=searchModel.MaxWSWarehouseNumber);
                if(sort=="WSWarehouseNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.WSWarehouseNumber):query.OrderByDescending(t=>t.WSWarehouseNumber);
                    isordered = true;
                }
				// WSCargoNumber INT 货物编号 
                if(searchModel.MinWSCargoNumber!=null) query = query.Where(t=>t.WSCargoNumber>=searchModel.MinWSCargoNumber);
                if(searchModel.MaxWSCargoNumber!=null) query = query.Where(t=>t.WSCargoNumber<=searchModel.MaxWSCargoNumber);
                if(sort=="WSCargoNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.WSCargoNumber):query.OrderByDescending(t=>t.WSCargoNumber);
                    isordered = true;
                }
				// WSAmount INT 数量 
                if(searchModel.MinWSAmount!=null) query = query.Where(t=>t.WSAmount>=searchModel.MinWSAmount);
                if(searchModel.MaxWSAmount!=null) query = query.Where(t=>t.WSAmount<=searchModel.MaxWSAmount);
                if(sort=="WSAmount")
                {
					query = order=="asc"?query.OrderBy(t=>t.WSAmount):query.OrderByDescending(t=>t.WSAmount);
                    isordered = true;
                }
				// WSRemarks NVARCHAR(50) 备注 
                if(!string.IsNullOrEmpty(searchModel.WSRemarks)) query = query.Where(t=>t.WSRemarks.Contains(searchModel.WSRemarks));
                if(sort=="WSRemarks")
                {
					query = order=="asc"?query.OrderBy(t=>t.WSRemarks):query.OrderByDescending(t=>t.WSRemarks);
                    isordered = true;
                }
				if(!string.IsNullOrEmpty(search)){
					query = query.Where(t=>t.WSRemarks.Contains(search));
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
                return new CommonOutputList<WarehouseStock>
                {
                    success = true, rows = rows, total = total, message="查询成功"
                };
            }
        }
    }


    /// <summary>
    /// 【货架库存】7次查询分别得到七天内记录的条数，形成柱状图，折线图
    /// </summary>
    public partial class ShelfStockCountEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
                var now = Parse(request.context.Request.Params["date"] ?? Now.ToString("yyyy-MM-dd"));
                var dates = Range(1, 7).Select(p => now.AddDays(-1 * p)).ToList();
                var list = dates.Select(p => ctx.ShelfStock.Count(m => m.CreateOn==p)).ToList();
                return new
                {
                    sum = list.Sum(),
                    xaxis = dates.Select(p => p.ToString("yyyy-MM-dd")).ToList(),
                    series = list
                };
            }
        }
        public override string Comments=> "【货架库存】7次查询分别得到七天内记录的条数，形成柱状图，折线图";
    }
	public partial class TruncateShelfStockEvaluator : Evaluator
	{
        protected override object Evaluate(CommonRequest request)
		{
            using (var ctx = new DefaultContext())
			{
                ctx.ShelfStock.RemoveRange(ctx.ShelfStock);
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
    /// 删除【货架库存】
    /// </summary>
    public partial class DeleteShelfStockEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            var data = JsonConvert.DeserializeObject<ShelfStock>(HttpUtility.UrlDecode(request.data));
            if (data == null)
                return new CommonOutputT<string>
                {
                    success = false,
                    message = "参数错误"
                };
            using (var ctx = new DefaultContext())
            {
                var one = ctx.ShelfStock.Find(data.id);
				if(one==null){
					return new CommonOutputT<string>
					{
						success = false,
						message = "未找到需要删除的数据"
					};
				}
                one.IsDeleted = 1;
				ctx.ShelfStock.AddOrUpdate(one);
				ctx.SaveChanges();
				return new CommonOutputT<string>
				{
					success = true,
					message = "删除成功"
				};
            }
			
        }
        public override string Comments=> "删除一条货架库存记录";
    }
	
    /// <summary>
    /// 保存【货架库存】
    /// </summary>
    public partial class SaveShelfStockEvaluator : Evaluator
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
			ShelfStock entity = null;
			try
			{
				entity = JsonConvert.DeserializeObject<ShelfStock>(HttpUtility.UrlDecode(s));
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
                    var one = ctx.ShelfStock.FirstOrDefault(p=>p.id==entity.id);
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
					ctx.ShelfStock.AddOrUpdate(one);
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
				

								// NVARCHAR(50) 备注
				entity.SSRemarks = HttpUtility.UrlDecode(entity.SSRemarks);
	
                entity.CreateBy = entity.CreateBy ?? user?.UILoginName ?? "未登录用户";
                entity.UpdateBy = user?.UILoginName ?? "未登录用户";
                entity.CreateOn = entity.CreateOn ?? Now;
                entity.TransactionID = transactionId;
                entity.UpdateOn = Now;
                entity.IsDeleted = 0;
	            entity.VersionNo = entity.VersionNo ?? 0;
                entity.DataLevel = entity.DataLevel ?? user?.DataLevel ?? "019999";
				ctx.ShelfStock.AddOrUpdate(entity);
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
        public override string Comments=> "保存一条ShelfStock记录";
    }
	
    /// <summary>
    /// 查询空的【货架库存】
    /// </summary>
    public partial class GetShelfStockEmptyEvaluator:Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            return new ShelfStock();
        }
        public override string Comments=> "获取空的货架库存记录";
    }
	
    /// <summary>
    /// 查询【货架库存】列表
    /// </summary>
    public partial class GetShelfStockListEvaluator : Evaluator
    {
        public override string Comments=> "获取ShelfStock列表 ";
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
				var datalevel = CurrentUserInformation?.DataLevel;
                var searchModel = HttpUtility.UrlDecode(request.data).Deserialize<ShelfStockSearchModel>() ?? new ShelfStockSearchModel();
                var query = ctx.ShelfStock.Where(t=>t.IsDeleted==0);
                var @params = request.context.Request.Params;
                searchModel.PageSize = (@params["limit"] ?? searchModel.PageSize.ToString()).ToInt();
				searchModel.PageSize = searchModel.PageSize==0?4000:searchModel.PageSize;
                searchModel.PageIndex = (@params["offset"]).ToInt()/ searchModel.PageSize;
                var isordered = false;
                var search = @params["search"] ?? searchModel.SearchKey;
                var sort = searchModel.Sort ?? @params["sort"];
				var order = @params["order"];
				// SSShelfNumber INT 货架编号 
                if(searchModel.MinSSShelfNumber!=null) query = query.Where(t=>t.SSShelfNumber>=searchModel.MinSSShelfNumber);
                if(searchModel.MaxSSShelfNumber!=null) query = query.Where(t=>t.SSShelfNumber<=searchModel.MaxSSShelfNumber);
                if(sort=="SSShelfNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.SSShelfNumber):query.OrderByDescending(t=>t.SSShelfNumber);
                    isordered = true;
                }
				// SSCargoNumber INT 货物编号 
                if(searchModel.MinSSCargoNumber!=null) query = query.Where(t=>t.SSCargoNumber>=searchModel.MinSSCargoNumber);
                if(searchModel.MaxSSCargoNumber!=null) query = query.Where(t=>t.SSCargoNumber<=searchModel.MaxSSCargoNumber);
                if(sort=="SSCargoNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.SSCargoNumber):query.OrderByDescending(t=>t.SSCargoNumber);
                    isordered = true;
                }
				// SSAmount INT 数量 
                if(searchModel.MinSSAmount!=null) query = query.Where(t=>t.SSAmount>=searchModel.MinSSAmount);
                if(searchModel.MaxSSAmount!=null) query = query.Where(t=>t.SSAmount<=searchModel.MaxSSAmount);
                if(sort=="SSAmount")
                {
					query = order=="asc"?query.OrderBy(t=>t.SSAmount):query.OrderByDescending(t=>t.SSAmount);
                    isordered = true;
                }
				// SSRemarks NVARCHAR(50) 备注 
                if(!string.IsNullOrEmpty(searchModel.SSRemarks)) query = query.Where(t=>t.SSRemarks.Contains(searchModel.SSRemarks));
                if(sort=="SSRemarks")
                {
					query = order=="asc"?query.OrderBy(t=>t.SSRemarks):query.OrderByDescending(t=>t.SSRemarks);
                    isordered = true;
                }
				if(!string.IsNullOrEmpty(search)){
					query = query.Where(t=>t.SSRemarks.Contains(search));
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
                return new CommonOutputList<ShelfStock>
                {
                    success = true, rows = rows, total = total, message="查询成功"
                };
            }
        }
    }


    /// <summary>
    /// 【上架单】7次查询分别得到七天内记录的条数，形成柱状图，折线图
    /// </summary>
    public partial class ShelfListCountEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
                var now = Parse(request.context.Request.Params["date"] ?? Now.ToString("yyyy-MM-dd"));
                var dates = Range(1, 7).Select(p => now.AddDays(-1 * p)).ToList();
                var list = dates.Select(p => ctx.ShelfList.Count(m => m.CreateOn==p)).ToList();
                return new
                {
                    sum = list.Sum(),
                    xaxis = dates.Select(p => p.ToString("yyyy-MM-dd")).ToList(),
                    series = list
                };
            }
        }
        public override string Comments=> "【上架单】7次查询分别得到七天内记录的条数，形成柱状图，折线图";
    }
	public partial class TruncateShelfListEvaluator : Evaluator
	{
        protected override object Evaluate(CommonRequest request)
		{
            using (var ctx = new DefaultContext())
			{
                ctx.ShelfList.RemoveRange(ctx.ShelfList);
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
    /// 删除【上架单】
    /// </summary>
    public partial class DeleteShelfListEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            var data = JsonConvert.DeserializeObject<ShelfList>(HttpUtility.UrlDecode(request.data));
            if (data == null)
                return new CommonOutputT<string>
                {
                    success = false,
                    message = "参数错误"
                };
            using (var ctx = new DefaultContext())
            {
                var one = ctx.ShelfList.Find(data.id);
				if(one==null){
					return new CommonOutputT<string>
					{
						success = false,
						message = "未找到需要删除的数据"
					};
				}
                one.IsDeleted = 1;
				ctx.ShelfList.AddOrUpdate(one);
				ctx.SaveChanges();
				return new CommonOutputT<string>
				{
					success = true,
					message = "删除成功"
				};
            }
			
        }
        public override string Comments=> "删除一条上架单记录";
    }
	
    /// <summary>
    /// 保存【上架单】
    /// </summary>
    public partial class SaveShelfListEvaluator : Evaluator
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
			ShelfList entity = null;
			try
			{
				entity = JsonConvert.DeserializeObject<ShelfList>(HttpUtility.UrlDecode(s));
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
                    var one = ctx.ShelfList.FirstOrDefault(p=>p.id==entity.id);
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
					ctx.ShelfList.AddOrUpdate(one);
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
				ctx.ShelfList.AddOrUpdate(entity);
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
        public override string Comments=> "保存一条ShelfList记录";
    }
	
    /// <summary>
    /// 查询空的【上架单】
    /// </summary>
    public partial class GetShelfListEmptyEvaluator:Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            return new ShelfList();
        }
        public override string Comments=> "获取空的上架单记录";
    }
	
    /// <summary>
    /// 查询【上架单】列表
    /// </summary>
    public partial class GetShelfListListEvaluator : Evaluator
    {
        public override string Comments=> "获取ShelfList列表 ";
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
				var datalevel = CurrentUserInformation?.DataLevel;
                var searchModel = HttpUtility.UrlDecode(request.data).Deserialize<ShelfListSearchModel>() ?? new ShelfListSearchModel();
                var query = ctx.ShelfList.Where(t=>t.IsDeleted==0);
                var @params = request.context.Request.Params;
                searchModel.PageSize = (@params["limit"] ?? searchModel.PageSize.ToString()).ToInt();
				searchModel.PageSize = searchModel.PageSize==0?4000:searchModel.PageSize;
                searchModel.PageIndex = (@params["offset"]).ToInt()/ searchModel.PageSize;
                var isordered = false;
                var search = @params["search"] ?? searchModel.SearchKey;
                var sort = searchModel.Sort ?? @params["sort"];
				var order = @params["order"];
				// SLShelfNumber INT 货架编号 
                if(searchModel.MinSLShelfNumber!=null) query = query.Where(t=>t.SLShelfNumber>=searchModel.MinSLShelfNumber);
                if(searchModel.MaxSLShelfNumber!=null) query = query.Where(t=>t.SLShelfNumber<=searchModel.MaxSLShelfNumber);
                if(sort=="SLShelfNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.SLShelfNumber):query.OrderByDescending(t=>t.SLShelfNumber);
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
				// SLDate DATETIME 日期 
                if(searchModel.FromSLDate!=null) query = query.Where(t=>t.SLDate>=searchModel.FromSLDate);
                if(searchModel.ToSLDate!=null) query = query.Where(t=>t.SLDate<=searchModel.ToSLDate);
                if(sort=="SLDate")
                {
					query = order=="asc"?query.OrderBy(t=>t.SLDate):query.OrderByDescending(t=>t.SLDate);
                    isordered = true;
                }
				// SLAmount INT 数量 
                if(searchModel.MinSLAmount!=null) query = query.Where(t=>t.SLAmount>=searchModel.MinSLAmount);
                if(searchModel.MaxSLAmount!=null) query = query.Where(t=>t.SLAmount<=searchModel.MaxSLAmount);
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
					query = query.Where(t=>t.SLRemarks.Contains(search));
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
                return new CommonOutputList<ShelfList>
                {
                    success = true, rows = rows, total = total, message="查询成功"
                };
            }
        }
    }


    /// <summary>
    /// 【采购凭证单】7次查询分别得到七天内记录的条数，形成柱状图，折线图
    /// </summary>
    public partial class PurchaseVoucherCountEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
                var now = Parse(request.context.Request.Params["date"] ?? Now.ToString("yyyy-MM-dd"));
                var dates = Range(1, 7).Select(p => now.AddDays(-1 * p)).ToList();
                var list = dates.Select(p => ctx.PurchaseVoucher.Count(m => m.CreateOn==p)).ToList();
                return new
                {
                    sum = list.Sum(),
                    xaxis = dates.Select(p => p.ToString("yyyy-MM-dd")).ToList(),
                    series = list
                };
            }
        }
        public override string Comments=> "【采购凭证单】7次查询分别得到七天内记录的条数，形成柱状图，折线图";
    }
	public partial class TruncatePurchaseVoucherEvaluator : Evaluator
	{
        protected override object Evaluate(CommonRequest request)
		{
            using (var ctx = new DefaultContext())
			{
                ctx.PurchaseVoucher.RemoveRange(ctx.PurchaseVoucher);
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
    /// 删除【采购凭证单】
    /// </summary>
    public partial class DeletePurchaseVoucherEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            var data = JsonConvert.DeserializeObject<PurchaseVoucher>(HttpUtility.UrlDecode(request.data));
            if (data == null)
                return new CommonOutputT<string>
                {
                    success = false,
                    message = "参数错误"
                };
            using (var ctx = new DefaultContext())
            {
                var one = ctx.PurchaseVoucher.Find(data.id);
				if(one==null){
					return new CommonOutputT<string>
					{
						success = false,
						message = "未找到需要删除的数据"
					};
				}
                one.IsDeleted = 1;
				ctx.PurchaseVoucher.AddOrUpdate(one);
				ctx.SaveChanges();
				return new CommonOutputT<string>
				{
					success = true,
					message = "删除成功"
				};
            }
			
        }
        public override string Comments=> "删除一条采购凭证单记录";
    }
	
    /// <summary>
    /// 保存【采购凭证单】
    /// </summary>
    public partial class SavePurchaseVoucherEvaluator : Evaluator
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
			PurchaseVoucher entity = null;
			try
			{
				entity = JsonConvert.DeserializeObject<PurchaseVoucher>(HttpUtility.UrlDecode(s));
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
                    var one = ctx.PurchaseVoucher.FirstOrDefault(p=>p.id==entity.id);
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
					ctx.PurchaseVoucher.AddOrUpdate(one);
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
				

								// NVARCHAR(50) 价格
				entity.PVPrice = HttpUtility.UrlDecode(entity.PVPrice);
					// NVARCHAR(50) 备注
				entity.PVRemarks = HttpUtility.UrlDecode(entity.PVRemarks);
	
                entity.CreateBy = entity.CreateBy ?? user?.UILoginName ?? "未登录用户";
                entity.UpdateBy = user?.UILoginName ?? "未登录用户";
                entity.CreateOn = entity.CreateOn ?? Now;
                entity.TransactionID = transactionId;
                entity.UpdateOn = Now;
                entity.IsDeleted = 0;
	            entity.VersionNo = entity.VersionNo ?? 0;
                entity.DataLevel = entity.DataLevel ?? user?.DataLevel ?? "019999";
				ctx.PurchaseVoucher.AddOrUpdate(entity);
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
        public override string Comments=> "保存一条PurchaseVoucher记录";
    }
	
    /// <summary>
    /// 查询空的【采购凭证单】
    /// </summary>
    public partial class GetPurchaseVoucherEmptyEvaluator:Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            return new PurchaseVoucher();
        }
        public override string Comments=> "获取空的采购凭证单记录";
    }
	
    /// <summary>
    /// 查询【采购凭证单】列表
    /// </summary>
    public partial class GetPurchaseVoucherListEvaluator : Evaluator
    {
        public override string Comments=> "获取PurchaseVoucher列表 ";
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
				var datalevel = CurrentUserInformation?.DataLevel;
                var searchModel = HttpUtility.UrlDecode(request.data).Deserialize<PurchaseVoucherSearchModel>() ?? new PurchaseVoucherSearchModel();
                var query = ctx.PurchaseVoucher.Where(t=>t.IsDeleted==0);
                var @params = request.context.Request.Params;
                searchModel.PageSize = (@params["limit"] ?? searchModel.PageSize.ToString()).ToInt();
				searchModel.PageSize = searchModel.PageSize==0?4000:searchModel.PageSize;
                searchModel.PageIndex = (@params["offset"]).ToInt()/ searchModel.PageSize;
                var isordered = false;
                var search = @params["search"] ?? searchModel.SearchKey;
                var sort = searchModel.Sort ?? @params["sort"];
				var order = @params["order"];
				// PVSupplierNumber INT 供应商编号 
                if(searchModel.MinPVSupplierNumber!=null) query = query.Where(t=>t.PVSupplierNumber>=searchModel.MinPVSupplierNumber);
                if(searchModel.MaxPVSupplierNumber!=null) query = query.Where(t=>t.PVSupplierNumber<=searchModel.MaxPVSupplierNumber);
                if(sort=="PVSupplierNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.PVSupplierNumber):query.OrderByDescending(t=>t.PVSupplierNumber);
                    isordered = true;
                }
				// PVCargoNumber INT 货物编号 
                if(searchModel.MinPVCargoNumber!=null) query = query.Where(t=>t.PVCargoNumber>=searchModel.MinPVCargoNumber);
                if(searchModel.MaxPVCargoNumber!=null) query = query.Where(t=>t.PVCargoNumber<=searchModel.MaxPVCargoNumber);
                if(sort=="PVCargoNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.PVCargoNumber):query.OrderByDescending(t=>t.PVCargoNumber);
                    isordered = true;
                }
				// PVJobNumber INT 工号 
                if(searchModel.MinPVJobNumber!=null) query = query.Where(t=>t.PVJobNumber>=searchModel.MinPVJobNumber);
                if(searchModel.MaxPVJobNumber!=null) query = query.Where(t=>t.PVJobNumber<=searchModel.MaxPVJobNumber);
                if(sort=="PVJobNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.PVJobNumber):query.OrderByDescending(t=>t.PVJobNumber);
                    isordered = true;
                }
				// PVDate DATETIME 日期 
                if(searchModel.FromPVDate!=null) query = query.Where(t=>t.PVDate>=searchModel.FromPVDate);
                if(searchModel.ToPVDate!=null) query = query.Where(t=>t.PVDate<=searchModel.ToPVDate);
                if(sort=="PVDate")
                {
					query = order=="asc"?query.OrderBy(t=>t.PVDate):query.OrderByDescending(t=>t.PVDate);
                    isordered = true;
                }
				// PVAmount INT 数量 
                if(searchModel.MinPVAmount!=null) query = query.Where(t=>t.PVAmount>=searchModel.MinPVAmount);
                if(searchModel.MaxPVAmount!=null) query = query.Where(t=>t.PVAmount<=searchModel.MaxPVAmount);
                if(sort=="PVAmount")
                {
					query = order=="asc"?query.OrderBy(t=>t.PVAmount):query.OrderByDescending(t=>t.PVAmount);
                    isordered = true;
                }
				// PVPrice NVARCHAR(50) 价格 
                if(!string.IsNullOrEmpty(searchModel.PVPrice)) query = query.Where(t=>t.PVPrice.Contains(searchModel.PVPrice));
                if(sort=="PVPrice")
                {
					query = order=="asc"?query.OrderBy(t=>t.PVPrice):query.OrderByDescending(t=>t.PVPrice);
                    isordered = true;
                }
				// PVRemarks NVARCHAR(50) 备注 
                if(!string.IsNullOrEmpty(searchModel.PVRemarks)) query = query.Where(t=>t.PVRemarks.Contains(searchModel.PVRemarks));
                if(sort=="PVRemarks")
                {
					query = order=="asc"?query.OrderBy(t=>t.PVRemarks):query.OrderByDescending(t=>t.PVRemarks);
                    isordered = true;
                }
				if(!string.IsNullOrEmpty(search)){
					query = query.Where(t=>t.PVPrice.Contains(search)||t.PVRemarks.Contains(search));
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
                return new CommonOutputList<PurchaseVoucher>
                {
                    success = true, rows = rows, total = total, message="查询成功"
                };
            }
        }
    }


    /// <summary>
    /// 【出库单】7次查询分别得到七天内记录的条数，形成柱状图，折线图
    /// </summary>
    public partial class OutboundOrderCountEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
                var now = Parse(request.context.Request.Params["date"] ?? Now.ToString("yyyy-MM-dd"));
                var dates = Range(1, 7).Select(p => now.AddDays(-1 * p)).ToList();
                var list = dates.Select(p => ctx.OutboundOrder.Count(m => m.CreateOn==p)).ToList();
                return new
                {
                    sum = list.Sum(),
                    xaxis = dates.Select(p => p.ToString("yyyy-MM-dd")).ToList(),
                    series = list
                };
            }
        }
        public override string Comments=> "【出库单】7次查询分别得到七天内记录的条数，形成柱状图，折线图";
    }
	public partial class TruncateOutboundOrderEvaluator : Evaluator
	{
        protected override object Evaluate(CommonRequest request)
		{
            using (var ctx = new DefaultContext())
			{
                ctx.OutboundOrder.RemoveRange(ctx.OutboundOrder);
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
    /// 删除【出库单】
    /// </summary>
    public partial class DeleteOutboundOrderEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            var data = JsonConvert.DeserializeObject<OutboundOrder>(HttpUtility.UrlDecode(request.data));
            if (data == null)
                return new CommonOutputT<string>
                {
                    success = false,
                    message = "参数错误"
                };
            using (var ctx = new DefaultContext())
            {
                var one = ctx.OutboundOrder.Find(data.id);
				if(one==null){
					return new CommonOutputT<string>
					{
						success = false,
						message = "未找到需要删除的数据"
					};
				}
                one.IsDeleted = 1;
				ctx.OutboundOrder.AddOrUpdate(one);
				ctx.SaveChanges();
				return new CommonOutputT<string>
				{
					success = true,
					message = "删除成功"
				};
            }
			
        }
        public override string Comments=> "删除一条出库单记录";
    }
	
    /// <summary>
    /// 保存【出库单】
    /// </summary>
    public partial class SaveOutboundOrderEvaluator : Evaluator
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
			OutboundOrder entity = null;
			try
			{
				entity = JsonConvert.DeserializeObject<OutboundOrder>(HttpUtility.UrlDecode(s));
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
                    var one = ctx.OutboundOrder.FirstOrDefault(p=>p.id==entity.id);
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
					ctx.OutboundOrder.AddOrUpdate(one);
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
				

								// NVARCHAR(50) 备注
				entity.OORemarks = HttpUtility.UrlDecode(entity.OORemarks);
	
                entity.CreateBy = entity.CreateBy ?? user?.UILoginName ?? "未登录用户";
                entity.UpdateBy = user?.UILoginName ?? "未登录用户";
                entity.CreateOn = entity.CreateOn ?? Now;
                entity.TransactionID = transactionId;
                entity.UpdateOn = Now;
                entity.IsDeleted = 0;
	            entity.VersionNo = entity.VersionNo ?? 0;
                entity.DataLevel = entity.DataLevel ?? user?.DataLevel ?? "019999";
				ctx.OutboundOrder.AddOrUpdate(entity);
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
        public override string Comments=> "保存一条OutboundOrder记录";
    }
	
    /// <summary>
    /// 查询空的【出库单】
    /// </summary>
    public partial class GetOutboundOrderEmptyEvaluator:Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            return new OutboundOrder();
        }
        public override string Comments=> "获取空的出库单记录";
    }
	
    /// <summary>
    /// 查询【出库单】列表
    /// </summary>
    public partial class GetOutboundOrderListEvaluator : Evaluator
    {
        public override string Comments=> "获取OutboundOrder列表 ";
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
				var datalevel = CurrentUserInformation?.DataLevel;
                var searchModel = HttpUtility.UrlDecode(request.data).Deserialize<OutboundOrderSearchModel>() ?? new OutboundOrderSearchModel();
                var query = ctx.OutboundOrder.Where(t=>t.IsDeleted==0);
                var @params = request.context.Request.Params;
                searchModel.PageSize = (@params["limit"] ?? searchModel.PageSize.ToString()).ToInt();
				searchModel.PageSize = searchModel.PageSize==0?4000:searchModel.PageSize;
                searchModel.PageIndex = (@params["offset"]).ToInt()/ searchModel.PageSize;
                var isordered = false;
                var search = @params["search"] ?? searchModel.SearchKey;
                var sort = searchModel.Sort ?? @params["sort"];
				var order = @params["order"];
				// OOWarehouseNumber INT 仓库编号 
                if(searchModel.MinOOWarehouseNumber!=null) query = query.Where(t=>t.OOWarehouseNumber>=searchModel.MinOOWarehouseNumber);
                if(searchModel.MaxOOWarehouseNumber!=null) query = query.Where(t=>t.OOWarehouseNumber<=searchModel.MaxOOWarehouseNumber);
                if(sort=="OOWarehouseNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.OOWarehouseNumber):query.OrderByDescending(t=>t.OOWarehouseNumber);
                    isordered = true;
                }
				// OOCargoNumber INT 货物编号 
                if(searchModel.MinOOCargoNumber!=null) query = query.Where(t=>t.OOCargoNumber>=searchModel.MinOOCargoNumber);
                if(searchModel.MaxOOCargoNumber!=null) query = query.Where(t=>t.OOCargoNumber<=searchModel.MaxOOCargoNumber);
                if(sort=="OOCargoNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.OOCargoNumber):query.OrderByDescending(t=>t.OOCargoNumber);
                    isordered = true;
                }
				// OOJobNumber INT 工号 
                if(searchModel.MinOOJobNumber!=null) query = query.Where(t=>t.OOJobNumber>=searchModel.MinOOJobNumber);
                if(searchModel.MaxOOJobNumber!=null) query = query.Where(t=>t.OOJobNumber<=searchModel.MaxOOJobNumber);
                if(sort=="OOJobNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.OOJobNumber):query.OrderByDescending(t=>t.OOJobNumber);
                    isordered = true;
                }
				// OODate DATETIME 日期 
                if(searchModel.FromOODate!=null) query = query.Where(t=>t.OODate>=searchModel.FromOODate);
                if(searchModel.ToOODate!=null) query = query.Where(t=>t.OODate<=searchModel.ToOODate);
                if(sort=="OODate")
                {
					query = order=="asc"?query.OrderBy(t=>t.OODate):query.OrderByDescending(t=>t.OODate);
                    isordered = true;
                }
				// OOAmount INT 数量 
                if(searchModel.MinOOAmount!=null) query = query.Where(t=>t.OOAmount>=searchModel.MinOOAmount);
                if(searchModel.MaxOOAmount!=null) query = query.Where(t=>t.OOAmount<=searchModel.MaxOOAmount);
                if(sort=="OOAmount")
                {
					query = order=="asc"?query.OrderBy(t=>t.OOAmount):query.OrderByDescending(t=>t.OOAmount);
                    isordered = true;
                }
				// OORemarks NVARCHAR(50) 备注 
                if(!string.IsNullOrEmpty(searchModel.OORemarks)) query = query.Where(t=>t.OORemarks.Contains(searchModel.OORemarks));
                if(sort=="OORemarks")
                {
					query = order=="asc"?query.OrderBy(t=>t.OORemarks):query.OrderByDescending(t=>t.OORemarks);
                    isordered = true;
                }
				if(!string.IsNullOrEmpty(search)){
					query = query.Where(t=>t.OORemarks.Contains(search));
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
                return new CommonOutputList<OutboundOrder>
                {
                    success = true, rows = rows, total = total, message="查询成功"
                };
            }
        }
    }


    /// <summary>
    /// 【下架单】7次查询分别得到七天内记录的条数，形成柱状图，折线图
    /// </summary>
    public partial class LowerFrameCountEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
                var now = Parse(request.context.Request.Params["date"] ?? Now.ToString("yyyy-MM-dd"));
                var dates = Range(1, 7).Select(p => now.AddDays(-1 * p)).ToList();
                var list = dates.Select(p => ctx.LowerFrame.Count(m => m.CreateOn==p)).ToList();
                return new
                {
                    sum = list.Sum(),
                    xaxis = dates.Select(p => p.ToString("yyyy-MM-dd")).ToList(),
                    series = list
                };
            }
        }
        public override string Comments=> "【下架单】7次查询分别得到七天内记录的条数，形成柱状图，折线图";
    }
	public partial class TruncateLowerFrameEvaluator : Evaluator
	{
        protected override object Evaluate(CommonRequest request)
		{
            using (var ctx = new DefaultContext())
			{
                ctx.LowerFrame.RemoveRange(ctx.LowerFrame);
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
    /// 删除【下架单】
    /// </summary>
    public partial class DeleteLowerFrameEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            var data = JsonConvert.DeserializeObject<LowerFrame>(HttpUtility.UrlDecode(request.data));
            if (data == null)
                return new CommonOutputT<string>
                {
                    success = false,
                    message = "参数错误"
                };
            using (var ctx = new DefaultContext())
            {
                var one = ctx.LowerFrame.Find(data.id);
				if(one==null){
					return new CommonOutputT<string>
					{
						success = false,
						message = "未找到需要删除的数据"
					};
				}
                one.IsDeleted = 1;
				ctx.LowerFrame.AddOrUpdate(one);
				ctx.SaveChanges();
				return new CommonOutputT<string>
				{
					success = true,
					message = "删除成功"
				};
            }
			
        }
        public override string Comments=> "删除一条下架单记录";
    }
	
    /// <summary>
    /// 保存【下架单】
    /// </summary>
    public partial class SaveLowerFrameEvaluator : Evaluator
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
			LowerFrame entity = null;
			try
			{
				entity = JsonConvert.DeserializeObject<LowerFrame>(HttpUtility.UrlDecode(s));
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
                    var one = ctx.LowerFrame.FirstOrDefault(p=>p.id==entity.id);
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
					ctx.LowerFrame.AddOrUpdate(one);
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
				

								// NVARCHAR(50) 备注
				entity.LFRemarks = HttpUtility.UrlDecode(entity.LFRemarks);
	
                entity.CreateBy = entity.CreateBy ?? user?.UILoginName ?? "未登录用户";
                entity.UpdateBy = user?.UILoginName ?? "未登录用户";
                entity.CreateOn = entity.CreateOn ?? Now;
                entity.TransactionID = transactionId;
                entity.UpdateOn = Now;
                entity.IsDeleted = 0;
	            entity.VersionNo = entity.VersionNo ?? 0;
                entity.DataLevel = entity.DataLevel ?? user?.DataLevel ?? "019999";
				ctx.LowerFrame.AddOrUpdate(entity);
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
        public override string Comments=> "保存一条LowerFrame记录";
    }
	
    /// <summary>
    /// 查询空的【下架单】
    /// </summary>
    public partial class GetLowerFrameEmptyEvaluator:Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            return new LowerFrame();
        }
        public override string Comments=> "获取空的下架单记录";
    }
	
    /// <summary>
    /// 查询【下架单】列表
    /// </summary>
    public partial class GetLowerFrameListEvaluator : Evaluator
    {
        public override string Comments=> "获取LowerFrame列表 ";
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
				var datalevel = CurrentUserInformation?.DataLevel;
                var searchModel = HttpUtility.UrlDecode(request.data).Deserialize<LowerFrameSearchModel>() ?? new LowerFrameSearchModel();
                var query = ctx.LowerFrame.Where(t=>t.IsDeleted==0);
                var @params = request.context.Request.Params;
                searchModel.PageSize = (@params["limit"] ?? searchModel.PageSize.ToString()).ToInt();
				searchModel.PageSize = searchModel.PageSize==0?4000:searchModel.PageSize;
                searchModel.PageIndex = (@params["offset"]).ToInt()/ searchModel.PageSize;
                var isordered = false;
                var search = @params["search"] ?? searchModel.SearchKey;
                var sort = searchModel.Sort ?? @params["sort"];
				var order = @params["order"];
				// LFShelfNumber INT 货架编号 
                if(searchModel.MinLFShelfNumber!=null) query = query.Where(t=>t.LFShelfNumber>=searchModel.MinLFShelfNumber);
                if(searchModel.MaxLFShelfNumber!=null) query = query.Where(t=>t.LFShelfNumber<=searchModel.MaxLFShelfNumber);
                if(sort=="LFShelfNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.LFShelfNumber):query.OrderByDescending(t=>t.LFShelfNumber);
                    isordered = true;
                }
				// LFCargoNumber INT 货物编号 
                if(searchModel.MinLFCargoNumber!=null) query = query.Where(t=>t.LFCargoNumber>=searchModel.MinLFCargoNumber);
                if(searchModel.MaxLFCargoNumber!=null) query = query.Where(t=>t.LFCargoNumber<=searchModel.MaxLFCargoNumber);
                if(sort=="LFCargoNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.LFCargoNumber):query.OrderByDescending(t=>t.LFCargoNumber);
                    isordered = true;
                }
				// LFJobNumber INT 工号 
                if(searchModel.MinLFJobNumber!=null) query = query.Where(t=>t.LFJobNumber>=searchModel.MinLFJobNumber);
                if(searchModel.MaxLFJobNumber!=null) query = query.Where(t=>t.LFJobNumber<=searchModel.MaxLFJobNumber);
                if(sort=="LFJobNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.LFJobNumber):query.OrderByDescending(t=>t.LFJobNumber);
                    isordered = true;
                }
				// LFDate DATETIME 日期 
                if(searchModel.FromLFDate!=null) query = query.Where(t=>t.LFDate>=searchModel.FromLFDate);
                if(searchModel.ToLFDate!=null) query = query.Where(t=>t.LFDate<=searchModel.ToLFDate);
                if(sort=="LFDate")
                {
					query = order=="asc"?query.OrderBy(t=>t.LFDate):query.OrderByDescending(t=>t.LFDate);
                    isordered = true;
                }
				// LFAmount INT 数量 
                if(searchModel.MinLFAmount!=null) query = query.Where(t=>t.LFAmount>=searchModel.MinLFAmount);
                if(searchModel.MaxLFAmount!=null) query = query.Where(t=>t.LFAmount<=searchModel.MaxLFAmount);
                if(sort=="LFAmount")
                {
					query = order=="asc"?query.OrderBy(t=>t.LFAmount):query.OrderByDescending(t=>t.LFAmount);
                    isordered = true;
                }
				// LFRemarks NVARCHAR(50) 备注 
                if(!string.IsNullOrEmpty(searchModel.LFRemarks)) query = query.Where(t=>t.LFRemarks.Contains(searchModel.LFRemarks));
                if(sort=="LFRemarks")
                {
					query = order=="asc"?query.OrderBy(t=>t.LFRemarks):query.OrderByDescending(t=>t.LFRemarks);
                    isordered = true;
                }
				if(!string.IsNullOrEmpty(search)){
					query = query.Where(t=>t.LFRemarks.Contains(search));
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
                return new CommonOutputList<LowerFrame>
                {
                    success = true, rows = rows, total = total, message="查询成功"
                };
            }
        }
    }


    /// <summary>
    /// 【销售凭证单】7次查询分别得到七天内记录的条数，形成柱状图，折线图
    /// </summary>
    public partial class SalesVoucherCountEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
                var now = Parse(request.context.Request.Params["date"] ?? Now.ToString("yyyy-MM-dd"));
                var dates = Range(1, 7).Select(p => now.AddDays(-1 * p)).ToList();
                var list = dates.Select(p => ctx.SalesVoucher.Count(m => m.CreateOn==p)).ToList();
                return new
                {
                    sum = list.Sum(),
                    xaxis = dates.Select(p => p.ToString("yyyy-MM-dd")).ToList(),
                    series = list
                };
            }
        }
        public override string Comments=> "【销售凭证单】7次查询分别得到七天内记录的条数，形成柱状图，折线图";
    }
	public partial class TruncateSalesVoucherEvaluator : Evaluator
	{
        protected override object Evaluate(CommonRequest request)
		{
            using (var ctx = new DefaultContext())
			{
                ctx.SalesVoucher.RemoveRange(ctx.SalesVoucher);
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
    /// 删除【销售凭证单】
    /// </summary>
    public partial class DeleteSalesVoucherEvaluator : Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            var data = JsonConvert.DeserializeObject<SalesVoucher>(HttpUtility.UrlDecode(request.data));
            if (data == null)
                return new CommonOutputT<string>
                {
                    success = false,
                    message = "参数错误"
                };
            using (var ctx = new DefaultContext())
            {
                var one = ctx.SalesVoucher.Find(data.id);
				if(one==null){
					return new CommonOutputT<string>
					{
						success = false,
						message = "未找到需要删除的数据"
					};
				}
                one.IsDeleted = 1;
				ctx.SalesVoucher.AddOrUpdate(one);
				ctx.SaveChanges();
				return new CommonOutputT<string>
				{
					success = true,
					message = "删除成功"
				};
            }
			
        }
        public override string Comments=> "删除一条销售凭证单记录";
    }
	
    /// <summary>
    /// 保存【销售凭证单】
    /// </summary>
    public partial class SaveSalesVoucherEvaluator : Evaluator
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
			SalesVoucher entity = null;
			try
			{
				entity = JsonConvert.DeserializeObject<SalesVoucher>(HttpUtility.UrlDecode(s));
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
                    var one = ctx.SalesVoucher.FirstOrDefault(p=>p.id==entity.id);
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
					ctx.SalesVoucher.AddOrUpdate(one);
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
				

								// NVARCHAR(50) 价格
				entity.SVPrice = HttpUtility.UrlDecode(entity.SVPrice);
					// NVARCHAR(50) 备注
				entity.SVRemarks = HttpUtility.UrlDecode(entity.SVRemarks);
	
                entity.CreateBy = entity.CreateBy ?? user?.UILoginName ?? "未登录用户";
                entity.UpdateBy = user?.UILoginName ?? "未登录用户";
                entity.CreateOn = entity.CreateOn ?? Now;
                entity.TransactionID = transactionId;
                entity.UpdateOn = Now;
                entity.IsDeleted = 0;
	            entity.VersionNo = entity.VersionNo ?? 0;
                entity.DataLevel = entity.DataLevel ?? user?.DataLevel ?? "019999";
				ctx.SalesVoucher.AddOrUpdate(entity);
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
        public override string Comments=> "保存一条SalesVoucher记录";
    }
	
    /// <summary>
    /// 查询空的【销售凭证单】
    /// </summary>
    public partial class GetSalesVoucherEmptyEvaluator:Evaluator
    {
        protected override object Evaluate(CommonRequest request)
        {
            return new SalesVoucher();
        }
        public override string Comments=> "获取空的销售凭证单记录";
    }
	
    /// <summary>
    /// 查询【销售凭证单】列表
    /// </summary>
    public partial class GetSalesVoucherListEvaluator : Evaluator
    {
        public override string Comments=> "获取SalesVoucher列表 ";
        protected override object Evaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
				var datalevel = CurrentUserInformation?.DataLevel;
                var searchModel = HttpUtility.UrlDecode(request.data).Deserialize<SalesVoucherSearchModel>() ?? new SalesVoucherSearchModel();
                var query = ctx.SalesVoucher.Where(t=>t.IsDeleted==0);
                var @params = request.context.Request.Params;
                searchModel.PageSize = (@params["limit"] ?? searchModel.PageSize.ToString()).ToInt();
				searchModel.PageSize = searchModel.PageSize==0?4000:searchModel.PageSize;
                searchModel.PageIndex = (@params["offset"]).ToInt()/ searchModel.PageSize;
                var isordered = false;
                var search = @params["search"] ?? searchModel.SearchKey;
                var sort = searchModel.Sort ?? @params["sort"];
				var order = @params["order"];
				// SVCustomerNumber INT 客户编号 
                if(searchModel.MinSVCustomerNumber!=null) query = query.Where(t=>t.SVCustomerNumber>=searchModel.MinSVCustomerNumber);
                if(searchModel.MaxSVCustomerNumber!=null) query = query.Where(t=>t.SVCustomerNumber<=searchModel.MaxSVCustomerNumber);
                if(sort=="SVCustomerNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.SVCustomerNumber):query.OrderByDescending(t=>t.SVCustomerNumber);
                    isordered = true;
                }
				// SVCargoNumber INT 货物编号 
                if(searchModel.MinSVCargoNumber!=null) query = query.Where(t=>t.SVCargoNumber>=searchModel.MinSVCargoNumber);
                if(searchModel.MaxSVCargoNumber!=null) query = query.Where(t=>t.SVCargoNumber<=searchModel.MaxSVCargoNumber);
                if(sort=="SVCargoNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.SVCargoNumber):query.OrderByDescending(t=>t.SVCargoNumber);
                    isordered = true;
                }
				// SVJobNumber INT 工号 
                if(searchModel.MinSVJobNumber!=null) query = query.Where(t=>t.SVJobNumber>=searchModel.MinSVJobNumber);
                if(searchModel.MaxSVJobNumber!=null) query = query.Where(t=>t.SVJobNumber<=searchModel.MaxSVJobNumber);
                if(sort=="SVJobNumber")
                {
					query = order=="asc"?query.OrderBy(t=>t.SVJobNumber):query.OrderByDescending(t=>t.SVJobNumber);
                    isordered = true;
                }
				// SVDate DATETIME 日期 
                if(searchModel.FromSVDate!=null) query = query.Where(t=>t.SVDate>=searchModel.FromSVDate);
                if(searchModel.ToSVDate!=null) query = query.Where(t=>t.SVDate<=searchModel.ToSVDate);
                if(sort=="SVDate")
                {
					query = order=="asc"?query.OrderBy(t=>t.SVDate):query.OrderByDescending(t=>t.SVDate);
                    isordered = true;
                }
				// SVAmount INT 数量 
                if(searchModel.MinSVAmount!=null) query = query.Where(t=>t.SVAmount>=searchModel.MinSVAmount);
                if(searchModel.MaxSVAmount!=null) query = query.Where(t=>t.SVAmount<=searchModel.MaxSVAmount);
                if(sort=="SVAmount")
                {
					query = order=="asc"?query.OrderBy(t=>t.SVAmount):query.OrderByDescending(t=>t.SVAmount);
                    isordered = true;
                }
				// SVPrice NVARCHAR(50) 价格 
                if(!string.IsNullOrEmpty(searchModel.SVPrice)) query = query.Where(t=>t.SVPrice.Contains(searchModel.SVPrice));
                if(sort=="SVPrice")
                {
					query = order=="asc"?query.OrderBy(t=>t.SVPrice):query.OrderByDescending(t=>t.SVPrice);
                    isordered = true;
                }
				// SVRemarks NVARCHAR(50) 备注 
                if(!string.IsNullOrEmpty(searchModel.SVRemarks)) query = query.Where(t=>t.SVRemarks.Contains(searchModel.SVRemarks));
                if(sort=="SVRemarks")
                {
					query = order=="asc"?query.OrderBy(t=>t.SVRemarks):query.OrderByDescending(t=>t.SVRemarks);
                    isordered = true;
                }
				if(!string.IsNullOrEmpty(search)){
					query = query.Where(t=>t.SVPrice.Contains(search)||t.SVRemarks.Contains(search));
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
                return new CommonOutputList<SalesVoucher>
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
