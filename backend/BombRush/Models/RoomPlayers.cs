namespace BombRush.Models
{
    public class RoomPlayers
    {
        public long Id { get; set; }
        public bool IsSpectating { get; set; }

        public long UserId { get; set; }
        public required User User { get; set; }
        public long RoomId { get; set; }
        public required Room Room { get; set; }
    }
}
