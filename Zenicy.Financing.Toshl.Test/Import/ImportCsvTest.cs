using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using Zenicy.Financing.Core.Services;
using Zenicy.Financing.Toshl.Services;

namespace Zenicy.Financing.Toshl.Test.Import
{
    [TestClass]
    public class ImportCsvTest
    {
        [TestMethod]
        public void ImportData()
        {
            IImportService service = new ImportService();

            using (Stream stream = File.OpenRead("./TestData/ANZ.csv"))
            {
                Assert.IsTrue(stream.Length > 0);

                var result = service.Import(stream, "csv", "ANZ");

                Assert.IsTrue(result.Success);
                Assert.IsNotNull(result.Transactions);

                var transactions = result.Transactions.ToArray();
                Assert.AreEqual(3, transactions.Length);
                Assert.AreEqual(7777.77m, transactions[0].Amount);
                Assert.AreEqual("PAY/SALARY FROM SUPERIOR SOFTWAR FORTNIGHTLY SALARY", transactions[0].Description);
                Assert.AreEqual(new DateTime(2017, 4, 7), transactions[0].Date);
            }
        }
    }
}
