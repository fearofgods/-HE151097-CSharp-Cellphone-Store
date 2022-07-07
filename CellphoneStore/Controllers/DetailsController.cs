using CellphoneStore.Logics;
using CellphoneStore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CellphoneStore.Controllers
{
    public class DetailsController : Controller
    {
        public IActionResult ProductDetails(string id)
        {
            ProductLogics productLogics = new ProductLogics();
            Product product = productLogics.GetProductByPid(id);
            ProductDetail productDetail = productLogics.GetProductDetail(id);
            List<ColorDetail> colors = productLogics.GetColorDetail(id);
            List<StorageDetail> storages = productLogics.GetStorageDetails(id);

            if (productDetail == null || colors == null || storages == null)
            {
                return Redirect("/Error/Index/500");
            }

            ViewData["Product"] = product;
            ViewData["ProductDetails"] = productDetail;
            ViewData["ColorDetails"] = colors;
            ViewData["StorageDetails"] = storages;

            return View();
        }
    }
}
