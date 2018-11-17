


/* ------------------------------------------------------------ *
 * 此文件由生成器引擎根据既有规则生成，所有手工的更改将会被覆盖
 * 生成时间：11/17/2018 14:58:55
 * 生成版本：11/17/2018 14:58:50 
 * 作者：路正遥
 * ------------------------------------------------------------ */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data.Entity.Migrations;
using T.Evaluators;
using TEntities.EF;
using TENtities;
using TENtities.EF;

namespace T
{
    public class ImportProc
    {	
        public static string ExportEntities(string title)
        {
            var buf = new StringBuilder();
            using (var tx = new DefaultContext())
            {
				if(string.IsNullOrEmpty(title)) return string.Empty;
											
				else if (title == "Customertype")
				{
					buf.AppendLine("唯一编号\t客户编号\t姓名\t联系电话\t性别\t出生年月\t地址\t邮编\t备注\t顺序号\t版本号\t会话编号\t创建人\t创建时间\t更新人\t更新时间\t数据级别\t");
					var type = typeof (Customertype);
					foreach (var entity in tx.Customertype)
					{
						buf.Append(entity.id);
						buf.Append("\t");
						
													
						buf.Append(entity.CCustomerNumber);buf.Append("\t");
														
						buf.Append(entity.CName);buf.Append("\t");
														
						buf.Append(entity.CContactNumber);buf.Append("\t");
														
						buf.Append(entity.CChairperson);buf.Append("\t");
														
						buf.Append(entity.CDateOfBirth);buf.Append("\t");
														
						buf.Append(entity.CAddress);buf.Append("\t");
														
						buf.Append(entity.CZipCode);buf.Append("\t");
														
						buf.Append(entity.CRemarks);buf.Append("\t");
							
                        buf.Append(entity.ord); buf.Append("\t");
                        buf.Append(entity.VersionNo); buf.Append("\t");
                        buf.Append(entity.TransactionID); buf.Append("\t");
                        buf.Append(entity.CreateBy); buf.Append("\t");
                        buf.Append(entity.CreateOn); buf.Append("\t");
                        buf.Append(entity.UpdateBy); buf.Append("\t");
                        buf.Append(entity.UpdateOn); buf.Append("\t");
                        buf.Append(entity.DataLevel); buf.Append("\r\n");
					}
				}
											
				else if (title == "Supplier")
				{
					buf.AppendLine("唯一编号\t供应商编号\t供应商名称\t联系电话\t办公地点\t顺序号\t版本号\t会话编号\t创建人\t创建时间\t更新人\t更新时间\t数据级别\t");
					var type = typeof (Supplier);
					foreach (var entity in tx.Supplier)
					{
						buf.Append(entity.id);
						buf.Append("\t");
						
													
						buf.Append(entity.SSupplierNumber);buf.Append("\t");
														
						buf.Append(entity.SSupplierName);buf.Append("\t");
														
						buf.Append(entity.SContactNumber);buf.Append("\t");
														
						buf.Append(entity.SOfficeLocation);buf.Append("\t");
							
                        buf.Append(entity.ord); buf.Append("\t");
                        buf.Append(entity.VersionNo); buf.Append("\t");
                        buf.Append(entity.TransactionID); buf.Append("\t");
                        buf.Append(entity.CreateBy); buf.Append("\t");
                        buf.Append(entity.CreateOn); buf.Append("\t");
                        buf.Append(entity.UpdateBy); buf.Append("\t");
                        buf.Append(entity.UpdateOn); buf.Append("\t");
                        buf.Append(entity.DataLevel); buf.Append("\r\n");
					}
				}
											
				else if (title == "Cargo")
				{
					buf.AppendLine("唯一编号\t货物编号\t货物名称\t型号\t有无配件\t顺序号\t版本号\t会话编号\t创建人\t创建时间\t更新人\t更新时间\t数据级别\t");
					var type = typeof (Cargo);
					foreach (var entity in tx.Cargo)
					{
						buf.Append(entity.id);
						buf.Append("\t");
						
													
						buf.Append(entity.CCargoNumber);buf.Append("\t");
														
						buf.Append(entity.CNameOfGoods);buf.Append("\t");
														
						buf.Append(entity.CModel);buf.Append("\t");
														
						buf.Append(entity.CHaveParts);buf.Append("\t");
							
                        buf.Append(entity.ord); buf.Append("\t");
                        buf.Append(entity.VersionNo); buf.Append("\t");
                        buf.Append(entity.TransactionID); buf.Append("\t");
                        buf.Append(entity.CreateBy); buf.Append("\t");
                        buf.Append(entity.CreateOn); buf.Append("\t");
                        buf.Append(entity.UpdateBy); buf.Append("\t");
                        buf.Append(entity.UpdateOn); buf.Append("\t");
                        buf.Append(entity.DataLevel); buf.Append("\r\n");
					}
				}
											
				else if (title == "Staffname")
				{
					buf.AppendLine("唯一编号\t工号\t姓名\t学历\t联系电话\t顺序号\t版本号\t会话编号\t创建人\t创建时间\t更新人\t更新时间\t数据级别\t");
					var type = typeof (Staffname);
					foreach (var entity in tx.Staffname)
					{
						buf.Append(entity.id);
						buf.Append("\t");
						
													
						buf.Append(entity.SJobNumber);buf.Append("\t");
														
						buf.Append(entity.SName);buf.Append("\t");
														
						buf.Append(entity.SEducation);buf.Append("\t");
														
						buf.Append(entity.SContactNumber);buf.Append("\t");
							
                        buf.Append(entity.ord); buf.Append("\t");
                        buf.Append(entity.VersionNo); buf.Append("\t");
                        buf.Append(entity.TransactionID); buf.Append("\t");
                        buf.Append(entity.CreateBy); buf.Append("\t");
                        buf.Append(entity.CreateOn); buf.Append("\t");
                        buf.Append(entity.UpdateBy); buf.Append("\t");
                        buf.Append(entity.UpdateOn); buf.Append("\t");
                        buf.Append(entity.DataLevel); buf.Append("\r\n");
					}
				}
											
				else if (title == "Warehouse")
				{
					buf.AppendLine("唯一编号\t仓库编号\t容量\t地点\t顺序号\t版本号\t会话编号\t创建人\t创建时间\t更新人\t更新时间\t数据级别\t");
					var type = typeof (Warehouse);
					foreach (var entity in tx.Warehouse)
					{
						buf.Append(entity.id);
						buf.Append("\t");
						
													
						buf.Append(entity.WWarehouseNumber);buf.Append("\t");
														
						buf.Append(entity.WCapacity);buf.Append("\t");
														
						buf.Append(entity.WLocality);buf.Append("\t");
							
                        buf.Append(entity.ord); buf.Append("\t");
                        buf.Append(entity.VersionNo); buf.Append("\t");
                        buf.Append(entity.TransactionID); buf.Append("\t");
                        buf.Append(entity.CreateBy); buf.Append("\t");
                        buf.Append(entity.CreateOn); buf.Append("\t");
                        buf.Append(entity.UpdateBy); buf.Append("\t");
                        buf.Append(entity.UpdateOn); buf.Append("\t");
                        buf.Append(entity.DataLevel); buf.Append("\r\n");
					}
				}
											
				else if (title == "GoodsShelves")
				{
					buf.AppendLine("唯一编号\t货架编号\t容量\t地点\t顺序号\t版本号\t会话编号\t创建人\t创建时间\t更新人\t更新时间\t数据级别\t");
					var type = typeof (GoodsShelves);
					foreach (var entity in tx.GoodsShelves)
					{
						buf.Append(entity.id);
						buf.Append("\t");
						
													
						buf.Append(entity.GSShelfNumber);buf.Append("\t");
														
						buf.Append(entity.GSCapacity);buf.Append("\t");
														
						buf.Append(entity.GSLocality);buf.Append("\t");
							
                        buf.Append(entity.ord); buf.Append("\t");
                        buf.Append(entity.VersionNo); buf.Append("\t");
                        buf.Append(entity.TransactionID); buf.Append("\t");
                        buf.Append(entity.CreateBy); buf.Append("\t");
                        buf.Append(entity.CreateOn); buf.Append("\t");
                        buf.Append(entity.UpdateBy); buf.Append("\t");
                        buf.Append(entity.UpdateOn); buf.Append("\t");
                        buf.Append(entity.DataLevel); buf.Append("\r\n");
					}
				}
											
				else if (title == "PurchaseUnitPrice")
				{
					buf.AppendLine("唯一编号\t供应商编号\t货物编号\t采购员工号\t日期\t数量\t价格\t备注\t顺序号\t版本号\t会话编号\t创建人\t创建时间\t更新人\t更新时间\t数据级别\t");
					var type = typeof (PurchaseUnitPrice);
					foreach (var entity in tx.PurchaseUnitPrice)
					{
						buf.Append(entity.id);
						buf.Append("\t");
						
													
						buf.Append(entity.PUPSupplierNumber);buf.Append("\t");
														
						buf.Append(entity.PUPCargoNumber);buf.Append("\t");
														
						buf.Append(entity.PUPPurchasingStaffNumber);buf.Append("\t");
														
						buf.Append(entity.PUPDate);buf.Append("\t");
														
						buf.Append(entity.PUPAmount);buf.Append("\t");
														
						buf.Append(entity.PUPPrice);buf.Append("\t");
														
						buf.Append(entity.PUPRemarks);buf.Append("\t");
							
                        buf.Append(entity.ord); buf.Append("\t");
                        buf.Append(entity.VersionNo); buf.Append("\t");
                        buf.Append(entity.TransactionID); buf.Append("\t");
                        buf.Append(entity.CreateBy); buf.Append("\t");
                        buf.Append(entity.CreateOn); buf.Append("\t");
                        buf.Append(entity.UpdateBy); buf.Append("\t");
                        buf.Append(entity.UpdateOn); buf.Append("\t");
                        buf.Append(entity.DataLevel); buf.Append("\r\n");
					}
				}
											
				else if (title == "SalesUnitPrice")
				{
					buf.AppendLine("唯一编号\t货物编号\t客户编号\t销售员工号\t日期\t数量\t价格\t备注\t顺序号\t版本号\t会话编号\t创建人\t创建时间\t更新人\t更新时间\t数据级别\t");
					var type = typeof (SalesUnitPrice);
					foreach (var entity in tx.SalesUnitPrice)
					{
						buf.Append(entity.id);
						buf.Append("\t");
						
													
						buf.Append(entity.SUPCargoNumber);buf.Append("\t");
														
						buf.Append(entity.SUPCustomerNumber);buf.Append("\t");
														
						buf.Append(entity.SUPSalesStaffNumber);buf.Append("\t");
														
						buf.Append(entity.SUPDate);buf.Append("\t");
														
						buf.Append(entity.SUPAmount);buf.Append("\t");
														
						buf.Append(entity.SUPPrice);buf.Append("\t");
														
						buf.Append(entity.SUPRemarks);buf.Append("\t");
							
                        buf.Append(entity.ord); buf.Append("\t");
                        buf.Append(entity.VersionNo); buf.Append("\t");
                        buf.Append(entity.TransactionID); buf.Append("\t");
                        buf.Append(entity.CreateBy); buf.Append("\t");
                        buf.Append(entity.CreateOn); buf.Append("\t");
                        buf.Append(entity.UpdateBy); buf.Append("\t");
                        buf.Append(entity.UpdateOn); buf.Append("\t");
                        buf.Append(entity.DataLevel); buf.Append("\r\n");
					}
				}
											
				else if (title == "SupplyWarehousingList")
				{
					buf.AppendLine("唯一编号\t仓库编号\t货物编号\t供应商编号\t日期\t数量\t备注\t顺序号\t版本号\t会话编号\t创建人\t创建时间\t更新人\t更新时间\t数据级别\t");
					var type = typeof (SupplyWarehousingList);
					foreach (var entity in tx.SupplyWarehousingList)
					{
						buf.Append(entity.id);
						buf.Append("\t");
						
													
						buf.Append(entity.SWLWarehouseNumber);buf.Append("\t");
														
						buf.Append(entity.SWLCargoNumber);buf.Append("\t");
														
						buf.Append(entity.SWLSupplierNumber);buf.Append("\t");
														
						buf.Append(entity.SWLDate);buf.Append("\t");
														
						buf.Append(entity.SWLAmount);buf.Append("\t");
														
						buf.Append(entity.SWLRemarks);buf.Append("\t");
							
                        buf.Append(entity.ord); buf.Append("\t");
                        buf.Append(entity.VersionNo); buf.Append("\t");
                        buf.Append(entity.TransactionID); buf.Append("\t");
                        buf.Append(entity.CreateBy); buf.Append("\t");
                        buf.Append(entity.CreateOn); buf.Append("\t");
                        buf.Append(entity.UpdateBy); buf.Append("\t");
                        buf.Append(entity.UpdateOn); buf.Append("\t");
                        buf.Append(entity.DataLevel); buf.Append("\r\n");
					}
				}
											
				else if (title == "ReplenishmentBill")
				{
					buf.AppendLine("唯一编号\t货架编号\t仓库编号\t货物编号\t日期\t数量\t备注\t顺序号\t版本号\t会话编号\t创建人\t创建时间\t更新人\t更新时间\t数据级别\t");
					var type = typeof (ReplenishmentBill);
					foreach (var entity in tx.ReplenishmentBill)
					{
						buf.Append(entity.id);
						buf.Append("\t");
						
													
						buf.Append(entity.RBShelfNumber);buf.Append("\t");
														
						buf.Append(entity.RBWarehouseNumber);buf.Append("\t");
														
						buf.Append(entity.RBCargoNumber);buf.Append("\t");
														
						buf.Append(entity.RBDate);buf.Append("\t");
														
						buf.Append(entity.RBAmount);buf.Append("\t");
														
						buf.Append(entity.RBRemarks);buf.Append("\t");
							
                        buf.Append(entity.ord); buf.Append("\t");
                        buf.Append(entity.VersionNo); buf.Append("\t");
                        buf.Append(entity.TransactionID); buf.Append("\t");
                        buf.Append(entity.CreateBy); buf.Append("\t");
                        buf.Append(entity.CreateOn); buf.Append("\t");
                        buf.Append(entity.UpdateBy); buf.Append("\t");
                        buf.Append(entity.UpdateOn); buf.Append("\t");
                        buf.Append(entity.DataLevel); buf.Append("\r\n");
					}
				}
											
				else if (title == "WarehouseStock")
				{
					buf.AppendLine("唯一编号\t仓库编号\t货物编号\t数量\t备注\t顺序号\t版本号\t会话编号\t创建人\t创建时间\t更新人\t更新时间\t数据级别\t");
					var type = typeof (WarehouseStock);
					foreach (var entity in tx.WarehouseStock)
					{
						buf.Append(entity.id);
						buf.Append("\t");
						
													
						buf.Append(entity.WSWarehouseNumber);buf.Append("\t");
														
						buf.Append(entity.WSCargoNumber);buf.Append("\t");
														
						buf.Append(entity.WSAmount);buf.Append("\t");
														
						buf.Append(entity.WSRemarks);buf.Append("\t");
							
                        buf.Append(entity.ord); buf.Append("\t");
                        buf.Append(entity.VersionNo); buf.Append("\t");
                        buf.Append(entity.TransactionID); buf.Append("\t");
                        buf.Append(entity.CreateBy); buf.Append("\t");
                        buf.Append(entity.CreateOn); buf.Append("\t");
                        buf.Append(entity.UpdateBy); buf.Append("\t");
                        buf.Append(entity.UpdateOn); buf.Append("\t");
                        buf.Append(entity.DataLevel); buf.Append("\r\n");
					}
				}
											
				else if (title == "ShelfStock")
				{
					buf.AppendLine("唯一编号\t货架编号\t货物编号\t数量\t备注\t顺序号\t版本号\t会话编号\t创建人\t创建时间\t更新人\t更新时间\t数据级别\t");
					var type = typeof (ShelfStock);
					foreach (var entity in tx.ShelfStock)
					{
						buf.Append(entity.id);
						buf.Append("\t");
						
													
						buf.Append(entity.SSShelfNumber);buf.Append("\t");
														
						buf.Append(entity.SSCargoNumber);buf.Append("\t");
														
						buf.Append(entity.SSAmount);buf.Append("\t");
														
						buf.Append(entity.SSRemarks);buf.Append("\t");
							
                        buf.Append(entity.ord); buf.Append("\t");
                        buf.Append(entity.VersionNo); buf.Append("\t");
                        buf.Append(entity.TransactionID); buf.Append("\t");
                        buf.Append(entity.CreateBy); buf.Append("\t");
                        buf.Append(entity.CreateOn); buf.Append("\t");
                        buf.Append(entity.UpdateBy); buf.Append("\t");
                        buf.Append(entity.UpdateOn); buf.Append("\t");
                        buf.Append(entity.DataLevel); buf.Append("\r\n");
					}
				}
											
				else if (title == "ShelfList")
				{
					buf.AppendLine("唯一编号\t货架编号\t货物编号\t日期\t数量\t备注\t顺序号\t版本号\t会话编号\t创建人\t创建时间\t更新人\t更新时间\t数据级别\t");
					var type = typeof (ShelfList);
					foreach (var entity in tx.ShelfList)
					{
						buf.Append(entity.id);
						buf.Append("\t");
						
													
						buf.Append(entity.SLShelfNumber);buf.Append("\t");
														
						buf.Append(entity.SLCargoNumber);buf.Append("\t");
														
						buf.Append(entity.SLDate);buf.Append("\t");
														
						buf.Append(entity.SLAmount);buf.Append("\t");
														
						buf.Append(entity.SLRemarks);buf.Append("\t");
							
                        buf.Append(entity.ord); buf.Append("\t");
                        buf.Append(entity.VersionNo); buf.Append("\t");
                        buf.Append(entity.TransactionID); buf.Append("\t");
                        buf.Append(entity.CreateBy); buf.Append("\t");
                        buf.Append(entity.CreateOn); buf.Append("\t");
                        buf.Append(entity.UpdateBy); buf.Append("\t");
                        buf.Append(entity.UpdateOn); buf.Append("\t");
                        buf.Append(entity.DataLevel); buf.Append("\r\n");
					}
				}
											
				else if (title == "PurchaseVoucher")
				{
					buf.AppendLine("唯一编号\t供应商编号\t货物编号\t工号\t日期\t数量\t价格\t备注\t顺序号\t版本号\t会话编号\t创建人\t创建时间\t更新人\t更新时间\t数据级别\t");
					var type = typeof (PurchaseVoucher);
					foreach (var entity in tx.PurchaseVoucher)
					{
						buf.Append(entity.id);
						buf.Append("\t");
						
													
						buf.Append(entity.PVSupplierNumber);buf.Append("\t");
														
						buf.Append(entity.PVCargoNumber);buf.Append("\t");
														
						buf.Append(entity.PVJobNumber);buf.Append("\t");
														
						buf.Append(entity.PVDate);buf.Append("\t");
														
						buf.Append(entity.PVAmount);buf.Append("\t");
														
						buf.Append(entity.PVPrice);buf.Append("\t");
														
						buf.Append(entity.PVRemarks);buf.Append("\t");
							
                        buf.Append(entity.ord); buf.Append("\t");
                        buf.Append(entity.VersionNo); buf.Append("\t");
                        buf.Append(entity.TransactionID); buf.Append("\t");
                        buf.Append(entity.CreateBy); buf.Append("\t");
                        buf.Append(entity.CreateOn); buf.Append("\t");
                        buf.Append(entity.UpdateBy); buf.Append("\t");
                        buf.Append(entity.UpdateOn); buf.Append("\t");
                        buf.Append(entity.DataLevel); buf.Append("\r\n");
					}
				}
											
				else if (title == "OutboundOrder")
				{
					buf.AppendLine("唯一编号\t仓库编号\t货物编号\t工号\t日期\t数量\t备注\t顺序号\t版本号\t会话编号\t创建人\t创建时间\t更新人\t更新时间\t数据级别\t");
					var type = typeof (OutboundOrder);
					foreach (var entity in tx.OutboundOrder)
					{
						buf.Append(entity.id);
						buf.Append("\t");
						
													
						buf.Append(entity.OOWarehouseNumber);buf.Append("\t");
														
						buf.Append(entity.OOCargoNumber);buf.Append("\t");
														
						buf.Append(entity.OOJobNumber);buf.Append("\t");
														
						buf.Append(entity.OODate);buf.Append("\t");
														
						buf.Append(entity.OOAmount);buf.Append("\t");
														
						buf.Append(entity.OORemarks);buf.Append("\t");
							
                        buf.Append(entity.ord); buf.Append("\t");
                        buf.Append(entity.VersionNo); buf.Append("\t");
                        buf.Append(entity.TransactionID); buf.Append("\t");
                        buf.Append(entity.CreateBy); buf.Append("\t");
                        buf.Append(entity.CreateOn); buf.Append("\t");
                        buf.Append(entity.UpdateBy); buf.Append("\t");
                        buf.Append(entity.UpdateOn); buf.Append("\t");
                        buf.Append(entity.DataLevel); buf.Append("\r\n");
					}
				}
											
				else if (title == "LowerFrame")
				{
					buf.AppendLine("唯一编号\t货架编号\t货物编号\t工号\t日期\t数量\t备注\t顺序号\t版本号\t会话编号\t创建人\t创建时间\t更新人\t更新时间\t数据级别\t");
					var type = typeof (LowerFrame);
					foreach (var entity in tx.LowerFrame)
					{
						buf.Append(entity.id);
						buf.Append("\t");
						
													
						buf.Append(entity.LFShelfNumber);buf.Append("\t");
														
						buf.Append(entity.LFCargoNumber);buf.Append("\t");
														
						buf.Append(entity.LFJobNumber);buf.Append("\t");
														
						buf.Append(entity.LFDate);buf.Append("\t");
														
						buf.Append(entity.LFAmount);buf.Append("\t");
														
						buf.Append(entity.LFRemarks);buf.Append("\t");
							
                        buf.Append(entity.ord); buf.Append("\t");
                        buf.Append(entity.VersionNo); buf.Append("\t");
                        buf.Append(entity.TransactionID); buf.Append("\t");
                        buf.Append(entity.CreateBy); buf.Append("\t");
                        buf.Append(entity.CreateOn); buf.Append("\t");
                        buf.Append(entity.UpdateBy); buf.Append("\t");
                        buf.Append(entity.UpdateOn); buf.Append("\t");
                        buf.Append(entity.DataLevel); buf.Append("\r\n");
					}
				}
											
				else if (title == "SalesVoucher")
				{
					buf.AppendLine("唯一编号\t客户编号\t货物编号\t工号\t日期\t数量\t价格\t备注\t顺序号\t版本号\t会话编号\t创建人\t创建时间\t更新人\t更新时间\t数据级别\t");
					var type = typeof (SalesVoucher);
					foreach (var entity in tx.SalesVoucher)
					{
						buf.Append(entity.id);
						buf.Append("\t");
						
													
						buf.Append(entity.SVCustomerNumber);buf.Append("\t");
														
						buf.Append(entity.SVCargoNumber);buf.Append("\t");
														
						buf.Append(entity.SVJobNumber);buf.Append("\t");
														
						buf.Append(entity.SVDate);buf.Append("\t");
														
						buf.Append(entity.SVAmount);buf.Append("\t");
														
						buf.Append(entity.SVPrice);buf.Append("\t");
														
						buf.Append(entity.SVRemarks);buf.Append("\t");
							
                        buf.Append(entity.ord); buf.Append("\t");
                        buf.Append(entity.VersionNo); buf.Append("\t");
                        buf.Append(entity.TransactionID); buf.Append("\t");
                        buf.Append(entity.CreateBy); buf.Append("\t");
                        buf.Append(entity.CreateOn); buf.Append("\t");
                        buf.Append(entity.UpdateBy); buf.Append("\t");
                        buf.Append(entity.UpdateOn); buf.Append("\t");
                        buf.Append(entity.DataLevel); buf.Append("\r\n");
					}
				}
											
				else if (title == "MenuConfiguration")
				{
					buf.AppendLine("唯一编号\t标题\t父级标题\t链接\t菜单类型\t顺序\t显示名称\t图片\t顺序号\t版本号\t会话编号\t创建人\t创建时间\t更新人\t更新时间\t数据级别\t");
					var type = typeof (MenuConfiguration);
					foreach (var entity in tx.MenuConfiguration)
					{
						buf.Append(entity.id);
						buf.Append("\t");
						
													
						buf.Append(entity.MCCaption);buf.Append("\t");
														
						buf.Append(entity.MCParentTitle);buf.Append("\t");
														
						buf.Append(entity.MCLink);buf.Append("\t");
														
						buf.Append(entity.MCMenuType);buf.Append("\t");
														
						buf.Append(entity.MCSequence);buf.Append("\t");
														
						buf.Append(entity.MCDisplayName);buf.Append("\t");
														
						buf.Append(entity.MCPicture);buf.Append("\t");
							
                        buf.Append(entity.ord); buf.Append("\t");
                        buf.Append(entity.VersionNo); buf.Append("\t");
                        buf.Append(entity.TransactionID); buf.Append("\t");
                        buf.Append(entity.CreateBy); buf.Append("\t");
                        buf.Append(entity.CreateOn); buf.Append("\t");
                        buf.Append(entity.UpdateBy); buf.Append("\t");
                        buf.Append(entity.UpdateOn); buf.Append("\t");
                        buf.Append(entity.DataLevel); buf.Append("\r\n");
					}
				}
											
				else if (title == "RoleMenu")
				{
					buf.AppendLine("唯一编号\t角色名称\t菜单标题\t顺序号\t版本号\t会话编号\t创建人\t创建时间\t更新人\t更新时间\t数据级别\t");
					var type = typeof (RoleMenu);
					foreach (var entity in tx.RoleMenu)
					{
						buf.Append(entity.id);
						buf.Append("\t");
						
													
						buf.Append(entity.RMRoleName);buf.Append("\t");
														
						buf.Append(entity.RMMenuTitle);buf.Append("\t");
							
                        buf.Append(entity.ord); buf.Append("\t");
                        buf.Append(entity.VersionNo); buf.Append("\t");
                        buf.Append(entity.TransactionID); buf.Append("\t");
                        buf.Append(entity.CreateBy); buf.Append("\t");
                        buf.Append(entity.CreateOn); buf.Append("\t");
                        buf.Append(entity.UpdateBy); buf.Append("\t");
                        buf.Append(entity.UpdateOn); buf.Append("\t");
                        buf.Append(entity.DataLevel); buf.Append("\r\n");
					}
				}
											
				else if (title == "UserRole")
				{
					buf.AppendLine("唯一编号\t角色名称\t登录名\t顺序号\t版本号\t会话编号\t创建人\t创建时间\t更新人\t更新时间\t数据级别\t");
					var type = typeof (UserRole);
					foreach (var entity in tx.UserRole)
					{
						buf.Append(entity.id);
						buf.Append("\t");
						
													
						buf.Append(entity.URRoleName);buf.Append("\t");
														
						buf.Append(entity.URLoginName);buf.Append("\t");
							
                        buf.Append(entity.ord); buf.Append("\t");
                        buf.Append(entity.VersionNo); buf.Append("\t");
                        buf.Append(entity.TransactionID); buf.Append("\t");
                        buf.Append(entity.CreateBy); buf.Append("\t");
                        buf.Append(entity.CreateOn); buf.Append("\t");
                        buf.Append(entity.UpdateBy); buf.Append("\t");
                        buf.Append(entity.UpdateOn); buf.Append("\t");
                        buf.Append(entity.DataLevel); buf.Append("\r\n");
					}
				}
											
				else if (title == "RoleConfiguration")
				{
					buf.AppendLine("唯一编号\t角色名称\t所属组织\t顺序号\t版本号\t会话编号\t创建人\t创建时间\t更新人\t更新时间\t数据级别\t");
					var type = typeof (RoleConfiguration);
					foreach (var entity in tx.RoleConfiguration)
					{
						buf.Append(entity.id);
						buf.Append("\t");
						
													
						buf.Append(entity.RCRoleName);buf.Append("\t");
														
						buf.Append(entity.RCAffiliatedOrganization);buf.Append("\t");
							
                        buf.Append(entity.ord); buf.Append("\t");
                        buf.Append(entity.VersionNo); buf.Append("\t");
                        buf.Append(entity.TransactionID); buf.Append("\t");
                        buf.Append(entity.CreateBy); buf.Append("\t");
                        buf.Append(entity.CreateOn); buf.Append("\t");
                        buf.Append(entity.UpdateBy); buf.Append("\t");
                        buf.Append(entity.UpdateOn); buf.Append("\t");
                        buf.Append(entity.DataLevel); buf.Append("\r\n");
					}
				}
											
				else if (title == "UserInformation")
				{
					buf.AppendLine("唯一编号\t工号\t登录名\t昵称\t真实姓名\t头像\t部门\t职位\t电话\t照片\t用户类型\t用户级别\t入职时间\t离职时间\t密码\t顺序号\t版本号\t会话编号\t创建人\t创建时间\t更新人\t更新时间\t数据级别\t");
					var type = typeof (UserInformation);
					foreach (var entity in tx.UserInformation)
					{
						buf.Append(entity.id);
						buf.Append("\t");
						
													
						buf.Append(entity.UIJobNumber);buf.Append("\t");
														
						buf.Append(entity.UILoginName);buf.Append("\t");
														
						buf.Append(entity.UINickname);buf.Append("\t");
														
						buf.Append(entity.UIRealName);buf.Append("\t");
														
						buf.Append(entity.UIHeadPortrait);buf.Append("\t");
														
						buf.Append(entity.UIDepartment);buf.Append("\t");
														
						buf.Append(entity.UIPost);buf.Append("\t");
														
						buf.Append(entity.UIBooth);buf.Append("\t");
														
						buf.Append(entity.UIPhoto);buf.Append("\t");
														
						buf.Append(entity.UICustomerType);buf.Append("\t");
														
						buf.Append(entity.UIUserLevel);buf.Append("\t");
														
						buf.Append(entity.UITimeOfEntry);buf.Append("\t");
														
						buf.Append(entity.UIDepartureTime);buf.Append("\t");
														
						buf.Append(entity.UICode);buf.Append("\t");
							
                        buf.Append(entity.ord); buf.Append("\t");
                        buf.Append(entity.VersionNo); buf.Append("\t");
                        buf.Append(entity.TransactionID); buf.Append("\t");
                        buf.Append(entity.CreateBy); buf.Append("\t");
                        buf.Append(entity.CreateOn); buf.Append("\t");
                        buf.Append(entity.UpdateBy); buf.Append("\t");
                        buf.Append(entity.UpdateOn); buf.Append("\t");
                        buf.Append(entity.DataLevel); buf.Append("\r\n");
					}
				}
											
				else if (title == "LogonRecord")
				{
					buf.AppendLine("唯一编号\t登录名\t登录时间\t顺序号\t版本号\t会话编号\t创建人\t创建时间\t更新人\t更新时间\t数据级别\t");
					var type = typeof (LogonRecord);
					foreach (var entity in tx.LogonRecord)
					{
						buf.Append(entity.id);
						buf.Append("\t");
						
													
						buf.Append(entity.LRLoginName);buf.Append("\t");
														
						buf.Append(entity.LRLoginTime);buf.Append("\t");
							
                        buf.Append(entity.ord); buf.Append("\t");
                        buf.Append(entity.VersionNo); buf.Append("\t");
                        buf.Append(entity.TransactionID); buf.Append("\t");
                        buf.Append(entity.CreateBy); buf.Append("\t");
                        buf.Append(entity.CreateOn); buf.Append("\t");
                        buf.Append(entity.UpdateBy); buf.Append("\t");
                        buf.Append(entity.UpdateOn); buf.Append("\t");
                        buf.Append(entity.DataLevel); buf.Append("\r\n");
					}
				}
											
				else if (title == "UserMenu")
				{
					buf.AppendLine("唯一编号\t登录名\t标题\t顺序号\t版本号\t会话编号\t创建人\t创建时间\t更新人\t更新时间\t数据级别\t");
					var type = typeof (UserMenu);
					foreach (var entity in tx.UserMenu)
					{
						buf.Append(entity.id);
						buf.Append("\t");
						
													
						buf.Append(entity.UMLoginName);buf.Append("\t");
														
						buf.Append(entity.UMCaption);buf.Append("\t");
							
                        buf.Append(entity.ord); buf.Append("\t");
                        buf.Append(entity.VersionNo); buf.Append("\t");
                        buf.Append(entity.TransactionID); buf.Append("\t");
                        buf.Append(entity.CreateBy); buf.Append("\t");
                        buf.Append(entity.CreateOn); buf.Append("\t");
                        buf.Append(entity.UpdateBy); buf.Append("\t");
                        buf.Append(entity.UpdateOn); buf.Append("\t");
                        buf.Append(entity.DataLevel); buf.Append("\r\n");
					}
				}
											
				else if (title == "SystemConfiguration")
				{
					buf.AppendLine("唯一编号\t键\t值\t顺序号\t版本号\t会话编号\t创建人\t创建时间\t更新人\t更新时间\t数据级别\t");
					var type = typeof (SystemConfiguration);
					foreach (var entity in tx.SystemConfiguration)
					{
						buf.Append(entity.id);
						buf.Append("\t");
						
													
						buf.Append(entity.SCKey);buf.Append("\t");
														
						buf.Append(entity.SCAccrued);buf.Append("\t");
							
                        buf.Append(entity.ord); buf.Append("\t");
                        buf.Append(entity.VersionNo); buf.Append("\t");
                        buf.Append(entity.TransactionID); buf.Append("\t");
                        buf.Append(entity.CreateBy); buf.Append("\t");
                        buf.Append(entity.CreateOn); buf.Append("\t");
                        buf.Append(entity.UpdateBy); buf.Append("\t");
                        buf.Append(entity.UpdateOn); buf.Append("\t");
                        buf.Append(entity.DataLevel); buf.Append("\r\n");
					}
				}
				            }
            return buf.ToString();
        }

		public static void Proc(
			string fileType,
			string filePath,
			UserInformation user,
			out StringBuilder errorMsg){
			errorMsg = new StringBuilder();
            using (var tx = new DefaultContext())
            {
                Dictionary<string, Dictionary<string, string>> dic = null;
                string cacheFile = "table_mapping_file2018-11-17T145850.txt";
                if (File.Exists(cacheFile))
                    dic = File.ReadAllText(cacheFile).Deserialize<Dictionary<string, Dictionary<string, string>>>();
                else
                {
#region init
					dic = new Dictionary<string, Dictionary<string,string>>();
									
					dic.Add("Customertype", new Dictionary<string,string>{ {"CCustomerNumber","客户编号"},{"CName","姓名"},{"CContactNumber","联系电话"},{"CChairperson","性别"},{"CDateOfBirth","出生年月"},{"CAddress","地址"},{"CZipCode","邮编"},{"CRemarks","备注"} });
										
					dic.Add("Supplier", new Dictionary<string,string>{ {"SSupplierNumber","供应商编号"},{"SSupplierName","供应商名称"},{"SContactNumber","联系电话"},{"SOfficeLocation","办公地点"} });
										
					dic.Add("Cargo", new Dictionary<string,string>{ {"CCargoNumber","货物编号"},{"CNameOfGoods","货物名称"},{"CModel","型号"},{"CHaveParts","有无配件"} });
										
					dic.Add("Staffname", new Dictionary<string,string>{ {"SJobNumber","工号"},{"SName","姓名"},{"SEducation","学历"},{"SContactNumber","联系电话"} });
										
					dic.Add("Warehouse", new Dictionary<string,string>{ {"WWarehouseNumber","仓库编号"},{"WCapacity","容量"},{"WLocality","地点"} });
										
					dic.Add("GoodsShelves", new Dictionary<string,string>{ {"GSShelfNumber","货架编号"},{"GSCapacity","容量"},{"GSLocality","地点"} });
										
					dic.Add("PurchaseUnitPrice", new Dictionary<string,string>{ {"PUPSupplierNumber","供应商编号"},{"PUPCargoNumber","货物编号"},{"PUPPurchasingStaffNumber","采购员工号"},{"PUPDate","日期"},{"PUPAmount","数量"},{"PUPPrice","价格"},{"PUPRemarks","备注"} });
										
					dic.Add("SalesUnitPrice", new Dictionary<string,string>{ {"SUPCargoNumber","货物编号"},{"SUPCustomerNumber","客户编号"},{"SUPSalesStaffNumber","销售员工号"},{"SUPDate","日期"},{"SUPAmount","数量"},{"SUPPrice","价格"},{"SUPRemarks","备注"} });
										
					dic.Add("SupplyWarehousingList", new Dictionary<string,string>{ {"SWLWarehouseNumber","仓库编号"},{"SWLCargoNumber","货物编号"},{"SWLSupplierNumber","供应商编号"},{"SWLDate","日期"},{"SWLAmount","数量"},{"SWLRemarks","备注"} });
										
					dic.Add("ReplenishmentBill", new Dictionary<string,string>{ {"RBShelfNumber","货架编号"},{"RBWarehouseNumber","仓库编号"},{"RBCargoNumber","货物编号"},{"RBDate","日期"},{"RBAmount","数量"},{"RBRemarks","备注"} });
										
					dic.Add("WarehouseStock", new Dictionary<string,string>{ {"WSWarehouseNumber","仓库编号"},{"WSCargoNumber","货物编号"},{"WSAmount","数量"},{"WSRemarks","备注"} });
										
					dic.Add("ShelfStock", new Dictionary<string,string>{ {"SSShelfNumber","货架编号"},{"SSCargoNumber","货物编号"},{"SSAmount","数量"},{"SSRemarks","备注"} });
										
					dic.Add("ShelfList", new Dictionary<string,string>{ {"SLShelfNumber","货架编号"},{"SLCargoNumber","货物编号"},{"SLDate","日期"},{"SLAmount","数量"},{"SLRemarks","备注"} });
										
					dic.Add("PurchaseVoucher", new Dictionary<string,string>{ {"PVSupplierNumber","供应商编号"},{"PVCargoNumber","货物编号"},{"PVJobNumber","工号"},{"PVDate","日期"},{"PVAmount","数量"},{"PVPrice","价格"},{"PVRemarks","备注"} });
										
					dic.Add("OutboundOrder", new Dictionary<string,string>{ {"OOWarehouseNumber","仓库编号"},{"OOCargoNumber","货物编号"},{"OOJobNumber","工号"},{"OODate","日期"},{"OOAmount","数量"},{"OORemarks","备注"} });
										
					dic.Add("LowerFrame", new Dictionary<string,string>{ {"LFShelfNumber","货架编号"},{"LFCargoNumber","货物编号"},{"LFJobNumber","工号"},{"LFDate","日期"},{"LFAmount","数量"},{"LFRemarks","备注"} });
										
					dic.Add("SalesVoucher", new Dictionary<string,string>{ {"SVCustomerNumber","客户编号"},{"SVCargoNumber","货物编号"},{"SVJobNumber","工号"},{"SVDate","日期"},{"SVAmount","数量"},{"SVPrice","价格"},{"SVRemarks","备注"} });
										
					dic.Add("MenuConfiguration", new Dictionary<string,string>{ {"MCCaption","标题"},{"MCParentTitle","父级标题"},{"MCLink","链接"},{"MCMenuType","菜单类型"},{"MCSequence","顺序"},{"MCDisplayName","显示名称"},{"MCPicture","图片"} });
										
					dic.Add("RoleMenu", new Dictionary<string,string>{ {"RMRoleName","角色名称"},{"RMMenuTitle","菜单标题"} });
										
					dic.Add("UserRole", new Dictionary<string,string>{ {"URRoleName","角色名称"},{"URLoginName","登录名"} });
										
					dic.Add("RoleConfiguration", new Dictionary<string,string>{ {"RCRoleName","角色名称"},{"RCAffiliatedOrganization","所属组织"} });
										
					dic.Add("UserInformation", new Dictionary<string,string>{ {"UIJobNumber","工号"},{"UILoginName","登录名"},{"UINickname","昵称"},{"UIRealName","真实姓名"},{"UIHeadPortrait","头像"},{"UIDepartment","部门"},{"UIPost","职位"},{"UIBooth","电话"},{"UIPhoto","照片"},{"UICustomerType","用户类型"},{"UIUserLevel","用户级别"},{"UITimeOfEntry","入职时间"},{"UIDepartureTime","离职时间"},{"UICode","密码"} });
										
					dic.Add("LogonRecord", new Dictionary<string,string>{ {"LRLoginName","登录名"},{"LRLoginTime","登录时间"} });
										
					dic.Add("UserMenu", new Dictionary<string,string>{ {"UMLoginName","登录名"},{"UMCaption","标题"} });
										
					dic.Add("SystemConfiguration", new Dictionary<string,string>{ {"SCKey","键"},{"SCAccrued","值"} });
					
					File.WriteAllText(cacheFile,dic.ToJson());
#endregion
				}
				var transactionId = Guid.NewGuid().ToString();
                var keypair = dic[fileType]; //commentses.ToDictionary(f => f.column_name, f => f.column_description);
                if (string.IsNullOrEmpty(fileType)) return ;
				else if (fileType == "Customertype") ExcelHelper.ExcelToNewEntityList<Customertype>(keypair, filePath, out errorMsg).ForEach(one=>{
                    one.CreateBy = one.CreateBy ?? user?.UILoginName ?? "未登录用户";
                    one.UpdateBy = user?.UILoginName ?? "未登录用户";
                    one.CreateOn = one.CreateOn ?? DateTime.Now;
                    one.TransactionID = transactionId;
                    one.UpdateOn = DateTime.Now;
                    one.IsDeleted = 0;
                    one.DataLevel = user?.DataLevel ?? "01";
					tx.Customertype.AddOrUpdate(one);
					});
				else if (fileType == "Supplier") ExcelHelper.ExcelToNewEntityList<Supplier>(keypair, filePath, out errorMsg).ForEach(one=>{
                    one.CreateBy = one.CreateBy ?? user?.UILoginName ?? "未登录用户";
                    one.UpdateBy = user?.UILoginName ?? "未登录用户";
                    one.CreateOn = one.CreateOn ?? DateTime.Now;
                    one.TransactionID = transactionId;
                    one.UpdateOn = DateTime.Now;
                    one.IsDeleted = 0;
                    one.DataLevel = user?.DataLevel ?? "01";
					tx.Supplier.AddOrUpdate(one);
					});
				else if (fileType == "Cargo") ExcelHelper.ExcelToNewEntityList<Cargo>(keypair, filePath, out errorMsg).ForEach(one=>{
                    one.CreateBy = one.CreateBy ?? user?.UILoginName ?? "未登录用户";
                    one.UpdateBy = user?.UILoginName ?? "未登录用户";
                    one.CreateOn = one.CreateOn ?? DateTime.Now;
                    one.TransactionID = transactionId;
                    one.UpdateOn = DateTime.Now;
                    one.IsDeleted = 0;
                    one.DataLevel = user?.DataLevel ?? "01";
					tx.Cargo.AddOrUpdate(one);
					});
				else if (fileType == "Staffname") ExcelHelper.ExcelToNewEntityList<Staffname>(keypair, filePath, out errorMsg).ForEach(one=>{
                    one.CreateBy = one.CreateBy ?? user?.UILoginName ?? "未登录用户";
                    one.UpdateBy = user?.UILoginName ?? "未登录用户";
                    one.CreateOn = one.CreateOn ?? DateTime.Now;
                    one.TransactionID = transactionId;
                    one.UpdateOn = DateTime.Now;
                    one.IsDeleted = 0;
                    one.DataLevel = user?.DataLevel ?? "01";
					tx.Staffname.AddOrUpdate(one);
					});
				else if (fileType == "Warehouse") ExcelHelper.ExcelToNewEntityList<Warehouse>(keypair, filePath, out errorMsg).ForEach(one=>{
                    one.CreateBy = one.CreateBy ?? user?.UILoginName ?? "未登录用户";
                    one.UpdateBy = user?.UILoginName ?? "未登录用户";
                    one.CreateOn = one.CreateOn ?? DateTime.Now;
                    one.TransactionID = transactionId;
                    one.UpdateOn = DateTime.Now;
                    one.IsDeleted = 0;
                    one.DataLevel = user?.DataLevel ?? "01";
					tx.Warehouse.AddOrUpdate(one);
					});
				else if (fileType == "GoodsShelves") ExcelHelper.ExcelToNewEntityList<GoodsShelves>(keypair, filePath, out errorMsg).ForEach(one=>{
                    one.CreateBy = one.CreateBy ?? user?.UILoginName ?? "未登录用户";
                    one.UpdateBy = user?.UILoginName ?? "未登录用户";
                    one.CreateOn = one.CreateOn ?? DateTime.Now;
                    one.TransactionID = transactionId;
                    one.UpdateOn = DateTime.Now;
                    one.IsDeleted = 0;
                    one.DataLevel = user?.DataLevel ?? "01";
					tx.GoodsShelves.AddOrUpdate(one);
					});
				else if (fileType == "PurchaseUnitPrice") ExcelHelper.ExcelToNewEntityList<PurchaseUnitPrice>(keypair, filePath, out errorMsg).ForEach(one=>{
                    one.CreateBy = one.CreateBy ?? user?.UILoginName ?? "未登录用户";
                    one.UpdateBy = user?.UILoginName ?? "未登录用户";
                    one.CreateOn = one.CreateOn ?? DateTime.Now;
                    one.TransactionID = transactionId;
                    one.UpdateOn = DateTime.Now;
                    one.IsDeleted = 0;
                    one.DataLevel = user?.DataLevel ?? "01";
					tx.PurchaseUnitPrice.AddOrUpdate(one);
					});
				else if (fileType == "SalesUnitPrice") ExcelHelper.ExcelToNewEntityList<SalesUnitPrice>(keypair, filePath, out errorMsg).ForEach(one=>{
                    one.CreateBy = one.CreateBy ?? user?.UILoginName ?? "未登录用户";
                    one.UpdateBy = user?.UILoginName ?? "未登录用户";
                    one.CreateOn = one.CreateOn ?? DateTime.Now;
                    one.TransactionID = transactionId;
                    one.UpdateOn = DateTime.Now;
                    one.IsDeleted = 0;
                    one.DataLevel = user?.DataLevel ?? "01";
					tx.SalesUnitPrice.AddOrUpdate(one);
					});
				else if (fileType == "SupplyWarehousingList") ExcelHelper.ExcelToNewEntityList<SupplyWarehousingList>(keypair, filePath, out errorMsg).ForEach(one=>{
                    one.CreateBy = one.CreateBy ?? user?.UILoginName ?? "未登录用户";
                    one.UpdateBy = user?.UILoginName ?? "未登录用户";
                    one.CreateOn = one.CreateOn ?? DateTime.Now;
                    one.TransactionID = transactionId;
                    one.UpdateOn = DateTime.Now;
                    one.IsDeleted = 0;
                    one.DataLevel = user?.DataLevel ?? "01";
					tx.SupplyWarehousingList.AddOrUpdate(one);
					});
				else if (fileType == "ReplenishmentBill") ExcelHelper.ExcelToNewEntityList<ReplenishmentBill>(keypair, filePath, out errorMsg).ForEach(one=>{
                    one.CreateBy = one.CreateBy ?? user?.UILoginName ?? "未登录用户";
                    one.UpdateBy = user?.UILoginName ?? "未登录用户";
                    one.CreateOn = one.CreateOn ?? DateTime.Now;
                    one.TransactionID = transactionId;
                    one.UpdateOn = DateTime.Now;
                    one.IsDeleted = 0;
                    one.DataLevel = user?.DataLevel ?? "01";
					tx.ReplenishmentBill.AddOrUpdate(one);
					});
				else if (fileType == "WarehouseStock") ExcelHelper.ExcelToNewEntityList<WarehouseStock>(keypair, filePath, out errorMsg).ForEach(one=>{
                    one.CreateBy = one.CreateBy ?? user?.UILoginName ?? "未登录用户";
                    one.UpdateBy = user?.UILoginName ?? "未登录用户";
                    one.CreateOn = one.CreateOn ?? DateTime.Now;
                    one.TransactionID = transactionId;
                    one.UpdateOn = DateTime.Now;
                    one.IsDeleted = 0;
                    one.DataLevel = user?.DataLevel ?? "01";
					tx.WarehouseStock.AddOrUpdate(one);
					});
				else if (fileType == "ShelfStock") ExcelHelper.ExcelToNewEntityList<ShelfStock>(keypair, filePath, out errorMsg).ForEach(one=>{
                    one.CreateBy = one.CreateBy ?? user?.UILoginName ?? "未登录用户";
                    one.UpdateBy = user?.UILoginName ?? "未登录用户";
                    one.CreateOn = one.CreateOn ?? DateTime.Now;
                    one.TransactionID = transactionId;
                    one.UpdateOn = DateTime.Now;
                    one.IsDeleted = 0;
                    one.DataLevel = user?.DataLevel ?? "01";
					tx.ShelfStock.AddOrUpdate(one);
					});
				else if (fileType == "ShelfList") ExcelHelper.ExcelToNewEntityList<ShelfList>(keypair, filePath, out errorMsg).ForEach(one=>{
                    one.CreateBy = one.CreateBy ?? user?.UILoginName ?? "未登录用户";
                    one.UpdateBy = user?.UILoginName ?? "未登录用户";
                    one.CreateOn = one.CreateOn ?? DateTime.Now;
                    one.TransactionID = transactionId;
                    one.UpdateOn = DateTime.Now;
                    one.IsDeleted = 0;
                    one.DataLevel = user?.DataLevel ?? "01";
					tx.ShelfList.AddOrUpdate(one);
					});
				else if (fileType == "PurchaseVoucher") ExcelHelper.ExcelToNewEntityList<PurchaseVoucher>(keypair, filePath, out errorMsg).ForEach(one=>{
                    one.CreateBy = one.CreateBy ?? user?.UILoginName ?? "未登录用户";
                    one.UpdateBy = user?.UILoginName ?? "未登录用户";
                    one.CreateOn = one.CreateOn ?? DateTime.Now;
                    one.TransactionID = transactionId;
                    one.UpdateOn = DateTime.Now;
                    one.IsDeleted = 0;
                    one.DataLevel = user?.DataLevel ?? "01";
					tx.PurchaseVoucher.AddOrUpdate(one);
					});
				else if (fileType == "OutboundOrder") ExcelHelper.ExcelToNewEntityList<OutboundOrder>(keypair, filePath, out errorMsg).ForEach(one=>{
                    one.CreateBy = one.CreateBy ?? user?.UILoginName ?? "未登录用户";
                    one.UpdateBy = user?.UILoginName ?? "未登录用户";
                    one.CreateOn = one.CreateOn ?? DateTime.Now;
                    one.TransactionID = transactionId;
                    one.UpdateOn = DateTime.Now;
                    one.IsDeleted = 0;
                    one.DataLevel = user?.DataLevel ?? "01";
					tx.OutboundOrder.AddOrUpdate(one);
					});
				else if (fileType == "LowerFrame") ExcelHelper.ExcelToNewEntityList<LowerFrame>(keypair, filePath, out errorMsg).ForEach(one=>{
                    one.CreateBy = one.CreateBy ?? user?.UILoginName ?? "未登录用户";
                    one.UpdateBy = user?.UILoginName ?? "未登录用户";
                    one.CreateOn = one.CreateOn ?? DateTime.Now;
                    one.TransactionID = transactionId;
                    one.UpdateOn = DateTime.Now;
                    one.IsDeleted = 0;
                    one.DataLevel = user?.DataLevel ?? "01";
					tx.LowerFrame.AddOrUpdate(one);
					});
				else if (fileType == "SalesVoucher") ExcelHelper.ExcelToNewEntityList<SalesVoucher>(keypair, filePath, out errorMsg).ForEach(one=>{
                    one.CreateBy = one.CreateBy ?? user?.UILoginName ?? "未登录用户";
                    one.UpdateBy = user?.UILoginName ?? "未登录用户";
                    one.CreateOn = one.CreateOn ?? DateTime.Now;
                    one.TransactionID = transactionId;
                    one.UpdateOn = DateTime.Now;
                    one.IsDeleted = 0;
                    one.DataLevel = user?.DataLevel ?? "01";
					tx.SalesVoucher.AddOrUpdate(one);
					});
				else if (fileType == "MenuConfiguration") ExcelHelper.ExcelToNewEntityList<MenuConfiguration>(keypair, filePath, out errorMsg).ForEach(one=>{
                    one.CreateBy = one.CreateBy ?? user?.UILoginName ?? "未登录用户";
                    one.UpdateBy = user?.UILoginName ?? "未登录用户";
                    one.CreateOn = one.CreateOn ?? DateTime.Now;
                    one.TransactionID = transactionId;
                    one.UpdateOn = DateTime.Now;
                    one.IsDeleted = 0;
                    one.DataLevel = user?.DataLevel ?? "01";
					tx.MenuConfiguration.AddOrUpdate(one);
					});
				else if (fileType == "RoleMenu") ExcelHelper.ExcelToNewEntityList<RoleMenu>(keypair, filePath, out errorMsg).ForEach(one=>{
                    one.CreateBy = one.CreateBy ?? user?.UILoginName ?? "未登录用户";
                    one.UpdateBy = user?.UILoginName ?? "未登录用户";
                    one.CreateOn = one.CreateOn ?? DateTime.Now;
                    one.TransactionID = transactionId;
                    one.UpdateOn = DateTime.Now;
                    one.IsDeleted = 0;
                    one.DataLevel = user?.DataLevel ?? "01";
					tx.RoleMenu.AddOrUpdate(one);
					});
				else if (fileType == "UserRole") ExcelHelper.ExcelToNewEntityList<UserRole>(keypair, filePath, out errorMsg).ForEach(one=>{
                    one.CreateBy = one.CreateBy ?? user?.UILoginName ?? "未登录用户";
                    one.UpdateBy = user?.UILoginName ?? "未登录用户";
                    one.CreateOn = one.CreateOn ?? DateTime.Now;
                    one.TransactionID = transactionId;
                    one.UpdateOn = DateTime.Now;
                    one.IsDeleted = 0;
                    one.DataLevel = user?.DataLevel ?? "01";
					tx.UserRole.AddOrUpdate(one);
					});
				else if (fileType == "RoleConfiguration") ExcelHelper.ExcelToNewEntityList<RoleConfiguration>(keypair, filePath, out errorMsg).ForEach(one=>{
                    one.CreateBy = one.CreateBy ?? user?.UILoginName ?? "未登录用户";
                    one.UpdateBy = user?.UILoginName ?? "未登录用户";
                    one.CreateOn = one.CreateOn ?? DateTime.Now;
                    one.TransactionID = transactionId;
                    one.UpdateOn = DateTime.Now;
                    one.IsDeleted = 0;
                    one.DataLevel = user?.DataLevel ?? "01";
					tx.RoleConfiguration.AddOrUpdate(one);
					});
				else if (fileType == "UserInformation") ExcelHelper.ExcelToNewEntityList<UserInformation>(keypair, filePath, out errorMsg).ForEach(one=>{
                    one.CreateBy = one.CreateBy ?? user?.UILoginName ?? "未登录用户";
                    one.UpdateBy = user?.UILoginName ?? "未登录用户";
                    one.CreateOn = one.CreateOn ?? DateTime.Now;
                    one.TransactionID = transactionId;
                    one.UpdateOn = DateTime.Now;
                    one.IsDeleted = 0;
                    one.DataLevel = user?.DataLevel ?? "01";
					tx.UserInformation.AddOrUpdate(one);
					});
				else if (fileType == "LogonRecord") ExcelHelper.ExcelToNewEntityList<LogonRecord>(keypair, filePath, out errorMsg).ForEach(one=>{
                    one.CreateBy = one.CreateBy ?? user?.UILoginName ?? "未登录用户";
                    one.UpdateBy = user?.UILoginName ?? "未登录用户";
                    one.CreateOn = one.CreateOn ?? DateTime.Now;
                    one.TransactionID = transactionId;
                    one.UpdateOn = DateTime.Now;
                    one.IsDeleted = 0;
                    one.DataLevel = user?.DataLevel ?? "01";
					tx.LogonRecord.AddOrUpdate(one);
					});
				else if (fileType == "UserMenu") ExcelHelper.ExcelToNewEntityList<UserMenu>(keypair, filePath, out errorMsg).ForEach(one=>{
                    one.CreateBy = one.CreateBy ?? user?.UILoginName ?? "未登录用户";
                    one.UpdateBy = user?.UILoginName ?? "未登录用户";
                    one.CreateOn = one.CreateOn ?? DateTime.Now;
                    one.TransactionID = transactionId;
                    one.UpdateOn = DateTime.Now;
                    one.IsDeleted = 0;
                    one.DataLevel = user?.DataLevel ?? "01";
					tx.UserMenu.AddOrUpdate(one);
					});
				else if (fileType == "SystemConfiguration") ExcelHelper.ExcelToNewEntityList<SystemConfiguration>(keypair, filePath, out errorMsg).ForEach(one=>{
                    one.CreateBy = one.CreateBy ?? user?.UILoginName ?? "未登录用户";
                    one.UpdateBy = user?.UILoginName ?? "未登录用户";
                    one.CreateOn = one.CreateOn ?? DateTime.Now;
                    one.TransactionID = transactionId;
                    one.UpdateOn = DateTime.Now;
                    one.IsDeleted = 0;
                    one.DataLevel = user?.DataLevel ?? "01";
					tx.SystemConfiguration.AddOrUpdate(one);
					});					tx.SaveChanges();
            }
		}
    }
}
