using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LAP.EntityFrameworkCore.Application;
using LAP.Web.Filters;

namespace LAP.Web.Controllers
{
    [ExceptionFilter]
    public class LoggerController : Controller
    {
        private static readonly LogService LogService = new();

        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details()
        {
            return View();
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageIndex">分页下标</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="moduleCode">模块代码</param>
        /// <param name="logLevel">日志等级</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Load(int pageIndex, int pageSize, int moduleCode, int logLevel)
        {
            var query = await LogService.PageQuery(pageIndex, pageSize, moduleCode, logLevel);
            var obj = new
            {
                pageIndex,
                pageSize,
                query.total,
                data = query.dataList.Select(p => new
                {
                    p.id,
                    p.module_code,
                    p.module_name,
                    p.level,
                    p.request_url,
                    p.request_body,
                    p.method,
                    p.exception,
                    p.message,
                    p.ip_address,
                    p.remark,
                    log_create_time = p.log_create_time.ToString("yyyy-MM-dd HH:mm:ss"),
                    created_time = p.created_time.ToString("yyyy-MM-dd HH:mm:ss")
                }).ToList()
            };
            return Json(obj);
        }

    }
}
