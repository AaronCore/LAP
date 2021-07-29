using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LAP.Web.Filters;

namespace LAP.Web.Controllers
{
    [ExceptionFilter]
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
