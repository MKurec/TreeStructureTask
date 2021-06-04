using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeStructure.Infrastructure.DTO
{
    public class AccountDto
    {
        public Guid Id { get; set; }
        public string Role { get; set; }
        public string Name { get; set; }
    }
}
