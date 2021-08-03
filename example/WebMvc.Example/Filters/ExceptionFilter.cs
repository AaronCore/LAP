using LAP.HttpClient.Enum;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WebMvc.Example.Filters
{
    public class MyExceptionFilter : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);
            var httpContext = context.HttpContext;
            Task.Run(async () => await MyLogger.AddLog(httpContext, LogLevel.Error));
        }
    }
}
