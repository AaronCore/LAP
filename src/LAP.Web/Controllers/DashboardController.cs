using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public async Task<IActionResult> GetLogChart(string startDate, string endDate)
        {
            var model = await DashboardService.LogChart(startDate, endDate);
            return Json(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetStatisticLogChart(string startDate, string endDate)
        {
            var model = await DashboardService.StatisticLogChart(startDate, endDate);
            return Json(model);
        }
    }
}
