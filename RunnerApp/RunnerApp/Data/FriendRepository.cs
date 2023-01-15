using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RunnerApp.DTOs;
using RunnerApp.Entities;
using RunnerApp.Interfaces;

namespace RunnerApp.Data
{
    public class FriendRepository : IFriendRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public FriendRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> AddFriend(int userId, int friendId)
        {
            _context.Friends
                .Add(new Friend()
                {
                    AppUserId = userId,
                    FriendId = friendId,
                });

            return await _context.SaveChangesAsync() != 0;
        }

        public async Task<IEnumerable<Friend>> GetAllFriends(int id)
        {
            return await _context.Friends
                .Where(friend => friend.AppUserId == id && friend.FriendId != id)
                .Include(u => u.FriendUser)
                .ThenInclude(p => p.Photos)
                .ToListAsync();
        }

        public async Task<bool> RemoveFriend(int userId, int friendId)
        {
            var friend =  await _context.Friends
                .Where(x => x.FriendId == friendId && x.AppUserId == userId)
                .SingleOrDefaultAsync();

            if(friend != null)
            {
                _context.Friends.Remove(friend);
            }

            return await _context.SaveChangesAsync() != 0;
        }
    }
}
