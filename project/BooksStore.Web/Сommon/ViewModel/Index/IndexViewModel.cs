using BooksStore.Web.Сommon.Pagination;
using System.Collections.Generic;
using System.Linq;

namespace BooksStore.Web.Сommon.ViewModel.Index
{
    public class IndexViewModel<T> where T : class
    {
        public List<T> Objects { get; set; }
        public PaginationInfo PageInfo { get; set; }

        public IndexViewModel() { }

        public IndexViewModel(int pageNum, int pageSize, int totalItems, IEnumerable<T> objects)
        {
            if (objects != null && objects.Count() != 0)
            {
                Objects = objects.ToList();

                PageInfo = new PaginationInfo()
                {
                    PageNumber = pageNum,
                    PageSize = pageSize,
                    TotalItems = totalItems
                };
            }
            else
            {
                Objects = new List<T>();
                PageInfo = new PaginationInfo();
            }            
        }
    }
}