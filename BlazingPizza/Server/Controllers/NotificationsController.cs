using BlazingPizza.Server.Data;
using BlazingPizza.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlazingPizza.Server.Controllers
{
    [Route("notifications")]
    [ApiController]
    [Authorize]
    public class NotificationsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public NotificationsController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpPut("subscribe")]
        public async Task<NotificationSubscription> Subscribe(NotificationSubscription subscription)
        {
            // We're storing at most one subscription per user, so delete old ones.
            // Alternatively, you could let the user register multiple subscriptions from different browsers/devices.
            var userId = GetUserId();
            var oldSubscriptions = _db.NotificationSubscriptions.Where(e => e.UserId == userId);
            _db.NotificationSubscriptions.RemoveRange(oldSubscriptions);

            // Store new subscription
            subscription.UserId = userId;
            _db.NotificationSubscriptions.Attach(subscription);

            await _db.SaveChangesAsync();
            return subscription;
        }

        private string GetUserId()
        {
            return HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
