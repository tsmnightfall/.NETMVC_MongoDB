using DemoMongoDB.Models.DataModels;
using Microsoft.Net.Http.Headers;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Net.WebSockets;

namespace DemoMongoDB.Models.BusinessModels
{
    public class RepositoryProduct : IRepositoryProduct
    {
        TestDbContext db;
        public RepositoryProduct(TestDbContext db)
        {
            this.db = db;
        }
        public bool Delete(string id)
        {
            var product = db.Products.DeleteOne(x => x._id.Equals(id));
            return true;
        }

        public List<Product> GetAll()
        {
            var productCollection = db.Products;
            var products = productCollection.Find(FilterDefinition<Product>.Empty).ToList();
            return products;
        }
        public List<ProductViewModel> GetProductFull()
        {
            BsonDocument[] lookup = new BsonDocument[2]
           {
                new BsonDocument
                {
                    { "$lookup", new BsonDocument{
                        { "from", "categories" },
                        { "localField", "CategoryId" },
                        { "foreignField", "_id" },
                        { "as", "categories" }
                        }
                    }
                },
                 new BsonDocument {
                     { "$lookup", new BsonDocument{
                        { "from", "brands" },
                        { "localField", "BrandId" },
                        { "foreignField", "_id" },
                        { "as", "brands" }
                    }
                     }
                }
           };
            var products = db.Products.Aggregate<BsonDocument>(lookup).ToList();
            var data = new List<ProductViewModel>();
            foreach (var e in products)
            {
                var p = new ProductViewModel();
                p._id = e["_id"].ToString();
                p.ProductName = e["ProductName"].ToString();
                p.Price = e["Price"].ToDouble();
                p.CategoryName = e["categories"].AsBsonArray[0]["CategoryName"].ToString();
                p.BrandName = e["brands"].AsBsonArray[0]["BrandName"].ToString();
                p.Description = e["Description"].ToString();
                p.Picture = e["Picture"].ToString();
                data.Add(p);
            }
            return data;
        }
        public Product GetById(string id)
        {
            var p= db.Products.Find(x => x._id.Equals(id)).FirstOrDefault();
            return p;
        }

        public bool Insert(Product entity)
        {
            entity.Picture = entity.Picture ?? "";
            entity.Description = entity.Description ?? "";
            db.Products.InsertOne(entity);
            return true;
        }


        public bool Update(Product entity)
        {
            var p = Builders<Product>.Update.Set("ProductName", entity.ProductName)
                .Set("Price",entity.Price)
                .Set("CategoryId",entity.CategoryId)
                .Set("BrandId",entity.BrandId)
                .Set("Picture",entity.Picture??"")
                .Set("Description",entity.Description??"");
            db.Products.UpdateOne(x => x._id.Equals(entity._id), p);
            return true;
        }

        public List<Product> Paging(int page, int pageSize,out long totalpage)
        {
            int skip = pageSize * (page - 1);
            long rows = db.Products.CountDocuments(FilterDefinition<Product>.Empty);
            totalpage = rows % pageSize == 0 ? rows / pageSize : (rows / pageSize) + 1;
            return db.Products.Find(FilterDefinition<Product>.Empty).Skip(skip).Limit(pageSize).ToList();
        }

        public List<Product> SearchPaging(string name, int page, int pageSize, out long totalpage)
        {
            int skip = pageSize * (page - 1) ;
            long rows = db.Products.CountDocuments(x => x.ProductName.ToLower().Contains(name.ToLower()));
            totalpage = rows % pageSize == 0 ? rows / pageSize : (rows / pageSize) + 1;
            return db.Products.Find(x=>x.ProductName.ToLower().Contains(name.ToLower())).Skip(skip).Limit(pageSize).ToList();
        }
    }
}
