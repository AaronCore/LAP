using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LAP.EntityFrameworkCore.Application;

namespace LAP.Web.Controllers
{
    public class ModuleController : Controller
    {
        private readonly ModuleService _moduleService;
        public ModuleController(ModuleService moduleService)
        {
            _moduleService = moduleService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
