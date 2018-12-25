using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
	public class GigsController : Controller
	{
		private readonly ApplicationDbContext _context;
		private IEnumerable<Genre> _genres;

		public GigsController()
		{
			_context = new ApplicationDbContext();
		}

		protected override void Dispose(bool disposing)
		{
			_context.Dispose();
		}

		[Authorize]
		public ActionResult Create()
		{
	
			if (_genres == null || !_genres.Any())
				_genres = _context.Genres.ToList();

			var gigViewModel = new GigFormViewModel
			{
				Genres = _genres,
                HeadingText = "Add a Gig"
				
			};
			return View("GigForm",gigViewModel);
		}


		[HttpPost]
		[Authorize]
		[ValidateAntiForgeryToken]
		public ActionResult Create(GigFormViewModel gigViewModel)
		{
			if (!ModelState.IsValid)
			{
				if (_genres == null || !_genres.Any())
					_genres = _context.Genres.ToList();


				gigViewModel.Genres = _genres;
				return View("GigForm",gigViewModel);
			}
				

			var gig = new Gig
			{
				ArtistId = User.Identity.GetUserId(),
				GenreId = gigViewModel.Genre,
				Venue = gigViewModel.Venue,
				DateTime = gigViewModel.GetDateTime()
				
			};

			_context.Gigs.Add(gig);
			_context.SaveChanges();

			return RedirectToAction("Mine","Gigs");
		}
	    [Authorize]
	    public ActionResult Edit(int gigid)
	    {
	        var userId = User.Identity.GetUserId();
	        var gig = _context.Gigs.Single(g => g.Id == gigid && g.ArtistId == userId);

	        var viewModel = new GigFormViewModel
	        {
                Id = gig.Id,
	            Genres = _context.Genres,
	            Time = gig.DateTime.ToString("HH:mm"),
	            Date = gig.DateTime.ToString("dd MMM yyyy"),
	            Venue = gig.Venue,
	            Genre = gig.GenreId,
	            HeadingText = "Edit a Gig"


	        };
	        return View("GigForm", viewModel);
	    }


        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigFormViewModel gigViewModel)
        {
            if (!ModelState.IsValid)
            {
                gigViewModel.Genres = _context.Genres.ToList();
                return View("GigForm","Gigs");
            }

            var userId = User.Identity.GetUserId();
            var gig = _context.Gigs.Include(g=>g.Attendances.Select(a=>a.Attendee)).Single(g => g.Id == gigViewModel.Id && g.ArtistId == userId);

            gig.Modify(gigViewModel);

            _context.SaveChanges();
            
            return RedirectToAction("Mine" , "Gigs");
        }

		public ActionResult Attending()
		{
			var userId = User.Identity.GetUserId();

		    var attendances = _context.Attendances.Include(a=>a.Gig).Include(a=>a.Gig.Genre).Include(a=>a.Gig.Artist).Where(a => a.AttendeeId == userId).ToList();

		    var att = attendances.ToLookup(a => a.GigId);
 
            var upCominGigs = attendances.Select(a => a.Gig);

            
			var viewModel = new GigsViewModel
			{
				UpCominGigs = upCominGigs,
				ShowActions = User.Identity.IsAuthenticated,
				HeaderText = "Gigs I'm Attending",
                attend = att
			};

			return View("Gigs",viewModel);
		}


		public ActionResult Following()
		{
			var userId = User.Identity.GetUserId();

			var Follows = _context.Followings.Where(f => f.FollowerId == userId).ToList();

			var gigs = _context.Gigs.Include(g => g.Artist).Include(g => g.Genre).ToList().Where(g => Follows.Any(f => f.FolloweeId == g.ArtistId));

			var viewModel = new GigsViewModel
			{
			   UpCominGigs = gigs,
				ShowActions = User.Identity.IsAuthenticated,
				HeaderText = "My Followers"

			};


			return View("Gigs",viewModel);
		}


        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();

            var gigs = _context.Gigs
                .Include(g=>g.Genre)
                .Where(g => g.ArtistId == userId && g.DateTime > DateTime.Now && !g.IsCancled)
                .ToList();

            return View(gigs);
        }

	    public ActionResult Detail(int gigId)
	    {
	        var userId = User.Identity.GetUserId();
	        var gig = _context.Gigs.Include(g=>g.Artist).SingleOrDefault(g => g.Id == gigId);

	        var ViewModel = new GigDetailsViewModel {Gig = gig};

	        ViewModel.IsFollowing = _context.Followings.Any(f => f.FollowerId == userId && f.FolloweeId == gig.ArtistId);

	        ViewModel.IsAttending = _context.Attendances.Any(a => a.AttendeeId == userId && a.GigId == gig.Id);



            return View(ViewModel);
	    }
	}
}