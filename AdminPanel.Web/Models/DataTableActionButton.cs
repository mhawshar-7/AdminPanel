namespace AdminPanel.Web.Models
{
    public class DataTableActionButton
    {
        public string Text { get; set; } = string.Empty;
        public string CssClass { get; set; } = "btn btn-sm btn-light";
        public string Icon { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string? Controller { get; set; }
        public string? Action { get; set; }
        public string? RouteParameter { get; set; } = "id"; // Default route parameter name
        public bool ConfirmAction { get; set; } = false;
        public string? ConfirmMessage { get; set; }
        public DataTableActionButtonType Type { get; set; } = DataTableActionButtonType.Link;
    }

    public enum DataTableActionButtonType
    {
        Link,
        Button,
        Modal
    }
}