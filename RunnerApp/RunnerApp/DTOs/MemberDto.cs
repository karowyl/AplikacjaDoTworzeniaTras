

namespace RunnerApp.DTOs
{
    public class MemberDto
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? PhotoUrl { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? Interests { get; set; }
        public int Age { get; set; }
        public ICollection<PhotoDto>? Photos { get; set; }
    }
}
