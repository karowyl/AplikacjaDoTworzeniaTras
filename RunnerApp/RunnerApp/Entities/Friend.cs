namespace RunnerApp.Entities
{
    public class Friend
    {
        public int Id { get; set; }
        public AppUser FriendUser { get; set; }
        public int FriendId { get; set; }
        public AppUser AppUser { get; set; }
        public int AppUserId { get; set; }
    }
}
