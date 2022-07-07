using CellphoneStore.Logics;
using CellphoneStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace CellphoneStore.Controllers
{
    public class SearchController : Controller
    {
        IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        [HttpGet]
        public IActionResult Product(int id, int value)
        {
            int paging = Convert.ToInt32(config.GetSection("PageSettings")["Paging"]);

            ProductLogics productLogics = new ProductLogics();
            HeaderLogics headerLogics = new HeaderLogics();
            Category category = headerLogics.GetCategoryById(id);
            List<Product> products = productLogics.SearchByCategoryPaging(id, value);
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
        public IActionResult ProductByPrice(string id, int value)
        {
            int paging = Convert.ToInt32(config.GetSection("PageSettings")["Paging"]);
            ProductLogics productLogics = new ProductLogics();

            string[] searchValue = id.Trim().Split("-");
            int min = 0, max = 0;

            if (searchValue.Length > 0)
            {
                min = Convert.ToInt32(searchValue[0]);
                max = Convert.ToInt32(searchValue[1]);
            }

            List<Product> products = productLogics.SearchByPricePaging(min, max, value);
            int totalProducts = productLogics.GetAllProductsByPrice(min,max);
            int totalPage = totalProducts / paging;
            if (totalProducts % paging != 0)
            {
                totalPage++;
            }

            ViewData["TotalPage"] = totalPage;
            ViewData["SearchValue"] = id;
            ViewData["CurrentPage"] = value;
            ViewData["SearchTitle"] = $"{min}tr đến {max}tr";

            return View(products);
        }

        [HttpGet]
        public IActionResult SearchByName()
        {
            int paging = Convert.ToInt32(config.GetSection("PageSettings")["Paging"]);
            ProductLogics productLogics = new ProductLogics();
            string searchName = "";
            if (!string.IsNullOrEmpty(HttpContext.Request.Query["value"]))
            {
                searchName = HttpContext.Request.Query["value"];
            }

            int value = 0;
            if (!string.IsNullOrEmpty(HttpContext.Request.Query["index"]))
            {
                value = Convert.ToInt32(HttpContext.Request.Query["index"]);
            }

            int items = productLogics.GetAllProductsByName(searchName);
            List<Product> products = productLogics.SearchByName(searchName, value);
            int page = items / paging;
            if (items % paging != 0)
            {
                page++;
            }

            ViewData["SearchData"] = searchName;
            ViewData["TotalPage"] = page;
            ViewData["CurrentPage"] = value;

            return View(products);
        }
    }
}
