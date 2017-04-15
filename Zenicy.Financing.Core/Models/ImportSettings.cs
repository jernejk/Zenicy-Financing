using System.Collections.Generic;

namespace Zenicy.Financing.Core.Models
{
    public class ImportSettings
    {
        public IEnumerable<TagDefinition> Tags { get; set; }

        public IEnumerable<CategoryDefinition> Categories { get; set; }

        public string Format { get; set; }

        public string Source { get; set; }

        public string[] Files { get; set; }
    }
}
