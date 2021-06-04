using TreeStructure.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TreeStructure.Core.Repositories
{
    public interface ISortMainCategoriesRepository
    {
        Task<SortMainCategories> GetAsync(string path);
        Task UpdateAsync(string path);
    }
}
