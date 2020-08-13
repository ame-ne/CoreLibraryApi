using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.Extensions.DependencyInjection;
using CoreLibraryApi.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Linq;
using CoreLibraryApi.Attributes;

namespace CoreLibraryApi.Filters
{
    public class ApiActionFilter : IActionFilter
    {
        private Log _logRecord;
        private bool _skipLogging = false;

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (_skipLogging)
            {
                return;
            }

            if (_logRecord != null)
            {
                var _logger = context.HttpContext.RequestServices.GetService<ILogger>();
                _logRecord.ActionResult = Infrastructure.LogActionEnum.Success;
                _logger.Log(LogLevel.Information, default, _logRecord, null, null);
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _skipLogging = false;
            var descriptor = (ControllerActionDescriptor)context.ActionDescriptor;
            var attributes = descriptor.MethodInfo.CustomAttributes;
            if (attributes.Any(a => a.AttributeType == typeof(IgnoreLogging)))
            {
                _skipLogging = true;
                return;
            }

            var httpContext = context.HttpContext;
            _logRecord = new Log
            {
                ActionBy = httpContext.User.Identity.IsAuthenticated
                ? httpContext.User.FindFirstValue(ClaimsIdentity.DefaultNameClaimType)
                : "anonymous",
                ActionDate = DateTime.UtcNow,
                Url = httpContext.Request.GetDisplayUrl()
            };
        }
    }
}
