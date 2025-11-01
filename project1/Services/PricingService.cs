using project1.Models;

namespace project1.Services
{
    public interface IPricingService
    {
        decimal CalculateEstimatedPrice(TravelPackage package, int durationDays);
    }

    public class PricingService : IPricingService
    {
        public decimal CalculateEstimatedPrice(TravelPackage package, int durationDays)
        {
            if (package == null) return 0m;
            return package.CalculateEstimatedPrice(durationDays);
        }
    }
}



