using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LAP.EntityFrameworkCore.Application;
using LAP.EntityFrameworkCore.ViewModel;
using LAP.EntityFrameworkCore.Enum;
using LAP.Client.Extensions;
using LAP.Client.Filters;
using LAP.Common;

namespace LAP.Client.Controllers
{
    [Route("api/log")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private static readonly LogService LogService = new();

        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="dto">日志输入模型</param>
        /// <returns></returns>
        [HttpPost("addlog")]
        [ActionFilter]
        public async Task<IActionResult> AddLog([FromBody] LogInputDto dto)
        {
            if (dto == null)
            {
                return NotFound();
            }

            try
            {
                await LogService.Inster(dto);
            }
            catch (Exception)
            {
                // 故障转移
                await RabbitMQMessage.Send(RabbitMQMessageType.日志, dto.ToJson());
            }

            return Ok();
        }
    }
}
