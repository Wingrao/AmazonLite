using AmazonLite.Core.Contracts;
using AmazonLite.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace AmazonLite.DataAccess.InMemory
{
    public class InMemoryRepo<T> : IRepo<T> where T : BaseEntity
    {
        ObjectCache cache = MemoryCache.Default;
        List<T> items;
        string ClassName;
        public InMemoryRepo()
        {
            ClassName = typeof(T).Name;
            items = cache[ClassName] as List<T>;
            if (items == null)
            {
                items = new List<T>();
            }
        }
        public void Commit()
        {
            cache[ClassName] = items;
        }
        public void Insert(T p)
        {
            items.Add(p);
        }
        public void Update(T product)
        {
            T ProToUpdate = items.Find(p => p.Id == product.Id);
            if (ProToUpdate != null)
            {
                ProToUpdate = product;
            }
            else
            {
                throw new Exception("Product No Found");
            }


        }
        public T Find(string Id)
        {
            T product = items.Find(p => p.Id == Id);
            if (product != null)
            {
                return product;
            }
            else
            {
                throw new Exception("Product No Found");
            }
        }
        public IQueryable<T> Collection()
        {
            return items.AsQueryable();


        }
        public void Delete(string Id)
        {
            T producttodel = items.Find(p => p.Id == Id);
            if (producttodel != null)
            {
                items.Remove(producttodel);
            }
            else
            {
                throw new Exception("Product No Found");
            }
        }


    
}
}
