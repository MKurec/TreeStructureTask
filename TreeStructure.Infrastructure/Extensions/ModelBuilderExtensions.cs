using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeStructure.Core.Domain;

namespace TreeStructure.Infrastructure.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User(Guid.NewGuid(),"admin","admin","admin"),
                new User(Guid.NewGuid(),"user","user","user")
            );

        }
    }
}
