using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LAP.EntityFrameworkCore.Application;
using LAP.EntityFrameworkCore.ViewModel;
using LAP.EntityFrameworkCore.Enum;
using LAP.Client.Extensions;
using LAP.Common;

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
                await StatisticLogService.Inster(dto);
            }
            catch (Exception)
            {
                // 故障转移
                await RabbitMQMessage.Send(RabbitMQMessageType.请求日志, dto.ToJson());
            }

            return Ok();
        }
    }
}
