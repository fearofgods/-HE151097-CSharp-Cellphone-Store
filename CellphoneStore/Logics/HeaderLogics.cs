using CellphoneStore.Models;
using System.Collections.Generic;
using System.Linq;

namespace CellphoneStore.Logics
{
    public class HeaderLogics
    {
        WebContext context;

        public HeaderLogics()
        {
            context = new WebContext();
        }

        public List<Category> LoadCategory()
        {
            return context.Categories.ToList();
        }

        public Category GetCategoryById(int id)
        {
            return context.Categories.Where(x => x.Cid == id).FirstOrDefault();
        }
    }
}
