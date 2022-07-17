namespace CellphoneStore.Models
{
    public class Item
    {
        public Product products { get; set; }
        public ColorDetail colorDetail { get; set; }
        public StorageDetail storageDetail { get; set; }
        public int quantity { get; set; }

        public Item()
        {
        }

        public Item(Product products, ColorDetail colorDetail, StorageDetail storageDetail, int quantity)
        {
            this.products = products;
            this.colorDetail = colorDetail;
            this.storageDetail = storageDetail;
            this.quantity = quantity;
        }

    }
}
