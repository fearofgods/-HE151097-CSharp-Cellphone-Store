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

        public ProductLogics()
        {
            context = new WebContext();
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
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            int paging = Convert.ToInt32(config.GetSection("PageSettings")["Paging"]);
            if (id == 0)
            {
                return context.Products.Skip(skip*paging).Take(paging).ToList();
            }
            else
            {
                return context.Products.Where(x => x.Cid == id).Skip(skip*paging).Take(paging).ToList();
            }
        }


        
    }
}
