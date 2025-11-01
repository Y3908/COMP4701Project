using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace project1.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        public UserRole Role { get; set; } = UserRole.Customer;

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();

        public User()
        {
        }

        public User(string fullName, string email)
        {
            FullName = fullName;
            Email = email;
            PasswordHash = string.Empty;
            Role = UserRole.Customer;
        }

        public bool IsAdmin()
        {
            return Role == UserRole.Admin;
        }
    }
}


