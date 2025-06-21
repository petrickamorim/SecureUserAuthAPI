using SecureUserAuthAPI.Models;

namespace SecureUserAuthAPI.Services
{
    public interface IAuthService
    {
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
        string CreateToken(User user);
        RefreshToken GenerateRefreshToken();
    }
}
