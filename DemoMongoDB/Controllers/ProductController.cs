using DemoMongoDB.Models.BusinessModels;
using DemoMongoDB.Models.DataModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using MongoDB.Bson;
using System.IO;
using System.Threading.Tasks;
using System.Web;

namespace DemoMongoDB.Controllers
{
    public class ProductController : Controller
    {
        IHostEnvironment env;
        IRepositoryProduct repositoryProduct;
        IRepositoryCategory repositoryCategory;
        IRepositoryBrand repositoryBrand;
        public ProductController(IHostEnvironment env, IRepositoryProduct repositoryProduct, IRepositoryCategory repositoryCategory, IRepositoryBrand repositoryBrand)
        {
            this.env= env;
            this.repositoryProduct = repositoryProduct;
            this.repositoryBrand= repositoryBrand;
            this.repositoryCategory = repositoryCategory;
        }
        public IActionResult Index(string name, int page=1)
        {
            page = page < 1 ? 1 : page;
            if (string.IsNullOrEmpty(name))
            {
                long totalpage;
                var data = repositoryProduct.Paging(page,3,out totalpage);
                ViewBag.totalpage = totalpage;
                ViewBag.page = page;
                return View(data);
            }
            else
            {
                long totalpage;
                var data = repositoryProduct.SearchPaging(name, page, 3, out totalpage);
                ViewBag.totalpage = totalpage;
                ViewBag.page = page;
                ViewBag.name = name;
                return View(data);
            }
        }
        public IActionResult GetProductFull()
        {
            var data = repositoryProduct.GetProductFull();
            return View(data);
        }
        public IActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(repositoryCategory.GetAll(),"_id","CategoryName");
            ViewBag.BrandId = new SelectList(repositoryBrand.GetAll(),"_id","BrandName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(Product product, IFormFile pictureupload)
        {
            if (pictureupload.Length > 0)
            {
                var filePath = Path.Combine("wwwroot", "images/" + pictureupload.FileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    await pictureupload.CopyToAsync(stream);
                }
                product.Picture = "/images/" + pictureupload.FileName;
            }
            repositoryProduct.Insert(product);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(string id)
        {
            System.IO.File.Delete(env.ContentRootPath+"\\wwwroot\\"+ repositoryProduct.GetById(id).Picture);
            repositoryProduct.Delete(id);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(string id)
        {
            return View(repositoryProduct.GetById(id));
        }
        public IActionResult Edit(string id)
        {
            var p = repositoryProduct.GetById(id);
            ViewBag.CategoryId = new SelectList(repositoryCategory.GetAll(), "_id", "CategoryName",p.CategoryId);
            ViewBag.BrandId = new SelectList(repositoryBrand.GetAll(), "_id", "BrandName",p.BrandId);
            return View(p);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, Product product)
        {

            repositoryProduct.Update(product);
            return RedirectToAction(nameof(Index));
        }
    }
}
