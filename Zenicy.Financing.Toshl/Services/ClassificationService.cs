using System.Collections.Generic;
using System.Linq;
using Zenicy.Financing.Core.Models;
using Zenicy.Financing.Core.Services;

namespace Zenicy.Financing.Toshl.Services
{
    public class ClassificationService : IClassificationService
    {
        public void Classify(IEnumerable<Transaction> transactions, IEnumerable<CategoryDefinition> categoryDefs, IEnumerable<TagDefinition> tagDefs, Category undefined = null)
        {
            foreach (var transaction in transactions)
            {
                var tags = transaction.Tags?.ToList() ?? new List<Tag>();
                tags.AddRange(
                    tagDefs
                    .Where(t => t.Includes.Any(i => transaction.Description.Contains(i)))
                    .Where(t => t.Excludes.All(e => !transaction.Description.Contains(e)))
                    .Select(t => t.Tag)
                    .ToArray());

                transaction.Tags = tags.GroupBy(t => t.Id).Select(t => t.FirstOrDefault());
                transaction.Category = categoryDefs
                    .Where(t => t.Includes.Any(i => transaction.Description.Contains(i)))
                    .Where(t => t.Excludes.All(e => !transaction.Description.Contains(e)))
                    .FirstOrDefault()
                    ?.Category
                    ?? undefined;
            }
        }
    }
}
