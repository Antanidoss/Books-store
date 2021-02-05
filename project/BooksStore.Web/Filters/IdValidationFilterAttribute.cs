using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Web.Filters
{
    public class IdValidationFilterAttribute : ActionFilterAttribute
    {
        private readonly string _idName;
        public IdValidationFilterAttribute(string idName)
        {
            _idName = idName;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var parameterExists = context.ActionArguments.TryGetValue(_idName, out object id);

            if (parameterExists)
            {
                if (string.IsNullOrEmpty(id.ToString()))
                {
                    context.Result = new StatusCodeResult(404);
                    return;
                }
            }           
        }
    }
}
