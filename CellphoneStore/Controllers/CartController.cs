using CellphoneStore.Logics;
using CellphoneStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CellphoneStore.Controllers
{
    public class CartController : Controller
    {
        public IActionResult ShowCart()
        {
            ProductLogics productLogics = new ProductLogics();

            List<Product> products = productLogics.GetAllProduct();
            List<ColorDetail> colorDetail = productLogics.GetAllColor();
            List<StorageDetail> storageDetail = productLogics.GetAllStorage();

            string element = "";

            if (Request.Cookies["cartS"] != null)
            {
                element += Request.Cookies["cartS"];
            }

            Cart cart = new Cart(element, products, colorDetail,storageDetail);

            Console.WriteLine("Cart"+" "+element);

            if (cart.Items.Count <= 0)
            {
                ViewData["CartSize"] = "hidden";
            }
            else
            {
                ViewData["CartSize"] = "display";
            }

            return View(cart);
        }

        public IActionResult AddToCart(int id, int num, int cid, int sid)
        {
            string element = "";
            CookieOptions cookieOptions = new CookieOptions();

            if (Request.Cookies["cartS"] != null)
            {
                element += Request.Cookies["cartS"];
                cookieOptions.Expires = DateTime.Now;
                Response.Cookies.Append("cartS", "", cookieOptions);
            }

            if (string.IsNullOrEmpty(element))
            {
                element = $"{id}:{num}:{cid}:{sid}";
            }
            else
            {
                element = element+ $",{id}:{num}:{cid}:{sid}";
            }

            cookieOptions.Expires = new DateTimeOffset(DateTime.Now.AddDays(7));
            Response.Cookies.Append("cartS", element, cookieOptions);
            return Json(new { Status = "Success"});
        }

        [HttpPost]
        public IActionResult QuantityProcess(int id, int num, int cid, int sid)
        {
            ProductLogics productLogics = new ProductLogics();

            List<Product> products = productLogics.GetAllProduct();
            List<ColorDetail> colorDetail = productLogics.GetAllColor();
            List<StorageDetail> storageDetail = productLogics.GetAllStorage();

            string element = "";
            CookieOptions cookieOptions = new CookieOptions();

            if (Request.Cookies["cartS"] != null)
            {
                element += Request.Cookies["cartS"];
                cookieOptions.Expires = DateTime.Now;
                Response.Cookies.Append("cartS", "", cookieOptions);
            }

            Cart cart = new Cart(element, products, colorDetail, storageDetail);
            Product product = productLogics.GetProductById(id);
            ColorDetail colorDetail1 = productLogics.GetColorById(cid);
            StorageDetail storageDetail1 = productLogics.GetStorageById(sid);

            int store = product.Amount;

            if (num == -1 && cart.GetQuantityById(id, cid, sid) <= 1)
            {
                cart.Remove(id, cid, sid);
            }
            else if (num == 0)
            {
                cart.Remove(id, cid, sid);

            }
            else
            {
                if (num == 1 && cart.GetQuantityById(id, cid, sid) >= store)
                {
                    num = 0;
                }

                Item item = new Item(product, colorDetail1, storageDetail1, num);
                cart.AddItem(item);
            }

            List<Item> items = cart.Items;
            element = "";
            if (items.Count > 0)
            {
                element = $"{items[0].products.Id}:{items[0].quantity}:{items[0].colorDetail.Id}:{items[0].storageDetail.Id}";
                for (int i = 1; i < items.Count; i++)
                {
                    element += $",{items[i].products.Id}:{items[i].quantity}:{items[i].colorDetail.Id}:{items[i].storageDetail.Id}";
                }
            }

            cookieOptions.Expires = new DateTimeOffset(DateTime.Now.AddDays(7));
            Response.Cookies.Append("cartS", element, cookieOptions);

            if (cart.Items.Count <= 0)
            {
                ViewData["CartSize"] = "hidden";
            }
            else
            {
                ViewData["CartSize"] = "display";
            }

            return Json(new {Status = "Success"});
        }

        [HttpGet]
        public IActionResult Payment()
        {
            ProductLogics productLogics = new ProductLogics();

            List<Product> products = productLogics.GetAllProduct();
            List<ColorDetail> colorDetail = productLogics.GetAllColor();
            List<StorageDetail> storageDetail = productLogics.GetAllStorage();

            string element = "";

            if (Request.Cookies["cartS"] != null)
            {
                element += Request.Cookies["cartS"];
            }

            Cart cart = new Cart(element, products, colorDetail, storageDetail);

            if (cart.Items.Count <= 0)
            {
                ViewData["CartSize"] = "hidden";
            }
            else
            {
                ViewData["CartSize"] = "display";
            }
            string? json = HttpContext.Session.GetString("user");
            User user = null;
            if (json != null) 
                user = JsonConvert.DeserializeObject<User>(json);

            if (user != null)
            {
                return View("~/Views/Cart/Payment.cshtml", cart);
            }
            else
            {
                return Redirect("/Authentication/Authentications");
            }

        }

        [HttpPost]
        public IActionResult ConfirmPayment(string description)
        {
            ProductLogics productLogics = new ProductLogics();

            List<Product> products = productLogics.GetAllProduct();
            List<ColorDetail> colorDetail = productLogics.GetAllColor();
            List<StorageDetail> storageDetail = productLogics.GetAllStorage();

            string element = "";

            if (Request.Cookies["cartS"] != null)
            {
                element += Request.Cookies["cartS"];
            }

            Cart cart = new Cart(element, products, colorDetail, storageDetail);

            string? json = HttpContext.Session.GetString("user");
            User user = null;
            if (json != null)
                user = JsonConvert.DeserializeObject<User>(json);

            if (user != null)
            {
                UserLogics userLogics = new UserLogics();
                userLogics.AddOrder(user, cart, description);

                CookieOptions cookieOptions = new CookieOptions();
                cookieOptions.Expires = DateTime.Now;
                Response.Cookies.Append("cartS", "", cookieOptions);

                return Redirect("/");
            }
            else
            {
                return Redirect("/Authentication/Authentications");
            }
        }
    }
}
