using AdminPanel.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Web.ViewComponents
{
    public class ProjectListViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(IEnumerable<ProjectViewModel> projects)
        {
            return View(projects);
        }   
    }
}
