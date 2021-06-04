using TreeStructure.Core;
using TreeStructure.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace TreeStructure.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AplicationDbContext _context;

        public UnitOfWork(AplicationDbContext context)
        {
            _context = context;
            Categories = new CategoryRepository(_context);

        }

        public ICategoryRepository Categories { get; private set; }



        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
