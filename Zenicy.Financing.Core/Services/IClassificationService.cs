using System.Collections.Generic;
using Zenicy.Financing.Core.Models;

namespace Zenicy.Financing.Core.Services
{
    public interface IClassificationService
    {
        void Classify(IEnumerable<Transaction> transactions, IEnumerable<CategoryDefinition> categories, IEnumerable<TagDefinition> tags, Category undefined = null);
    }
}
