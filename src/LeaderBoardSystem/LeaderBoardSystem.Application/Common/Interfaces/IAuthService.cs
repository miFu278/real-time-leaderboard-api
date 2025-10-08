namespace LeaderboardSystem.Application.Common.Interfaces
{
    public interface IAuthService
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hash);
        string GenerateJwtToken(Guid userId, string username, string role);
    }
}
