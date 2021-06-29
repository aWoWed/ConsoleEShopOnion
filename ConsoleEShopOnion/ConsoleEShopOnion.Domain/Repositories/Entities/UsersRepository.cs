using System.Collections.Generic;
using System.Linq;
using ConsoleEShopOnion.Domain.Models;
using ConsoleEShopOnion.Domain.Repositories.Abstract;
using ConsoleEShopOnion.Service;
using ConsoleEShopOnion.Service.Services.Abstract;
using ConsoleEShopOnion.Service.Services.Entities;

namespace ConsoleEShopOnion.Domain.Repositories.Entities
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IDictionary<int, User> _users;
        private readonly IFileService _fileService;
        private readonly IJsonService _jsonService;

        public UsersRepository(AppContext context, ServiceManager serviceManager)
        {
            _fileService = serviceManager.FileService;
            _jsonService = serviceManager.JsonService;
            _users = context.Users;
        }
        /// <summary>
        /// Loading users file
        /// </summary>
        /// <param name="fileName"></param>
        public void Load(string fileName)
        {
            var users = _jsonService.Deserialize<IEnumerable<User>>(_fileService.Read(fileName)) ?? new List<User>();

            foreach (var user in users)
                Save(user);
        }
        /// <summary>
        /// Write users to file
        /// </summary>
        /// <param name="fileName"></param>
        public void Write(string fileName) => FileService.Write(fileName, _jsonService.Serialize(_users.Values));
        /// <summary>
        /// All users
        /// </summary>
        /// <returns></returns>
        public IDictionary<int, User> GetAll() => _users;
        /// <summary>
        /// User by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>user by id</returns>
        public User GetById(int id)
        {
            var user = _users[id];
            if (user == null) throw new KeyNotFoundException("User with such Id does not exist.");
            return user;
        }
        /// <summary>
        /// User by login
        /// </summary>
        /// <param name="login"></param>
        /// <returns>user with current login</returns>
        public User GetUserByLogin(string login) => _users.FirstOrDefault(user => user.Value.Login == login).Value;
        /// <summary>
        /// User by login, password
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns>user with current login, password</returns>
        public User GetUserByLoginAndPassword(string login, string password) => _users.FirstOrDefault(user => user.Value.Login == login && user.Value.Password == password).Value;
        /// <summary>
        /// Adds user to collection and to file
        /// </summary>
        /// <param name="user"></param>
        /// <param name="isWrite"></param>
        public void Save(User user, bool isWrite = true)
        {

            if (_users.ContainsKey(user.Id))
            {
                _users[user.Id] = user;
                if (isWrite) Write("users.json");
                return;
            }


            _users.Add(user.Id, user);
            if (isWrite) Write("users.json");
        }

    }
}
