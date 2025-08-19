using AdminPanel.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Web.ViewComponents
{
    public class SidebarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
