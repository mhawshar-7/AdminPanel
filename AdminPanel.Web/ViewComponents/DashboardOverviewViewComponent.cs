using AdminPanel.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Web.ViewComponents
{
    public class DashboardOverviewViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(EntityStatsModel model)
        {
            return View(model);
        }
    }
}
