using System.Collections.Generic;
using ConsoleEShopOnion.Domain;
using ConsoleEShopOnion.Domain.Models;
using ConsoleEShopOnion.Domain.Repositories.Entities;
using ConsoleEShopOnion.Service;
using Xunit;
using Xunit.Abstractions;

namespace ConsoleEShopOnion.Tests
{
    public class Tests
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly AppContext _appContext = new AppContext();
        private readonly ServiceManager _serviceManager = new ServiceManager();
        private readonly DataManager _dataManager;

        public Tests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _dataManager = new DataManager(_appContext, _serviceManager);
        }

        [Fact]
        public void IsSingleton()
        {
            var newProductsRepository = new ProductsRepository(_appContext, _serviceManager);
            var product = new Product() { Name = "1", Category = "1", Description = "1", Price = 1 };

            _dataManager.Products.Save(product, false);

            Assert.Equal(_dataManager.Products.GetAll().Count, newProductsRepository.GetAll().Count);
            Assert.Same(_dataManager.Products.GetProductByName(product.Name), newProductsRepository.GetProductByName(product.Name));
        }

        [Fact]
        public void GetAllUsers_UsersDictionary()
        {
            var users = _dataManager.Users.GetAll();

            Assert.IsType<Dictionary<int, User>>(users);
        }

        [Fact]
        public void GetAllProducts_ProductsDictionary()
        {
            var product = _dataManager.Products.GetAll();

            Assert.IsType<Dictionary<int, Product>>(product);
        }

        [Fact]
        public void GetAllOrders_OrdersDictionary()
        {
            var orders = _dataManager.Orders.GetAll();

            Assert.IsType<Dictionary<int, Order>>(orders);
        }

        #region Users

        [Theory]
        [InlineData("admin", "admin")]
        [InlineData("aaa", "aaa")]
        public void SaveUser_WorksCorrectly(string login, string password)
        {
            var user = new User() { Login = login, Password = password };

            _dataManager.Users.Save(user, false);

            Assert.Contains(user, _dataManager.Users.GetAll().Values);
        }

        [Theory]
        [InlineData(Roles.Admin, "admin", "admin")]
        [InlineData(Roles.User, "aaa", "aaa")]
        public void GetUserById_WorksCorrectly(Roles role, string login, string password)
        {
            var user = new User() { Login = login, Password = password, Role = role };

            _dataManager.Users.Save(user, false);

            Assert.Equal(user, _dataManager.Users.GetById(user.Id));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-3)]
        [InlineData(1000)]
        public void GetUserById_ExceptionThrown(int id)
        {
            Assert.Throws<KeyNotFoundException>(() => _dataManager.Users.GetById(id));
        }

        [Theory]
        [InlineData(Roles.Admin, "admin", "admin")]
        [InlineData(Roles.User, "aaa", "aaa")]
        public void GetUserByLogin_WorksCorrectly(Roles role, string login, string password)
        {
            var user = new User() { Login = login, Password = password, Role = role };

            _dataManager.Users.Save(user, false);

            Assert.Equal(user, _dataManager.Users.GetUserByLogin(login));
        }

        [Theory]
        [InlineData(Roles.Admin, "admin", "admin")]
        [InlineData(Roles.User, "aaa", "aaa")]
        public void GetUserByLoginAndPassword_WorksCorrectly(Roles role, string login, string password)
        {
            var user = new User() { Login = login, Password = password, Role = role };

            _dataManager.Users.Save(user, false);

            Assert.Equal(user, _dataManager.Users.GetUserByLoginAndPassword(login, password));
        }


        #endregion

        #region Products

        [Theory]
        [InlineData("1", "1", "1", 1)]
        [InlineData("2", "2", "2", 2)]
        public void SaveProduct_WorksCorrectly(string name, string category, string description, decimal price)
        {

            var product = new Product() { Name = name, Category = category, Description = description, Price = price };

            _dataManager.Products.Save(product, false);

            Assert.Contains(product, _dataManager.Products.GetAll().Values);
        }

        [Theory]
        [InlineData("1", "1", "1", 1)]
        [InlineData("2", "2", "2", 2)]
        public void GetProductById_WorksCorrectly(string name, string category, string description, decimal price)
        {

            var product = new Product() { Name = name, Category = category, Description = description, Price = price };

            _dataManager.Products.Save(product, false);

            Assert.Equal(product, _dataManager.Products.GetById(product.Id));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-3)]
        [InlineData(200)]
        public void GetProductById_ExceptionThrown(int id)
        {
            Assert.Throws<KeyNotFoundException>(() => _dataManager.Products.GetById(id));
        }

        [Theory]
        [InlineData("1", "1", "1", 1)]
        [InlineData("2", "2", "2", 2)]
        public void GetProductByName_WorksCorrectly(string name, string category, string description, decimal price)
        {

            var product = new Product() { Name = name, Category = category, Description = description, Price = price };

            _dataManager.Products.Save(product, false);

            Assert.Equal(product, _dataManager.Products.GetProductByName(name));
        }

        [Theory]
        [InlineData("1", "1", "1", 1)]
        [InlineData("2", "2", "2", 2)]
        public void GetProductByNameCategoryDescriptionPrice_WorksCorrectly(string name, string category, string description, decimal price)
        {
            var product = new Product() { Name = name, Category = category, Description = description, Price = price };

            _dataManager.Products.Save(product, false);

            Assert.Equal(product, _dataManager.Products.GetProductByNameCategoryDescriptionPrice(name, category, description, price));
        }

        #endregion

        #region Orders

        [Theory]
        [InlineData(1, new[] { "1", "product" })]
        [InlineData(2, new[] { "Prod1", "2" })]
        public void SaveOrder_WorksCorrectly(int userId, string[] productIds)
        {
            var order = new Order() { UserId = userId, ProductsIds = productIds };

            _dataManager.Orders.Save(order, false);

            Assert.Contains(order, _dataManager.Orders.GetAll().Values);
        }

        [Theory]
        [InlineData(1, new[] { "1", "product" })]
        [InlineData(2, new[] { "Prod1", "2" })]
        public void GetOrderById_WorksCorrectly(int userId, string[] productIds)
        {
            var order = new Order() { UserId = userId, ProductsIds = productIds };

            _dataManager.Orders.Save(order, false);

            Assert.Equal(order, _dataManager.Orders.GetById(order.Id));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-3)]
        [InlineData(1000)]
        public void GetOrderById_ExceptionThrown(int id)
        {
            Assert.Throws<KeyNotFoundException>(() => _dataManager.Orders.GetById(id));
        }

        [Theory]
        [InlineData(1, new[] { "1", "product" })]
        [InlineData(2, new[] { "Prod1", "2" })]
        public void GetOrderByUserId_WorksCorrectly(int userId, string[] productIds)
        {
            var order = new Order() { UserId = userId, ProductsIds = productIds };

            _dataManager.Orders.Save(order, false);

            Assert.Equal(order, _dataManager.Orders.GetOrderByUserId(userId));
        }

        [Theory]
        [InlineData(1, new[] { "1", "product" })]
        [InlineData(2, new[] { "Prod1", "2" })]
        public void GetOrdersByUserId_WorksCorrectly(int userId, string[] productIds)
        {
            var order = new Order() { UserId = userId, ProductsIds = productIds };
            var newOrderRepository = new List<Order> { order };

            _dataManager.Orders.Save(order, false);

            Assert.Equal(newOrderRepository, _dataManager.Orders.GetOrdersByUserId(userId));
        }


        #endregion
    }
}
