using CellphoneStore.Logics;
using CellphoneStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CellphoneStore.Controllers
{
    public class AuthenticationController : Controller
    {
        [HttpGet]
        public IActionResult Authentications()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            UserLogics userLogics = new UserLogics();
            User Login = userLogics.Login(user.Username, user.Password);
            string json = JsonConvert.SerializeObject(Login);

            if (Login != null)
            {
                HttpContext.Session.SetString("user", json);
                var message = new
                {
                    Status = "Success",
                    Content = "Pass"
                };
                return Json(message);
            }
            else
            {
                var message = new
                {
                    Status = "Fail",
                    Content = "Sai tài khoản hoặc mật khẩu!"
                };
                return Json(message);
            }
        }

        [HttpGet]
        public IActionResult Logout()
        {
            var session = HttpContext.Session;
            session.Clear();
            return Redirect("/Home/Index");
        }
    }
}
