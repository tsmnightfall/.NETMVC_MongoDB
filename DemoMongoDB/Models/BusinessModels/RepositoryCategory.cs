using DemoMongoDB.Models.DataModels;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;

namespace DemoMongoDB.Models.BusinessModels
{
    public class RepositoryCategory : IRepositoryCategory
    {
        TestDbContext db;
        public RepositoryCategory(TestDbContext db)
        {
            this.db = db;
        }
        public bool Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<Category> GetAll()
        {
            var categoryCollection = db.Categories;
            var categories = categoryCollection.Find(FilterDefinition<Category>.Empty).ToList();
            return categories;
        }

        public Category GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public bool Insert(Category entity)
        {
            throw new System.NotImplementedException();
        }

        public bool Update(Category entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
