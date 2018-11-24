using GigHub.Models;
using GigHub.ViewModels;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();

        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            var upComingGigs = _context.Gigs.Include(g=>g.Genre).Include(g=>g.Artist).Where(g=>g.DateTime>DateTime.Now);

            var viewModel = new GigsViewModel
            {
                UpCominGigs = upComingGigs,
                ShowActions = User.Identity.IsAuthenticated,
                HeaderText = "UpComing Gigs"
            };

            return View("Gigs",viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}