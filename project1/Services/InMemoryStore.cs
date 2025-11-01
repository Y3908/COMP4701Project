using System.Collections.Generic;
using System.Linq;
using project1.Models;

namespace project1.Services
{
    public class InMemoryStore
    {
        public List<User> Users { get; } = new();
        public List<TravelPackage> Packages { get; } = new();
        public List<Booking> Bookings { get; } = new();
        public List<Review> Reviews { get; } = new();

        public InMemoryStore()
        {
            Seed();
        }

        private void Seed()
        {
            if (Packages.Any()) return;

            Packages.AddRange(new[]
            {
                new TravelPackage("Tour in Muscat","Explore the Grand Mosque, Royal Opera House and old Muscat.",1,30,"Muscat,Grand Mosque,Opera House"){ ImageUrl="/uploads/muscat2.jpg", IsFeatured=true },
                new TravelPackage("Nizwa fort and souq","Visit Nizwa fort and the bustling traditional souq.",1,40,"Nizwa,Fort,Souq"){ ImageUrl="/uploads/nizwa.jpg", IsFeatured=true },
                new TravelPackage("Tour in Salalah","Khareef greenery, waterfalls and beaches in Salalah.",1,35,"Salalah,Waterfalls,Beaches"){ ImageUrl="/uploads/salalah.jpg", IsFeatured=true },
                new TravelPackage("Wadi Shab","Hike and swim in turquoise pools at Wadi Shab.",1,50,"Wadi Shab,Tiwi"){ ImageUrl="/uploads/wadiShab.jpg", IsFeatured=true },
                new TravelPackage("JebalShams and WadiGhoul","Grand Canyon of Oman views and mountain villages.",1,55,"Jabal Shams,Wadi Ghul"){ ImageUrl="/uploads/JebalShams%20and%20WadiGhoul.jpg", IsFeatured=true },
                new TravelPackage("Bidiya Desert Camp","Wahiba Sands dunes, sunset and Bedouin camp.",1,45,"Bidiya,Wahiba Sands"){ ImageUrl="/uploads/Bidiya.jpeg", IsFeatured=true }
            });

            // Assign stable IDs
            int id = 1;
            foreach (var p in Packages)
            {
                p.PackageID = id++;
            }
        }
    }
}


