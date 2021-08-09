using System;
using System.ComponentModel.DataAnnotations;
using LAP.HttpClientCore.Enum;

namespace LAP.HttpClientCore.Model
{
    /// <summary>
    /// 添加日志输入实体
    /// </summary>
    public class AddLogModel
    {
        /// <summary>
        /// 模块代码
        /// </summary>
        [Required]
        public int module_code { get; set; }
        /// <summary>
        /// 日志等级
        /// </summary>
        [Required]
        public LogLevel level { get; set; }
        /// <summary>
        /// 请求路径
        /// </summary>
        [Required]
        public string request_path { get; set; }
        /// <summary>
        /// 请求地址
        /// </summary>
        [Required]
        public string request_url { get; set; }
        /// <summary>
        /// 请求内容
        /// </summary>
        public string request_form { get; set; }
        /// <summary>
        /// 请求方式(get、post、put、delete...)
        /// </summary>
        public string method { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string exception { get; set; }
        /// <summary>
        /// 普通信息
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// IP地址
        /// </summary>
        public string ip_address { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 日志创建时间
        /// </summary>
        [Required]
        public DateTime log_create_time { get; set; }
    }
}
