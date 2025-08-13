using AdminPanel.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AdminPanel.Web.ViewComponents
{
    public class ProjectListViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(IEnumerable<ProjectDto> projects)
        {
            return View(projects);
        }
    }
}
