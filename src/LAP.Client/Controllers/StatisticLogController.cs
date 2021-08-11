using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LAP.Client.Extensions;
using LAP.Common;
using LAP.EntityFrameworkCore.Application;
using LAP.EntityFrameworkCore.Enum;
using LAP.EntityFrameworkCore.ViewModel;

namespace LAP.Client.Controllers
{
    [ApiController]
    [Route("api/statisticlog")]
    public class StatisticLogController : ControllerBase
    {
        private static readonly StatisticLogService StatisticLogService = new();

        /// <summary>
        /// 添加统计日志
        /// </summary>
        /// <param name="dto">统计日志输入模型</param>
        /// <returns></returns>
        [HttpPost("addstatisticlog")]
        public async Task<IActionResult> AddStatisticLog([FromBody] StatisticLogInputDto dto)
        {
            if (dto == null)
            {
                return NotFound();
            }

            try
            {
                await StatisticLogService.InsterStatisticLog(dto);
            }
            catch (Exception)
            {
                // 故障转移
                await SendMessage.Send(MessageType.请求日志, dto.ToJson());
            }

            return Ok();
        }
    }
}
