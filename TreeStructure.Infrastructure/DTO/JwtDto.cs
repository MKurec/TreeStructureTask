﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TreeStructure.Infrastructure.DTO
{
    public class JwtDto
    {
        public string Token { get; set; }
        public long Expires { get; set; }
    }
}