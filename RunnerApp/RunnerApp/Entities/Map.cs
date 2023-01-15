
namespace RunnerApp.Entities
{
    public class Map
    {
        public int Id { get; set; }
        public string? Polyline { get; set; }
        public string? CreatedBy { get; set; }
        public string? City { get; set; }
        public ICollection<Comment>? Comments { get; set; }
    }
}
