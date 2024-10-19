using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsNewsWebApp.Data;
using SportsNewsWebApp.Models;
using System;
using System.Threading.Tasks;

namespace SportsNewsWebApp.Controllers
{
    public class NotificationsController : Controller
    {
        private readonly AppDbContext _context;

        public NotificationsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> SendNotification(string message)
        {
            var notification = new Notification
            {
                Message = message,
                CreatedDate = DateTime.UtcNow
            };

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Admin");
        }

        [HttpGet]
        public IActionResult GetLatestNotifications()
        {
            // Get the current time minus 10 seconds
            var lastTenSeconds = DateTime.UtcNow.AddSeconds(-10);

            // Get notifications from the last 10 seconds
            var notifications = _context.Notifications
                .Where(n => n.CreatedDate >= lastTenSeconds)
                .ToList();

            return Json(notifications);
        }
    }
}
