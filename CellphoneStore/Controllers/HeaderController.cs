using CellphoneStore.Logics;
using CellphoneStore.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CellphoneStore.Controllers
{
    public class HeaderController : Controller
    {
        [HttpGet]
        public string LoadCategory()
        {
            HeaderLogics headerLogics = new HeaderLogics();
            List<Category> categories = headerLogics.LoadCategory();
            
            string jsonCategory = JsonConvert.SerializeObject(categories);

            return jsonCategory;
        }


    }
}
