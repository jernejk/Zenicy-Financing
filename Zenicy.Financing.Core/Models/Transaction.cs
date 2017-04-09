using System;
using System.Collections.Generic;

namespace Zenicy.Financing.Core.Models
{
    public class Transaction
    {
        public Transaction() { }

        public Transaction(DateTime date, decimal amount, string description)
        {
            Date = date;
            Amount = amount;
            Description = description;
        }

        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public IEnumerable<Tag> Tags { get; set; } = new Tag[0];
    }
}
