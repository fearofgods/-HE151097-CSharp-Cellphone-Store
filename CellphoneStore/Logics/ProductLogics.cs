using CellphoneStore.Models;
using System.Collections.Generic;
using System.Linq;

namespace CellphoneStore.Logics
{
    public class ProductLogics
    {
        WebContext context;

        public ProductLogics()
        {
            context = new WebContext();
        }

        public List<Product> GetTop4Product()
        {
            return context.Products.OrderByDescending(x=>x.Id).Take(4).ToList();
        }
    }
}
