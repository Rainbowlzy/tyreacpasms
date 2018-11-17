using System;
using System.Linq;
using T.Models;
using TEntities.EF;
using TENtities;

namespace T.Evaluators
{
    /// <summary>
    /// 保存【补货单】
    /// </summary>
    public partial class SaveReplenishmentBillEvaluator : Evaluator
    {
        protected override CommonRequest OnBeforeEvaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
                var bill = request.data.Deserialize<ReplenishmentBill>();
                if (bill.RBAmount == 0) throw new Exception("补货数量为0，存在虚假数据");
                var time = bill.RBDate ?? DateTime.Now;
                var warehouse =
                    ctx.Warehouse.FirstOrDefault(t =>
                        t.WWarehouseNumber == bill.RBWarehouseNumber && t.IsDeleted == 0);
                if (warehouse == null) throw new Exception("没有找到对应的仓库，请检查仓库编号");
                var totalIn = ctx.SupplyWarehousingList.Where(t =>
                                      t.SWLWarehouseNumber == bill.RBWarehouseNumber &&
                                      t.SWLCargoNumber == bill.RBCargoNumber &&
                                      t.SWLDate <= time &&
                                      t.IsDeleted == 0)
                                  .Sum(t => t.SWLAmount) ?? 0;
                var totalOut = ctx.ReplenishmentBill
                                   .Where(t => t.RBWarehouseNumber ==
                                               bill.RBWarehouseNumber &&
                                               t.RBCargoNumber == bill.RBCargoNumber &&
                                               t.RBDate <= time &&
                                               t.IsDeleted == 0)
                                   .Sum(t => t.RBAmount) ?? 0;
                var remaining = totalIn - totalOut;
                if (remaining < (bill.RBAmount ?? 0)) throw new Exception($"仓库库存不足，当前库存（{remaining}）");
            }

            return request;
        }
    }
}