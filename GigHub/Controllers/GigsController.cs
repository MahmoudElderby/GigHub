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
				Genres = _genres
				
			};
			return View(gigViewModel);
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
				return View(gigViewModel);
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


		public ActionResult Attending()
		{
			var userId = User.Identity.GetUserId();
			var attendings = _context.Attendances.Where(a => a.AttendeeId == userId).Select(a=>a.Gig).Include(a=>a.Artist).Include(a=>a.Genre);

			var viewModel = new GigsViewModel
			{
				UpCominGigs = attendings,
				ShowActions = User.Identity.IsAuthenticated,
				HeaderText = "Gigs I'm Attending"
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
                .Where(g => g.ArtistId == userId && g.DateTime > DateTime.Now)
                .ToList();

            return View(gigs);
        }
	}
}