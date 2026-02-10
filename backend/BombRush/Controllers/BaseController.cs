using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BombRush.Controllers
{
    public class BaseController : ControllerBase
    {
        public long? UserId
        {
            get
            {
                var id = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid);
                if (id == null)
                    return null;

                if (!long.TryParse(id.Value, out long userId))
                    return null;

                return userId;
            }
        }

        public string? RefreshToken
        {
            get
            {
                return Request.Cookies["RefreshToken"];
            }
        }
    }
}
