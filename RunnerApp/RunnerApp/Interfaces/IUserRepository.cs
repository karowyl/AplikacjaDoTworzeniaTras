using RunnerApp.DTOs;
using RunnerApp.Entities;

namespace RunnerApp.Interfaces
{
    public interface IUserRepository
    {
        void Update(AppUser user);
        Task<bool> SaveAllAsync();
        Task<IEnumerable<AppUser>> GetUsersAsync(int id);
        Task<AppUser> GetUserByIdAsync(int id);
        Task<AppUser> GetUserByUsernameAsync(string username);
        Task<IEnumerable<MemberDto>> GetMembersAsync(string username);
        Task<MemberDto> GetMemberAsync(string username);
        Task<IEnumerable<AppUser>> FindUserAsync(string keyWord);
    }
}
