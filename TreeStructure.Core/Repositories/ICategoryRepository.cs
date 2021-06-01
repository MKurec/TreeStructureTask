using TreeStructure.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TreeStructure.Core.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> BrowseAsync(string name = "");
        Task<IEnumerable<Category>> BrowseTreeStructureAsync();
        Task<Category> GetAsync(Guid id);

        Task AddAsync(Category category);
        Task UpdateAsync(Category product);
        Task DeleteAsync(Category product);
    }
}
