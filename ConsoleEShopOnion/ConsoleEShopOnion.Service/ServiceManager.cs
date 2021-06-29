using ConsoleEShopOnion.Service.Services.Abstract;
using ConsoleEShopOnion.Service.Services.Entities;

namespace ConsoleEShopOnion.Service
{
    public class ServiceManager
    {
        public IFileService FileService { get; }
        public IJsonService JsonService { get; }

        public ServiceManager()
        {
            FileService = new FileService();
            JsonService = new JsonService();
        }
    }
}
