using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Web.Models.Pagination
{
    public class PageInfo
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPage 
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / PageSize); }
        }

        public static bool PageNumberIsValid(int pageNumber)
        {
            return pageNumber >= 1;
        }
    }
}
