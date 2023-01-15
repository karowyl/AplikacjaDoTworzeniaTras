using RunnerApp.Entities;

namespace RunnerApp.Interfaces
{
    public interface IMapRepository
    {
        Task<bool> AddMap(Map map);
        Task<IEnumerable<Map>> GetAllMaps();
        Task<IEnumerable<Map>> GetMyMaps(string username); 
        Task<IEnumerable<Map>> GetMapsInTheCity(string city);

        Task<IEnumerable<Map>> GetMapsCreatedByUser(string username);
        Task<bool> AddComment(Comment comment);
    }
}
