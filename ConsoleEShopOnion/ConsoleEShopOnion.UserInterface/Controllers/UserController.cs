using System;
using System.Linq;
using ConsoleEShopOnion.Domain;
using ConsoleEShopOnion.Domain.Models;
using ConsoleEShopOnion.Domain.Repositories.Abstract;
using ConsoleEShopOnion.UserInterface.View;

namespace ConsoleEShopOnion.UserInterface.Controllers
{
    public class UserController
    {
        private readonly IView _view;
        private readonly IUsersRepository _users;
        public UserController(IView view, DataManager dataManager)
        {
            _view = view;
            _users = dataManager.Users;
            _users.Load("users.json");
        }

        #region Start

        public User Start() =>
            _view.ShowMainMenu() switch
            {
                1 => Login(),
                2 => Register(),
                3 => new User(),
                4 => null,
                _ => throw new ArgumentOutOfRangeException()
            };

        private User Login()
        {
            var (login, password) = _view.ShowLogin();
            var user = _users.GetUserByLoginAndPassword(login, password);
            if (user != null) return user;
            _view.ShowMessage("Login or password is wrong!");
            return Start();
        }

        private User Register()
        {
            var (login, password) = _view.ShowRegistration();
            if (_users.GetUserByLogin(login) != null)
            {
                _view.ShowMessage("This login already exists!");
                return Start();
            }

            var user = new User
            {
                Id = _users.GetAll().Values.OrderBy(usr => usr.Id).Last().Id + 1,
                Login = login,
                Password = password,
                Role = Roles.User
            };

            _users.Save(user);
            return Start();
        }

        #endregion

    }
}
