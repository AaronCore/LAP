using System;
using System.ComponentModel.DataAnnotations;

namespace LAP.EntityFrameworkCore.ViewModel
{
    /// <summary>
    /// 统计日志模型
    /// </summary>
    public class StatisticLogDto
    {
        /// <summary>
        /// 主键id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 模块代码
        /// </summary>
        public int module_code { get; set; }
        /// <summary>
        /// 模块名称
        /// </summary>
        public string module_name { get; set; }
        /// <summary>
        /// 请求页面
        /// </summary>
        public string request_page { get; set; }
        /// <summary>
        /// 执行动作,例如：登录、登出...
        /// </summary>
        public int action { get; set; }
        /// <summary>
        /// 请求地址
        /// </summary>
        public string request_url { get; set; }
        /// <summary>
        /// 说明信息
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// 请求时间
        /// </summary>
        public DateTime request_time { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime created_time { get; set; }
    }
    /// <summary>
    /// 统计日志输入模型
    /// </summary>
    public class StatisticLogInputDto
    {
        /// <summary>
        /// 模块代码
        /// </summary>
        [Required]
        public int module_code { get; set; }
        /// <summary>
        /// 请求页面
        /// </summary>
        [Required]
        public string request_page { get; set; }
        /// <summary>
        /// 执行动作,例如：登录、登出...
        /// </summary>
        [Required]
        public int action { get; set; }
        /// <summary>
        /// 请求地址
        /// </summary>
        [Required]
        public string request_url { get; set; }
        /// <summary>
        /// 说明信息
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// 请求时间
        /// </summary>
        [Required]
        public DateTime request_time { get; set; }
    }
}
