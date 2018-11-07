  

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Migrations;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using EF.Entities;

namespace EF.Entities
{
	
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
        ///  经度
        /// </summary>
        public string Longitude { get; set; }
        /// <summary>
        ///  纬度
        /// </summary>
        public string Latitude { get; set; }
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
        ///  经度
        /// </summary>
        public string Longitude { get; set; }
        /// <summary>
        ///  纬度
        /// </summary>
        public string Latitude { get; set; }
        /// <summary>
        ///  排序
        /// </summary>
        public int? ord { get; set; }
			
        /// <summary>
        ///  标题 NVARCHAR(4000) 4000
        /// </summary>
		// [NotNullValidator(MessageTemplate = "标题不能为空")]
		/* [StringLengthValidator(0,4000)] */
        public string MCCaption { get; set; }
			
        /// <summary>
        ///  父级标题 NVARCHAR(4000) 4000
        /// </summary>
		// [NotNullValidator(MessageTemplate = "父级标题不能为空")]
		/* [StringLengthValidator(0,4000)] */
        public string MCParentTitle { get; set; }
			
        /// <summary>
        ///  链接 NVARCHAR(4000) 4000
        /// </summary>
		// [NotNullValidator(MessageTemplate = "链接不能为空")]
		/* [StringLengthValidator(0,4000)] */
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
        ///  图片 NVARCHAR(4000) 4000
        /// </summary>
		// [NotNullValidator(MessageTemplate = "图片不能为空")]
		/* [StringLengthValidator(0,4000)] */
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
        ///  经度
        /// </summary>
        public string Longitude { get; set; }
        /// <summary>
        ///  纬度
        /// </summary>
        public string Latitude { get; set; }
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
        ///  菜单标题 NVARCHAR(4000) 4000
        /// </summary>
		// [NotNullValidator(MessageTemplate = "菜单标题不能为空")]
		/* [StringLengthValidator(0,4000)] */
        public string RMMenuTitle { get; set; }
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
        ///  经度
        /// </summary>
        public string Longitude { get; set; }
        /// <summary>
        ///  纬度
        /// </summary>
        public string Latitude { get; set; }
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
        ///  经度
        /// </summary>
        public string Longitude { get; set; }
        /// <summary>
        ///  纬度
        /// </summary>
        public string Latitude { get; set; }
        /// <summary>
        ///  排序
        /// </summary>
        public int? ord { get; set; }
			
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
        ///  头像 NVARCHAR(4000) 4000
        /// </summary>
		// [NotNullValidator(MessageTemplate = "头像不能为空")]
		/* [StringLengthValidator(0,4000)] */
        public string UIHeadPortrait { get; set; }
			
        /// <summary>
        ///  所属部门 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "所属部门不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string UISubordinateDepartment { get; set; }
			
        /// <summary>
        ///  电话 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "电话不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string UIBooth { get; set; }
			
        /// <summary>
        ///  照片 NVARCHAR(4000) 4000
        /// </summary>
		// [NotNullValidator(MessageTemplate = "照片不能为空")]
		/* [StringLengthValidator(0,4000)] */
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
        ///  经度
        /// </summary>
        public string Longitude { get; set; }
        /// <summary>
        ///  纬度
        /// </summary>
        public string Latitude { get; set; }
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
				[RelativeDateTimeValidator(-140, DateTimeUnit.Year, 0, DateTimeUnit.Second, MessageTemplate = "日期格式错误")]
		
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
        ///  经度
        /// </summary>
        public string Longitude { get; set; }
        /// <summary>
        ///  纬度
        /// </summary>
        public string Latitude { get; set; }
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
        ///  标题 NVARCHAR(4000) 4000
        /// </summary>
		// [NotNullValidator(MessageTemplate = "标题不能为空")]
		/* [StringLengthValidator(0,4000)] */
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
        ///  经度
        /// </summary>
        public string Longitude { get; set; }
        /// <summary>
        ///  纬度
        /// </summary>
        public string Latitude { get; set; }
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
        ///  经度
        /// </summary>
        public string Longitude { get; set; }
        /// <summary>
        ///  纬度
        /// </summary>
        public string Latitude { get; set; }
        /// <summary>
        ///  排序
        /// </summary>
        public int? ord { get; set; }
			
        /// <summary>
        ///  工号 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "工号不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string SJobNumber { get; set; }
			
        /// <summary>
        ///  姓名 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "姓名不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string SName { get; set; }
			
        /// <summary>
        ///  公司 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "公司不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string SAffiliate { get; set; }
			
        /// <summary>
        ///  部门 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "部门不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string SDepartment { get; set; }
			
        /// <summary>
        ///  职位 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "职位不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string SPost { get; set; }
			
        /// <summary>
        ///  联系方式 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "联系方式不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string SCommonModeOfContact { get; set; }
			
        /// <summary>
        ///  备用联系方式 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "备用联系方式不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string SAlternateContactMode { get; set; }
			
        /// <summary>
        ///  入职时间 DATETIME 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "入职时间不能为空")]
				[RelativeDateTimeValidator(-140, DateTimeUnit.Year, 0, DateTimeUnit.Second, MessageTemplate = "日期格式错误")]
		
        public Nullable<DateTime> STimeOfEntry { get; set; }
			
        /// <summary>
        ///  离职时间 DATETIME 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "离职时间不能为空")]
				[RelativeDateTimeValidator(-140, DateTimeUnit.Year, 0, DateTimeUnit.Second, MessageTemplate = "日期格式错误")]
		
        public Nullable<DateTime> SDepartureTime { get; set; }
			
        /// <summary>
        ///  密码 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "密码不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string SCode { get; set; }
	}

	
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
        ///  经度
        /// </summary>
        public string Longitude { get; set; }
        /// <summary>
        ///  纬度
        /// </summary>
        public string Latitude { get; set; }
        /// <summary>
        ///  排序
        /// </summary>
        public int? ord { get; set; }
			
        /// <summary>
        ///  客户编号 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "客户编号不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string CCustomerNumber { get; set; }
			
        /// <summary>
        ///  姓 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "姓不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string CName { get; set; }
			
        /// <summary>
        ///  名 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "名不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string CCard { get; set; }
			
        /// <summary>
        ///  性别 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "性别不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string CChairperson { get; set; }
			
        /// <summary>
        ///  称呼 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "称呼不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string CCall { get; set; }
			
        /// <summary>
        ///  联系方式 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "联系方式不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string CCommonModeOfContact { get; set; }
			
        /// <summary>
        ///  地址 NVARCHAR(4000) 4000
        /// </summary>
		// [NotNullValidator(MessageTemplate = "地址不能为空")]
		/* [StringLengthValidator(0,4000)] */
        public string CAddress { get; set; }
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
        ///  经度
        /// </summary>
        public string Longitude { get; set; }
        /// <summary>
        ///  纬度
        /// </summary>
        public string Latitude { get; set; }
        /// <summary>
        ///  排序
        /// </summary>
        public int? ord { get; set; }
			
        /// <summary>
        ///  供应商编号 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "供应商编号不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string SSupplierNumber { get; set; }
			
        /// <summary>
        ///  供应商名称 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "供应商名称不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string SSupplierName { get; set; }
			
        /// <summary>
        ///  联系方式 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "联系方式不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string SCommonModeOfContact { get; set; }
			
        /// <summary>
        ///  办公地点 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "办公地点不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string SOfficeLocation { get; set; }
			
        /// <summary>
        ///  经营范围 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "经营范围不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string SScopeOfOperation { get; set; }
	}

	
    /// <summary>
    ///  货物种类 
    /// </summary>
	[Table("TypeOfGoods")]
    public class TypeOfGoods 
    {
			        
        /// <summary>
        ///  TypeOfGoods编号
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
        ///  经度
        /// </summary>
        public string Longitude { get; set; }
        /// <summary>
        ///  纬度
        /// </summary>
        public string Latitude { get; set; }
        /// <summary>
        ///  排序
        /// </summary>
        public int? ord { get; set; }
			
        /// <summary>
        ///  货品种类编号 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "货品种类编号不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string TOGCategoryNumberOfGoods { get; set; }
			
        /// <summary>
        ///  货物名称 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "货物名称不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string TOGNameOfGoods { get; set; }
			
        /// <summary>
        ///  货物类别 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "货物类别不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string TOGCategoryOfGoods { get; set; }
			
        /// <summary>
        ///  货物子类别 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "货物子类别不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string TOGCargoSubcategory { get; set; }
			
        /// <summary>
        ///  体积 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "体积不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string TOGBulk { get; set; }
			
        /// <summary>
        ///  颜色 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "颜色不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string TOGColor { get; set; }
			
        /// <summary>
        ///  型号 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "型号不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string TOGModel { get; set; }
			
        /// <summary>
        ///  别称 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "别称不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string TOGAlias { get; set; }
			
        /// <summary>
        ///  采购单价 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "采购单价不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string TOGPurchaseUnitPrice { get; set; }
			
        /// <summary>
        ///  销售单价 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "销售单价不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string TOGSalesUnitPrice { get; set; }
	}

	
    /// <summary>
    ///  供货渠道 
    /// </summary>
	[Table("SupplyChannel")]
    public class SupplyChannel 
    {
			        
        /// <summary>
        ///  SupplyChannel编号
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
        ///  经度
        /// </summary>
        public string Longitude { get; set; }
        /// <summary>
        ///  纬度
        /// </summary>
        public string Latitude { get; set; }
        /// <summary>
        ///  排序
        /// </summary>
        public int? ord { get; set; }
			
        /// <summary>
        ///  供应商 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "供应商不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string SCSupplier { get; set; }
			
        /// <summary>
        ///  货物种类 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "货物种类不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string SCTypeOfGoods { get; set; }
	}

	
    /// <summary>
    ///  订单 
    /// </summary>
	[Table("Order")]
    public class Order 
    {
			        
        /// <summary>
        ///  Order编号
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
        ///  经度
        /// </summary>
        public string Longitude { get; set; }
        /// <summary>
        ///  纬度
        /// </summary>
        public string Latitude { get; set; }
        /// <summary>
        ///  排序
        /// </summary>
        public int? ord { get; set; }
			
        /// <summary>
        ///  订单编号 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "订单编号不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string OOrderNumber { get; set; }
			
        /// <summary>
        ///  供应商编号 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "供应商编号不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string OSupplierNumber { get; set; }
			
        /// <summary>
        ///  期望到达日期 DATETIME 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "期望到达日期不能为空")]
				[RelativeDateTimeValidator(-140, DateTimeUnit.Year, 0, DateTimeUnit.Second, MessageTemplate = "日期格式错误")]
		
        public Nullable<DateTime> OExpectedArrivalDate { get; set; }
			
        /// <summary>
        ///  提交日期 DATETIME 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "提交日期不能为空")]
				[RelativeDateTimeValidator(-140, DateTimeUnit.Year, 0, DateTimeUnit.Second, MessageTemplate = "日期格式错误")]
		
        public Nullable<DateTime> ODateOfSubmission { get; set; }
			
        /// <summary>
        ///  提交人 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "提交人不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string OSubmitter { get; set; }
			
        /// <summary>
        ///  提交人联系方式 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "提交人联系方式不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string OAuthorsContactInformation { get; set; }
			
        /// <summary>
        ///  订单状态 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "订单状态不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string OOrderStatus { get; set; }
			
        /// <summary>
        ///  备注 NVARCHAR(4000) 4000
        /// </summary>
		// [NotNullValidator(MessageTemplate = "备注不能为空")]
		/* [StringLengthValidator(0,4000)] */
        public string ORemarks { get; set; }
	}

	
    /// <summary>
    ///  订单明细 
    /// </summary>
	[Table("OrderDetails")]
    public class OrderDetails 
    {
			        
        /// <summary>
        ///  OrderDetails编号
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
        ///  经度
        /// </summary>
        public string Longitude { get; set; }
        /// <summary>
        ///  纬度
        /// </summary>
        public string Latitude { get; set; }
        /// <summary>
        ///  排序
        /// </summary>
        public int? ord { get; set; }
			
        /// <summary>
        ///  订单编号 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "订单编号不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string ODOrderNumber { get; set; }
			
        /// <summary>
        ///  货物种类 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "货物种类不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string ODTypeOfGoods { get; set; }
			
        /// <summary>
        ///  货物数量 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "货物数量不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string ODQuantityOfGoods { get; set; }
			
        /// <summary>
        ///  采购单价 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "采购单价不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string ODPurchaseUnitPrice { get; set; }
	}

	
    /// <summary>
    ///  入库记录 
    /// </summary>
	[Table("WarehousingRecord")]
    public class WarehousingRecord 
    {
			        
        /// <summary>
        ///  WarehousingRecord编号
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
        ///  经度
        /// </summary>
        public string Longitude { get; set; }
        /// <summary>
        ///  纬度
        /// </summary>
        public string Latitude { get; set; }
        /// <summary>
        ///  排序
        /// </summary>
        public int? ord { get; set; }
			
        /// <summary>
        ///  订单编号 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "订单编号不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string WROrderNumber { get; set; }
			
        /// <summary>
        ///  到货日期 DATETIME 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "到货日期不能为空")]
				[RelativeDateTimeValidator(-140, DateTimeUnit.Year, 0, DateTimeUnit.Second, MessageTemplate = "日期格式错误")]
		
        public Nullable<DateTime> WRDateOfArrival { get; set; }
			
        /// <summary>
        ///  经办人 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "经办人不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string WRAgent { get; set; }
			
        /// <summary>
        ///  经办人联系方式 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "经办人联系方式不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string WROperatorContact { get; set; }
			
        /// <summary>
        ///  备注 NVARCHAR(4000) 4000
        /// </summary>
		// [NotNullValidator(MessageTemplate = "备注不能为空")]
		/* [StringLengthValidator(0,4000)] */
        public string WRRemarks { get; set; }
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
        ///  经度
        /// </summary>
        public string Longitude { get; set; }
        /// <summary>
        ///  纬度
        /// </summary>
        public string Latitude { get; set; }
        /// <summary>
        ///  排序
        /// </summary>
        public int? ord { get; set; }
			
        /// <summary>
        ///  仓库编号 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "仓库编号不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string WWarehouseNumber { get; set; }
			
        /// <summary>
        ///  容积 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "容积不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string WCapacity { get; set; }
			
        /// <summary>
        ///  位置 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "位置不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string WLocation { get; set; }
			
        /// <summary>
        ///  负责人工号 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "负责人工号不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string WResponsibleForManualNumber { get; set; }
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
        ///  经度
        /// </summary>
        public string Longitude { get; set; }
        /// <summary>
        ///  纬度
        /// </summary>
        public string Latitude { get; set; }
        /// <summary>
        ///  排序
        /// </summary>
        public int? ord { get; set; }
			
        /// <summary>
        ///  货架编号 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "货架编号不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string GSShelfNumber { get; set; }
			
        /// <summary>
        ///  容积  NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "容积 不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string GSVolume { get; set; }
			
        /// <summary>
        ///  位置 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "位置不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string GSLocation { get; set; }
			
        /// <summary>
        ///  负责人工号 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "负责人工号不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string GSResponsibleForManualNumber { get; set; }
	}

	
    /// <summary>
    ///  补货申请单 
    /// </summary>
	[Table("ReplenishmentApplicationForm")]
    public class ReplenishmentApplicationForm 
    {
			        
        /// <summary>
        ///  ReplenishmentApplicationForm编号
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
        ///  经度
        /// </summary>
        public string Longitude { get; set; }
        /// <summary>
        ///  纬度
        /// </summary>
        public string Latitude { get; set; }
        /// <summary>
        ///  排序
        /// </summary>
        public int? ord { get; set; }
			
        /// <summary>
        ///  申请单编号 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "申请单编号不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string RAFApplicationNumber { get; set; }
			
        /// <summary>
        ///  货架编号 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "货架编号不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string RAFShelfNumber { get; set; }
			
        /// <summary>
        ///  仓库编号 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "仓库编号不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string RAFWarehouseNumber { get; set; }
			
        /// <summary>
        ///  申请人工号 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "申请人工号不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string RAFApplicationManualNumber { get; set; }
			
        /// <summary>
        ///  货品种类编号 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "货品种类编号不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string RAFCategoryNumberOfGoods { get; set; }
			
        /// <summary>
        ///  货品数量 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "货品数量不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string RAFQuantityOfGoods { get; set; }
			
        /// <summary>
        ///  申请日期 DATETIME 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "申请日期不能为空")]
				[RelativeDateTimeValidator(-140, DateTimeUnit.Year, 0, DateTimeUnit.Second, MessageTemplate = "日期格式错误")]
		
        public Nullable<DateTime> RAFApplicationDate { get; set; }
			
        /// <summary>
        ///  申请单状态 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "申请单状态不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string RAFApplicationStatus { get; set; }
			
        /// <summary>
        ///  备注 NVARCHAR(4000) 4000
        /// </summary>
		// [NotNullValidator(MessageTemplate = "备注不能为空")]
		/* [StringLengthValidator(0,4000)] */
        public string RAFRemarks { get; set; }
	}

	
    /// <summary>
    ///  补货记录 
    /// </summary>
	[Table("ReplenishmentRecord")]
    public class ReplenishmentRecord 
    {
			        
        /// <summary>
        ///  ReplenishmentRecord编号
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
        ///  经度
        /// </summary>
        public string Longitude { get; set; }
        /// <summary>
        ///  纬度
        /// </summary>
        public string Latitude { get; set; }
        /// <summary>
        ///  排序
        /// </summary>
        public int? ord { get; set; }
			
        /// <summary>
        ///  申请单编号 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "申请单编号不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string RRApplicationNumber { get; set; }
			
        /// <summary>
        ///  到货日期 DATETIME 
        /// </summary>
		// [NotNullValidator(MessageTemplate = "到货日期不能为空")]
				[RelativeDateTimeValidator(-140, DateTimeUnit.Year, 0, DateTimeUnit.Second, MessageTemplate = "日期格式错误")]
		
        public Nullable<DateTime> RRDateOfArrival { get; set; }
			
        /// <summary>
        ///  备注 NVARCHAR(4000) 4000
        /// </summary>
		// [NotNullValidator(MessageTemplate = "备注不能为空")]
		/* [StringLengthValidator(0,4000)] */
        public string RRRemarks { get; set; }
	}

	
    /// <summary>
    ///  销售记录 
    /// </summary>
	[Table("SalesRecord")]
    public class SalesRecord 
    {
			        
        /// <summary>
        ///  SalesRecord编号
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
        ///  经度
        /// </summary>
        public string Longitude { get; set; }
        /// <summary>
        ///  纬度
        /// </summary>
        public string Latitude { get; set; }
        /// <summary>
        ///  排序
        /// </summary>
        public int? ord { get; set; }
			
        /// <summary>
        ///  销售批次号 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "销售批次号不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string SRSalesLotNumber { get; set; }
			
        /// <summary>
        ///  货物种类 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "货物种类不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string SRTypeOfGoods { get; set; }
			
        /// <summary>
        ///  数量 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "数量不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string SRAmount { get; set; }
			
        /// <summary>
        ///  单价 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "单价不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string SRUnitPrice { get; set; }
			
        /// <summary>
        ///  销售工号 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "销售工号不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string SRSalesNumber { get; set; }
			
        /// <summary>
        ///  发票编号 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "发票编号不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string SRInvoiceNumber { get; set; }
			
        /// <summary>
        ///  发票抬头 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "发票抬头不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string SRInvoicesAreRaised { get; set; }
			
        /// <summary>
        ///  税号 NVARCHAR(50) 50
        /// </summary>
		// [NotNullValidator(MessageTemplate = "税号不能为空")]
		/* [StringLengthValidator(0,50)] */
        public string SRDutyParagraph { get; set; }
			
        /// <summary>
        ///  备注 NVARCHAR(4000) 4000
        /// </summary>
		// [NotNullValidator(MessageTemplate = "备注不能为空")]
		/* [StringLengthValidator(0,4000)] */
        public string SRRemarks { get; set; }
	}

}
