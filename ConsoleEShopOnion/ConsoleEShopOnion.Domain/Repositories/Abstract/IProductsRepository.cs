using ConsoleEShopOnion.Domain.Models;

namespace ConsoleEShopOnion.Domain.Repositories.Abstract
{
    public interface IProductsRepository : IRepository<Product>
    {
        Product GetProductByName(string name);
        Product GetProductByNameCategoryDescriptionPrice(string name, string category, string description, decimal price);
    }
}
