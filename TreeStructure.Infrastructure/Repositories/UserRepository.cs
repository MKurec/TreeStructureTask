using TreeStructure.Core.Domain;
using TreeStructure.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeStructure.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        protected readonly DbContext Context;
        private DbSet<User> _users;
        public UserRepository(DbContext context)
        {
            this.Context = context;
            this._users = Context.Set<User>();
        }


        public async Task AddAsync(User user)
        {
            _users.Add(user);
            Context.SaveChanges();
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(User user)
        {
            _users.Remove(user);
            Context.SaveChanges();
            await Task.CompletedTask;
        }

        public async Task<User> GetAsync(Guid id)
        {
            var @user = await Task.FromResult(_users.SingleOrDefault(x => x.Id == id));
            return user;
        }
        public async Task<User> GetAsync(string name)
        {
            var @user = await Task.FromResult(_users.SingleOrDefault(x => x.Name.ToLower() == name.ToLower()));
            return user;
        }

    }
}
