using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LAP.EntityFrameworkCore.ViewModel;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LAP.Client.Filters
{
    public class ActionFilter : ActionFilterAttribute
    {
        /// <summary>
        /// OnActionExecuting
        /// </summary>
        /// <param name="context"></param>
        public override async void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            try
            {
                // 日志提醒通知
                if (context.ActionArguments["dto"] is LogInputDto dto)
                    await LogNotice.Notice(dto.module_code, dto.level);
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
