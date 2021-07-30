using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LAP.EntityFrameworkCore.Application;
using LAP.EntityFrameworkCore.Enum;
using LAP.EntityFrameworkCore.ViewModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace LAP.Web.MiddlerWare
{
    public static class RequestLogMiddlerWareExtension
    {
        public static IApplicationBuilder UseRequestLog(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestLogMiddleWare>();
        }
    }

    public class RequestLogMiddleWare
    {
        private static readonly StatisticLogService StatisticLogService = new();
        private RequestDelegate _next;

        public RequestLogMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            //if (context.Request.Path != "/")
            //{
            //    var requestModel = new StatisticLogInputDto()
            //    {
            //        module_code = 101,
            //        request_page = context.Request.Path,
            //        action = (int)StatisticLogAction.页面访问,
            //        request_url = (context.Request.Path + context.Request.QueryString).ToLower(),
            //        request_time = DateTime.Now
            //    };
            //    await StatisticLogService.InsterStatisticLog(requestModel);
            //}
            // Call the next delegate/middleware in the pipeline
            await this._next(context);
        }
    }
}
