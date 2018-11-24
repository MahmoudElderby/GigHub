using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.Api
{


    [Authorize]
    public class AttendancesController : ApiController
    {
        private ApplicationDbContext _context;


        public AttendancesController()
        {
            _context = new ApplicationDbContext();
        }



        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto dto)
        {
            var gig = _context.Gigs.SingleOrDefault(g => g.Id == dto.GigId);

            if (gig == null)
                return NotFound();

            var userId = User.Identity.GetUserId();
            var exist = _context.Attendances.Any(a => a.AttendeeId == userId && a.GigId == dto.GigId);

            if (exist)
                return BadRequest("The Attendance Already exists.");

            Attendance newAttendance = new Attendance
            {
                GigId = dto.GigId,
                AttendeeId = User.Identity.GetUserId()

            };

            _context.Attendances.Add(newAttendance);
            _context.SaveChanges();
            return Ok();
        }

    }
}
