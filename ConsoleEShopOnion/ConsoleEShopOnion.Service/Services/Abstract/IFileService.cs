namespace ConsoleEShopOnion.Service.Services.Abstract
{
    public interface IFileService
    {
        string Read(string fileName);

        void Write(string fileName, string text);
    }
}
