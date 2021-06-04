// TreeStructure.Infrastructure.Repositories.CategoryRepository
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TreeStructure.Core.Domain;
using TreeStructure.Core.Repositories;

namespace TreeStructure.Infrastructure.Repositories
{
	public class CategoryRepository : ICategoryRepository
	{
		protected readonly DbContext Context;

		private DbSet<Category> categories;

		public CategoryRepository(DbContext context)
		{
			Context = context;
			categories = Context.Set<Category>();
		}

		public async Task AddAsync(Category category)
		{
			categories.Add(category);
			Context.SaveChanges();
			await Task.CompletedTask;
		}

		public async Task<Category> GetAsync(Guid id)
		{
			return await Task.FromResult(categories.Include( x=> x.SubCategories).SingleOrDefault( x=> x.Id == id));
		}

		public async Task<IEnumerable<Category>> BrowseTreeStructureAsync()
		{
			IEnumerable<Category> flatcategories = categories.Include(x => x.SubCategories).AsEnumerable();
			IEnumerable<Category> xcategories = categories.Include(x => x.SubCategories).AsEnumerable().Where(x => !x.ParentId.HasValue);
			IEnumerable<Category> treeCategories = categories.Include(x => x.SubCategories).AsEnumerable().Where(x => !x.ParentId.HasValue).Where(x => !x.SubCategories.Any());
			foreach (Category category in xcategories)
			{
				if (category.SubCategories.Any())
				{
					treeCategories.Append(await GetChildren(category, flatcategories));
				}
			}
			return await Task.FromResult(xcategories);
		}

		public async Task<Category> GetChildren(Category parentCategory, IEnumerable<Category> categories)
		{
			Category thisCategory = categories.SingleOrDefault((Category x) => x.Id == parentCategory.Id);
			Category returnCategory = new Category(parentCategory.Id, parentCategory.Name);
			if (thisCategory.SubCategories.Any())
			{
				foreach (Category category in thisCategory.SubCategories)
				{
					returnCategory.AddSubCategory(await GetChildren(category, categories));
				}
			}
			return returnCategory;
		}

		public async Task<IEnumerable<Category>> BrowseAsync(string name = "")
		{
			IEnumerable<Category> xcategories = categories.AsEnumerable();
			if (!string.IsNullOrEmpty(name))
			{
				xcategories = xcategories.Where((Category x) => x.Name.ToLower().Contains(name.ToLower()));
			}
			return await Task.FromResult(xcategories);
		}

		public async Task DeleteAsync(Category category)
		{
			if(category.SubCategories.Any())
            {
				foreach (var subcategory in category.SubCategories.ToList()) await RecursiveDeleteAsync(subcategory);
			}
			//category.Parent.SubCategories.Remove(category);
			categories.Remove(category);
			Context.SaveChanges();
			await Task.CompletedTask;
		}

		public async Task RecursiveDeleteAsync(Category scategory)
		{
			var category = await GetAsync(scategory.Id);
			if (category.SubCategories.Any())
			{
				foreach (var subcategory in category.SubCategories.ToList()) await RecursiveDeleteAsync(subcategory);
			}
			categories.Remove(category);
			await Task.CompletedTask;
		}


		public async Task UpdateAsync(Category category)
		{
			Context.Entry(category).State = EntityState.Modified;
			Context.SaveChanges();
			await Task.CompletedTask;
		}
	}
}
