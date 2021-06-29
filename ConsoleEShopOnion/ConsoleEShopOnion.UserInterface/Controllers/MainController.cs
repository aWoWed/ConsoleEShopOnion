using System;
using ConsoleEShopOnion.Domain;
using ConsoleEShopOnion.Domain.Models;
using ConsoleEShopOnion.UserInterface.View;

namespace ConsoleEShopOnion.UserInterface.Controllers
{
    public class MainController
    {
        private readonly IView _view;
        private readonly DataManager _dataManager;

        public MainController(IView view, DataManager dataManager)
        {
            _view = view;
            _dataManager = dataManager;
        }

        public void Start()
        {
            var userController = new UserController(_view, _dataManager);
            var user = userController.Start();
            if (user == null) return;
            var productController = new ProductAndOrderController(_view, _dataManager, user);

            _view.ShowMessage(user.ToString());

            switch (user.Role)
            {
                case Roles.Guest:
                    productController.GuestStart();
                    Start();
                    break;
                case Roles.User:
                    productController.UserStart();
                    Start();
                    break;
                case Roles.Admin:
                    productController.AdminStart();
                    Start();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
