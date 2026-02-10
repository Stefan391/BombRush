using BombRush.Common.Consts;

namespace BombRush.Models
{
    public class RoomSettings
    {
        public long Id { get; set; }
        public Room Room { get; set; }

        public short Language { get; set; } = (short)eLanguage.Serbia;
        public short MinimumTurnDuration { get; set; } = 5;
        public short MaximumPromptAge { get; set; } = 2;
        public short StartingLives { get; set; } = 2;
        public short PlayerLimit { get; set; } = 16;
    }
}
