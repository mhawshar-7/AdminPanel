using AdminPanel.Application.Dtos;
using AdminPanel.Data.Entities;
using AdminPanel.Data.Interfaces;
using AdminPanel.Data.Specifications;
using AdminPanel.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdminPanel.Web.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly IClientService _clientService;

        public ProjectsController(IProjectService projectService, IClientService clientService)
        {
            _projectService = projectService;
            _clientService = clientService;
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

                var projectSpecParams = new ProjectSpecParams
                {
                    PageIndex = int.TryParse(start, out var s) ? (s / (int.TryParse(length, out var l) ? l : 10)) + 1 : 1,
                    PageSize = int.TryParse(length, out var len) ? len : 10,
                    Search = searchValue,
                    Sort = sortDirection,
                    ColumnIndex = int.TryParse(sortColumn, out var col) ? col : 0
                };

                var spec = new ProjectsSpecification(projectSpecParams);
                var countSpec = new ProjectCountSpecification(projectSpecParams);

                var projects = await _projectService.GetAllWithSpec(spec);
                var totalCount = await _projectService.Count();
                var countFiltered = await _projectService.CountWithSpecAsync(countSpec);

                var data = projects.Select(s => new
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

                return Json(new
                {
                    draw = draw,
                    recordsTotal = totalCount,
                    recordsFiltered = countFiltered,
                    data = data
                });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            ProjectViewModel model = new();
            var clients = await _clientService.GetIdNameClients();
            if (id.HasValue && id.Value > 0)
            {
                var dto = await _projectService.GetById(id.Value);
                if (dto is not null)
                {
                    model = new ProjectViewModel
                    {
                        Id = dto.Id,
                        Name = dto.Name,
                        Description = dto.Description,
                        StartDate = dto.StartDate,
                        EndDate = dto.EndDate,
                        Status = dto.Status,
                        Budget = dto.Budget,
                        ClientId = dto.ClientId
                    };
                }
            }
            model.Clients = clients.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToList();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProjectViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var dto = new ProjectDto
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Status = model.Status,
                Budget = model.Budget ?? 0,
                ClientId = model.ClientId ?? 0
            };
            await _projectService.Save(dto);
            return RedirectToAction("Index", "Projects");
        }

        public async Task<ActionResult> Remove(int id)
        {
            await _projectService.Remove(id);
            return RedirectToAction("Index", "Projects");
        }
    }
}
