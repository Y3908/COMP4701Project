using System.ComponentModel.DataAnnotations;

namespace project1.Models
{
    public class Review
    {
        [Key]
        public int ReviewID { get; set; }

        [Required]
        public int UserID { get; set; }
        public User? User { get; set; }

        [Required]
        public int PackageID { get; set; }
        public TravelPackage? Package { get; set; }

        [Range(0, 5)]
        public int Rating { get; set; }

        public string? Comment { get; set; }
    }
}



