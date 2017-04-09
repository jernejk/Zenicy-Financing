using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Zenicy.Financing.Core.Models;
using Zenicy.Financing.Core.Services;
using Zenicy.Financing.Toshl.Services;

namespace Zenicy.Financing.Toshl.Test.Import
{
    [TestClass]
    public class ClassificationServiceTest
    {
        [TestMethod]
        public void Test()
        {
            var transactions = new List<Transaction>();
            transactions.Add(new Transaction(new DateTime(2017, 4, 7), 7777.77m, "PAY/SALARY FROM SUPERIOR SOFTWAR FORTNIGHTLY SALARY"));
            transactions.Add(new Transaction(new DateTime(2017, 4, 5), -1000m, "ANZ M-BANKING PAYMENT TRANSFER 1234 TO LAND LORD"));
            transactions.Add(new Transaction(new DateTime(2017, 4, 1), -10m, "MISC thing"));

            List<TagDefinition> tagDefs = new List<TagDefinition>();
            tagDefs.Add(new TagDefinition("ssw", "SSW", new[] { "FROM SUPERIOR SOFTWAR" }, new string[0]));
            tagDefs.Add(new TagDefinition("salary", "Salary", new[] { "SALARY" }, new string[0]));
            tagDefs.Add(new TagDefinition("rent", "Rent", new[] { "LAND LORD" }, new string[0]));
            tagDefs.Add(new TagDefinition("land-lord", "Land lord", new[] { "LAND LORD" }, new string[0]));

            List<CategoryDefinition> categoryDefs = new List<CategoryDefinition>();
            categoryDefs.Add(new CategoryDefinition("salary", "Salary", new[] { "FROM SUPERIOR SOFTWAR" }, new string[0]));
            categoryDefs.Add(new CategoryDefinition("home", "Home and Utillities", new[] { "LAND LORD" }, new string[0]));

            var undefined = new Category("xyz", "XYZ");

            IClassificationService service = new ClassificationService();
            service.Classify(transactions, categoryDefs, tagDefs, undefined);

            Assert.IsTrue(transactions[0].Tags.Any(t => t.Id == "ssw"));
            Assert.IsTrue(transactions[0].Tags.Any(t => t.Id == "salary"));
            Assert.IsTrue(transactions[1].Tags.Any(t => t.Id == "rent"));
            Assert.IsNotNull(transactions[0].Category);
            Assert.IsNotNull(transactions[1].Category);
            Assert.IsNotNull(transactions[2].Category);
            Assert.AreEqual("salary", transactions[0].Category.Id);
            Assert.AreEqual("home", transactions[1].Category.Id);
            Assert.AreEqual("xyz", transactions[2].Category.Id);
        }
    }
}
