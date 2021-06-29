using Newtonsoft.Json;

namespace ConsoleEShopOnion.Domain.Models
{
    /// <summary>
    /// Product class
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Product id
        /// </summary>
        [JsonProperty("id")] public int Id { get; set; }
        /// <summary>
        /// Product name
        /// </summary>
        [JsonProperty("name")] public string Name { get; set; }
        /// <summary>
        /// Product category
        /// </summary>
        [JsonProperty("category")] public string Category { get; set; }
        /// <summary>
        /// Product description
        /// </summary>
        [JsonProperty("description")] public string Description { get; set; }
        /// <summary>
        /// Product price
        /// </summary>
        [JsonProperty("price")] public decimal Price { get; set; }

        public Product(int? id = null, string? name = null, string? category = null, string? description = null,
            decimal? price = null)
        {
            Id = id ?? 1;
            Name = name ?? "Default name";
            Category = category ?? "Default category";
            Description = description ?? "Default description";
            Price = price ?? 0;
        }

        public override string ToString() =>
            $"ID: {Id}\nName: {Name}\nCategory: {Category}\nDescription: {Description}\nPrice: {Price} UAH";
    }
}
