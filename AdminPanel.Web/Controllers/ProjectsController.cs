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
        public IActionResult Examples()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GetProjectsData()
        {
            try
            {
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                var sortColumn = Request.Form["order[0][column]"].FirstOrDefault();
                var sortDirection = Request.Form["order[0][dir]"].FirstOrDefault();

                // Parse parameters
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;

                // Get all projects
                var projects = await _projectService.GetAll();

                // Convert to ProjectViewModel
                var projectViewModels = projects.Select(s => new
                {
                    id = s.Id,
                    name = s.Name,
                    description = s.Description,
                    status = s.Status.ToString(),
                    startDate = s.StartDate.ToString("dd/MM/yyyy"),
                    endDate = s.EndDate.HasValue ? s.EndDate.Value.ToString("dd/MM/yyyy") : null,
                    budget = s.Budget.ToString("C"),
                    modifiedDate = s.CreatedDate?.ToString("dd/MM/yyyy")
                }).ToList();

                if (!string.IsNullOrEmpty(searchValue))
                {
                    projectViewModels = projectViewModels.Where(x =>
                        (x.name != null && x.name.Contains(searchValue, StringComparison.OrdinalIgnoreCase)) ||
                        (x.description != null && x.description.Contains(searchValue, StringComparison.OrdinalIgnoreCase))
                    ).ToList();
                }

                // Total records after filtering
                int recordsFiltered = projectViewModels.Count;

                if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortDirection))
                {
                    int columnIndex = Convert.ToInt32(sortColumn);
                    switch (columnIndex)
                    {
                        case 1: // Name
                            projectViewModels = sortDirection == "asc"
                                ? projectViewModels.OrderBy(x => x.name).ToList()
                                : projectViewModels.OrderByDescending(x => x.name).ToList();
                            break;
                        case 2: // Description
                            projectViewModels = sortDirection == "asc"
                                ? projectViewModels.OrderBy(x => x.description).ToList()
                                : projectViewModels.OrderByDescending(x => x.description).ToList();
                            break;
                        case 3: // Status
                            projectViewModels = sortDirection == "asc"
                                ? projectViewModels.OrderBy(x => x.status).ToList()
                                : projectViewModels.OrderByDescending(x => x.status).ToList();
                            break;
                        case 4: // Start Date
                            projectViewModels = sortDirection == "asc"
                                ? projectViewModels.OrderBy(x => x.startDate).ToList()
                                : projectViewModels.OrderByDescending(x => x.startDate).ToList();
                            break;
                        case 5: // End Date
                            projectViewModels = sortDirection == "asc"
                                ? projectViewModels.OrderBy(x => x.endDate).ToList()
                                : projectViewModels.OrderByDescending(x => x.endDate).ToList();
                            break;
                        case 6: // Modified Date
                            projectViewModels = sortDirection == "asc"
                                ? projectViewModels.OrderBy(x => x.modifiedDate).ToList()
                                : projectViewModels.OrderByDescending(x => x.modifiedDate).ToList();
                            break;
                    }
                }

                // Paging
                var data = projectViewModels.Skip(skip).Take(pageSize).ToList();

                // Get total count (before filtering)
                var totalRecords = projects.Count();

                // Return JSON result
                return Json(new
                {
                    draw = draw,
                    recordsTotal = totalRecords,
                    recordsFiltered = recordsFiltered,
                    data = data
                });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
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
