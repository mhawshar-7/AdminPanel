namespace AdminPanel.Web.Models.DataTable
{
    public class DataTableColumn
    {
        public string Name { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public bool Visible { get; set; } = true;
        public bool Sortable { get; set; } = true;
        public bool Searchable { get; set; } = true;
        public string? Width { get; set; }
        public string? ClassName { get; set; }
        public string? Render { get; set; }
        public DataTableColumnType Type { get; set; } = DataTableColumnType.String;
    }

    public enum DataTableColumnType
    {
        String,
        Number,
        Date,
        Html,
        Custom
    }
}