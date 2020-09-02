using BooksStore.Web.Models.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Web.Models.ViewModel.Index
{
    public class IndexViewModel<T> where T : class
    {
        public List<T> Objects { get; set; }
        public PageInfo PageInfo { get; set; }

        public IndexViewModel(int pageNum, int pageSize, int totalItems, IEnumerable<T> objects)
        {
            if (objects != null && objects.Count() != 0)
            {
                Objects = objects.ToList<T>();

                PageInfo = new PageInfo()
                {
                    PageNumber = pageNum,
                    PageSize = pageSize,
                    TotalItems = totalItems
                };
            }
            else
            {
                Objects = new List<T>();
                PageInfo = new PageInfo();
            }            
        }

        public IndexViewModel() { }
    }
}
