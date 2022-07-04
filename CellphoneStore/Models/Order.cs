using System;
using System.Collections.Generic;

#nullable disable

namespace CellphoneStore.Models
{
    public partial class Order
    {
        public int Id { get; set; }
        public string Uname { get; set; }
        public DateTime Orderdate { get; set; }
        public int Total { get; set; }

        public virtual User UnameNavigation { get; set; }
    }
}
