// TreeStructure.Infrastructure.Repositories.CategoryRepository
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TreeStructure.Core.Domain;
using TreeStructure.Core.Repositories;

namespace TreeStructure.Infrastructure.Repositories
{
	public class SortMainCategoriesRepository : ISortMainCategoriesRepository
	{

		public async Task<SortMainCategories> GetAsync(string path)
		{
			TextReader reader = null;
			bool? decending = null;
			try
			{
				reader = new StreamReader(Path.Combine(path + "\\SortingOrderInfo\\" + "sortingorder.json"));
				var fileContents = await reader.ReadToEndAsync();
				decending = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType(fileContents, decending);
			}
			finally
			{
				if (reader != null)
					reader.Close();
			}
			SortMainCategories.Instance.SetOrder(decending);
			SortMainCategories sortMainCategories = new();
			return sortMainCategories;
		}



		public async Task UpdateAsync(string path)
		{
			var sortOrder = await GetAsync(path);
			TextWriter writer = null;
			try
			{
				var contentsToWriteToFile = Newtonsoft.Json.JsonConvert.SerializeObject(sortOrder.DecendingOrder);
				writer = new StreamWriter(Path.Combine(path + "\\SortingOrderInfo\\" + "sortingorder.json"), false);
				writer.Write(contentsToWriteToFile);
			}
			finally
			{
				if (writer != null)
					writer.Close();
			}
			await Task.CompletedTask;
		}
	}
}
