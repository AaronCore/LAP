using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LAP.EntityFrameworkCore.Application;
using LAP.EntityFrameworkCore.ViewModel;

namespace LAP.Client.Controllers
{
    [ApiController]
    [Route("api/log")]
    public class LogController : ControllerBase
    {
        private static readonly LogService LogService = new LogService();

        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="dto">日志输入模型</param>
        /// <returns></returns>
        [HttpPost("addlog")]
        public async Task<IActionResult> AddLog(LogInputDto dto)
        {
            if (dto == null)
            {
                return NotFound();
            }

            await LogService.InsterLog(dto);

            return Ok();
        }
    }
}
