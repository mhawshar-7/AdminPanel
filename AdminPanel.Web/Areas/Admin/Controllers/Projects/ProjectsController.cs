using AdminPanel.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Web.Areas.Admin.Controllers.Projects
{
    [Area("Admin")]
    public class ProjectsController : Controller
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }
        public async Task<IActionResult> Index()
        {
            var projects = await _projectService.GetAll();
            return View(projects);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            return View();
        }
    }
}
