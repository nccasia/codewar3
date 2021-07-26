using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaiAnVat.Models.CustomModels
{
    public class JobTypeDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int DefaultTimeInHours { get; set; }
        public string ColorCode { get; set; }
        public bool? IsException { get; set; }
    }
}
