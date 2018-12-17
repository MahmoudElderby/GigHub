using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GigHub.Models
{
    public class Notification
    {
        public int Id { get; private set; }

        public DateTime DateTime { get; private set; }

        public NotificationType Type { get; private set; }

        public DateTime? OriginalDateTime { get;  private set; }
        public string OriginalVenue { get; private set; }


        [Required]
        public Gig Gig { get; private set; }

        protected Notification()
        {
            
        }

        private Notification(Gig gig, NotificationType type)
        {
            if(gig ==null)
                throw  new ArgumentException();

            Gig = gig;
            DateTime = DateTime.Now;
            Type = type;
        }


        public static Notification GigCreatedNotification(Gig NewGig)
        {
            return new Notification(NewGig, NotificationType.GigCreated);
        }

        public static Notification GigUpdatedNotification(Gig NewGig, DateTime originalDateTime,
            string originalVenue)
        {
            var notification  = new Notification(NewGig,NotificationType.GigUpdated);
            notification.OriginalDateTime = originalDateTime;
            notification.OriginalVenue = originalVenue;

            return notification;
        }

        public static Notification GigCanceledNotification(Gig Gig)
        {
            return new Notification(Gig, NotificationType.GigCanceled);
        }
    }
}