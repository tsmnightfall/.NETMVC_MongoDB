using MongoDB.Bson;

namespace DemoMongoDB.Models.DataModels
{
    public class Category
    {
        public int _id { get; set; }
        public string CategoryName { get; set; }
        public int Status { get; set; }
    }
}
