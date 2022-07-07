using Microsoft.AspNetCore.Mvc;

namespace CellphoneStore.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index(string id)
        {
            ViewData["Code"] = id;
            return View("~/Views/Shared/Error.cshtml");
        }
    }
}
