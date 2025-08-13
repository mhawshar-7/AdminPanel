using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Web.Controllers.Projects
{
    [Route("Projects/[controller]/[action]")]
    public class ViewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
