using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PR6_Lashkov.Models
{
    internal class DbHelper
    {
        private static DrinkFactoryEntities _context;

        public static DrinkFactoryEntities GetContext()
        {
            if (_context == null)
                _context = new DrinkFactoryEntities();
            return _context;
        }

        public static void CreateUser(Users user)
        {
            var context = GetContext();
            context.Users.Add(user);
            context.SaveChanges();
        }
    }
}
