using System;

namespace ExploreNorthwind.Models
{
    public class CategoryDTO
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }

        public CategoryDTO()
        { }
        public CategoryDTO(Category category)
        {
            CategoryID = category.CategoryID;
            CategoryName = category.CategoryName;
            Description = category.Description;
        }
    }
}
