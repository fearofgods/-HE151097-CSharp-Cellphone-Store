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

        public User Login(string user, string pass)
        {
            return context.Users.Where(x => x.Username.Equals(user) && x.Password.Equals(pass)).FirstOrDefault();
        }
    }
}
