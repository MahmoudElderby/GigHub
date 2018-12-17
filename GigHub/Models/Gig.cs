using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using GigHub.ViewModels;

namespace GigHub.Models
{
    public class Gig
    { 
        public int Id { get; set; }

        [Required]
        public string ArtistId { get; set; }
        public ApplicationUser Artist { get; set; }

        public DateTime DateTime { get; set; }

        [Required]
        [StringLength(255)]
        public string Venue { get; set; }

        [Required]
        public byte GenreId { get; set; }
        public Genre Genre { get; set; }

        public bool IsCancled { get; private set; }

        public ICollection<Attendance> Attendances { get; private set; }


        public Gig()
        {
            Attendances = new List<Attendance>();
        }


        public void Cancel()
        {
            IsCancled = true;
            var notification = Notification.GigCanceledNotification(this);

            foreach (var user in this.Attendances.Select(a => a.Attendee))
            {
                user.Notify(notification);
            }
        }

        public void Modify(GigFormViewModel gigViewModel)
        {
            var notification = Notification.GigUpdatedNotification(this,this.DateTime,this.Venue);

            this.GenreId = gigViewModel.Genre;
            this.Venue = gigViewModel.Venue;
            this.DateTime = gigViewModel.GetDateTime();
             
            foreach (var attendee in this.Attendances.Select(a => a.Attendee))
                    attendee.Notify(notification);
        }
    }
}