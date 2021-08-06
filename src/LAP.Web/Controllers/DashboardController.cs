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
    public class DashboardController : Controller
    {
        private static readonly DashboardService DashboardService = new();

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetStatistics()
        {
            var model = await DashboardService.Statistics();
            return Json(model);
        }
    }
}
