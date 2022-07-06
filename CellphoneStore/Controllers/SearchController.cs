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
        public IActionResult Product(int id, int value)
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            int paging = Convert.ToInt32(config.GetSection("PageSettings")["Paging"]);

            ProductLogics productLogics = new ProductLogics();
            HeaderLogics headerLogics = new HeaderLogics();
            Category category = headerLogics.GetCategoryById(id);
            List<Product> products = productLogics.SearchByCategoryPaging(id, value);
            int totalProducts = productLogics.CountAllProducts(id);

            int totalPage = totalProducts / paging;
            if (totalProducts % 10 != 0)
            {
                totalPage++;
            }

            ViewData["TotalPage"] = totalPage;
            ViewData["Category"] = category;
            ViewData["CurrentPage"] = value;

            return View(products);
        }
    }
}
