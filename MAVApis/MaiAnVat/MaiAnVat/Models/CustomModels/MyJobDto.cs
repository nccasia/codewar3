using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaiAnVat.Models.CustomModels
{
    public class MyJobDto: JobDto
    {
        public bool IsAccepted { get; set; }
    }
}
