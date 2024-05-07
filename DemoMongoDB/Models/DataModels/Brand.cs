using MongoDB.Bson;

namespace DemoMongoDB.Models.DataModels
{
    public class Brand
    {
        public int _id { get; set; }
        public string BrandName { get; set; }
        public int Status { get; set; }
    }
}
