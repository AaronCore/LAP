namespace LAP.EntityFrameworkCore.Entity
{
    /// <summary>
    /// LAP菜单表
    /// </summary>
    public class MenuEntity
    {
        /// <summary>
        /// 主键id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 父级id
        /// </summary>
        public int parent_id { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 菜单地址
        /// </summary>
        public string path { get; set; }
        /// <summary>
        /// 菜单图标
        /// </summary>
        public string icon { get; set; }
        /// <summary>
        /// 菜单排序号
        /// </summary>
        public int sort { get; set; }
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
