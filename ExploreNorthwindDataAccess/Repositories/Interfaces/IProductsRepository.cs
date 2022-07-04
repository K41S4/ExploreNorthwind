using ExploreNorthwindDataAccess.Models;
using System.Collections.Generic;

namespace ExploreNorthwindDataAccess.Repositories.Interfaces
{
    public interface IProductsRepository
    {
        void Create(Product product);
        IEnumerable<Product> Get(int maxCount);
        IEnumerable<Product> Get();
        Product GetById(int id);
        void Update(Product product);
        void Delete(int id);
    }
}