using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaiAnVat.Common
{
    /// <summary>
    /// Indicates that an object to be injected into a controller should only live per HTTP request
    /// and be disposed of when the request terminates.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public class RequestScopeAttribute : Attribute
    {
    }
}
