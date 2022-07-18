using System;
using System.Collections.Generic;

#nullable disable

namespace CellphoneStore.Models
{
    public partial class OrderDetail
    {
        public int Id { get; set; }
        public int Oid { get; set; }
        public string Pid { get; set; }
        public int Quantity { get; set; }
        public string Color { get; set; }
        public string Storage { get; set; }
        public int Price { get; set; }
        public int TotalPrice { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }

        public virtual Order OidNavigation { get; set; }
        public virtual Product PidNavigation { get; set; }
    }
}
