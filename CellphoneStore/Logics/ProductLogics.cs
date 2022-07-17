using CellphoneStore.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CellphoneStore.Logics
{
    public class ProductLogics
    {
        WebContext context;
        IConfiguration config;
        int paging;
        public ProductLogics()
        {
            context = new WebContext();
            config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            paging = Convert.ToInt32(config.GetSection("PageSettings")["Paging"]);

        }

        public List<Product> GetTop4Product()
        {
            return context.Products.OrderByDescending(x => x.Id).Take(4).ToList();
        }

        public List<Product> GetNextTop4Products(int skip)
        {
            return context.Products.OrderByDescending(x => x.Id).Skip(skip).Take(4).ToList();
        }

        public List<Order> GetTopValueOrder(int take)
        {
            return context.Orders.OrderByDescending(x => x.Total).Take(take).ToList();
        }

        public dynamic BestSellProduct(int take)
        {
            return context.Products.Join(context.OrderDetails, pro => pro.Pid, od => od.Pid, (pro, od) => new { pro, od }).GroupBy(x => new
            {
                x.pro.Id,
                x.pro.Pid,
                x.pro.Cid,
                x.pro.Name,
                x.pro.Image,
                x.pro.Price,
                x.pro.Description
            }).OrderByDescending(x => x.Sum(y => y.od.Quantity)).Select(x => new
            {
                Id = x.Key.Id,
                Pid = x.Key.Pid,
                Cid = x.Key.Cid,
                Name = x.Key.Name,
                Image = x.Key.Image,
                Price = x.Key.Price,
                Description = x.Key.Description,
                Count = x.Sum(x=>x.od.Quantity)

            }).Take(take).ToList();
        }

        public int CountAllProducts(int cateId)
        {
            if (cateId == 0)
            {
                return context.Products.ToList().Count;
            }
            else
            {
                return context.Products.Where(x => x.Cid == cateId).ToList().Count;
            }


        }

        public List<Product> SearchByCategoryPaging(int id, int skip)
        {

            if (id == 0)
            {
                return context.Products.Skip(skip * paging).Take(paging).ToList();
            }
            else
            {
                return context.Products.Where(x => x.Cid == id).Skip(skip * paging).Take(paging).ToList();
            }
        }

        public int GetAllProductsByPrice(int from, int to)
        {
            return context.Products.Where(x => x.Price >= from * 1000000 && x.Price <= to * 1000000).ToList().Count;
        }

        public List<Product> SearchByPricePaging(int from, int to, int skip)
        {
            //int paging = Convert.ToInt32(config.GetSection("PageSettings")["Paging"]);
            return context.Products.Where(x => x.Price >= from * 1000000 && x.Price <= to * 1000000).Skip(skip * paging).Take(paging).ToList();
        }

        public int GetAllProductsByName(string search)
        {
            return context.Products.Where(x => x.Name.Contains(search)).ToList().Count;
        }

        public List<Product> SearchByName(string search, int skip)
        {
            return context.Products.Where(x => x.Name.Contains(search)).Skip(skip).Take(paging).ToList();
        }

        public List<Product> GetAllProduct()
        {
            return context.Products.ToList();
        }

        public List<ColorDetail> GetAllColor()
        {
            return context.ColorDetails.ToList();
        }

        public List<StorageDetail> GetAllStorage()
        {
            return context.StorageDetails.ToList();
        }

        public Product GetProductById(int id)
        {
            return context.Products.Where(x => x.Id == id).FirstOrDefault();
        }

        public ColorDetail GetColorById(int cid)
        {
            return context.ColorDetails.FirstOrDefault(x => x.Id == cid);
        }

        public StorageDetail GetStorageById(int cid)
        {
            return context.StorageDetails.FirstOrDefault(x => x.Id == cid); 
        }

        //Product details

        public ProductDetail GetProductDetail(string pid)
        {
            return context.ProductDetails.Where(x => x.Pid.Equals(pid)).FirstOrDefault();
        }

        public List<ColorDetail> GetColorDetail(string pid)
        {
            return context.ColorDetails.Where(x => x.Pid.Equals(pid)).ToList();
        }

        public List<StorageDetail> GetStorageDetails(string pid)
        {
            return context.StorageDetails.Where(x => x.Pid.Equals(pid)).ToList();
        }

        public Product GetProductByPid(string pid)
        {
            return context.Products.Where(x => x.Pid.Equals(pid)).FirstOrDefault();
        }

        //Add new product

        public void AddNewProduct(Product product)
        {
            context.Add(product);
            context.SaveChanges();
        }

        public void AddProductDetails(ProductDetail productDetail)
        {
            context.Add(productDetail);
            context.SaveChanges();
        }

        public void AddColorDetails(List<string> colors, string pid)
        {
            List<ColorDetail> colorsList = new List<ColorDetail>();
            foreach (string color in colors)
            {
                colorsList.Add(new ColorDetail { Pid = pid, Color = color });
            };
            context.AddRange(colorsList);
            context.SaveChanges();
        }

        public void AddStorageDetails(List<string> storages, string pid)
        {
            List<StorageDetail> storageDetails = new List<StorageDetail>();
            foreach (string storage in storages)
            {
                storageDetails.Add(new StorageDetail { Pid = pid, Storage = storage });
            };
            context.AddRange(storageDetails);
            context.SaveChanges();
        }

        //Update product
        public int UpdateProduct(Product product)
        {
            Product p = context.Products.FirstOrDefault(x => x.Pid == product.Pid);
            try
            {
                p.Cid = product.Cid;
                p.Name = product.Name;
                p.Image = product.Image;
                p.Price = product.Price;
                p.Description = product.Description;
                p.Amount = product.Amount;
                //context.Update(product);
                context.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        public int UpdateProductDetails(ProductDetail productDetail)
        {
            ProductDetail pd = context.ProductDetails.FirstOrDefault(x => x.Pid == productDetail.Pid);
            try
            {
                pd.Screen = productDetail.Screen;
                pd.Os = productDetail.Os;
                pd.Rearcam = productDetail.Rearcam;
                pd.Frontcam = productDetail.Frontcam;
                pd.Soc = productDetail.Soc;
                pd.Ram = productDetail.Ram;
                pd.Sim = productDetail.Sim;
                pd.Battery = productDetail.Battery;
                //context.Update(productDetail);
                context.SaveChanges();
                return 1;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        public int UpdateColorDetails(List<ColorDetail> colorDetails)
        {
            try
            {
                foreach (ColorDetail colorDetail in colorDetails)
                {
                    ColorDetail pd = context.ColorDetails.Where(x => x.Id == colorDetail.Id && x.Pid == colorDetail.Pid).FirstOrDefault();
                    pd.Color = colorDetail.Color;
                }
                //context.UpdateRange(colorDetails);
                context.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        public int UpdateStorageDetails(List<StorageDetail> storageDetails)
        {
            try
            {
                foreach (StorageDetail storageDetail in storageDetails)
                {
                    StorageDetail sd = context.StorageDetails.Where(x => x.Id == storageDetail.Id && x.Pid == storageDetail.Pid).FirstOrDefault();
                    sd.Storage = storageDetail.Storage;
                }
                //context.UpdateRange(storageDetails);
                context.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        //Remove product
        public int RemoveProduct(string pid)
        {
            Product product = context.Products.Where(x => x.Pid.Equals(pid)).FirstOrDefault();
            ProductDetail productDetail = context.ProductDetails.Where(x => x.Pid.Equals(pid)).FirstOrDefault();
            List<StorageDetail> storageDetail = context.StorageDetails.Where(x => x.Pid.Equals(pid)).ToList();
            List<ColorDetail> colorDetail = context.ColorDetails.Where(x => x.Pid.Equals(pid)).ToList();
            try
            {
                if (productDetail != null)
                {
                    context.ProductDetails.Remove(productDetail);
                }

                if (storageDetail != null && storageDetail.Count > 0)
                {
                    context.StorageDetails.RemoveRange(storageDetail);
                }

                if (colorDetail != null && colorDetail.Count > 0)
                {
                    context.ColorDetails.RemoveRange(colorDetail);
                }
                context.Products.Remove(product);
                context.SaveChanges();
                return 1;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return 0;
            }

        }

        public int RemoveColor(int id)
        {
            ColorDetail color = context.ColorDetails.Where(x => x.Id == id).FirstOrDefault();
            try
            {
                context.ColorDetails.Remove(color);
                context.SaveChanges();
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public int RemoveStorage(int id)
        {
            StorageDetail storage = context.StorageDetails.Where(x => x.Id == id).FirstOrDefault();
            try
            {
                context.StorageDetails.Remove(storage);
                context.SaveChanges();
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

    }
}
