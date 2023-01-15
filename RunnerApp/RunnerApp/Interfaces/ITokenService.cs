using RunnerApp.Entities;

namespace RunnerApp.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
