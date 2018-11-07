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
using T;
using T.Evaluators;
using T.Models;
using TENtities.EF;
using TENtities.EF.NewEntities;

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
        public void Run()
        {
            Assert.IsFalse(false);
        }

        [TestMethod]
        public void AMapSearchPOI()
        {
            string keywords = "香溪社区居委会";
            POI[] results = SearchAMapPOI(keywords);

            string actual1 = results.ToJson();
            Assert.AreEqual("", actual1);
        }

        private static POI[] SearchAMapPOI(string keywords)
        {
            string url = string.Format("https://restapi.amap.com/v3/place/text?keywords={0}&city=suzhou&offset=20&page=1&key=f053dcd72f0ad04daeaa371b0b410bac&extensions=all&output=json", keywords);
            HttpWebRequest request = WebRequest.CreateHttp(url);
            request.Timeout = 5000;
            WebResponse response = GetResponse(request);
            if (response == null)
            {
                return new POI[0];
            }
            StringBuilder builder = new StringBuilder();
            using (var stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream, Encoding.GetEncoding("UTF-8")))
                {
                    builder.Append(reader.ReadToEnd());
                }
                stream.Close();
            }
            string actual = builder.ToString();
            AMapSearchPOIResponse aMapSearchPOIResponse = actual.Deserialize<AMapSearchPOIResponse>();
            Pois[] pois = aMapSearchPOIResponse?.pois;//.Where(t => t.name == keywords).ToArray();
            POI[] results = pois?.Select(t => new POI { POIAddress = t.address, Longitude = t.location.Split(',')[0], Latitude = t.location.Split(',')[1] }).ToArray();
            if (results == null) return new POI[0];
            return results;
        }

        private static WebResponse GetResponse(HttpWebRequest request, int times = 0)
        {
            if (times > 3)
            {
                File.WriteAllText(@"D:\log\" + DateTime.Now.Ticks + ".log", string.Format("url {0} failed too many times", request.RequestUri.AbsoluteUri.ToString()));
                return null;
            }
            try
            {
                //Thread.Sleep(5000);
                WebResponse response = request.GetResponse();
                return response;
            }
            catch (System.Net.WebException exception)
            {
                return GetResponse(request, ++times);
            }
        }

        [TestMethod]
        public async Task PopulationDataTestAsync()
        {
            var loginoutput = await Evaluator.Call<CommonOutputT<string>>("login", new UserInformation
            {
                UILoginName = "admin",
                UICode = "123",
            });


            var token = loginoutput.data;
            var saveresult = true;
            foreach (var item in Enumerable.Repeat<int>(1, 123))
            {
                var savepopulation = await Evaluator.Call<CommonOutputT<string>>("savepopulation", new Population
                {
                    PName = "测试数据"
                }, token);
                saveresult = saveresult && savepopulation.success;
            }
            var getpopulation_output = await Evaluator.Call<CommonOutputList<Population>>("getpopulationlist", null, token);
            Assert.AreEqual(getpopulation_output.total, 123);
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


                ctx.UserInformation.RemoveRange(ctx.UserInformation.ToList());

                ctx.UserInformation.Add(new UserInformation { CreateBy = CreateBy, TransactionID = TransactionID, IsDeleted = 0, id = Guid.NewGuid().ToString(), UILoginName = "admin", UISubordinateDepartment = "香溪管理员", UICode = UICode, DataLevel = "01" });
                ctx.UserInformation.Add(new UserInformation { CreateBy = CreateBy, TransactionID = TransactionID, IsDeleted = 0, id = Guid.NewGuid().ToString(), UILoginName = "vheader", UISubordinateDepartment = "香溪村长", UICode = UICode, DataLevel = "01" });
                ctx.UserInformation.Add(new UserInformation { CreateBy = CreateBy, TransactionID = TransactionID, IsDeleted = 0, id = Guid.NewGuid().ToString(), UILoginName = "manager", UISubordinateDepartment = "香溪村委会理事", UICode = UICode, DataLevel = "0101" });
                ctx.UserInformation.Add(new UserInformation { CreateBy = CreateBy, TransactionID = TransactionID, IsDeleted = 0, id = Guid.NewGuid().ToString(), UILoginName = "partymanager", UISubordinateDepartment = "香溪党建管理员", UICode = UICode, DataLevel = "0102" });
                ctx.UserInformation.Add(new UserInformation { CreateBy = CreateBy, TransactionID = TransactionID, IsDeleted = 0, id = Guid.NewGuid().ToString(), UILoginName = "financialmanager", UISubordinateDepartment = "香溪经济管理员", UICode = UICode, DataLevel = "0103" });
                ctx.UserInformation.Add(new UserInformation { CreateBy = CreateBy, TransactionID = TransactionID, IsDeleted = 0, id = Guid.NewGuid().ToString(), UILoginName = "businessmanager", UISubordinateDepartment = "村务管理员", UICode = UICode, DataLevel = "0104" });
                ctx.UserInformation.Add(new UserInformation { CreateBy = CreateBy, TransactionID = TransactionID, IsDeleted = 0, id = Guid.NewGuid().ToString(), UILoginName = "interactionmanager", UISubordinateDepartment = "信息互动管理员", UICode = UICode, DataLevel = "0105" });
                ctx.UserInformation.Add(new UserInformation { CreateBy = CreateBy, TransactionID = TransactionID, IsDeleted = 0, id = Guid.NewGuid().ToString(), UILoginName = "populationmanager", UISubordinateDepartment = "香溪人口管理员", UICode = UICode, DataLevel = "0106" });

                ctx.RoleConfiguration.RemoveRange(ctx.RoleConfiguration.ToList());

                ctx.RoleConfiguration.Add(new RoleConfiguration { CreateBy = CreateBy, TransactionID = TransactionID, DataLevel = "01", IsDeleted = 0, id = Guid.NewGuid().ToString(), RCRoleName = "香溪管理员", RCAffiliatedOrganization = "香溪村委会" });
                ctx.RoleConfiguration.Add(new RoleConfiguration { CreateBy = CreateBy, TransactionID = TransactionID, DataLevel = "01", IsDeleted = 0, id = Guid.NewGuid().ToString(), RCRoleName = "香溪村长", RCAffiliatedOrganization = "香溪村委会" });
                ctx.RoleConfiguration.Add(new RoleConfiguration { CreateBy = CreateBy, TransactionID = TransactionID, DataLevel = "01", IsDeleted = 0, id = Guid.NewGuid().ToString(), RCRoleName = "香溪村委会理事", RCAffiliatedOrganization = "香溪村委会" });
                ctx.RoleConfiguration.Add(new RoleConfiguration { CreateBy = CreateBy, TransactionID = TransactionID, DataLevel = "01", IsDeleted = 0, id = Guid.NewGuid().ToString(), RCRoleName = "香溪党建管理员", RCAffiliatedOrganization = "香溪村委会" });
                ctx.RoleConfiguration.Add(new RoleConfiguration { CreateBy = CreateBy, TransactionID = TransactionID, DataLevel = "01", IsDeleted = 0, id = Guid.NewGuid().ToString(), RCRoleName = "香溪经济管理员", RCAffiliatedOrganization = "香溪村委会" });
                ctx.RoleConfiguration.Add(new RoleConfiguration { CreateBy = CreateBy, TransactionID = TransactionID, DataLevel = "01", IsDeleted = 0, id = Guid.NewGuid().ToString(), RCRoleName = "村务管理员", RCAffiliatedOrganization = "香溪村委会" });
                ctx.RoleConfiguration.Add(new RoleConfiguration { CreateBy = CreateBy, TransactionID = TransactionID, DataLevel = "01", IsDeleted = 0, id = Guid.NewGuid().ToString(), RCRoleName = "信息互动管理员", RCAffiliatedOrganization = "香溪村委会" });
                ctx.RoleConfiguration.Add(new RoleConfiguration { CreateBy = CreateBy, TransactionID = TransactionID, DataLevel = "01", IsDeleted = 0, id = Guid.NewGuid().ToString(), RCRoleName = "香溪人口管理员", RCAffiliatedOrganization = "香溪村委会" });

                ctx.UserRole.RemoveRange(ctx.UserRole.ToList());

                ctx.UserRole.Add(new UserRole { CreateBy = CreateBy, TransactionID = TransactionID, DataLevel = "01", IsDeleted = 0, id = Guid.NewGuid().ToString(), URLoginName = "admin", URRoleName = "香溪管理员" });
                ctx.UserRole.Add(new UserRole { CreateBy = CreateBy, TransactionID = TransactionID, DataLevel = "01", IsDeleted = 0, id = Guid.NewGuid().ToString(), URLoginName = "vheader", URRoleName = "香溪村长" });
                ctx.UserRole.Add(new UserRole { CreateBy = CreateBy, TransactionID = TransactionID, DataLevel = "01", IsDeleted = 0, id = Guid.NewGuid().ToString(), URLoginName = "manager", URRoleName = "香溪村委会理事" });
                ctx.UserRole.Add(new UserRole { CreateBy = CreateBy, TransactionID = TransactionID, DataLevel = "01", IsDeleted = 0, id = Guid.NewGuid().ToString(), URLoginName = "partymanager", URRoleName = "香溪党建管理员" });
                ctx.UserRole.Add(new UserRole { CreateBy = CreateBy, TransactionID = TransactionID, DataLevel = "01", IsDeleted = 0, id = Guid.NewGuid().ToString(), URLoginName = "financialmanager", URRoleName = "香溪经济管理员" });
                ctx.UserRole.Add(new UserRole { CreateBy = CreateBy, TransactionID = TransactionID, DataLevel = "01", IsDeleted = 0, id = Guid.NewGuid().ToString(), URLoginName = "businessmanager", URRoleName = "村务管理员" });
                ctx.UserRole.Add(new UserRole { CreateBy = CreateBy, TransactionID = TransactionID, DataLevel = "01", IsDeleted = 0, id = Guid.NewGuid().ToString(), URLoginName = "interactionmanager", URRoleName = "信息互动管理员" });
                ctx.UserRole.Add(new UserRole { CreateBy = CreateBy, TransactionID = TransactionID, DataLevel = "01", IsDeleted = 0, id = Guid.NewGuid().ToString(), URLoginName = "populationmanager", URRoleName = "香溪人口管理员" });

                ctx.RoleMenu.RemoveRange(ctx.RoleMenu.ToList());

                foreach (var title in ctx.MenuConfiguration.Select(p => p.MCCaption))
                {
                    ctx.RoleMenu.Add(new RoleMenu { CreateBy = CreateBy, TransactionID = TransactionID, DataLevel = "01", IsDeleted = 0, id = Guid.NewGuid().ToString(), RMRoleName = "香溪管理员", RMMenuTitle = title });
                    ctx.RoleMenu.Add(new RoleMenu { CreateBy = CreateBy, TransactionID = TransactionID, DataLevel = "01", IsDeleted = 0, id = Guid.NewGuid().ToString(), RMRoleName = "香溪村长", RMMenuTitle = title });
                    ctx.RoleMenu.Add(new RoleMenu { CreateBy = CreateBy, TransactionID = TransactionID, DataLevel = "01", IsDeleted = 0, id = Guid.NewGuid().ToString(), RMRoleName = "香溪村委会理事", RMMenuTitle = title });
                }

                foreach (var title in ctx.MenuConfiguration.Where(p => p.MCCaption == "党建管理" || p.MCParentTitle == "党建管理").Select(p => p.MCCaption))
                {
                    ctx.RoleMenu.Add(new RoleMenu { CreateBy = CreateBy, TransactionID = TransactionID, DataLevel = "01", IsDeleted = 0, id = Guid.NewGuid().ToString(), RMRoleName = "香溪党建管理员", RMMenuTitle = title });
                }

                foreach (var title in ctx.MenuConfiguration.Where(p => p.MCCaption == "产业管理" || p.MCParentTitle == "产业管理").Select(p => p.MCCaption))
                {
                    ctx.RoleMenu.Add(new RoleMenu { CreateBy = CreateBy, TransactionID = TransactionID, DataLevel = "01", IsDeleted = 0, id = Guid.NewGuid().ToString(), RMRoleName = "香溪经济管理员", RMMenuTitle = title });
                }

                foreach (var title in ctx.MenuConfiguration.Where(p => p.MCCaption == "村务管理" || p.MCParentTitle == "村务管理").Select(p => p.MCCaption))
                {
                    ctx.RoleMenu.Add(new RoleMenu { CreateBy = CreateBy, TransactionID = TransactionID, DataLevel = "01", IsDeleted = 0, id = Guid.NewGuid().ToString(), RMRoleName = "村务管理员", RMMenuTitle = title });
                }

                foreach (var title in ctx.MenuConfiguration.Where(p => p.MCCaption == "香溪发布" || p.MCParentTitle == "香溪发布").Select(p => p.MCCaption))
                {
                    ctx.RoleMenu.Add(new RoleMenu { CreateBy = CreateBy, TransactionID = TransactionID, DataLevel = "01", IsDeleted = 0, id = Guid.NewGuid().ToString(), RMRoleName = "信息互动管理员", RMMenuTitle = title });
                }

                foreach (var title in ctx.MenuConfiguration.Where(p => p.MCCaption == "人口管理" || p.MCParentTitle == "人口管理").Select(p => p.MCCaption))
                {
                    ctx.RoleMenu.Add(new RoleMenu { CreateBy = CreateBy, TransactionID = TransactionID, DataLevel = "01", IsDeleted = 0, id = Guid.NewGuid().ToString(), RMRoleName = "香溪人口管理员", RMMenuTitle = title });
                }

                var population_list = ctx.Population.Take(80).ToList();
                foreach (var population in population_list)
                {
                    ctx.InformationManagementOfPartyMembers.Add(new InformationManagementOfPartyMembers { id = Guid.NewGuid().ToString(), IMOPMName = population.PName, IMOPMIdNumber = population.PCitizenshipNumber, DataLevel = "01", IsDeleted=0 });
                    ctx.TheObjectOfCare.Add(new TheObjectOfCare { id = Guid.NewGuid().ToString(), DataLevel = "01", IsDeleted = 0, TOOCIdNumber = population.PCitizenshipNumber, TOOCName = population.PName });
                    ctx.Cadre.Add(new Cadre { id = Guid.NewGuid().ToString(), DataLevel = "01", IsDeleted = 0, CIdCard = population.PCitizenshipNumber });
                    ctx.Militia.Add(new Militia { id = Guid.NewGuid().ToString(), DataLevel = "01", IsDeleted = 0, MIdCard = population.PCitizenshipNumber, MName = population.PName });
                }

                ctx.SaveChanges();
            }
        }

        [TestMethod]
        public void CreatePOI()
        {
            var TransactionID = Guid.NewGuid().ToString();
            var CreateBy = "Initialization Job";
            using (var ctx = new DefaultContext())
            {
                int count = 0;
                var address_list = ctx.Building.GroupBy(t => t.BTenant).Select(t => t.Key).ToList();
                address_list.AddRange(ctx
                    .Population.GroupBy(t => t.PAddress).Select(t => t.Key).ToList());
                foreach (var address in address_list)
                {
                    if (ctx.POI.Any(t => t.POIAddress == address)) continue;
                    var poi = SearchAMapPOI(address)?.FirstOrDefault();
                    if (poi == null) continue;
                    if (poi != null)
                    {
                        poi.DataLevel = "01";
                        poi.id = Guid.NewGuid().ToString();
                        poi.POIAddress = address;
                        poi.IsDeleted = 0;
                        poi.CreateBy = CreateBy;
                        poi.TransactionID = TransactionID;
                        ctx.POI.Add(poi);
                    }
                    if (++count > 5)
                    {
                        count = 0;
                        ctx.SaveChanges();
                    }
                }
                ctx.SaveChanges();

            }
        }
    }
}
