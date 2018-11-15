using System;
using System.Linq;
using T.Models;
using TEntities.EF;
using TENtities;

namespace T.Evaluators
{
    /// <summary>
    /// 保存【销售单】
    /// </summary>
    public partial class SaveSalesUnitPriceEvaluator : Evaluator
    {
        protected override CommonRequest OnBeforeEvaluate(CommonRequest request)
        {
            using (var ctx = new DefaultContext())
            {
                var salesBill = request.data.Deserialize<SalesUnitPrice>();
                var time = salesBill.SUPDate ?? DateTime.Now;
                if (salesBill.SUPAmount == 0) throw new Exception("销售数量为0，存在虚假数据");
                var totalIn = ctx.ReplenishmentBill
                                  .Where(t => t.RBCargoNumber == salesBill.SUPCargoNumber && t.RBDate <= time &&
                                              t.IsDeleted == 0)
                                  .Sum(t => t.RBAmount) ?? 0;
                var totalOut = ctx.SalesUnitPrice
                                   .Where(t => t.SUPCargoNumber == salesBill.SUPCargoNumber && t.SUPDate <= time &&
                                               t.IsDeleted == 0)
                                   .Sum(t => t.SUPAmount) ?? 0;
                var remaining = totalIn - totalOut;
                if (salesBill.SUPAmount > remaining)
                {
                    throw new Exception($"货架库存不足，不能完成销售单，当前库存（{remaining}）");
                }
            }

            return request;
        }
    }
}