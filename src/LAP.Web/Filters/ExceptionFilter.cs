using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using LAP.Common;
using LAP.EntityFrameworkCore.Application;
using LAP.EntityFrameworkCore.Enum;
using LAP.EntityFrameworkCore.ViewModel;

namespace LAP.Web.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        private static readonly LogService LogService = new();

        public override async Task OnExceptionAsync(ExceptionContext context)
        {
            var request = context.HttpContext.Request;
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (request.Form.Count > 0)
            {
                foreach (var key in request.Form.Keys)
                {
                    var v = request.Form[key][0];
                    dic.Add(key, v);
                }
            }
            var errorModel = new LogInputDto()
            {
                module_code = 101,
                level = (int)LogLevel.Error,
                request_path = request.Path.ToString(),
                request_url = request.Path + request.QueryString,
                request_form = dic.ToJson(),
                method = request.Method,
                exception = context.Exception.ToString(),
                message = context.Exception.Message,
                ip_address = context.HttpContext.Connection.RemoteIpAddress?.ToString(),
                log_create_time = DateTime.Now,
            };
            await LogService.Inster(errorModel);
            context.ExceptionHandled = true;
            context.HttpContext.Response.StatusCode = 200;
            context.Result = new JsonResult(new { code = -999, message = context.Exception.Message });
        }
    }
}
