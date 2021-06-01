using TreeStructure.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TreeStructure.Infrastructure.Repositories
{
    public class AplicationDbContext : DbContext
    {
        const string connectionString = "Server=(localdb)\\mssqllocaldb;Database=TreeStructure;Trusted_Connection=True;MultipleActiveResultSets=true;";

        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString, b => b.MigrationsAssembly("TreeStructure.Api"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Name);
                entity.HasMany(x => x.SubCategories)
                      .WithOne(e => e.Parent)
                      .HasForeignKey(x => x.ParentId)
                      .IsRequired(false)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
