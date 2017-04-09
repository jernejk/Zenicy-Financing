using System.Collections.Generic;

namespace Zenicy.Financing.Core.Models
{
    public class ImportResult
    {
        public bool Success { get; set; }

        public IEnumerable<Transaction> Transactions { get; set; }
    }
}
