using System.IO;
using Zenicy.Financing.Core.Models;

namespace Zenicy.Financing.Core.Services
{
    public interface IImportService
    {
        ImportResult Import(Stream stream, string format, string source);
    }
}
