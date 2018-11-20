using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
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

			return RedirectToAction("Index","Home");
		}
	}
}