using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class FollowingsController : ApiController
    {
        private ApplicationDbContext _context;
        public FollowingsController()
        {
            _context = new ApplicationDbContext();
        }


        public IHttpActionResult Follow(FollowingDto followingDto)
        {
            var userId = User.Identity.GetUserId();

            var exist = _context.Followings.Any(f => f.FollowerId == userId && f.FolloweeId == followingDto.ArtistId);

            if (exist)
                return BadRequest("You Are already following this artist");

            var follow = new Following
            {
                FollowerId = userId,
                FolloweeId = followingDto.ArtistId

            };

            _context.Followings.Add(follow);
            _context.SaveChanges();

            return Ok("Success");

        }

    }
}
