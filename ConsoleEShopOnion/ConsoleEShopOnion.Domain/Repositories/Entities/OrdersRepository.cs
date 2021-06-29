using System.Collections.Generic;
using System.Linq;
using ConsoleEShopOnion.Domain.Models;
using ConsoleEShopOnion.Domain.Repositories.Abstract;
using ConsoleEShopOnion.Service;
using ConsoleEShopOnion.Service.Services.Abstract;
using ConsoleEShopOnion.Service.Services.Entities;

namespace ConsoleEShopOnion.Domain.Repositories.Entities
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly IDictionary<int, Order> _orders;
        private readonly IFileService _fileService;
        private readonly IJsonService _jsonService;

        public OrdersRepository(AppContext context, ServiceManager serviceManager)
        {
            _fileService = serviceManager.FileService;
            _jsonService = serviceManager.JsonService;
            _orders = context.Orders;
        }
        /// <summary>
        /// Loading orders file
        /// </summary>
        /// <param name="fileName"></param>
        public void Load(string fileName)
        {
            var orders = _jsonService.Deserialize<IEnumerable<Order>>(_fileService.Read(fileName)) ?? new List<Order>();

            foreach (var order in orders)
                Save(order);
        }
        /// <summary>
        /// Write orders to file
        /// </summary>
        /// <param name="fileName"></param>
        public void Write(string fileName) => FileService.Write(fileName, _jsonService.Serialize(_orders.Values));
        /// <summary>
        /// All orders
        /// </summary>
        /// <returns>orders</returns>
        public IDictionary<int, Order> GetAll() => _orders;
        /// <summary>
        /// Order by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>order with current id</returns>
        public Order GetById(int id)
        {
            var order = _orders[id];
            if (order == null) throw new KeyNotFoundException("Order with such Id does not exist.");
            return order;
        }
        /// <summary>
        /// Order by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>order with current user id</returns>
        public Order GetOrderByUserId(int userId) => _orders.Values.FirstOrDefault(order => order.UserId == userId);
        /// <summary>
        /// Orders by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>orders with current user id</returns>
        public List<Order> GetOrdersByUserId(int userId) => _orders.Values.Where(order => order.UserId == userId).ToList();
        /// <summary>
        /// Adds order to collection and to file
        /// </summary>
        /// <param name="order"></param>
        /// <param name="isWrite"></param>
        public void Save(Order order, bool isWrite = true)
        {

            if (_orders.ContainsKey(order.Id))
            {
                _orders[order.Id] = order;
                if (isWrite) Write("orders.json");
                return;
            }

            _orders.Add(order.Id, order);
            if (isWrite) Write("orders.json");

        }

    }
}
