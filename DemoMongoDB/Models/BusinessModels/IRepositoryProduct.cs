using DemoMongoDB.Models.DataModels;
using System.Collections.Generic;

namespace DemoMongoDB.Models.BusinessModels
{
    public interface IRepositoryProduct:IRepository<Product,string>
    {
        List<Product> SearchPaging(string name,int page, int pageSize, out long totalpage);
        List<ProductViewModel> GetProductFull();
        List<Product> Paging(int page, int pageSize, out long totalpage);
    }
}
