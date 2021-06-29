using Newtonsoft.Json;

namespace ConsoleEShopOnion.Domain.Models
{
    /// <summary>
    /// User class
    /// </summary>
    public class User
    {
        /// <summary>
        /// User id
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }
        /// <summary>
        /// User Role
        /// </summary>
        [JsonProperty("role")]
        public Roles Role { get; set; }
        /// <summary>
        /// User login
        /// </summary>
        [JsonProperty("login")]
        public string Login { get; set; }
        /// <summary>
        /// User password
        /// </summary>
        [JsonProperty("password")]
        public string Password { get; set; }

        public User(int? id = null, Roles? role = null, string? login = null, string? password = null)
        {
            Id = id ?? 1;
            Role = role ?? Roles.Guest;
            Login = login ?? "Default Login";
            Password = password ?? "Default Password";
        }

        public override string ToString() =>
            $"ID: {Id}\nRole: {Role}\nLogin: {Login}\nPassword: {Password}";
    }
}
