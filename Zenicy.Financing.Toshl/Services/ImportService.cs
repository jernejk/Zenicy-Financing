using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Zenicy.Financing.Core.Models;
using Zenicy.Financing.Core.Services;

namespace Zenicy.Financing.Toshl.Services
{
    public class ImportService : IImportService
    {
        public ImportResult Import(Stream stream, string format, string source)
        {
            string text;
            using (MemoryStream ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                byte[] data = ms.ToArray();

                text = Encoding.UTF8.GetString(data);
            }

            List<Transaction> transactions = new List<Transaction>();
            string[] lines = text.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                var columns = line.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                if (columns.Length != 3) continue;

                var transaction = new Transaction();
                transaction.Description = columns[2];

                if (decimal.TryParse(columns[1].Replace("\"", string.Empty), out decimal amount))
                {
                    transaction.Amount = amount;
                }

                if (DateTime.TryParseExact(columns[0], "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out DateTime date))
                {
                    transaction.Date = date;
                }

                transactions.Add(transaction);
            }

            ImportResult result = new ImportResult();
            result.Success = true;
            result.Transactions = transactions;

            return result;
        }
    }
}
