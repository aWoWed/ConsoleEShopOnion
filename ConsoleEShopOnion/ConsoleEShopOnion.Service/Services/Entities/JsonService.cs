using ConsoleEShopOnion.Service.Services.Abstract;
using Newtonsoft.Json;

namespace ConsoleEShopOnion.Service.Services.Entities
{
    public class JsonService : IJsonService
    {
        /// <summary>
        /// Deserializes file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns>Deserializes file</returns>
        public static T Deserialize<T>(string json) => JsonConvert.DeserializeObject<T>(json);

        /// <summary>
        /// Serializes file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns>Serializes file</returns>
        public static string Serialize<T>(T obj) => JsonConvert.SerializeObject(obj, Formatting.Indented);

        T IJsonService.Deserialize<T>(string json) => Deserialize<T>(json);
        string IJsonService.Serialize<T>(T obj) => Serialize<T>(obj);
    }
}
