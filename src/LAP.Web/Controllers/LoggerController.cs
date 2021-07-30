﻿using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using EnumsNET;
using LAP.EntityFrameworkCore.Application;
using LAP.Web.Filters;

namespace LAP.Web.Controllers
{
    [ExceptionFilter]
    public class LoggerController : Controller
    {
        private static readonly LogService LogService = new();
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
        /// <param name="moduleCode">模块代码</param>
        /// <param name="logLevel">日志等级</param>
        /// <param name="searchKey">查询条件</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Load(int pageIndex, int pageSize, int moduleCode, int logLevel, string searchKey)
        {
            var query = await LogService.PageQuery(pageIndex, pageSize, moduleCode, logLevel, searchKey);
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
                    p.request_path,
                    p.request_url,
                    p.request_form,
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

        /// <summary>
        /// 获取日志信息
        /// </summary>
        /// <param name="id">主键id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetLog(int id)
        {
            var model = await LogService.Find(id);
            return Json(model);
        }

        /// <summary>
        /// 获取模块列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetMoudleList()
        {
            var modules = await ModuleService.GetList();
            var result = modules.OrderBy(p => p.name).Select(p => new
            {
                text = p.name,
                value = p.code
            });
            return Json(result);
        }

        /// <summary>
        /// 获取日志等级列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetLogLevelList()
        {
            var members = Enums.GetMembers<EntityFrameworkCore.Enum.LogLevel>();
            var result = members.Select(p => new
            {
                text = p.Name,
                value = p.ToInt32()
            });
            return Json(result);
        }
    }
}