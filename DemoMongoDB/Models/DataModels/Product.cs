using MongoDB.Bson;

namespace DemoMongoDB.Models.DataModels
{
    public class Product
    {
        public string _id { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public double Price { get; set; }
        public string Picture { get; set; }
        public string Description { get; set; }
    }
}
