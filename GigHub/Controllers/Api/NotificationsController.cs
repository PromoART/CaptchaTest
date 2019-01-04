using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using GigHub.DTO;
using GigHub.Models;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class NotificationsController : ApiController
    {
        private ApplicationDbContext _context;

        public NotificationsController()
        {
            _context = new ApplicationDbContext();
        }

        public IEnumerable<NotificationDto> GetNewNotifications()
        {
            var userId = User.Identity.GetUserId();

            var notifications = _context.UserNotifications
                .Where(x => x.UserId == userId && !x.IsRead)
                .Select(x => x.Notifiaction)
                .Include(x => x.Gig.Artist)
                .ToList();

            return notifications.Select(Mapper.Map<Notification, NotificationDto>);
        }
    }
}