using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project1.Models
{
    public class TravelPackage
    {
        [Key]
        public int PackageID { get; set; }

        [Required]
        [MaxLength(150)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Range(1, int.MaxValue)]
        public int DurationDays { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        [Range(0, 99999999.99)]
        public decimal BasePrice { get; set; }

        [Required]
        [MaxLength(255)]
        public string Destinations { get; set; } = string.Empty; // comma-separated list for v1

        [MaxLength(500)]
        public string? ImageUrl { get; set; }

        public bool IsFeatured { get; set; }

        public int? CreatedBy { get; set; }
        public User? CreatedByUser { get; set; }

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();

        public TravelPackage()
        {
        }

        public TravelPackage(string title, string description, int durationDays, decimal basePrice, string destinations)
        {
            Title = title;
            Description = description;
            DurationDays = durationDays;
            BasePrice = basePrice;
            Destinations = destinations;
        }

        public decimal CalculateEstimatedPrice(int durationDays)
        {
            if (durationDays <= 1) return BasePrice;
            var multiplier = 1m + (durationDays - 1) * 0.05m;
            return decimal.Round(BasePrice * multiplier, 2);
        }
    }
}


