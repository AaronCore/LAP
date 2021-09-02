using System;

namespace LAP.EntityFrameworkCore.Entity
{
    /// <summary>
    /// 模块实体
    /// </summary>
    public class ModuleEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 模块名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 模块代码
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 是否开启提醒通知
        /// </summary>
        public bool is_notice { get; set; }
        /// <summary>
        /// 日志等级
        /// </summary>
        public string log_level { get; set; }
        /// <summary>
        /// 通知方式，1-邮件，2-短信
        /// </summary>
        public int notice_way { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string mobile { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string created_by { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime created_time { get; set; }
    }
}
