using Newtonsoft.Json;
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
            string json = File.ReadAllText("importSettings.json");
            var settings = JsonConvert.DeserializeObject<ImportSettings>(json);

            decimal total = 0;
            foreach (var file in settings.Files)
            {
                System.Console.WriteLine($"File {file}");
                System.Console.WriteLine("--------------------------------------------");
                using (var stream = File.OpenRead(file))
                {
                    IImportService importService = new ImportService();
                    var result = importService.Import(stream, "csv", "ANZ");

                    IClassificationService classificationService = new ClassificationService();
                    classificationService.Classify(result.Transactions, settings.Categories, settings.Tags);

                    decimal subtotal = 0;
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

                        subtotal += t.Amount;
                    }

                    total += subtotal;

                    System.Console.WriteLine("-------------------");
                    System.Console.WriteLine("Total: ${0}", subtotal);
                    System.Console.WriteLine();
                }

            }

            System.Console.WriteLine("Full total: ${0}", total);

            if (System.Diagnostics.Debugger.IsAttached)
            {
                System.Console.WriteLine("Press any key to continue...");
                System.Console.ReadLine();
            }
        }
    }
}
