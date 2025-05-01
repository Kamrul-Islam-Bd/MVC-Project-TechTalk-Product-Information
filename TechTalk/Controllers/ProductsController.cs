using System.IO;
using System.Linq;
using System.Net;
using System.Data.Entity;
using System.Web.Mvc;
using TechTalk.ViewModels;
using System.Web;
using System;
using System.Web.Mvc.Routing;
using System.Web.Security;
using TechTalk.Models;


namespace TechTalk.Controllers
{
    public class ProductsController : Controller
    {
        private TechDbContext db = new TechDbContext();

        public ActionResult Index()
        {
            var products = db.Products.Include("ProductCategory").Include("ProductSubCategory").Include("Program").ToList();
            return View(products);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.ProductCategoryId = new SelectList(db.ProductCategories, "ProductCategoryId", "ProCatName");
            ViewBag.ProductSubCategoryId = new SelectList(db.ProductSubCategories, "ProductSubCategoryId", "ProSubCatName");
            ViewBag.ProgramId = new SelectList(db.Programs, "ProgramId", "ProgramName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductViewModel pvm, int[] ProductCategoryId, int[] ProductSubCategoryId, int[] ProgramId)
        {
            if (ModelState.IsValid)
            {
                Product product = new Product()
                {
                    ProductName = pvm.ProductName,
                    ProductDescription = pvm.ProductDescription,
                    Price = pvm.Price,
                    StockQuantity  = pvm.StockQuantity,
                    ProductCategoryId=pvm.ProductCategoryId,
                    ProductSubCategoryId=pvm.ProductSubCategoryId,
                    ProgramId=pvm.ProgramId

                };


                //Image Upload
                HttpPostedFileBase file = pvm.PictureFile;
                if (file != null)
                {
                    string fileName=DateTime.Now.Ticks.ToString()+Path.GetExtension(file.FileName);
                    string filePath = Path.Combine("/Images", fileName);
                    file.SaveAs(Server.MapPath(filePath));
                    product.PictureUrl = filePath;
                }
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
                               
            }
            ViewBag.ProductCategoryId = new SelectList(db.ProductCategories, "ProductCategoryId", "ProCatName", pvm.ProductCategoryId);
            ViewBag.ProductSubCategoryId = new SelectList(db.ProductSubCategories, "ProductSubCategoryId", "ProSubCatName", pvm.ProductSubCategoryId);
            ViewBag.ProgramId = new SelectList(db.Programs, "ProgramId", "ProgramName", pvm.ProgramId);
            return View(pvm);
        }

        public ActionResult Edit(int? id)
        {
            Product product = db.Products.First(x => x.ProductId == id);
            var procat = db.ProductCategories.Where(x => x.ProductCategoryId == id).ToList();
            var proSubCat = db.ProductSubCategories.Where(x => x.ProductSubCategoryId == id).ToList();
            var program = db.Programs.Where(x => x.ProgramId == id).ToList();
            
            ProductViewModel pvm = new ProductViewModel()
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                ProductDescription = product.ProductDescription,
                Price = product.Price,
                PictureUrl=product.PictureUrl,
                

            };

            if (procat.Count() > 0 && proSubCat.Count()>0 && program.Count()>0)
            {
                foreach (var item in procat)
                {
                    pvm.CategoryList.Add(item.ProductCategoryId);
                }
                foreach (var item in proSubCat)
                {
                    pvm.SubCatList.Add(item.ProductSubCategoryId);
                }
                foreach (var item in program)
                {
                    pvm.ProgramList.Add(item.ProgramId);
                }
            }

            ViewBag.ProductCategoryId = new SelectList(db.ProductCategories, "ProductCategoryId", "ProCatName", product.ProductCategoryId);
            ViewBag.ProductSubCategoryId = new SelectList(db.ProductSubCategories, "ProductSubCategoryId", "ProSubCatName", product.ProductSubCategoryId);
            ViewBag.ProgramId = new SelectList(db.Programs, "ProgramId", "ProgramName", product.ProgramId);
            return View(pvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TechTalk.ViewModels.ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                string fileName = model.PictureUrl;
                if (model.PictureFile != null)
                {
                    fileName = Path.GetFileName(model.PictureFile.FileName);
                    string path = Path.Combine(Server.MapPath("~/Images"), fileName);
                    model.PictureFile.SaveAs(path);
                }

                var product = db.Products.Find(model.ProductId);
                if (product != null)
                {
                    product.ProductName = model.ProductName;
                    product.ProductDescription = model.ProductDescription;
                    product.Price = model.Price;
                    product.StockQuantity = model.StockQuantity;
                    product.PictureUrl = fileName != null ? "/Images/" + fileName : null;
                    product.ProductSubCategoryId = model.ProductSubCategoryId;
                    product.ProgramId = model.ProgramId;

                    db.Entry(product).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            ViewBag.ProductSubCategoryId = new SelectList(db.ProductSubCategories, "ProductSubCategoryId", "ProSubCatName", model.ProductSubCategoryId);
            ViewBag.ProgramId = new SelectList(db.Programs, "ProgramId", "ProgramName", model.ProgramId);
            return View(model);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Details(int id)
        {
            var product = db.Products
                            .Include("ProductCategory")
                            .Include("ProductSubCategory")
                            .Include("Program")
                            .Include("Reviews")
                            .FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return HttpNotFound();
            }

            // Increment view count
            product.ViewCount++;
            db.SaveChanges();

            return View(product);
        }

        [HttpPost]
        public ActionResult AddReview(int productId, string reviewerName, string content, int rating)
        {
            var product = db.Products.Find(productId);
            if (product == null)
            {
                return HttpNotFound();
            }

            var review = new Review
            {
                ProductId = productId,
                ReviewerName = reviewerName,
                Content = content,
                Rating = rating
            };

            db.Reviews.Add(review);
            product.Reviews.Add(review);

            // Update product rating
            product.TotalRatings++;
            product.AverageRating = product.Reviews.Average(r => r.Rating);

            db.SaveChanges();

            return RedirectToAction("Details", new { id = productId });
        }

        // Action to add product to cart
        [HttpPost]
        public ActionResult AddToCart(int id)
        {
            var product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            // Increment cart add count
            product.CartAddCount++;
            db.SaveChanges();

            // Logic to add product to cart (not shown)

            return RedirectToAction("Cart");
        }

        // Action to display popular products
        public ActionResult PopularProducts()
        {
            var popularProducts = db.Products
                .OrderByDescending(p => p.ViewCount + p.CartAddCount)
                .Take(2)
                .ToList();

            return View(popularProducts);
        }
    }
}


