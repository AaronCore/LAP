using System;
using System.ComponentModel.DataAnnotations;

namespace LAP.EntityFrameworkCore.ViewModel
{
    /// <summary>
    /// 日志输入模型
    /// </summary>
    public class LogDto
    {
        /// <summary>
        /// 主键
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
        /// 日志等级
        /// </summary>
        public int level { get; set; }
        /// <summary>
        /// 请求路径
        /// </summary>
        public string request_path { get; set; }
        /// <summary>
        /// 请求地址
        /// </summary>
        public string request_url { get; set; }
        /// <summary>
        /// 请求内容
        /// </summary>
        public string request_body { get; set; }
        /// <summary>
        /// 请求方式(get、post、put、delete...)
        /// </summary>
        public string method { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string exception { get; set; }
        /// <summary>
        /// 日志信息
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
        public DateTime log_create_time { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime created_time { get; set; }
    }
    /// <summary>
    /// 日志输入模型
    /// </summary>
    public class LogInputDto
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
        public int level { get; set; }
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
