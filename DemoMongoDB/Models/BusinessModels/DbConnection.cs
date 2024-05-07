using DemoMongoDB.Models.DataModels;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
namespace DemoMongoDB.Models.BusinessModels
{
    public class TestDbContext
    {
        IConfiguration config;
        public TestDbContext(IConfiguration config)
        {
            this.config = config;
        }
        public IMongoDatabase Connection
        {
            get
            {
                var client = new MongoClient(config.GetConnectionString("mongoConnection"));
                var database = client.GetDatabase(config.GetConnectionString("database"));
                return database;
            }
        }
        public IMongoCollection<Category> Categories => Connection.GetCollection<Category>("categories");
    
        public IMongoCollection<Product> Products => Connection.GetCollection<Product>("products");
            
        public IMongoCollection<Brand> Brands => Connection.GetCollection<Brand>("brands");
           
    }
}
