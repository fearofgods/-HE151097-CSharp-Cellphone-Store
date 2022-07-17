using System;
using System.Collections.Generic;
using System.Linq;

namespace CellphoneStore.Models
{
    public class Cart
    {
        private List<Item> items = new List<Item>();
        private int total = 0;

        public Cart()
        {
        }

        public Cart(int total)
        {
            this.total = total;
        }

        public List<Item> Items
        {
            get { return items; }
            set { items = value; }
        }

        public int Total
        {
            get { return total; }
            set { total = value; }
        }

        public Item GetItemById(int id, int cid, int sid)
        {
            foreach (Item item in items)
            {
                if (item.products.Id == id && item.colorDetail.Id == cid && item.storageDetail.Id == sid)
                {
                    return item;
                }
            }
            return null;
        }

        public Product GetProductById(int id, List<Product> list)
        {
            return list.FirstOrDefault(x => x.Id == id);
        }

        public ColorDetail GetColorById(int cid, List<ColorDetail> list)
        {
            return list.FirstOrDefault(x => x.Id == cid);
        }

        public StorageDetail GetStorageById(int sid, List<StorageDetail> list)
        {
            return list.FirstOrDefault(x => x.Id == sid);
        }

        public void Remove(int id, int cid, int sid)
        {
            if (GetItemById(id, cid, sid) != null)
            {
                items.Remove(GetItemById(id, cid, sid));
            }
        }

        public void AddItem(Item item)
        {
            if (GetItemById(item.products.Id, item.colorDetail.Id, item.storageDetail.Id) != null)
            {
                Item itemInList = GetItemById(item.products.Id, item.colorDetail.Id, item.storageDetail.Id);
                itemInList.quantity = itemInList.quantity + item.quantity;
            }
            else
            {
                items.Add(item);
            }
        }

        public int GetQuantityById(int id, int cid, int sid)
        {
            return GetItemById(id, cid, sid).quantity;
        }

        public int GetTotalMoney()
        {
            foreach (Item item in items)
            {
                total += item.quantity*item.products.Price;
            }
            return total;
        }

        public Cart(string txt, List<Product> list, List<ColorDetail> listc, List<StorageDetail> lists)
        {
            if (!string.IsNullOrEmpty(txt))
            {
                string[] s = txt.Split(',');
                foreach (string atr in s)
                {
                    string[] split = atr.Split(':');
                    int id = Convert.ToInt32(split[0]);
                    int quantity = Convert.ToInt32(split[1]);
                    int cid = Convert.ToInt32(split[2]);
                    int sid = Convert.ToInt32(split[3]);
                    Product product = GetProductById(id, list);
                    ColorDetail colorDetail = GetColorById(cid, listc);
                    StorageDetail storageDetail = GetStorageById(sid, lists);
                    Item item = new Item(product, colorDetail, storageDetail, quantity);
                    AddItem(item);
                }
            }
        }
    }
}
