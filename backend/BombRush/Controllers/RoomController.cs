using BombRush.DatabaseContext;
using BombRush.DTO.Room;
using BombRush.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BombRush.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RoomController : BaseController
    {
        ApplicationDbContext _context;

        public RoomController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("rooms")]
        [AllowAnonymous]
        public IActionResult Rooms(int page = 1)
        {
            var rooms = _context.Rooms.Include(r => r.Host)
                                      .Include(r => r.RoomPlayers)
                                      .Include(r => r.RoomSettings)
                                      .Skip((page - 1) * 20)
                                      .Take(20)
                                      .Select(r => new
                                      {
                                          code = r.Code,
                                          host = r.Host.DisplayName,
                                          players = r.RoomPlayers.Count,
                                          playerLimit = r.RoomSettings.PlayerLimit
                                      })
                                      .ToList();

            if (rooms == null)
                return Ok(new List<object>().ToList());

            return Ok(rooms);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create()
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == (UserId ?? 0));
            if (user == null)
                return BadRequest("You need to be logged in to create a room.");

            var exists = _context.Rooms.Any(x => x.HostId == user.Id);
            if (exists)
                return BadRequest("You already have a room.");

            string code = "";
            while (true)
            {
                code = GenerateRoomCode();
                if (!_context.Rooms.Any(x => x.Code == code))
                    break;
            }

            var room = new Room { HostId = user.Id, Code = code };
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();

            var roomSettings = new RoomSettings
            {
                Id = room.Id
            };

            _context.RoomSettings.Add(roomSettings);
            await _context.SaveChangesAsync();

            return Ok(new { code = code });
        }

        [HttpPut("edit")]
        public async Task<IActionResult> Edit(EditRequest req)
        {
            var room = _context.Rooms.Include(r => r.RoomSettings).FirstOrDefault(r => r.Id == req.RoomId && r.HostId == (UserId ?? 0));
            if (room == null)
                return BadRequest("Room doesn't exist.");

            var roomSettings = room.RoomSettings;
            roomSettings.Language = req.Language;
            roomSettings.MinimumTurnDuration = req.MinimumTurnDuration;
            roomSettings.MaximumPromptAge = req.MaximumPromptAge;
            roomSettings.StartingLives = req.StartingLives;
            roomSettings.PlayerLimit = req.PlayerLimit;

            _context.Update(roomSettings);
            await _context.SaveChangesAsync();

            return Ok(new { ok = true });
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete()
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == (UserId ?? 0));
            if (user == null)
                return BadRequest("You need to be logged in to delete a room.");

            var room = _context.Rooms.Include(r => r.RoomPlayers).Include(r => r.RoomSettings).FirstOrDefault(x => x.HostId == user.Id);
            if (room == null)
                return BadRequest("You don't have a room to delete");

            _context.RoomPlayers.RemoveRange(room.RoomPlayers);
            _context.RoomSettings.Remove(room.RoomSettings);
            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();

            return Ok(new { ok = true });
        }

        private string GenerateRoomCode()
        {
            string code = "";

            for (int i = 0; i < 4; i++)
            {
                code += (char)('A' + Random.Shared.Next(0, 26));
            }

            return code;
        }
    }
}
