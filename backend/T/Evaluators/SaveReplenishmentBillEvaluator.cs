using System;
using System.Linq;
using T.Models;
using TEntities.EF;
using TENtities;

namespace T.Evaluators
{
    public partial class SaveReplenishmentBillEvaluator : Evaluator
    {
        /// <summary>
        /// 检查仓库，检查实时库存
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected override CommonRequest OnBeforeEvaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
                var bill = request.data.Deserialize<ReplenishmentBill>();
                var warehouse =
                    ctx.Warehouse.FirstOrDefault(t =>
                        t.WWarehouseNumber == bill.RBWarehouseManagementStaffNumber && t.IsDeleted == 0);
                if (warehouse == null) throw new Exception("没有找到对应的仓库，请检查仓库编号");
                var totalIn = ctx.SupplyList.Where(t =>
                                      t.SLWarehouseNumber == bill.RBWarehouseManagementStaffNumber &&
                                      t.SLCargoNumber == bill.RBCargoNumber &&
                                      t.IsDeleted == 0)
                                  .Sum(t => t.SLAmount) ?? 0;
                var totalOut = ctx.ReplenishmentBill
                                   .Where(t => t.RBWarehouseManagementStaffNumber ==
                                               bill.RBWarehouseManagementStaffNumber &&
                                               t.RBCargoNumber == bill.RBCargoNumber &&
                                               t.IsDeleted == 0)
                                   .Sum(t => t.RBAmount) ?? 0;
                var remaining = totalIn - totalOut;
                if (remaining < (bill.RBAmount ?? 0)) throw new Exception($"仓库库存不足，当前库存（{remaining}）");
            }

            return request;
        }
    }
}