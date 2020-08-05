using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Web.Cache
{
    public static class CacheTime
    {
        public static int GetBooksCacheTime() => 10;
        public static int GetBookCacheTime() => 5;
        public static int GetBasketCacheTime() => 10;
        public static int GetOrdersCacheTime() => 15;
        public static int GetCategoriesCacheTime() => 10;
        public static int GetCategoryCacheTime() => 15;
        public static int GetCommentsCacheTime() => 15;
        public static int GetBooksByCategoryCacheTime() => 10;
    }
}
