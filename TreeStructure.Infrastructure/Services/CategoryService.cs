using AutoMapper;
using TreeStructure.Core.Domain;
using TreeStructure.Core.Repositories;
using TreeStructure.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace TreeStructure.Infrastructure.Services
{
    public class CategoryService :ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISortMainCategoriesRepository _sortMainCategoriesRepository;

        public CategoryService(IMapper mapper, ICategoryRepository categoryRepository, ISortMainCategoriesRepository sortMainCategoriesRepository)
        {
            _mapper = mapper;
            _categoryRepository=categoryRepository;
            _sortMainCategoriesRepository = sortMainCategoriesRepository;
        }
        public async Task<CategoryDto> GetAsync(Guid id)
        {
            var @category = await _categoryRepository.GetAsync(id);
            return _mapper.Map<CategoryDto>(@category);
        }
        public async Task<IEnumerable<CategoryDto>> BrowseAsync(string name = null)
        {
            var categories = await _categoryRepository.BrowseAsync(name);
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }
        public async Task<IEnumerable<CategoryTreeDto>> CategoryTreeAsync()
        {
            var categories = await _categoryRepository.BrowseTreeStructureAsync();
            return _mapper.Map<IEnumerable<CategoryTreeDto>>(categories);
        }


        public async Task UpdateAsync(Guid id, string name)
        {
            var @category = await _categoryRepository.GetAsync(id);
            @category.SetName(name);
            await _categoryRepository.UpdateAsync(@category);
        }
        public async Task DeleteAsync(Guid id)
        {
            var @category = await _categoryRepository.GetAsync(id);
            await _categoryRepository.DeleteAsync(@category);
        }

        public async Task AddSubCategory(Guid categoryId,Guid subCategoryId, string name)
        {
            var @category = new Category(subCategoryId, name);
            var @parentcategory = await _categoryRepository.GetAsync(categoryId);
            @parentcategory.AddSubCategory(@category);
            await _categoryRepository.AddAsync(@category);
            await _categoryRepository.UpdateAsync(@parentcategory);
        }
        public async Task CreateAsync(Guid id,string name)
        {
            //var @category = await _categoryRepository.GetAsync(name);
            //if(@category != null)
            //{
            //    throw new Exception($"Category named: ' {name}' alredy exist.");
            //}
            Category @category = new (id, name);
            await _categoryRepository.AddAsync(@category);
        }

        public async Task SortCategories(bool decending, bool sortSubcategories, Guid? id, string path)
        {
            if (id != null)
            {
                var category = await _categoryRepository.GetAsync((Guid)id);
                if (sortSubcategories && category.SubCategories.Any())
                {
                    foreach(var subCatrgory in category.SubCategories )
                        await SortCategories(decending, sortSubcategories, subCatrgory.Id,path);
                }
                category.SortSubCategories(decending);
                await _categoryRepository.UpdateAsync(category);

            }
            else
            {
                var sortCatOrder = await _sortMainCategoriesRepository.GetAsync(path);
                sortCatOrder.DecendingOrder = decending;
                if (sortSubcategories)
                {
                    var category = await _categoryRepository.GetAsync((Guid)id);
                    if (sortSubcategories && category.SubCategories.Any())
                    {
                        foreach (var subCatrgory in category.SubCategories)
                            await SortCategories(decending, sortSubcategories, subCatrgory.Id, path);
                    }
                    category.SortSubCategories(decending);
                    await _categoryRepository.UpdateAsync(category);

                }
            }

            
        }
    }
}
