using GigHub.Models;
using GigHub.ViewModels;
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

		public ActionResult Create()
		{


			var gigViewModel = new GigFormViewModel
			{
				Genres = _context.Genres.ToList()
			};
			return View(gigViewModel);
		}
	}
}