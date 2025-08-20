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
                // Add logging to see if method is called
                Console.WriteLine("GetProjectsData method called");

                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                var sortColumn = Request.Form["order[0][column]"].FirstOrDefault();
                var sortDirection = Request.Form["order[0][dir]"].FirstOrDefault();

                Console.WriteLine($"Draw: {draw}, Start: {start}, Length: {length}");
                Console.WriteLine($"Search: {searchValue}, Sort Column: {sortColumn}, Sort Direction: {sortDirection}");

                // Parse parameters
                int pageSize = length != null ? Convert.ToInt32(length) : 10;
                int skip = start != null ? Convert.ToInt32(start) : 0;

                // Get all projects
                var projects = await _projectService.GetAll();
                Console.WriteLine($"Total projects from service: {projects.Count()}");

                // Convert to anonymous object (matching DataTables column names)
                var projectViewModels = projects.Select(s => new
                {
                    id = s.Id,
                    name = s.Name,
                    description = s.Description,
                    modifiedDate = s.CreatedDate?.ToString("dd/MM/yyyy")
                }).ToList();

                // Filter by search value
                if (!string.IsNullOrEmpty(searchValue))
                {
                    projectViewModels = projectViewModels.Where(x =>
                        (x.name != null && x.name.Contains(searchValue, StringComparison.OrdinalIgnoreCase)) ||
                        (x.description != null && x.description.Contains(searchValue, StringComparison.OrdinalIgnoreCase))
                    ).ToList();
                }

                // Total records after filtering
                int recordsFiltered = projectViewModels.Count;

                // Sorting - Account for checkbox column (index 0 if present)
                if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortDirection))
                {
                    int columnIndex = Convert.ToInt32(sortColumn);

                    // Adjust column index if checkbox column is present (subtract 1)
                    bool hasCheckbox = true; // Based on your configuration
                    if (hasCheckbox && columnIndex > 0)
                    {
                        columnIndex -= 1;
                    }

                    Console.WriteLine($"Adjusted column index: {columnIndex}");

                    switch (columnIndex)
                    {
                        case 0: // Name (first data column after checkbox)
                            projectViewModels = sortDirection == "asc"
                                ? projectViewModels.OrderBy(x => x.name).ToList()
                                : projectViewModels.OrderByDescending(x => x.name).ToList();
                            break;
                        case 1: // Description
                            projectViewModels = sortDirection == "asc"
                                ? projectViewModels.OrderBy(x => x.description).ToList()
                                : projectViewModels.OrderByDescending(x => x.description).ToList();
                            break;
                        case 2: // Modified Date
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

                var result = new
                {
                    draw = draw,
                    recordsTotal = totalRecords,
                    recordsFiltered = recordsFiltered,
                    data = data
                };

                Console.WriteLine($"Returning {data.Count} records out of {totalRecords} total");

                // Return JSON result
                return Json(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetProjectsData: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                return Json(new { error = ex.Message });
            }
        }
        //[HttpPost]
        //public async Task<IActionResult> GetProjectsData()
        //{
        //    try
        //    {
        //        var draw = Request.Form["draw"].FirstOrDefault();
        //        var start = Request.Form["start"].FirstOrDefault();
        //        var length = Request.Form["length"].FirstOrDefault();
        //        var searchValue = Request.Form["search[value]"].FirstOrDefault();
        //        var sortColumn = Request.Form["order[0][column]"].FirstOrDefault();
        //        var sortDirection = Request.Form["order[0][dir]"].FirstOrDefault();

        //        // Parse parameters
        //        int pageSize = length != null ? Convert.ToInt32(length) : 0;
        //        int skip = start != null ? Convert.ToInt32(start) : 0;

        //        // Get all projects
        //        var projects = await _projectService.GetAll();

        //        // Convert to ProjectViewModel
        //        var projectViewModels = projects.Select(s => new
        //        {
        //            id = s.Id,
        //            name = s.Name,
        //            description = s.Description,
        //            modifiedDate = s.CreatedDate?.ToString("dd/MM/yyyy")
        //        }).ToList();

        //        // Filter by search value
        //        if (!string.IsNullOrEmpty(searchValue))
        //        {
        //            projectViewModels = projectViewModels.Where(x => 
        //                (x.name != null && x.name.Contains(searchValue, StringComparison.OrdinalIgnoreCase)) ||
        //                (x.description != null && x.description.Contains(searchValue, StringComparison.OrdinalIgnoreCase))
        //            ).ToList();
        //        }

        //        // Total records after filtering
        //        int recordsFiltered = projectViewModels.Count;

        //        // Sorting
        //        if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortDirection))
        //        {
        //            int columnIndex = Convert.ToInt32(sortColumn);
        //            switch (columnIndex)
        //            {
        //                case 0: // Name
        //                    projectViewModels = sortDirection == "asc" 
        //                        ? projectViewModels.OrderBy(x => x.name).ToList()
        //                        : projectViewModels.OrderByDescending(x => x.name).ToList();
        //                    break;
        //                case 1: // Description
        //                    projectViewModels = sortDirection == "asc" 
        //                        ? projectViewModels.OrderBy(x => x.description).ToList()
        //                        : projectViewModels.OrderByDescending(x => x.description).ToList();
        //                    break;
        //                case 2: // Modified Date
        //                    projectViewModels = sortDirection == "asc" 
        //                        ? projectViewModels.OrderBy(x => x.modifiedDate).ToList()
        //                        : projectViewModels.OrderByDescending(x => x.modifiedDate).ToList();
        //                    break;
        //            }
        //        }

        //        // Paging
        //        var data = projectViewModels.Skip(skip).Take(pageSize).ToList();

        //        // Get total count (before filtering)
        //        var totalRecords = projects.Count();

        //        // Return JSON result
        //        return Json(new
        //        {
        //            draw = draw,
        //            recordsTotal = totalRecords,
        //            recordsFiltered = recordsFiltered,
        //            data = data
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { error = ex.Message });
        //    }
        //}

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
