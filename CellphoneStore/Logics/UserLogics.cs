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

        public UserLogics()
        {
            context = new WebContext();
        }

        public List<User> GetAll()
        {
            return context.Users.ToList();
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

        public User GetUserData(string userName)
        {
            return context.Users.Where(x => x.Username.Equals(userName)).FirstOrDefault();
        }


    }
}
