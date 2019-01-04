using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigHub.Models
{
    public class UserNotification
    {
        protected UserNotification()
        {
        }

        public UserNotification(ApplicationUser user, Notification notifiaction)
        {
            User = user ?? throw new ArgumentNullException(nameof(user));
            Notifiaction = notifiaction ?? throw new ArgumentNullException(nameof(notifiaction));

            UserId = user.Id;
            NotificationId = notifiaction.Id.ToString();
        }

        [Key]
        [Column(Order = 1)]
        public string UserId { get; private set; }

        [Key]
        [Column(Order = 2)]
        public string NotificationId { get; private set; }

        public ApplicationUser User { get; private set; }

        public Notification Notifiaction { get; private set; }

        public bool IsRead { get; private set; }

        public void Read()
        {
            IsRead = true;
        }
    }
}