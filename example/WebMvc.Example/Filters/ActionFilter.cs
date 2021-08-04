using System.Threading.Tasks;
using System.Web.Mvc;
using LAP.HttpClient.Enum;

namespace WebMvc.Example.Filters
{
    public class ActionFilter : ActionFilterAttribute
    {
        public virtual void OnActionExecuting(ActionExecutedContext context)
        {
            var httpContext = context.HttpContext;
            Task.Run(async () => await LapLogger.AddStatisticLog(httpContext, StatisticAction.页面访问));
        }
    }
}