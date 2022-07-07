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

        public List<Product> BestSellProduct()
        {
            return context.Products.Join(context.OrderDetails, pro => pro.Pid, od => od.Pid, (pro, od) => new { pro, od }).GroupBy(x => new
            {
                x.pro.Id,
                x.pro.Pid,
                x.pro.Cid,
                x.pro.Name,
                x.pro.Image,
                x.pro.Price,
                x.pro.Description,
                x.pro.Amount,

            }).OrderByDescending(x => x.Sum(y => y.od.Quantity)).Select(x => new Product
            {
                Id = x.Key.Id,
                Pid = x.Key.Pid,
                Cid = x.Key.Cid,
                Name = x.Key.Name,
                Image = x.Key.Image,
                Price = x.Key.Price,
                Description = x.Key.Description,
                Amount = x.Key.Amount


            }).Take(4).ToList();
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
    }
}
