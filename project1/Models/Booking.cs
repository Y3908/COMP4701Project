using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project1.Models
{
    public class Booking
    {
        [Key]
        public int BookingID { get; set; }

        [Required]
        public int UserID { get; set; }
        public User? User { get; set; }

        [Required]
        public int PackageID { get; set; }
        public TravelPackage? Package { get; set; }

        public DateTime BookingDate { get; set; } = DateTime.UtcNow;

        [Column(TypeName = "decimal(10,2)")]
        [Range(0, 99999999.99)]
        public decimal TotalPrice { get; set; }

        [Required]
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;

        public Booking()
        {
        }

        public Booking(int userId, int packageId, decimal totalPrice)
        {
            UserID = userId;
            PackageID = packageId;
            TotalPrice = totalPrice;
            PaymentStatus = PaymentStatus.Pending;
        }

        public void MarkPaid()
        {
            PaymentStatus = PaymentStatus.Completed;
        }

        public void MarkFailed()
        {
            PaymentStatus = PaymentStatus.Failed;
        }
    }
}


