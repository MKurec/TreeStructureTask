using TreeStructure.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace TreeStructure.Infrastructure.Services
{
    public interface IJwtHandler
    {
        JwtDto CreateToken(Guid userId, string role);
    }
}
