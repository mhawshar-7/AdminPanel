using AdminPanel.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Web.ViewComponents
{
    public class DataTableViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(DataTableConfiguration config)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            // Ensure required properties are set
            if (string.IsNullOrEmpty(config.Id))
            {
                config.Id = "dataTable_" + Guid.NewGuid().ToString("N")[..8];
            }

            // Add actions column if action buttons are defined
            if (config.ActionButtons.Any())
            {
                var actionsColumn = config.Columns.FirstOrDefault(c => c.Name.Equals("actions", StringComparison.OrdinalIgnoreCase));
                if (actionsColumn == null)
                {
                    config.Columns.Add(new DataTableColumn
                    {
                        Name = "actions",
                        Title = "Actions",
                        Sortable = false,
                        Searchable = false,
                        ClassName = "text-end min-w-70px",
                        Type = DataTableColumnType.Html
                    });
                }
            }

            return View(config);
        }
    }
}