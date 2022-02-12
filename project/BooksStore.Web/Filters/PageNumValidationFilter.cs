using System;
using BooksStore.Common.Constants;
using BooksStore.Web.Сommon.Pagination;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BooksStore.Web.Filters
{
    public class PageNumValidationFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var parameterExists = context.ActionArguments.TryGetValue("pageNum", out object pageNum);

            if (parameterExists && !PaginationInfo.PageNumberIsValid((int)pageNum))
            {
                throw new ArgumentException(ExceptionMessages.InvalidPageNum);
            }
        }
    }
}