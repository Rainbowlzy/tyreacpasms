  

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

namespace TEntities.EF
{
    public class DefaultContext : DbContext
    {
	
#if DEBUG
        public DefaultContext():base("LocalDefaultContext") { }
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
			
			if (!MenuConfiguration.Any(t => t.MCCaption == "菜单配置"))
			{
				MenuConfiguration.Add(new MenuConfiguration
				{
					
					MCCaption = "菜单配置",
					MCLink = "/T/gen/MenuConfigurationList.html",
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
					
					MCCaption = "菜单配置统计",
					MCLink = "/T/gen/MenuConfigurationAnalysis.html",
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
				menuconfiguration.MCLink = "/T/gen/MenuConfigurationList.html";
				MenuConfiguration.AddOrUpdate(menuconfiguration);
			}

			if (!MenuConfiguration.Any(t => t.MCCaption == "角色菜单"))
			{
				MenuConfiguration.Add(new MenuConfiguration
				{
					
					MCCaption = "角色菜单",
					MCLink = "/T/gen/RoleMenuList.html",
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
					
					MCCaption = "角色菜单统计",
					MCLink = "/T/gen/RoleMenuAnalysis.html",
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
				rolemenu.MCLink = "/T/gen/RoleMenuList.html";
				MenuConfiguration.AddOrUpdate(rolemenu);
			}

			if (!MenuConfiguration.Any(t => t.MCCaption == "用户角色"))
			{
				MenuConfiguration.Add(new MenuConfiguration
				{
					
					MCCaption = "用户角色",
					MCLink = "/T/gen/UserRoleList.html",
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
					
					MCCaption = "用户角色统计",
					MCLink = "/T/gen/UserRoleAnalysis.html",
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
				userrole.MCLink = "/T/gen/UserRoleList.html";
				MenuConfiguration.AddOrUpdate(userrole);
			}

			if (!MenuConfiguration.Any(t => t.MCCaption == "角色配置"))
			{
				MenuConfiguration.Add(new MenuConfiguration
				{
					
					MCCaption = "角色配置",
					MCLink = "/T/gen/RoleConfigurationList.html",
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
					
					MCCaption = "角色配置统计",
					MCLink = "/T/gen/RoleConfigurationAnalysis.html",
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
				roleconfiguration.MCLink = "/T/gen/RoleConfigurationList.html";
				MenuConfiguration.AddOrUpdate(roleconfiguration);
			}

			if (!MenuConfiguration.Any(t => t.MCCaption == "用户信息"))
			{
				MenuConfiguration.Add(new MenuConfiguration
				{
					
					MCCaption = "用户信息",
					MCLink = "/T/gen/UserInformationList.html",
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
					
					MCCaption = "用户信息统计",
					MCLink = "/T/gen/UserInformationAnalysis.html",
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
				userinformation.MCLink = "/T/gen/UserInformationList.html";
				MenuConfiguration.AddOrUpdate(userinformation);
			}

			if (!MenuConfiguration.Any(t => t.MCCaption == "登录记录"))
			{
				MenuConfiguration.Add(new MenuConfiguration
				{
					
					MCCaption = "登录记录",
					MCLink = "/T/gen/LogonRecordList.html",
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
					
					MCCaption = "登录记录统计",
					MCLink = "/T/gen/LogonRecordAnalysis.html",
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
				logonrecord.MCLink = "/T/gen/LogonRecordList.html";
				MenuConfiguration.AddOrUpdate(logonrecord);
			}

			if (!MenuConfiguration.Any(t => t.MCCaption == "用户菜单"))
			{
				MenuConfiguration.Add(new MenuConfiguration
				{
					
					MCCaption = "用户菜单",
					MCLink = "/T/gen/UserMenuList.html",
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
					
					MCCaption = "用户菜单统计",
					MCLink = "/T/gen/UserMenuAnalysis.html",
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
				usermenu.MCLink = "/T/gen/UserMenuList.html";
				MenuConfiguration.AddOrUpdate(usermenu);
			}

			if (!MenuConfiguration.Any(t => t.MCCaption == "系统配置"))
			{
				MenuConfiguration.Add(new MenuConfiguration
				{
					
					MCCaption = "系统配置",
					MCLink = "/T/gen/SystemConfigurationList.html",
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
					
					MCCaption = "系统配置统计",
					MCLink = "/T/gen/SystemConfigurationAnalysis.html",
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
				systemconfiguration.MCLink = "/T/gen/SystemConfigurationList.html";
				MenuConfiguration.AddOrUpdate(systemconfiguration);
			}

			if (!MenuConfiguration.Any(t => t.MCCaption == "客户"))
			{
				MenuConfiguration.Add(new MenuConfiguration
				{
					
					MCCaption = "客户",
					MCLink = "/T/gen/CustomertypeList.html",
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
					
					MCCaption = "客户统计",
					MCLink = "/T/gen/CustomertypeAnalysis.html",
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
				customertype.MCLink = "/T/gen/CustomertypeList.html";
				MenuConfiguration.AddOrUpdate(customertype);
			}

			if (!MenuConfiguration.Any(t => t.MCCaption == "供应商"))
			{
				MenuConfiguration.Add(new MenuConfiguration
				{
					
					MCCaption = "供应商",
					MCLink = "/T/gen/SupplierList.html",
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
					
					MCCaption = "供应商统计",
					MCLink = "/T/gen/SupplierAnalysis.html",
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
				supplier.MCLink = "/T/gen/SupplierList.html";
				MenuConfiguration.AddOrUpdate(supplier);
			}

			if (!MenuConfiguration.Any(t => t.MCCaption == "货物种类"))
			{
				MenuConfiguration.Add(new MenuConfiguration
				{
					
					MCCaption = "货物种类",
					MCLink = "/T/gen/TypeOfGoodsList.html",
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
					
					MCCaption = "货物种类统计",
					MCLink = "/T/gen/TypeOfGoodsAnalysis.html",
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
				typeofgoods.MCLink = "/T/gen/TypeOfGoodsList.html";
				MenuConfiguration.AddOrUpdate(typeofgoods);
			}

			if (!MenuConfiguration.Any(t => t.MCCaption == "供货渠道"))
			{
				MenuConfiguration.Add(new MenuConfiguration
				{
					
					MCCaption = "供货渠道",
					MCLink = "/T/gen/SupplyChannelList.html",
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
					
					MCCaption = "供货渠道统计",
					MCLink = "/T/gen/SupplyChannelAnalysis.html",
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
				supplychannel.MCLink = "/T/gen/SupplyChannelList.html";
				MenuConfiguration.AddOrUpdate(supplychannel);
			}

			if (!MenuConfiguration.Any(t => t.MCCaption == "订单"))
			{
				MenuConfiguration.Add(new MenuConfiguration
				{
					
					MCCaption = "订单",
					MCLink = "/T/gen/OrderList.html",
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
					
					MCCaption = "订单统计",
					MCLink = "/T/gen/OrderAnalysis.html",
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
				order.MCLink = "/T/gen/OrderList.html";
				MenuConfiguration.AddOrUpdate(order);
			}

			if (!MenuConfiguration.Any(t => t.MCCaption == "订单明细"))
			{
				MenuConfiguration.Add(new MenuConfiguration
				{
					
					MCCaption = "订单明细",
					MCLink = "/T/gen/OrderDetailsList.html",
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
					
					MCCaption = "订单明细统计",
					MCLink = "/T/gen/OrderDetailsAnalysis.html",
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
				orderdetails.MCLink = "/T/gen/OrderDetailsList.html";
				MenuConfiguration.AddOrUpdate(orderdetails);
			}

			if (!MenuConfiguration.Any(t => t.MCCaption == "入库记录"))
			{
				MenuConfiguration.Add(new MenuConfiguration
				{
					
					MCCaption = "入库记录",
					MCLink = "/T/gen/WarehousingRecordList.html",
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
					
					MCCaption = "入库记录统计",
					MCLink = "/T/gen/WarehousingRecordAnalysis.html",
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
				warehousingrecord.MCLink = "/T/gen/WarehousingRecordList.html";
				MenuConfiguration.AddOrUpdate(warehousingrecord);
			}

			if (!MenuConfiguration.Any(t => t.MCCaption == "仓库"))
			{
				MenuConfiguration.Add(new MenuConfiguration
				{
					
					MCCaption = "仓库",
					MCLink = "/T/gen/WarehouseList.html",
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
					
					MCCaption = "仓库统计",
					MCLink = "/T/gen/WarehouseAnalysis.html",
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
				warehouse.MCLink = "/T/gen/WarehouseList.html";
				MenuConfiguration.AddOrUpdate(warehouse);
			}

			if (!MenuConfiguration.Any(t => t.MCCaption == "货架"))
			{
				MenuConfiguration.Add(new MenuConfiguration
				{
					
					MCCaption = "货架",
					MCLink = "/T/gen/GoodsShelvesList.html",
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
					
					MCCaption = "货架统计",
					MCLink = "/T/gen/GoodsShelvesAnalysis.html",
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
				goodsshelves.MCLink = "/T/gen/GoodsShelvesList.html";
				MenuConfiguration.AddOrUpdate(goodsshelves);
			}

			if (!MenuConfiguration.Any(t => t.MCCaption == "补货申请单"))
			{
				MenuConfiguration.Add(new MenuConfiguration
				{
					
					MCCaption = "补货申请单",
					MCLink = "/T/gen/ReplenishmentApplicationFormList.html",
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
					
					MCCaption = "补货申请单统计",
					MCLink = "/T/gen/ReplenishmentApplicationFormAnalysis.html",
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
				replenishmentapplicationform.MCLink = "/T/gen/ReplenishmentApplicationFormList.html";
				MenuConfiguration.AddOrUpdate(replenishmentapplicationform);
			}

			if (!MenuConfiguration.Any(t => t.MCCaption == "补货记录"))
			{
				MenuConfiguration.Add(new MenuConfiguration
				{
					
					MCCaption = "补货记录",
					MCLink = "/T/gen/ReplenishmentRecordList.html",
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
					
					MCCaption = "补货记录统计",
					MCLink = "/T/gen/ReplenishmentRecordAnalysis.html",
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
				replenishmentrecord.MCLink = "/T/gen/ReplenishmentRecordList.html";
				MenuConfiguration.AddOrUpdate(replenishmentrecord);
			}

			if (!MenuConfiguration.Any(t => t.MCCaption == "销售记录"))
			{
				MenuConfiguration.Add(new MenuConfiguration
				{
					
					MCCaption = "销售记录",
					MCLink = "/T/gen/SalesRecordList.html",
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
					
					MCCaption = "销售记录统计",
					MCLink = "/T/gen/SalesRecordAnalysis.html",
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
				salesrecord.MCLink = "/T/gen/SalesRecordList.html";
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