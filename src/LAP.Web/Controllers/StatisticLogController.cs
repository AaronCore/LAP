using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LAP.EntityFrameworkCore.Application;
using LAP.EntityFrameworkCore.Enum;
using LAP.Web.Filters;

namespace LAP.Web.Controllers
{
    [ExceptionFilter]
    public class StatisticLogController : Controller
    {
        private static readonly StatisticLogService StatisticLogService = new();
        private static readonly ModuleService ModuleService = new();

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
        /// <param name="searchKey">查询条件</param>
        /// <param name="moduleCode">模块代码</param>
        /// <param name="logLevel">日志等级</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Load(int pageIndex, int pageSize, string searchKey, int moduleCode, string startDate, string endDate)
        {
            var query = await StatisticLogService.PageQuery(pageIndex, pageSize, searchKey, moduleCode, startDate, endDate);
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
                    p.request_page,
                    action = Enum.GetName(typeof(StatisticAction), p.action),
                    p.request_url,
                    p.message,
                    request_time = p.request_time.ToString("yyyy-MM-dd HH:mm:ss"),
                    created_time = p.created_time.ToString("yyyy-MM-dd HH:mm:ss")
                }).ToList()
            };
            return Json(obj);
        }


        /// <summary>
        /// 获取日志信息
        /// </summary>
        /// <param name="id">主键id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetStatisticLog(int id)
        {
            var model = await StatisticLogService.Find(id);
            var obj = new
            {
                model.id,
                model.module_code,
                model.module_name,
                model.request_page,
                action = Enum.GetName(typeof(StatisticAction), model.action),
                model.request_url,
                model.message,
                request_time = model.request_time.ToString("yyyy-MM-dd HH:mm:ss"),
                created_time = model.created_time.ToString("yyyy-MM-dd HH:mm:ss")
            };
            return Json(obj);
        }
    }
}
