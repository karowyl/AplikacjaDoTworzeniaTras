using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RunnerApp.Entities;
using RunnerApp.Interfaces;

namespace RunnerApp.Data
{
    public class MapRepository : IMapRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public MapRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> AddComment(Comment comment)
        {
            _context.Comments.Add(comment);

            return await _context.SaveChangesAsync() != 0;
        }

        public async Task<bool> AddMap(Map map)
        {
            _context.Maps.Add(map);

            return await _context.SaveChangesAsync() != 0;
        }

        public async Task<IEnumerable<Map>> GetAllMaps()
        {
            return await _context.Maps
                .Select(map => map)
                .Include(u => u.Comments)
                .ToListAsync();
        }

        public async Task<IEnumerable<Map>> GetMapsCreatedByUser(string username)
        {
            return await _context.Maps
                .Where(map => map.CreatedBy == username)
                .Include(u => u.Comments)
                .ToListAsync();
        }

        public async Task<IEnumerable<Map>> GetMapsInTheCity(string city)
        {
            return await _context.Maps
                .Where(map => map.City == city)
                .Include(u => u.Comments)
                .ToListAsync();
        }

        public async Task<IEnumerable<Map>> GetMyMaps(string username)
        {
            return await _context.Maps
                .Where(map => map.CreatedBy == username)
                .Include(u => u.Comments)
                .ToListAsync();
        }

    }
}
