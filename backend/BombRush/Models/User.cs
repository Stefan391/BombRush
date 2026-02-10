namespace BombRush.Models
{
    public class User
    {
        public long Id { get; set; }
        public required string DisplayName { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
        public string? ProfilePicture { get; set; }
        public DateTime RegistrationDate { get; set; }

        public ICollection<MessageHistory> MessageHistory { get; set; } = new List<MessageHistory>();
    }
}
