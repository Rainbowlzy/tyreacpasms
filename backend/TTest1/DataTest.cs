using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TEntities.EF;
using TENtities.EF;

namespace TTests1
{
    [TestClass()]
    public class DataTest
    {
        private const int MAX_BUF = 409600;

        public DataTest()
        {

        }



        [TestMethod]
        public void CreateData()
        {
            using (var ctx = new DefaultContext())
            {
                var UICode = "123";
                var TransactionID = Guid.NewGuid().ToString();
                var CreateBy = "Initialization Job";

                ctx.BuildMenu();

                ctx.SaveChanges();
            }
        }
    }
}
