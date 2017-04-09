using System.Collections.Generic;
using System.IO;
using System.Linq;
using Zenicy.Financing.Core.Models;
using Zenicy.Financing.Core.Services;
using Zenicy.Financing.Toshl.Services;

namespace Zenicy.Financing.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            List<TagDefinition> tagDefs = new List<TagDefinition>();
            tagDefs.Add(new TagDefinition("ssw", "SSW", new[] { "FROM SUPERIOR SOFTWAR" }, new string[0]));
            tagDefs.Add(new TagDefinition("salary", "Salary", new[] { "SALARY" }, new string[0]));
            tagDefs.Add(new TagDefinition("rent", "Rent", new[] { "LAND LORD" }, new string[0]));
            tagDefs.Add(new TagDefinition("land-lord", "Land lord", new[] { "LAND LORD" }, new string[0]));
            tagDefs.Add(new TagDefinition("pt", "Personal Trainer", new[] { "VICTORIA BRUNSBERG" }, new string[0]));
            tagDefs.Add(new TagDefinition("fitness", "Fitness", new[] { "VICTORIA BRUNSBERG" }, new string[0]));
            tagDefs.Add(new TagDefinition("fitness", "Fitness", new[] { "AUSFIT ANYTIME" }, new string[0]));

            List<CategoryDefinition> categoryDefs = new List<CategoryDefinition>();
            categoryDefs.Add(new CategoryDefinition("salary", "Salary", new[] { "FROM SUPERIOR SOFTWAR" }, new string[0]));
            categoryDefs.Add(new CategoryDefinition("home", "Home and Utillities", new[] { "LAND LORD" }, new string[0]));
            categoryDefs.Add(new CategoryDefinition("fitness", "Fitness", new[] { "VICTORIA BRUNSBERG" }, new string[0]));
            categoryDefs.Add(new CategoryDefinition("fitness", "Fitness", new[] { "AUSFIT ANYTIME" }, new string[0]));

            foreach (var file in new[] { "ANZ.csv", "ANZ (1).csv", "ANZ (2).csv" })
            {
                System.Console.WriteLine($"File {file}");
                using (var stream = File.OpenRead(file))
                {
                    IImportService importService = new ImportService();
                    var result = importService.Import(stream, "csv", "ANZ");

                    IClassificationService classificationService = new ClassificationService();
                    classificationService.Classify(result.Transactions, categoryDefs, tagDefs);

                    foreach (var t in result.Transactions.Where(t => t.Category != null || t.Tags.Any()))
                    {
                        System.Console.WriteLine($"{t.Date.ToString("dd/MM/yyyy")}\t${t.Amount.ToString()}\t{t.Description}");
                        System.Console.WriteLine($"\tCategory: { t.Category?.Name ?? "No category" }");

                        if (t.Tags.Any())
                        {
                            System.Console.WriteLine($"\tTags: {t.Tags?.Select(x => x.Name).Aggregate((a, b) => $"{a}, {b}") ?? "No tags"}");
                        }
                        else
                        {
                            System.Console.WriteLine("\tTags: No tags");
                        }
                    }

                    System.Console.WriteLine();
                }
            }
        }
    }
}
