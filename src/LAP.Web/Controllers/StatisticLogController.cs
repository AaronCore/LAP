﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LAP.Web.Controllers
{
    public class StatisticLogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
