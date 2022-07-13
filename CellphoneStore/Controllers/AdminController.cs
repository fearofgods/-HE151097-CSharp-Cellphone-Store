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

            return Json(new {
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
            return View(id);
        }
    }
}
