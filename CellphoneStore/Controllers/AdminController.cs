using Microsoft.AspNetCore.Mvc;

namespace CellphoneStore.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult DashBoard()
        {
            return View();
        }
    }
}
