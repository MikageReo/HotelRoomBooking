using HotelRoomBooking.Databases;
using HotelRoomBooking.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelRoomBooking.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly HotelDBContext _hotelDBContext;
        public BookingRepository(HotelDBContext hotelDBContext)
        {
            _hotelDBContext = hotelDBContext ?? throw new ArgumentNullException(nameof(hotelDBContext));
        }

        public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
        {
            return await _hotelDBContext.Bookings
                .Include(r => r.Room)
                .ToListAsync();
        }

        public async Task<Booking> CreateBookingAsync(Booking booking)
        {
            _hotelDBContext.Bookings.Add(booking);
            await _hotelDBContext.SaveChangesAsync();
            return booking;
        }
    }
}
