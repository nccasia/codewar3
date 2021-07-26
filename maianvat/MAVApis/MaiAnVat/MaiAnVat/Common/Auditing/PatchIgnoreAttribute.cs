using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaiAnVat.Common.Auditing
{
    /// <summary>
    /// Indicates that this field may not be updated via the DbContext.Delta extensions.
    /// </summary>
    public class PatchIgnoreAttribute : Attribute
    {
    }
}
