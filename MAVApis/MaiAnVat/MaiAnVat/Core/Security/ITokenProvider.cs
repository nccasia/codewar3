using MaiAnVat.Models;
using System;
namespace MaiVanVat.Security
{
    public interface ITokenProvider
    {
        bool ValidateToken(ref TokenIdentity tokenIdentity);
        TokenIdentity GenerateToken(User user, string userAgent, string ip, string guid, long effectiveTime);
    }
}
