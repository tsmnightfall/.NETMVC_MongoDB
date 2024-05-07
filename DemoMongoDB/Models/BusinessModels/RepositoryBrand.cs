using DemoMongoDB.Models.DataModels;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;

namespace DemoMongoDB.Models.BusinessModels
{
    public class RepositoryBrand : IRepositoryBrand
    {
        TestDbContext db;
        public RepositoryBrand(TestDbContext db)
        {
            this.db = db;
        }
        public bool Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<Brand> GetAll()
        {
            var brandCollection = db.Brands;
            var brands = brandCollection.Find(FilterDefinition<Brand>.Empty).ToList();
            return brands;
        }

        public Brand GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public bool Insert(Brand entity)
        {
            throw new System.NotImplementedException();
        }

        public bool Update(Brand entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
