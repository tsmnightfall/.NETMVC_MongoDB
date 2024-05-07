using System.Collections.Generic;

namespace DemoMongoDB.Models.BusinessModels
{
    public interface IRepository<T,K>
    {
        T GetById(K id);
        List<T> GetAll();
        bool Insert(T entity);
        bool Update(T entity);
        bool Delete(K id);
    }
}
