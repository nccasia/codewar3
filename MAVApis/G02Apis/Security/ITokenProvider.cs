using System;
namespace Hvit.Security
{
    public interface ITokenProvider
    {
        bool ValidateToken(ref TokenIdentity tokenIdentity);
        TokenIdentity GenerateToken(string username, string userAgent, string ip, string guid, long effectiveTime);
    }
}
