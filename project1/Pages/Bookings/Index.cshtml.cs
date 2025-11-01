using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using project1.Services;
using project1.Models;

namespace project1.Pages.Bookings
{
    public class IndexModel : PageModel
    {
        private readonly InMemoryStore _store;

        public IndexModel(InMemoryStore store)
        {
            _store = store;
        }

        public List<TravelPackage> Packages { get; set; } = new();

        public void OnGet()
        {
            Packages = _store.Packages.OrderBy(p => p.Title).ToList();
        }
    }
}


