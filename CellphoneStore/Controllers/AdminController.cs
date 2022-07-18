using CellphoneStore.Logics;
using CellphoneStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CellphoneStore.Controllers
{
    public class AdminController : Controller
    {
        IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        public IActionResult DashBoard()
        {
            return View();
        }

        public IActionResult ProductManage(int id, int value)
        {
            ProductLogics productLogics = new ProductLogics();
            HeaderLogics headerLogics = new HeaderLogics();

            List<Product> products = productLogics.SearchByCategoryPaging(id, value);
            Category category = headerLogics.GetCategoryById(id);

            int paging = Convert.ToInt32(config.GetSection("PageSettings")["Paging"]);
            int totalProducts = productLogics.CountAllProducts(id);

            int totalPage = totalProducts / paging;
            if (totalProducts % paging != 0)
            {
                totalPage++;
            }

            ViewData["TotalPage"] = totalPage;
            ViewData["Category"] = category;
            ViewData["CurrentPage"] = value;

            return View(products);
        }

        [HttpPost]
        public IActionResult GetTopSellProduct(int take)
        {
            ProductLogics productLogics = new ProductLogics();

            dynamic products = productLogics.BestSellProduct(take);

            return Json(products);
        }

        [HttpPost]
        public IActionResult GetTopValueOrders(int take)
        {
            ProductLogics productLogics = new ProductLogics();

            List<Order> orders = productLogics.GetTopValueOrder(take);

            return Json(orders);
        }

        public IActionResult UsersManage()
        {
            int paging = Convert.ToInt32(config.GetSection("PageSettings")["UserManage"]);
            UserLogics userLogics = new UserLogics();

            string uname = "";
            if (!string.IsNullOrEmpty(HttpContext.Request.Query["searchId"]))
            {
                uname = HttpContext.Request.Query["searchId"];
            }

            int value = 0;
            if (!string.IsNullOrEmpty(HttpContext.Request.Query["index"]))
            {
                value = Convert.ToInt32(HttpContext.Request.Query["index"]);
            }

            List<User> users = userLogics.GetUserPaging(value, uname);
            int totalUsers = userLogics.CountUser(uname);
            int totalPage = totalUsers / paging;
            if (totalUsers % paging != 0)
            {
                totalPage++;
            }

            ViewData["TotalPage"] = totalPage;
            ViewData["CurrentPage"] = value;
            ViewData["SearchValue"] = uname;

            return View(users);
        }

        [HttpGet]
        public IActionResult AddNewProduct()
        {
            HeaderLogics headerLogics = new HeaderLogics();
            List<Category> categories = headerLogics.LoadCategory();
            return View(categories);
        }

        [HttpPost]
        public IActionResult AddNewProduct(Product product, ProductDetail productDetail, string colors, string storages)
        {
            ProductLogics productLogics = new ProductLogics();

            List<string> color = JsonConvert.DeserializeObject<List<string>>(colors);
            List<string> storage = JsonConvert.DeserializeObject<List<string>>(storages);

            productLogics.AddNewProduct(product);
            productLogics.AddProductDetails(productDetail);
            productLogics.AddColorDetails(color, product.Pid);
            productLogics.AddStorageDetails(storage, product.Pid);

            return Json(new
            {
                Status = "Success",
                Content = "Thêm thành công!",
            });
        }

        public IActionResult RemoveProduct(string id)
        {
            ProductLogics productLogics = new ProductLogics();
            int status = productLogics.RemoveProduct(id);
            if (status == 1)
            {
                return Json(new
                {
                    Status = "Success",
                    Content = "Xóa thành công!",
                });
            }
            else
            {
                return Json(new
                {
                    Status = "Fail",
                    Content = "Xóa thất bại!",
                });
            }

        }

        public IActionResult ProductDetails(string id)
        {
            ProductLogics productLogics = new ProductLogics();
            HeaderLogics headerLogics = new HeaderLogics();

            List<Category> categories = headerLogics.LoadCategory();
            ProductDetail productDetail = productLogics.GetProductDetail(id);
            Product product = productLogics.GetProductByPid(id);
            List<ColorDetail> colors = productLogics.GetColorDetail(id);
            List<StorageDetail> storages = productLogics.GetStorageDetails(id);


            ViewData["ProductDetail"] = productDetail;
            ViewData["Product"] = product;
            ViewData["Colors"] = colors;
            ViewData["Storages"] = storages;

            return View(categories);
        }

        [HttpPost]
        public IActionResult RemoveColor(int id)
        {
            ProductLogics productLogics = new ProductLogics();
            int status = productLogics.RemoveColor(id);
            if (status == 1)
            {
                return Json(new { Status = "Success", Content = "Xóa thành công!" });
            }
            else
            {
                return Json(new { Status = "Fail", Content = "Xóa thất bại!" });
            }
        }

        [HttpPost]
        public IActionResult RemoveStorage(int id)
        {
            ProductLogics productLogics = new ProductLogics();
            int status = productLogics.RemoveStorage(id);
            if (status == 1)
            {
                return Json(new { Status = "Success", Content = "Xóa thành công!" });
            }
            else
            {
                return Json(new { Status = "Fail", Content = "Xóa thất bại!" });
            }
        }

        [HttpPost]
        public IActionResult UpdateProduct(string oldColors, string oldStorages, string newStorages, string newColors, Product product, ProductDetail productDetail)
        {
            ProductLogics productLogics = new ProductLogics();

            List<ColorDetail> colorDetails = JsonConvert.DeserializeObject<List<ColorDetail>>(oldColors);
            List<StorageDetail> storageDetails = JsonConvert.DeserializeObject<List<StorageDetail>>(oldStorages);


            List<string> color = JsonConvert.DeserializeObject<List<string>>(newColors);
            List<string> storage = JsonConvert.DeserializeObject<List<string>>(newStorages);



            int updateProduct = productLogics.UpdateProduct(product);
            int updateDetails = productLogics.UpdateProductDetails(productDetail);
            int updateColor = productLogics.UpdateColorDetails(colorDetails);
            int updateStorage = productLogics.UpdateStorageDetails(storageDetails);

            if (color != null && color.Count > 0)
            {
                productLogics.AddColorDetails(color, product.Pid);
            }

            if (storage != null && storage.Count > 0)
            {
                productLogics.AddStorageDetails(storage, product.Pid);
            }

            if (updateProduct == 1 && updateDetails == 1 && updateColor == 1 && updateStorage == 1)
            {
                return Json(new { Status = "Success", Content = "Cập nhật thành công!" });
            }
            else
            {
                return Json(new { Status = "Fail", Content = "Cập nhật thất bại!" });
            }

        }

        [HttpPost]
        public IActionResult UpdateUser(User user)
        {
            UserLogics userLogics = new UserLogics();
            User userX = userLogics.GetUserData(user.Username);
            if (!userX.Role.Equals("sa"))
            {
                int status = userLogics.UpdateRole(user);
                if (status == 0)
                {
                    return Json(new
                    {
                        Status = "Success",
                        Content = "Cập nhật thành công!"
                    });

                }
                else
                {
                    return Json(new
                    {
                        Status = "Fail",
                        Content = "Cập nhật thất bại!"
                    });

                }
            }
            else
            {
                return Json(new
                {
                    Status = "Fail",
                    Content = "Không thể thay đổi quyền admin!"
                });
            }
            

        }

        public IActionResult Orders()
        {
            ProductLogics productLogics = new ProductLogics();
            List<Order> orders = productLogics.GetAllOrders();

            return View(orders);
        }

        public IActionResult OrderDetails(int id)
        {
            UserLogics userLogics = new UserLogics();
            ProductLogics productLogics = new ProductLogics();

            List<OrderDetail> orderDetails = userLogics.GetOrderDetailsByOId(id);
            List<Product> products = new List<Product>();
            User user = productLogics.GetUserByOId(id);

            foreach (OrderDetail orderDetail in orderDetails)
            {
                products.Add(productLogics.GetProductByPid(orderDetail.Pid));

            }

            ViewData["User"] = user;
            ViewData["Products"] = products;

            return View(orderDetails);
        }
    }
}
