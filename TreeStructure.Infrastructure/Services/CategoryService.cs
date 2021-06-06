using AutoMapper;
using TreeStructure.Core.Domain;
using TreeStructure.Core.Repositories;
using TreeStructure.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using TreeStructure.Infrastructure.Extensions;

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
            var @category = await _categoryRepository.GetOrFailAsync(id);
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
                SortMainCategories.Instance.DecendingOrder = decending;
                await _sortMainCategoriesRepository.UpdateAsync(path);

                if (sortSubcategories)
                {
                    var categories = await _categoryRepository.BrowseTreeStructureAsync();
                        foreach (var category in categories)
                    {
                        await SortCategories(decending, sortSubcategories, category.Id, path);
                        category.SortSubCategories(decending);
                        await _categoryRepository.UpdateAsync(category);
                    }
                            
                }
            }

            
        }

        public async Task MoveCategoryAsync(Guid idCategoryToMove, Guid? newParentId)
        {
            var categoryToMove = await _categoryRepository.GetAsync(idCategoryToMove);
            if (categoryToMove == null) throw new Exception($"Cannot find category with id: '{idCategoryToMove}' ");
            if(newParentId==null)
            {
                if (categoryToMove.ParentId != null) categoryToMove.SetParentNull(); 
            }
            else
            {
                var newParentCategory = await _categoryRepository.GetAsync((Guid)newParentId);
                if (newParentCategory == null) throw new Exception($"Cannot find category with id: '{newParentId}' ");
                if (await IsParent(idCategoryToMove, (Guid)newParentId)) 
                    throw new Exception($"Cannot move {categoryToMove.Name} with Id : {categoryToMove.Id} becouse {newParentCategory.Name} with id : {newParentCategory.Id} is it's child");
                if (categoryToMove.ParentId != null)
                {
                    var oldParent = await _categoryRepository.GetAsync((Guid)categoryToMove.ParentId);
                    oldParent.RemoveFromSubcategories(categoryToMove);
                    await _categoryRepository.UpdateAsync(oldParent);
                }
                newParentCategory.AddSubCategory(categoryToMove);
                await _categoryRepository.UpdateAsync(newParentCategory);
            }
            await _categoryRepository.UpdateAsync(categoryToMove); 
        }

        public async Task<bool> IsParent(Guid containId ,Guid id )
        {
            var category = await _categoryRepository.GetAsync(id);
            if (category.ParentId == null) return false;
            else if (category.ParentId == containId) return true;
            else return await  IsParent(containId, (Guid)category.ParentId);
        }


    }
}
