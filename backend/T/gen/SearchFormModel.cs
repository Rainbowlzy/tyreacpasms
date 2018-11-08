





/* ------------------------------------------------------------ *
 * 此文件由生成器引擎根据既有规则生成，所有手工的更改将会被覆盖
 * 生成时间：11/08/2018 10:17:41
 * 生成版本：11/08/2018 10:17:24 
 * 作者：路正遥
 * ------------------------------------------------------------ */
using System;

namespace T.Evaluators 
{

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
        /// 工号
        /// </summary>
        public string SJobNumber { get; set; }



        /// <summary>
        /// 姓名
        /// </summary>
        public string SName { get; set; }



        /// <summary>
        /// 公司
        /// </summary>
        public string SAffiliate { get; set; }



        /// <summary>
        /// 部门
        /// </summary>
        public string SDepartment { get; set; }



        /// <summary>
        /// 职位
        /// </summary>
        public string SPost { get; set; }



        /// <summary>
        /// 联系方式
        /// </summary>
        public string SCommonModeOfContact { get; set; }



        /// <summary>
        /// 备用联系方式
        /// </summary>
        public string SAlternateContactMode { get; set; }




        /// <summary>
        /// 开始入职时间
        /// </summary>
        public DateTime? FromSTimeOfEntry { get; set; }

        /// <summary>
        /// 结束入职时间
        /// </summary>
        public DateTime? ToSTimeOfEntry { get; set; }




        /// <summary>
        /// 开始离职时间
        /// </summary>
        public DateTime? FromSDepartureTime { get; set; }

        /// <summary>
        /// 结束离职时间
        /// </summary>
        public DateTime? ToSDepartureTime { get; set; }



        /// <summary>
        /// 密码
        /// </summary>
        public string SCode { get; set; }



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
        /// 客户编号
        /// </summary>
        public string CCustomerNumber { get; set; }



        /// <summary>
        /// 姓名
        /// </summary>
        public string CName { get; set; }



        /// <summary>
        /// 性别
        /// </summary>
        public string CChairperson { get; set; }



        /// <summary>
        /// 称呼
        /// </summary>
        public string CCall { get; set; }



        /// <summary>
        /// 联系方式
        /// </summary>
        public string CCommonModeOfContact { get; set; }



        /// <summary>
        /// 地址
        /// </summary>
        public string CAddress { get; set; }



    }

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
        /// 供应商编号
        /// </summary>
        public string SSupplierNumber { get; set; }



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



        /// <summary>
        /// 经营范围
        /// </summary>
        public string SScopeOfOperation { get; set; }



    }

    /// <summary>
    /// 货物种类 搜索条件实体模型
    /// </summary>
    public class TypeOfGoodsSearchModel
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
        /// 货品种类编号
        /// </summary>
        public string TOGCategoryNumberOfGoods { get; set; }



        /// <summary>
        /// 货物名称
        /// </summary>
        public string TOGNameOfGoods { get; set; }



        /// <summary>
        /// 货物类别
        /// </summary>
        public string TOGCategoryOfGoods { get; set; }



        /// <summary>
        /// 货物子类别
        /// </summary>
        public string TOGCargoSubcategory { get; set; }



        /// <summary>
        /// 体积
        /// </summary>
        public string TOGBulk { get; set; }



        /// <summary>
        /// 颜色
        /// </summary>
        public string TOGColor { get; set; }



        /// <summary>
        /// 型号
        /// </summary>
        public string TOGModel { get; set; }



        /// <summary>
        /// 别称
        /// </summary>
        public string TOGAlias { get; set; }



        /// <summary>
        /// 采购单价
        /// </summary>
        public string TOGPurchaseUnitPrice { get; set; }



        /// <summary>
        /// 销售单价
        /// </summary>
        public string TOGSalesUnitPrice { get; set; }



    }

    /// <summary>
    /// 供货渠道 搜索条件实体模型
    /// </summary>
    public class SupplyChannelSearchModel
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
        /// 供应商
        /// </summary>
        public string SCSupplier { get; set; }



        /// <summary>
        /// 货物种类
        /// </summary>
        public string SCTypeOfGoods { get; set; }



    }

    /// <summary>
    /// 订单 搜索条件实体模型
    /// </summary>
    public class OrderSearchModel
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
        /// 订单编号
        /// </summary>
        public string OOrderNumber { get; set; }



        /// <summary>
        /// 供应商编号
        /// </summary>
        public string OSupplierNumber { get; set; }




        /// <summary>
        /// 开始期望到达日期
        /// </summary>
        public DateTime? FromOExpectedArrivalDate { get; set; }

        /// <summary>
        /// 结束期望到达日期
        /// </summary>
        public DateTime? ToOExpectedArrivalDate { get; set; }




        /// <summary>
        /// 开始提交日期
        /// </summary>
        public DateTime? FromODateOfSubmission { get; set; }

        /// <summary>
        /// 结束提交日期
        /// </summary>
        public DateTime? ToODateOfSubmission { get; set; }



        /// <summary>
        /// 提交人
        /// </summary>
        public string OSubmitter { get; set; }



        /// <summary>
        /// 提交人联系方式
        /// </summary>
        public string OAuthorsContactInformation { get; set; }



        /// <summary>
        /// 订单状态
        /// </summary>
        public string OOrderStatus { get; set; }



        /// <summary>
        /// 备注
        /// </summary>
        public string ORemarks { get; set; }



    }

    /// <summary>
    /// 订单明细 搜索条件实体模型
    /// </summary>
    public class OrderDetailsSearchModel
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
        /// 订单编号
        /// </summary>
        public string ODOrderNumber { get; set; }



        /// <summary>
        /// 货物种类
        /// </summary>
        public string ODTypeOfGoods { get; set; }



        /// <summary>
        /// 货物数量
        /// </summary>
        public string ODQuantityOfGoods { get; set; }



        /// <summary>
        /// 采购单价
        /// </summary>
        public string ODPurchaseUnitPrice { get; set; }



    }

    /// <summary>
    /// 入库记录 搜索条件实体模型
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
        /// 订单编号
        /// </summary>
        public string WROrderNumber { get; set; }




        /// <summary>
        /// 开始到货日期
        /// </summary>
        public DateTime? FromWRDateOfArrival { get; set; }

        /// <summary>
        /// 结束到货日期
        /// </summary>
        public DateTime? ToWRDateOfArrival { get; set; }



        /// <summary>
        /// 经办人
        /// </summary>
        public string WRAgent { get; set; }



        /// <summary>
        /// 经办人联系方式
        /// </summary>
        public string WROperatorContact { get; set; }



        /// <summary>
        /// 备注
        /// </summary>
        public string WRRemarks { get; set; }



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
        /// 仓库编号
        /// </summary>
        public string WWarehouseNumber { get; set; }



        /// <summary>
        /// 容积
        /// </summary>
        public string WCapacity { get; set; }



        /// <summary>
        /// 位置
        /// </summary>
        public string WLocation { get; set; }



        /// <summary>
        /// 负责人工号
        /// </summary>
        public string WResponsibleForManualNumber { get; set; }



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
        /// 货架编号
        /// </summary>
        public string GSShelfNumber { get; set; }



        /// <summary>
        /// 容积 
        /// </summary>
        public string GSVolume { get; set; }



        /// <summary>
        /// 位置
        /// </summary>
        public string GSLocation { get; set; }



        /// <summary>
        /// 负责人工号
        /// </summary>
        public string GSResponsibleForManualNumber { get; set; }



    }

    /// <summary>
    /// 补货申请单 搜索条件实体模型
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
        /// 申请单编号
        /// </summary>
        public string RAFApplicationNumber { get; set; }



        /// <summary>
        /// 货架编号
        /// </summary>
        public string RAFShelfNumber { get; set; }



        /// <summary>
        /// 仓库编号
        /// </summary>
        public string RAFWarehouseNumber { get; set; }



        /// <summary>
        /// 申请人工号
        /// </summary>
        public string RAFApplicationManualNumber { get; set; }



        /// <summary>
        /// 货品种类编号
        /// </summary>
        public string RAFCategoryNumberOfGoods { get; set; }



        /// <summary>
        /// 货品数量
        /// </summary>
        public string RAFQuantityOfGoods { get; set; }




        /// <summary>
        /// 开始申请日期
        /// </summary>
        public DateTime? FromRAFApplicationDate { get; set; }

        /// <summary>
        /// 结束申请日期
        /// </summary>
        public DateTime? ToRAFApplicationDate { get; set; }



        /// <summary>
        /// 申请单状态
        /// </summary>
        public string RAFApplicationStatus { get; set; }



        /// <summary>
        /// 备注
        /// </summary>
        public string RAFRemarks { get; set; }



    }

    /// <summary>
    /// 补货记录 搜索条件实体模型
    /// </summary>
    public class ReplenishmentRecordSearchModel
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
        /// 申请单编号
        /// </summary>
        public string RRApplicationNumber { get; set; }




        /// <summary>
        /// 开始到货日期
        /// </summary>
        public DateTime? FromRRDateOfArrival { get; set; }

        /// <summary>
        /// 结束到货日期
        /// </summary>
        public DateTime? ToRRDateOfArrival { get; set; }



        /// <summary>
        /// 备注
        /// </summary>
        public string RRRemarks { get; set; }



    }

    /// <summary>
    /// 销售记录 搜索条件实体模型
    /// </summary>
    public class SalesRecordSearchModel
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
        /// 销售批次号
        /// </summary>
        public string SRSalesLotNumber { get; set; }



        /// <summary>
        /// 货物种类
        /// </summary>
        public string SRTypeOfGoods { get; set; }



        /// <summary>
        /// 数量
        /// </summary>
        public string SRAmount { get; set; }



        /// <summary>
        /// 单价
        /// </summary>
        public string SRUnitPrice { get; set; }



        /// <summary>
        /// 销售工号
        /// </summary>
        public string SRSalesNumber { get; set; }



        /// <summary>
        /// 发票编号
        /// </summary>
        public string SRInvoiceNumber { get; set; }



        /// <summary>
        /// 发票抬头
        /// </summary>
        public string SRInvoicesAreRaised { get; set; }



        /// <summary>
        /// 税号
        /// </summary>
        public string SRDutyParagraph { get; set; }



        /// <summary>
        /// 备注
        /// </summary>
        public string SRRemarks { get; set; }



    }

}
