using TreeStructure.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace TreeStructure.Core.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Categories { get; }

        int Complete();
    }
}
