using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using project1.Services;
using project1.Models;

namespace project1.Pages.Reviews
{
    public class CreateModel : PageModel
    {
        private readonly InMemoryStore _store;

        public CreateModel(InMemoryStore store)
        {
            _store = store;
        }

        [BindProperty(SupportsGet = true)]
        public int PackageId { get; set; }

        [BindProperty]
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [BindProperty]
        [Range(0,5)]
        public int Rating { get; set; }

        [BindProperty]
        [StringLength(500)]
        public string? Comment { get; set; }

        public IActionResult OnGet()
        {
            var pkg = _store.Packages.FirstOrDefault(p => p.PackageID == PackageId);
            if (pkg == null) return NotFound();
            return Page();
        }

        public IActionResult OnPost()
        {
            try
            {
                if (!ModelState.IsValid) return Page();

                var user = _store.Users.FirstOrDefault(u => u.Email == Email) ?? new User("Reviewer", Email);
                if (user.UserID == 0)
                {
                    user.UserID = _store.Users.Count + 1;
                    _store.Users.Add(user);
                }

                _store.Reviews.Add(new Review
                {
                    UserID = user.UserID,
                    PackageID = PackageId,
                    Rating = Rating,
                    Comment = Comment
                });

                TempData["Success"] = "Thank you! Your review has been submitted.";
                return RedirectToPage("/Packages/Details", new { id = PackageId });
            }
            catch
            {
                TempData["Error"] = "Could not submit review right now.";
                return Page();
            }
        }
    }
}


