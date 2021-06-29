using System.Collections.Generic;
using ConsoleEShopOnion.Domain.Models;

namespace ConsoleEShopOnion.Domain.Repositories.Abstract
{
    public interface IOrdersRepository : IRepository<Order>
    {
        Order GetOrderByUserId(int userId);
        List<Order> GetOrdersByUserId(int userId);
    }
}
