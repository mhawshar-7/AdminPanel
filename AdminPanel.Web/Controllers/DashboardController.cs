using AdminPanel.Application.Implementations;
using AdminPanel.Data.Interfaces;
using AdminPanel.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Web.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly IProjectService _projectService;
        private readonly IClientService _clientService;

        public DashboardController(
            ILogger<DashboardController> logger,
            IProjectService projectService,
            IClientService clientService)
        {
            _logger = logger;
            _projectService = projectService;
            _clientService = clientService;
        }

        public async Task<IActionResult> Index()
        {
            var activeCount = await _projectService.Count();
            var deletedCount = await _projectService.CountDeleted();

            var projectStats = new EntityStatsModel
            {
                EntityName = "",
                ActiveCount = activeCount,
                DeletedCount = deletedCount
            };
            return View(projectStats);
        }
        public async Task<IActionResult> Projects()
        {
            var activeCount = await _projectService.Count();
            var deletedCount = await _projectService.CountDeleted();

            var projectStats = new EntityStatsModel
            {
                EntityName = "Projects",
                ActiveCount = activeCount,
                DeletedCount = deletedCount
            };
            return View(projectStats);
        }

        public async Task<IActionResult> Clients()
        {
            var activeCount = await _clientService.Count();
            var deletedCount = await _clientService.CountDeleted();

            var clientStats = new EntityStatsModel
            {
                EntityName = "Clients",
                ActiveCount = activeCount,
                DeletedCount = deletedCount
            };
            return View(clientStats);
        }
    }
}
