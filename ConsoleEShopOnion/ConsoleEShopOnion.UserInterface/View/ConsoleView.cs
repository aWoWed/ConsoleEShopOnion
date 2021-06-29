using System;

namespace ConsoleEShopOnion.UserInterface.View
{
    public class ConsoleView : IView
    {
        private static T DisplayAndGet<T>(Predicate<T> condition, params string[] messages) where T : IConvertible
        {
            T result;

            foreach (var message in messages)
                Console.WriteLine(message);

            while (true)
            {
                Console.Write("> ");
                var strResult = Console.ReadLine();

                if (typeof(T) == typeof(double))
                    strResult = strResult?.Replace('.', ',');

                try
                {
                    result = (T)((IConvertible)strResult)?.ToType(typeof(T), null);
                    if (condition != null && !condition(result))
                        throw new FormatException();
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Enter in correct format!");
                }
            }

            return result;
        }

        public void ShowMessage(string message) => Console.WriteLine(message);

        public int ShowMainMenu() => DisplayAndGet<int>(res => res == 1 || res == 2 || res == 3 || res == 4,
            "Welcome to the shop!! What are you going to do?", "1 - Login", "2 - Register", "3 - Continue as Guest", "4 - Exit");
        public (string, string) ShowLogin()
        {
            var login = DisplayAndGet<string>(null, "Enter your login");
            var password = DisplayAndGet<string>(null, "Enter your password");
            return (login, password);
        }

        public string ShowOldPassword() => DisplayAndGet<string>(null, "Enter your old password");

        public string ShowNewPassword()
        {
            var newPassword = DisplayAndGet<string>(null, "Enter your new password");
            var newRepeatPassword = DisplayAndGet<string>(null, "Repeat your new password");
            if (newPassword == newRepeatPassword) return newPassword;
            ShowMessage("Passwords are different");
            return ShowNewPassword();
        }

        public (string, string) ShowRegistration()
        {
            var login = DisplayAndGet<string>(null, "Enter your login");
            var password = DisplayAndGet<string>(null, "Enter your password");
            var repeatPassword = DisplayAndGet<string>(null, "Repeat your password");
            if (password == repeatPassword) return (login, password);
            ShowMessage("Passwords are different");
            return ShowRegistration();
        }

        public int ShowGuestOptions() => DisplayAndGet<int>(res => res == 1 || res == 2 || res == 3,
            "Welcome to the shop as a Guest!! What are you going to do?", "1 - Show All Products", "2 - Find Product by name", "3 - Go back to the main menu");

        public string ShowProductByName() => DisplayAndGet<string>(null, "Enter your product name");

        public int ShowProductById() => DisplayAndGet<int>(null, "Enter your product id");


        public (string, string, string, decimal) ShowProduct()
        {
            var name = DisplayAndGet<string>(null, "Enter product name");
            var category = DisplayAndGet<string>(null, "Enter product category");
            var description = DisplayAndGet<string>(null, "Enter product  description");
            var price = DisplayAndGet<decimal>(newPrice => newPrice >= 0, "Enter product price");
            return (name, category, description, price);
        }

        public (string, string, string, decimal) ShowChangedProduct()
        {
            var name = DisplayAndGet<string>(null, "Change product name");
            var category = DisplayAndGet<string>(null, "Change product category");
            var description = DisplayAndGet<string>(null, "Change product  description");
            var price = DisplayAndGet<decimal>(newPrice => newPrice >= 0, "Change product price");
            return (name, category, description, price);
        }

        public int ShowUserOptions() => DisplayAndGet<int>(res => res == 1 || res == 2 || res == 3 || res == 4 || res == 5 || res == 6 || res == 7 || res == 8 || res == 9,
            "Welcome to the shop as a User!! What are you going to do?", "1 - Show All Products",
            "2 - Find Product by name", "3 - Add to Cart", "4 - Order Validation", "5 - Cancel my Order", "6 - My Orders",
            "7 - Order change status to received", "8 - Change my password", "9 - Logout");

        public int ShowOrderId() => DisplayAndGet<int>(null, "Enter your order id");


        public int ShowAdminOptions() => DisplayAndGet<int>(res => res == 1 || res == 2 || res == 3 || res == 4 || res == 5 || res == 6 || res == 7 || res == 8 || res == 9 || res == 10,
            "Welcome to the shop as an Admin!! What are you going to do?", "1 - Show All Products",
            "2 - Find Product by name", "3 - Add to Cart", "4 - Order Validation", "5 - Show users info",
            "6 - Change users info", "7 - Add new Product", "8 - Change Product info", "9 - Change Order status", "10 - Logout");

        public int ShowUserById() => DisplayAndGet<int>(null, "Enter your user id");


        public int ShowOrderStatusAdmin() => DisplayAndGet<int>(
            res => res == 1 || res == 2 || res == 3 || res == 4 || res == 5 || res == 6,
            "All order statuses", "1 - New",
            "2 - CanceledByAdmin", "3 - ReceivedPayment", "4 - Sent", "5 - Received",
            "6 - Done");
    }
}
