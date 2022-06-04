using ExploreNorthwindDataAccess.Models;
using System.Collections.Generic;

namespace ExploreNorthwindDataAccess.Repositories.Interfaces
{
    public interface ICategoriesRepository
    {
        List<Category> GetAll();
    }
}
