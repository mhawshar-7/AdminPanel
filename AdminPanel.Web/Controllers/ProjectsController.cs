using AdminPanel.Application.Dtos;
using AdminPanel.Data.Interfaces;
using AdminPanel.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Web.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var projects = await _projectService.GetAll();
            var model = projects.Select(s => new ProjectViewModel
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                ModifiedDate = s.CreatedDate?.ToString("dd/MM/yyyy")
            }).ToList();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _projectService.GetById(id);
            ProjectViewModel model = null;
            if (dto is not null)
            {
                model = new ProjectViewModel
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    Description = dto.Description
                };
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProjectViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dto = new ProjectDto
                {
                    Id = model.Id,
                    Name = model.Name,
                    Description = model.Description,
                };
                await _projectService.Save(dto);
            }
            else
            {
                return View(model);
            }
            return RedirectToAction("Index", "Projects");
        }
        public async Task<ActionResult> Remove(int id)
        {
            await _projectService.Remove(id);
            return RedirectToAction("Index", "Projects");
        }
    }
}
