using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using project1.Services;
using project1.Models;

namespace project1.Pages.Packages
{
    public class CustomizeModel : PageModel
    {
        private readonly InMemoryStore _store;
        private readonly IPricingService _pricing;

        public CustomizeModel(InMemoryStore store, IPricingService pricing)
        {
            _store = store;
            _pricing = pricing;
        }

        [BindProperty(SupportsGet = true)]
        public int PackageId { get; set; }

        [BindProperty]
        [Required]
        [MaxLength(255)]
        [RegularExpression(@"^[\w\s,\-]+$", ErrorMessage = "Destinations can contain letters, numbers, spaces, commas and hyphens only.")]
        public string Destinations { get; set; } = string.Empty;

        [BindProperty]
        [Range(1, int.MaxValue)]
        public int DurationDays { get; set; }

        [BindProperty]
        public decimal EstimatedPrice { get; set; }

        public decimal BasePrice { get; set; }

        public IActionResult OnGet(int id)
        {
            PackageId = id;
            var pkg = _store.Packages.FirstOrDefault(p => p.PackageID == id);
            if (pkg == null) return NotFound();

            Destinations = pkg.Destinations;
            DurationDays = pkg.DurationDays;
            EstimatedPrice = _pricing.CalculateEstimatedPrice(pkg, pkg.DurationDays);
            BasePrice = pkg.BasePrice;
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Simple pricing heuristic handled also on client via jQuery
            if (DurationDays > 1)
            {
                var multiplier = 1m + (DurationDays - 1) * 0.05m;
                EstimatedPrice = decimal.Round(EstimatedPrice * multiplier, 2);
            }

            return RedirectToPage("/Bookings/Create", new { packageId = PackageId, destinations = Destinations, durationDays = DurationDays, price = EstimatedPrice });
        }
    }
}


