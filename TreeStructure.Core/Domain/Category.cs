using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeStructure.Core.Domain
{
    public class Category: Entity
    {
        public string Name { get; protected set; }
        private ISet<Category> _subCategories = new HashSet<Category>();
        public Category Parent { get; protected set; }
        public Guid? ParentId { get; protected set; }
        public bool? SortSubCategoriesInDecending{ get; protected set; }
        public IEnumerable<Category> SubCategories { get 
            {
                if (SortSubCategoriesInDecending == true) return _subCategories.OrderByDescending(x => x.Name);
                else if (SortSubCategoriesInDecending == false) return _subCategories.OrderBy(x => x.Name);
                else return _subCategories;
            } 
        }

        public Category(Guid id, string name)
        {
            Id = id;
            SetName(name);
        }
        public Category(Guid id, string name, Category @category)
        {
            Id = id;
            SetName(name);
            Parent = category;
            ParentId = category.Id;
        }
        public void AddSubCategory(Category @subCategory)
        {
            subCategory.Parent = this;
            subCategory.ParentId = this.Id;
            _subCategories.Add(@subCategory);
        }
        public void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new Exception($"Category with id'{Id}' cannot have empty name");
            }
            Name = name;
        }
        public void SortSubCategories(bool decending)
        {
            SortSubCategoriesInDecending=decending;
        }
    }
    
}
