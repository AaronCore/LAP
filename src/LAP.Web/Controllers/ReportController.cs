using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LAP.Common;
using LAP.EntityFrameworkCore.Application;
using WeihanLi.Npoi;

namespace LAP.Web.Controllers
{
    public class ReportController : Controller
    {
        private static readonly ReportService ReportService = new();
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> TableData()
        {
            var model = await ReportService.TableData();
            return Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> Query(string queryText)
        {
            try
            {
                bool isValidInput = Tools.IsValidInput(queryText);
                if (!isValidInput)
                {
                    return Json(new
                    {
                        code = -1,
                    });
                }

                var result = await ReportService.SqlQuery(queryText);
                return Json(new
                {
                    code = 1,
                    data = result
                });
            }
            catch (Exception e)
            {
                return Json(new
                {
                    code = 0,
                    message = e.Message
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Export(string queryText)
        {
            try
            {
                bool isValidInput = Tools.IsValidInput(queryText);
                if (!isValidInput)
                {
                    return Json(new
                    {
                        code = -1,
                    });
                }

                var dir = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\execl");
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                var dt = await ReportService.Query(queryText);

                var fileName = $"{Guid.NewGuid()}.xlsx";
                dt.ToExcelFile(Path.Combine(dir, fileName));

                return Json(new
                {
                    code = 1,
                    data = fileName
                });
            }
            catch (Exception e)
            {
                return Json(new
                {
                    code = 0,
                    message = e.Message
                });
            }
        }
    }
}
