using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using AmazonLite.Core.Models;

namespace AmazonLite.DataAccess.InMemory
{
    public class ProductCategoryRepo
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productcat;
        public ProductCategoryRepo()
        {
            productcat = cache["productCategory"] as List<ProductCategory>;
            if (productcat == null)
            {
                productcat = new List<ProductCategory>();
            }
        }
        public void Commit()
        {
            cache["productCategory"] = productcat;
        }
        public void Insert(ProductCategory p)
        {
            productcat.Add(p);
        }
        public void Update(ProductCategory product)
        {
            ProductCategory ProToUpdate = productcat.Find(p => p.Id == product.Id);
            if (ProToUpdate != null)
            {
                ProToUpdate = product;
            }
            else
            {
                throw new Exception("Product No Found");
            }


        }
        public ProductCategory Find(string Id)
        {
            ProductCategory product = productcat.Find(p => p.Id == Id);
            if (product != null)
            {
                return product;
            }
            else
            {
                throw new Exception("Product No Found");
            }
        }
        public IQueryable<ProductCategory> Collection()
        {
            return productcat.AsQueryable();


        }
        public void Delete(string Id)
        {
            ProductCategory producttodel = productcat.Find(p => p.Id == Id);
            if (producttodel != null)
            {
                productcat.Remove(producttodel);
            }
            else
            {
                throw new Exception("Product No Found");
            }
        }


    }
}
