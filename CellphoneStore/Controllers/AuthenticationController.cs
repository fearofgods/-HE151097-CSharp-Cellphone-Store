using CellphoneStore.Logics;
using CellphoneStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

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
                if (Login.Role.Equals("sa"))
                {
                    var message = new
                    {
                        Status = "Success",
                        Content = "Pass",
                        IsAdmin = "true"
                    };
                    return Json(message);
                }else if (Login.Role.Equals("us"))
                {
                    var message = new
                    {
                        Status = "Success",
                        Content = "Pass",
                        IsAdmin = "false"
                    };
                    return Json(message);
                }
                else
                {
                    var message = new
                    {
                        Status = "Fail",
                        Content = "Bạn không có quyền truy cập"
                    };
                    return Json(message);
                }
                
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

        public IActionResult ViewOrders()
        {
            UserLogics userLogics = new UserLogics();
            string json = HttpContext.Session.GetString("user");
            User userX = null;
            if (!string.IsNullOrEmpty(json))
            {
                userX = JsonConvert.DeserializeObject<User>(json);
            }

            if (userX == null)
            {
                return Redirect("/");

            }
            else
            {
                List<Order> orders = userLogics.GetOrderByUname(userX.Username);
                return View("~/Views/Authentication/MyOrders.cshtml", orders);

            }
        }

        public IActionResult OrdersDetails(int id)
        {
            UserLogics userLogics = new UserLogics();
            ProductLogics productLogics = new ProductLogics();

            string json = HttpContext.Session.GetString("user");
            User userX = null;
            if (!string.IsNullOrEmpty(json))
            {
                userX = JsonConvert.DeserializeObject<User>(json);
            }

            if (userX == null)
            {
                return Redirect("/");

            }
            else
            {
                List<OrderDetail> orderDetails = userLogics.GetOrderDetailsByOId(id);
                List<Product> products = new List<Product>();
                foreach (OrderDetail orderDetail in orderDetails)
                {
                    products.Add(productLogics.GetProductByPid(orderDetail.Pid));

                }

                ViewData["Products"] = products;

                return View("~/Views/Authentication/OrdersDetails.cshtml", orderDetails);
            }
        }
    }
}
