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
    }
}
