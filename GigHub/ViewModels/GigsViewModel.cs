using GigHub.Models;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.ViewModels
{
    public class GigsViewModel
    {
        public IEnumerable<Gig> UpCominGigs { get; set; }
        public bool ShowActions { get; set; }
        public string HeaderText { get; set; }
        public string SearchTerm { get; set; }
        public ILookup<int, Attendance> attend { get; set; }
    }
}