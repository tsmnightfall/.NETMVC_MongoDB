namespace DemoMongoDB.Models.DataModels
{
    public class ProductViewModel
    {
        public string _id { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public string BrandName { get; set; }
        public double Price { get; set; }
        public string Picture { get; set; }
        public string Description { get; set; }
    }
}
