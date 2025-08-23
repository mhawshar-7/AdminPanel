using AdminPanel.Application.Dtos;
using AdminPanel.Application.Interfaces;
using AdminPanel.Data.Specifications;
using AdminPanel.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Web.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetUsersData()
        {
            try
            {
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                var sortColumn = Request.Form["order[0][column]"].FirstOrDefault();
                var sortDirection = Request.Form["order[0][dir]"].FirstOrDefault();

                var userSpecParams = new UserSpecParams
                {
                    PageIndex = int.TryParse(start, out var s) ? (s / (int.TryParse(length, out var l) ? l : 10)) + 1 : 1,
                    PageSize = int.TryParse(length, out var len) ? len : 10,
                    Search = searchValue,
                    Sort = sortDirection,
                    ColumnIndex = int.TryParse(sortColumn, out var col) ? col : 0
                };

                var spec = new UsersSpecification(userSpecParams);
                var countSpec = new UsersCountSpecification(userSpecParams);

                var users = await _userService.GetAllWithSpec(spec);
                var totalCount = await _userService.Count();
                var countFiltered = await _userService.CountWithSpecAsync(countSpec);

                var data = users.Select(s => new
                {
                    id = s.Id,
                    firstName = s.FirstName,
                    lastName = s.LastName,
                    userName = s.UserName,
                    email = s.Email
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
        public async Task<IActionResult> Edit(string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return View(new UserViewModel());
            }
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();
            var vm = new UserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (string.IsNullOrEmpty(model.Id))
            {
                // Create
                var registerDto = new RegisterDto
                {
                    Username = model.UserName,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Password = model.Password!
                };
                var result = await _userService.CreateUserAsync(registerDto);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View(model);
                }
            }
            else
            {
                // Update
                var dto = new UserDto
                {
                    Id = model.Id,
                    UserName = model.UserName,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };
                var result = await _userService.UpdateUserAsync(dto);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View(model);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest();
            var result = await _userService.DeleteUserAsync(id);
            if (!result.Succeeded)
            {
                TempData["Error"] = string.Join("; ", result.Errors.Select(e => e.Description));
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
