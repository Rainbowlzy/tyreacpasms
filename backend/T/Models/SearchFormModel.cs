


/* ------------------------------------------------------------ *
 * 此文件由生成器引擎根据既有规则生成，所有手工的更改将会被覆盖
 * 生成时间：11/17/2018 14:58:56
 * 生成版本：11/17/2018 14:58:50 
 * 作者：路正遥
 * ------------------------------------------------------------ */
using System;

namespace T.Evaluators 
{
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
        /// 联系电话
        /// </summary>
        public string CContactNumber { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string CChairperson { get; set; }
        /// <summary>
        /// 出生年月
        /// </summary>
        public string CDateOfBirth { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string CAddress { get; set; }
        /// <summary>
        /// 邮编
        /// </summary>
        public string CZipCode { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string CRemarks { get; set; }

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
        /// 联系电话
        /// </summary>
        public string SContactNumber { get; set; }
        /// <summary>
        /// 办公地点
        /// </summary>
        public string SOfficeLocation { get; set; }

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
        /// <summary>
        /// 型号
        /// </summary>
        public string CModel { get; set; }
        /// <summary>
        /// 有无配件
        /// </summary>
        public string CHaveParts { get; set; }

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
        /// 联系电话
        /// </summary>
        public string SContactNumber { get; set; }

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
        /// 最小容量
        /// </summary>
        public int? MinWCapacity { get; set; }

        /// <summary>
        /// 最大容量
        /// </summary>
        public int? MaxWCapacity { get; set; }
        /// <summary>
        /// 地点
        /// </summary>
        public string WLocality { get; set; }

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
        /// 最小容量
        /// </summary>
        public int? MinGSCapacity { get; set; }

        /// <summary>
        /// 最大容量
        /// </summary>
        public int? MaxGSCapacity { get; set; }
        /// <summary>
        /// 地点
        /// </summary>
        public string GSLocality { get; set; }

    }
    /// <summary>
    /// 采购单 搜索条件实体模型
    /// </summary>
    public class PurchaseUnitPriceSearchModel
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
        public int? MinPUPSupplierNumber { get; set; }

        /// <summary>
        /// 最大供应商编号
        /// </summary>
        public int? MaxPUPSupplierNumber { get; set; }

        /// <summary>
        /// 最小货物编号
        /// </summary>
        public int? MinPUPCargoNumber { get; set; }

        /// <summary>
        /// 最大货物编号
        /// </summary>
        public int? MaxPUPCargoNumber { get; set; }

        /// <summary>
        /// 最小采购员工号
        /// </summary>
        public int? MinPUPPurchasingStaffNumber { get; set; }

        /// <summary>
        /// 最大采购员工号
        /// </summary>
        public int? MaxPUPPurchasingStaffNumber { get; set; }

        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime? FromPUPDate { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime? ToPUPDate { get; set; }

        /// <summary>
        /// 最小数量
        /// </summary>
        public int? MinPUPAmount { get; set; }

        /// <summary>
        /// 最大数量
        /// </summary>
        public int? MaxPUPAmount { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public string PUPPrice { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string PUPRemarks { get; set; }

    }
    /// <summary>
    /// 销售单 搜索条件实体模型
    /// </summary>
    public class SalesUnitPriceSearchModel
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
        public int? MinSUPCargoNumber { get; set; }

        /// <summary>
        /// 最大货物编号
        /// </summary>
        public int? MaxSUPCargoNumber { get; set; }

        /// <summary>
        /// 最小客户编号
        /// </summary>
        public int? MinSUPCustomerNumber { get; set; }

        /// <summary>
        /// 最大客户编号
        /// </summary>
        public int? MaxSUPCustomerNumber { get; set; }

        /// <summary>
        /// 最小销售员工号
        /// </summary>
        public int? MinSUPSalesStaffNumber { get; set; }

        /// <summary>
        /// 最大销售员工号
        /// </summary>
        public int? MaxSUPSalesStaffNumber { get; set; }

        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime? FromSUPDate { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime? ToSUPDate { get; set; }

        /// <summary>
        /// 最小数量
        /// </summary>
        public int? MinSUPAmount { get; set; }

        /// <summary>
        /// 最大数量
        /// </summary>
        public int? MaxSUPAmount { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public string SUPPrice { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string SUPRemarks { get; set; }

    }
    /// <summary>
    /// 供应入库单 搜索条件实体模型
    /// </summary>
    public class SupplyWarehousingListSearchModel
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
        public int? MinSWLWarehouseNumber { get; set; }

        /// <summary>
        /// 最大仓库编号
        /// </summary>
        public int? MaxSWLWarehouseNumber { get; set; }

        /// <summary>
        /// 最小货物编号
        /// </summary>
        public int? MinSWLCargoNumber { get; set; }

        /// <summary>
        /// 最大货物编号
        /// </summary>
        public int? MaxSWLCargoNumber { get; set; }

        /// <summary>
        /// 最小供应商编号
        /// </summary>
        public int? MinSWLSupplierNumber { get; set; }

        /// <summary>
        /// 最大供应商编号
        /// </summary>
        public int? MaxSWLSupplierNumber { get; set; }

        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime? FromSWLDate { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime? ToSWLDate { get; set; }

        /// <summary>
        /// 最小数量
        /// </summary>
        public int? MinSWLAmount { get; set; }

        /// <summary>
        /// 最大数量
        /// </summary>
        public int? MaxSWLAmount { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string SWLRemarks { get; set; }

    }
    /// <summary>
    /// 补货单 搜索条件实体模型
    /// </summary>
    public class ReplenishmentBillSearchModel
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
        public int? MinRBShelfNumber { get; set; }

        /// <summary>
        /// 最大货架编号
        /// </summary>
        public int? MaxRBShelfNumber { get; set; }

        /// <summary>
        /// 最小仓库编号
        /// </summary>
        public int? MinRBWarehouseNumber { get; set; }

        /// <summary>
        /// 最大仓库编号
        /// </summary>
        public int? MaxRBWarehouseNumber { get; set; }

        /// <summary>
        /// 最小货物编号
        /// </summary>
        public int? MinRBCargoNumber { get; set; }

        /// <summary>
        /// 最大货物编号
        /// </summary>
        public int? MaxRBCargoNumber { get; set; }

        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime? FromRBDate { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime? ToRBDate { get; set; }

        /// <summary>
        /// 最小数量
        /// </summary>
        public int? MinRBAmount { get; set; }

        /// <summary>
        /// 最大数量
        /// </summary>
        public int? MaxRBAmount { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string RBRemarks { get; set; }

    }
    /// <summary>
    /// 仓库库存 搜索条件实体模型
    /// </summary>
    public class WarehouseStockSearchModel
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
        public int? MinWSWarehouseNumber { get; set; }

        /// <summary>
        /// 最大仓库编号
        /// </summary>
        public int? MaxWSWarehouseNumber { get; set; }

        /// <summary>
        /// 最小货物编号
        /// </summary>
        public int? MinWSCargoNumber { get; set; }

        /// <summary>
        /// 最大货物编号
        /// </summary>
        public int? MaxWSCargoNumber { get; set; }

        /// <summary>
        /// 最小数量
        /// </summary>
        public int? MinWSAmount { get; set; }

        /// <summary>
        /// 最大数量
        /// </summary>
        public int? MaxWSAmount { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string WSRemarks { get; set; }

    }
    /// <summary>
    /// 货架库存 搜索条件实体模型
    /// </summary>
    public class ShelfStockSearchModel
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
        public int? MinSSShelfNumber { get; set; }

        /// <summary>
        /// 最大货架编号
        /// </summary>
        public int? MaxSSShelfNumber { get; set; }

        /// <summary>
        /// 最小货物编号
        /// </summary>
        public int? MinSSCargoNumber { get; set; }

        /// <summary>
        /// 最大货物编号
        /// </summary>
        public int? MaxSSCargoNumber { get; set; }

        /// <summary>
        /// 最小数量
        /// </summary>
        public int? MinSSAmount { get; set; }

        /// <summary>
        /// 最大数量
        /// </summary>
        public int? MaxSSAmount { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string SSRemarks { get; set; }

    }
    /// <summary>
    /// 上架单 搜索条件实体模型
    /// </summary>
    public class ShelfListSearchModel
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
        public int? MinSLShelfNumber { get; set; }

        /// <summary>
        /// 最大货架编号
        /// </summary>
        public int? MaxSLShelfNumber { get; set; }

        /// <summary>
        /// 最小货物编号
        /// </summary>
        public int? MinSLCargoNumber { get; set; }

        /// <summary>
        /// 最大货物编号
        /// </summary>
        public int? MaxSLCargoNumber { get; set; }

        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime? FromSLDate { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime? ToSLDate { get; set; }

        /// <summary>
        /// 最小数量
        /// </summary>
        public int? MinSLAmount { get; set; }

        /// <summary>
        /// 最大数量
        /// </summary>
        public int? MaxSLAmount { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string SLRemarks { get; set; }

    }
    /// <summary>
    /// 采购凭证单 搜索条件实体模型
    /// </summary>
    public class PurchaseVoucherSearchModel
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
        public int? MinPVSupplierNumber { get; set; }

        /// <summary>
        /// 最大供应商编号
        /// </summary>
        public int? MaxPVSupplierNumber { get; set; }

        /// <summary>
        /// 最小货物编号
        /// </summary>
        public int? MinPVCargoNumber { get; set; }

        /// <summary>
        /// 最大货物编号
        /// </summary>
        public int? MaxPVCargoNumber { get; set; }

        /// <summary>
        /// 最小工号
        /// </summary>
        public int? MinPVJobNumber { get; set; }

        /// <summary>
        /// 最大工号
        /// </summary>
        public int? MaxPVJobNumber { get; set; }

        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime? FromPVDate { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime? ToPVDate { get; set; }

        /// <summary>
        /// 最小数量
        /// </summary>
        public int? MinPVAmount { get; set; }

        /// <summary>
        /// 最大数量
        /// </summary>
        public int? MaxPVAmount { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public string PVPrice { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string PVRemarks { get; set; }

    }
    /// <summary>
    /// 出库单 搜索条件实体模型
    /// </summary>
    public class OutboundOrderSearchModel
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
        public int? MinOOWarehouseNumber { get; set; }

        /// <summary>
        /// 最大仓库编号
        /// </summary>
        public int? MaxOOWarehouseNumber { get; set; }

        /// <summary>
        /// 最小货物编号
        /// </summary>
        public int? MinOOCargoNumber { get; set; }

        /// <summary>
        /// 最大货物编号
        /// </summary>
        public int? MaxOOCargoNumber { get; set; }

        /// <summary>
        /// 最小工号
        /// </summary>
        public int? MinOOJobNumber { get; set; }

        /// <summary>
        /// 最大工号
        /// </summary>
        public int? MaxOOJobNumber { get; set; }

        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime? FromOODate { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime? ToOODate { get; set; }

        /// <summary>
        /// 最小数量
        /// </summary>
        public int? MinOOAmount { get; set; }

        /// <summary>
        /// 最大数量
        /// </summary>
        public int? MaxOOAmount { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string OORemarks { get; set; }

    }
    /// <summary>
    /// 下架单 搜索条件实体模型
    /// </summary>
    public class LowerFrameSearchModel
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
        public int? MinLFShelfNumber { get; set; }

        /// <summary>
        /// 最大货架编号
        /// </summary>
        public int? MaxLFShelfNumber { get; set; }

        /// <summary>
        /// 最小货物编号
        /// </summary>
        public int? MinLFCargoNumber { get; set; }

        /// <summary>
        /// 最大货物编号
        /// </summary>
        public int? MaxLFCargoNumber { get; set; }

        /// <summary>
        /// 最小工号
        /// </summary>
        public int? MinLFJobNumber { get; set; }

        /// <summary>
        /// 最大工号
        /// </summary>
        public int? MaxLFJobNumber { get; set; }

        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime? FromLFDate { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime? ToLFDate { get; set; }

        /// <summary>
        /// 最小数量
        /// </summary>
        public int? MinLFAmount { get; set; }

        /// <summary>
        /// 最大数量
        /// </summary>
        public int? MaxLFAmount { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string LFRemarks { get; set; }

    }
    /// <summary>
    /// 销售凭证单 搜索条件实体模型
    /// </summary>
    public class SalesVoucherSearchModel
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
        public int? MinSVCustomerNumber { get; set; }

        /// <summary>
        /// 最大客户编号
        /// </summary>
        public int? MaxSVCustomerNumber { get; set; }

        /// <summary>
        /// 最小货物编号
        /// </summary>
        public int? MinSVCargoNumber { get; set; }

        /// <summary>
        /// 最大货物编号
        /// </summary>
        public int? MaxSVCargoNumber { get; set; }

        /// <summary>
        /// 最小工号
        /// </summary>
        public int? MinSVJobNumber { get; set; }

        /// <summary>
        /// 最大工号
        /// </summary>
        public int? MaxSVJobNumber { get; set; }

        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime? FromSVDate { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime? ToSVDate { get; set; }

        /// <summary>
        /// 最小数量
        /// </summary>
        public int? MinSVAmount { get; set; }

        /// <summary>
        /// 最大数量
        /// </summary>
        public int? MaxSVAmount { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public string SVPrice { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string SVRemarks { get; set; }

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
