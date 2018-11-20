using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
	public class GigsController : Controller
	{
		private readonly ApplicationDbContext _context;

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
	        var gigViewModel = new GigFormViewModel
	        {
	            Genres = _context.Genres.ToList()
	        };
	        return View(gigViewModel);
	    }

	    [HttpPost]
		[Authorize]
		public ActionResult Create(GigFormViewModel gigViewModel)
		{
			if (!ModelState.IsValid)
				return View(gigViewModel);

			var gig = new Gig
			{
				ArtistId = User.Identity.GetUserId(),
				GenreId = gigViewModel.Genre,
				Venue = gigViewModel.Venue,
				DateTime = gigViewModel.DateTime
				
			};

			_context.Gigs.Add(gig);
			_context.SaveChanges();

			return RedirectToAction("Index","Home");
		}
	}
}