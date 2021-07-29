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
        /// 创建人
        /// </summary>
        public string created_by { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime created_time { get; set; }
    }
}
