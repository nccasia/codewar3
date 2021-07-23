using System.Collections.Generic;

namespace AutoGenerateTestcase.Authentication.External
{
    public interface IExternalAuthConfiguration
    {
        List<ExternalLoginProviderInfo> Providers { get; }
    }
}
