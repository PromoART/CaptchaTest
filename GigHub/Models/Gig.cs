using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GigHub.Models
{
    public class Gig
    {
        public Gig()
        {
            Attendances = new Collection<Attendance>();
        }

        public Gig(string userId, DateTime dateTime, byte genre, string venue)
        {
            Id = int.Parse(userId);
            DateTime = dateTime;
            GenreId = genre;
            Venue = venue;
        }

        public int Id { get; set; }

        public ApplicationUser Artist { get; set; }

        [Required]
        public string ArtistId { get; set; }

        public DateTime DateTime { get; set; }

        [Required]
        [StringLength(255)]
        public string Venue { get; private set; }

        public Genre Genre { get; set; }

        [Required]
        public byte GenreId { get; private set; }

        public bool IsCanceled { get; private set; }

        public ICollection<Attendance> Attendances { get; private set; }

        public void Cancel()
        {
            IsCanceled = true;

            foreach (var attendee in Attendances.Select(x => x.Attendee))
                attendee.Notify(Notification.GigCanceled(this));
        }

        public void Update(DateTime dateTime, byte genreId, string venue)
        {
            DateTime = dateTime;
            GenreId = genreId;
            Venue = venue;

            foreach (var attendee in Attendances.Select(x => x.Attendee))
                attendee.Notify(Notification.GigUpdated(this, DateTime, Venue));
        }
    }
}