using System.Collections.Generic;
using System.Linq;

namespace BooksStore.Service.Models
{
    public class BookFilterModel
    {
        public string BookName { get; set; }
        public decimal BookPriceFrom { get; set; }
        public decimal BookPriceTo { get; set; }
        public List<int> CategoryIds { get; set; }

        public bool FilterIsNull()
        {
            return string.IsNullOrEmpty(BookName) && BookPriceFrom == default && BookPriceTo == default && (CategoryIds == null || !CategoryIds.Any());
        }
    }
}