using AdminPanel.Application.Dtos;
using AdminPanel.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AdminPanel.Web.ViewComponents
{
    public class ProjectListViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(IEnumerable<ProjectViewModel> projects)
        {
            return View(projects);
        }   
    }
}
