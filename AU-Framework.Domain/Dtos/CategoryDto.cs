﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AU_Framework.Domain.Dtos
{
    public sealed class CategoryDto
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; }
    }
}
