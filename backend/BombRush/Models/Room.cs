namespace BombRush.Models
{
    public class Room
    {
        public long Id { get; set; }
        public required string Code { get; set; }

        public long HostId { get; set; }
        public User Host { get; set; }
        public RoomSettings RoomSettings { get; set; }

        public ICollection<RoomPlayers> RoomPlayers { get; set; } = new List<RoomPlayers>();
        public ICollection<MessageHistory> MessageHistory { get; set; } = new List<MessageHistory>();
    }
}
