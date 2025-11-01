using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using project1.Services;
using project1.Models;

namespace project1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly InMemoryStore _store;

        public IndexModel(ILogger<IndexModel> logger, InMemoryStore store)
        {
            _logger = logger;
            _store = store;
        }

        public List<TravelPackage> Featured { get; set; } = new();

        public void OnGet()
        {
            Featured = _store.Packages.Where(p => p.IsFeatured).Take(6).ToList();
        }
    }
}
