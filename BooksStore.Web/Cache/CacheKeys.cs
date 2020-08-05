using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Web.Cache
{
    public static class CacheKeys
    {
        public static string GetBookKey(int bookId) => "Book" + bookId;
        public static string GetBasketKey(int basketId) => "Basket" + basketId;
        public static string GetOrdersKey(string userId) => "Orders" + userId;
        public static string GetCategoriesKey(int pageNum) => "IndexCategories" + pageNum;
        public static string GetIndexCategoryKey(int categoryId) => "Category" + categoryId;
        public static string GetCommentsKey(int bookId, int pageNum) => "IndexComments" + bookId + pageNum;
        public static string GetBooksByCategoryKey(int categoryId) => "IndexBooksByCategory" + categoryId;
    }
}
