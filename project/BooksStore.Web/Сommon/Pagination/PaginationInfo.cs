using System;

namespace BooksStore.Web.Сommon.Pagination
{
    public class PaginationInfo
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

        public static int GetCountTakeItems(int pageNumber, int countPageItems)
        {
            return (pageNumber - 1) * countPageItems;
        }
    }
}
