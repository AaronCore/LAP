using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LAP.EntityFrameworkCore.Application;
using LAP.EntityFrameworkCore.Entity;
using LAP.EntityFrameworkCore.ViewModel;
using LAP.Web.Filters;

namespace LAP.Web.Controllers
{
    [ExceptionFilter]
    public class ModuleController : Controller
    {
        private static readonly ModuleService ModuleService = new();

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
                    created_time = p.created_time.ToString("yyyy-MM-dd HH:mm:ss")
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
                if (!await ModuleService.UpdateModule(model.id, model.name))
                {
                    return Json(0);
                }
            }
            else
            {
                model.created_by = "admin";
                if (!await ModuleService.InsterModule(model))
                {
                    return Json(0);
                }
            }
            return Json(1);
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
        /// 获取模块信息
        /// </summary>
        /// <param name="id">主键id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetModule(int id)
        {
            var model = await ModuleService.Find(id);
            return Json(model);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids">主键ids</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Deletes(string[] ids)
        {
            foreach (var id in ids)
            {
                if (string.IsNullOrWhiteSpace(id)) continue;
                await ModuleService.DeleteModule(Convert.ToInt32(id));
            }
            return Json(1);
        }
    }
}
