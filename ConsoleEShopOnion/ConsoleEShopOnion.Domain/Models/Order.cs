using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ConsoleEShopOnion.Domain.Models
{
    /// <summary>
    /// Orders Class
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Order id
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }
        /// <summary>
        /// Order user_id
        /// </summary>
        [JsonProperty("user_id")]
        public int UserId { get; set; }
        /// <summary>
        /// Product names
        /// </summary>
        [JsonProperty("products_ids")]
        public IEnumerable<string> ProductsIds { get; set; }
        /// <summary>
        /// Order Date = current date and time
        /// </summary>
        [JsonProperty("date_ordered")]
        public DateTime DateOrdered { get; }
        /// <summary>
        /// Order status
        /// </summary>
        [JsonProperty("order_status")]
        [JsonConverter(typeof(StringEnumConverter))]
        public OrderStatus OrderStatus { get; set; }

        public Order(IEnumerable<string>? items = null, int? id = null, int? userId = null)
        {
            DateOrdered = DateTime.Now;
            OrderStatus = OrderStatus.New;

            ProductsIds = items ?? new List<string>();

            Id = id ?? 1;
            UserId = userId ?? 1;
        }

        public override string ToString() =>
            $"ID: {Id}\nUser ID: {UserId}\nDate Ordered: {DateOrdered:hh:mm:ss dd-MM-yyyy}\nOrder Status: {OrderStatus}\nProducts:\n{string.Join("\n", ProductsIds)}";
    }
}
