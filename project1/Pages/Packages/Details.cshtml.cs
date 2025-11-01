using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using project1.Models;
using project1.Services;

namespace project1.Pages.Packages
{
    public class DetailsModel : PageModel
    {
        private readonly InMemoryStore _store;

        public DetailsModel(InMemoryStore store)
        {
            _store = store;
        }

        public TravelPackage? Package { get; set; }
        public List<Review> Reviews { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Package = _store.Packages.FirstOrDefault(p => p.PackageID == id);
            if (Package == null)
            {
                TempData["Error"] = "Package not found.";
                return RedirectToPage("Index");
            }
            Reviews = _store.Reviews.Where(r => r.PackageID == id).OrderByDescending(r => r.ReviewID).ToList();
            return Page();
        }
    }
}


