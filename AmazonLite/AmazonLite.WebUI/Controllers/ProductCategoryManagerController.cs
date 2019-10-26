using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmazonLite.DataAccess.InMemory;
using AmazonLite.Core.Models;

namespace AmazonLite.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {

        ProductCategoryRepo context;
        public ProductCategoryManagerController()
        {
            context = new ProductCategoryRepo();
        }
        // GET: ProductManager
        public ActionResult Index()
        {
            List<ProductCategory> products = context.Collection().ToList();
            return View(products);
        }
        public ActionResult Create()
        {
            ProductCategory products = new ProductCategory();
            return View(products);
        }
        [HttpPost]
        public ActionResult Create(ProductCategory product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                context.Insert(product);
                context.Commit();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Edit(string Id)
        {
            ProductCategory product = context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(product);
            }
        }
        [HttpPost]
        public ActionResult Edit(ProductCategory product, string Id)
        {
            ProductCategory ProToEdit = context.Find(Id);
            if (ProToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }
                ProToEdit.Category = product.Category;
                
                context.Commit();
                return RedirectToAction("Index");
            }

        }
        public ActionResult Delete(string Id)
        {
            ProductCategory ProToDel = context.Find(Id);
            if (ProToDel == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(ProToDel);
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            ProductCategory ProToDel = context.Find(Id);
            if (ProToDel == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Index");
            }
        }

    }
}