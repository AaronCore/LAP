using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LAP.EntityFrameworkCore.Application;
using LAP.EntityFrameworkCore.Entity;
using LAP.EntityFrameworkCore.ViewModel;

namespace LAP.Web.Controllers
{
    public class ModuleController : Controller
    {
        private static readonly ModuleService ModuleService = new ModuleService();

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 分页接口
        /// </summary>
        /// <param name="pageIndex">分页下标</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="searchKey">查询条件</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Load(int pageIndex, int pageSize, string searchKey = null)
        {
            var query = await ModuleService.PageQuery(pageIndex, pageSize, searchKey);
            var obj = new
            {
                pageIndex,
                pageSize,
                query.total,
                data = query.dataList.Select(p => new
                {
                    p.id,
                    p.name,
                    p.code,
                    created_time = p.create_time.ToString("yyyy-MM-dd HH:mm:ss")
                }).ToList()
            };
            return Json(obj);
        }

        /// <summary>
        /// 添加/修改
        /// </summary>
        /// <param name="model">模块实体</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Submit(ModuleEntity model)
        {
            if (await ModuleService.VerifyName(model.id, model.name))
            {
                return Json(-1);
            }
            if (model.id > 0)
            {
                await ModuleService.UpdateModule(model.id, model.name);
            }
            else
            {
                await ModuleService.InsterModule(model);
            }
            return Json(1);
        }
    }
}
