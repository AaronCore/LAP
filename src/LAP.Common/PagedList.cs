using System.Collections.Generic;

namespace LAP.Common
{
    public partial class PagedList<T>
    {
        /// <summary>
        /// 查询结果
        /// </summary>
        public IEnumerable<T> dataList { get; set; }
        /// <summary>
        /// 分页总数
        /// </summary>
        public int pageTotal { get; set; }
        /// <summary>
        /// 总数
        /// </summary>
        public int total { get; set; }
    }
}
