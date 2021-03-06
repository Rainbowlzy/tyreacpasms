﻿  


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
using TENtities.EF;

namespace TEntities.EF
{
    public class DefaultContext : DbContext
    {
	
#if DEBUG
        public DefaultContext():base("LocalDefaultContext") 
		{ 
			// BuildMenu();
		}
#elif RELEASE
#endif

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
			
			modelBuilder.Entity<Customertype>().Property(p => p.id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 
			modelBuilder.Entity<Supplier>().Property(p => p.id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 
			modelBuilder.Entity<Cargo>().Property(p => p.id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 
			modelBuilder.Entity<Staffname>().Property(p => p.id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 
			modelBuilder.Entity<Warehouse>().Property(p => p.id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 
			modelBuilder.Entity<GoodsShelves>().Property(p => p.id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 
			modelBuilder.Entity<PurchaseUnitPrice>().Property(p => p.id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 
			modelBuilder.Entity<SalesUnitPrice>().Property(p => p.id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 
			modelBuilder.Entity<SupplyWarehousingList>().Property(p => p.id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 
			modelBuilder.Entity<ReplenishmentBill>().Property(p => p.id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 
			modelBuilder.Entity<WarehouseStock>().Property(p => p.id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 
			modelBuilder.Entity<ShelfStock>().Property(p => p.id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 
			modelBuilder.Entity<ShelfList>().Property(p => p.id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 
			modelBuilder.Entity<PurchaseVoucher>().Property(p => p.id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 
			modelBuilder.Entity<OutboundOrder>().Property(p => p.id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 
			modelBuilder.Entity<LowerFrame>().Property(p => p.id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 
			modelBuilder.Entity<SalesVoucher>().Property(p => p.id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 
			modelBuilder.Entity<MenuConfiguration>().Property(p => p.id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 
			modelBuilder.Entity<RoleMenu>().Property(p => p.id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 
			modelBuilder.Entity<UserRole>().Property(p => p.id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 
			modelBuilder.Entity<RoleConfiguration>().Property(p => p.id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 
			modelBuilder.Entity<UserInformation>().Property(p => p.id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 
			modelBuilder.Entity<LogonRecord>().Property(p => p.id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 
			modelBuilder.Entity<UserMenu>().Property(p => p.id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 
			modelBuilder.Entity<SystemConfiguration>().Property(p => p.id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 
			            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DefaultContext, Configuration>());
        }

		public void BuildMenu()
		{
            var TransactionID = Guid.NewGuid().ToString();
            var CreateBy = "Initialization Job";
            var UICode = "123";
			
			if(!UserInformation.Any())
		    UserInformation.AddRange(Enumerable.Range(1000, 1050).Select(i => new UserInformation
            {
                UIJobNumber = i,
                UILoginName = i.ToString(),
                UICode = i.ToString(),
		        CreateBy = CreateBy,
		        TransactionID = TransactionID,
		        IsDeleted = 0,
		        DataLevel = "01",
		        CreateOn = DateTime.Now,
		        UpdateOn = DateTime.Now,
		        UpdateBy = CreateBy
            }));
			MenuConfiguration.RemoveRange(MenuConfiguration);
			RoleMenu.RemoveRange(RoleMenu);
			UserRole.RemoveRange(UserRole);
            SaveChanges();
			UserRole.Add(new UserRole
			{
				URLoginName = "1200",
				URRoleName = "超级管理员",
				CreateBy = CreateBy,
				TransactionID = TransactionID,
				IsDeleted = 0,
				DataLevel = "01",
				CreateOn = DateTime.Now,
				UpdateOn = DateTime.Now,
				UpdateBy = CreateBy
			});
			UserRole.Add(new UserRole
			{
				URLoginName = "1000",
				URRoleName = "销售员",
				CreateBy = CreateBy,
				TransactionID = TransactionID,
				IsDeleted = 0,
				DataLevel = "01",
				CreateOn = DateTime.Now,
				UpdateOn = DateTime.Now,
				UpdateBy = CreateBy
			});
			UserRole.Add(new UserRole
			{
				URLoginName = "1001",
				URRoleName = "采购员",
				CreateBy = CreateBy,
				TransactionID = TransactionID,
				IsDeleted = 0,
				DataLevel = "01",
				CreateOn = DateTime.Now,
				UpdateOn = DateTime.Now,
				UpdateBy = CreateBy
			});
			UserRole.Add(new UserRole
			{
				URLoginName = "1002",
				URRoleName = "仓库管理员",
				CreateBy = CreateBy,
				TransactionID = TransactionID,
				IsDeleted = 0,
				DataLevel = "01",
				CreateOn = DateTime.Now,
				UpdateOn = DateTime.Now,
				UpdateBy = CreateBy
			});

            if (!MenuConfiguration.Any(t => t.MCCaption == "客户"))
			{
				RoleMenu.Add(new RoleMenu
				{
					RMRoleName = "超级管理员",
					RMMenuTitle = "客户",
					CreateBy = CreateBy,
					TransactionID = TransactionID,
					IsDeleted = 0,
					DataLevel = "01",
					CreateOn = DateTime.Now,
					UpdateOn = DateTime.Now,
					UpdateBy = CreateBy
				});
				MenuConfiguration.Add(new MenuConfiguration
				{
					MCDisplayName = "客户",
					MCCaption = "客户",
					MCLink = "/pagelist/Customertype",
					MCParentTitle = "后台首页",
					CreateBy = CreateBy,
					TransactionID = TransactionID,
					IsDeleted = 0,
					DataLevel = "01",
                    CreateOn = DateTime.Now,
                    UpdateOn = DateTime.Now,
                    UpdateBy = CreateBy
				});
			}
			var customertype = MenuConfiguration.FirstOrDefault(t => t.MCCaption == "客户");
			if(customertype!=null)
			{
				customertype.MCLink = "/pagelist/CustomertypeList.html";
				MenuConfiguration.AddOrUpdate(customertype);
			}

			if (!MenuConfiguration.Any(t => t.MCCaption == "供应商"))
			{
				RoleMenu.Add(new RoleMenu
				{
					RMRoleName = "超级管理员",
					RMMenuTitle = "供应商",
					CreateBy = CreateBy,
					TransactionID = TransactionID,
					IsDeleted = 0,
					DataLevel = "01",
					CreateOn = DateTime.Now,
					UpdateOn = DateTime.Now,
					UpdateBy = CreateBy
				});
				MenuConfiguration.Add(new MenuConfiguration
				{
					MCDisplayName = "供应商",
					MCCaption = "供应商",
					MCLink = "/pagelist/Supplier",
					MCParentTitle = "后台首页",
					CreateBy = CreateBy,
					TransactionID = TransactionID,
					IsDeleted = 0,
					DataLevel = "01",
                    CreateOn = DateTime.Now,
                    UpdateOn = DateTime.Now,
                    UpdateBy = CreateBy
				});
			}
			var supplier = MenuConfiguration.FirstOrDefault(t => t.MCCaption == "供应商");
			if(supplier!=null)
			{
				supplier.MCLink = "/pagelist/SupplierList.html";
				MenuConfiguration.AddOrUpdate(supplier);
			}

			if (!MenuConfiguration.Any(t => t.MCCaption == "货物"))
			{
				RoleMenu.Add(new RoleMenu
				{
					RMRoleName = "超级管理员",
					RMMenuTitle = "货物",
					CreateBy = CreateBy,
					TransactionID = TransactionID,
					IsDeleted = 0,
					DataLevel = "01",
					CreateOn = DateTime.Now,
					UpdateOn = DateTime.Now,
					UpdateBy = CreateBy
				});
				MenuConfiguration.Add(new MenuConfiguration
				{
					MCDisplayName = "货物",
					MCCaption = "货物",
					MCLink = "/pagelist/Cargo",
					MCParentTitle = "后台首页",
					CreateBy = CreateBy,
					TransactionID = TransactionID,
					IsDeleted = 0,
					DataLevel = "01",
                    CreateOn = DateTime.Now,
                    UpdateOn = DateTime.Now,
                    UpdateBy = CreateBy
				});
			}
			var cargo = MenuConfiguration.FirstOrDefault(t => t.MCCaption == "货物");
			if(cargo!=null)
			{
				cargo.MCLink = "/pagelist/CargoList.html";
				MenuConfiguration.AddOrUpdate(cargo);
			}

			if (!MenuConfiguration.Any(t => t.MCCaption == "员工"))
			{
				RoleMenu.Add(new RoleMenu
				{
					RMRoleName = "超级管理员",
					RMMenuTitle = "员工",
					CreateBy = CreateBy,
					TransactionID = TransactionID,
					IsDeleted = 0,
					DataLevel = "01",
					CreateOn = DateTime.Now,
					UpdateOn = DateTime.Now,
					UpdateBy = CreateBy
				});
				MenuConfiguration.Add(new MenuConfiguration
				{
					MCDisplayName = "员工",
					MCCaption = "员工",
					MCLink = "/pagelist/Staffname",
					MCParentTitle = "后台首页",
					CreateBy = CreateBy,
					TransactionID = TransactionID,
					IsDeleted = 0,
					DataLevel = "01",
                    CreateOn = DateTime.Now,
                    UpdateOn = DateTime.Now,
                    UpdateBy = CreateBy
				});
			}
			var staffname = MenuConfiguration.FirstOrDefault(t => t.MCCaption == "员工");
			if(staffname!=null)
			{
				staffname.MCLink = "/pagelist/StaffnameList.html";
				MenuConfiguration.AddOrUpdate(staffname);
			}

			if (!MenuConfiguration.Any(t => t.MCCaption == "仓库"))
			{
				RoleMenu.Add(new RoleMenu
				{
					RMRoleName = "超级管理员",
					RMMenuTitle = "仓库",
					CreateBy = CreateBy,
					TransactionID = TransactionID,
					IsDeleted = 0,
					DataLevel = "01",
					CreateOn = DateTime.Now,
					UpdateOn = DateTime.Now,
					UpdateBy = CreateBy
				});
				MenuConfiguration.Add(new MenuConfiguration
				{
					MCDisplayName = "仓库",
					MCCaption = "仓库",
					MCLink = "/pagelist/Warehouse",
					MCParentTitle = "后台首页",
					CreateBy = CreateBy,
					TransactionID = TransactionID,
					IsDeleted = 0,
					DataLevel = "01",
                    CreateOn = DateTime.Now,
                    UpdateOn = DateTime.Now,
                    UpdateBy = CreateBy
				});
			}
			var warehouse = MenuConfiguration.FirstOrDefault(t => t.MCCaption == "仓库");
			if(warehouse!=null)
			{
				warehouse.MCLink = "/pagelist/WarehouseList.html";
				MenuConfiguration.AddOrUpdate(warehouse);
			}

			if (!MenuConfiguration.Any(t => t.MCCaption == "货架"))
			{
				RoleMenu.Add(new RoleMenu
				{
					RMRoleName = "超级管理员",
					RMMenuTitle = "货架",
					CreateBy = CreateBy,
					TransactionID = TransactionID,
					IsDeleted = 0,
					DataLevel = "01",
					CreateOn = DateTime.Now,
					UpdateOn = DateTime.Now,
					UpdateBy = CreateBy
				});
				MenuConfiguration.Add(new MenuConfiguration
				{
					MCDisplayName = "货架",
					MCCaption = "货架",
					MCLink = "/pagelist/GoodsShelves",
					MCParentTitle = "后台首页",
					CreateBy = CreateBy,
					TransactionID = TransactionID,
					IsDeleted = 0,
					DataLevel = "01",
                    CreateOn = DateTime.Now,
                    UpdateOn = DateTime.Now,
                    UpdateBy = CreateBy
				});
			}
			var goodsshelves = MenuConfiguration.FirstOrDefault(t => t.MCCaption == "货架");
			if(goodsshelves!=null)
			{
				goodsshelves.MCLink = "/pagelist/GoodsShelvesList.html";
				MenuConfiguration.AddOrUpdate(goodsshelves);
			}

			if (!MenuConfiguration.Any(t => t.MCCaption == "采购单"))
			{
				RoleMenu.Add(new RoleMenu
				{
					RMRoleName = "超级管理员",
					RMMenuTitle = "采购单",
					CreateBy = CreateBy,
					TransactionID = TransactionID,
					IsDeleted = 0,
					DataLevel = "01",
					CreateOn = DateTime.Now,
					UpdateOn = DateTime.Now,
					UpdateBy = CreateBy
				});
				MenuConfiguration.Add(new MenuConfiguration
				{
					MCDisplayName = "采购单",
					MCCaption = "采购单",
					MCLink = "/pagelist/PurchaseUnitPrice",
					MCParentTitle = "后台首页",
					CreateBy = CreateBy,
					TransactionID = TransactionID,
					IsDeleted = 0,
					DataLevel = "01",
                    CreateOn = DateTime.Now,
                    UpdateOn = DateTime.Now,
                    UpdateBy = CreateBy
				});
			}
			var purchaseunitprice = MenuConfiguration.FirstOrDefault(t => t.MCCaption == "采购单");
			if(purchaseunitprice!=null)
			{
				purchaseunitprice.MCLink = "/pagelist/PurchaseUnitPriceList.html";
				MenuConfiguration.AddOrUpdate(purchaseunitprice);
			}

			if (!MenuConfiguration.Any(t => t.MCCaption == "销售单"))
			{
				RoleMenu.Add(new RoleMenu
				{
					RMRoleName = "超级管理员",
					RMMenuTitle = "销售单",
					CreateBy = CreateBy,
					TransactionID = TransactionID,
					IsDeleted = 0,
					DataLevel = "01",
					CreateOn = DateTime.Now,
					UpdateOn = DateTime.Now,
					UpdateBy = CreateBy
				});
				MenuConfiguration.Add(new MenuConfiguration
				{
					MCDisplayName = "销售单",
					MCCaption = "销售单",
					MCLink = "/pagelist/SalesUnitPrice",
					MCParentTitle = "后台首页",
					CreateBy = CreateBy,
					TransactionID = TransactionID,
					IsDeleted = 0,
					DataLevel = "01",
                    CreateOn = DateTime.Now,
                    UpdateOn = DateTime.Now,
                    UpdateBy = CreateBy
				});
			}
			var salesunitprice = MenuConfiguration.FirstOrDefault(t => t.MCCaption == "销售单");
			if(salesunitprice!=null)
			{
				salesunitprice.MCLink = "/pagelist/SalesUnitPriceList.html";
				MenuConfiguration.AddOrUpdate(salesunitprice);
			}

			if (!MenuConfiguration.Any(t => t.MCCaption == "供应入库单"))
			{
				RoleMenu.Add(new RoleMenu
				{
					RMRoleName = "超级管理员",
					RMMenuTitle = "供应入库单",
					CreateBy = CreateBy,
					TransactionID = TransactionID,
					IsDeleted = 0,
					DataLevel = "01",
					CreateOn = DateTime.Now,
					UpdateOn = DateTime.Now,
					UpdateBy = CreateBy
				});
				MenuConfiguration.Add(new MenuConfiguration
				{
					MCDisplayName = "供应入库单",
					MCCaption = "供应入库单",
					MCLink = "/pagelist/SupplyWarehousingList",
					MCParentTitle = "后台首页",
					CreateBy = CreateBy,
					TransactionID = TransactionID,
					IsDeleted = 0,
					DataLevel = "01",
                    CreateOn = DateTime.Now,
                    UpdateOn = DateTime.Now,
                    UpdateBy = CreateBy
				});
			}
			var supplywarehousinglist = MenuConfiguration.FirstOrDefault(t => t.MCCaption == "供应入库单");
			if(supplywarehousinglist!=null)
			{
				supplywarehousinglist.MCLink = "/pagelist/SupplyWarehousingListList.html";
				MenuConfiguration.AddOrUpdate(supplywarehousinglist);
			}

			if (!MenuConfiguration.Any(t => t.MCCaption == "补货单"))
			{
				RoleMenu.Add(new RoleMenu
				{
					RMRoleName = "超级管理员",
					RMMenuTitle = "补货单",
					CreateBy = CreateBy,
					TransactionID = TransactionID,
					IsDeleted = 0,
					DataLevel = "01",
					CreateOn = DateTime.Now,
					UpdateOn = DateTime.Now,
					UpdateBy = CreateBy
				});
				MenuConfiguration.Add(new MenuConfiguration
				{
					MCDisplayName = "补货单",
					MCCaption = "补货单",
					MCLink = "/pagelist/ReplenishmentBill",
					MCParentTitle = "后台首页",
					CreateBy = CreateBy,
					TransactionID = TransactionID,
					IsDeleted = 0,
					DataLevel = "01",
                    CreateOn = DateTime.Now,
                    UpdateOn = DateTime.Now,
                    UpdateBy = CreateBy
				});
			}
			var replenishmentbill = MenuConfiguration.FirstOrDefault(t => t.MCCaption == "补货单");
			if(replenishmentbill!=null)
			{
				replenishmentbill.MCLink = "/pagelist/ReplenishmentBillList.html";
				MenuConfiguration.AddOrUpdate(replenishmentbill);
			}

			if (!MenuConfiguration.Any(t => t.MCCaption == "仓库库存"))
			{
				RoleMenu.Add(new RoleMenu
				{
					RMRoleName = "超级管理员",
					RMMenuTitle = "仓库库存",
					CreateBy = CreateBy,
					TransactionID = TransactionID,
					IsDeleted = 0,
					DataLevel = "01",
					CreateOn = DateTime.Now,
					UpdateOn = DateTime.Now,
					UpdateBy = CreateBy
				});
				MenuConfiguration.Add(new MenuConfiguration
				{
					MCDisplayName = "仓库库存",
					MCCaption = "仓库库存",
					MCLink = "/pagelist/WarehouseStock",
					MCParentTitle = "后台首页",
					CreateBy = CreateBy,
					TransactionID = TransactionID,
					IsDeleted = 0,
					DataLevel = "01",
                    CreateOn = DateTime.Now,
                    UpdateOn = DateTime.Now,
                    UpdateBy = CreateBy
				});
			}
			var warehousestock = MenuConfiguration.FirstOrDefault(t => t.MCCaption == "仓库库存");
			if(warehousestock!=null)
			{
				warehousestock.MCLink = "/pagelist/WarehouseStockList.html";
				MenuConfiguration.AddOrUpdate(warehousestock);
			}

			if (!MenuConfiguration.Any(t => t.MCCaption == "货架库存"))
			{
				RoleMenu.Add(new RoleMenu
				{
					RMRoleName = "超级管理员",
					RMMenuTitle = "货架库存",
					CreateBy = CreateBy,
					TransactionID = TransactionID,
					IsDeleted = 0,
					DataLevel = "01",
					CreateOn = DateTime.Now,
					UpdateOn = DateTime.Now,
					UpdateBy = CreateBy
				});
				MenuConfiguration.Add(new MenuConfiguration
				{
					MCDisplayName = "货架库存",
					MCCaption = "货架库存",
					MCLink = "/pagelist/ShelfStock",
					MCParentTitle = "后台首页",
					CreateBy = CreateBy,
					TransactionID = TransactionID,
					IsDeleted = 0,
					DataLevel = "01",
                    CreateOn = DateTime.Now,
                    UpdateOn = DateTime.Now,
                    UpdateBy = CreateBy
				});
			}
			var shelfstock = MenuConfiguration.FirstOrDefault(t => t.MCCaption == "货架库存");
			if(shelfstock!=null)
			{
				shelfstock.MCLink = "/pagelist/ShelfStockList.html";
				MenuConfiguration.AddOrUpdate(shelfstock);
			}

			if (!MenuConfiguration.Any(t => t.MCCaption == "上架单"))
			{
				RoleMenu.Add(new RoleMenu
				{
					RMRoleName = "超级管理员",
					RMMenuTitle = "上架单",
					CreateBy = CreateBy,
					TransactionID = TransactionID,
					IsDeleted = 0,
					DataLevel = "01",
					CreateOn = DateTime.Now,
					UpdateOn = DateTime.Now,
					UpdateBy = CreateBy
				});
				MenuConfiguration.Add(new MenuConfiguration
				{
					MCDisplayName = "上架单",
					MCCaption = "上架单",
					MCLink = "/pagelist/ShelfList",
					MCParentTitle = "后台首页",
					CreateBy = CreateBy,
					TransactionID = TransactionID,
					IsDeleted = 0,
					DataLevel = "01",
                    CreateOn = DateTime.Now,
                    UpdateOn = DateTime.Now,
                    UpdateBy = CreateBy
				});
			}
			var shelflist = MenuConfiguration.FirstOrDefault(t => t.MCCaption == "上架单");
			if(shelflist!=null)
			{
				shelflist.MCLink = "/pagelist/ShelfListList.html";
				MenuConfiguration.AddOrUpdate(shelflist);
			}

			if (!MenuConfiguration.Any(t => t.MCCaption == "采购凭证单"))
			{
				RoleMenu.Add(new RoleMenu
				{
					RMRoleName = "超级管理员",
					RMMenuTitle = "采购凭证单",
					CreateBy = CreateBy,
					TransactionID = TransactionID,
					IsDeleted = 0,
					DataLevel = "01",
					CreateOn = DateTime.Now,
					UpdateOn = DateTime.Now,
					UpdateBy = CreateBy
				});
				MenuConfiguration.Add(new MenuConfiguration
				{
					MCDisplayName = "采购凭证单",
					MCCaption = "采购凭证单",
					MCLink = "/pagelist/PurchaseVoucher",
					MCParentTitle = "后台首页",
					CreateBy = CreateBy,
					TransactionID = TransactionID,
					IsDeleted = 0,
					DataLevel = "01",
                    CreateOn = DateTime.Now,
                    UpdateOn = DateTime.Now,
                    UpdateBy = CreateBy
				});
			}
			var purchasevoucher = MenuConfiguration.FirstOrDefault(t => t.MCCaption == "采购凭证单");
			if(purchasevoucher!=null)
			{
				purchasevoucher.MCLink = "/pagelist/PurchaseVoucherList.html";
				MenuConfiguration.AddOrUpdate(purchasevoucher);
			}

			if (!MenuConfiguration.Any(t => t.MCCaption == "出库单"))
			{
				RoleMenu.Add(new RoleMenu
				{
					RMRoleName = "超级管理员",
					RMMenuTitle = "出库单",
					CreateBy = CreateBy,
					TransactionID = TransactionID,
					IsDeleted = 0,
					DataLevel = "01",
					CreateOn = DateTime.Now,
					UpdateOn = DateTime.Now,
					UpdateBy = CreateBy
				});
				MenuConfiguration.Add(new MenuConfiguration
				{
					MCDisplayName = "出库单",
					MCCaption = "出库单",
					MCLink = "/pagelist/OutboundOrder",
					MCParentTitle = "后台首页",
					CreateBy = CreateBy,
					TransactionID = TransactionID,
					IsDeleted = 0,
					DataLevel = "01",
                    CreateOn = DateTime.Now,
                    UpdateOn = DateTime.Now,
                    UpdateBy = CreateBy
				});
			}
			var outboundorder = MenuConfiguration.FirstOrDefault(t => t.MCCaption == "出库单");
			if(outboundorder!=null)
			{
				outboundorder.MCLink = "/pagelist/OutboundOrderList.html";
				MenuConfiguration.AddOrUpdate(outboundorder);
			}

			if (!MenuConfiguration.Any(t => t.MCCaption == "下架单"))
			{
				RoleMenu.Add(new RoleMenu
				{
					RMRoleName = "超级管理员",
					RMMenuTitle = "下架单",
					CreateBy = CreateBy,
					TransactionID = TransactionID,
					IsDeleted = 0,
					DataLevel = "01",
					CreateOn = DateTime.Now,
					UpdateOn = DateTime.Now,
					UpdateBy = CreateBy
				});
				MenuConfiguration.Add(new MenuConfiguration
				{
					MCDisplayName = "下架单",
					MCCaption = "下架单",
					MCLink = "/pagelist/LowerFrame",
					MCParentTitle = "后台首页",
					CreateBy = CreateBy,
					TransactionID = TransactionID,
					IsDeleted = 0,
					DataLevel = "01",
                    CreateOn = DateTime.Now,
                    UpdateOn = DateTime.Now,
                    UpdateBy = CreateBy
				});
			}
			var lowerframe = MenuConfiguration.FirstOrDefault(t => t.MCCaption == "下架单");
			if(lowerframe!=null)
			{
				lowerframe.MCLink = "/pagelist/LowerFrameList.html";
				MenuConfiguration.AddOrUpdate(lowerframe);
			}

			if (!MenuConfiguration.Any(t => t.MCCaption == "销售凭证单"))
			{
				RoleMenu.Add(new RoleMenu
				{
					RMRoleName = "超级管理员",
					RMMenuTitle = "销售凭证单",
					CreateBy = CreateBy,
					TransactionID = TransactionID,
					IsDeleted = 0,
					DataLevel = "01",
					CreateOn = DateTime.Now,
					UpdateOn = DateTime.Now,
					UpdateBy = CreateBy
				});
				MenuConfiguration.Add(new MenuConfiguration
				{
					MCDisplayName = "销售凭证单",
					MCCaption = "销售凭证单",
					MCLink = "/pagelist/SalesVoucher",
					MCParentTitle = "后台首页",
					CreateBy = CreateBy,
					TransactionID = TransactionID,
					IsDeleted = 0,
					DataLevel = "01",
                    CreateOn = DateTime.Now,
                    UpdateOn = DateTime.Now,
                    UpdateBy = CreateBy
				});
			}
			var salesvoucher = MenuConfiguration.FirstOrDefault(t => t.MCCaption == "销售凭证单");
			if(salesvoucher!=null)
			{
				salesvoucher.MCLink = "/pagelist/SalesVoucherList.html";
				MenuConfiguration.AddOrUpdate(salesvoucher);
			}

			if (!MenuConfiguration.Any(t => t.MCCaption == "菜单配置"))
			{
				RoleMenu.Add(new RoleMenu
				{
					RMRoleName = "超级管理员",
					RMMenuTitle = "菜单配置",
					CreateBy = CreateBy,
					TransactionID = TransactionID,
					IsDeleted = 0,
					DataLevel = "01",
					CreateOn = DateTime.Now,
					UpdateOn = DateTime.Now,
					UpdateBy = CreateBy
				});
				MenuConfiguration.Add(new MenuConfiguration
				{
					MCDisplayName = "菜单配置",
					MCCaption = "菜单配置",
					MCLink = "/pagelist/MenuConfiguration",
					MCParentTitle = "后台首页",
					CreateBy = CreateBy,
					TransactionID = TransactionID,
					IsDeleted = 0,
					DataLevel = "01",
                    CreateOn = DateTime.Now,
                    UpdateOn = DateTime.Now,
                    UpdateBy = CreateBy
				});
			}
			var menuconfiguration = MenuConfiguration.FirstOrDefault(t => t.MCCaption == "菜单配置");
			if(menuconfiguration!=null)
			{
				menuconfiguration.MCLink = "/pagelist/MenuConfigurationList.html";
				MenuConfiguration.AddOrUpdate(menuconfiguration);
			}

			if (!MenuConfiguration.Any(t => t.MCCaption == "角色菜单"))
			{
				RoleMenu.Add(new RoleMenu
				{
					RMRoleName = "超级管理员",
					RMMenuTitle = "角色菜单",
					CreateBy = CreateBy,
					TransactionID = TransactionID,
					IsDeleted = 0,
					DataLevel = "01",
					CreateOn = DateTime.Now,
					UpdateOn = DateTime.Now,
					UpdateBy = CreateBy
				});
				MenuConfiguration.Add(new MenuConfiguration
				{
					MCDisplayName = "角色菜单",
					MCCaption = "角色菜单",
					MCLink = "/pagelist/RoleMenu",
					MCParentTitle = "后台首页",
					CreateBy = CreateBy,
					TransactionID = TransactionID,
					IsDeleted = 0,
					DataLevel = "01",
                    CreateOn = DateTime.Now,
                    UpdateOn = DateTime.Now,
                    UpdateBy = CreateBy
				});
			}
			var rolemenu = MenuConfiguration.FirstOrDefault(t => t.MCCaption == "角色菜单");
			if(rolemenu!=null)
			{
				rolemenu.MCLink = "/pagelist/RoleMenuList.html";
				MenuConfiguration.AddOrUpdate(rolemenu);
			}

			if (!MenuConfiguration.Any(t => t.MCCaption == "用户角色"))
			{
				RoleMenu.Add(new RoleMenu
				{
					RMRoleName = "超级管理员",
					RMMenuTitle = "用户角色",
					CreateBy = CreateBy,
					TransactionID = TransactionID,
					IsDeleted = 0,
					DataLevel = "01",
					CreateOn = DateTime.Now,
					UpdateOn = DateTime.Now,
					UpdateBy = CreateBy
				});
				MenuConfiguration.Add(new MenuConfiguration
				{
					MCDisplayName = "用户角色",
					MCCaption = "用户角色",
					MCLink = "/pagelist/UserRole",
					MCParentTitle = "后台首页",
					CreateBy = CreateBy,
					TransactionID = TransactionID,
					IsDeleted = 0,
					DataLevel = "01",
                    CreateOn = DateTime.Now,
                    UpdateOn = DateTime.Now,
                    UpdateBy = CreateBy
				});
			}
			var userrole = MenuConfiguration.FirstOrDefault(t => t.MCCaption == "用户角色");
			if(userrole!=null)
			{
				userrole.MCLink = "/pagelist/UserRoleList.html";
				MenuConfiguration.AddOrUpdate(userrole);
			}

			if (!MenuConfiguration.Any(t => t.MCCaption == "角色配置"))
			{
				RoleMenu.Add(new RoleMenu
				{
					RMRoleName = "超级管理员",
					RMMenuTitle = "角色配置",
					CreateBy = CreateBy,
					TransactionID = TransactionID,
					IsDeleted = 0,
					DataLevel = "01",
					CreateOn = DateTime.Now,
					UpdateOn = DateTime.Now,
					UpdateBy = CreateBy
				});
				MenuConfiguration.Add(new MenuConfiguration
				{
					MCDisplayName = "角色配置",
					MCCaption = "角色配置",
					MCLink = "/pagelist/RoleConfiguration",
					MCParentTitle = "后台首页",
					CreateBy = CreateBy,
					TransactionID = TransactionID,
					IsDeleted = 0,
					DataLevel = "01",
                    CreateOn = DateTime.Now,
                    UpdateOn = DateTime.Now,
                    UpdateBy = CreateBy
				});
			}
			var roleconfiguration = MenuConfiguration.FirstOrDefault(t => t.MCCaption == "角色配置");
			if(roleconfiguration!=null)
			{
				roleconfiguration.MCLink = "/pagelist/RoleConfigurationList.html";
				MenuConfiguration.AddOrUpdate(roleconfiguration);
			}

			if (!MenuConfiguration.Any(t => t.MCCaption == "用户信息"))
			{
				RoleMenu.Add(new RoleMenu
				{
					RMRoleName = "超级管理员",
					RMMenuTitle = "用户信息",
					CreateBy = CreateBy,
					TransactionID = TransactionID,
					IsDeleted = 0,
					DataLevel = "01",
					CreateOn = DateTime.Now,
					UpdateOn = DateTime.Now,
					UpdateBy = CreateBy
				});
				MenuConfiguration.Add(new MenuConfiguration
				{
					MCDisplayName = "用户信息",
					MCCaption = "用户信息",
					MCLink = "/pagelist/UserInformation",
					MCParentTitle = "后台首页",
					CreateBy = CreateBy,
					TransactionID = TransactionID,
					IsDeleted = 0,
					DataLevel = "01",
                    CreateOn = DateTime.Now,
                    UpdateOn = DateTime.Now,
                    UpdateBy = CreateBy
				});
			}
			var userinformation = MenuConfiguration.FirstOrDefault(t => t.MCCaption == "用户信息");
			if(userinformation!=null)
			{
				userinformation.MCLink = "/pagelist/UserInformationList.html";
				MenuConfiguration.AddOrUpdate(userinformation);
			}

			if (!MenuConfiguration.Any(t => t.MCCaption == "登录记录"))
			{
				RoleMenu.Add(new RoleMenu
				{
					RMRoleName = "超级管理员",
					RMMenuTitle = "登录记录",
					CreateBy = CreateBy,
					TransactionID = TransactionID,
					IsDeleted = 0,
					DataLevel = "01",
					CreateOn = DateTime.Now,
					UpdateOn = DateTime.Now,
					UpdateBy = CreateBy
				});
				MenuConfiguration.Add(new MenuConfiguration
				{
					MCDisplayName = "登录记录",
					MCCaption = "登录记录",
					MCLink = "/pagelist/LogonRecord",
					MCParentTitle = "后台首页",
					CreateBy = CreateBy,
					TransactionID = TransactionID,
					IsDeleted = 0,
					DataLevel = "01",
                    CreateOn = DateTime.Now,
                    UpdateOn = DateTime.Now,
                    UpdateBy = CreateBy
				});
			}
			var logonrecord = MenuConfiguration.FirstOrDefault(t => t.MCCaption == "登录记录");
			if(logonrecord!=null)
			{
				logonrecord.MCLink = "/pagelist/LogonRecordList.html";
				MenuConfiguration.AddOrUpdate(logonrecord);
			}

			if (!MenuConfiguration.Any(t => t.MCCaption == "用户菜单"))
			{
				RoleMenu.Add(new RoleMenu
				{
					RMRoleName = "超级管理员",
					RMMenuTitle = "用户菜单",
					CreateBy = CreateBy,
					TransactionID = TransactionID,
					IsDeleted = 0,
					DataLevel = "01",
					CreateOn = DateTime.Now,
					UpdateOn = DateTime.Now,
					UpdateBy = CreateBy
				});
				MenuConfiguration.Add(new MenuConfiguration
				{
					MCDisplayName = "用户菜单",
					MCCaption = "用户菜单",
					MCLink = "/pagelist/UserMenu",
					MCParentTitle = "后台首页",
					CreateBy = CreateBy,
					TransactionID = TransactionID,
					IsDeleted = 0,
					DataLevel = "01",
                    CreateOn = DateTime.Now,
                    UpdateOn = DateTime.Now,
                    UpdateBy = CreateBy
				});
			}
			var usermenu = MenuConfiguration.FirstOrDefault(t => t.MCCaption == "用户菜单");
			if(usermenu!=null)
			{
				usermenu.MCLink = "/pagelist/UserMenuList.html";
				MenuConfiguration.AddOrUpdate(usermenu);
			}

			if (!MenuConfiguration.Any(t => t.MCCaption == "系统配置"))
			{
				RoleMenu.Add(new RoleMenu
				{
					RMRoleName = "超级管理员",
					RMMenuTitle = "系统配置",
					CreateBy = CreateBy,
					TransactionID = TransactionID,
					IsDeleted = 0,
					DataLevel = "01",
					CreateOn = DateTime.Now,
					UpdateOn = DateTime.Now,
					UpdateBy = CreateBy
				});
				MenuConfiguration.Add(new MenuConfiguration
				{
					MCDisplayName = "系统配置",
					MCCaption = "系统配置",
					MCLink = "/pagelist/SystemConfiguration",
					MCParentTitle = "后台首页",
					CreateBy = CreateBy,
					TransactionID = TransactionID,
					IsDeleted = 0,
					DataLevel = "01",
                    CreateOn = DateTime.Now,
                    UpdateOn = DateTime.Now,
                    UpdateBy = CreateBy
				});
			}
			var systemconfiguration = MenuConfiguration.FirstOrDefault(t => t.MCCaption == "系统配置");
			if(systemconfiguration!=null)
			{
				systemconfiguration.MCLink = "/pagelist/SystemConfigurationList.html";
				MenuConfiguration.AddOrUpdate(systemconfiguration);
			}
            

		    SaveChanges();
		}

        /// <summary>
        ///  客户 
        /// </summary>
        public virtual DbSet<Customertype> Customertype { get; set; }

        /// <summary>
        ///  供应商 
        /// </summary>
        public virtual DbSet<Supplier> Supplier { get; set; }

        /// <summary>
        ///  货物 
        /// </summary>
        public virtual DbSet<Cargo> Cargo { get; set; }

        /// <summary>
        ///  员工 
        /// </summary>
        public virtual DbSet<Staffname> Staffname { get; set; }

        /// <summary>
        ///  仓库 
        /// </summary>
        public virtual DbSet<Warehouse> Warehouse { get; set; }

        /// <summary>
        ///  货架 
        /// </summary>
        public virtual DbSet<GoodsShelves> GoodsShelves { get; set; }

        /// <summary>
        ///  采购单 
        /// </summary>
        public virtual DbSet<PurchaseUnitPrice> PurchaseUnitPrice { get; set; }

        /// <summary>
        ///  销售单 
        /// </summary>
        public virtual DbSet<SalesUnitPrice> SalesUnitPrice { get; set; }

        /// <summary>
        ///  供应入库单 
        /// </summary>
        public virtual DbSet<SupplyWarehousingList> SupplyWarehousingList { get; set; }

        /// <summary>
        ///  补货单 
        /// </summary>
        public virtual DbSet<ReplenishmentBill> ReplenishmentBill { get; set; }

        /// <summary>
        ///  仓库库存 
        /// </summary>
        public virtual DbSet<WarehouseStock> WarehouseStock { get; set; }

        /// <summary>
        ///  货架库存 
        /// </summary>
        public virtual DbSet<ShelfStock> ShelfStock { get; set; }

        /// <summary>
        ///  上架单 
        /// </summary>
        public virtual DbSet<ShelfList> ShelfList { get; set; }

        /// <summary>
        ///  采购凭证单 
        /// </summary>
        public virtual DbSet<PurchaseVoucher> PurchaseVoucher { get; set; }

        /// <summary>
        ///  出库单 
        /// </summary>
        public virtual DbSet<OutboundOrder> OutboundOrder { get; set; }

        /// <summary>
        ///  下架单 
        /// </summary>
        public virtual DbSet<LowerFrame> LowerFrame { get; set; }

        /// <summary>
        ///  销售凭证单 
        /// </summary>
        public virtual DbSet<SalesVoucher> SalesVoucher { get; set; }

        /// <summary>
        ///  菜单配置 
        /// </summary>
        public virtual DbSet<MenuConfiguration> MenuConfiguration { get; set; }

        /// <summary>
        ///  角色菜单 
        /// </summary>
        public virtual DbSet<RoleMenu> RoleMenu { get; set; }

        /// <summary>
        ///  用户角色 
        /// </summary>
        public virtual DbSet<UserRole> UserRole { get; set; }

        /// <summary>
        ///  角色配置 
        /// </summary>
        public virtual DbSet<RoleConfiguration> RoleConfiguration { get; set; }

        /// <summary>
        ///  用户信息 
        /// </summary>
        public virtual DbSet<UserInformation> UserInformation { get; set; }

        /// <summary>
        ///  登录记录 
        /// </summary>
        public virtual DbSet<LogonRecord> LogonRecord { get; set; }

        /// <summary>
        ///  用户菜单 
        /// </summary>
        public virtual DbSet<UserMenu> UserMenu { get; set; }

        /// <summary>
        ///  系统配置 
        /// </summary>
        public virtual DbSet<SystemConfiguration> SystemConfiguration { get; set; }
    }
}