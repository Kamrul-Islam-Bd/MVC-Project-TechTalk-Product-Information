using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TechTalk.ViewModels;


namespace TechTalk.Controllers
{
    public class ProductSubCategoriesController : Controller
    {
        private TechDbContext db = new TechDbContext();

        public ActionResult Index()
        {
            var subCategories = db.ProductSubCategories.ToList();
            return View(subCategories);
        }

        public ActionResult Create()
        {
            ViewBag.ProductCategoryId = new SelectList(db.ProductCategories, "ProductCategoryId", "ProCatName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductSubCategory productSubCategory)
        {
            if (ModelState.IsValid)
            {
                db.ProductSubCategories.Add(productSubCategory);
                db.SaveChanges();
                return RedirectToAction("Index", "ProductCategories");
            }

            ViewBag.ProductCategoryId = new SelectList(db.ProductCategories, "ProductCategoryId", "ProCatName", productSubCategory.ProductCategoryId);
            return View(productSubCategory);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductSubCategory productSubCategory = db.ProductSubCategories.Find(id);
            if (productSubCategory == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductCategoryId = new SelectList(db.ProductCategories, "ProductCategoryId", "ProCatName", productSubCategory.ProductCategoryId);
            return View(productSubCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductSubCategory productSubCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productSubCategory).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "ProductCategories");
            }
            ViewBag.ProductCategoryId = new SelectList(db.ProductCategories, "ProductCategoryId", "ProCatName", productSubCategory.ProductCategoryId);
            return View(productSubCategory);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductSubCategory productSubCategory = db.ProductSubCategories.Find(id);
            if (productSubCategory == null)
            {
                return HttpNotFound();
            }
            return View(productSubCategory);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // Find the sub-category to be deleted
            ProductSubCategory productSubCategory = db.ProductSubCategories.Find(id);

            if (productSubCategory != null)
            {
                // Find all products that reference this sub-category
                var products = db.Products.Where(p => p.ProductSubCategoryId == id).ToList();
                foreach (var product in products)
                {
                    product.ProductSubCategoryId = null; 
                    db.Entry(product).State = EntityState.Modified;
                }

                // Save changes to the database
                db.SaveChanges();

                // Remove the sub-category
                db.ProductSubCategories.Remove(productSubCategory);
                db.SaveChanges();
            }

            return RedirectToAction("Index", "ProductSubCategories");
        }

    }
}


