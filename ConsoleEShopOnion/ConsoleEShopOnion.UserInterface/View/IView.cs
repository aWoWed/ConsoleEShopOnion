namespace ConsoleEShopOnion.UserInterface.View
{
    public interface IView
    {
        void ShowMessage(string message);
        int ShowMainMenu();
        (string, string) ShowLogin();
        string ShowOldPassword();
        string ShowNewPassword();
        (string, string) ShowRegistration();
        int ShowGuestOptions();
        int ShowUserOptions();
        int ShowAdminOptions();
        int ShowUserById();
        int ShowProductById();
        int ShowOrderId();
        string ShowProductByName();
        (string, string, string, decimal) ShowProduct();
        (string, string, string, decimal) ShowChangedProduct();
        int ShowOrderStatusAdmin();
    }
}
