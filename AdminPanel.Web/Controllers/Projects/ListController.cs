using AdminPanel.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AdminPanel.Web.Controllers.Projects
{
    [Route("Projects/[controller]/[action]")]
    public class ListController : Controller
    {
        private readonly IProjectService _projectService;

        public ListController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public async Task<IActionResult> Index()
        {
            var projects = await _projectService.GetAll();
            return View(projects);
        }
    }
}
