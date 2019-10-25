using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmazonLite.Core.Models;
using AmazonLite.DataAccess.InMemory;
using AmazonLite.Core.ViewModel;
using AmazonLite.Core.Contracts;

namespace AmazonLite.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        IRepo<Product> context;
        IRepo<ProductCategory> productcats;
        public ProductManagerController(IRepo<Product> ProductContext , IRepo<ProductCategory> ProductCatContext)
        {
            context = ProductContext;
            productcats = ProductCatContext;
        } 
        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList(); 
            return View(products);
        }
        public ActionResult Create ()
        {
            ProductManagerViewModel ViewModel = new ProductManagerViewModel();
            ViewModel.product = new Product();
            ViewModel.productcat = productcats.Collection();
            return View(ViewModel);
        }
        [HttpPost]
        public ActionResult Create(Product product)
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
        public ActionResult Edit (string Id)
        {
            Product product = context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                ProductManagerViewModel ViewModel = new ProductManagerViewModel();
                ViewModel.product = product;
                ViewModel.productcat = productcats.Collection();
                return View(ViewModel);
            }
        }
        [HttpPost]
        public ActionResult Edit (Product product, string Id)
        {
            Product ProToEdit = context.Find(Id);
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
                ProToEdit.Description = product.Description;
                ProToEdit.Image = product.Image;
                ProToEdit.Price = product.Price;
                ProToEdit.Name = product.Name;
                context.Commit();
                return RedirectToAction("Index");
            }

        }
        public ActionResult Delete(string Id)
        {
            Product ProToDel = context.Find(Id);
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
            Product ProToDel = context.Find(Id);
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