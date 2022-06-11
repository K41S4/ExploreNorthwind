using ExploreNorthwindDataAccess.Models;
using ExploreNorthwindDataAccess.NorthwindDB;
using ExploreNorthwindDataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExploreNorthwindDataAccess.Repositories
{
    public class CategoriesRepository: ICategoriesRepository
    {
        private INorthwindContext Context { get; }
        public CategoriesRepository(INorthwindContext northwindContext)
        {
            this.Context = northwindContext;
        }

        public List<Category> GetAll()
        {
            return Context.Categories.ToList();
        }

        public Category GetById(int id)
        {
            return Context.Categories.Where(w => w.CategoryID == id).First();
        }

        public byte[] GetPictureByCategoryId(int id)
        {
            var category = this.GetById(id);

            if (category.Picture == null)
            {
                return null;
            }

            byte[] cleanedPicture = new byte[category.Picture.Length - 78];
            Array.Copy(category.Picture, 78, cleanedPicture, 0, cleanedPicture.Length);
            return cleanedPicture;
        }

        public void AddPicture(int id, byte[] picture)
        {
            var category = this.GetById(id);
            if (category == null) return;

            byte[] pictureWithGarbage = new byte[picture.Length + 78];
            Random rnd = new Random();
            rnd.NextBytes(pictureWithGarbage);
            Array.Copy(picture, 0, pictureWithGarbage, 78, picture.Length);

            category.Picture = pictureWithGarbage;
            Context.SaveChanges();
        }
    }
}
