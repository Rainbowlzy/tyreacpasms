using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using EF.Entities;
using T;
using T.Evaluators;
using T.Models;
using TEntities.EF;

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
