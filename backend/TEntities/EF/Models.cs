﻿  


using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TEntities.EF
{
	
    /// <summary>
    ///  客户 
    /// </summary>
	[Table("Customertype")]
    public class Customertype 
    {
			        
        /// <summary>
        ///  Customertype编号
        /// </summary>
		[Key]
        public int id { get; set; }
        /// <summary>
        ///  版本号
        /// </summary>
        public int? VersionNo { get; set; }
        /// <summary>
        ///  创建人
        /// </summary>
        public string CreateBy { get; set; }
        /// <summary>
        ///  创建时间
        /// </summary>
        public DateTime? CreateOn { get; set; }
        /// <summary>
        ///  更新时间
        /// </summary>
        public string UpdateBy { get; set; }
        /// <summary>
        ///  更新人
        /// </summary>
        public DateTime? UpdateOn { get; set; }
        /// <summary>
        ///  事务编号
        /// </summary>
        public string TransactionID { get; set; }
        /// <summary>
        ///  是否删除
        /// </summary>
		[Index]
        public int? IsDeleted { get; set; }
        /// <summary>
        ///  数据级别
        /// </summary>
        public string DataLevel { get; set; }
        /// <summary>
        ///  排序
        /// </summary>
        public int? ord { get; set; }
			
        /// <summary>
        ///  客户编号 INT 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "客户编号不能为空")]
		
        public Nullable<int> CCustomerNumber { get; set; }
			
        /// <summary>
        ///  姓名 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "姓名不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string CName { get; set; }
			
        /// <summary>
        ///  联系电话 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "联系电话不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string CContactNumber { get; set; }
			
        /// <summary>
        ///  性别 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "性别不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string CChairperson { get; set; }
			
        /// <summary>
        ///  出生年月 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "出生年月不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string CDateOfBirth { get; set; }
			
        /// <summary>
        ///  地址 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "地址不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string CAddress { get; set; }
			
        /// <summary>
        ///  邮编 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "邮编不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string CZipCode { get; set; }
			
        /// <summary>
        ///  备注 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "备注不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string CRemarks { get; set; }
	}

	
    /// <summary>
    ///  供应商 
    /// </summary>
	[Table("Supplier")]
    public class Supplier 
    {
			        
        /// <summary>
        ///  Supplier编号
        /// </summary>
		[Key]
        public int id { get; set; }
        /// <summary>
        ///  版本号
        /// </summary>
        public int? VersionNo { get; set; }
        /// <summary>
        ///  创建人
        /// </summary>
        public string CreateBy { get; set; }
        /// <summary>
        ///  创建时间
        /// </summary>
        public DateTime? CreateOn { get; set; }
        /// <summary>
        ///  更新时间
        /// </summary>
        public string UpdateBy { get; set; }
        /// <summary>
        ///  更新人
        /// </summary>
        public DateTime? UpdateOn { get; set; }
        /// <summary>
        ///  事务编号
        /// </summary>
        public string TransactionID { get; set; }
        /// <summary>
        ///  是否删除
        /// </summary>
		[Index]
        public int? IsDeleted { get; set; }
        /// <summary>
        ///  数据级别
        /// </summary>
        public string DataLevel { get; set; }
        /// <summary>
        ///  排序
        /// </summary>
        public int? ord { get; set; }
			
        /// <summary>
        ///  供应商编号 INT 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "供应商编号不能为空")]
		
        public Nullable<int> SSupplierNumber { get; set; }
			
        /// <summary>
        ///  供应商名称 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "供应商名称不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string SSupplierName { get; set; }
			
        /// <summary>
        ///  联系电话 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "联系电话不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string SContactNumber { get; set; }
			
        /// <summary>
        ///  办公地点 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "办公地点不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string SOfficeLocation { get; set; }
	}

	
    /// <summary>
    ///  货物 
    /// </summary>
	[Table("Cargo")]
    public class Cargo 
    {
			        
        /// <summary>
        ///  Cargo编号
        /// </summary>
		[Key]
        public int id { get; set; }
        /// <summary>
        ///  版本号
        /// </summary>
        public int? VersionNo { get; set; }
        /// <summary>
        ///  创建人
        /// </summary>
        public string CreateBy { get; set; }
        /// <summary>
        ///  创建时间
        /// </summary>
        public DateTime? CreateOn { get; set; }
        /// <summary>
        ///  更新时间
        /// </summary>
        public string UpdateBy { get; set; }
        /// <summary>
        ///  更新人
        /// </summary>
        public DateTime? UpdateOn { get; set; }
        /// <summary>
        ///  事务编号
        /// </summary>
        public string TransactionID { get; set; }
        /// <summary>
        ///  是否删除
        /// </summary>
		[Index]
        public int? IsDeleted { get; set; }
        /// <summary>
        ///  数据级别
        /// </summary>
        public string DataLevel { get; set; }
        /// <summary>
        ///  排序
        /// </summary>
        public int? ord { get; set; }
			
        /// <summary>
        ///  货物编号 INT 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "货物编号不能为空")]
		
        public Nullable<int> CCargoNumber { get; set; }
			
        /// <summary>
        ///  货物名称 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "货物名称不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string CNameOfGoods { get; set; }
			
        /// <summary>
        ///  型号 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "型号不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string CModel { get; set; }
			
        /// <summary>
        ///  有无配件 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "有无配件不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string CHaveParts { get; set; }
	}

	
    /// <summary>
    ///  员工 
    /// </summary>
	[Table("Staffname")]
    public class Staffname 
    {
			        
        /// <summary>
        ///  Staffname编号
        /// </summary>
		[Key]
        public int id { get; set; }
        /// <summary>
        ///  版本号
        /// </summary>
        public int? VersionNo { get; set; }
        /// <summary>
        ///  创建人
        /// </summary>
        public string CreateBy { get; set; }
        /// <summary>
        ///  创建时间
        /// </summary>
        public DateTime? CreateOn { get; set; }
        /// <summary>
        ///  更新时间
        /// </summary>
        public string UpdateBy { get; set; }
        /// <summary>
        ///  更新人
        /// </summary>
        public DateTime? UpdateOn { get; set; }
        /// <summary>
        ///  事务编号
        /// </summary>
        public string TransactionID { get; set; }
        /// <summary>
        ///  是否删除
        /// </summary>
		[Index]
        public int? IsDeleted { get; set; }
        /// <summary>
        ///  数据级别
        /// </summary>
        public string DataLevel { get; set; }
        /// <summary>
        ///  排序
        /// </summary>
        public int? ord { get; set; }
			
        /// <summary>
        ///  工号 INT 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "工号不能为空")]
		
        public Nullable<int> SJobNumber { get; set; }
			
        /// <summary>
        ///  姓名 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "姓名不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string SName { get; set; }
			
        /// <summary>
        ///  学历 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "学历不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string SEducation { get; set; }
			
        /// <summary>
        ///  联系电话 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "联系电话不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string SContactNumber { get; set; }
	}

	
    /// <summary>
    ///  仓库 
    /// </summary>
	[Table("Warehouse")]
    public class Warehouse 
    {
			        
        /// <summary>
        ///  Warehouse编号
        /// </summary>
		[Key]
        public int id { get; set; }
        /// <summary>
        ///  版本号
        /// </summary>
        public int? VersionNo { get; set; }
        /// <summary>
        ///  创建人
        /// </summary>
        public string CreateBy { get; set; }
        /// <summary>
        ///  创建时间
        /// </summary>
        public DateTime? CreateOn { get; set; }
        /// <summary>
        ///  更新时间
        /// </summary>
        public string UpdateBy { get; set; }
        /// <summary>
        ///  更新人
        /// </summary>
        public DateTime? UpdateOn { get; set; }
        /// <summary>
        ///  事务编号
        /// </summary>
        public string TransactionID { get; set; }
        /// <summary>
        ///  是否删除
        /// </summary>
		[Index]
        public int? IsDeleted { get; set; }
        /// <summary>
        ///  数据级别
        /// </summary>
        public string DataLevel { get; set; }
        /// <summary>
        ///  排序
        /// </summary>
        public int? ord { get; set; }
			
        /// <summary>
        ///  仓库编号 INT 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "仓库编号不能为空")]
		
        public Nullable<int> WWarehouseNumber { get; set; }
			
        /// <summary>
        ///  容量 INT 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "容量不能为空")]
		
        public Nullable<int> WCapacity { get; set; }
			
        /// <summary>
        ///  地点 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "地点不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string WLocality { get; set; }
	}

	
    /// <summary>
    ///  货架 
    /// </summary>
	[Table("GoodsShelves")]
    public class GoodsShelves 
    {
			        
        /// <summary>
        ///  GoodsShelves编号
        /// </summary>
		[Key]
        public int id { get; set; }
        /// <summary>
        ///  版本号
        /// </summary>
        public int? VersionNo { get; set; }
        /// <summary>
        ///  创建人
        /// </summary>
        public string CreateBy { get; set; }
        /// <summary>
        ///  创建时间
        /// </summary>
        public DateTime? CreateOn { get; set; }
        /// <summary>
        ///  更新时间
        /// </summary>
        public string UpdateBy { get; set; }
        /// <summary>
        ///  更新人
        /// </summary>
        public DateTime? UpdateOn { get; set; }
        /// <summary>
        ///  事务编号
        /// </summary>
        public string TransactionID { get; set; }
        /// <summary>
        ///  是否删除
        /// </summary>
		[Index]
        public int? IsDeleted { get; set; }
        /// <summary>
        ///  数据级别
        /// </summary>
        public string DataLevel { get; set; }
        /// <summary>
        ///  排序
        /// </summary>
        public int? ord { get; set; }
			
        /// <summary>
        ///  货架编号 INT 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "货架编号不能为空")]
		
        public Nullable<int> GSShelfNumber { get; set; }
			
        /// <summary>
        ///  容量 INT 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "容量不能为空")]
		
        public Nullable<int> GSCapacity { get; set; }
			
        /// <summary>
        ///  地点 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "地点不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string GSLocality { get; set; }
	}

	
    /// <summary>
    ///  采购单 
    /// </summary>
	[Table("PurchaseUnitPrice")]
    public class PurchaseUnitPrice 
    {
			        
        /// <summary>
        ///  PurchaseUnitPrice编号
        /// </summary>
		[Key]
        public int id { get; set; }
        /// <summary>
        ///  版本号
        /// </summary>
        public int? VersionNo { get; set; }
        /// <summary>
        ///  创建人
        /// </summary>
        public string CreateBy { get; set; }
        /// <summary>
        ///  创建时间
        /// </summary>
        public DateTime? CreateOn { get; set; }
        /// <summary>
        ///  更新时间
        /// </summary>
        public string UpdateBy { get; set; }
        /// <summary>
        ///  更新人
        /// </summary>
        public DateTime? UpdateOn { get; set; }
        /// <summary>
        ///  事务编号
        /// </summary>
        public string TransactionID { get; set; }
        /// <summary>
        ///  是否删除
        /// </summary>
		[Index]
        public int? IsDeleted { get; set; }
        /// <summary>
        ///  数据级别
        /// </summary>
        public string DataLevel { get; set; }
        /// <summary>
        ///  排序
        /// </summary>
        public int? ord { get; set; }
			
        /// <summary>
        ///  供应商编号 INT 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "供应商编号不能为空")]
		
        public Nullable<int> PUPSupplierNumber { get; set; }
			
        /// <summary>
        ///  货物编号 INT 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "货物编号不能为空")]
		
        public Nullable<int> PUPCargoNumber { get; set; }
			
        /// <summary>
        ///  采购员工号 INT 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "采购员工号不能为空")]
		
        public Nullable<int> PUPPurchasingStaffNumber { get; set; }
			
        /// <summary>
        ///  日期 DATETIME 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "日期不能为空")]
				//[RelativeDateTimeValidator(-140, DateTimeUnit.Year, 0, DateTimeUnit.Second, MessageTemplate = "日期格式错误")]
		
        public Nullable<DateTime> PUPDate { get; set; }
			
        /// <summary>
        ///  数量 INT 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "数量不能为空")]
		
        public Nullable<int> PUPAmount { get; set; }
			
        /// <summary>
        ///  价格 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "价格不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string PUPPrice { get; set; }
			
        /// <summary>
        ///  备注 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "备注不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string PUPRemarks { get; set; }
	}

	
    /// <summary>
    ///  销售单 
    /// </summary>
	[Table("SalesUnitPrice")]
    public class SalesUnitPrice 
    {
			        
        /// <summary>
        ///  SalesUnitPrice编号
        /// </summary>
		[Key]
        public int id { get; set; }
        /// <summary>
        ///  版本号
        /// </summary>
        public int? VersionNo { get; set; }
        /// <summary>
        ///  创建人
        /// </summary>
        public string CreateBy { get; set; }
        /// <summary>
        ///  创建时间
        /// </summary>
        public DateTime? CreateOn { get; set; }
        /// <summary>
        ///  更新时间
        /// </summary>
        public string UpdateBy { get; set; }
        /// <summary>
        ///  更新人
        /// </summary>
        public DateTime? UpdateOn { get; set; }
        /// <summary>
        ///  事务编号
        /// </summary>
        public string TransactionID { get; set; }
        /// <summary>
        ///  是否删除
        /// </summary>
		[Index]
        public int? IsDeleted { get; set; }
        /// <summary>
        ///  数据级别
        /// </summary>
        public string DataLevel { get; set; }
        /// <summary>
        ///  排序
        /// </summary>
        public int? ord { get; set; }
			
        /// <summary>
        ///  货物编号 INT 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "货物编号不能为空")]
		
        public Nullable<int> SUPCargoNumber { get; set; }
			
        /// <summary>
        ///  客户编号 INT 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "客户编号不能为空")]
		
        public Nullable<int> SUPCustomerNumber { get; set; }
			
        /// <summary>
        ///  销售员工号 INT 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "销售员工号不能为空")]
		
        public Nullable<int> SUPSalesStaffNumber { get; set; }
			
        /// <summary>
        ///  日期 DATETIME 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "日期不能为空")]
				//[RelativeDateTimeValidator(-140, DateTimeUnit.Year, 0, DateTimeUnit.Second, MessageTemplate = "日期格式错误")]
		
        public Nullable<DateTime> SUPDate { get; set; }
			
        /// <summary>
        ///  数量 INT 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "数量不能为空")]
		
        public Nullable<int> SUPAmount { get; set; }
			
        /// <summary>
        ///  价格 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "价格不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string SUPPrice { get; set; }
			
        /// <summary>
        ///  备注 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "备注不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string SUPRemarks { get; set; }
	}

	
    /// <summary>
    ///  供应入库单 
    /// </summary>
	[Table("SupplyWarehousingList")]
    public class SupplyWarehousingList 
    {
			        
        /// <summary>
        ///  SupplyWarehousingList编号
        /// </summary>
		[Key]
        public int id { get; set; }
        /// <summary>
        ///  版本号
        /// </summary>
        public int? VersionNo { get; set; }
        /// <summary>
        ///  创建人
        /// </summary>
        public string CreateBy { get; set; }
        /// <summary>
        ///  创建时间
        /// </summary>
        public DateTime? CreateOn { get; set; }
        /// <summary>
        ///  更新时间
        /// </summary>
        public string UpdateBy { get; set; }
        /// <summary>
        ///  更新人
        /// </summary>
        public DateTime? UpdateOn { get; set; }
        /// <summary>
        ///  事务编号
        /// </summary>
        public string TransactionID { get; set; }
        /// <summary>
        ///  是否删除
        /// </summary>
		[Index]
        public int? IsDeleted { get; set; }
        /// <summary>
        ///  数据级别
        /// </summary>
        public string DataLevel { get; set; }
        /// <summary>
        ///  排序
        /// </summary>
        public int? ord { get; set; }
			
        /// <summary>
        ///  仓库编号 INT 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "仓库编号不能为空")]
		
        public Nullable<int> SWLWarehouseNumber { get; set; }
			
        /// <summary>
        ///  货物编号 INT 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "货物编号不能为空")]
		
        public Nullable<int> SWLCargoNumber { get; set; }
			
        /// <summary>
        ///  供应商编号 INT 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "供应商编号不能为空")]
		
        public Nullable<int> SWLSupplierNumber { get; set; }
			
        /// <summary>
        ///  日期 DATETIME 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "日期不能为空")]
				//[RelativeDateTimeValidator(-140, DateTimeUnit.Year, 0, DateTimeUnit.Second, MessageTemplate = "日期格式错误")]
		
        public Nullable<DateTime> SWLDate { get; set; }
			
        /// <summary>
        ///  数量 INT 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "数量不能为空")]
		
        public Nullable<int> SWLAmount { get; set; }
			
        /// <summary>
        ///  备注 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "备注不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string SWLRemarks { get; set; }
	}

	
    /// <summary>
    ///  补货单 
    /// </summary>
	[Table("ReplenishmentBill")]
    public class ReplenishmentBill 
    {
			        
        /// <summary>
        ///  ReplenishmentBill编号
        /// </summary>
		[Key]
        public int id { get; set; }
        /// <summary>
        ///  版本号
        /// </summary>
        public int? VersionNo { get; set; }
        /// <summary>
        ///  创建人
        /// </summary>
        public string CreateBy { get; set; }
        /// <summary>
        ///  创建时间
        /// </summary>
        public DateTime? CreateOn { get; set; }
        /// <summary>
        ///  更新时间
        /// </summary>
        public string UpdateBy { get; set; }
        /// <summary>
        ///  更新人
        /// </summary>
        public DateTime? UpdateOn { get; set; }
        /// <summary>
        ///  事务编号
        /// </summary>
        public string TransactionID { get; set; }
        /// <summary>
        ///  是否删除
        /// </summary>
		[Index]
        public int? IsDeleted { get; set; }
        /// <summary>
        ///  数据级别
        /// </summary>
        public string DataLevel { get; set; }
        /// <summary>
        ///  排序
        /// </summary>
        public int? ord { get; set; }
			
        /// <summary>
        ///  货架编号 INT 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "货架编号不能为空")]
		
        public Nullable<int> RBShelfNumber { get; set; }
			
        /// <summary>
        ///  仓库编号 INT 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "仓库编号不能为空")]
		
        public Nullable<int> RBWarehouseNumber { get; set; }
			
        /// <summary>
        ///  货物编号 INT 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "货物编号不能为空")]
		
        public Nullable<int> RBCargoNumber { get; set; }
			
        /// <summary>
        ///  日期 DATETIME 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "日期不能为空")]
				//[RelativeDateTimeValidator(-140, DateTimeUnit.Year, 0, DateTimeUnit.Second, MessageTemplate = "日期格式错误")]
		
        public Nullable<DateTime> RBDate { get; set; }
			
        /// <summary>
        ///  数量 INT 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "数量不能为空")]
		
        public Nullable<int> RBAmount { get; set; }
			
        /// <summary>
        ///  备注 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "备注不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string RBRemarks { get; set; }
	}

	
    /// <summary>
    ///  仓库库存 
    /// </summary>
	[Table("WarehouseStock")]
    public class WarehouseStock 
    {
			        
        /// <summary>
        ///  WarehouseStock编号
        /// </summary>
		[Key]
        public int id { get; set; }
        /// <summary>
        ///  版本号
        /// </summary>
        public int? VersionNo { get; set; }
        /// <summary>
        ///  创建人
        /// </summary>
        public string CreateBy { get; set; }
        /// <summary>
        ///  创建时间
        /// </summary>
        public DateTime? CreateOn { get; set; }
        /// <summary>
        ///  更新时间
        /// </summary>
        public string UpdateBy { get; set; }
        /// <summary>
        ///  更新人
        /// </summary>
        public DateTime? UpdateOn { get; set; }
        /// <summary>
        ///  事务编号
        /// </summary>
        public string TransactionID { get; set; }
        /// <summary>
        ///  是否删除
        /// </summary>
		[Index]
        public int? IsDeleted { get; set; }
        /// <summary>
        ///  数据级别
        /// </summary>
        public string DataLevel { get; set; }
        /// <summary>
        ///  排序
        /// </summary>
        public int? ord { get; set; }
			
        /// <summary>
        ///  仓库编号 INT 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "仓库编号不能为空")]
		
        public Nullable<int> WSWarehouseNumber { get; set; }
			
        /// <summary>
        ///  货物编号 INT 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "货物编号不能为空")]
		
        public Nullable<int> WSCargoNumber { get; set; }
			
        /// <summary>
        ///  数量 INT 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "数量不能为空")]
		
        public Nullable<int> WSAmount { get; set; }
			
        /// <summary>
        ///  备注 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "备注不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string WSRemarks { get; set; }
	}

	
    /// <summary>
    ///  货架库存 
    /// </summary>
	[Table("ShelfStock")]
    public class ShelfStock 
    {
			        
        /// <summary>
        ///  ShelfStock编号
        /// </summary>
		[Key]
        public int id { get; set; }
        /// <summary>
        ///  版本号
        /// </summary>
        public int? VersionNo { get; set; }
        /// <summary>
        ///  创建人
        /// </summary>
        public string CreateBy { get; set; }
        /// <summary>
        ///  创建时间
        /// </summary>
        public DateTime? CreateOn { get; set; }
        /// <summary>
        ///  更新时间
        /// </summary>
        public string UpdateBy { get; set; }
        /// <summary>
        ///  更新人
        /// </summary>
        public DateTime? UpdateOn { get; set; }
        /// <summary>
        ///  事务编号
        /// </summary>
        public string TransactionID { get; set; }
        /// <summary>
        ///  是否删除
        /// </summary>
		[Index]
        public int? IsDeleted { get; set; }
        /// <summary>
        ///  数据级别
        /// </summary>
        public string DataLevel { get; set; }
        /// <summary>
        ///  排序
        /// </summary>
        public int? ord { get; set; }
			
        /// <summary>
        ///  货架编号 INT 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "货架编号不能为空")]
		
        public Nullable<int> SSShelfNumber { get; set; }
			
        /// <summary>
        ///  货物编号 INT 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "货物编号不能为空")]
		
        public Nullable<int> SSCargoNumber { get; set; }
			
        /// <summary>
        ///  数量 INT 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "数量不能为空")]
		
        public Nullable<int> SSAmount { get; set; }
			
        /// <summary>
        ///  备注 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "备注不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string SSRemarks { get; set; }
	}

	
    /// <summary>
    ///  上架单 
    /// </summary>
	[Table("ShelfList")]
    public class ShelfList 
    {
			        
        /// <summary>
        ///  ShelfList编号
        /// </summary>
		[Key]
        public int id { get; set; }
        /// <summary>
        ///  版本号
        /// </summary>
        public int? VersionNo { get; set; }
        /// <summary>
        ///  创建人
        /// </summary>
        public string CreateBy { get; set; }
        /// <summary>
        ///  创建时间
        /// </summary>
        public DateTime? CreateOn { get; set; }
        /// <summary>
        ///  更新时间
        /// </summary>
        public string UpdateBy { get; set; }
        /// <summary>
        ///  更新人
        /// </summary>
        public DateTime? UpdateOn { get; set; }
        /// <summary>
        ///  事务编号
        /// </summary>
        public string TransactionID { get; set; }
        /// <summary>
        ///  是否删除
        /// </summary>
		[Index]
        public int? IsDeleted { get; set; }
        /// <summary>
        ///  数据级别
        /// </summary>
        public string DataLevel { get; set; }
        /// <summary>
        ///  排序
        /// </summary>
        public int? ord { get; set; }
			
        /// <summary>
        ///  货架编号 INT 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "货架编号不能为空")]
		
        public Nullable<int> SLShelfNumber { get; set; }
			
        /// <summary>
        ///  货物编号 INT 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "货物编号不能为空")]
		
        public Nullable<int> SLCargoNumber { get; set; }
			
        /// <summary>
        ///  日期 DATETIME 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "日期不能为空")]
				//[RelativeDateTimeValidator(-140, DateTimeUnit.Year, 0, DateTimeUnit.Second, MessageTemplate = "日期格式错误")]
		
        public Nullable<DateTime> SLDate { get; set; }
			
        /// <summary>
        ///  数量 INT 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "数量不能为空")]
		
        public Nullable<int> SLAmount { get; set; }
			
        /// <summary>
        ///  备注 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "备注不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string SLRemarks { get; set; }
	}

	
    /// <summary>
    ///  采购凭证单 
    /// </summary>
	[Table("PurchaseVoucher")]
    public class PurchaseVoucher 
    {
			        
        /// <summary>
        ///  PurchaseVoucher编号
        /// </summary>
		[Key]
        public int id { get; set; }
        /// <summary>
        ///  版本号
        /// </summary>
        public int? VersionNo { get; set; }
        /// <summary>
        ///  创建人
        /// </summary>
        public string CreateBy { get; set; }
        /// <summary>
        ///  创建时间
        /// </summary>
        public DateTime? CreateOn { get; set; }
        /// <summary>
        ///  更新时间
        /// </summary>
        public string UpdateBy { get; set; }
        /// <summary>
        ///  更新人
        /// </summary>
        public DateTime? UpdateOn { get; set; }
        /// <summary>
        ///  事务编号
        /// </summary>
        public string TransactionID { get; set; }
        /// <summary>
        ///  是否删除
        /// </summary>
		[Index]
        public int? IsDeleted { get; set; }
        /// <summary>
        ///  数据级别
        /// </summary>
        public string DataLevel { get; set; }
        /// <summary>
        ///  排序
        /// </summary>
        public int? ord { get; set; }
			
        /// <summary>
        ///  供应商编号 INT 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "供应商编号不能为空")]
		
        public Nullable<int> PVSupplierNumber { get; set; }
			
        /// <summary>
        ///  货物编号 INT 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "货物编号不能为空")]
		
        public Nullable<int> PVCargoNumber { get; set; }
			
        /// <summary>
        ///  工号 INT 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "工号不能为空")]
		
        public Nullable<int> PVJobNumber { get; set; }
			
        /// <summary>
        ///  日期 DATETIME 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "日期不能为空")]
				//[RelativeDateTimeValidator(-140, DateTimeUnit.Year, 0, DateTimeUnit.Second, MessageTemplate = "日期格式错误")]
		
        public Nullable<DateTime> PVDate { get; set; }
			
        /// <summary>
        ///  数量 INT 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "数量不能为空")]
		
        public Nullable<int> PVAmount { get; set; }
			
        /// <summary>
        ///  价格 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "价格不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string PVPrice { get; set; }
			
        /// <summary>
        ///  备注 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "备注不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string PVRemarks { get; set; }
	}

	
    /// <summary>
    ///  出库单 
    /// </summary>
	[Table("OutboundOrder")]
    public class OutboundOrder 
    {
			        
        /// <summary>
        ///  OutboundOrder编号
        /// </summary>
		[Key]
        public int id { get; set; }
        /// <summary>
        ///  版本号
        /// </summary>
        public int? VersionNo { get; set; }
        /// <summary>
        ///  创建人
        /// </summary>
        public string CreateBy { get; set; }
        /// <summary>
        ///  创建时间
        /// </summary>
        public DateTime? CreateOn { get; set; }
        /// <summary>
        ///  更新时间
        /// </summary>
        public string UpdateBy { get; set; }
        /// <summary>
        ///  更新人
        /// </summary>
        public DateTime? UpdateOn { get; set; }
        /// <summary>
        ///  事务编号
        /// </summary>
        public string TransactionID { get; set; }
        /// <summary>
        ///  是否删除
        /// </summary>
		[Index]
        public int? IsDeleted { get; set; }
        /// <summary>
        ///  数据级别
        /// </summary>
        public string DataLevel { get; set; }
        /// <summary>
        ///  排序
        /// </summary>
        public int? ord { get; set; }
			
        /// <summary>
        ///  仓库编号 INT 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "仓库编号不能为空")]
		
        public Nullable<int> OOWarehouseNumber { get; set; }
			
        /// <summary>
        ///  货物编号 INT 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "货物编号不能为空")]
		
        public Nullable<int> OOCargoNumber { get; set; }
			
        /// <summary>
        ///  工号 INT 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "工号不能为空")]
		
        public Nullable<int> OOJobNumber { get; set; }
			
        /// <summary>
        ///  日期 DATETIME 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "日期不能为空")]
				//[RelativeDateTimeValidator(-140, DateTimeUnit.Year, 0, DateTimeUnit.Second, MessageTemplate = "日期格式错误")]
		
        public Nullable<DateTime> OODate { get; set; }
			
        /// <summary>
        ///  数量 INT 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "数量不能为空")]
		
        public Nullable<int> OOAmount { get; set; }
			
        /// <summary>
        ///  备注 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "备注不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string OORemarks { get; set; }
	}

	
    /// <summary>
    ///  下架单 
    /// </summary>
	[Table("LowerFrame")]
    public class LowerFrame 
    {
			        
        /// <summary>
        ///  LowerFrame编号
        /// </summary>
		[Key]
        public int id { get; set; }
        /// <summary>
        ///  版本号
        /// </summary>
        public int? VersionNo { get; set; }
        /// <summary>
        ///  创建人
        /// </summary>
        public string CreateBy { get; set; }
        /// <summary>
        ///  创建时间
        /// </summary>
        public DateTime? CreateOn { get; set; }
        /// <summary>
        ///  更新时间
        /// </summary>
        public string UpdateBy { get; set; }
        /// <summary>
        ///  更新人
        /// </summary>
        public DateTime? UpdateOn { get; set; }
        /// <summary>
        ///  事务编号
        /// </summary>
        public string TransactionID { get; set; }
        /// <summary>
        ///  是否删除
        /// </summary>
		[Index]
        public int? IsDeleted { get; set; }
        /// <summary>
        ///  数据级别
        /// </summary>
        public string DataLevel { get; set; }
        /// <summary>
        ///  排序
        /// </summary>
        public int? ord { get; set; }
			
        /// <summary>
        ///  货架编号 INT 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "货架编号不能为空")]
		
        public Nullable<int> LFShelfNumber { get; set; }
			
        /// <summary>
        ///  货物编号 INT 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "货物编号不能为空")]
		
        public Nullable<int> LFCargoNumber { get; set; }
			
        /// <summary>
        ///  工号 INT 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "工号不能为空")]
		
        public Nullable<int> LFJobNumber { get; set; }
			
        /// <summary>
        ///  日期 DATETIME 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "日期不能为空")]
				//[RelativeDateTimeValidator(-140, DateTimeUnit.Year, 0, DateTimeUnit.Second, MessageTemplate = "日期格式错误")]
		
        public Nullable<DateTime> LFDate { get; set; }
			
        /// <summary>
        ///  数量 INT 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "数量不能为空")]
		
        public Nullable<int> LFAmount { get; set; }
			
        /// <summary>
        ///  备注 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "备注不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string LFRemarks { get; set; }
	}

	
    /// <summary>
    ///  销售凭证单 
    /// </summary>
	[Table("SalesVoucher")]
    public class SalesVoucher 
    {
			        
        /// <summary>
        ///  SalesVoucher编号
        /// </summary>
		[Key]
        public int id { get; set; }
        /// <summary>
        ///  版本号
        /// </summary>
        public int? VersionNo { get; set; }
        /// <summary>
        ///  创建人
        /// </summary>
        public string CreateBy { get; set; }
        /// <summary>
        ///  创建时间
        /// </summary>
        public DateTime? CreateOn { get; set; }
        /// <summary>
        ///  更新时间
        /// </summary>
        public string UpdateBy { get; set; }
        /// <summary>
        ///  更新人
        /// </summary>
        public DateTime? UpdateOn { get; set; }
        /// <summary>
        ///  事务编号
        /// </summary>
        public string TransactionID { get; set; }
        /// <summary>
        ///  是否删除
        /// </summary>
		[Index]
        public int? IsDeleted { get; set; }
        /// <summary>
        ///  数据级别
        /// </summary>
        public string DataLevel { get; set; }
        /// <summary>
        ///  排序
        /// </summary>
        public int? ord { get; set; }
			
        /// <summary>
        ///  客户编号 INT 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "客户编号不能为空")]
		
        public Nullable<int> SVCustomerNumber { get; set; }
			
        /// <summary>
        ///  货物编号 INT 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "货物编号不能为空")]
		
        public Nullable<int> SVCargoNumber { get; set; }
			
        /// <summary>
        ///  工号 INT 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "工号不能为空")]
		
        public Nullable<int> SVJobNumber { get; set; }
			
        /// <summary>
        ///  日期 DATETIME 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "日期不能为空")]
				//[RelativeDateTimeValidator(-140, DateTimeUnit.Year, 0, DateTimeUnit.Second, MessageTemplate = "日期格式错误")]
		
        public Nullable<DateTime> SVDate { get; set; }
			
        /// <summary>
        ///  数量 INT 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "数量不能为空")]
		
        public Nullable<int> SVAmount { get; set; }
			
        /// <summary>
        ///  价格 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "价格不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string SVPrice { get; set; }
			
        /// <summary>
        ///  备注 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "备注不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string SVRemarks { get; set; }
	}

	
    /// <summary>
    ///  菜单配置 
    /// </summary>
	[Table("MenuConfiguration")]
    public class MenuConfiguration 
    {
			        
        /// <summary>
        ///  MenuConfiguration编号
        /// </summary>
		[Key]
        public int id { get; set; }
        /// <summary>
        ///  版本号
        /// </summary>
        public int? VersionNo { get; set; }
        /// <summary>
        ///  创建人
        /// </summary>
        public string CreateBy { get; set; }
        /// <summary>
        ///  创建时间
        /// </summary>
        public DateTime? CreateOn { get; set; }
        /// <summary>
        ///  更新时间
        /// </summary>
        public string UpdateBy { get; set; }
        /// <summary>
        ///  更新人
        /// </summary>
        public DateTime? UpdateOn { get; set; }
        /// <summary>
        ///  事务编号
        /// </summary>
        public string TransactionID { get; set; }
        /// <summary>
        ///  是否删除
        /// </summary>
		[Index]
        public int? IsDeleted { get; set; }
        /// <summary>
        ///  数据级别
        /// </summary>
        public string DataLevel { get; set; }
        /// <summary>
        ///  排序
        /// </summary>
        public int? ord { get; set; }
			
        /// <summary>
        ///  标题 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "标题不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string MCCaption { get; set; }
			
        /// <summary>
        ///  父级标题 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "父级标题不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string MCParentTitle { get; set; }
			
        /// <summary>
        ///  链接 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "链接不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string MCLink { get; set; }
			
        /// <summary>
        ///  菜单类型 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "菜单类型不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string MCMenuType { get; set; }
			
        /// <summary>
        ///  顺序 INT 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "顺序不能为空")]
		
        public Nullable<int> MCSequence { get; set; }
			
        /// <summary>
        ///  显示名称 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "显示名称不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string MCDisplayName { get; set; }
			
        /// <summary>
        ///  图片 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "图片不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string MCPicture { get; set; }
	}

	
    /// <summary>
    ///  角色菜单 
    /// </summary>
	[Table("RoleMenu")]
    public class RoleMenu 
    {
			        
        /// <summary>
        ///  RoleMenu编号
        /// </summary>
		[Key]
        public int id { get; set; }
        /// <summary>
        ///  版本号
        /// </summary>
        public int? VersionNo { get; set; }
        /// <summary>
        ///  创建人
        /// </summary>
        public string CreateBy { get; set; }
        /// <summary>
        ///  创建时间
        /// </summary>
        public DateTime? CreateOn { get; set; }
        /// <summary>
        ///  更新时间
        /// </summary>
        public string UpdateBy { get; set; }
        /// <summary>
        ///  更新人
        /// </summary>
        public DateTime? UpdateOn { get; set; }
        /// <summary>
        ///  事务编号
        /// </summary>
        public string TransactionID { get; set; }
        /// <summary>
        ///  是否删除
        /// </summary>
		[Index]
        public int? IsDeleted { get; set; }
        /// <summary>
        ///  数据级别
        /// </summary>
        public string DataLevel { get; set; }
        /// <summary>
        ///  排序
        /// </summary>
        public int? ord { get; set; }
			
        /// <summary>
        ///  角色名称 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "角色名称不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string RMRoleName { get; set; }
			
        /// <summary>
        ///  菜单标题 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "菜单标题不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string RMMenuTitle { get; set; }
	}

	
    /// <summary>
    ///  用户角色 
    /// </summary>
	[Table("UserRole")]
    public class UserRole 
    {
			        
        /// <summary>
        ///  UserRole编号
        /// </summary>
		[Key]
        public int id { get; set; }
        /// <summary>
        ///  版本号
        /// </summary>
        public int? VersionNo { get; set; }
        /// <summary>
        ///  创建人
        /// </summary>
        public string CreateBy { get; set; }
        /// <summary>
        ///  创建时间
        /// </summary>
        public DateTime? CreateOn { get; set; }
        /// <summary>
        ///  更新时间
        /// </summary>
        public string UpdateBy { get; set; }
        /// <summary>
        ///  更新人
        /// </summary>
        public DateTime? UpdateOn { get; set; }
        /// <summary>
        ///  事务编号
        /// </summary>
        public string TransactionID { get; set; }
        /// <summary>
        ///  是否删除
        /// </summary>
		[Index]
        public int? IsDeleted { get; set; }
        /// <summary>
        ///  数据级别
        /// </summary>
        public string DataLevel { get; set; }
        /// <summary>
        ///  排序
        /// </summary>
        public int? ord { get; set; }
			
        /// <summary>
        ///  角色名称 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "角色名称不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string URRoleName { get; set; }
			
        /// <summary>
        ///  登录名 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "登录名不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string URLoginName { get; set; }
	}

	
    /// <summary>
    ///  角色配置 
    /// </summary>
	[Table("RoleConfiguration")]
    public class RoleConfiguration 
    {
			        
        /// <summary>
        ///  RoleConfiguration编号
        /// </summary>
		[Key]
        public int id { get; set; }
        /// <summary>
        ///  版本号
        /// </summary>
        public int? VersionNo { get; set; }
        /// <summary>
        ///  创建人
        /// </summary>
        public string CreateBy { get; set; }
        /// <summary>
        ///  创建时间
        /// </summary>
        public DateTime? CreateOn { get; set; }
        /// <summary>
        ///  更新时间
        /// </summary>
        public string UpdateBy { get; set; }
        /// <summary>
        ///  更新人
        /// </summary>
        public DateTime? UpdateOn { get; set; }
        /// <summary>
        ///  事务编号
        /// </summary>
        public string TransactionID { get; set; }
        /// <summary>
        ///  是否删除
        /// </summary>
		[Index]
        public int? IsDeleted { get; set; }
        /// <summary>
        ///  数据级别
        /// </summary>
        public string DataLevel { get; set; }
        /// <summary>
        ///  排序
        /// </summary>
        public int? ord { get; set; }
			
        /// <summary>
        ///  角色名称 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "角色名称不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string RCRoleName { get; set; }
			
        /// <summary>
        ///  所属组织 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "所属组织不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string RCAffiliatedOrganization { get; set; }
	}

	
    /// <summary>
    ///  用户信息 
    /// </summary>
	[Table("UserInformation")]
    public class UserInformation 
    {
			        
        /// <summary>
        ///  UserInformation编号
        /// </summary>
		[Key]
        public int id { get; set; }
        /// <summary>
        ///  版本号
        /// </summary>
        public int? VersionNo { get; set; }
        /// <summary>
        ///  创建人
        /// </summary>
        public string CreateBy { get; set; }
        /// <summary>
        ///  创建时间
        /// </summary>
        public DateTime? CreateOn { get; set; }
        /// <summary>
        ///  更新时间
        /// </summary>
        public string UpdateBy { get; set; }
        /// <summary>
        ///  更新人
        /// </summary>
        public DateTime? UpdateOn { get; set; }
        /// <summary>
        ///  事务编号
        /// </summary>
        public string TransactionID { get; set; }
        /// <summary>
        ///  是否删除
        /// </summary>
		[Index]
        public int? IsDeleted { get; set; }
        /// <summary>
        ///  数据级别
        /// </summary>
        public string DataLevel { get; set; }
        /// <summary>
        ///  排序
        /// </summary>
        public int? ord { get; set; }
			
        /// <summary>
        ///  工号 INT 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "工号不能为空")]
		
        public Nullable<int> UIJobNumber { get; set; }
			
        /// <summary>
        ///  登录名 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "登录名不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string UILoginName { get; set; }
			
        /// <summary>
        ///  昵称 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "昵称不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string UINickname { get; set; }
			
        /// <summary>
        ///  真实姓名 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "真实姓名不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string UIRealName { get; set; }
			
        /// <summary>
        ///  头像 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "头像不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string UIHeadPortrait { get; set; }
			
        /// <summary>
        ///  部门 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "部门不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string UIDepartment { get; set; }
			
        /// <summary>
        ///  职位 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "职位不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string UIPost { get; set; }
			
        /// <summary>
        ///  电话 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "电话不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string UIBooth { get; set; }
			
        /// <summary>
        ///  照片 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "照片不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string UIPhoto { get; set; }
			
        /// <summary>
        ///  用户类型 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "用户类型不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string UICustomerType { get; set; }
			
        /// <summary>
        ///  用户级别 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "用户级别不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string UIUserLevel { get; set; }
			
        /// <summary>
        ///  入职时间 DATETIME 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "入职时间不能为空")]
				//[RelativeDateTimeValidator(-140, DateTimeUnit.Year, 0, DateTimeUnit.Second, MessageTemplate = "日期格式错误")]
		
        public Nullable<DateTime> UITimeOfEntry { get; set; }
			
        /// <summary>
        ///  离职时间 DATETIME 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "离职时间不能为空")]
				//[RelativeDateTimeValidator(-140, DateTimeUnit.Year, 0, DateTimeUnit.Second, MessageTemplate = "日期格式错误")]
		
        public Nullable<DateTime> UIDepartureTime { get; set; }
			
        /// <summary>
        ///  密码 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "密码不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string UICode { get; set; }
	}

	
    /// <summary>
    ///  登录记录 
    /// </summary>
	[Table("LogonRecord")]
    public class LogonRecord 
    {
			        
        /// <summary>
        ///  LogonRecord编号
        /// </summary>
		[Key]
        public int id { get; set; }
        /// <summary>
        ///  版本号
        /// </summary>
        public int? VersionNo { get; set; }
        /// <summary>
        ///  创建人
        /// </summary>
        public string CreateBy { get; set; }
        /// <summary>
        ///  创建时间
        /// </summary>
        public DateTime? CreateOn { get; set; }
        /// <summary>
        ///  更新时间
        /// </summary>
        public string UpdateBy { get; set; }
        /// <summary>
        ///  更新人
        /// </summary>
        public DateTime? UpdateOn { get; set; }
        /// <summary>
        ///  事务编号
        /// </summary>
        public string TransactionID { get; set; }
        /// <summary>
        ///  是否删除
        /// </summary>
		[Index]
        public int? IsDeleted { get; set; }
        /// <summary>
        ///  数据级别
        /// </summary>
        public string DataLevel { get; set; }
        /// <summary>
        ///  排序
        /// </summary>
        public int? ord { get; set; }
			
        /// <summary>
        ///  登录名 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "登录名不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string LRLoginName { get; set; }
			
        /// <summary>
        ///  登录时间 DATETIME 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "登录时间不能为空")]
				//[RelativeDateTimeValidator(-140, DateTimeUnit.Year, 0, DateTimeUnit.Second, MessageTemplate = "日期格式错误")]
		
        public Nullable<DateTime> LRLoginTime { get; set; }
	}

	
    /// <summary>
    ///  用户菜单 
    /// </summary>
	[Table("UserMenu")]
    public class UserMenu 
    {
			        
        /// <summary>
        ///  UserMenu编号
        /// </summary>
		[Key]
        public int id { get; set; }
        /// <summary>
        ///  版本号
        /// </summary>
        public int? VersionNo { get; set; }
        /// <summary>
        ///  创建人
        /// </summary>
        public string CreateBy { get; set; }
        /// <summary>
        ///  创建时间
        /// </summary>
        public DateTime? CreateOn { get; set; }
        /// <summary>
        ///  更新时间
        /// </summary>
        public string UpdateBy { get; set; }
        /// <summary>
        ///  更新人
        /// </summary>
        public DateTime? UpdateOn { get; set; }
        /// <summary>
        ///  事务编号
        /// </summary>
        public string TransactionID { get; set; }
        /// <summary>
        ///  是否删除
        /// </summary>
		[Index]
        public int? IsDeleted { get; set; }
        /// <summary>
        ///  数据级别
        /// </summary>
        public string DataLevel { get; set; }
        /// <summary>
        ///  排序
        /// </summary>
        public int? ord { get; set; }
			
        /// <summary>
        ///  登录名 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "登录名不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string UMLoginName { get; set; }
			
        /// <summary>
        ///  标题 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "标题不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string UMCaption { get; set; }
	}

	
    /// <summary>
    ///  系统配置 
    /// </summary>
	[Table("SystemConfiguration")]
    public class SystemConfiguration 
    {
			        
        /// <summary>
        ///  SystemConfiguration编号
        /// </summary>
		[Key]
        public int id { get; set; }
        /// <summary>
        ///  版本号
        /// </summary>
        public int? VersionNo { get; set; }
        /// <summary>
        ///  创建人
        /// </summary>
        public string CreateBy { get; set; }
        /// <summary>
        ///  创建时间
        /// </summary>
        public DateTime? CreateOn { get; set; }
        /// <summary>
        ///  更新时间
        /// </summary>
        public string UpdateBy { get; set; }
        /// <summary>
        ///  更新人
        /// </summary>
        public DateTime? UpdateOn { get; set; }
        /// <summary>
        ///  事务编号
        /// </summary>
        public string TransactionID { get; set; }
        /// <summary>
        ///  是否删除
        /// </summary>
		[Index]
        public int? IsDeleted { get; set; }
        /// <summary>
        ///  数据级别
        /// </summary>
        public string DataLevel { get; set; }
        /// <summary>
        ///  排序
        /// </summary>
        public int? ord { get; set; }
			
        /// <summary>
        ///  键 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "键不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string SCKey { get; set; }
			
        /// <summary>
        ///  值 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "值不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string SCAccrued { get; set; }
	}

}
