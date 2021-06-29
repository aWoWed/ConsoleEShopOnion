using ConsoleEShopOnion.Domain;
using ConsoleEShopOnion.Domain.Models;
using ConsoleEShopOnion.Service;
using ConsoleEShopOnion.UserInterface.Controllers;
using ConsoleEShopOnion.UserInterface.View;

namespace ConsoleEShopOnion
{
    class Program
    {
        public static void Main(string[] args)
        {
            var console = new ConsoleView();

            var appContext = new AppContext();
            var serviceManager = new ServiceManager();
            var dataManager = new DataManager(appContext, serviceManager);

            var controller = new MainController(console, dataManager);

            controller.Start();
        }
    }
}
