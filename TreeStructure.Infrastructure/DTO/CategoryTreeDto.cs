using TreeStructure.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace TreeStructure.Infrastructure.DTO
{
   public class CategoryTreeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<CategoryTreeDto> SubCategories { get; set; }
    }
}
