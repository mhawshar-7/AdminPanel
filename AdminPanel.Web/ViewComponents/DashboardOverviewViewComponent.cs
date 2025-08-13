using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Web.ViewComponents
{
    public class DashboardOverviewViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
