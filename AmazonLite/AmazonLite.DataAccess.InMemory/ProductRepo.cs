using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using AmazonLite.Core.Models;

namespace AmazonLite.DataAccess.InMemory
{
    public class ProductRepo
    {
        ObjectCache cache = MemoryCache.Default;
        List<Product> products;
        public ProductRepo ()
        {
            products = cache["products"] as List<Product>;
            if (products == null)
            {
                products = new List<Product>();
            }
        }
        public void Commit()
        {
            cache["products"] = products;
        } 
        public void Insert(Product p)
        {
            products.Add(p);
        }
        public void Update(Product product)
        {
            Product ProToUpdate = products.Find(p => p.Id == product.Id);
            if (ProToUpdate != null)
            {
                ProToUpdate = product;
            }
            else
            {
                throw new Exception("Product No Found");
            }
           

        }
        public Product Find(string Id)
        {
            Product product = products.Find(p => p.Id ==Id);
            if (product != null)
            {
                return product;
            }
            else
            {
                throw new Exception("Product No Found");
            }
        }
        public IQueryable<Product> Collection()
        {
            return products.AsQueryable();


        }
        public void Delete(string Id)
        {
            Product producttodel = products.Find(p => p.Id == Id);
            if (producttodel != null)
            {
                products.Remove(producttodel);
            }
            else
            {
                throw new Exception("Product No Found");
            }
        }


    }
}
