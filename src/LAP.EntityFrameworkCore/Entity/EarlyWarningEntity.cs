using System;

namespace LAP.EntityFrameworkCore.Entity
{
    /// <summary>
    /// 预警实体
    /// </summary>
    public class EarlyWarningEntity
    {
        /// <summary>
        /// 主键id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// host地址
        /// </summary>
        public string host { get; set; }
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
        /// 负责人
        /// </summary>
        public string principal { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime created_time { get; set; }
    }
}
