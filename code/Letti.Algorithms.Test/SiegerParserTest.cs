using Leeti.Algorithms;
using Letti.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Letti.Algorithms.Test
{
    [TestClass]
    public class SiegerParserTest
    {
        [TestMethod]
        public void can_find_single_company()
        {
            SigerParser parser = new SigerParser();
            string test = System.IO.File.ReadAllText("sieger1.txt");
            string testCompany = "INGENIERIA DE BOMBAS Y CONTROLES, S.A. DE C.V.";
            List<SiegerCompanyRecord> records=parser.GetCompanies(test);
            Assert.AreEqual(1, records.Count);
            Assert.AreEqual(0, records.First().ControlIndex);
            Assert.AreEqual(testCompany, records.First().CompanyName);
        }
    }
}
