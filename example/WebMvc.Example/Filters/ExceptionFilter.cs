using System.Threading.Tasks;
using System.Web.Mvc;
using LAP.HttpClient.Enum;

namespace WebMvc.Example.Filters
{
    public class MyExceptionFilter : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);
            var httpContext = context.HttpContext;
            Task.Run(async () => await LapLogger.AddLog(httpContext, LogLevel.Error));
        }
    }
}
