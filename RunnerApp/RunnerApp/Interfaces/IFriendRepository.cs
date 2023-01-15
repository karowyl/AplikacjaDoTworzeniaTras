using RunnerApp.DTOs;
using RunnerApp.Entities;

namespace RunnerApp.Interfaces
{
    public interface IFriendRepository
    {
        Task<bool> RemoveFriend(int userId, int friendId);
        Task<bool> AddFriend(int userId, int friendId);
        Task<IEnumerable<Friend>> GetAllFriends(int id);
    }
}
