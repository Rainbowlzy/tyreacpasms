




/* ------------------------------------------------------------ *
 * 此文件由生成器引擎根据既有规则生成，所有手工的更改将会被覆盖
 * 生成时间：11/12/2018 18:00:09
 * 生成版本：11/12/2018 18:00:02 
 * 作者：路正遥
 * ------------------------------------------------------------ */
using System;

namespace T.Evaluators 
{

    /// <summary>
    /// 供应商 搜索条件实体模型
    /// </summary>
    public class SupplierSearchModel
    {
        /// <summary>
        /// 每页记录数量
        /// </summary>
		public int PageSize { get; set; } = 5;
		
        /// <summary>
        /// 页码
        /// </summary>
		public int PageIndex { get; set; } = 0;
		
        /// <summary>
        /// 搜索关键字
        /// </summary>
		public string SearchKey { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
		public string Sort { get; set; }




        /// <summary>
        /// 最小供应商编号
        /// </summary>
        public int? MinSSupplierNumber { get; set; }

        /// <summary>
        /// 最大供应商编号
        /// </summary>
        public int? MaxSSupplierNumber { get; set; }



        /// <summary>
        /// 供应商名称
        /// </summary>
        public string SSupplierName { get; set; }



        /// <summary>
        /// 联系方式
        /// </summary>
        public string SCommonModeOfContact { get; set; }



        /// <summary>
        /// 办公地点
        /// </summary>
        public string SOfficeLocation { get; set; }



    }

    /// <summary>
    /// 仓库 搜索条件实体模型
    /// </summary>
    public class WarehouseSearchModel
    {
        /// <summary>
        /// 每页记录数量
        /// </summary>
		public int PageSize { get; set; } = 5;
		
        /// <summary>
        /// 页码
        /// </summary>
		public int PageIndex { get; set; } = 0;
		
        /// <summary>
        /// 搜索关键字
        /// </summary>
		public string SearchKey { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
		public string Sort { get; set; }




        /// <summary>
        /// 最小仓库编号
        /// </summary>
        public int? MinWWarehouseNumber { get; set; }

        /// <summary>
        /// 最大仓库编号
        /// </summary>
        public int? MaxWWarehouseNumber { get; set; }



        /// <summary>
        /// 容量
        /// </summary>
        public string WCapacity { get; set; }



        /// <summary>
        /// 地点
        /// </summary>
        public string WLocality { get; set; }




        /// <summary>
        /// 最小负责人工号
        /// </summary>
        public int? MinWResponsibleForManualNumber { get; set; }

        /// <summary>
        /// 最大负责人工号
        /// </summary>
        public int? MaxWResponsibleForManualNumber { get; set; }



    }

    /// <summary>
    /// 客户 搜索条件实体模型
    /// </summary>
    public class CustomertypeSearchModel
    {
        /// <summary>
        /// 每页记录数量
        /// </summary>
		public int PageSize { get; set; } = 5;
		
        /// <summary>
        /// 页码
        /// </summary>
		public int PageIndex { get; set; } = 0;
		
        /// <summary>
        /// 搜索关键字
        /// </summary>
		public string SearchKey { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
		public string Sort { get; set; }




        /// <summary>
        /// 最小客户编号
        /// </summary>
        public int? MinCCustomerNumber { get; set; }

        /// <summary>
        /// 最大客户编号
        /// </summary>
        public int? MaxCCustomerNumber { get; set; }



        /// <summary>
        /// 姓名
        /// </summary>
        public string CName { get; set; }



        /// <summary>
        /// 联系方式
        /// </summary>
        public string CCommonModeOfContact { get; set; }



    }

    /// <summary>
    /// 货物 搜索条件实体模型
    /// </summary>
    public class CargoSearchModel
    {
        /// <summary>
        /// 每页记录数量
        /// </summary>
		public int PageSize { get; set; } = 5;
		
        /// <summary>
        /// 页码
        /// </summary>
		public int PageIndex { get; set; } = 0;
		
        /// <summary>
        /// 搜索关键字
        /// </summary>
		public string SearchKey { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
		public string Sort { get; set; }




        /// <summary>
        /// 最小货物编号
        /// </summary>
        public int? MinCCargoNumber { get; set; }

        /// <summary>
        /// 最大货物编号
        /// </summary>
        public int? MaxCCargoNumber { get; set; }



        /// <summary>
        /// 货物名称
        /// </summary>
        public string CNameOfGoods { get; set; }



    }

    /// <summary>
    /// 货架 搜索条件实体模型
    /// </summary>
    public class GoodsShelvesSearchModel
    {
        /// <summary>
        /// 每页记录数量
        /// </summary>
		public int PageSize { get; set; } = 5;
		
        /// <summary>
        /// 页码
        /// </summary>
		public int PageIndex { get; set; } = 0;
		
        /// <summary>
        /// 搜索关键字
        /// </summary>
		public string SearchKey { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
		public string Sort { get; set; }




        /// <summary>
        /// 最小货架编号
        /// </summary>
        public int? MinGSShelfNumber { get; set; }

        /// <summary>
        /// 最大货架编号
        /// </summary>
        public int? MaxGSShelfNumber { get; set; }



        /// <summary>
        /// 容量
        /// </summary>
        public string GSCapacity { get; set; }



        /// <summary>
        /// 地点
        /// </summary>
        public string GSLocality { get; set; }




        /// <summary>
        /// 最小负责人工号
        /// </summary>
        public int? MinGSResponsibleForManualNumber { get; set; }

        /// <summary>
        /// 最大负责人工号
        /// </summary>
        public int? MaxGSResponsibleForManualNumber { get; set; }



    }

    /// <summary>
    /// 员工 搜索条件实体模型
    /// </summary>
    public class StaffnameSearchModel
    {
        /// <summary>
        /// 每页记录数量
        /// </summary>
		public int PageSize { get; set; } = 5;
		
        /// <summary>
        /// 页码
        /// </summary>
		public int PageIndex { get; set; } = 0;
		
        /// <summary>
        /// 搜索关键字
        /// </summary>
		public string SearchKey { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
		public string Sort { get; set; }




        /// <summary>
        /// 最小工号
        /// </summary>
        public int? MinSJobNumber { get; set; }

        /// <summary>
        /// 最大工号
        /// </summary>
        public int? MaxSJobNumber { get; set; }



        /// <summary>
        /// 姓名
        /// </summary>
        public string SName { get; set; }



        /// <summary>
        /// 学历
        /// </summary>
        public string SEducation { get; set; }



        /// <summary>
        /// 联系方式
        /// </summary>
        public string SCommonModeOfContact { get; set; }



    }

    /// <summary>
    /// 采购 搜索条件实体模型
    /// </summary>
    public class ProcureSearchModel
    {
        /// <summary>
        /// 每页记录数量
        /// </summary>
		public int PageSize { get; set; } = 5;
		
        /// <summary>
        /// 页码
        /// </summary>
		public int PageIndex { get; set; } = 0;
		
        /// <summary>
        /// 搜索关键字
        /// </summary>
		public string SearchKey { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
		public string Sort { get; set; }




        /// <summary>
        /// 最小供应商编号
        /// </summary>
        public int? MinPSupplierNumber { get; set; }

        /// <summary>
        /// 最大供应商编号
        /// </summary>
        public int? MaxPSupplierNumber { get; set; }




        /// <summary>
        /// 最小货物编号
        /// </summary>
        public int? MinPCargoNumber { get; set; }

        /// <summary>
        /// 最大货物编号
        /// </summary>
        public int? MaxPCargoNumber { get; set; }




        /// <summary>
        /// 最小采购员工号
        /// </summary>
        public int? MinPPurchasingStaffNumber { get; set; }

        /// <summary>
        /// 最大采购员工号
        /// </summary>
        public int? MaxPPurchasingStaffNumber { get; set; }




        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime? FromPDate { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime? ToPDate { get; set; }



        /// <summary>
        /// 数量
        /// </summary>
        public string PAmount { get; set; }



        /// <summary>
        /// 价格
        /// </summary>
        public string PPrice { get; set; }



        /// <summary>
        /// 备注
        /// </summary>
        public string PRemarks { get; set; }



    }

    /// <summary>
    /// 销售 搜索条件实体模型
    /// </summary>
    public class SalesSearchModel
    {
        /// <summary>
        /// 每页记录数量
        /// </summary>
		public int PageSize { get; set; } = 5;
		
        /// <summary>
        /// 页码
        /// </summary>
		public int PageIndex { get; set; } = 0;
		
        /// <summary>
        /// 搜索关键字
        /// </summary>
		public string SearchKey { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
		public string Sort { get; set; }




        /// <summary>
        /// 最小货物编号
        /// </summary>
        public int? MinSCargoNumber { get; set; }

        /// <summary>
        /// 最大货物编号
        /// </summary>
        public int? MaxSCargoNumber { get; set; }




        /// <summary>
        /// 最小客户编号
        /// </summary>
        public int? MinSCustomerNumber { get; set; }

        /// <summary>
        /// 最大客户编号
        /// </summary>
        public int? MaxSCustomerNumber { get; set; }




        /// <summary>
        /// 最小销售员工号
        /// </summary>
        public int? MinSSalesStaffNumber { get; set; }

        /// <summary>
        /// 最大销售员工号
        /// </summary>
        public int? MaxSSalesStaffNumber { get; set; }




        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime? FromSDate { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime? ToSDate { get; set; }



        /// <summary>
        /// 数量
        /// </summary>
        public string SAmount { get; set; }



        /// <summary>
        /// 价格
        /// </summary>
        public string SPrice { get; set; }



        /// <summary>
        /// 备注
        /// </summary>
        public string SRemarks { get; set; }



    }

    /// <summary>
    /// 供应 搜索条件实体模型
    /// </summary>
    public class FurnishSearchModel
    {
        /// <summary>
        /// 每页记录数量
        /// </summary>
		public int PageSize { get; set; } = 5;
		
        /// <summary>
        /// 页码
        /// </summary>
		public int PageIndex { get; set; } = 0;
		
        /// <summary>
        /// 搜索关键字
        /// </summary>
		public string SearchKey { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
		public string Sort { get; set; }




        /// <summary>
        /// 最小供应商编号
        /// </summary>
        public int? MinFSupplierNumber { get; set; }

        /// <summary>
        /// 最大供应商编号
        /// </summary>
        public int? MaxFSupplierNumber { get; set; }




        /// <summary>
        /// 最小货物编号
        /// </summary>
        public int? MinFCargoNumber { get; set; }

        /// <summary>
        /// 最大货物编号
        /// </summary>
        public int? MaxFCargoNumber { get; set; }




        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime? FromFDate { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime? ToFDate { get; set; }



        /// <summary>
        /// 数量
        /// </summary>
        public string FAmount { get; set; }



        /// <summary>
        /// 备注
        /// </summary>
        public string FRemarks { get; set; }



    }

    /// <summary>
    /// 入库 搜索条件实体模型
    /// </summary>
    public class WarehousingRecordSearchModel
    {
        /// <summary>
        /// 每页记录数量
        /// </summary>
		public int PageSize { get; set; } = 5;
		
        /// <summary>
        /// 页码
        /// </summary>
		public int PageIndex { get; set; } = 0;
		
        /// <summary>
        /// 搜索关键字
        /// </summary>
		public string SearchKey { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
		public string Sort { get; set; }




        /// <summary>
        /// 最小仓库编号
        /// </summary>
        public int? MinWRWarehouseNumber { get; set; }

        /// <summary>
        /// 最大仓库编号
        /// </summary>
        public int? MaxWRWarehouseNumber { get; set; }




        /// <summary>
        /// 最小货物编号
        /// </summary>
        public int? MinWRCargoNumber { get; set; }

        /// <summary>
        /// 最大货物编号
        /// </summary>
        public int? MaxWRCargoNumber { get; set; }




        /// <summary>
        /// 最小仓库管理员工号
        /// </summary>
        public int? MinWRWarehouseManagementStaffNumber { get; set; }

        /// <summary>
        /// 最大仓库管理员工号
        /// </summary>
        public int? MaxWRWarehouseManagementStaffNumber { get; set; }




        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime? FromWRDate { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime? ToWRDate { get; set; }



        /// <summary>
        /// 数量
        /// </summary>
        public string WRAmount { get; set; }



        /// <summary>
        /// 备注
        /// </summary>
        public string WRRemarks { get; set; }



    }

    /// <summary>
    /// 补货 搜索条件实体模型
    /// </summary>
    public class ReplenishmentApplicationFormSearchModel
    {
        /// <summary>
        /// 每页记录数量
        /// </summary>
		public int PageSize { get; set; } = 5;
		
        /// <summary>
        /// 页码
        /// </summary>
		public int PageIndex { get; set; } = 0;
		
        /// <summary>
        /// 搜索关键字
        /// </summary>
		public string SearchKey { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
		public string Sort { get; set; }




        /// <summary>
        /// 最小货架编号
        /// </summary>
        public int? MinRAFShelfNumber { get; set; }

        /// <summary>
        /// 最大货架编号
        /// </summary>
        public int? MaxRAFShelfNumber { get; set; }




        /// <summary>
        /// 最小货物编号
        /// </summary>
        public int? MinRAFCargoNumber { get; set; }

        /// <summary>
        /// 最大货物编号
        /// </summary>
        public int? MaxRAFCargoNumber { get; set; }




        /// <summary>
        /// 最小仓库管理员工号
        /// </summary>
        public int? MinRAFWarehouseManagementStaffNumber { get; set; }

        /// <summary>
        /// 最大仓库管理员工号
        /// </summary>
        public int? MaxRAFWarehouseManagementStaffNumber { get; set; }




        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime? FromRAFDate { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime? ToRAFDate { get; set; }



        /// <summary>
        /// 数量
        /// </summary>
        public string RAFAmount { get; set; }



        /// <summary>
        /// 备注
        /// </summary>
        public string RAFRemarks { get; set; }



    }

    /// <summary>
    /// 菜单配置 搜索条件实体模型
    /// </summary>
    public class MenuConfigurationSearchModel
    {
        /// <summary>
        /// 每页记录数量
        /// </summary>
		public int PageSize { get; set; } = 5;
		
        /// <summary>
        /// 页码
        /// </summary>
		public int PageIndex { get; set; } = 0;
		
        /// <summary>
        /// 搜索关键字
        /// </summary>
		public string SearchKey { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
		public string Sort { get; set; }



        /// <summary>
        /// 标题
        /// </summary>
        public string MCCaption { get; set; }



        /// <summary>
        /// 父级标题
        /// </summary>
        public string MCParentTitle { get; set; }



        /// <summary>
        /// 链接
        /// </summary>
        public string MCLink { get; set; }



        /// <summary>
        /// 菜单类型
        /// </summary>
        public string MCMenuType { get; set; }




        /// <summary>
        /// 最小顺序
        /// </summary>
        public int? MinMCSequence { get; set; }

        /// <summary>
        /// 最大顺序
        /// </summary>
        public int? MaxMCSequence { get; set; }



        /// <summary>
        /// 显示名称
        /// </summary>
        public string MCDisplayName { get; set; }



        /// <summary>
        /// 图片
        /// </summary>
        public string MCPicture { get; set; }



    }

    /// <summary>
    /// 角色菜单 搜索条件实体模型
    /// </summary>
    public class RoleMenuSearchModel
    {
        /// <summary>
        /// 每页记录数量
        /// </summary>
		public int PageSize { get; set; } = 5;
		
        /// <summary>
        /// 页码
        /// </summary>
		public int PageIndex { get; set; } = 0;
		
        /// <summary>
        /// 搜索关键字
        /// </summary>
		public string SearchKey { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
		public string Sort { get; set; }



        /// <summary>
        /// 角色名称
        /// </summary>
        public string RMRoleName { get; set; }



        /// <summary>
        /// 菜单标题
        /// </summary>
        public string RMMenuTitle { get; set; }



    }

    /// <summary>
    /// 用户角色 搜索条件实体模型
    /// </summary>
    public class UserRoleSearchModel
    {
        /// <summary>
        /// 每页记录数量
        /// </summary>
		public int PageSize { get; set; } = 5;
		
        /// <summary>
        /// 页码
        /// </summary>
		public int PageIndex { get; set; } = 0;
		
        /// <summary>
        /// 搜索关键字
        /// </summary>
		public string SearchKey { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
		public string Sort { get; set; }



        /// <summary>
        /// 角色名称
        /// </summary>
        public string URRoleName { get; set; }



        /// <summary>
        /// 登录名
        /// </summary>
        public string URLoginName { get; set; }



    }

    /// <summary>
    /// 角色配置 搜索条件实体模型
    /// </summary>
    public class RoleConfigurationSearchModel
    {
        /// <summary>
        /// 每页记录数量
        /// </summary>
		public int PageSize { get; set; } = 5;
		
        /// <summary>
        /// 页码
        /// </summary>
		public int PageIndex { get; set; } = 0;
		
        /// <summary>
        /// 搜索关键字
        /// </summary>
		public string SearchKey { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
		public string Sort { get; set; }



        /// <summary>
        /// 角色名称
        /// </summary>
        public string RCRoleName { get; set; }



        /// <summary>
        /// 所属组织
        /// </summary>
        public string RCAffiliatedOrganization { get; set; }



    }

    /// <summary>
    /// 用户信息 搜索条件实体模型
    /// </summary>
    public class UserInformationSearchModel
    {
        /// <summary>
        /// 每页记录数量
        /// </summary>
		public int PageSize { get; set; } = 5;
		
        /// <summary>
        /// 页码
        /// </summary>
		public int PageIndex { get; set; } = 0;
		
        /// <summary>
        /// 搜索关键字
        /// </summary>
		public string SearchKey { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
		public string Sort { get; set; }




        /// <summary>
        /// 最小工号
        /// </summary>
        public int? MinUIJobNumber { get; set; }

        /// <summary>
        /// 最大工号
        /// </summary>
        public int? MaxUIJobNumber { get; set; }



        /// <summary>
        /// 登录名
        /// </summary>
        public string UILoginName { get; set; }



        /// <summary>
        /// 昵称
        /// </summary>
        public string UINickname { get; set; }



        /// <summary>
        /// 真实姓名
        /// </summary>
        public string UIRealName { get; set; }



        /// <summary>
        /// 头像
        /// </summary>
        public string UIHeadPortrait { get; set; }



        /// <summary>
        /// 部门
        /// </summary>
        public string UIDepartment { get; set; }



        /// <summary>
        /// 职位
        /// </summary>
        public string UIPost { get; set; }



        /// <summary>
        /// 电话
        /// </summary>
        public string UIBooth { get; set; }



        /// <summary>
        /// 照片
        /// </summary>
        public string UIPhoto { get; set; }



        /// <summary>
        /// 用户类型
        /// </summary>
        public string UICustomerType { get; set; }



        /// <summary>
        /// 用户级别
        /// </summary>
        public string UIUserLevel { get; set; }




        /// <summary>
        /// 开始入职时间
        /// </summary>
        public DateTime? FromUITimeOfEntry { get; set; }

        /// <summary>
        /// 结束入职时间
        /// </summary>
        public DateTime? ToUITimeOfEntry { get; set; }




        /// <summary>
        /// 开始离职时间
        /// </summary>
        public DateTime? FromUIDepartureTime { get; set; }

        /// <summary>
        /// 结束离职时间
        /// </summary>
        public DateTime? ToUIDepartureTime { get; set; }



        /// <summary>
        /// 密码
        /// </summary>
        public string UICode { get; set; }



    }

    /// <summary>
    /// 登录记录 搜索条件实体模型
    /// </summary>
    public class LogonRecordSearchModel
    {
        /// <summary>
        /// 每页记录数量
        /// </summary>
		public int PageSize { get; set; } = 5;
		
        /// <summary>
        /// 页码
        /// </summary>
		public int PageIndex { get; set; } = 0;
		
        /// <summary>
        /// 搜索关键字
        /// </summary>
		public string SearchKey { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
		public string Sort { get; set; }



        /// <summary>
        /// 登录名
        /// </summary>
        public string LRLoginName { get; set; }




        /// <summary>
        /// 开始登录时间
        /// </summary>
        public DateTime? FromLRLoginTime { get; set; }

        /// <summary>
        /// 结束登录时间
        /// </summary>
        public DateTime? ToLRLoginTime { get; set; }



    }

    /// <summary>
    /// 用户菜单 搜索条件实体模型
    /// </summary>
    public class UserMenuSearchModel
    {
        /// <summary>
        /// 每页记录数量
        /// </summary>
		public int PageSize { get; set; } = 5;
		
        /// <summary>
        /// 页码
        /// </summary>
		public int PageIndex { get; set; } = 0;
		
        /// <summary>
        /// 搜索关键字
        /// </summary>
		public string SearchKey { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
		public string Sort { get; set; }



        /// <summary>
        /// 登录名
        /// </summary>
        public string UMLoginName { get; set; }



        /// <summary>
        /// 标题
        /// </summary>
        public string UMCaption { get; set; }



    }

    /// <summary>
    /// 系统配置 搜索条件实体模型
    /// </summary>
    public class SystemConfigurationSearchModel
    {
        /// <summary>
        /// 每页记录数量
        /// </summary>
		public int PageSize { get; set; } = 5;
		
        /// <summary>
        /// 页码
        /// </summary>
		public int PageIndex { get; set; } = 0;
		
        /// <summary>
        /// 搜索关键字
        /// </summary>
		public string SearchKey { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
		public string Sort { get; set; }



        /// <summary>
        /// 键
        /// </summary>
        public string SCKey { get; set; }



        /// <summary>
        /// 值
        /// </summary>
        public string SCAccrued { get; set; }



    }

}
