using System.Collections.Generic;

namespace ConsoleEShopOnion.Domain.Repositories.Abstract
{
    public interface IRepository<T>
    {
        void Load(string fileName);
        void Write(string fileName);
        IDictionary<int, T> GetAll();
        T GetById(int id);
        void Save(T obj, bool isWrite = true);
    }
}
