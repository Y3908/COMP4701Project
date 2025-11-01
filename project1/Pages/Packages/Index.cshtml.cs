using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using project1.Models;
using project1.Services;

namespace project1.Pages.Packages
{
    public class IndexModel : PageModel
    {
        private readonly InMemoryStore _store;

        public IndexModel(InMemoryStore store)
        {
            _store = store;
        }

        [BindProperty(SupportsGet = true)]
        public string? Search { get; set; }

        public List<TravelPackage> Packages { get; set; } = new();

        public void OnGet()
        {
            IEnumerable<TravelPackage> query = _store.Packages;
            if (!string.IsNullOrWhiteSpace(Search))
            {
                var term = Search.Trim();
                query = query.Where(p => p.Title.Contains(term) || p.Destinations.Contains(term));
            }
            Packages = query.OrderBy(p => p.Title).Take(60).ToList();
        }
    }
}



