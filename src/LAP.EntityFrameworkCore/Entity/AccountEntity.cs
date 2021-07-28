namespace LAP.EntityFrameworkCore.Entity
{
    /// <summary>
    /// LAP账号表
    /// </summary>
    public class AccountEntity
    {
        /// <summary>
        /// 主键id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string password { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool enabled { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string created_by { get; set; }
        /// <summary>
        /// 创建实际
        /// </summary>
        public string created_time { get; set; }
    }
}
