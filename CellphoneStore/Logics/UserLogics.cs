using CellphoneStore.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CellphoneStore.Logics
{
    public class UserLogics
    {
        WebContext context;
        IConfiguration config;
        int paging;

        public UserLogics()
        {
            context = new WebContext();
            config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            paging = Convert.ToInt32(config.GetSection("PageSettings")["UserManage"]);
        }

        public List<User> GetAll()
        {
            return context.Users.ToList();
        }

        public int CountUser(string uname)
        {
            return context.Users.Where(x => x.Username.Contains(uname)).ToList().Count;
        }

        public List<User> GetUserPaging(int take, string uname)
        {
            return context.Users.Where(x => x.Username.Contains(uname)).Skip(take * paging).Take(paging).ToList();
        }

        public User Login(string user, string pass)
        {
            return context.Users.Where(x => x.Username.Equals(user) && x.Password.Equals(pass)).FirstOrDefault();
        }

        public User CheckExist(string username)
        {
            return context.Users.Where(x => x.Username.Equals(username)).FirstOrDefault();
        }

        public int Register(User user)
        {
            User check = CheckExist(user.Username);
            if (check == null)
            {
                user.Role = "us";
                context.Users.Add(user);
                context.SaveChanges();
                return 0;
            }
            else return 1;
        }

        public int UpdateProfile(User user)
        {
            User check = CheckExist(user.Username);
            if (check != null)
            {
                check.Firstname = user.Firstname;
                check.Lastname = user.Lastname;
                check.Birthday = user.Birthday;
                check.Email = user.Email;
                check.Phone = user.Phone;
                check.Address = user.Address;
                context.SaveChanges();
                return 0;
            }
            else return 1;
        }

        public int UpdateRole(User user)
        {

            try
            {
                User check = CheckExist(user.Username);
                if (check != null)
                {
                    check.Role = user.Role;
                    context.SaveChanges();
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            catch (Exception)
            {
                return 1;
            }
        }

        public User GetUserData(string userName)
        {
            return context.Users.Where(x => x.Username.Equals(userName)).FirstOrDefault();
        }

        public int AddOrder(User user, Cart cart, string description)
        {
            context.Orders.Add(new Order
            {
                Uname = user.Username,
                Orderdate = DateTime.Now,
                Total = cart.GetTotalMoney()
            });
            context.SaveChanges();
            Order order = context.Orders.Where(x => x.Uname.Equals(user.Username)).OrderByDescending(x => x.Id).FirstOrDefault();

            foreach (Item i in cart.Items)
            {
                context.OrderDetails.Add(new OrderDetail
                {
                    Oid = order.Id,
                    Pid = i.products.Pid,
                    Quantity = i.quantity,
                    Color = i.colorDetail.Color,
                    Storage = i.storageDetail.Storage,
                    Price = i.products.Price,
                    TotalPrice = i.quantity * i.products.Price,
                    Status = "pd",
                    Description = description
                });
                context.SaveChanges();

                Product product = context.Products.FirstOrDefault(x => x.Pid == i.products.Pid);
                if (product != null)
                {
                    product.Amount = product.Amount - i.quantity;
                }

                context.SaveChanges();

            }


            return 1;
        }
    }
}
