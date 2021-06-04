using TreeStructure.Core.Domain;
using TreeStructure.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TreeStructure.Infrastructure.Extensions
{
    public static class RepositoryExtensions
    {
        public static async Task<User> GetOrFailAsync(this IUserRepository repository, Guid id)
        {
            var user = await repository.GetAsync(id);
            if (user == null)
            {
                throw new Exception($"User with id: '{id}' does not exist.");
            }

            return user;
        }

        public static async Task<Category> GetOrFailAsync(this ICategoryRepository repository, Guid id)
        {
            var category = await repository.GetAsync(id);
            if (category == null)
            {
                throw new Exception($"Category with id: '{id}' does not exist.");
            }

            return category;
        }

    }
}
