using System.Linq;
using AmazonLite.Core.Models;

namespace AmazonLite.Core.Contracts
{
    public interface IRepo<T> where T : BaseEntity
    {
        IQueryable<T> Collection();
        void Commit();
        void Delete(string Id);
        T Find(string Id);
        void Insert(T p);
        void Update(T product);
    }
}