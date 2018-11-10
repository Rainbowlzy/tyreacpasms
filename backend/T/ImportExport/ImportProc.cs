

/* ------------------------------------------------------------ *
 * 此文件由生成器引擎根据既有规则生成，所有手工的更改将会被覆盖
 * 生成时间：11/11/2018 01:05:55
 * 生成版本：11/10/2018 13:57:48 
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
using EF.Entities;

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
											
				else if (title == "Customertype")
				{
					buf.AppendLine("唯一编号\t客户编号\t姓名\t性别\t称呼\t联系方式\t地址\t顺序号\t版本号\t会话编号\t创建人\t创建时间\t更新人\t更新时间\t数据级别\t");
					var type = typeof (Customertype);
					foreach (var entity in tx.Customertype)
					{
						buf.Append(entity.id);
						buf.Append("\t");
						
													
						buf.Append(entity.CCustomerNumber);buf.Append("\t");
														
						buf.Append(entity.CName);buf.Append("\t");
														
						buf.Append(entity.CChairperson);buf.Append("\t");
														
						buf.Append(entity.CCall);buf.Append("\t");
														
						buf.Append(entity.CCommonModeOfContact);buf.Append("\t");
														
						buf.Append(entity.CAddress);buf.Append("\t");
							
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
					buf.AppendLine("唯一编号\t供应商编号\t供应商名称\t联系方式\t办公地点\t经营范围\t顺序号\t版本号\t会话编号\t创建人\t创建时间\t更新人\t更新时间\t数据级别\t");
					var type = typeof (Supplier);
					foreach (var entity in tx.Supplier)
					{
						buf.Append(entity.id);
						buf.Append("\t");
						
													
						buf.Append(entity.SSupplierNumber);buf.Append("\t");
														
						buf.Append(entity.SSupplierName);buf.Append("\t");
														
						buf.Append(entity.SCommonModeOfContact);buf.Append("\t");
														
						buf.Append(entity.SOfficeLocation);buf.Append("\t");
														
						buf.Append(entity.SScopeOfOperation);buf.Append("\t");
							
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
											
				else if (title == "TypeOfGoods")
				{
					buf.AppendLine("唯一编号\t货品种类编号\t货物名称\t货物类别\t货物子类别\t体积\t颜色\t型号\t别称\t采购单价\t销售单价\t顺序号\t版本号\t会话编号\t创建人\t创建时间\t更新人\t更新时间\t数据级别\t");
					var type = typeof (TypeOfGoods);
					foreach (var entity in tx.TypeOfGoods)
					{
						buf.Append(entity.id);
						buf.Append("\t");
						
													
						buf.Append(entity.TOGCategoryNumberOfGoods);buf.Append("\t");
														
						buf.Append(entity.TOGNameOfGoods);buf.Append("\t");
														
						buf.Append(entity.TOGCategoryOfGoods);buf.Append("\t");
														
						buf.Append(entity.TOGCargoSubcategory);buf.Append("\t");
														
						buf.Append(entity.TOGBulk);buf.Append("\t");
														
						buf.Append(entity.TOGColor);buf.Append("\t");
														
						buf.Append(entity.TOGModel);buf.Append("\t");
														
						buf.Append(entity.TOGAlias);buf.Append("\t");
														
						buf.Append(entity.TOGPurchaseUnitPrice);buf.Append("\t");
														
						buf.Append(entity.TOGSalesUnitPrice);buf.Append("\t");
							
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
											
				else if (title == "SupplyChannel")
				{
					buf.AppendLine("唯一编号\t供应商\t货物种类\t顺序号\t版本号\t会话编号\t创建人\t创建时间\t更新人\t更新时间\t数据级别\t");
					var type = typeof (SupplyChannel);
					foreach (var entity in tx.SupplyChannel)
					{
						buf.Append(entity.id);
						buf.Append("\t");
						
													
						buf.Append(entity.SCSupplier);buf.Append("\t");
														
						buf.Append(entity.SCTypeOfGoods);buf.Append("\t");
							
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
											
				else if (title == "Order")
				{
					buf.AppendLine("唯一编号\t订单编号\t供应商编号\t期望到达日期\t提交日期\t提交人\t提交人联系方式\t订单状态\t备注\t顺序号\t版本号\t会话编号\t创建人\t创建时间\t更新人\t更新时间\t数据级别\t");
					var type = typeof (Order);
					foreach (var entity in tx.Order)
					{
						buf.Append(entity.id);
						buf.Append("\t");
						
													
						buf.Append(entity.OOrderNumber);buf.Append("\t");
														
						buf.Append(entity.OSupplierNumber);buf.Append("\t");
														
						buf.Append(entity.OExpectedArrivalDate);buf.Append("\t");
														
						buf.Append(entity.ODateOfSubmission);buf.Append("\t");
														
						buf.Append(entity.OSubmitter);buf.Append("\t");
														
						buf.Append(entity.OAuthorsContactInformation);buf.Append("\t");
														
						buf.Append(entity.OOrderStatus);buf.Append("\t");
														
						buf.Append(entity.ORemarks);buf.Append("\t");
							
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
											
				else if (title == "OrderDetails")
				{
					buf.AppendLine("唯一编号\t订单编号\t货物种类\t货物数量\t采购单价\t顺序号\t版本号\t会话编号\t创建人\t创建时间\t更新人\t更新时间\t数据级别\t");
					var type = typeof (OrderDetails);
					foreach (var entity in tx.OrderDetails)
					{
						buf.Append(entity.id);
						buf.Append("\t");
						
													
						buf.Append(entity.ODOrderNumber);buf.Append("\t");
														
						buf.Append(entity.ODTypeOfGoods);buf.Append("\t");
														
						buf.Append(entity.ODQuantityOfGoods);buf.Append("\t");
														
						buf.Append(entity.ODPurchaseUnitPrice);buf.Append("\t");
							
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
											
				else if (title == "WarehousingRecord")
				{
					buf.AppendLine("唯一编号\t订单编号\t到货日期\t经办人\t经办人联系方式\t备注\t顺序号\t版本号\t会话编号\t创建人\t创建时间\t更新人\t更新时间\t数据级别\t");
					var type = typeof (WarehousingRecord);
					foreach (var entity in tx.WarehousingRecord)
					{
						buf.Append(entity.id);
						buf.Append("\t");
						
													
						buf.Append(entity.WROrderNumber);buf.Append("\t");
														
						buf.Append(entity.WRDateOfArrival);buf.Append("\t");
														
						buf.Append(entity.WRAgent);buf.Append("\t");
														
						buf.Append(entity.WROperatorContact);buf.Append("\t");
														
						buf.Append(entity.WRRemarks);buf.Append("\t");
							
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
					buf.AppendLine("唯一编号\t仓库编号\t容积\t位置\t负责人工号\t顺序号\t版本号\t会话编号\t创建人\t创建时间\t更新人\t更新时间\t数据级别\t");
					var type = typeof (Warehouse);
					foreach (var entity in tx.Warehouse)
					{
						buf.Append(entity.id);
						buf.Append("\t");
						
													
						buf.Append(entity.WWarehouseNumber);buf.Append("\t");
														
						buf.Append(entity.WCapacity);buf.Append("\t");
														
						buf.Append(entity.WLocation);buf.Append("\t");
														
						buf.Append(entity.WResponsibleForManualNumber);buf.Append("\t");
							
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
					buf.AppendLine("唯一编号\t货架编号\t容积 \t位置\t负责人工号\t顺序号\t版本号\t会话编号\t创建人\t创建时间\t更新人\t更新时间\t数据级别\t");
					var type = typeof (GoodsShelves);
					foreach (var entity in tx.GoodsShelves)
					{
						buf.Append(entity.id);
						buf.Append("\t");
						
													
						buf.Append(entity.GSShelfNumber);buf.Append("\t");
														
						buf.Append(entity.GS);buf.Append("\t");
														
						buf.Append(entity.GSLocation);buf.Append("\t");
														
						buf.Append(entity.GSResponsibleForManualNumber);buf.Append("\t");
							
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
											
				else if (title == "ReplenishmentApplicationForm")
				{
					buf.AppendLine("唯一编号\t申请单编号\t货架编号\t仓库编号\t申请人工号\t货品种类编号\t货品数量\t申请日期\t申请单状态\t备注\t顺序号\t版本号\t会话编号\t创建人\t创建时间\t更新人\t更新时间\t数据级别\t");
					var type = typeof (ReplenishmentApplicationForm);
					foreach (var entity in tx.ReplenishmentApplicationForm)
					{
						buf.Append(entity.id);
						buf.Append("\t");
						
													
						buf.Append(entity.RAFApplicationNumber);buf.Append("\t");
														
						buf.Append(entity.RAFShelfNumber);buf.Append("\t");
														
						buf.Append(entity.RAFWarehouseNumber);buf.Append("\t");
														
						buf.Append(entity.RAFApplicationManualNumber);buf.Append("\t");
														
						buf.Append(entity.RAFCategoryNumberOfGoods);buf.Append("\t");
														
						buf.Append(entity.RAFQuantityOfGoods);buf.Append("\t");
														
						buf.Append(entity.RAFApplicationDate);buf.Append("\t");
														
						buf.Append(entity.RAFApplicationStatus);buf.Append("\t");
														
						buf.Append(entity.RAFRemarks);buf.Append("\t");
							
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
											
				else if (title == "ReplenishmentRecord")
				{
					buf.AppendLine("唯一编号\t申请单编号\t到货日期\t备注\t顺序号\t版本号\t会话编号\t创建人\t创建时间\t更新人\t更新时间\t数据级别\t");
					var type = typeof (ReplenishmentRecord);
					foreach (var entity in tx.ReplenishmentRecord)
					{
						buf.Append(entity.id);
						buf.Append("\t");
						
													
						buf.Append(entity.RRApplicationNumber);buf.Append("\t");
														
						buf.Append(entity.RRDateOfArrival);buf.Append("\t");
														
						buf.Append(entity.RRRemarks);buf.Append("\t");
							
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
											
				else if (title == "SalesRecord")
				{
					buf.AppendLine("唯一编号\t销售批次号\t货物种类\t数量\t单价\t销售工号\t发票编号\t发票抬头\t税号\t备注\t顺序号\t版本号\t会话编号\t创建人\t创建时间\t更新人\t更新时间\t数据级别\t");
					var type = typeof (SalesRecord);
					foreach (var entity in tx.SalesRecord)
					{
						buf.Append(entity.id);
						buf.Append("\t");
						
													
						buf.Append(entity.SRSalesLotNumber);buf.Append("\t");
														
						buf.Append(entity.SRTypeOfGoods);buf.Append("\t");
														
						buf.Append(entity.SRAmount);buf.Append("\t");
														
						buf.Append(entity.SRUnitPrice);buf.Append("\t");
														
						buf.Append(entity.SRSalesNumber);buf.Append("\t");
														
						buf.Append(entity.SRInvoiceNumber);buf.Append("\t");
														
						buf.Append(entity.SRInvoicesAreRaised);buf.Append("\t");
														
						buf.Append(entity.SRDutyParagraph);buf.Append("\t");
														
						buf.Append(entity.SRRemarks);buf.Append("\t");
							
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
                string cacheFile = "table_mapping_file2018-11-10T135748.txt";
                if (File.Exists(cacheFile))
                    dic = File.ReadAllText(cacheFile).Deserialize<Dictionary<string, Dictionary<string, string>>>();
                else
                {
#region init
					dic = new Dictionary<string, Dictionary<string,string>>();
									
					dic.Add("RoleMenu", new Dictionary<string,string>{ {"RMRoleName","角色名称"},{"RMMenuTitle","菜单标题"} });
										
					dic.Add("RoleConfiguration", new Dictionary<string,string>{ {"RCRoleName","角色名称"},{"RCAffiliatedOrganization","所属组织"} });
										
					dic.Add("UserRole", new Dictionary<string,string>{ {"URRoleName","角色名称"},{"URLoginName","登录名"} });
										
					dic.Add("MenuConfiguration", new Dictionary<string,string>{ {"MCCaption","标题"},{"MCParentTitle","父级标题"},{"MCLink","链接"},{"MCMenuType","菜单类型"},{"MCSequence","顺序"},{"MCDisplayName","显示名称"},{"MCPicture","图片"} });
										
					dic.Add("UserInformation", new Dictionary<string,string>{ {"UIJobNumber","工号"},{"UILoginName","登录名"},{"UINickname","昵称"},{"UIRealName","真实姓名"},{"UIHeadPortrait","头像"},{"UIDepartment","部门"},{"UIPost","职位"},{"UIBooth","电话"},{"UIPhoto","照片"},{"UICustomerType","用户类型"},{"UIUserLevel","用户级别"},{"UITimeOfEntry","入职时间"},{"UIDepartureTime","离职时间"},{"UICode","密码"} });
										
					dic.Add("LogonRecord", new Dictionary<string,string>{ {"LRLoginName","登录名"},{"LRLoginTime","登录时间"} });
										
					dic.Add("UserMenu", new Dictionary<string,string>{ {"UMLoginName","登录名"},{"UMCaption","标题"} });
										
					dic.Add("SystemConfiguration", new Dictionary<string,string>{ {"SCKey","键"},{"SCAccrued","值"} });
										
					dic.Add("Customertype", new Dictionary<string,string>{ {"CCustomerNumber","客户编号"},{"CName","姓名"},{"CChairperson","性别"},{"CCall","称呼"},{"CCommonModeOfContact","联系方式"},{"CAddress","地址"} });
										
					dic.Add("Supplier", new Dictionary<string,string>{ {"SSupplierNumber","供应商编号"},{"SSupplierName","供应商名称"},{"SCommonModeOfContact","联系方式"},{"SOfficeLocation","办公地点"},{"SScopeOfOperation","经营范围"} });
										
					dic.Add("TypeOfGoods", new Dictionary<string,string>{ {"TOGCategoryNumberOfGoods","货品种类编号"},{"TOGNameOfGoods","货物名称"},{"TOGCategoryOfGoods","货物类别"},{"TOGCargoSubcategory","货物子类别"},{"TOGBulk","体积"},{"TOGColor","颜色"},{"TOGModel","型号"},{"TOGAlias","别称"},{"TOGPurchaseUnitPrice","采购单价"},{"TOGSalesUnitPrice","销售单价"} });
										
					dic.Add("SupplyChannel", new Dictionary<string,string>{ {"SCSupplier","供应商"},{"SCTypeOfGoods","货物种类"} });
										
					dic.Add("Order", new Dictionary<string,string>{ {"OOrderNumber","订单编号"},{"OSupplierNumber","供应商编号"},{"OExpectedArrivalDate","期望到达日期"},{"ODateOfSubmission","提交日期"},{"OSubmitter","提交人"},{"OAuthorsContactInformation","提交人联系方式"},{"OOrderStatus","订单状态"},{"ORemarks","备注"} });
										
					dic.Add("OrderDetails", new Dictionary<string,string>{ {"ODOrderNumber","订单编号"},{"ODTypeOfGoods","货物种类"},{"ODQuantityOfGoods","货物数量"},{"ODPurchaseUnitPrice","采购单价"} });
										
					dic.Add("WarehousingRecord", new Dictionary<string,string>{ {"WROrderNumber","订单编号"},{"WRDateOfArrival","到货日期"},{"WRAgent","经办人"},{"WROperatorContact","经办人联系方式"},{"WRRemarks","备注"} });
										
					dic.Add("Warehouse", new Dictionary<string,string>{ {"WWarehouseNumber","仓库编号"},{"WCapacity","容积"},{"WLocation","位置"},{"WResponsibleForManualNumber","负责人工号"} });
										
					dic.Add("GoodsShelves", new Dictionary<string,string>{ {"GSShelfNumber","货架编号"},{"GS","容积 "},{"GSLocation","位置"},{"GSResponsibleForManualNumber","负责人工号"} });
										
					dic.Add("ReplenishmentApplicationForm", new Dictionary<string,string>{ {"RAFApplicationNumber","申请单编号"},{"RAFShelfNumber","货架编号"},{"RAFWarehouseNumber","仓库编号"},{"RAFApplicationManualNumber","申请人工号"},{"RAFCategoryNumberOfGoods","货品种类编号"},{"RAFQuantityOfGoods","货品数量"},{"RAFApplicationDate","申请日期"},{"RAFApplicationStatus","申请单状态"},{"RAFRemarks","备注"} });
										
					dic.Add("ReplenishmentRecord", new Dictionary<string,string>{ {"RRApplicationNumber","申请单编号"},{"RRDateOfArrival","到货日期"},{"RRRemarks","备注"} });
										
					dic.Add("SalesRecord", new Dictionary<string,string>{ {"SRSalesLotNumber","销售批次号"},{"SRTypeOfGoods","货物种类"},{"SRAmount","数量"},{"SRUnitPrice","单价"},{"SRSalesNumber","销售工号"},{"SRInvoiceNumber","发票编号"},{"SRInvoicesAreRaised","发票抬头"},{"SRDutyParagraph","税号"},{"SRRemarks","备注"} });
					
					File.WriteAllText(cacheFile,dic.ToJson());
#endregion
				}
				var transactionId = Guid.NewGuid().ToString();
                var keypair = dic[fileType]; //commentses.ToDictionary(f => f.column_name, f => f.column_description);
                if (string.IsNullOrEmpty(fileType)) return ;
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
					});
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
				else if (fileType == "TypeOfGoods") ExcelHelper.ExcelToNewEntityList<TypeOfGoods>(keypair, filePath, out errorMsg).ForEach(one=>{
                    one.CreateBy = one.CreateBy ?? user?.UILoginName ?? "未登录用户";
                    one.UpdateBy = user?.UILoginName ?? "未登录用户";
                    one.CreateOn = one.CreateOn ?? DateTime.Now;
                    one.TransactionID = transactionId;
                    one.UpdateOn = DateTime.Now;
                    one.IsDeleted = 0;
                    one.DataLevel = user?.DataLevel ?? "01";
					tx.TypeOfGoods.AddOrUpdate(one);
					});
				else if (fileType == "SupplyChannel") ExcelHelper.ExcelToNewEntityList<SupplyChannel>(keypair, filePath, out errorMsg).ForEach(one=>{
                    one.CreateBy = one.CreateBy ?? user?.UILoginName ?? "未登录用户";
                    one.UpdateBy = user?.UILoginName ?? "未登录用户";
                    one.CreateOn = one.CreateOn ?? DateTime.Now;
                    one.TransactionID = transactionId;
                    one.UpdateOn = DateTime.Now;
                    one.IsDeleted = 0;
                    one.DataLevel = user?.DataLevel ?? "01";
					tx.SupplyChannel.AddOrUpdate(one);
					});
				else if (fileType == "Order") ExcelHelper.ExcelToNewEntityList<Order>(keypair, filePath, out errorMsg).ForEach(one=>{
                    one.CreateBy = one.CreateBy ?? user?.UILoginName ?? "未登录用户";
                    one.UpdateBy = user?.UILoginName ?? "未登录用户";
                    one.CreateOn = one.CreateOn ?? DateTime.Now;
                    one.TransactionID = transactionId;
                    one.UpdateOn = DateTime.Now;
                    one.IsDeleted = 0;
                    one.DataLevel = user?.DataLevel ?? "01";
					tx.Order.AddOrUpdate(one);
					});
				else if (fileType == "OrderDetails") ExcelHelper.ExcelToNewEntityList<OrderDetails>(keypair, filePath, out errorMsg).ForEach(one=>{
                    one.CreateBy = one.CreateBy ?? user?.UILoginName ?? "未登录用户";
                    one.UpdateBy = user?.UILoginName ?? "未登录用户";
                    one.CreateOn = one.CreateOn ?? DateTime.Now;
                    one.TransactionID = transactionId;
                    one.UpdateOn = DateTime.Now;
                    one.IsDeleted = 0;
                    one.DataLevel = user?.DataLevel ?? "01";
					tx.OrderDetails.AddOrUpdate(one);
					});
				else if (fileType == "WarehousingRecord") ExcelHelper.ExcelToNewEntityList<WarehousingRecord>(keypair, filePath, out errorMsg).ForEach(one=>{
                    one.CreateBy = one.CreateBy ?? user?.UILoginName ?? "未登录用户";
                    one.UpdateBy = user?.UILoginName ?? "未登录用户";
                    one.CreateOn = one.CreateOn ?? DateTime.Now;
                    one.TransactionID = transactionId;
                    one.UpdateOn = DateTime.Now;
                    one.IsDeleted = 0;
                    one.DataLevel = user?.DataLevel ?? "01";
					tx.WarehousingRecord.AddOrUpdate(one);
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
				else if (fileType == "ReplenishmentApplicationForm") ExcelHelper.ExcelToNewEntityList<ReplenishmentApplicationForm>(keypair, filePath, out errorMsg).ForEach(one=>{
                    one.CreateBy = one.CreateBy ?? user?.UILoginName ?? "未登录用户";
                    one.UpdateBy = user?.UILoginName ?? "未登录用户";
                    one.CreateOn = one.CreateOn ?? DateTime.Now;
                    one.TransactionID = transactionId;
                    one.UpdateOn = DateTime.Now;
                    one.IsDeleted = 0;
                    one.DataLevel = user?.DataLevel ?? "01";
					tx.ReplenishmentApplicationForm.AddOrUpdate(one);
					});
				else if (fileType == "ReplenishmentRecord") ExcelHelper.ExcelToNewEntityList<ReplenishmentRecord>(keypair, filePath, out errorMsg).ForEach(one=>{
                    one.CreateBy = one.CreateBy ?? user?.UILoginName ?? "未登录用户";
                    one.UpdateBy = user?.UILoginName ?? "未登录用户";
                    one.CreateOn = one.CreateOn ?? DateTime.Now;
                    one.TransactionID = transactionId;
                    one.UpdateOn = DateTime.Now;
                    one.IsDeleted = 0;
                    one.DataLevel = user?.DataLevel ?? "01";
					tx.ReplenishmentRecord.AddOrUpdate(one);
					});
				else if (fileType == "SalesRecord") ExcelHelper.ExcelToNewEntityList<SalesRecord>(keypair, filePath, out errorMsg).ForEach(one=>{
                    one.CreateBy = one.CreateBy ?? user?.UILoginName ?? "未登录用户";
                    one.UpdateBy = user?.UILoginName ?? "未登录用户";
                    one.CreateOn = one.CreateOn ?? DateTime.Now;
                    one.TransactionID = transactionId;
                    one.UpdateOn = DateTime.Now;
                    one.IsDeleted = 0;
                    one.DataLevel = user?.DataLevel ?? "01";
					tx.SalesRecord.AddOrUpdate(one);
					});					tx.SaveChanges();
            }
		}
    }
}
