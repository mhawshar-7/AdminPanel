using AdminPanel.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Web.ViewComponents
{
    public class DashboardOverviewViewComponent : ViewComponent
    {
        private readonly IProjectService _projectService;

        public DashboardOverviewViewComponent(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var projectCount = await _projectService.Count();
            return View(projectCount);
        }
    }
}
