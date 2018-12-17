using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using AutoMapper;
using GigHub.Dtos;

namespace GigHub.Controllers.Api
{
    //[Authorize]
    public class NotificationsController : ApiController
    {
        private ApplicationDbContext _context;
        public NotificationsController()
        {
            _context = new ApplicationDbContext();
        }


        [HttpGet]
        public IEnumerable<NotificationDto> GetNotifications()
        {

            var userId = "b147771b-6081-4ff7-a803-b206b1229e92";
            //var userId = User.Identity.GetUserId();
            var notifications = _context.UserNotifications
                .Where(n => n.UserId == userId)
                .Select(n => n.Notification)
                .Include(n => n.Gig.Artist)
                .ToList();




            return notifications.Select(Mapper.Map<Notification,NotificationDto>);

            //return   notifications.Select(n => new NotificationDto
            //   {
            //       DateTime = n.DateTime,
            //       OriginalDateTime = n.OriginalDateTime,
            //       OriginalVenue = n.OriginalVenue,

            //       Gig = new GigDto
            //       {
            //           Id = n.Gig.Id,
            //           DateTime = n.Gig.DateTime,
            //           IsCancled = n.Gig.IsCancled,
            //           Venue = n.Gig.Venue,
            //           Artist = new ApplicationUserDto
            //           {
            //            Id = n.Gig.Artist.Id,
            //               Name = n.Gig.Artist.Name
            //           }
            //       }
            //   });
        }

    }
}
