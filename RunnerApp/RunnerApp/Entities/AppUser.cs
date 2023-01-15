using RunnerApp.Extensions;

namespace RunnerApp.Entities
{
    public class AppUser
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; } 
        public DateTime CreatedDate { get; set; }
        public string? Interests    { get; set; }
        public DateTime DateOfBirth { get; set; }  
        public ICollection<Photo>? Photos { get; set; }
        public List<Message> MessagesSent { get; set; }
        public List<Message> MessagesReceived { get; set; }

        public List<Friend> MyFriends { get; set; }

        public List<Friend> Followers { get; set; }

        //public int GetAge()
        //{
        //    return DataOfBirth.CalculateAge();
        //}
    }
}
