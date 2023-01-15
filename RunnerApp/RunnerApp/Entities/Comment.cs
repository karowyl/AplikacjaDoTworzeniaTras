﻿namespace RunnerApp.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string? Context { get; set; }
        public string? CreatedBy { get; set; }
        public int MapId { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
