using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Web.CacheOptions
{
    public static class CacheTimes
    {
        public static int BooksCacheTime => 10;
        public static int BookCacheTime => 5;
        public static int BasketCacheTime => 10;
        public static int OrdersCacheTime => 15;
        public static int CategoriesCacheTime => 10;
        public static int CategoryCacheTime => 15;
        public static int CommentsCacheTime => 15;
        public static int BooksByCategoryCacheTime => 10;
        public static int AuthorCacheTime => 15;
    }
}