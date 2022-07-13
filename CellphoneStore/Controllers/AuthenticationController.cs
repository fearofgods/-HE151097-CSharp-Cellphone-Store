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

        [HttpPost]
        public IActionResult Register(User user)
        {
            UserLogics userLogics = new UserLogics();
            int status = userLogics.Register(user);
            if (status == 0)
            {
                var message = new
                {
                    Status = "Success",
                    Content = "Đã đăng kí thành công!",
                };
                return Json(message);
            }
            else
            {
                var message = new
                {
                    Status = "Fail",
                    Content = "Tên đăng nhập đã tồn tại!"
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

        [HttpGet]
        public IActionResult Profile()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Profile(User user)
        {
            UserLogics userLogics = new UserLogics();
            string json = HttpContext.Session.GetString("user");
            User userX = null;
            if (!string.IsNullOrEmpty(json))
            {
                userX = JsonConvert.DeserializeObject<User>(json);
            }
            user.Username = userX.Username;
            int status = userLogics.UpdateProfile(user);
            if (status == 0)
            {
                User newData = userLogics.GetUserData(user.Username);
                string jsons = JsonConvert.SerializeObject(newData);
                HttpContext.Session.SetString("user", jsons);
                ViewData["Message"] = "Cập nhật thành công!";
            }
            else
            {
                ViewData["Message"] = "Cập nhật thất bại!";

            }
            return View();
        }
    }
}
