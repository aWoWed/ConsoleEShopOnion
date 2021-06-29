using System.Collections.Generic;
using System.Linq;
using ConsoleEShopOnion.Domain.Models;
using ConsoleEShopOnion.Domain.Repositories.Abstract;
using ConsoleEShopOnion.Service;
using ConsoleEShopOnion.Service.Services.Abstract;
using ConsoleEShopOnion.Service.Services.Entities;

namespace ConsoleEShopOnion.Domain.Repositories.Entities
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly IDictionary<int, Product> _products;
        private readonly IFileService _fileService;
        private readonly IJsonService _jsonService;

        public ProductsRepository(AppContext context, ServiceManager serviceManager)
        {
            _fileService = serviceManager.FileService;
            _jsonService = serviceManager.JsonService;
            _products = context.Products;
        }

        /// <summary>
        /// Loading products file
        /// </summary>
        /// <param name="fileName"></param>
        public void Load(string fileName)
        {
            var products = _jsonService.Deserialize<IEnumerable<Product>>(_fileService.Read(fileName)) ?? new List<Product>();

            foreach (var product in products)
                Save(product);
        }
        /// <summary>
        /// Write products to file
        /// </summary>
        /// <param name="fileName"></param>
        public void Write(string fileName) => FileService.Write(fileName, _jsonService.Serialize(_products.Values));
        /// <summary>
        /// All products
        /// </summary>
        /// <returns>all products</returns>
        public IDictionary<int, Product> GetAll() => _products;
        /// <summary>
        /// Product by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>product by id</returns>
        public Product GetById(int id)
        {
            var product = _products[id];
            if (product == null) throw new KeyNotFoundException("Product with such Id does not exist.");
            return product;
        }
        /// <summary>
        /// product by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns>product with current name</returns>
        public Product GetProductByName(string name) => _products.Values.FirstOrDefault(prod => prod.Name == name);

        /// <summary>
        /// product by name, category, description, price
        /// </summary>
        /// <param name="name"></param>
        /// <param name="category"></param>
        /// <param name="description"></param>
        /// <param name="price"></param>
        /// <returns>product with current name, category, description, price</returns>
        public Product
            GetProductByNameCategoryDescriptionPrice(string name, string category, string description, decimal price) =>
            _products.Values.FirstOrDefault(prod =>
                prod.Name == name && prod.Category == category && prod.Description == description &&
                prod.Price == price);
        /// <summary>
        /// Adds product to collection and to file
        /// </summary>
        /// <param name="product"></param>
        /// <param name="isWrite"></param>
        public void Save(Product product, bool isWrite = true)
        {

            if (_products.ContainsKey(product.Id))
            {
                _products[product.Id] = product;
                if (isWrite) Write("products.json");
                return;
            }

            _products.Add(product.Id, product);
            if (isWrite) Write("products.json");
        }
    }
}
