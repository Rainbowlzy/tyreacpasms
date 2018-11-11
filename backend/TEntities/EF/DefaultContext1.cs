  

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
using EF.Entities;

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
			
			modelBuilder.Entity<MenuConfiguration>().Property(p => p.id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 
			modelBuilder.Entity<RoleMenu>().Property(p => p.id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 
			modelBuilder.Entity<UserRole>().Property(p => p.id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 
			modelBuilder.Entity<RoleConfiguration>().Property(p => p.id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 
			modelBuilder.Entity<UserInformation>().Property(p => p.id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 
			modelBuilder.Entity<LogonRecord>().Property(p => p.id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 
			modelBuilder.Entity<UserMenu>().Property(p => p.id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 
			modelBuilder.Entity<SystemConfiguration>().Property(p => p.id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 
			modelBuilder.Entity<Customertype>().Property(p => p.id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 
			modelBuilder.Entity<Supplier>().Property(p => p.id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 
			modelBuilder.Entity<TypeOfGoods>().Property(p => p.id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 
			modelBuilder.Entity<SupplyChannel>().Property(p => p.id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 
			modelBuilder.Entity<Order>().Property(p => p.id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 
			modelBuilder.Entity<OrderDetails>().Property(p => p.id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 
			modelBuilder.Entity<WarehousingRecord>().Property(p => p.id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 
			modelBuilder.Entity<Warehouse>().Property(p => p.id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 
			modelBuilder.Entity<GoodsShelves>().Property(p => p.id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 
			modelBuilder.Entity<ReplenishmentApplicationForm>().Property(p => p.id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 
			modelBuilder.Entity<ReplenishmentRecord>().Property(p => p.id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 
			modelBuilder.Entity<SalesRecord>().Property(p => p.id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 
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
                UIJobNumber = i.ToString(),
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

			if (!MenuConfiguration.Any(t => t.MCCaption == "货物种类"))
			{
				RoleMenu.Add(new RoleMenu
				{
					RMRoleName = "超级管理员",
					RMMenuTitle = "货物种类",
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
					MCDisplayName = "货物种类",
					MCCaption = "货物种类",
					MCLink = "/pagelist/TypeOfGoods",
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
			var typeofgoods = MenuConfiguration.FirstOrDefault(t => t.MCCaption == "货物种类");
			if(typeofgoods!=null)
			{
				typeofgoods.MCLink = "/pagelist/TypeOfGoodsList.html";
				MenuConfiguration.AddOrUpdate(typeofgoods);
			}

			if (!MenuConfiguration.Any(t => t.MCCaption == "供货渠道"))
			{
				RoleMenu.Add(new RoleMenu
				{
					RMRoleName = "超级管理员",
					RMMenuTitle = "供货渠道",
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
					MCDisplayName = "供货渠道",
					MCCaption = "供货渠道",
					MCLink = "/pagelist/SupplyChannel",
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
			var supplychannel = MenuConfiguration.FirstOrDefault(t => t.MCCaption == "供货渠道");
			if(supplychannel!=null)
			{
				supplychannel.MCLink = "/pagelist/SupplyChannelList.html";
				MenuConfiguration.AddOrUpdate(supplychannel);
			}

			if (!MenuConfiguration.Any(t => t.MCCaption == "订单"))
			{
				RoleMenu.Add(new RoleMenu
				{
					RMRoleName = "超级管理员",
					RMMenuTitle = "订单",
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
					MCDisplayName = "订单",
					MCCaption = "订单",
					MCLink = "/pagelist/Order",
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
			var order = MenuConfiguration.FirstOrDefault(t => t.MCCaption == "订单");
			if(order!=null)
			{
				order.MCLink = "/pagelist/OrderList.html";
				MenuConfiguration.AddOrUpdate(order);
			}

			if (!MenuConfiguration.Any(t => t.MCCaption == "订单明细"))
			{
				RoleMenu.Add(new RoleMenu
				{
					RMRoleName = "超级管理员",
					RMMenuTitle = "订单明细",
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
					MCDisplayName = "订单明细",
					MCCaption = "订单明细",
					MCLink = "/pagelist/OrderDetails",
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
			var orderdetails = MenuConfiguration.FirstOrDefault(t => t.MCCaption == "订单明细");
			if(orderdetails!=null)
			{
				orderdetails.MCLink = "/pagelist/OrderDetailsList.html";
				MenuConfiguration.AddOrUpdate(orderdetails);
			}

			if (!MenuConfiguration.Any(t => t.MCCaption == "入库记录"))
			{
				RoleMenu.Add(new RoleMenu
				{
					RMRoleName = "超级管理员",
					RMMenuTitle = "入库记录",
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
					MCDisplayName = "入库记录",
					MCCaption = "入库记录",
					MCLink = "/pagelist/WarehousingRecord",
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
			var warehousingrecord = MenuConfiguration.FirstOrDefault(t => t.MCCaption == "入库记录");
			if(warehousingrecord!=null)
			{
				warehousingrecord.MCLink = "/pagelist/WarehousingRecordList.html";
				MenuConfiguration.AddOrUpdate(warehousingrecord);
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

			if (!MenuConfiguration.Any(t => t.MCCaption == "补货申请单"))
			{
				RoleMenu.Add(new RoleMenu
				{
					RMRoleName = "超级管理员",
					RMMenuTitle = "补货申请单",
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
					MCDisplayName = "补货申请单",
					MCCaption = "补货申请单",
					MCLink = "/pagelist/ReplenishmentApplicationForm",
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
			var replenishmentapplicationform = MenuConfiguration.FirstOrDefault(t => t.MCCaption == "补货申请单");
			if(replenishmentapplicationform!=null)
			{
				replenishmentapplicationform.MCLink = "/pagelist/ReplenishmentApplicationFormList.html";
				MenuConfiguration.AddOrUpdate(replenishmentapplicationform);
			}

			if (!MenuConfiguration.Any(t => t.MCCaption == "补货记录"))
			{
				RoleMenu.Add(new RoleMenu
				{
					RMRoleName = "超级管理员",
					RMMenuTitle = "补货记录",
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
					MCDisplayName = "补货记录",
					MCCaption = "补货记录",
					MCLink = "/pagelist/ReplenishmentRecord",
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
			var replenishmentrecord = MenuConfiguration.FirstOrDefault(t => t.MCCaption == "补货记录");
			if(replenishmentrecord!=null)
			{
				replenishmentrecord.MCLink = "/pagelist/ReplenishmentRecordList.html";
				MenuConfiguration.AddOrUpdate(replenishmentrecord);
			}

			if (!MenuConfiguration.Any(t => t.MCCaption == "销售记录"))
			{
				RoleMenu.Add(new RoleMenu
				{
					RMRoleName = "超级管理员",
					RMMenuTitle = "销售记录",
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
					MCDisplayName = "销售记录",
					MCCaption = "销售记录",
					MCLink = "/pagelist/SalesRecord",
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
			var salesrecord = MenuConfiguration.FirstOrDefault(t => t.MCCaption == "销售记录");
			if(salesrecord!=null)
			{
				salesrecord.MCLink = "/pagelist/SalesRecordList.html";
				MenuConfiguration.AddOrUpdate(salesrecord);
			}
            

		    SaveChanges();
		}

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

        /// <summary>
        ///  客户 
        /// </summary>
        public virtual DbSet<Customertype> Customertype { get; set; }

        /// <summary>
        ///  供应商 
        /// </summary>
        public virtual DbSet<Supplier> Supplier { get; set; }

        /// <summary>
        ///  货物种类 
        /// </summary>
        public virtual DbSet<TypeOfGoods> TypeOfGoods { get; set; }

        /// <summary>
        ///  供货渠道 
        /// </summary>
        public virtual DbSet<SupplyChannel> SupplyChannel { get; set; }

        /// <summary>
        ///  订单 
        /// </summary>
        public virtual DbSet<Order> Order { get; set; }

        /// <summary>
        ///  订单明细 
        /// </summary>
        public virtual DbSet<OrderDetails> OrderDetails { get; set; }

        /// <summary>
        ///  入库记录 
        /// </summary>
        public virtual DbSet<WarehousingRecord> WarehousingRecord { get; set; }

        /// <summary>
        ///  仓库 
        /// </summary>
        public virtual DbSet<Warehouse> Warehouse { get; set; }

        /// <summary>
        ///  货架 
        /// </summary>
        public virtual DbSet<GoodsShelves> GoodsShelves { get; set; }

        /// <summary>
        ///  补货申请单 
        /// </summary>
        public virtual DbSet<ReplenishmentApplicationForm> ReplenishmentApplicationForm { get; set; }

        /// <summary>
        ///  补货记录 
        /// </summary>
        public virtual DbSet<ReplenishmentRecord> ReplenishmentRecord { get; set; }

        /// <summary>
        ///  销售记录 
        /// </summary>
        public virtual DbSet<SalesRecord> SalesRecord { get; set; }
    }
}