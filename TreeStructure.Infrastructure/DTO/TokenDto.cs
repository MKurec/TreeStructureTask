using System;
using System.Collections.Generic;
using System.Text;

namespace TreeStructure.Infrastructure.DTO
{
    public class TokenDto
    {
        public string Token { get; set; }
        public string Role { get; set; }
        public long Expires { get; set; }
    }
}
