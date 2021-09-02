using System.Collections.Generic;

namespace LAP.EntityFrameworkCore.Model
{
    public class TableModel
    {
        public List<string> Columns { get; set; } = new();
        public List<List<string>> Rows { get; set; } = new();
    }
    public class TableTreeModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public List<TreeModel> children { get; set; }
    }
    public class TableSchemaModel
    {
        public string TABLE_SCHEMA { get; set; }
        public string TABLE_NAME { get; set; }
        public string TABLE_COMMENT { get; set; }
        public string TABLE_ROWS { get; set; }
    }
    public class TableColumnModel
    {
        public string COLUMN_NAME { get; set; }
        public string COLUMN_COMMENT { get; set; }
    }
    public class TreeModel
    {
        public string id { get; set; }
        public string name { get; set; }
    }
}
