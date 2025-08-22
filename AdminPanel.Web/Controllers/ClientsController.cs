using AdminPanel.Application.Dtos;
using AdminPanel.Data.Entities;
using AdminPanel.Data.Interfaces;
using AdminPanel.Data.Specifications;
using AdminPanel.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdminPanel.Web.Controllers
{
    public class ClientsController : Controller
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetClientsData()
        {
            try
            {
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                var sortColumn = Request.Form["order[0][column]"].FirstOrDefault();
                var sortDirection = Request.Form["order[0][dir]"].FirstOrDefault();

                var clientSpecParams = new ClientSpecParams
                {
                    PageIndex = int.TryParse(start, out var s) ? (s / (int.TryParse(length, out var l) ? l : 10)) + 1 : 1,
                    PageSize = int.TryParse(length, out var len) ? len : 10,
                    Search = searchValue,
                    Sort = sortDirection,
                    ColumnIndex = int.TryParse(sortColumn, out var col) ? col : 0
                };

                var spec = new ClientsSpecification(clientSpecParams);
                var countSpec = new ClientsCountSpecification(clientSpecParams);

                var clients = await _clientService.GetAllWithSpec(spec);
                var totalCount = await _clientService.Count();
                var countFiltered = await _clientService.CountWithSpecAsync(countSpec);

                var data = clients.Select(s => new
                {
                    id = s.Id,
                    name = s.Name,
                    email = s.Email,
                    phone = s.Phone,
                    address = s.Address,
                    modifiedDate = s.ModifiedDate?.ToString("dd/MM/yyyy")
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
            ClientViewModel model = new();
            var clients = await _clientService.GetIdNameClients();
            if (id.HasValue && id.Value > 0)
            {
                var dto = await _clientService.GetById(id.Value);
                if (dto is not null)
                {
                    model = new ClientViewModel
                    {
                        Id = dto.Id,
                        Name = dto.Name,
                        Email = dto.Email,
                        Phone = dto.Phone,
                        Address = dto.Address
                    };
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ClientViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var dto = new ClientDto
            {
                Id = model.Id,
                Name = model.Name,
                Email = model.Email,
                Phone = model.Phone,
                Address = model.Address
            };
            await _clientService.Save(dto);
            return RedirectToAction("Index", "Clients");
        }

        public async Task<ActionResult> Remove(int id)
        {
            await _clientService.Remove(id);
            return RedirectToAction("Index", "Clients");
        }
    }
}
