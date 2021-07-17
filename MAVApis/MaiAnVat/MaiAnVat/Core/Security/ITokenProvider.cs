using System;
namespace MaiVanVat.Security
{
    public interface ITokenProvider
    {
        bool ValidateToken(ref TokenIdentity tokenIdentity);
        TokenIdentity GenerateToken(int userId, string username, string userAgent, string ip, string guid, long effectiveTime);
    }
}
