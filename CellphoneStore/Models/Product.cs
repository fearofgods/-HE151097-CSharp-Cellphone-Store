using System;
using System.Collections.Generic;

#nullable disable

namespace CellphoneStore.Models
{
    public partial class Product
    {
        public Product()
        {
            ColorDetails = new HashSet<ColorDetail>();
            ProductDetails = new HashSet<ProductDetail>();
            StorageDetails = new HashSet<StorageDetail>();
        }

        public int Id { get; set; }
        public string Pid { get; set; }
        public int Cid { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }

        public virtual Category CidNavigation { get; set; }
        public virtual ICollection<ColorDetail> ColorDetails { get; set; }
        public virtual ICollection<ProductDetail> ProductDetails { get; set; }
        public virtual ICollection<StorageDetail> StorageDetails { get; set; }
    }
}
