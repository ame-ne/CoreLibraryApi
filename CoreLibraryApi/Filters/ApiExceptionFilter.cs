using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CoreLibraryApi.Filters
{
    public class ApiExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var e = context.Exception;
            while (e.InnerException != null)
            {
                e = e.InnerException;
            }
            context.Result = new BadRequestObjectResult(new { errorText = e.Message });
            context.ExceptionHandled = true;            
        }
    }
}
