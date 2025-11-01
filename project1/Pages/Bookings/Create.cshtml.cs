using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using project1.Models;
using project1.Services;

namespace project1.Pages.Bookings
{
    public class CreateModel : PageModel
    {
        private readonly InMemoryStore _store;
        private readonly IPricingService _pricing;

        public CreateModel(InMemoryStore store, IPricingService pricing)
        {
            _store = store;
            _pricing = pricing;
        }

        [BindProperty(SupportsGet = true)]
        public int PackageId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Destinations { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? DurationDays { get; set; }

        [BindProperty(SupportsGet = true)]
        public decimal? Price { get; set; }

        [BindProperty]
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string FullName { get; set; } = string.Empty;

        [BindProperty]
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public IActionResult OnGet()
        {
            var pkg = _store.Packages.FirstOrDefault(p => p.PackageID == PackageId);
            if (pkg == null) return NotFound();

            if (Price is null)
            {
                Price = _pricing.CalculateEstimatedPrice(pkg, DurationDays ?? pkg.DurationDays);
            }
            if (DurationDays is null)
            {
                DurationDays = pkg.DurationDays;
            }
            if (string.IsNullOrWhiteSpace(Destinations))
            {
                Destinations = pkg.Destinations;
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                var user = _store.Users.FirstOrDefault(u => u.Email == Email) ?? new User(FullName, Email);
                if (user.UserID == 0)
                {
                    user.UserID = _store.Users.Count + 1;
                    _store.Users.Add(user);
                }
                var booking = new Booking(user.UserID, PackageId, Price ?? 0)
                {
                    BookingID = _store.Bookings.Count + 1
                };
                _store.Bookings.Add(booking);

                TempData["Success"] = "Booking created. Proceed to secure payment (coming soon).";
                return RedirectToPage("/Packages/Details", new { id = PackageId });
            }
            catch
            {
                TempData["Error"] = "Could not create booking. Please try again.";
                return Page();
            }
        }
    }
}


