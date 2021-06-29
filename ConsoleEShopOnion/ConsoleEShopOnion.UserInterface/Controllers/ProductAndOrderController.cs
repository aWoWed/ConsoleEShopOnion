using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleEShopOnion.Domain;
using ConsoleEShopOnion.Domain.Models;
using ConsoleEShopOnion.Domain.Repositories.Abstract;
using ConsoleEShopOnion.UserInterface.View;

namespace ConsoleEShopOnion.UserInterface.Controllers
{
    public class ProductAndOrderController
    {
        public User User { get; }
        private readonly IView _view;
        private readonly IProductsRepository _products;
        private readonly IOrdersRepository _orders;
        private readonly IUsersRepository _users;
        private protected List<string> Cart { get; set; } = new List<string>();
        public ProductAndOrderController(IView view, DataManager dataManager, User user)
        {
            _view = view;
            User = user;
            _users = dataManager.Users;
            _products = dataManager.Products;
            _orders = dataManager.Orders;
            _users.Load("users.json");
            _products.Load("products.json");
            _orders.Load("orders.json");
        }

        #region RolesStart

        public void GuestStart()
        {
            var res = _view.ShowGuestOptions();
            switch (res)
            {
                case 1:
                    ShowAllProducts();
                    GuestStart();
                    break;
                case 2:
                    FindProductByName();
                    GuestStart();
                    break;
                case 3:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void UserStart()
        {
            var res = _view.ShowUserOptions();
            switch (res)
            {
                case 1:
                    ShowAllProducts();
                    UserStart();
                    break;
                case 2:
                    FindProductByName();
                    UserStart();
                    break;
                case 3:
                    AddToCart();
                    UserStart();
                    break;
                case 4:
                    OrderValidation();
                    UserStart();
                    break;
                case 5:
                    CancelOrder();
                    UserStart();
                    break;
                case 6:
                    ShowAllMyOrders();
                    UserStart();
                    break;
                case 7:
                    SetReceivedStatus();
                    UserStart();
                    break;
                case 8:
                    ChangeMyPassword();
                    UserStart();
                    break;
                case 9:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void AdminStart()
        {
            var res = _view.ShowAdminOptions();
            switch (res)
            {
                case 1:
                    ShowAllProducts();
                    AdminStart();
                    break;
                case 2:
                    FindProductByName();
                    AdminStart();
                    break;
                case 3:
                    AddToCart();
                    AdminStart();
                    break;
                case 4:
                    OrderValidation();
                    AdminStart();
                    break;
                case 5:
                    ShowAllUsers();
                    AdminStart();
                    break;
                case 6:
                    ChangeUserInfo();
                    AdminStart();
                    break;
                case 7:
                    CreateNewProduct();
                    AdminStart();
                    break;
                case 8:
                    ChangeProductInfo();
                    AdminStart();
                    break;
                case 9:
                    ChangeOrderStatus();
                    AdminStart();
                    break;
                case 10:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion

        #region Products

        private void FindProductByName()
        {
            ShowAllProducts();

            var product = _view.ShowProductByName();

            if (_products.GetProductByName(product) == null)
            {
                _view.ShowMessage("This product with such name does not exist!");
                return;
            }

            _view.ShowMessage(_products.GetProductByName(product).ToString());
        }

        private void ShowAllProducts()
        {
            if (_products.GetAll().Count == 0)
            {
                _view.ShowMessage("The products list is empty!");
                return;
            }

            foreach (var product in _products.GetAll())
            {
                _view.ShowMessage(product.ToString());
            }
        }

        private void CreateNewProduct()
        {
            var (name, category, description, price) = _view.ShowProduct();

            var product = new Product
            {
                Id = _products.GetAll().Count > 0 ? _products.GetAll().Values.OrderBy(prod => prod.Id).Last().Id + 1 : 1,
                Name = name,
                Category = category,
                Description = description,
                Price = price
            };

            if (_products.GetProductByNameCategoryDescriptionPrice(name, category, description, price) != null)
            {
                _view.ShowMessage("This product already exists!");
                CreateNewProduct();
                return;
            }

            _products.Save(product);
            _view.ShowMessage("Product Added!");
            _view.ShowMessage(product.ToString());
        }

        private void ChangeProductInfo()
        {
            ShowAllProducts();

            try
            {
                var idProduct = _view.ShowProductById();
                var product = _products.GetById(idProduct);

                if (product == null)
                {
                    _view.ShowMessage("Product with such id does not exist!");
                    ChangeProductInfo();
                    return;
                }

                var (name, category, description, price) = _view.ShowChangedProduct();

                if (_products.GetProductByNameCategoryDescriptionPrice(name, category, description, price) != null)
                {
                    _view.ShowMessage("This product already exists!");
                    ChangeProductInfo();
                    return;
                }

                product.Name = name;
                product.Category = category;
                product.Description = description;
                product.Price = price;

                _products.Save(product);
                _view.ShowMessage("Product Changed!");
                _view.ShowMessage(product.ToString());
            }
            catch (KeyNotFoundException)
            {
                _view.ShowMessage("Not valid Id!");
            }
        }

        #endregion

        #region Orders

        private void AddToCart()
        {

            ShowAllProducts();

            var (name, category, description, price) = _view.ShowProduct();

            if (_products.GetProductByNameCategoryDescriptionPrice(name, category, description, price) != null)
            {
                Cart.Add(name);
                _view.ShowMessage("Added to cart");
                return;
            }

            _view.ShowMessage("This item does not exist!");
        }

        private void OrderValidation()
        {
            ShowAllProducts();

            var productIds = Cart;

            var order = new Order
            {
                Id = _orders.GetAll().Count > 0 ? _orders.GetAll().Values.OrderBy(ord => ord.Id).Last().Id + 1 : 1,
                UserId = User.Id,
                ProductsIds = productIds
            };

            _orders.Save(order);
            _view.ShowMessage("Order Added!");
            _view.ShowMessage(order.ToString());
            Cart = new List<string>();
        }

        private void CancelOrder()
        {
            var userOrders = _orders.GetOrdersByUserId(User.Id);

            if (userOrders.Count == 0)
            {
                _view.ShowMessage("The order list is empty!");
                return;
            }

            try
            {
                var orderId = _view.ShowOrderId();
                var order = _orders.GetById(orderId);

                if (order == null)
                {
                    _view.ShowMessage("Order with such id does not exist!");
                    CancelOrder();
                    return;
                }

                if (order.OrderStatus == OrderStatus.Received ||
                    order.OrderStatus == OrderStatus.Done ||
                    order.OrderStatus == OrderStatus.CanceledByUser ||
                    order.OrderStatus == OrderStatus.CanceledByAdmin)
                {
                    _view.ShowMessage("You can't cancel this order!");
                    CancelOrder();
                    return;
                }

                order.OrderStatus = OrderStatus.CanceledByUser;
                _orders.Save(order);
                _view.ShowMessage("Order was canceled");
            }
            catch (KeyNotFoundException)
            {
                _view.ShowMessage("Not valid Id!");
            }
        }

        private void ShowAllMyOrders()
        {
            var userOrders = _orders.GetOrdersByUserId(User.Id);

            if (userOrders.Count == 0)
            {
                _view.ShowMessage("The order list is empty!");
                return;
            }

            foreach (var order in userOrders)
            {
                _view.ShowMessage(order.ToString());
            }
        }

        private void ShowAllOrders()
        {
            if (_orders.GetAll().Count == 0)
            {
                _view.ShowMessage("The orders list is empty!");
                return;
            }

            foreach (var order in _orders.GetAll())
            {
                _view.ShowMessage(order.ToString());
            }
        }

        private void SetReceivedStatus()
        {

            ShowAllMyOrders();

            try
            {
                var orderId = _view.ShowOrderId();
                var order = _orders.GetById(orderId);

                if (order == null)
                {
                    _view.ShowMessage("Order with such id does not exist!");
                    SetReceivedStatus();
                    return;
                }

                order.OrderStatus = OrderStatus.Received;
                _orders.Save(order);
                _view.ShowMessage("Order was received");
            }
            catch (KeyNotFoundException)
            {
                _view.ShowMessage("Not valid Id!");
            }
        }

        private void ChangeOrderStatus()
        {
            ShowAllOrders();

            try
            {
                var orderId = _view.ShowOrderId();
                var order = _orders.GetById(orderId);

                if (order == null)
                {
                    _view.ShowMessage("Order with such id does not exist!");
                    ChangeOrderStatus();
                    return;
                }

                if (order.OrderStatus == OrderStatus.CanceledByUser)
                {
                    _view.ShowMessage("You can't cancel this order!");
                    return;
                }

                order.OrderStatus = _view.ShowOrderStatusAdmin() switch
                {
                    1 => OrderStatus.New,
                    2 => OrderStatus.CanceledByAdmin,
                    3 => OrderStatus.ReceivedPayment,
                    4 => OrderStatus.Sent,
                    5 => OrderStatus.Received,
                    6 => OrderStatus.Done
                };
                _orders.Save(order);
                _view.ShowMessage("Order status was changed");
            }
            catch (KeyNotFoundException)
            {
                _view.ShowMessage("Not valid Id!");
            }

        }

        #endregion

        #region Users

        private void ChangeMyPassword()
        {
            var oldPassword = _view.ShowOldPassword();

            if (oldPassword != User.Password)
            {
                _view.ShowMessage("Your password is wrong!");
                ChangeMyPassword();
                return;
            }

            var password = _view.ShowNewPassword();

            User.Password = password;

            _users.Save(User);

            _view.ShowMessage("Password changed");
        }

        private void ShowAllUsers()
        {
            if (_users.GetAll().Count == 0)
            {
                _view.ShowMessage("The users list is empty!");
                return;
            }

            foreach (var user in _users.GetAll())
            {
                _view.ShowMessage(user.ToString());
            }
        }

        private void ChangeUserInfo()
        {
            ShowAllUsers();

            try
            {
                var userId = _view.ShowUserById();
                var user = _users.GetById(userId);

                if (user == null)
                {
                    _view.ShowMessage("User with such id does not exist!");
                    ChangeUserInfo();
                    return;
                }

                var (login, password) = _view.ShowLogin();

                user.Login = login;
                user.Password = password;

                _users.Save(user);
                _view.ShowMessage("User info was changed");
            }
            catch (KeyNotFoundException)
            {
                _view.ShowMessage("Not valid Id!");
            }
        }

        #endregion
    }
}
