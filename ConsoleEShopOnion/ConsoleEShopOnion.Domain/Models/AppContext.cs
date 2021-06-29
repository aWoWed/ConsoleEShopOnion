using System.Collections.Generic;

namespace ConsoleEShopOnion.Domain.Models
{
    public class AppContext
    {
        public IDictionary<int, Product> Products { get; set; }
        public IDictionary<int, Order> Orders { get; set; }
        public IDictionary<int, User> Users { get; set; }

        public AppContext()
        {
            Products = new Dictionary<int, Product>();
            Orders = new Dictionary<int, Order>();
            Users = new Dictionary<int, User>
            {
                { 1,
                    new User
                    {
                        Role = Roles.Admin,
                        Login = "admin",
                        Password = "admin"
                    }
                }
            };
        }
    }
}
