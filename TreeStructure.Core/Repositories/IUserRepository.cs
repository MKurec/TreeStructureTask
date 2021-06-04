using TreeStructure.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TreeStructure.Core.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetAsync(Guid id);
        Task AddAsync(User user);
        Task DeleteAsync(User user);
        Task<User> GetAsync(string name);

    }
}
