using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Models
{
    public class Notification
    {
        protected Notification()
        {
        }

        private Notification(Gig gig, NotificationType type)
        {
            Gig = gig ?? throw new ArgumentNullException(nameof(gig));

            DateTime = DateTime.Now;
            Type = type;
        }

        public int Id { get; private set; }
        public DateTime DateTime { get; private set; }
        public NotificationType Type { get; private set; }
        public DateTime? OriginalDateTime { get; private set; }
        public string OriginalVenue { get; private set; }

        [Required]
        public Gig Gig { get; private set; }

        public static Notification GigCreated(Gig gig)
        {
            return new Notification(gig, NotificationType.GigCreated);
        }

        public static Notification GigUpdated(Gig gig, DateTime originDateTime, string originVenue)
        {
            return new Notification(gig, NotificationType.GigCreated)
            {
                OriginalDateTime = originDateTime,
                OriginalVenue = originVenue
            };
        }

        public static Notification GigCanceled(Gig gig)
        {
            return new Notification(gig, NotificationType.GigCanceled);
        }
    }
}