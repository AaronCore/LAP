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
    [Route("api/log")]
    public class LogController : ControllerBase
    {
        private static readonly LogService LogService = new();

        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="dto">日志输入模型</param>
        /// <returns></returns>
        [HttpPost("addlog")]
        public async Task<IActionResult> AddLog([FromBody] LogInputDto dto)
        {
            if (dto == null)
            {
                return NotFound();
            }

            try
            {
                await LogService.InsterLog(dto);
            }
            catch (Exception)
            {
                // 故障转移
                await SendMessage.Send(MessageType.日志, dto.ToJson());
            }

            return Ok();
        }
    }
}
