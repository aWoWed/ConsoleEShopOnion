using ConsoleEShopOnion.Domain.Models;

namespace ConsoleEShopOnion.Domain.Repositories.Abstract
{
    public interface IUsersRepository : IRepository<User>
    {
        User GetUserByLogin(string login);
        User GetUserByLoginAndPassword(string login, string password);
    }
}
