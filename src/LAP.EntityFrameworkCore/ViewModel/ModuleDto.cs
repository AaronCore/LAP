namespace LAP.EntityFrameworkCore.ViewModel
{
    /// <summary>
    /// 日志输入模型
    /// </summary>
    public class ModuleInputDto
    {
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
    }
}
