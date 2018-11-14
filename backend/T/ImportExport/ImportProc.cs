

/* ------------------------------------------------------------ *
 * 此文件由生成器引擎根据既有规则生成，所有手工的更改将会被覆盖
 * 生成时间：11/14/2018 23:55:51
 * 生成版本：11/14/2018 22:32:14 
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
					buf.AppendLine("唯一编号\t客户编号\t姓名\t联系方式\t顺序号\t版本号\t会话编号\t创建人\t创建时间\t更新人\t更新时间\t数据级别\t");
					var type = typeof (Customertype);
					foreach (var entity in tx.Customertype)
					{
						buf.Append(entity.id);
						buf.Append("\t");
						
													
						buf.Append(entity.CCustomerNumber);buf.Append("\t");
														
						buf.Append(entity.CName);buf.Append("\t");
														
						buf.Append(entity.CCommonModeOfContact);buf.Append("\t");
							
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
					buf.AppendLine("唯一编号\t供应商编号\t供应商名称\t联系方式\t办公地点\t顺序号\t版本号\t会话编号\t创建人\t创建时间\t更新人\t更新时间\t数据级别\t");
					var type = typeof (Supplier);
					foreach (var entity in tx.Supplier)
					{
						buf.Append(entity.id);
						buf.Append("\t");
						
													
						buf.Append(entity.SSupplierNumber);buf.Append("\t");
														
						buf.Append(entity.SSupplierName);buf.Append("\t");
														
						buf.Append(entity.SCommonModeOfContact);buf.Append("\t");
														
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
					buf.AppendLine("唯一编号\t货物编号\t货物名称\t顺序号\t版本号\t会话编号\t创建人\t创建时间\t更新人\t更新时间\t数据级别\t");
					var type = typeof (Cargo);
					foreach (var entity in tx.Cargo)
					{
						buf.Append(entity.id);
						buf.Append("\t");
						
													
						buf.Append(entity.CCargoNumber);buf.Append("\t");
														
						buf.Append(entity.CNameOfGoods);buf.Append("\t");
							
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
					buf.AppendLine("唯一编号\t工号\t姓名\t学历\t联系方式\t顺序号\t版本号\t会话编号\t创建人\t创建时间\t更新人\t更新时间\t数据级别\t");
					var type = typeof (Staffname);
					foreach (var entity in tx.Staffname)
					{
						buf.Append(entity.id);
						buf.Append("\t");
						
													
						buf.Append(entity.SJobNumber);buf.Append("\t");
														
						buf.Append(entity.SName);buf.Append("\t");
														
						buf.Append(entity.SEducation);buf.Append("\t");
														
						buf.Append(entity.SCommonModeOfContact);buf.Append("\t");
							
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
					buf.AppendLine("唯一编号\t仓库编号\t容量\t地点\t负责人工号\t顺序号\t版本号\t会话编号\t创建人\t创建时间\t更新人\t更新时间\t数据级别\t");
					var type = typeof (Warehouse);
					foreach (var entity in tx.Warehouse)
					{
						buf.Append(entity.id);
						buf.Append("\t");
						
													
						buf.Append(entity.WWarehouseNumber);buf.Append("\t");
														
						buf.Append(entity.WCapacity);buf.Append("\t");
														
						buf.Append(entity.WLocality);buf.Append("\t");
														
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
					buf.AppendLine("唯一编号\t货架编号\t容量\t地点\t负责人工号\t顺序号\t版本号\t会话编号\t创建人\t创建时间\t更新人\t更新时间\t数据级别\t");
					var type = typeof (GoodsShelves);
					foreach (var entity in tx.GoodsShelves)
					{
						buf.Append(entity.id);
						buf.Append("\t");
						
													
						buf.Append(entity.GSShelfNumber);buf.Append("\t");
														
						buf.Append(entity.GSCapacity);buf.Append("\t");
														
						buf.Append(entity.GSLocality);buf.Append("\t");
														
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
											
				else if (title == "SupplyList")
				{
					buf.AppendLine("唯一编号\t仓库编号\t货物编号\t仓库管理员工号\t日期\t数量\t备注\t顺序号\t版本号\t会话编号\t创建人\t创建时间\t更新人\t更新时间\t数据级别\t");
					var type = typeof (SupplyList);
					foreach (var entity in tx.SupplyList)
					{
						buf.Append(entity.id);
						buf.Append("\t");
						
													
						buf.Append(entity.SLWarehouseNumber);buf.Append("\t");
														
						buf.Append(entity.SLCargoNumber);buf.Append("\t");
														
						buf.Append(entity.SLWarehouseManagementStaffNumber);buf.Append("\t");
														
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
											
				else if (title == "ReplenishmentBill")
				{
					buf.AppendLine("唯一编号\t货架编号\t货物编号\t仓库管理员工号\t日期\t数量\t备注\t顺序号\t版本号\t会话编号\t创建人\t创建时间\t更新人\t更新时间\t数据级别\t");
					var type = typeof (ReplenishmentBill);
					foreach (var entity in tx.ReplenishmentBill)
					{
						buf.Append(entity.id);
						buf.Append("\t");
						
													
						buf.Append(entity.RBShelfNumber);buf.Append("\t");
														
						buf.Append(entity.RBCargoNumber);buf.Append("\t");
														
						buf.Append(entity.RBWarehouseManagementStaffNumber);buf.Append("\t");
														
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
                string cacheFile = "table_mapping_file2018-11-14T223214.txt";
                if (File.Exists(cacheFile))
                    dic = File.ReadAllText(cacheFile).Deserialize<Dictionary<string, Dictionary<string, string>>>();
                else
                {
#region init
					dic = new Dictionary<string, Dictionary<string,string>>();
									
					dic.Add("Customertype", new Dictionary<string,string>{ {"CCustomerNumber","客户编号"},{"CName","姓名"},{"CCommonModeOfContact","联系方式"} });
										
					dic.Add("Supplier", new Dictionary<string,string>{ {"SSupplierNumber","供应商编号"},{"SSupplierName","供应商名称"},{"SCommonModeOfContact","联系方式"},{"SOfficeLocation","办公地点"} });
										
					dic.Add("Cargo", new Dictionary<string,string>{ {"CCargoNumber","货物编号"},{"CNameOfGoods","货物名称"} });
										
					dic.Add("Staffname", new Dictionary<string,string>{ {"SJobNumber","工号"},{"SName","姓名"},{"SEducation","学历"},{"SCommonModeOfContact","联系方式"} });
										
					dic.Add("Warehouse", new Dictionary<string,string>{ {"WWarehouseNumber","仓库编号"},{"WCapacity","容量"},{"WLocality","地点"},{"WResponsibleForManualNumber","负责人工号"} });
										
					dic.Add("GoodsShelves", new Dictionary<string,string>{ {"GSShelfNumber","货架编号"},{"GSCapacity","容量"},{"GSLocality","地点"},{"GSResponsibleForManualNumber","负责人工号"} });
										
					dic.Add("PurchaseUnitPrice", new Dictionary<string,string>{ {"PUPSupplierNumber","供应商编号"},{"PUPCargoNumber","货物编号"},{"PUPPurchasingStaffNumber","采购员工号"},{"PUPDate","日期"},{"PUPAmount","数量"},{"PUPPrice","价格"},{"PUPRemarks","备注"} });
										
					dic.Add("SalesUnitPrice", new Dictionary<string,string>{ {"SUPCargoNumber","货物编号"},{"SUPCustomerNumber","客户编号"},{"SUPSalesStaffNumber","销售员工号"},{"SUPDate","日期"},{"SUPAmount","数量"},{"SUPPrice","价格"},{"SUPRemarks","备注"} });
										
					dic.Add("SupplyList", new Dictionary<string,string>{ {"SLWarehouseNumber","仓库编号"},{"SLCargoNumber","货物编号"},{"SLWarehouseManagementStaffNumber","仓库管理员工号"},{"SLDate","日期"},{"SLAmount","数量"},{"SLRemarks","备注"} });
										
					dic.Add("ReplenishmentBill", new Dictionary<string,string>{ {"RBShelfNumber","货架编号"},{"RBCargoNumber","货物编号"},{"RBWarehouseManagementStaffNumber","仓库管理员工号"},{"RBDate","日期"},{"RBAmount","数量"},{"RBRemarks","备注"} });
										
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
				else if (fileType == "SupplyList") ExcelHelper.ExcelToNewEntityList<SupplyList>(keypair, filePath, out errorMsg).ForEach(one=>{
                    one.CreateBy = one.CreateBy ?? user?.UILoginName ?? "未登录用户";
                    one.UpdateBy = user?.UILoginName ?? "未登录用户";
                    one.CreateOn = one.CreateOn ?? DateTime.Now;
                    one.TransactionID = transactionId;
                    one.UpdateOn = DateTime.Now;
                    one.IsDeleted = 0;
                    one.DataLevel = user?.DataLevel ?? "01";
					tx.SupplyList.AddOrUpdate(one);
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
