using GigHub.Models;
using System.Collections.Generic;

namespace GigHub.ViewModels
{
    public class GigsViewModel
    {
        public IEnumerable<Gig> UpCominGigs { get; set; }
        public bool ShowActions { get; set; }
        public string HeaderText { get; set; }
    }
}