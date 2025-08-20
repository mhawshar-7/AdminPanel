namespace AdminPanel.Web.Models.DataTable
{
    public class DataTableConfiguration
    {
        public string Id { get; set; } = "dataTable";
        public string AjaxUrl { get; set; } = string.Empty;
        public List<DataTableColumn> Columns { get; set; } = new();
        public List<DataTableActionButton> ActionButtons { get; set; } = new();
        public bool ServerSide { get; set; } = true;
        public bool Paging { get; set; } = true;
        public bool Searching { get; set; } = true;
        public bool Ordering { get; set; } = true;
        public bool Info { get; set; } = true;
        public int PageLength { get; set; } = 10;
        public bool Responsive { get; set; } = true;
        public string CssClass { get; set; } = "table align-middle table-row-dashed fs-6 gy-5";
        public bool ShowCheckboxes { get; set; } = false;
        public string? EmptyTableMessage { get; set; } = "No data available in table";
        public Dictionary<string, object> AdditionalOptions { get; set; } = new();
        
        // Header customization
        public string? Title { get; set; }
        public bool ShowSearchBox { get; set; } = true;
        public string? SearchPlaceholder { get; set; } = "Search...";
        public List<DataTableActionButton> HeaderButtons { get; set; } = new();
        
        // Card wrapper settings
        public bool WrapInCard { get; set; } = true;
        public string CardCssClass { get; set; } = "card card-flush";
    }
}