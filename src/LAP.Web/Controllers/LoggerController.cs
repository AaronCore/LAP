using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Load(int pageIndex, int pageSize, string searchKey, int moduleCode, int logLevel, string startDate, string endDate)
        {
            var query = await LogService.PageQuery(pageIndex, pageSize, searchKey, moduleCode, logLevel, startDate, endDate);
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
        public async Task<IActionResult> GetLog(string id)
        {
            var model = await LogService.Find(id);
            var obj = new
            {
                model.id,
                model.module_code,
                model.module_name,
                model.level,
                model.request_path,
                model.request_url,
                model.request_form,
                method = model.method.ToUpper(),
                model.exception,
                model.message,
                model.ip_address,
                model.remark,
                log_create_time = model.log_create_time.ToString("yyyy-MM-dd HH:mm:ss"),
                created_time = model.created_time.ToString("yyyy-MM-dd HH:mm:ss")
            };
            return Json(obj);
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
