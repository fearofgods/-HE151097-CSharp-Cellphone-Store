using CellphoneStore.Logics;
using CellphoneStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CellphoneStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string LoadTop4Products()
        {
            ProductLogics productLogics = new ProductLogics();
            List<Product> products = productLogics.GetTop4Product();
            string json = JsonConvert.SerializeObject(products);
            return json;
        }

        [HttpPost]
        public string LoadNextTop4Products(int id)
        {
            ProductLogics productLogics = new ProductLogics();
            if (id < 8)
            {
                List<Product> products = productLogics.GetNextTop4Products(id);
                string json = JsonConvert.SerializeObject(products);
                return json;
            }
            else
            {
                return "";

            }


        }

        [HttpPost]
        public string TopSell()
        {
            ProductLogics productLogics = new ProductLogics();
            List<Product> test = productLogics.BestSellProduct();
            string json = JsonConvert.SerializeObject(test);
            return json;

        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
