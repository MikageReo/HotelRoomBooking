using Microsoft.EntityFrameworkCore;
using HotelRoomBooking.Models;

namespace HotelRoomBooking.Databases
{
    public class HotelDBContext : DbContext
    {
        public HotelDBContext(DbContextOptions<HotelDBContext> options) : base(options)
        {
        }

        public DbSet<Room> Rooms => Set<Room>();
        public DbSet<Booking> Bookings => Set<Booking>();
    }
}
