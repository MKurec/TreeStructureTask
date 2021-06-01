using TreeStructure.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TreeStructure.Infrastructure.Services
{
    public interface ICategoryService
    {
        Task<CategoryDto> GetAsync(Guid id);

        Task<IEnumerable<CategoryDto>> BrowseAsync(string name = null);
        Task<IEnumerable<CategoryTreeDto>> CategoryTreeAsync();
        Task UpdateAsync(Guid id, string name);
        Task DeleteAsync(Guid id);
        Task AddSubCategory(Guid categoryId, Guid subCategoryId, string name);
        Task CreateAsync(Guid id, string name);
    }
}
