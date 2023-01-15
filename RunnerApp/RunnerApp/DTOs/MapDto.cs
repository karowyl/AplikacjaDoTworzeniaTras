namespace RunnerApp.DTOs
{
    public class MapDto
    {
        public int Id { get; set; }
        public string? Polyline { get; set; }
        public string? CreatedBy { get; set; }
        public string? City { get; set; }
        public ICollection<CommentDto>? Comments { get; set; }
    }
}
