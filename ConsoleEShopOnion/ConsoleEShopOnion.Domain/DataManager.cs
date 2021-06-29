using ConsoleEShopOnion.Domain.Models;
using ConsoleEShopOnion.Domain.Repositories.Abstract;
using ConsoleEShopOnion.Domain.Repositories.Entities;
using ConsoleEShopOnion.Service;

namespace ConsoleEShopOnion.Domain
{
    public class DataManager
    {
        public IUsersRepository Users { get; }
        public IProductsRepository Products { get; }
        public IOrdersRepository Orders { get; }

        public DataManager(AppContext context, ServiceManager serviceManager)
        {
            Users = new UsersRepository(context, serviceManager);
            Products = new ProductsRepository(context, serviceManager);
            Orders = new OrdersRepository(context, serviceManager);
        }
    }
}
