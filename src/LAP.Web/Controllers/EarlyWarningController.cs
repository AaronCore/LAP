using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LAP.EntityFrameworkCore.Application;
using LAP.EntityFrameworkCore.Entity;

namespace LAP.Web.Controllers
{
    public class EarlyWarningController : Controller
    {
        private static readonly EarlyWarningService EarlyWarningService = new();

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
            var query = await EarlyWarningService.PageQuery(pageIndex, pageSize, searchKey);
            var obj = new
            {
                pageIndex,
                pageSize,
                query.total,
                data = query.dataList.Select(p => new
                {
                    p.id,
                    p.name,
                    p.host,
                    p.notice_way,
                    p.email,
                    p.mobile,
                    p.principal,
                    status = p.status ?? -1,
                    p.remark,
                    created_time = p.created_time.ToString("yyyy-MM-dd HH:mm:ss")
                }).ToList()
            };
            return Json(obj);
        }

        /// <summary>
        /// 添加/修改
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Submit(EarlyWarningEntity model)
        {
            if (model.id > 0)
            {
                if (!await EarlyWarningService.Update(model))
                {
                    return Json(0);
                }
            }
            else
            {
                if (!await EarlyWarningService.Inster(model))
                {
                    return Json(0);
                }
            }
            return Json(1);
        }

        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="id">主键id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetEarlyWarning(int id)
        {
            var model = await EarlyWarningService.Find(id);
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
                await EarlyWarningService.Delete(Convert.ToInt32(id));
            }
            return Json(1);
        }
    }
}
