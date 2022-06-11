using ExploreNorthwindDataAccess.Models;
using System.Collections.Generic;

namespace ExploreNorthwindDataAccess.Repositories.Interfaces
{
    public interface ICategoriesRepository
    {
        List<Category> GetAll();
        byte[] GetPictureByCategoryId(int id);
        Category GetById(int id);
        void AddPicture(int id, byte[] picture);
    }
}
