using System.Collections.Generic;
using System.Linq;

namespace BooksStore.Service.Models
{
    public class FilterModel
    {
        public string BookName { get; set; }
        public decimal BookPrice { get; set; }
        public IEnumerable<int> CategoryIds { get; set; }

        public bool FilterIsNull()
        {
            return string.IsNullOrEmpty(BookName) && BookPrice == default && (CategoryIds == null || !CategoryIds.Any());
        }
    }
}